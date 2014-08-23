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

    /// <summary>
    /// Façade for a git repository that exposes only what we need (and hide lib2git complexity and objects).
    /// Use the <see cref="Find"/> factory method to create an instance. One then need to <see cref="Open"/> it.
    /// </summary>
    public class GitManager : IDisposable
    {
        public readonly string GitPath;
        
        Repository _repository;
        string _branchName;
        DateTime _commitUtcTime;
        string _commitSha;
        VersionOnBranch _releasedVersion;
        bool _isDirty;

        GitManager( string gitPath )
        {
            GitPath = gitPath;
            _commitUtcTime = Util.UtcMinValue;
        }

        /// <summary>
        /// Captures released information from a release Tag like "v4.0.0-master".
        /// </summary>
        public class ReleasedCommit : IEquatable<ReleasedCommit>
        {
            public readonly string CommitSha;
            public readonly DateTime CommitUtcTime;
            public readonly VersionOnBranch Version;

            internal ReleasedCommit( Commit c, VersionOnBranch v )
                : this( c.Sha, c.Committer.When.UtcDateTime, v )
            {
            }

            internal ReleasedCommit( string sha, DateTime time, VersionOnBranch v )
            {
                CommitSha = sha;
                CommitUtcTime = time;
                Version = v;
            }

            static public bool operator==( ReleasedCommit rc1, ReleasedCommit rc2 )
            {
                return Object.ReferenceEquals( rc1, null ) ? Object.ReferenceEquals( rc2, null ) : rc1.Equals( rc2 );
            }

            static public bool operator !=( ReleasedCommit rc1, ReleasedCommit rc2 )
            {
                return !(rc1 == rc2 );
            }

            public bool Equals( ReleasedCommit x )
            {
                return x != null && CommitSha == x.CommitSha && CommitUtcTime == x.CommitUtcTime && Version == x.Version;
            }

            public override bool Equals( object obj )
            {
                var rc = obj as ReleasedCommit;
                return rc != null ? Equals( rc ) : false;
            }

            public override int GetHashCode()
            {
                return Util.Hash.Combine( Util.Hash.StartValue, CommitSha, CommitUtcTime, Version ).GetHashCode();
            }
        }

        /// <summary>
        /// Factory method: if a git repository is found at the given path or above,
        /// a closed <see cref="GitManager"/> is returned, otherwise null is returned.
        /// </summary>
        /// <param name="m">The monitor used to track any errors.</param>
        /// <param name="path">The path (or subpath) of a potential git repository.</param>
        /// <returns>Null if not found, otherwise a closed instance.</returns>
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

        /// <summary>
        /// Opens this <see cref="GitManager"/>.
        /// </summary>
        /// <param name="m">The monitor used to track any errors.</param>
        /// <returns>True on success, false on error.</returns>
        public bool Open( IActivityMonitor m )
        {
            if( _repository != null ) return true;
            try
            {
                _repository = new Repository( GitPath );
                m.Trace().Send( "Repository '{0}' opened.", GitPath );
                RefreshBranchInfo( m );
                return true;
            }
            catch( Exception ex )
            {
                m.Error().Send( ex );
                return false;
            }
        }

        /// <summary>
        /// Refreshes information from the underlying repository.
        /// Returns true if something changed, false otherwise.
        /// </summary>
        /// <param name="m">The monitor used to track any errors.</param>
        /// <returns>True if something changed, false otherwise.</returns>
        public bool RefreshCachedInfo( IActivityMonitor m )
        {
            if( _repository == null ) return false;
            return RefreshBranchInfo( m );
        }

        /// <summary>
        /// Gets whether this <see cref="GitManager"/> is opened.
        /// </summary>
        public bool IsOpen
        {
            get { return _repository != null; }
        }
        
        /// <summary>
        /// Gets whether this <see cref="IsOpen"/> is true and the repository is correctly initialized.
        /// The <see cref="CurrentBranchName"/> can not be null but can be "(no branch)" if on a detached head.
        /// </summary>
        public bool IsValid
        {
            get { return _branchName != null; }
        }

        /// <summary>
        /// Gets the current branch name (the repository's head Name).
        /// Can be null if the <see cref="IsValid"/> is false or "(no branch)" when on a detached head.
        /// </summary>
        public string CurrentBranchName
        {
            get { return _branchName; }
        }

        /// <summary>
        /// Gets the current commit time (the repository's head Tip.Committer.When.DateTime).
        /// Can be <see cref="Util.UtcMinValue"/> when <see cref="IsValid"/> is false (the repository is not correctly initialized or <see cref="IsOpen"/> is false).
        /// </summary>
        public DateTime CommitUtcTime
        {
            get { return _commitUtcTime; }
        }

        /// <summary>
        /// Gets the current commit 40 characters Sha1.
        /// Can be null if the repository is not correctly initialized or <see cref="IsOpen"/> is false.
        /// </summary>
        public string CommitSha
        {
            get { return _commitSha; }
        }

        /// <summary>
        /// Gets whether any changes exist in the working folder that have not
        /// been commited.
        /// Always false when <see cref="IsOpen"/> is false.
        /// </summary>
        public bool IsDirty
        {
            get { return _isDirty; }
        }

        /// <summary>
        /// When on a detached head (<see cref="CurrentBranchName"/> is null), this may contain
        /// the release tag associated to the commit.
        /// When no release tag can be found, this <see cref="VersionOnBranch.IsValid"/> is false.
        /// </summary>
        public VersionOnBranch ReleasedVersion
        {
            get { return _releasedVersion; }
        }

        /// <summary>
        /// Gets whether the <see cref="ReleasedVersion"/> can be set on the current commit point.
        /// <see cref="IsValid"/> must be true, the repository must not be dirty and there must not be already a valid <see cref="ReleasedVersion"/>.
        /// </summary>
        public bool CanSetReleasedVersion
        {
            get { return IsValid && !_isDirty && !_releasedVersion.IsValid; }
        }

        /// <summary>
        /// Sets a released version tag on the current head.
        /// <see cref="CanSetReleasedVersion"/> must be true otherwise an <see cref="InvalidOperationException"/> is thrown.
        /// This does not change this <see cref="ReleasedVersion"/>: RefreshCachedInfo should be called to update 
        /// the whole information.
        /// </summary>
        /// <param name="major">Major version.</param>
        /// <param name="minor">Minor version.</param>
        /// <param name="patch">Patch version.</param>
        public void SetReleasedVersion( int major, int minor, int patch )
        {
            if( !CanSetReleasedVersion ) throw new InvalidOperationException();
            var v = new VersionOnBranch( major, minor, patch, _branchName );
            _repository.ApplyTag( 'v' + v.ToString() );
        }

        /// <summary>
        /// Gets the last released commit. It may be this current commit if it has been released or null if no 
        /// release currently exist in all parents for this commit.
        /// </summary>
        /// <returns></returns>
        public ReleasedCommit GetLastReleased()
        {
            if( !IsValid ) return null;
            if( _releasedVersion.IsValid )
            {
                return new ReleasedCommit( _commitSha, _commitUtcTime, _releasedVersion );
            }
            return FindLastReleasedVersion( _repository.Head.Tip );
        }

        /// <summary>
        /// Closes this <see cref="GitManager"/>.
        /// </summary>
        public void Close()
        {
            if( _repository == null ) return;
            ResetBranchInfo();
            try
            {
                _repository.Dispose();
                _repository = null;
            }
            catch( Exception ex )
            {
                ActivityMonitor.CriticalErrorCollector.Add( ex, String.Format( "While closing '{0}'.", GitPath ) );
            }
        }

        void IDisposable.Dispose()
        {
            Close();
        }

        void ResetBranchInfo()
        {
            _branchName = null;
            _commitUtcTime = Util.UtcMinValue;
            _releasedVersion = new VersionOnBranch();
            _isDirty = false;
        }

        bool RefreshBranchInfo( IActivityMonitor m )
        {
            var branch = _repository.Head;
            if( branch.Tip == null )
            {
                if( _branchName != null )
                {
                    ResetBranchInfo();
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

            VersionOnBranch releasedVersion = FindReleasedVersion( branch.Tip );

            if( _branchName != branch.Name
                || _commitUtcTime != branch.Tip.Committer.When.UtcDateTime
                || _isDirty != isDirty
                || _releasedVersion != releasedVersion )
            {
                _branchName = branch.Name;
                _commitUtcTime = branch.Tip.Committer.When.UtcDateTime;
                _isDirty = isDirty;
                _commitSha = branch.Tip.Sha;
                _releasedVersion = releasedVersion;
                return true;
            }
            return false;
        }

        ReleasedCommit FindLastReleasedVersion( Commit commit )
        {
            var releasedCommitsBySha = _repository
                                            .Tags
                                            .Where( t => t.Target is Commit )
                                            .Select( t => new ReleasedCommit( (Commit)t.Target, VersionOnBranch.TryParse( t.Name ) ) )
                                            .Where( cv => cv.Version.IsValid )
                                            .ToDictionary( cv => cv.CommitSha );

            var seen = new HashSet<string>();
            var queue = new Queue<Commit>();
            foreach( var p in commit.Parents ) queue.Enqueue( p );

            ReleasedCommit best = null;
            while( queue.Count > 0 )
            {
                var c = queue.Dequeue();
                string sha = c.Sha;
                if( !seen.Add( sha ) ) continue;
                ReleasedCommit rc;
                if( releasedCommitsBySha.TryGetValue( sha, out rc ) )
                {
                    if( best == null ) best = rc;
                    else if( rc.Version.CompareTo( best.Version ) > 0 ) best = rc;
                }
                else
                {
                    foreach( var p in c.Parents ) queue.Enqueue( p );
                }
            }
            return best;
        }


        VersionOnBranch FindReleasedVersion( Commit commit )
        {
            foreach( var tag in _repository.Tags )
            {
                if( tag.Target.Sha == commit.Sha )
                {
                    VersionOnBranch v = VersionOnBranch.TryParse( tag.Name );
                    if( v.IsValid ) return v;
                }
            }
            return new VersionOnBranch();
        }

    }
}
