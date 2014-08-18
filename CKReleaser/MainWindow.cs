#region LGPL License
/*----------------------------------------------------------------------------
* This file (CKReleaser\MainWindow.cs) is part of CiviKey. 
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CK.Core;

namespace CK.Releaser
{
    public partial class MainWindow : Form
    {
        readonly GUIDevContext _devContext;

        Page _currentPage;
        readonly Dictionary<object,Page> _pages;
        readonly Page _home;

        class Page
        {
            public readonly MainWindow Window;
            public readonly TreeNode Node;
            public readonly string Name;
            public readonly Type PageType;
            ReleaserControl _page;

            public Page( MainWindow w, TreeNodeCollection location, string name, string folderText )
            {
                Window = w;
                Name = name;
                Node = location.Add( Name, folderText );
                Node.Tag = this;
            }

            public Page( MainWindow w, TreeNodeCollection location, Type pageType )
            {
                Window = w;
                PageType = pageType;
                string text = pageType.GetCustomAttributes( typeof(DisplayNameAttribute), true ).Cast<DisplayNameAttribute>().Select( a => a.DisplayName ).FirstOrDefault() ?? pageType.FullName;
                Name = pageType.Name;
                Node = location.Add( Name, text );
                Node.Tag = this;
            }

            public bool IsFolderPage
            {
                get { return PageType == null; }
            }

            public ReleaserControl Control
            {
                get
                {
                    if( _page == null && !IsFolderPage )
                    {
                        try
                        {
                            _page = (ReleaserControl)Activator.CreateInstance( PageType );
                            _page.Initialize( Window._devContext );
                            _page.Dock = DockStyle.Fill;
                        }
                        catch( Exception ex )
                        {
                            ActivityMonitor.CriticalErrorCollector.Add( ex, "while creating Page." );
                        }
                    }
                    return _page;
                }
            }
        }

        public MainWindow( GUIDevContext devContext )
        {
            _devContext = devContext;
            InitializeComponent();

            _pages = new Dictionary<object, Page>();
            _home = CreateRootPage( typeof( Home.HomePage ) );
            var develop = CreateFolderPage( _home, "Develop", "Develop" );
            CreateSubPage( develop, "HeaderUpdate", typeof( HeaderUpdate.HeaderUpdatePage ) );
            CreateSubPage( develop, "TestDevelop", typeof( Tests.NUnitPage ) );
            var releaseInfo = CreateSubPage( _home, "InfoRelease", typeof( Info.InfoReleasePage ) );
            CreateSubPage( releaseInfo, "StrongName", typeof( Signing.StrongNameAndSignPage ) );
            CreateRootPage( typeof( Doc.EmbeddedDocPage ) );
            _home.Node.Expand();
            _mainWindowHeader.Initialize( _devContext );

            _devContext.RefreshErrors += _devContext_RefreshErrors;
            _devContext.Refreshed += _devContext_Refresh;
        }

        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad( e );
            Display( _home );
        }

        private Page CreateRootPage( Type pageType )
        {
            var p = new Page( this, _navigation.Nodes, pageType );
            _pages.Add( p.Name, p );
            return p;
        }

        private Page CreateFolderPage( Page parent, string name, string text )
        {
            var p = new Page( this, parent.Node.Nodes, name, text );
            _pages.Add( p.Name, p );
            return p;
        }

        private Page CreateSubPage( Page parent, string name, Type pageType )
        {
            var p = new Page( this, parent.Node.Nodes, pageType );
            _pages.Add( name, p );
            return p;
        }

        void Display( string pageName, bool syncTree = true )
        {
            Display( _pages[pageName], syncTree );
        }

        void Display( Page p, bool syncTree = true )
        {
            if( _currentPage != null ) _mainSplit.Panel2.Controls.Remove( _currentPage.Control );
            var c = p.Control;
            if( c != null )
            {
                _currentPage = p;
                _mainSplit.Panel2.Controls.Add( c );
                if( syncTree )
                {
                    _navigation.SelectedNode = p.Node;
                }
                _currentPage.Control.Select();
            }
        }

        void _navigation_AfterSelect( object sender, TreeViewEventArgs e )
        {
            var p =  (Page)e.Node.Tag;
            if( p.IsFolderPage )
            {
                if( p.Node.Nodes.Count > 0 ) _navigation.BeginInvoke( new Action( () => _navigation.SelectedNode = p.Node.Nodes[0] ) ); 
            }
            else Display( p, false );
        }

        void _devContext_RefreshErrors( object sender, RefreshErrorsEventArgs e )
        {
            foreach( var error in e.Errors )
            {
                ((Home.HomePage)_home.Control).AddSimpleLog( error.MaskedLevel, error.Text );
            }
            Display( _home );
        }

        void _devContext_Refresh( object sender, EventArgs e )
        {
            Display( _home );
        }

    }
}
