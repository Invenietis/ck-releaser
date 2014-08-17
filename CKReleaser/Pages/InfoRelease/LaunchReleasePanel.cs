#region LGPL License
/*----------------------------------------------------------------------------
* This file (CKReleaser\Pages\InfoRelease\LaunchReleasePanel.cs) is part of CiviKey. 
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
using System.IO;

namespace CK.Releaser.Info
{
    partial class LaunchReleasePanel : UserControl
    {
        InfoReleasePage Page;
        InfoRelease Info;

        public LaunchReleasePanel()
        {
            InitializeComponent();
        }

        public void Initialize( InfoReleasePage page, InfoRelease info )
        {
            Page = page;
            Info = info;
        }

        void _copyToRelease_Click( object sender, EventArgs e )
        {
            string releasePath = Page.DevContext.Workspace.ReleasePath;
            string msg = String.Format( "Content of the {0} folder will be cleared and replaced with this content.", releasePath );
            if( MessageBox.Show( msg, "Confirmation", MessageBoxButtons.YesNo ) == DialogResult.Yes )
            {
                try
                {
                    foreach( string file in Directory.EnumerateFiles( releasePath ) )
                    {
                        File.Delete( file );
                    }
                    foreach( string dir in Directory.EnumerateDirectories( releasePath ) )
                    {
                        Directory.Delete( dir, true );
                    }
                }
                catch( Exception ex )
                {
                    MessageBox.Show( ex.Message, "Unable to delete Release content folder.", MessageBoxButtons.OK, MessageBoxIcon.Error );
                }
                if( !Info.CopyContentTo( Page.DevContext.MainMonitor, releasePath ) )
                {
                    MessageBox.Show( "An error occured while copying files.", "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error );
                }
            }
        }

        void _linkReleaseFolder_LinkClicked( object sender, LinkLabelLinkClickedEventArgs e )
        {
            GUIUtil.OpenUrl( Page.DevContext.Workspace.ReleasePath );
        }



    }
}
