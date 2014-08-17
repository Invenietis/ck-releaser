#region LGPL License
/*----------------------------------------------------------------------------
* This file (CKReleaser\CKReleaser\Pages\HeaderUpdate\HeaderUpdatePage.Designer.cs) is part of CiviKey. 
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

namespace CK.Releaser.HeaderUpdate
{
    partial class HeaderUpdatePage
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this._selectedPath = new System.Windows.Forms.TextBox();
            this._choose = new System.Windows.Forms.Button();
            this._process = new System.Windows.Forms.Button();
            this._addNew = new System.Windows.Forms.CheckBox();
            this._removeExisting = new System.Windows.Forms.CheckBox();
            this._text = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 398);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Root to Process";
            // 
            // _selectedPath
            // 
            this._selectedPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._selectedPath.Location = new System.Drawing.Point(96, 395);
            this._selectedPath.Name = "_selectedPath";
            this._selectedPath.Size = new System.Drawing.Size(558, 20);
            this._selectedPath.TabIndex = 17;
            // 
            // _choose
            // 
            this._choose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._choose.Location = new System.Drawing.Point(660, 393);
            this._choose.Name = "_choose";
            this._choose.Size = new System.Drawing.Size(27, 22);
            this._choose.TabIndex = 15;
            this._choose.Text = "...";
            this._choose.UseVisualStyleBackColor = true;
            this._choose.Click += new System.EventHandler(this._choose_Click);
            // 
            // _process
            // 
            this._process.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._process.Location = new System.Drawing.Point(584, 422);
            this._process.Name = "_process";
            this._process.Size = new System.Drawing.Size(103, 21);
            this._process.TabIndex = 16;
            this._process.Text = "Process";
            this._process.UseVisualStyleBackColor = true;
            this._process.Click += new System.EventHandler(this._process_Click);
            // 
            // _addNew
            // 
            this._addNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._addNew.AutoSize = true;
            this._addNew.Checked = true;
            this._addNew.CheckState = System.Windows.Forms.CheckState.Checked;
            this._addNew.Location = new System.Drawing.Point(164, 424);
            this._addNew.Name = "_addNew";
            this._addNew.Size = new System.Drawing.Size(106, 17);
            this._addNew.TabIndex = 13;
            this._addNew.Text = "Add new Header";
            this._addNew.UseVisualStyleBackColor = true;
            // 
            // _removeExisting
            // 
            this._removeExisting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._removeExisting.AutoSize = true;
            this._removeExisting.Checked = true;
            this._removeExisting.CheckState = System.Windows.Forms.CheckState.Checked;
            this._removeExisting.Location = new System.Drawing.Point(11, 425);
            this._removeExisting.Name = "_removeExisting";
            this._removeExisting.Size = new System.Drawing.Size(147, 17);
            this._removeExisting.TabIndex = 14;
            this._removeExisting.Text = "Remove existing Headers";
            this._removeExisting.UseVisualStyleBackColor = true;
            // 
            // _text
            // 
            this._text.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._text.Location = new System.Drawing.Point(3, 3);
            this._text.Multiline = true;
            this._text.Name = "_text";
            this._text.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._text.Size = new System.Drawing.Size(685, 384);
            this._text.TabIndex = 12;
            // 
            // HeaderUpdatePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this._selectedPath);
            this.Controls.Add(this._choose);
            this.Controls.Add(this._process);
            this.Controls.Add(this._addNew);
            this.Controls.Add(this._removeExisting);
            this.Controls.Add(this._text);
            this.Name = "HeaderUpdatePage";
            this.Size = new System.Drawing.Size(691, 455);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _selectedPath;
        private System.Windows.Forms.Button _choose;
        private System.Windows.Forms.Button _process;
        private System.Windows.Forms.CheckBox _addNew;
        private System.Windows.Forms.CheckBox _removeExisting;
        private System.Windows.Forms.TextBox _text;
    }
}
