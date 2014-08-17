#region LGPL License
/*----------------------------------------------------------------------------
* This file (CKReleaser\Pages\Home\FixWindow.cs) is part of CiviKey. 
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

namespace CK.Releaser.Home
{
    public partial class FixWindow : Form
    {
        readonly IInteractiveDevContext _devContext;
        readonly ValidationContext _ctx;
        readonly string _applyTextFormat;
        int _enabledFixeCount;

        public FixWindow( IInteractiveDevContext devContext, ValidationContext ctx )
        {
            _devContext = devContext;
            _devContext.Refreshed += _close_Click;
            _ctx = ctx;
            InitializeComponent();
            _applyTextFormat = _closeAndApply.Text;
        }

        protected override void OnLoad( EventArgs e )
        {
            _fixList.SuspendLayout();
            foreach( var fix in _ctx.Fixes )
            {
                var i = _fixList.Items.Add( fix.Title );
                if( !fix.IsDisabled )
                {
                    i.Checked = true;
                }
                i.SubItems.Add( fix.MemoryKey );
            }
            _fixList.ResumeLayout();
            UpdateCloseAndApplyText();
            if( !_devContext.IsWorkingFolderWritable() )
            {
                _closeAndApply.Enabled = _fixList.Enabled = false;
            }
            base.OnLoad( e );
        }

        private void _close_Click( object sender, EventArgs e )
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void _closeAndApply_Click( object sender, EventArgs e )
        {
            for( int i = 0; i < _fixList.Items.Count; ++i )
            {
                _ctx.Fixes[i].IsDisabled = !_fixList.Items[i].Checked;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void _fixList_ItemCheck( object sender, ItemCheckEventArgs e )
        {
            if( e.NewValue == CheckState.Unchecked )
            {
                if( MessageBox.Show( "This fix will be disabled. The fact that you disabled it will be stored in the Solution.ck: it will remain disabled.\r\nAre youy sure to disable it?",
                                     "Caution", MessageBoxButtons.YesNo ) == DialogResult.No )
                {
                    e.NewValue = e.CurrentValue;
                }
                else --_enabledFixeCount;
            }
            else ++_enabledFixeCount;
            UpdateCloseAndApplyText();
        }

        void UpdateCloseAndApplyText()
        {
            _closeAndApply.Text = String.Format( _applyTextFormat, _enabledFixeCount );
        }
    }
}
