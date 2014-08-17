#region LGPL License
/*----------------------------------------------------------------------------
* This file (CKReleaser\CKReleaser\MainWindowHeader.Designer.cs) is part of CiviKey. 
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
    partial class MainWindowHeader
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindowHeader));
            this._infoCount = new System.Windows.Forms.Label();
            this._gitBranchName = new System.Windows.Forms.TextBox();
            this._chooseFolderPath = new System.Windows.Forms.Button();
            this._toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this._groupVersionAndGit = new System.Windows.Forms.GroupBox();
            this._versionButton = new System.Windows.Forms.Button();
            this._solutionFolderPath = new System.Windows.Forms.TextBox();
            this._openPath = new System.Windows.Forms.Button();
            this._imageList = new System.Windows.Forms.ImageList(this.components);
            this._folderWritable = new System.Windows.Forms.Label();
            this._groupVersionAndGit.SuspendLayout();
            this.SuspendLayout();
            // 
            // _infoCount
            // 
            this._infoCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._infoCount.AutoSize = true;
            this._infoCount.Location = new System.Drawing.Point(307, 17);
            this._infoCount.Name = "_infoCount";
            this._infoCount.Size = new System.Drawing.Size(159, 13);
            this._infoCount.TabIndex = 6;
            this._infoCount.Text = "Contains {0} .csproj and {1} .sln.";
            // 
            // _gitBranchName
            // 
            this._gitBranchName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._gitBranchName.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this._gitBranchName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._gitBranchName.Location = new System.Drawing.Point(236, 13);
            this._gitBranchName.Name = "_gitBranchName";
            this._gitBranchName.ReadOnly = true;
            this._gitBranchName.Size = new System.Drawing.Size(114, 20);
            this._gitBranchName.TabIndex = 7;
            this._gitBranchName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._toolTip.SetToolTip(this._gitBranchName, "AAA");
            // 
            // _chooseFolderPath
            // 
            this._chooseFolderPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._chooseFolderPath.Location = new System.Drawing.Point(280, 14);
            this._chooseFolderPath.Name = "_chooseFolderPath";
            this._chooseFolderPath.Size = new System.Drawing.Size(24, 20);
            this._chooseFolderPath.TabIndex = 8;
            this._chooseFolderPath.Text = "...";
            this._chooseFolderPath.UseVisualStyleBackColor = true;
            this._chooseFolderPath.Click += new System.EventHandler(this._chooseFolderPath_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Version";
            // 
            // _groupVersionAndGit
            // 
            this._groupVersionAndGit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._groupVersionAndGit.Controls.Add(this._folderWritable);
            this._groupVersionAndGit.Controls.Add(this._versionButton);
            this._groupVersionAndGit.Controls.Add(this.label2);
            this._groupVersionAndGit.Controls.Add(this._gitBranchName);
            this._groupVersionAndGit.Location = new System.Drawing.Point(472, -1);
            this._groupVersionAndGit.Name = "_groupVersionAndGit";
            this._groupVersionAndGit.Size = new System.Drawing.Size(371, 42);
            this._groupVersionAndGit.TabIndex = 10;
            this._groupVersionAndGit.TabStop = false;
            // 
            // _versionButton
            // 
            this._versionButton.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._versionButton.Location = new System.Drawing.Point(47, 12);
            this._versionButton.Name = "_versionButton";
            this._versionButton.Size = new System.Drawing.Size(185, 23);
            this._versionButton.TabIndex = 10;
            this._versionButton.Text = "14.14.15-develop.12";
            this._versionButton.UseVisualStyleBackColor = true;
            this._versionButton.Click += new System.EventHandler(this._versionButton_Click);
            // 
            // _solutionFolderPath
            // 
            this._solutionFolderPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._solutionFolderPath.Location = new System.Drawing.Point(33, 14);
            this._solutionFolderPath.Name = "_solutionFolderPath";
            this._solutionFolderPath.ReadOnly = true;
            this._solutionFolderPath.Size = new System.Drawing.Size(243, 20);
            this._solutionFolderPath.TabIndex = 5;
            // 
            // _openPath
            // 
            this._openPath.ImageIndex = 0;
            this._openPath.ImageList = this._imageList;
            this._openPath.Location = new System.Drawing.Point(4, 14);
            this._openPath.Name = "_openPath";
            this._openPath.Size = new System.Drawing.Size(23, 20);
            this._openPath.TabIndex = 11;
            this._openPath.UseVisualStyleBackColor = true;
            this._openPath.Click += new System.EventHandler(this._openPath_Click);
            // 
            // _imageList
            // 
            this._imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_imageList.ImageStream")));
            this._imageList.TransparentColor = System.Drawing.Color.Transparent;
            this._imageList.Images.SetKeyName(0, "Folder.png");
            this._imageList.Images.SetKeyName(1, "LockOpened.png");
            this._imageList.Images.SetKeyName(2, "LockClosed.png");
            // 
            // _folderWritable
            // 
            this._folderWritable.AutoSize = true;
            this._folderWritable.ImageIndex = 2;
            this._folderWritable.ImageList = this._imageList;
            this._folderWritable.Location = new System.Drawing.Point(354, 16);
            this._folderWritable.Name = "_folderWritable";
            this._folderWritable.Size = new System.Drawing.Size(10, 13);
            this._folderWritable.TabIndex = 11;
            this._folderWritable.Text = " ";
            // 
            // MainWindowHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._openPath);
            this.Controls.Add(this._groupVersionAndGit);
            this.Controls.Add(this._chooseFolderPath);
            this.Controls.Add(this._infoCount);
            this.Controls.Add(this._solutionFolderPath);
            this.Name = "MainWindowHeader";
            this.Size = new System.Drawing.Size(846, 43);
            this._groupVersionAndGit.ResumeLayout(false);
            this._groupVersionAndGit.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _infoCount;
        private System.Windows.Forms.TextBox _gitBranchName;
        private System.Windows.Forms.Button _chooseFolderPath;
        private System.Windows.Forms.ToolTip _toolTip;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox _groupVersionAndGit;
        private System.Windows.Forms.Button _versionButton;
        private System.Windows.Forms.TextBox _solutionFolderPath;
        private System.Windows.Forms.Button _openPath;
        private System.Windows.Forms.ImageList _imageList;
        private System.Windows.Forms.Label _folderWritable;
    }
}
