#region LGPL License
/*----------------------------------------------------------------------------
* This file (CKReleaser\GUI\PromptDlg.cs) is part of CiviKey. 
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
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace CK.Releaser
{
	class PromptDlg : Form
	{
		internal	TextBox		_txt;
		internal	Label		_desc;
		Button		_okBtn;
		Button		_cancelBtn;
		bool		_acceptNoChange;
		string		_originalText;

		internal PromptDlg( string originalText, bool acceptNoChange, bool maskedInput )
		{
			InitializeComponent();
			_acceptNoChange = acceptNoChange;
			_txt.Text = originalText;
			_originalText = _txt.Text;
			_okBtn.Enabled = _acceptNoChange;
            if( maskedInput ) _txt.PasswordChar = '*';
		}

		private System.ComponentModel.Container components = null;
		
		private void InitializeComponent()
		{
			this._txt = new System.Windows.Forms.TextBox();
			this._okBtn = new System.Windows.Forms.Button();
			this._cancelBtn = new System.Windows.Forms.Button();
			this._desc = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// _txt
			// 
			this._txt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._txt.Location = new System.Drawing.Point( 8, 66 );
			this._txt.Name = "_txt";
			this._txt.Size = new System.Drawing.Size( 382, 20 );
			this._txt.TabIndex = 0;
			this._txt.TextChanged += new System.EventHandler( this.OnTextChanged );
			// 
			// _okBtn
			// 
			this._okBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._okBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this._okBtn.Location = new System.Drawing.Point( 270, 92 );
			this._okBtn.Name = "_okBtn";
			this._okBtn.Size = new System.Drawing.Size( 56, 22 );
			this._okBtn.TabIndex = 1;
			this._okBtn.Text = "OK";
			// 
			// _cancelBtn
			// 
			this._cancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this._cancelBtn.Location = new System.Drawing.Point( 334, 92 );
			this._cancelBtn.Name = "_cancelBtn";
			this._cancelBtn.Size = new System.Drawing.Size( 56, 22 );
			this._cancelBtn.TabIndex = 2;
			this._cancelBtn.Text = "Cancel";
			// 
			// _desc
			// 
			this._desc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._desc.Location = new System.Drawing.Point( 8, 8 );
			this._desc.Name = "_desc";
			this._desc.Size = new System.Drawing.Size( 382, 51 );
			this._desc.TabIndex = 2;
			this._desc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// PromptDlg
			// 
			this.AcceptButton = this._okBtn;
			this.AutoScaleBaseSize = new System.Drawing.Size( 5, 13 );
			this.CancelButton = this._cancelBtn;
			this.ClientSize = new System.Drawing.Size( 400, 124 );
			this.Controls.Add( this._desc );
			this.Controls.Add( this._okBtn );
			this.Controls.Add( this._txt );
			this.Controls.Add( this._cancelBtn );
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size( 153, 111 );
			this.Name = "PromptDlg";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "PromptDlg...";
			this.ResumeLayout( false );
			this.PerformLayout();

		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if( components != null )
					components.Dispose();
			}
			base.Dispose( disposing );
		}

		private void OnTextChanged(object sender, System.EventArgs e)
		{
			_okBtn.Enabled = _acceptNoChange || _txt.Text != _originalText;
		}
	}
}
