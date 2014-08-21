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
            var git = DevContext.GitManager;
            if( git == null ) _gitBranchName.Text = "(No repository)";
            else if( !git.IsOpen ) _gitBranchName.Text = "(unable to open .git repo)";
            else
            {
                string s = git.CurrentBranchName;
                if( s == null ) _gitBranchName.Text = "(unitialized repo)";
                else
                {
                    if( git.IsDirty )
                    {
                        s += " *";
                    }
                    _gitBranchName.Text = s;
                    _toolTip.SetToolTip( _gitBranchName, git.CommitUtcTime.ToString( Info.InfoReleaseDatabase.TimeFormat ) );
                }
            }
            _versionButton.Text = DevContext.ReleaseHead.Status.DisplayMainVersion;
            if( DevContext.IsWorkingFolderWritable() )
            {
                _folderWritable.ImageIndex = 1;
                _versionButton.Enabled = true;
            }
            else
            {
                _folderWritable.ImageIndex = 2;
                _versionButton.Enabled = false;
            }
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
            using( var w = new SimpleModeVersionSetterForm( DevContext ) )
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
