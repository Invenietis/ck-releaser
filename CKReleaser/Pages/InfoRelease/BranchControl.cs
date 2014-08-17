#region LGPL License
/*----------------------------------------------------------------------------
* This file (CKReleaser\Pages\InfoRelease\BranchControl.cs) is part of CiviKey. 
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
using System.Diagnostics;

namespace CK.Releaser.Info
{
    partial class BranchControl : PageControl
    {
        public readonly InfoReleasePage Page;
        public readonly TreeNode Node;
        public readonly BranchRelease Branch;

        public BranchControl( InfoReleasePage page, TreeNode node, BranchRelease branch )
        {
            Page = page;
            Node = node;
            Branch = branch;
            InitializeComponent();
            _baseReleaseDataEditor.Initialize( page, branch.Current );
            _launchReleasePanel.Initialize( Page, branch.Current );
            RefreshDBView();
        }

        public override void RefreshDBView()
        {
            _folderPath.Text = Branch.Path;
            DevContextReleaseStatus status = Page.DevContext.ReleaseHead.Status;

            bool isCurrentBranch = status.CurrentBranch == Branch;

            _createInfoRelease.Enabled = isCurrentBranch && status.CanReadyToReleaseCurrent;
            _warningEditCurrent.Visible = isCurrentBranch && status.ReadyToRelease != null;
            _createInfoRelease.Visible = !_warningEditCurrent.Visible;
            _baseReleaseDataEditor.RefreshDBView();
        }

        void _createInfoRelease_Click( object sender, EventArgs e )
        {
            var status = Page.DevContext.ReleaseHead.Status;
            Debug.Assert( status.CanReadyToReleaseCurrent );
            var desc = String.Format( "Commit {1} ({0}) will be ready to release.", status.CommitSha, status.CommitStandardTimeDisplay  );
            string actor = PromptBox.Prompt( "Create an info release for current commit.", desc, Environment.MachineName, true );
            if( actor != null && Page.DevContext.ReleaseHead.ReadyToReleaseCurrent( actor ) )
            {
                Page.SelectBestNode();
            }
        }

        void _folderPath_LinkClicked( object sender, LinkLabelLinkClickedEventArgs e )
        {
            GUIUtil.OpenUrl( ((LinkLabel)sender).Text );
        }


    }
}
