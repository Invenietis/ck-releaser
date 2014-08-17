#region LGPL License
/*----------------------------------------------------------------------------
* This file (CKReleaser\GUIDevContext.cs) is part of CiviKey. 
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
using System.Windows.Forms;

namespace CK.Releaser
{
    public class GUIDevContext : DevContext, IInteractiveDevContext
    {
        readonly IActivityMonitor _mainMonitor;
        readonly InteractiveReleaseStatus _releaseHead;
        bool _autoRefreshOnDirty;

        public event EventHandler Refreshed;

        public event EventHandler GitInfoChanged;

        public event EventHandler<RefreshErrorsEventArgs> RefreshErrors;

        public GUIDevContext( IActivityMonitor m, string solutionFolder )
            : base( m, solutionFolder )
        {
            _mainMonitor = m;
            _autoRefreshOnDirty = true;
            _releaseHead = new InteractiveReleaseStatus( this );
            Application.Idle += OnAppIdle;
        }

        public bool IsDirty { get; set; }

        public InteractiveReleaseStatus ReleaseHead
        {
            get { return _releaseHead; }
        }

        void OnAppIdle( object sender, EventArgs e )
        {
            if( _autoRefreshOnDirty && IsDirty ) Refresh( _mainMonitor );
            else
            {
                if( GitManager != null && GitManager.RefreshCachedInfo( _mainMonitor ) )
                {
                    _releaseHead.Refresh();
                    var h = GitInfoChanged;
                    if( h != null ) h( this, EventArgs.Empty );
                }
                else _releaseHead.Refresh();
            }
        }

        protected override void OnInitialized( bool isValid, ValidationContext ctx )
        {
            base.OnInitialized( isValid, ctx );
            _releaseHead.Refresh();
        }

        public override bool Refresh( IActivityMonitor monitor, string newSolutionPath = null )
        {
            IsDirty = false;
            using( monitor.Catch( OnRefreshErrors ) )
            {
                _autoRefreshOnDirty = base.Refresh( monitor, newSolutionPath );
                _releaseHead.Refresh();
                return _autoRefreshOnDirty;
            }
        }

        void OnRefreshErrors( IReadOnlyList<ActivityMonitorSimpleCollector.Entry> errors )
        {
            var h = RefreshErrors;
            if( h != null ) h( this, new RefreshErrorsEventArgs( errors ) );
        }

        protected override void OnRefreshed()
        {
            var h = Refreshed;
            if( h != null ) h( this, EventArgs.Empty );
        }

        public IActivityMonitor MainMonitor
        {
            get { return _mainMonitor; }
        }

    }
}
