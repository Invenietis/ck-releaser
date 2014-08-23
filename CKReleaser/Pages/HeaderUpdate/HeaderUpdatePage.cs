#region LGPL License
/*----------------------------------------------------------------------------
* This file (CKReleaser\Pages\HeaderUpdate\HeaderUpdatePage.cs) is part of CiviKey. 
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

namespace CK.Releaser.HeaderUpdate
{
    [DisplayName( "Update License headers" )]
    partial class HeaderUpdatePage : ReleaserControl
    {
        public HeaderUpdatePage()
        {
            InitializeComponent();
        }

        public override void Initialize( IInteractiveDevContext ctx )
        {
            base.Initialize( ctx );
            if( File.Exists( ctx.Workspace.WorkspacePath + "license.txt" ) )
            {
                _text.Text = File.ReadAllText( ctx.Workspace.WorkspacePath + "license.txt" );
            }
            _text.Text = Tools.FileHeaderProcessor.DefaultLicenceText;
            _selectedPath.Text = DevContext.Workspace.WorkspacePath;
        }

        void _choose_Click( object sender, EventArgs e )
        {
            using( FolderBrowserDialog d = new FolderBrowserDialog() )
            {
                d.SelectedPath = _selectedPath.Text;
                d.ShowNewFolderButton = false;
                d.Description = "Choose the root folder of the source files to process. Only .cs (except .Designer.cs) files will be processed.";
                if( d.ShowDialog( this ) == DialogResult.OK )
                {
                    _selectedPath.Text = d.SelectedPath;
                }
            }
        }

        void _process_Click( object sender, EventArgs e )
        {
            if( _selectedPath.Text.Length > 0 )
            {
                string rootPath = _selectedPath.Text;
                bool addNew = _addNew.Checked;
                bool removeExisting = _addNew.Checked;
                string newLicenceText = _text.Text;
                try
                {
                    UseWaitCursor = true;
                    int count = Tools.FileHeaderProcessor.Process( rootPath, addNew, removeExisting, newLicenceText );
                    MessageBox.Show( String.Format( "{0} file(s) processed.", count ), "Done" );
                }
                finally
                {
                    UseWaitCursor = false;
                }
            }
        }
    }
}
