#region LGPL License
/*----------------------------------------------------------------------------
* This file (CK.Releaser\Model\VersionFileManager.cs) is part of CiviKey. 
*  
* CiviKey is free software: you can redistribute it and/or modify 
* it under the terms of the GNU Lesser General Public License as published 
* by the Free Software Foundation, either version 3 of the License, or 
* (at your option) any later version. 
*  
* CiviKey is distributed in the hope that it will be useful, 
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the 
* GNU Lesser General Public License for more details. 
* You should have received a copy of the GNU Lesser General Public License 
* along with CiviKey.  If not, see <http://www.gnu.org/licenses/>. 
*  
* Copyright © 2007-2014, 
*     Invenietis <http://www.invenietis.com>,
*     In’Tech INFO <http://www.intechinfo.fr>,
* All rights reserved. 
*-----------------------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CK.Core;

namespace CK.Releaser
{

    public class VersionFileManager
    {
        public readonly Workspace Workspace;

        readonly List<VFile> _files;
        VFile _sharedAssemblyInfo;


        public enum VersionAttributeStatus
        {
            MayExistOrNot,
            MustExist,
            MustNotExist
        }

        /// <summary>
        /// Supported by objects that depends on a VFile. 
        /// </summary>
        public interface IVersionedByVFile
        {
            void OnVersionChanged();
        }

        /// <summary>
        /// Maps a file that may contain Version information: the SharedAssemblyInfo file,
        /// any file named AssemblyInfo.cs, VersionInfo.cs, AssemblyVersionInfo.cs or SharedVersionInfo.cs.
        /// This should be ready to manage complex versioning mode (one day).
        /// </summary>
        public class VFile : IFile
        {
            readonly string _fullPath;
            readonly string _solutionBasedPath;
            List<IVersionedByVFile> _versionedBy;

            readonly VersionFileManager _manager;
            VersionAttributeStatus _attrStatus;
            string _versionStringRead;
            Version _versionRead;
            string _source;
            bool _isDirty;

            internal VFile( VersionFileManager manager, string fullPath, bool isTheSharedAssemblyInfo )
            {
                _manager = manager;
                _fullPath = fullPath;
                _solutionBasedPath = fullPath.Substring( _manager.Workspace.WorkspacePath.Length );
                if( isTheSharedAssemblyInfo ) _attrStatus = VersionFileManager.VersionAttributeStatus.MustExist;
            }

            public VersionAttributeStatus VersionAttributeStatus
            {
                get { return _attrStatus; }
            }

            internal void Register( IVersionedByVFile o )
            {
                if( _versionedBy == null ) _versionedBy = new List<IVersionedByVFile>();
                Debug.Assert( o != null && !_versionedBy.Contains( o ) );
                _versionedBy.Add( o );
            }

            public string Source
            {
                get 
                { 
                    if( _source == null )
                    {
                        _source = File.ReadAllText( _fullPath );
                        _isDirty = false;
                    }
                    return _source;  
                }
                set
                {
                    if( value == null ) throw new ArgumentNullException();
                    if( _source != value )
                    {
                        _source = value;
                        _isDirty = true;
                        _versionStringRead = null;
                        _versionRead = null;
                    }
                }
            }

            public bool IsDirty
            {
                get { return _isDirty; }
            }

            public void Save( IActivityMonitor m )
            {
                if( _isDirty )
                {
                    try
                    {
                        m.Info().Send( "Saving file '{0}'.", WorkspaceBasedPath );
                        File.WriteAllText( _fullPath, _source );
                        _isDirty = false;
                    }
                    catch( Exception ex )
                    {
                        m.Error().Send( ex );
                    }
                }
            }

            public string WorkspaceBasedPath
            {
                get { return _solutionBasedPath; }
            }

            public string VersionString
            {
                get
                {
                    if( _versionStringRead == null )
                    {
                        _versionStringRead = VersionUtil.ReadVersionAttribute( Source );
                    }
                    return _versionStringRead;
                }
            }

            /// <summary>
            /// Gets or sets the version.
            /// Settings requires that a version already exists and that the value exactly respects the Major.Minor.Patch form.
            /// </summary>
            public Version Version
            {
                get 
                { 
                    if( _versionRead == null && VersionString != null )
                    {
                        Version.TryParse( _versionStringRead, out _versionRead );
                    }
                    return _versionRead; 
                }
                set 
                { 
                    if( value != Version )
                    {
                        Source = VersionUtil.SetVersionAttribute( Source, value );
                        foreach( var r in _versionedBy )
                        {
                            r.OnVersionChanged();
                        }
                    }
                }
            }

            internal void Validate( ValidationContext ctx )
            {
                var m = ctx.Monitor;
                VersionUtil.CheckStatus s = VersionUtil.CheckVersionAttributes( Source );
                // Always remove FileVersionAttribute.
                if( (s & VersionUtil.CheckStatus.UselessFileVersion) != 0 )
                {
                    m.Warn().Send( "Useless FileVersion attribute in '{0}' (CK.Stamp.Fody does the job).", WorkspaceBasedPath );
                    ctx.Add( new Fixes.RemoveUselessFileVersionAttribute( this ) );
                }
                bool ensureDone = false;
                if( _attrStatus == VersionFileManager.VersionAttributeStatus.MustExist )
                {
                    bool ensureNeeded = false;
                    if( (s & VersionUtil.CheckStatus.BadOrMissingInformationalVersion) != 0 )
                    {
                        m.Error().Send( "Missing InformationalVersion attribute in '{0}'.", WorkspaceBasedPath );
                        ensureNeeded = true;
                    }
                    if( (s & VersionUtil.CheckStatus.MissingVersion) != 0 )
                    {
                        m.Error().Send( "Missing Version attribute in '{0}'.", WorkspaceBasedPath );
                        ensureNeeded = true;
                    }
                    if( ensureNeeded )
                    {
                        ctx.Add( new Fixes.EnsureFileHasVersionAttributes( this ) );
                    }
                }
                // If it does have a Version attribute...
                if( VersionString != null )
                {
                    // For the sake of completeness (even if it not used).
                    if( _attrStatus == VersionFileManager.VersionAttributeStatus.MustNotExist )
                    {
                        m.Error().Send( "'{0}' must not contain an AssemblyVersion attribute. Remove it manually.", WorkspaceBasedPath );
                    }
                    else
                    {
                        // It must be a valid one and contain the AssemblyInformationalVersion.
                        if( !ensureDone && (s & VersionUtil.CheckStatus.BadOrMissingInformationalVersion) != 0 )
                        {
                            m.Error().Send( "'{0}' declares an AssemblyVersion attribute: it must also declare the AssemblyInformationalVersion.", WorkspaceBasedPath );
                            ensureDone = true;
                            ctx.Add( new Fixes.EnsureFileHasVersionAttributes( this ) );
                        }
                        if( Version == null )
                        {
                            ctx.Monitor.Error().Send( "Invalid AssemblyVersionAttribute content '{0}' in {1}. Please correct it manually.", _versionStringRead, _solutionBasedPath );
                        }
                        else
                        {
                            var common = String.Format( "'{0}' defines version '{1}'.", _solutionBasedPath, _versionStringRead );
                            if( _versionRead.Build == -1 || _versionRead.Revision >= 0 )
                            {
                                ctx.Monitor.Error().Send( "{0} Version must be Major.Minor.Patch. (Three and only three numbers.)", common );
                                ctx.Add( new Fixes.CorrectMajorMinorPatchVersion( this ) );
                            }
                            else ctx.Monitor.Trace().Send( common );
                        }
                    }
                }
            }

        }

        internal VersionFileManager( Workspace s )
        {
            Workspace = s;
            _files = new List<VFile>();
        }

        public IReadOnlyList<IFile> Files
        {
            get { return _files; }
        }

        public VFile SharedAssemblyInfo
        {
            get { return _sharedAssemblyInfo; }
        }

        /// <summary>
        /// Gets the global version (the one in <see cref="SharedAssemblyInfo"/>).
        /// Null if no SharedAssemblyInfo exists, if it does not contain a valid version, or if we are not 
        /// in simple versionning mode.
        /// </summary>
        public Version SimpleModeVersion
        {
            get { return _sharedAssemblyInfo != null ? _sharedAssemblyInfo.Version : null; }
            set
            {
                if( !CanSetSimpleModeVersion ) throw new InvalidOperationException( "CanSetSimpleModeVersion is false." );
                _sharedAssemblyInfo.Version = value;
            }
        }

        public bool CanSetSimpleModeVersion
        {
            get { return SimpleModeVersion != null; }
        }

        internal void Add( string fullPath )
        {
            bool isShared = fullPath.EndsWith( Path.DirectorySeparatorChar + CKMP.SharedAssemblyInfoFileName, StringComparison.OrdinalIgnoreCase );
            var f = new VFile( this, fullPath, isShared );
            if( isShared )
            {
                _sharedAssemblyInfo = f;
            }
            _files.Add( f );
        }


        internal void Validate( ValidationContext ctx, bool simpleMode )
        {
            if( !simpleMode ) throw new NotImplementedException( "Multiple versioning mode is not implemented yet." );
            using( ctx.Monitor.OpenInfo().Send( ValidationContext.ContextProcessingTag, "Validating Versions informations." ) )
            {
                foreach( var f in _files )
                {
                    f.Validate( ctx );
                }
            }
        }

        static public bool IsVFileName( string path )
        {
            return path.EndsWith( Path.DirectorySeparatorChar + CKMP.SharedAssemblyInfoFileName, StringComparison.OrdinalIgnoreCase )
                    || path.EndsWith( Path.DirectorySeparatorChar + "AssemblyInfo.cs" )
                    || Regex.IsMatch( path, @"(\\|/)[^(\\|/)]*VersionInfo\.cs$", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture );
        }
    }

}
