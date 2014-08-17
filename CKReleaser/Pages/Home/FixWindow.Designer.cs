#region LGPL License
/*----------------------------------------------------------------------------
* This file (CKReleaser\CKReleaser\Pages\Home\FixWindow.Designer.cs) is part of CiviKey. 
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

namespace CK.Releaser.Home
{
    partial class FixWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if( disposing && (components != null) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._closeAndApply = new System.Windows.Forms.Button();
            this._close = new System.Windows.Forms.Button();
            this._fixList = new System.Windows.Forms.ListView();
            this._columnText = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._columnMemoryKey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // _closeAndApply
            // 
            this._closeAndApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._closeAndApply.Location = new System.Drawing.Point(12, 386);
            this._closeAndApply.Name = "_closeAndApply";
            this._closeAndApply.Size = new System.Drawing.Size(147, 23);
            this._closeAndApply.TabIndex = 0;
            this._closeAndApply.Text = "Close and Apply {0} fix(es)";
            this._closeAndApply.UseVisualStyleBackColor = true;
            this._closeAndApply.Click += new System.EventHandler(this._closeAndApply_Click);
            // 
            // _close
            // 
            this._close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._close.Location = new System.Drawing.Point(457, 386);
            this._close.Name = "_close";
            this._close.Size = new System.Drawing.Size(75, 23);
            this._close.TabIndex = 1;
            this._close.Text = "Close";
            this._close.UseVisualStyleBackColor = true;
            this._close.Click += new System.EventHandler(this._close_Click);
            // 
            // _fixList
            // 
            this._fixList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._fixList.CheckBoxes = true;
            this._fixList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this._columnText,
            this._columnMemoryKey});
            this._fixList.Location = new System.Drawing.Point(12, 12);
            this._fixList.Name = "_fixList";
            this._fixList.Size = new System.Drawing.Size(520, 368);
            this._fixList.TabIndex = 2;
            this._fixList.UseCompatibleStateImageBehavior = false;
            this._fixList.View = System.Windows.Forms.View.Details;
            this._fixList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this._fixList_ItemCheck);
            // 
            // _columnText
            // 
            this._columnText.Text = "Text";
            this._columnText.Width = 359;
            // 
            // _columnMemoryKey
            // 
            this._columnMemoryKey.Text = "MemoryKey (in Solution.ck)";
            this._columnMemoryKey.Width = 142;
            // 
            // FixWindow
            // 
            this.AcceptButton = this._closeAndApply;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._close;
            this.ClientSize = new System.Drawing.Size(544, 421);
            this.Controls.Add(this._fixList);
            this.Controls.Add(this._close);
            this.Controls.Add(this._closeAndApply);
            this.Name = "FixWindow";
            this.Text = "FixWindow";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _closeAndApply;
        private System.Windows.Forms.Button _close;
        private System.Windows.Forms.ListView _fixList;
        private System.Windows.Forms.ColumnHeader _columnText;
        private System.Windows.Forms.ColumnHeader _columnMemoryKey;
    }
}