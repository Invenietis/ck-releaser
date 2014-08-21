#region LGPL License
/*----------------------------------------------------------------------------
* This file (CK.Releaser\Model\Workspace.cs) is part of CiviKey. 
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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CK.Core;

namespace CK.Releaser
{
    public class Workspace
    {
        public readonly IDevContext DevContext;
        public readonly string WorkspacePath;
        public readonly string FolderName;

        List<Tuple<CSProjectObject,CSProjectObject>> _sameDirectoryProjects;
        IReadOnlyList<CSProjectObject> _projects;
        IReadOnlyList<VSSolutionObject> _solutions;
        IReadOnlyList<string> _solutionsCKPaths;
        readonly ExeAndDllFiles _exeAndDllFiles;
        readonly SolutionCKFile _solutionCK;
        readonly VersionFileManager _versionFileManager;

        internal Workspace( IDevContext devContext, string rootPath )
        {
            DevContext = devContext;
            WorkspacePath = FileUtil.NormalizePathSeparator( rootPath, true );
            FolderName = Path.GetFileName( WorkspacePath.Remove( WorkspacePath.Length - 1 ) );
            _versionFileManager = new VersionFileManager( this );
            _exeAndDllFiles = new ExeAndDllFiles( this );
            List<CSProjectObject> projects = new List<CSProjectObject>();
            List<VSSolutionObject> solutions = new List<VSSolutionObject>();
            List<string> solutionCKPaths = new List<string>();
            List<string> exeAdnDlls = new List<string>();
            Process( rootPath, null, projects, solutions, solutionCKPaths, exeAdnDlls );
            _projects = projects.ToArray();
            _solutions = solutions.ToArray();
            _solutionsCKPaths = solutionCKPaths.ToArray();
            _exeAndDllFiles.SetFiles( exeAdnDlls, _projects );
            _solutionCK = new SolutionCKFile( this );
        }

        private void Process( string rootPath, CSProjectObject aboveProject, List<CSProjectObject> projects, List<VSSolutionObject> solutions, List<string> solutionCKPaths, List<string> exeAdnDlls )
        {
            CSProjectObject isProject = null;
            foreach( var f in Directory.EnumerateFiles( rootPath ) )
            {
                if( f.EndsWith( ".csproj", StringComparison.OrdinalIgnoreCase ) )
                {
                    var newP = new CSProjectObject( this, f, aboveProject );
                    if( isProject != null )
                    {
                        // Two .csproj in the same folder.
                        if( _sameDirectoryProjects == null ) _sameDirectoryProjects = new List<Tuple<CSProjectObject,CSProjectObject>>();
                        _sameDirectoryProjects.Add( Tuple.Create( isProject, newP ) );
                    }
                    else
                    {
                        isProject = newP;
                    }
                    projects.Add( newP );
                }
                else if( f.EndsWith( ".sln", StringComparison.OrdinalIgnoreCase ) )
                {
                    solutions.Add( new VSSolutionObject( this, f ) );
                }
                else if( f.EndsWith( Path.DirectorySeparatorChar + "Solution.ck", StringComparison.OrdinalIgnoreCase ) )
                {
                    solutionCKPaths.Add( f );
                }
                else if( f.EndsWith( ".exe", StringComparison.OrdinalIgnoreCase ) || f.EndsWith( ".dll", StringComparison.OrdinalIgnoreCase ) )
                {
                    exeAdnDlls.Add( f );
                }
                else if( VersionFileManager.IsVFileName( f ) )
                {
                    _versionFileManager.Add( f );
                }
            }
            foreach( var f in Directory.EnumerateDirectories( rootPath ) )
            {
                if( f.EndsWith( Path.DirectorySeparatorChar + ".svn" ) 
                    || f.EndsWith( Path.DirectorySeparatorChar + ".nuget" ) 
                    || f.EndsWith( Path.DirectorySeparatorChar + ".git" ) 
                    || f.EndsWith( Path.DirectorySeparatorChar + "obj", StringComparison.Ordinal ) 
                    || (f.EndsWith( Path.DirectorySeparatorChar + "packages" ) && File.Exists( f + Path.DirectorySeparatorChar + "repositories.config" ) )
                    || (File.GetAttributes( f ) & FileAttributes.Hidden) != 0 ) continue;
                
                Process( f, isProject ?? aboveProject, projects, solutions, solutionCKPaths, exeAdnDlls );
            }
        }

        /// <summary>
        /// Gets the main version: when <see cref="SolutionCKFile.VersioningModeSimple"/> is true, this 
        /// is the <see cref="VersionFileManager.SharedVersionInfo"/>.<see cref="VersionFileManager.VFile.Version">Version</see>.
        /// In Multiple mode, this MainVersion will be stored in the Solution.ck file (and the SharedAssemblyInfo will not contain it).
        /// </summary>
        public Version MainVersion
        {
            get
            {
                return _versionFileManager.SharedAssemblyInfoVersion;
            }
        }

        public ExeAndDllFiles ExeAndDllFiles
        {
            get { return _exeAndDllFiles; }
        }

        public VersionFileManager VersionFileManager
        {
            get { return _versionFileManager; }
        }

        public bool HasSharedSnkFile
        {
            get { return File.Exists( SharedSnkFilePath ); }
        }

        public string SharedSnkFilePath
        {
            get { return Path.Combine( WorkspacePath, CKMP.SharedKeyFileName ); }
        }

        public string ReleasePath
        {
            get { return Path.Combine( WorkspacePath, "Release" ); }
        }

        public bool HasSharedAssemblyInfo
        {
            get { return _versionFileManager.SharedAssemblyInfo != null; }
        }

        public string SharedAssemblyInfoFilePath
        {
            get { return Path.Combine( WorkspacePath, CKMP.SharedAssemblyInfoFileName ); }
        }

        public IReadOnlyList<CSProjectObject> CSProjects
        {
            get { return _projects; }
        }

        public IReadOnlyList<VSSolutionObject> VSSolutions
        {
            get { return _solutions; }
        }

        public SolutionCKFile SolutionCKFile
        {
            get { return _solutionCK; }
        }

        public IEnumerable<IFile> GetFiles()
        {
            return new CKReadOnlyListMono<IFile>( _solutionCK ).Concat( _solutions ).Concat( _projects ).Concat( _versionFileManager.Files );
        }

        public void SaveDirtyFiles( IActivityMonitor m )
        {
            var dirtyFiles = GetFiles().Where( f => f.IsDirty ).ToList();
            if( dirtyFiles.Count > 0 )
            {
                using( m.OpenInfo().Send( ValidationContext.ContextProcessingTag, "Saving {0} files.", dirtyFiles.Count ) )
                {
                    foreach( var file in dirtyFiles )
                    {
                        file.Save( m );
                    }
                }
            }
        }

        public string MakeRootRelativePath( string path )
        {
            if( path.StartsWith( WorkspacePath ) )
            {
                return path.Substring( WorkspacePath.Length );
            }
            return path;
        }

        public void Validate( ValidationContext ctx )
        {
            var m = ctx.Monitor;
            SolutionCKFile.EnsureValid( m );
            ctx.SetDisabledFixes( SolutionCKFile.ReadDisabledFixes() );

            using( m.OpenInfo().Send( ValidationContext.ContextProcessingTag, "Validating Solution." ) )
            {
                if( SolutionCKFile.SolutionName != FolderName )
                {
                    m.Error().Send( "Solution name (defined in Solution.ck) is '{0}' but the root folder is '{1}'. Please rename the root folder.", SolutionCKFile.SolutionName, FolderName );
                }
                if( _sameDirectoryProjects != null )
                {
                    foreach( var dups in _sameDirectoryProjects.GroupBy( p => p.Item1 ) )
                    {
                        m.Error().Send( "Folder {0}: '{1}' can not be in the same folder as '{2}'.",
                                            dups.Key.ThisDirectoryPath,
                                            String.Join( "', '", dups.Select( d => d.Item2.ProjectFileName ) ),
                                            dups.Key.ProjectFileName );
                    }
                }
                if( !Directory.Exists( ReleasePath ) )
                {
                    m.Error().Send( "Missing '/Release' folder." );
                    ctx.Add( new Fixes.EnsureReleaseFolder() );
                }
                if( !HasSharedSnkFile )
                {
                    m.Error().Send( "Missing {0} file.", CKMP.SharedKeyFileName );
                    ctx.Add( new Fixes.EnsureStandardSharedKey() );
                }
                else
                {
                    var key = File.ReadAllBytes( SharedSnkFilePath );
                    if( !key.SequenceEqual( Signing.KnownStrongNames.SharedKeyBytes ) )
                    {
                        m.Error().Send( "{0} file is not the same as the Standard one.", CKMP.SharedKeyFileName );
                        ctx.Add( new Fixes.EnsureStandardSharedKey() );
                    }
                }
                if( !HasSharedAssemblyInfo )
                {
                    m.Error().Send( "Missing {0} file.", CKMP.SharedAssemblyInfoFileName );
                    ctx.Add( new Fixes.EnsureStandardSharedAssemblyInfo() );
                }
            }

            _versionFileManager.Validate( ctx, SolutionCKFile.VersioningModeSimple );

            using( m.OpenInfo().Send( "Validating {0} project(s).", _projects.Count ) )
            {
                foreach( var p in _projects ) p.Validate( ctx );
            }
        }

        /// <summary>
        /// Finds the first directory above that contains a .sln and then tries to find the closest to the root.
        /// </summary>
        /// <param name="path">Starting path. When null use AppDomain.CurrentDomain.BaseDirectory.</param>
        /// <returns>Null if not found.</returns>
        static public string FindSolutionFolderFrom( string path = null )
        {
            if( path == null ) path = AppDomain.CurrentDomain.BaseDirectory;
            string r = AppDomain.CurrentDomain.BaseDirectory;
            while( r != null && r.Length >= 3 && !Directory.EnumerateFiles( r, "*.sln" ).Any() ) r = Path.GetDirectoryName( r );
            if( r.Length <= 3 ) return null;
            // r is the result except if we find one .sln above us.
            string above = Path.GetDirectoryName( r );
            do
            {
                if( Directory.EnumerateFiles( above, "*.sln" ).Any() ) r = above;
                above = Path.GetDirectoryName( above );
            }
            while( above != null && above.Length >= 3 );
            return r;
        }
    }
}
