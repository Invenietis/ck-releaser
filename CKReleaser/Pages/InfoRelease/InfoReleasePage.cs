#region LGPL License
/*----------------------------------------------------------------------------
* This file (CKReleaser\Pages\InfoRelease\InfoReleasePage.cs) is part of CiviKey. 
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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CK.Core;
using System.Diagnostics;

namespace CK.Releaser.Info
{
    [DisplayName("Release Information")]
    partial class InfoReleasePage : ReleaserControl
    {
        readonly string _noDBString;
        InfoReleaseDatabase _currentDB;

        public InfoReleasePage()
        {
            InitializeComponent();
            _noDBString = _currentPath.Text;
        }

        public override void Initialize( IInteractiveDevContext ctx )
        {
            base.Initialize( ctx );
            ctx.ReleaseHead.StatusChanged += ( o, e ) => RefreshDBView();
        }

        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad( e );
            OnDevContextRefreshed();
        }

        protected override void OnDevContextRefreshed()
        {
            base.OnDevContextRefreshed();
            _currentPath.Text = DevContext.InfoReleaseDatabase != null ? DevContext.InfoReleaseDatabase.DatabasePath : _noDBString;
            RefreshDBView();
        }

        void _chooseCurrent_Click( object sender, EventArgs e )
        {
            using( var finder = new InfoReleaseDatabaseFinder( DevContext ) )
            {
                if( finder.ShowDialog( this ) == DialogResult.OK && finder.SelectedDB != null )
                {
                    var solutionCK = DevContext.Workspace.SolutionCKFile;
                    solutionCK.InfoReleaseDatabaseName = finder.SelectedDB.Name;
                    solutionCK.InfoReleaseDatabaseGUID = finder.SelectedDB.DatabaseId;
                    solutionCK.Save( DevContext.MainMonitor );
                    DevContext.IsDirty = true;
                }
            }
        }


        void RefreshDBView()
        {
            var db = DevContext.InfoReleaseDatabase;
            if( db == null )
            {
                _currentDB = null;
                return;
            }
            if( db != _currentDB )
            {
                _currentDB = db;
            }
            var status = DevContext.ReleaseHead.Status;
        }

    }
}
