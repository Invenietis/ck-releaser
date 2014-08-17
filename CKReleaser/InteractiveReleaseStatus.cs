#region LGPL License
/*----------------------------------------------------------------------------
* This file (CKReleaser\InteractiveReleaseStatus.cs) is part of CiviKey. 
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CK.Core;

namespace CK.Releaser
{
    /// <summary>
    /// Exposes the status in terms release capabilities of a <see cref="IDevContext"/>.
    /// </summary>
    public class InteractiveReleaseStatus
    {
        readonly GUIDevContext _ctx;
        DevContextReleaseStatus _current;

        internal InteractiveReleaseStatus( GUIDevContext ctx )
        {
            _ctx = ctx;
            _current = new DevContextReleaseStatus();
        }

        /// <summary>
        /// Raised when something chnaged in the <see cref="Status"/>.
        /// </summary>
        public event EventHandler StatusChanged;

        /// <summary>
        /// Gets the current release status.
        /// </summary>
        public DevContextReleaseStatus Status
        {
            get { return _current; }
        }

        /// <summary>
        /// Create the information for the current branch.
        /// Status.CanCreateCurrentBranch must be true otherwise an <see cref="InvalidOperationException"/> is thrown.
        /// </summary>
        /// <returns>The created <see cref="BranchRelease"/> object.</returns>
        public Info.BranchRelease CreateCurrentBranch()
        {
            if( !_current.CanCreateCurrentBranch ) throw new InvalidOperationException( "Status.CanCreateCurrentBranch must not be false." );
            Debug.Assert( _current.BranchName != null && _ctx.InfoReleaseDatabase != null );
            _ctx.InfoReleaseDatabase.EnsureBranch( _ctx.Workspace.SolutionCKFile.SolutionName, _current.BranchName );
            Refresh();
            return _current.CurrentBranch;
        }

        public bool ReadyToReleaseCurrent( string actor )
        {
            if( String.IsNullOrEmpty( actor ) ) throw new ArgumentNullException( "actor" );
            if( !_current.CanReadyToReleaseCurrent ) throw new InvalidOperationException( "Status.CanReadyToReleaseCurrent must not be false." );
            bool success = _current.CurrentBranch.ReadyToReleaseCurrent( _ctx.MainMonitor, _current.SimpleModeVersion, _current.CommitSha, _current.CommitUtcTime, actor );
            Refresh();
            return success;
        }

        public void SetVersionsWhenPossible( Version v, string preRelease, string buildMetadata )
        {
            if( Status.CanSetSimpleModeVersion ) DoSetSimpleModeVersion( v );
            if( Status.CanSetPreReleaseVersion ) DoSetPreReleaseVersion( preRelease );
            if( Status.CanSetBuildMetadataVersion ) DoSetBuildMetadataVersion( buildMetadata );
            if( Status.CanSetSimpleModeVersion || Status.CanSetPreReleaseVersion || Status.CanSetBuildMetadataVersion ) Refresh();
        }

        public void SetSimpleModeVersion( Version v )
        {
            if( !Status.CanSetSimpleModeVersion ) throw new InvalidOperationException();
            DoSetSimpleModeVersion( v );
            Refresh();
        }

        public void SetPreReleaseVersion( string v )
        {
            if( !Status.CanSetPreReleaseVersion ) throw new InvalidOperationException();
            DoSetPreReleaseVersion( v );
            Refresh();
        }

        public void SetBuildMetadataVersion( string v )
        {
            if( !Status.CanSetBuildMetadataVersion ) throw new InvalidOperationException();
            DoSetBuildMetadataVersion( v );
            Refresh();
        }

        void DoSetSimpleModeVersion( Version v )
        {
            _ctx.Workspace.VersionFileManager.SharedAssemblyInfo.Version = v;
            _ctx.Workspace.SaveDirtyFiles( _ctx.MainMonitor );
        }

        void DoSetPreReleaseVersion( string v )
        {
            var data = _current.ReadyToRelease.GetData( _ctx.MainMonitor );
            if( data != null )
            {
                data.PreReleaseVersion = v;
                _current.ReadyToRelease.SaveData( _ctx.MainMonitor );
            }
        }

        void DoSetBuildMetadataVersion( string v )
        {
            var data = _current.ReadyToRelease.GetData( _ctx.MainMonitor );
            if( data != null )
            {
                data.BuildMetadataVersion = v;
                _current.ReadyToRelease.SaveData( _ctx.MainMonitor );
            }
        }

        /// <summary>
        /// Refreshes information.
        /// </summary>
        internal bool Refresh()
        {
            var newOne = new DevContextReleaseStatus( _ctx.MainMonitor, _ctx );
            if( !newOne.Equals( _current ) )
            {
                _current = newOne;
                var h = StatusChanged;
                if( h != null ) h( this, EventArgs.Empty );
                return true;
            }
            return false;
        }

    }
}
