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

        ///// <summary>
        ///// Create the information for the current branch.
        ///// Status.CanCreateCurrentBranch must be true otherwise an <see cref="InvalidOperationException"/> is thrown.
        ///// </summary>
        ///// <returns>The created <see cref="BranchRelease"/> object.</returns>
        //public Info.BranchRelease CreateCurrentBranch()
        //{
        //    if( !_current.CanCreateCurrentBranch ) throw new InvalidOperationException( "Status.CanCreateCurrentBranch must not be false." );
        //    Debug.Assert( _current.BranchName != null && _ctx.InfoReleaseDatabase != null );
        //    _ctx.InfoReleaseDatabase.EnsureBranch( _ctx.MainMonitor, _ctx.Workspace.SolutionCKFile.SolutionName, _current.BranchName );
        //    Refresh();
        //    return _current.CurrentBranch;
        //}

        //public bool ReadyToReleaseCurrent( string actor )
        //{
        //    if( String.IsNullOrEmpty( actor ) ) throw new ArgumentNullException( "actor" );
        //    if( !_current.CanReadyToReleaseCurrent ) throw new InvalidOperationException( "Status.CanReadyToReleaseCurrent must not be false." );
        //    bool success = _current.CurrentBranch.ReadyToReleaseCurrent( _ctx.MainMonitor, _current.MainVersion, _current.CommitSha, _current.CommitUtcTime, actor );
        //    Refresh();
        //    return success;
        //}

        public void SetSimpleModeVersion( Version v )
        {
            if( !Status.CanSetMainVersion ) throw new InvalidOperationException();
            _ctx.Workspace.MainVersion = v;
            _ctx.Workspace.SaveDirtyFiles( _ctx.MainMonitor );
            Refresh();
        }

        /// <summary>
        /// Refreshes information.
        /// </summary>
        internal bool Refresh()
        {
            var newOne = new DevContextReleaseStatus( _ctx.MainMonitor, _ctx );
            if( newOne != _current )
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
