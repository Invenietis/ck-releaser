using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CK.Releaser.Signing
{
    [DisplayName( "ClickOnce Applications" )]
    partial class ClickOnceApplicationsPage : ReleaserControl
    {
        public ClickOnceApplicationsPage()
        {
            InitializeComponent();
        }

        public override void Initialize( IInteractiveDevContext ctx )
        {
            base.Initialize( ctx );
            var items = ctx.Workspace
                            .CSProjects
                            .Where( p => p.PublishUrl != null )
                            .Select( p => new ListViewItem( p.WorkspaceBasedPath ) { Checked = true, Tag = p } );
            //_allOutputs.Items.AddRange( items.ToArray() );
            //_signToolPath.Text = AuthentiCodeSigner.FindDefaultSignToolPath( ctx.MainMonitor );
        }


    }
}
