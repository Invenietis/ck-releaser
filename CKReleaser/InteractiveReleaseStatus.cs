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
            var newOne = new DevContextReleaseStatus( _ctx );
            if( _current == null || !_current.EqualsWithLog( newOne, _ctx.MainMonitor ) )
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
