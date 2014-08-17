#region LGPL License
/*----------------------------------------------------------------------------
* This file (CKReleaser\Pages\InfoRelease\ReleaseInfoDatabaseFinder.cs) is part of CiviKey. 
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
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CK.Core;
using CK.Releaser.Info;

namespace CK.Releaser.Info
{
    public partial class InfoReleaseDatabaseFinder : Form
    {
        readonly IInteractiveDevContext _ctx;
        readonly string _resultPathLabelFormat;
        InfoReleaseDatabase _selectedDB;

        public InfoReleaseDatabaseFinder( IInteractiveDevContext ctx )
        {
            _ctx = ctx;
            InitializeComponent();
            _resultPathLabelFormat = _resultPathLabel.Text;
            _resultPathLabel.Text = String.Empty;
            _createButton.Enabled = false;
        }

        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad( e );
            RefreshAvailableList();
        }

        void _choosePathCreate_Click( object sender, EventArgs e )
        {
            using( FolderBrowserDialog d = new FolderBrowserDialog() )
            {
                d.SelectedPath = Path.GetDirectoryName( _ctx.Workspace.WorkspacePath );
                d.ShowNewFolderButton = true;
                d.Description = String.Format( "The selected path must be above '{0}'.", _ctx.Workspace.WorkspacePath );
                while( d.ShowDialog( this ) == DialogResult.OK )
                {
                    string p = FileUtil.NormalizePathSeparator( d.SelectedPath, true );
                    if( !_ctx.Workspace.WorkspacePath.StartsWith( p, StringComparison.OrdinalIgnoreCase ) )
                    {
                        MessageBox.Show( d.Description );
                    }
                    else
                    {
                        _pathCreate.Text = p;
                        UpdatePathResultLabel();
                        break;
                    }
                }
            }
        }

        public InfoReleaseDatabase SelectedDB
        {
            get { return _selectedDB; }
        }

        void UpdatePathResultLabel()
        {
            if( String.IsNullOrWhiteSpace( _pathCreate.Text ) || !Directory.Exists( _pathCreate.Text ) )
            {
                _resultPathLabel.Text = "Choose Path first.";
            }
            else
            {
                _resultPathLabel.Text = String.Format( _resultPathLabelFormat, _pathCreate.Text, _nameCreate.Text );
            }
        }

        void _nameOrPathCreate_TextChanged( object sender, EventArgs e )
        {
            UpdatePathResultLabel();
            _createButton.Enabled = !String.IsNullOrWhiteSpace( _pathCreate.Text )
                                    && Directory.Exists( _pathCreate.Text )
                                    && !String.IsNullOrWhiteSpace( _nameCreate.Text )
                                    && FileUtil.IndexOfInvalidFileNameChars( _nameCreate.Text ) < 0;
        }

        void _createButton_Click( object sender, EventArgs e )
        {
            string p = _pathCreate.Text + _nameCreate.Text;
            if( Directory.Exists( _pathCreate.Text + _nameCreate.Text ))
            {
                if( MessageBox.Show( "This folder already exists. Do you still want to consider it as a Release Info database?", "Caution", MessageBoxButtons.YesNo ) == DialogResult.No )
                {
                    return;
                }
            }
            else Directory.CreateDirectory( p );
            _selectedDB = new InfoReleaseDatabase( p, true );
            RefreshAvailableList();
        }

        void RefreshAvailableList()
        {
            _availableList.Items.Clear();
            var all = InfoReleaseDatabase.FindAll( _ctx.Workspace.WorkspacePath ).Select( db => CreateItem( db ) ).ToArray();
            _availableList.Items.AddRange( all );
            _availableList.Focus();
        }

        ListViewItem CreateItem( InfoReleaseDatabase db )
        {
            var r = new ListViewItem( db.Name );
            r.SubItems.Add( db.DatabasePath );
            r.SubItems.Add( db.DatabaseId.ToString() );
            if( Directory.Exists( db.DatabasePath + ".git" ) )
                r.SubItems.Add( "yes" );
            else r.SubItems.Add( "no" );
            r.Tag = db;
            if( _selectedDB != null && db.DatabasePath == _selectedDB.DatabasePath )
            {
                _selectedDB = db;
                r.Selected = true;
            }
            return r;
        }

        private void _availableList_ItemSelectionChanged( object sender, ListViewItemSelectionChangedEventArgs e )
        {
            _selectedDB = (InfoReleaseDatabase)e.Item.Tag;
        }

        void _cancel_Click( object sender, EventArgs e )
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void _okButton_Click( object sender, EventArgs e )
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
