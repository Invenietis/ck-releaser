#region LGPL License
/*----------------------------------------------------------------------------
* This file (CK.Releaser\Model\SolutionCKFile.cs) is part of CiviKey. 
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
    public class SolutionCKFile : IFile
    {
        const int CurrentVersion = 2;

        public readonly Workspace Workspace;
        XDocument _xmlDoc;
        XElement _root;
        XElement _eName;
        XElement _eVersioning;
        XElement _eFixes;
        XElement _eInfoReleaseDatabase;
        XElement _eInfoReleaseDatabaseName;
        XElement _eInfoReleaseDatabaseGUID;
        bool _isDirty;

        internal SolutionCKFile( Workspace s )
        {
            Workspace = s;
        }

        /// <summary>
        /// Full path of the Solution.ck file.
        /// </summary>
        public string SolutionCKFilePath
        {
            get { return Workspace.WorkspacePath + WorkspaceBasedPath; }
        }

        public string WorkspaceBasedPath 
        {
            get { return "Solution.ck"; } 
        }

        public bool VersioningModeSimple
        {
            get { return _eVersioning.Attribute( CKMP.Mode ).Value != "Multiple"; }
        }

        public bool IsDirty
        {
            get { return _isDirty; }
        }

        public void Save( IActivityMonitor m )
        {
            if( _isDirty && Workspace.DevContext.IsWorkingFolderWritable() )
            {
                try
                {
                    m.Info().Send( "Saving file '{0}'.", WorkspaceBasedPath );
                    _xmlDoc.Save( SolutionCKFilePath );
                    _isDirty = false;
                }
                catch( Exception ex )
                {
                    m.Error().Send( ex );
                }
            }
        }

        public bool FileExists
        {
            get { return File.Exists( SolutionCKFilePath ); }
        }

        /// <summary>
        /// Gets the solution name.
        /// </summary>
        public string SolutionName
        {
            get { return _eName.Value; }
        }

        /// <summary>
        /// Gets or sets the InfoReleaseDatabase name.
        /// Never null.
        /// </summary>
        public string InfoReleaseDatabaseName
        {
            get { return _eInfoReleaseDatabaseName.Value; }
            set 
            {
                if( value == null ) value = String.Empty;
                if( _eInfoReleaseDatabaseName.Value != value ) _eInfoReleaseDatabaseName.Value = value; 
            }
        }

        /// <summary>
        /// Gets or sets the InfoReleaseDatabase identifier.
        /// </summary>
        public Guid? InfoReleaseDatabaseGUID
        {
            get 
            {
                Guid id;
                if( Guid.TryParse( _eInfoReleaseDatabaseGUID.Value, out id ) ) return id; 
                return null; 
            }
            set 
            { 
                string v = value.HasValue ? value.Value.ToString() : String.Empty;
                if( _eInfoReleaseDatabaseGUID.Value != v ) _eInfoReleaseDatabaseGUID.Value = v; 
            }
        }

        public void EnsureValid( IActivityMonitor m )
        {
            if( _xmlDoc == null )
            {
                using( m.OpenInfo().Send( ValidationContext.ContextProcessingTag, "Solution.ck" ) )
                {
                    if( FileExists )
                    {
                        m.Info().Send( "File Solution.ck exists.", SolutionCKFilePath );
                        try
                        {
                            _xmlDoc = XDocument.Load( SolutionCKFilePath );
                        }
                        catch( Exception ex )
                        {
                            m.Error().Send( ex );
                        }
                    }
                    if( _xmlDoc == null )
                    {
                        m.Info().Send( "File Solution.ck created.", SolutionCKFilePath );
                        _xmlDoc = new XDocument( new XElement( CKMP.Solution, new XAttribute( XNamespace.Xmlns + "c", CKMP.releaser ),
                                                                                new XElement( CKMP.Version, CurrentVersion ) ) );
                    }
                    _xmlDoc.Changed += _xmlDoc_Changed;
                    Initialize( m );
                    Save( m );
                }
            }
        }

        void _xmlDoc_Changed( object sender, XObjectChangeEventArgs e )
        {
            _isDirty = true;
        }

        void Initialize( IActivityMonitor m )
        {
            _root = _xmlDoc.Root;
            int version;
            var eVersion = _root.Element( CKMP.Version );
            if( eVersion == null || !Int32.TryParse( eVersion.Value, out version ) )
            {
                m.Warn().Send( "Invalid Version element. Corrected." );
                _root.SetElementValue( CKMP.Version, CurrentVersion );
            }
            else if( version != CurrentVersion )
            {
                m.Info().Send( "Upgrading from Version {0} to {1}.", version, CurrentVersion );
                eVersion.SetValue( CurrentVersion );
            }
            _eName = _root.Element( CKMP.Name );
            if( _eName == null )
            {
                _root.Add( _eName = new XElement( CKMP.Name, Workspace.FolderName ) );
            }
            _eFixes =  _root.Element( CKMP.Fixes );
            if( _eFixes == null ) 
            {
                _root.Add( _eFixes = new XElement( CKMP.Fixes ) );
            }
            _eVersioning = _root.Element( CKMP.Versioning );
            if( _eVersioning == null )
            {
                _root.Add( _eVersioning = new XElement( CKMP.Versioning, new XAttribute( CKMP.Mode, "Simple" ) ) );
            }
            _eInfoReleaseDatabase = _root.Element( CKMP.InfoReleaseDatabase );
            if( _eInfoReleaseDatabase == null )
            {
                _root.Add( _eInfoReleaseDatabase = new XElement( CKMP.InfoReleaseDatabase ) );
            }
            _eInfoReleaseDatabaseName = _eInfoReleaseDatabase.Element( CKMP.Name );
            if( _eInfoReleaseDatabaseName == null )
            {
                _eInfoReleaseDatabase.Add( _eInfoReleaseDatabaseName = new XElement( CKMP.Name ) );
            }
            _eInfoReleaseDatabaseGUID = _eInfoReleaseDatabase.Element( CKMP.GUID );
            if( _eInfoReleaseDatabaseGUID == null )
            {
                _eInfoReleaseDatabase.Add( _eInfoReleaseDatabaseGUID = new XElement( CKMP.GUID ) );
            }
        }

        public HashSet<string> ReadDisabledFixes()
        {
            return new HashSet<string>( _xmlDoc.Root.Element( CKMP.Fixes ).Elements( CKMP.Disabled ).Select( e => e.Value ) );
        }

        public void SetDisabledFixes( IEnumerable<string> disabledFixes )
        {
            var f = _xmlDoc.Root.Element( CKMP.Fixes );
            f.Elements( CKMP.Disabled ).Remove();
            f.Add( disabledFixes.Select( v => new XElement( CKMP.Disabled, v ) ) );
        }
        
    }
}
