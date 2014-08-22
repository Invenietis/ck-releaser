#region LGPL License
/*----------------------------------------------------------------------------
* This file (CKReleaser\CKReleaser\Pages\Versions\VersionsPage.Designer.cs) is part of CiviKey. 
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

namespace CK.Releaser.Info
{
    partial class InfoReleasePage
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
            this._currentBox = new System.Windows.Forms.GroupBox();
            this._currentPath = new System.Windows.Forms.TextBox();
            this._currentPathLabel = new System.Windows.Forms.Label();
            this._chooseCurrent = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this._currentHeadStatus = new System.Windows.Forms.TextBox();
            this._createCurrentBranch = new System.Windows.Forms.Button();
            this._currentBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // _currentBox
            // 
            this._currentBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._currentBox.Controls.Add(this._currentPath);
            this._currentBox.Controls.Add(this._currentPathLabel);
            this._currentBox.Controls.Add(this._chooseCurrent);
            this._currentBox.Location = new System.Drawing.Point(4, 4);
            this._currentBox.Name = "_currentBox";
            this._currentBox.Size = new System.Drawing.Size(749, 47);
            this._currentBox.TabIndex = 0;
            this._currentBox.TabStop = false;
            this._currentBox.Text = "Current database";
            // 
            // _currentPath
            // 
            this._currentPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._currentPath.Location = new System.Drawing.Point(61, 17);
            this._currentPath.Name = "_currentPath";
            this._currentPath.ReadOnly = true;
            this._currentPath.Size = new System.Drawing.Size(647, 20);
            this._currentPath.TabIndex = 2;
            this._currentPath.Text = "<none>";
            // 
            // _currentPathLabel
            // 
            this._currentPathLabel.AutoSize = true;
            this._currentPathLabel.Location = new System.Drawing.Point(7, 20);
            this._currentPathLabel.Name = "_currentPathLabel";
            this._currentPathLabel.Size = new System.Drawing.Size(48, 13);
            this._currentPathLabel.TabIndex = 1;
            this._currentPathLabel.Text = "Full Path";
            // 
            // _chooseCurrent
            // 
            this._chooseCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._chooseCurrent.Location = new System.Drawing.Point(714, 15);
            this._chooseCurrent.Name = "_chooseCurrent";
            this._chooseCurrent.Size = new System.Drawing.Size(29, 23);
            this._chooseCurrent.TabIndex = 0;
            this._chooseCurrent.Text = "...";
            this._chooseCurrent.UseVisualStyleBackColor = true;
            this._chooseCurrent.Click += new System.EventHandler(this._chooseCurrent_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(606, 228);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(606, 228);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2ddcdcdcdc";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(606, 228);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // _currentHeadStatus
            // 
            this._currentHeadStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._currentHeadStatus.BackColor = System.Drawing.SystemColors.Control;
            this._currentHeadStatus.Location = new System.Drawing.Point(4, 57);
            this._currentHeadStatus.Multiline = true;
            this._currentHeadStatus.Name = "_currentHeadStatus";
            this._currentHeadStatus.Size = new System.Drawing.Size(618, 50);
            this._currentHeadStatus.TabIndex = 5;
            // 
            // _createCurrentBranch
            // 
            this._createCurrentBranch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._createCurrentBranch.Location = new System.Drawing.Point(629, 71);
            this._createCurrentBranch.Name = "_createCurrentBranch";
            this._createCurrentBranch.Size = new System.Drawing.Size(118, 23);
            this._createCurrentBranch.TabIndex = 6;
            this._createCurrentBranch.Text = "Create branch";
            this._createCurrentBranch.UseVisualStyleBackColor = true;
            // 
            // InfoReleasePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._createCurrentBranch);
            this.Controls.Add(this._currentHeadStatus);
            this.Controls.Add(this._currentBox);
            this.Name = "InfoReleasePage";
            this.Size = new System.Drawing.Size(756, 439);
            this._currentBox.ResumeLayout(false);
            this._currentBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox _currentBox;
        private System.Windows.Forms.TextBox _currentPath;
        private System.Windows.Forms.Label _currentPathLabel;
        private System.Windows.Forms.Button _chooseCurrent;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox _currentHeadStatus;
        private System.Windows.Forms.Button _createCurrentBranch;
    }
}
