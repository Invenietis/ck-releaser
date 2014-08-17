#region LGPL License
/*----------------------------------------------------------------------------
* This file (CK.Releaser\Tools\GitManager.cs) is part of CiviKey. 
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
using System.Threading.Tasks;
using CK.Core;
using LibGit2Sharp;

namespace CK.Releaser
{
    public class GitManager : IDisposable
    {
        public readonly string GitPath;
        
        Repository _repository;
        string _branchName;
        DateTime _commitUtcTime;
        bool _isDirty;
        string _sha;

        GitManager( string gitPath )
        {
            GitPath = gitPath;
            _commitUtcTime = Util.UtcMinValue;
        }

        static public GitManager Find( IActivityMonitor m, string path )
        {
            try
            {
                string p = path;
                string found = null;
                do
                {
                    found = Path.Combine( p, ".git" );
                    if( Directory.Exists( found ) ) break;
                    found = null;
                }
                while( !String.IsNullOrEmpty( p = Path.GetDirectoryName( p )) );
                if( found == null )
                {
                    m.Trace().Send( "Repository not found from: {0}", path );
                    return null;
                }
                m.Trace().Send( "Repository found: {0}", found );
                return new GitManager( found );
            }
            catch( Exception ex )
            {
                m.Error().Send( ex );
                return null;
            }
        }

        public bool Open( IActivityMonitor m )
        {
            if( _repository != null ) return true;
            try
            {
                _repository = new Repository( GitPath );
                m.Trace().Send( "Repository '{0}' opened.", GitPath );
                RefreshCachedInfo( m );
                return true;
            }
            catch( Exception ex )
            {
                m.Error().Send( ex );
                return false;
            }
        }

        public bool RefreshCachedInfo( IActivityMonitor m )
        {
            if( _repository == null ) return false;
            return RefreshBranchInfo( m );
        }

        /// <summary>
        /// Gets the current branch name (the repository's head Name).
        /// Can be null.
        /// </summary>
        public string CurrentBranchName
        {
            get { return _branchName; }
        }

        /// <summary>
        /// Gets the current commit time (the repository's head Tip.Committer.When.DateTime).
        /// Can be <see cref="Util.UtcMinValue"/> when none.
        /// </summary>
        public DateTime CommitUtcTime
        {
            get { return _commitUtcTime; }
        }

        /// <summary>
        /// Gets whether the working folder is writable.
        /// It is writable if the <see cref="CurrentBranchName"/> is null (unitialized repo) or if is not the special name "(no branch)".
        /// </summary>
        public bool IsWorkingFolderWritable
        {
            get { return _branchName == null || _branchName != "(no branch)"; }
        }

        /// <summary>
        /// Gets the current commit 40 characters Sha1.
        /// Can be null.
        /// </summary>
        public string CommitSha
        {
            get { return _sha; }
        }

        /// <summary>
        /// Gets whether any changes exist in the working folder that have not
        /// been commited.
        /// </summary>
        public bool IsDirty
        {
            get { return _isDirty; }
        }

        public void Close()
        {
            if( _repository == null ) return;
            _branchName = null;
            _commitUtcTime = Util.UtcMinValue;
            try
            {
                _repository.Dispose();
                _repository = null;
            }
            catch( Exception ex )
            {
                ActivityMonitor.MonitoringError.Add( ex, String.Format( "While closing '{0}'.", GitPath ) );
            }
        }

        public bool IsOpen
        {
            get { return _repository != null; }
        }

        void IDisposable.Dispose()
        {
            Close();
        }

        bool RefreshBranchInfo( IActivityMonitor m )
        {
            var branch = _repository.Head;
            if( branch.Tip == null )
            {
                if( _branchName != null )
                {
                    _branchName = null;
                    _commitUtcTime = Util.UtcMinValue;
                    m.Warn().Send( "No Tip found on current Head. Has the repository been initialized?" );
                    return true;
                }
                return false;
            }
            
            var repositoryStatus = _repository.Index.RetrieveStatus();
            bool isDirty = repositoryStatus.Added.Any() 
                            || repositoryStatus.Missing.Any() 
                            || repositoryStatus.Modified.Any() 
                            || repositoryStatus.Removed.Any() 
                            || repositoryStatus.Staged.Any();	

            if( _branchName != branch.Name
                || _commitUtcTime != branch.Tip.Committer.When.UtcDateTime
                || _isDirty != isDirty )
            {
                _branchName = branch.Name;
                _commitUtcTime = branch.Tip.Committer.When.UtcDateTime;
                _isDirty = isDirty;
                _sha = branch.Tip.Sha;
                return true;
            }
            return false;
        }

    }
}
