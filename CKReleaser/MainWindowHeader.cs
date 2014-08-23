#region LGPL License
/*----------------------------------------------------------------------------
* This file (CKReleaser\MainWindowHeader.cs) is part of CiviKey. 
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

namespace CK.Releaser
{
    partial class MainWindowHeader : ReleaserControl
    {
        readonly string _infoCountFormat;
        readonly Color _normalBranchNameColor;

        public MainWindowHeader()
        {
            InitializeComponent();
            _infoCountFormat = _infoCount.Text;
            _infoCount.Text = String.Empty;
            _normalBranchNameColor = _gitBranchName.BackColor;
        }

        public override void Initialize( IInteractiveDevContext ctx )
        {
            base.Initialize( ctx );
            ctx.ReleaseHead.StatusChanged += ( o, e ) => OnStatusChanged();
            OnDevContextRefreshed();
        }

        protected override void OnDevContextRefreshed()
        {
            _infoCount.Text = String.Format( _infoCountFormat, DevContext.Workspace.CSProjects.Count, DevContext.Workspace.VSSolutions.Count );
            _solutionFolderPath.Text = DevContext.Workspace.WorkspacePath;
            OnStatusChanged();
        }

        void OnStatusChanged()
        {
            DevContextReleaseStatus status = DevContext.ReleaseHead.Status;
            var git = DevContext.GitManager;
            _gitBranchName.Text = git.DisplayBranchName();
            if( git != null && git.IsValid )
            {
                _toolTip.SetToolTip( _gitBranchName, status.CommitStandardTimeDisplay );
            }
            else
            {
                _toolTip.SetToolTip( _gitBranchName, null );
            }
            _versionButton.Text = DevContext.ReleaseHead.Status.DisplayMainVersion;
            _toolTip.SetToolTip( _versionLabel, "Version is correct." );
            if( status.HasVersionError )
            {
                Debug.Assert( git.ReleasedVersion.IsValid );
                _versionLabel.ImageIndex = 5;
                if( status.ReleasedTagDifferentBranchError )
                {
                    _toolTip.SetToolTip( _versionLabel, String.Format( "Branch name clash between actual branch name '{0}' and released tag '{1}'.", git.CurrentBranchName, git.ReleasedVersion ) );
                }
                else
                {
                    Debug.Assert( status.VersionStatus == DevContextReleaseStatus.MainVersionStatus.FatalMismatchWithReleasedTag );
                    _toolTip.SetToolTip( _versionLabel, String.Format( "Version from source '{0}' is not the same as the released tag '{1}'.", status.MainVersion, git.ReleasedVersion ) );
                }
            }
            else if( status.HasVersionWarning )
            {
                _versionLabel.ImageIndex = 4;
                if( status.VersionStatus == DevContextReleaseStatus.MainVersionStatus.TooSmallVersionFromLastReleased )
                {
                    Debug.Assert( status.LastReleased != null && status.LastReleased.Version.IsValid );
                    _toolTip.SetToolTip( _versionLabel, String.Format( "Version must be greater than '{0}'.", status.LastReleased.Version.ToStringWithoutBranchName() ) );
                }
                else
                {
                    Debug.Assert( status.VersionStatus == DevContextReleaseStatus.MainVersionStatus.VersionMustBeUpgraded );
                    Debug.Assert( status.ReleasedVersion.IsValid );
                    _toolTip.SetToolTip( _versionLabel, String.Format( "There are changed files: current version should be increased above '{0}'.", status.ReleasedVersion.ToStringWithoutBranchName() ) );
                }
            }
            else
            {
                _versionLabel.ImageIndex = 3;
            }

            if( git.IsWorkingFolderWritable() )
            {
                _folderWritable.ImageIndex = 1;
                _versionButton.Enabled = true;
            }
            else
            {
                _folderWritable.ImageIndex = 2;
                _versionButton.Enabled = false;
            }
            _toolTip.SetToolTip( _folderWritable, git.IsWorkingFolderWritableDescription() );
        }

        private void _chooseFolderPath_Click( object sender, EventArgs e )
        {
            using( var d = new FolderBrowserDialog() )
            {
                d.SelectedPath = _solutionFolderPath.Text;
                d.ShowNewFolderButton = true;
                d.Description = "Change current Solution folder.";
                if( d.ShowDialog( this ) == DialogResult.OK )
                {
                    DevContext.Refresh( DevContext.MainMonitor, d.SelectedPath );
                }
            }
        }

        private void _versionButton_Click( object sender, EventArgs e )
        {
            using( var w = new MainVersionSetterForm( DevContext ) )
            {
                w.ShowDialog( this );
            }
        }

        private void _openPath_Click( object sender, EventArgs e )
        {
            try
            {
                Process.Start( _solutionFolderPath.Text );
            }
            catch( Exception ex )
            {
                MessageBox.Show( ex.Message, "Unable to open Path." );
            }
        }

    }
}
