#region LGPL License
/*----------------------------------------------------------------------------
* This file (CKReleaser\CKReleaser\MainWindow.Designer.cs) is part of CiviKey. 
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

namespace CK.Releaser
{
    partial class MainWindow
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
            this._mainSplit = new System.Windows.Forms.SplitContainer();
            this._navigation = new System.Windows.Forms.TreeView();
            this._mainWindowHeader = new CK.Releaser.MainWindowHeader();
            ((System.ComponentModel.ISupportInitialize)(this._mainSplit)).BeginInit();
            this._mainSplit.Panel1.SuspendLayout();
            this._mainSplit.SuspendLayout();
            this.SuspendLayout();
            // 
            // _mainSplit
            // 
            this._mainSplit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._mainSplit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this._mainSplit.Location = new System.Drawing.Point(0, 56);
            this._mainSplit.Name = "_mainSplit";
            // 
            // _mainSplit.Panel1
            // 
            this._mainSplit.Panel1.Controls.Add(this._navigation);
            this._mainSplit.Size = new System.Drawing.Size(1053, 448);
            this._mainSplit.SplitterDistance = 258;
            this._mainSplit.TabIndex = 1;
            // 
            // _navigation
            // 
            this._navigation.Dock = System.Windows.Forms.DockStyle.Fill;
            this._navigation.Location = new System.Drawing.Point(0, 0);
            this._navigation.Name = "_navigation";
            this._navigation.Size = new System.Drawing.Size(254, 444);
            this._navigation.TabIndex = 1;
            this._navigation.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this._navigation_AfterSelect);
            // 
            // _mainWindowHeader
            // 
            this._mainWindowHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._mainWindowHeader.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this._mainWindowHeader.Location = new System.Drawing.Point(0, 0);
            this._mainWindowHeader.Name = "_mainWindowHeader";
            this._mainWindowHeader.Size = new System.Drawing.Size(1053, 50);
            this._mainWindowHeader.TabIndex = 2;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1053, 502);
            this.Controls.Add(this._mainWindowHeader);
            this.Controls.Add(this._mainSplit);
            this.Name = "MainWindow";
            this.Text = "Form1";
            this._mainSplit.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._mainSplit)).EndInit();
            this._mainSplit.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer _mainSplit;
        private System.Windows.Forms.TreeView _navigation;
        private MainWindowHeader _mainWindowHeader;
    }
}

