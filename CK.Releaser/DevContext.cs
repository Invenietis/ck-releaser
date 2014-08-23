#region LGPL License
/*----------------------------------------------------------------------------
* This file (CK.Releaser\DevContext.cs) is part of CiviKey. 
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CK.Core;
using CK.Releaser.Info;

namespace CK.Releaser
{
    public class DevContext : IDevContext
    {
        readonly string _initialRoot;
        Workspace _solutionFolder;
        InfoReleaseDatabase _riDb;
        GitManager _git;

        public DevContext( IActivityMonitor m, string solutionFolder )
        {
            _initialRoot = solutionFolder;
            _solutionFolder = new Workspace( this, _initialRoot );
            _git = GitManager.Find( m, _solutionFolder.WorkspacePath );
            if( _git != null ) _git.Open( m );
        }

        public string InitialRoot 
        { 
            get { return _initialRoot; } 
        }

        public Workspace Workspace
        {
            get { return _solutionFolder; }
        }

        public GitManager GitManager
        {
            get { return _git; }
        }

        public InfoReleaseDatabase InfoReleaseDatabase
        {
            get { return _riDb; }
        }

        public bool Initialize( ValidationContext ctx )
        {
            var m = ctx.Monitor;
            bool isValid = true;
            using( m.CatchCounter( _ => isValid = false ) )
            {
                Workspace.Validate( ctx );
                using( ctx.Monitor.OpenInfo().Send( ValidationContext.ContextProcessingTag, "InfoRelease database." ) )
                {
                    _riDb = FindInfoReleaseDatabase( m );
                    if( _riDb == null )
                    {
                        var name = _solutionFolder.SolutionCKFile.InfoReleaseDatabaseName;
                        if( name.Length == 0 )
                        {
                            ctx.Monitor.Error().Send( "No InfoRelease database name is specified in Solution.ck. Use the 'Release Information' page." );
                        }
                        else
                        {
                            ctx.Monitor.Error().Send( "Unable to locate InfoRelease database '{0}'. Use the 'Release Information' page.", name );
                        }
                    }
                }
                OnInitialized( isValid, ctx );
            }
            return isValid;
        }

        InfoReleaseDatabase FindInfoReleaseDatabase(IActivityMonitor m )
        {
            InfoReleaseDatabase result = null;
            var dbId = _solutionFolder.SolutionCKFile.InfoReleaseDatabaseGUID;
            if( dbId.HasValue )
            {
                result = InfoReleaseDatabase.FindSiblingOrAbove( _solutionFolder.WorkspacePath, dbId.Value );
                if( result == null ) m.Info().Send( "Unable to find InfoReleaseDatabase id='{0}'.", dbId.Value.ToString() );
                else _solutionFolder.SolutionCKFile.InfoReleaseDatabaseName = result.Name;
            }
            else
            {
                string name = _solutionFolder.SolutionCKFile.InfoReleaseDatabaseName;
                if( !String.IsNullOrWhiteSpace( name ) )
                {
                    result = InfoReleaseDatabase.FindSiblingOrAbove( _solutionFolder.WorkspacePath, name );
                    if( result == null ) m.Info().Send( "Unable to find InfoReleaseDatabase with name='{0}'.", name.ToString() );
                    else _solutionFolder.SolutionCKFile.InfoReleaseDatabaseGUID = result.DatabaseId;
                }
            }
            return result;
        }

        public virtual bool Refresh( IActivityMonitor monitor, string newWorkspacePath )
        {
            string path = newWorkspacePath ?? _solutionFolder.WorkspacePath;
            Workspace s;
            try
            {
                s = new Workspace( this, path );
            }
            catch( Exception ex )
            {
                monitor.Error().Send( ex, "Unable to create Solution Folder for path: '{0}'.", path );
                return false;
            }
            if( _git == null || (_git != null && _git.GitPath != s.WorkspacePath) )
            {
                if( _git != null ) _git.Close();
                _git = GitManager.Find( monitor, s.WorkspacePath );
                if( _git != null ) _git.Open( monitor );
            }
            _riDb = FindInfoReleaseDatabase( monitor );
            _solutionFolder = s;
            OnRefreshed();
            return true;
        }

        protected virtual void OnRefreshed()
        {
        }

        protected virtual void OnInitialized( bool isValid, ValidationContext ctx )
        {
        }

    }
}
