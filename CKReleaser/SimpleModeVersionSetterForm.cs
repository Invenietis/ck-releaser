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
    public partial class SimpleModeVersionSetterForm : Form
    {
        readonly IInteractiveDevContext _ctx;

        public SimpleModeVersionSetterForm( IInteractiveDevContext ctx )
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
            //_versionEditorPanel.Version = status.MainVersion;
            //_versionEditorPanel.PreRelease = status.PreReleaseVersion;
            //_versionEditorPanel.BuildMetaData = status.BuildMetadataVersion;
            //_versionEditorPanel.FromSourceEnabled = status.CanSetMainVersion;
            //_versionEditorPanel.PreReleaseEnabled = status.CanSetPreReleaseVersion;
            //_versionEditorPanel.BuildMetaDataEnabled = status.CanSetBuildMetadataVersion;
        }

        private void _okButton_Click( object sender, EventArgs e )
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
            _ctx.ReleaseHead.StatusChanged -= OnSomethingChanged;
            _ctx.ReleaseHead.SetSimpleModeVersion( _versionEditorPanel.Version );
        }

        private void _cancelButton_Click( object sender, EventArgs e )
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }
    }
}
