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
        readonly string _sourceLabelTextSaved;
        
        public HeaderUpdatePage()
        {
            InitializeComponent();
            _sourceLabelTextSaved = _sourceLabel.Text;
        }

        public override void Initialize( IInteractiveDevContext ctx )
        {
            base.Initialize( ctx );
            Display();
        }

        protected override void OnDevContextRefreshed()
        {
            Display();
        }

        void Display()
        {
            string licHeaderFile = LicenseHeaderFilePah;
            if( File.Exists( licHeaderFile ) )
            {
                _text.Text = File.ReadAllText( licHeaderFile );
                _createLicenseFileHeader.Visible = false;
                _sourceLabel.Text = licHeaderFile;
                _saveLicenseHeaderFile.Visible = true;
            }
            else
            {
                _createLicenseFileHeader.Visible = true;
                _sourceLabel.Text = _sourceLabelTextSaved;
                _text.Text = Tools.FileHeaderProcessor.DefaultLicenceText;
                _saveLicenseHeaderFile.Visible = false;
            }
            _selectedPath.Text = DevContext.Workspace.WorkspacePath;
        }

        private string LicenseHeaderFilePah
        {
            get { return DevContext.Workspace.WorkspacePath + "licenseFileHeader.txt"; }
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

        private void _createLicenseFileHeader_Click( object sender, EventArgs e )
        {
            File.WriteAllText( LicenseHeaderFilePah, _text.Text );
            _sourceLabel.Text = LicenseHeaderFilePah;
            _createLicenseFileHeader.Visible = false;
            _saveLicenseHeaderFile.Visible = true;
            _saveLicenseHeaderFile.Enabled = false;
        }

        private void _saveLicenseHeaderFile_Click( object sender, EventArgs e )
        {
            File.WriteAllText( LicenseHeaderFilePah, _text.Text );
            _saveLicenseHeaderFile.Enabled = false;
        }

        private void _text_TextChanged( object sender, EventArgs e )
        {
            _saveLicenseHeaderFile.Enabled = true;
        }
    }
}
