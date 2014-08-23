#region LGPL License
/*----------------------------------------------------------------------------
* This file (CKReleaser\SimpleModeVersionSetterForm.cs) is part of CiviKey. 
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
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CK.Releaser
{
    public partial class MainVersionSetterForm : Form
    {
        readonly IInteractiveDevContext _ctx;

        public MainVersionSetterForm( IInteractiveDevContext ctx )
        {
            _ctx = ctx;
            Debug.Assert( ctx.Workspace.SolutionCKFile.VersioningModeSimple );
            InitializeComponent();
            _ctx.ReleaseHead.StatusChanged += OnSomethingChanged;
            OnSomethingChanged( this, EventArgs.Empty );
        }

        void OnSomethingChanged( object sender, EventArgs e )
        {
            var status = _ctx.ReleaseHead.Status;
            _major.Value = status.MainVersion.Major;
            _minor.Value = status.MainVersion.Minor;
            _patch.Value = status.MainVersion.Build;
            string text;
            if( status.ReleasedVersion.IsValid )
            {
                text = String.Format( "Current commit has been released with version {0}.", status.ReleasedVersion.ToStringWithoutBranchName() );
            }
            else
            {
                if( status.LastReleased == null )
                {
                    text = "There is no previous release.";
                }
                else
                {
                    text = String.Format( "A previous release '{0}' exists (committed at {1}).", status.LastReleased.Version.ToString(), status.LastReleased.CommitUtcTime.ToString( Info.InfoReleaseDatabase.TimeFormat ) );
                }
            }
            _currentInfoLabel.Text = text;
        }

        private void _okButton_Click( object sender, EventArgs e )
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
            _ctx.ReleaseHead.StatusChanged -= OnSomethingChanged;
            _ctx.ReleaseHead.SetSimpleModeVersion( new Version( (int)_major.Value, (int)_minor.Value, (int)_patch.Value ) );
        }

        private void _cancelButton_Click( object sender, EventArgs e )
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }
    }
}
