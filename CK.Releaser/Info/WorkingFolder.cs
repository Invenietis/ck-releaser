#region LGPL License
/*----------------------------------------------------------------------------
* This file (CK.Releaser\Info\WorkingFolder.cs) is part of CiviKey. 
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

namespace CK.Releaser.Info
{
    public class WorkingFolder : InfoReleaseDatabase.DBObject, IDisposable
    {
        readonly BranchRelease _branch;
        readonly DirectoryInfo _base;
        readonly DateTime _targetTime;
        DirectoryInfo _workingDir;
        InfoRelease _infoRelease;

        internal WorkingFolder( string keyId, BranchRelease branch, DirectoryInfo baseDir, DirectoryInfo tempDir, DateTime targetTime )
            : base( keyId )
        {
            Debug.Assert( tempDir != null && tempDir.Exists );
            Debug.Assert( _workingDir == null );
            _branch = branch;
            _base = baseDir;
            _workingDir = tempDir;
            _targetTime = targetTime;
        }

        internal void ReOpen( DirectoryInfo tempDir )
        {
            Debug.Assert( tempDir != null && tempDir.Exists );
            Debug.Assert( _workingDir == null );
            _workingDir = tempDir;
        }

        internal InfoRelease InfoRelease
        {
            get
            {
                if( _infoRelease == null )
                {
                    _infoRelease = _branch.Database.FindInfoRelease( _branch, _targetTime );
                }
                return _infoRelease;
            }
            set { _infoRelease = value; }
        }

        /// <summary>
        /// Gets the time associated to this WorkingFolder.
        /// </summary>
        public DateTime TargetTime
        {
            get { return _targetTime; }
        }

        /// <summary>
        /// Gets the temporary folder path that contains release information and resources.
        /// Null if this has been closed.
        /// </summary>
        public string TemporaryPath
        {
            get { return _workingDir != null ? _workingDir.FullName : null; }
        }

        /// <summary>
        /// Gets whether this working folder is opened.
        /// </summary>
        public bool IsOpen
        {
            get { return _workingDir != null; }
        }

        /// <summary>
        /// Attempts to close this working folder.
        /// If an error occurs, it is logged and false is returned.
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public bool TryClose( IActivityMonitor m )
        {
            if( m == null ) throw new ArgumentNullException( "m" );
            return DoClose( m );
        }

        /// <summary>
        /// Closes this working folder.
        /// If an error occurs, it is logged into <see cref="ActivityMonitor.CriticalErrorCollector"/> but this WorkingFolder
        /// is closed anyway.
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public void Close()
        {
            DoClose( null );
        }

        /// <summary>
        /// Writes the current content of this <see cref="WorkingFolder"/> to its <see cref="InfoRelease"/>.
        /// This may create a new InfoRelease if it does not exist yet.
        /// </summary>
        /// <param name="autoClose">True to <see cref="TryClose"/> this WorkingFolder once done.</param>
        public bool Write( IActivityMonitor m, bool autoClose = true )
        {
            if( m == null ) throw new ArgumentNullException( "m" );
            if( _workingDir == null ) throw new InvalidOperationException( "WorkingFolder is closed." );
            string targetName = Path.Combine( _branch.Path, InfoReleaseDatabase.FormatFolderName( _targetTime ) );
            string backupName = targetName + ".bak";
            bool backupDone = false;

            try
            {
                DirectoryInfo target = new DirectoryInfo( targetName );
                if( target.Exists )
                {
                    if( Directory.Exists( backupName ) ) Directory.Delete( backupName, true );
                    target.MoveTo( backupName );
                    backupDone = true;
                    target = new DirectoryInfo( targetName );
                    target.Create();
                    string releaseInfoSaved = Path.Combine( backupName, InfoRelease.ReleaseInfoFileName );
                    string releaseInfoRestored = Path.Combine( targetName, InfoRelease.ReleaseInfoFileName );
                    File.Copy( releaseInfoSaved, releaseInfoRestored );
                }
                else target.Create();
                FileUtil.CopyDirectory( _workingDir, target, true, true );
                var info = InfoRelease;
                if( info != null ) info.OnWorkingFolderWritten( this );
            }
            catch( Exception ex )
            {
                m.Error().Send( ex, "While trying to Write: '{0}'.", targetName );
                if( backupDone )
                {
                    try
                    {
                        if( Directory.Exists( targetName ) ) Directory.Delete( targetName, true );
                        Directory.Move( backupName, targetName );
                    }
                    catch( Exception ex2 )
                    {
                        m.Error().Send( ex2, "While trying to restore: '{0}'.", targetName );
                    }
                }
                return false;
            }
            return autoClose ? DoClose( m ) : true;
        }

        bool DoClose( IActivityMonitor tryOnly )
        {
            if( _workingDir == null ) return true;
            if( _workingDir.Exists )
            {
                try
                {
                    _workingDir.Delete( true );
                }
                catch( Exception ex )
                {
                    string msg = "While deleting: " + _workingDir.FullName;
                    if( tryOnly == null )
                    {
                        ActivityMonitor.MonitoringError.Add( ex, msg );
                        _workingDir = null;
                    }
                    else
                    {
                        tryOnly.Error().Send( ex, msg );
                    }
                    return false;
                }
            }
            _workingDir = null;
            return true;
        }

        void IDisposable.Dispose()
        {
            DoClose( null );
        }
    }

}
