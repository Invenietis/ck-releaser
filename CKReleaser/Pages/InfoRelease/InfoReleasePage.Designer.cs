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
            this._dbTreeView = new System.Windows.Forms.TreeView();
            this._dbContentContainer = new System.Windows.Forms.SplitContainer();
            this._dbContentPages = new CK.Releaser.GUI.TabControlWithClosingBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this._currentHeadStatus = new System.Windows.Forms.TextBox();
            this._createCurrentBranch = new System.Windows.Forms.Button();
            this._currentBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dbContentContainer)).BeginInit();
            this._dbContentContainer.Panel1.SuspendLayout();
            this._dbContentContainer.Panel2.SuspendLayout();
            this._dbContentContainer.SuspendLayout();
            this._dbContentPages.SuspendLayout();
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
            // _dbTreeView
            // 
            this._dbTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dbTreeView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._dbTreeView.HideSelection = false;
            this._dbTreeView.Location = new System.Drawing.Point(0, 0);
            this._dbTreeView.Name = "_dbTreeView";
            this._dbTreeView.Size = new System.Drawing.Size(138, 319);
            this._dbTreeView.TabIndex = 2;
            this._dbTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this._dbTreeView_AfterSelect);
            this._dbTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this._dbTreeView_NodeMouseClick);
            // 
            // _dbContentContainer
            // 
            this._dbContentContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._dbContentContainer.Location = new System.Drawing.Point(0, 113);
            this._dbContentContainer.Name = "_dbContentContainer";
            // 
            // _dbContentContainer.Panel1
            // 
            this._dbContentContainer.Panel1.Controls.Add(this._dbTreeView);
            // 
            // _dbContentContainer.Panel2
            // 
            this._dbContentContainer.Panel2.Controls.Add(this._dbContentPages);
            this._dbContentContainer.Size = new System.Drawing.Size(753, 319);
            this._dbContentContainer.SplitterDistance = 138;
            this._dbContentContainer.TabIndex = 3;
            // 
            // _dbContentPages
            // 
            this._dbContentPages.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this._dbContentPages.Controls.Add(this.tabPage4);
            this._dbContentPages.Controls.Add(this.tabPage5);
            this._dbContentPages.Controls.Add(this.tabPage6);
            this._dbContentPages.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dbContentPages.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this._dbContentPages.ItemSize = new System.Drawing.Size(300, 18);
            this._dbContentPages.Location = new System.Drawing.Point(0, 0);
            this._dbContentPages.Name = "_dbContentPages";
            this._dbContentPages.Padding = new System.Drawing.Point(20, 0);
            this._dbContentPages.SelectedIndex = 0;
            this._dbContentPages.Size = new System.Drawing.Size(611, 319);
            this._dbContentPages.TabIndex = 1;
            this._dbContentPages.SelectedIndexChanged += new System.EventHandler(this._dbContentPages_SelectedIndexChanged);
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 4);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(603, 293);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "tabPage4 etec cy rtyr t crtcy";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Location = new System.Drawing.Point(4, 4);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(603, 293);
            this.tabPage5.TabIndex = 2;
            this.tabPage5.Text = "tabPage5";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tabPage6
            // 
            this.tabPage6.Location = new System.Drawing.Point(4, 4);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(603, 293);
            this.tabPage6.TabIndex = 3;
            this.tabPage6.Text = "t";
            this.tabPage6.UseVisualStyleBackColor = true;
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
            this._createCurrentBranch.Click += new System.EventHandler(this._createCurrentBranch_Click);
            // 
            // InfoReleasePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._createCurrentBranch);
            this.Controls.Add(this._currentHeadStatus);
            this.Controls.Add(this._dbContentContainer);
            this.Controls.Add(this._currentBox);
            this.Name = "InfoReleasePage";
            this.Size = new System.Drawing.Size(756, 439);
            this._currentBox.ResumeLayout(false);
            this._currentBox.PerformLayout();
            this._dbContentContainer.Panel1.ResumeLayout(false);
            this._dbContentContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._dbContentContainer)).EndInit();
            this._dbContentContainer.ResumeLayout(false);
            this._dbContentPages.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox _currentBox;
        private System.Windows.Forms.TextBox _currentPath;
        private System.Windows.Forms.Label _currentPathLabel;
        private System.Windows.Forms.Button _chooseCurrent;
        private System.Windows.Forms.TreeView _dbTreeView;
        private System.Windows.Forms.SplitContainer _dbContentContainer;
        private CK.Releaser.GUI.TabControlWithClosingBox _dbContentPages;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox _currentHeadStatus;
        private System.Windows.Forms.Button _createCurrentBranch;
    }
}
