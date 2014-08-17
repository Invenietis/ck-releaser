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
        TreeNode _bestSelectedNode;

        internal readonly Font BoldFont;
        internal readonly Font NormalFont;
        internal readonly Font ItalicFont;
        internal readonly Font BoldItalicFont;

        public InfoReleasePage()
        {
            InitializeComponent();
            _noDBString = _currentPath.Text;
            BoldFont = _dbTreeView.Font;
            BoldItalicFont = new Font( _dbTreeView.Font, _dbTreeView.Font.Style | FontStyle.Italic );
            NormalFont = new Font( _dbTreeView.Font, _dbTreeView.Font.Style & ~FontStyle.Bold );
            ItalicFont = new Font( NormalFont, NormalFont.Style | FontStyle.Italic );
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

        internal void SelectBestNode()
        {
            if( _bestSelectedNode != null )
            {
                _dbTreeView.SelectedNode = _bestSelectedNode;
            }
        }

        void RefreshDBView()
        {
            var db = DevContext.InfoReleaseDatabase;
            if( db == null )
            {
                _currentDB = null;
                _dbTreeView.Nodes.Clear();
                _dbContentPages.TabPages.Clear();
                _dbContentContainer.Enabled = false;
                return;
            }
            if( db != _currentDB )
            {
                _currentDB = db;
                _dbTreeView.Nodes.Clear();
                _dbContentPages.TabPages.Clear();
            }
            var status = DevContext.ReleaseHead.Status;
            _currentHeadStatus.Text = status.StatusText;
            _createCurrentBranch.Visible = status.CanCreateCurrentBranch;
            Dictionary<object,TreeNode> nodePoolByTag = new Dictionary<object, TreeNode>();
            SaveToPool( nodePoolByTag, _dbTreeView.Nodes );
            _bestSelectedNode = null;
            TreeNode previousSelectedNode = _dbTreeView.SelectedNode;
            _dbTreeView.Nodes.Clear();
            foreach( var b in db.GetExistingBranches( DevContext.Workspace.SolutionCKFile.SolutionName ) )
            {
                TreeNode nB = FindInPoolOrCreate( nodePoolByTag, b );
                bool isActiveBranch = b == status.CurrentBranch;
                if( isActiveBranch )
                {
                    Debug.Assert( _bestSelectedNode == null );
                    _bestSelectedNode = nB;
                    nB.Expand();
                    nB.NodeFont = ItalicFont;
                }
                foreach( var r in b.GetPastInfoReleases( DevContext.MainMonitor ) )
                {
                    TreeNode nR = FindInPoolOrCreate( nodePoolByTag, r );
                    if( r == status.ReadyToRelease )
                    {
                        Debug.Assert( _bestSelectedNode == nB );
                        _bestSelectedNode = nR;
                        nR.NodeFont = ItalicFont;
                    }
                    nB.Nodes.Add( nR );
                }
                _dbTreeView.Nodes.Add( nB );
            }
            var disappearedPages = _dbContentPages.TabPages.Cast<ActualContentViewTabPage>().Where( p => nodePoolByTag.ContainsKey( p.Node ) ).ToList();
            foreach( var noMore in disappearedPages ) _dbContentPages.TabPages.Remove( noMore );
            foreach( ActualContentViewTabPage p in _dbContentPages.TabPages )
            {
                p.Content.RefreshDBView();
            }
            if( previousSelectedNode == null )
            {
                _dbTreeView.SelectedNode = _bestSelectedNode;
            }
        }

        TreeNode FindInPoolOrCreate( Dictionary<object, TreeNode> nodePoolByTag, BranchRelease b )
        {
            TreeNode n;
            if( nodePoolByTag.TryGetValue( b, out n ) )
            {
                nodePoolByTag.Remove( b );
            }
            else
            {
                n = new TreeNode( b.BranchName );
                n.Tag = b;
                n.NodeFont = NormalFont;
            }
            return n;
        }

        TreeNode FindInPoolOrCreate( Dictionary<object, TreeNode> nodePoolByTag, InfoRelease r )
        {
            TreeNode n;
            if( nodePoolByTag.TryGetValue( r, out n ) )
            {
                nodePoolByTag.Remove( r );
            }
            else 
            {
                n = new TreeNode( r.StandardTimeDisplay );
                n.Tag = r;
                n.NodeFont = NormalFont;
            }
            return n;
        }

        void SaveToPool( Dictionary<object, TreeNode> nodePoolByTag, TreeNodeCollection nodes )
        {
            foreach( TreeNode n in nodes )
            {
                nodePoolByTag.Add( n.Tag, n );
                SaveToPool( nodePoolByTag, n.Nodes );
                n.Nodes.Clear();
            }
        }

        void _dbTreeView_AfterSelect( object sender, TreeViewEventArgs e )
        {
            _dbContentPages.SelectedTab = FindOrCreateWorkingFolderPage( e.Node );
        }

        void _dbTreeView_NodeMouseClick( object sender, TreeNodeMouseClickEventArgs e )
        {
            _dbContentPages.SelectedTab = FindOrCreateWorkingFolderPage( e.Node );
        }

        class ActualContentViewTabPage : TabPage
        {
            public readonly InfoReleasePage Page;
            public readonly TreeNode Node;
            public readonly PageControl Content;

            public ActualContentViewTabPage( InfoReleasePage page, TreeNode node )
            {
                Page = page;
                Node = node;

                string title;
                if( node.Level == 0 )
                {
                    BranchRelease r = (BranchRelease)node.Tag;
                    title = r.BranchName;
                    Content = new BranchControl( page, node, r );
                }
                else
                {
                    InfoRelease r = ((InfoRelease)node.Tag);
                    title = r.Branch.BranchName + '/' + r.StandardTimeDisplay;
                    Content = new InfoReleaseControl( Page, Node, r );
                }
                Text = title;
                Controls.Add( Content );
                Content.Dock = DockStyle.Fill;
            }
        }

        ActualContentViewTabPage FindWorkingFolderPage( TreeNode node )
        {
            return _dbContentPages.TabPages.Cast<ActualContentViewTabPage>().FirstOrDefault( p => p.Node == node );
        }

        ActualContentViewTabPage FindOrCreateWorkingFolderPage( TreeNode node )
        {
            ActualContentViewTabPage p = FindWorkingFolderPage( node );
            if( p == null )
            {
                p = new ActualContentViewTabPage( this, node );
                _dbContentPages.TabPages.Add( p );
            }
            return p;
        }

        private void _createCurrentBranch_Click( object sender, EventArgs e )
        {
            DevContext.ReleaseHead.CreateCurrentBranch();
            SelectBestNode();
        }

        void _dbContentPages_SelectedIndexChanged( object sender, EventArgs e )
        {
            var page = (ActualContentViewTabPage)_dbContentPages.SelectedTab;
            if( page != null ) _dbTreeView.SelectedNode = page.Node;
        }

    }
}
