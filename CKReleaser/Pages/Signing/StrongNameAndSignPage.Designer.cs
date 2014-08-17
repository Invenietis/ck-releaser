#region LGPL License
/*----------------------------------------------------------------------------
* This file (CKReleaser\CKReleaser\Pages\StrongNaming\StrongNamingPage.Designer.cs) is part of CiviKey. 
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

namespace CK.Releaser.Signing
{
    partial class StrongNameAndSignPage
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

        void InitializeComponent()
        {            this._allOutputs = new System.Windows.Forms.ListView();
            this._columnSolutionPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._privateKeyPath = new System.Windows.Forms.TextBox();
            this._privateKeyBox = new System.Windows.Forms.GroupBox();
            this._privateKeyDesc = new System.Windows.Forms.Label();
            this._processStrongName = new System.Windows.Forms.Button();
            this._privateKeyPathChoose = new System.Windows.Forms.Button();
            this._strongNameLabel = new System.Windows.Forms.Label();
            this._outputBox = new System.Windows.Forms.GroupBox();
            this._digitalSignatureBox = new System.Windows.Forms.GroupBox();
            this._pfxDescription = new System.Windows.Forms.Label();
            this._pfxPathLabel = new System.Windows.Forms.Label();
            this._processSignTool = new System.Windows.Forms.Button();
            this._pfxFilePathFind = new System.Windows.Forms.Button();
            this._pfxFilePath = new System.Windows.Forms.TextBox();
            this._signToolPathLabel = new System.Windows.Forms.Label();
            this._signToolFind = new System.Windows.Forms.Button();
            this._signToolPath = new System.Windows.Forms.TextBox();
            this._privateKeyDesc2 = new System.Windows.Forms.Label();
            this._privateKeyBox.SuspendLayout();
            this._outputBox.SuspendLayout();
            this._digitalSignatureBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // _allOutputs
            // 
            this._allOutputs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._allOutputs.CheckBoxes = true;
            this._allOutputs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this._columnSolutionPath});
            this._allOutputs.Location = new System.Drawing.Point(6, 14);
            this._allOutputs.Name = "_allOutputs";
            this._allOutputs.Size = new System.Drawing.Size(770, 307);
            this._allOutputs.TabIndex = 0;
            this._allOutputs.UseCompatibleStateImageBehavior = false;
            this._allOutputs.View = System.Windows.Forms.View.Details;
            // 
            // _columnSolutionPath
            // 
            this._columnSolutionPath.Text = "Path";
            this._columnSolutionPath.Width = 334;
            // 
            // _privateKeyPath
            // 
            this._privateKeyPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._privateKeyPath.Location = new System.Drawing.Point(127, 19);
            this._privateKeyPath.Name = "_privateKeyPath";
            this._privateKeyPath.ReadOnly = true;
            this._privateKeyPath.Size = new System.Drawing.Size(614, 20);
            this._privateKeyPath.TabIndex = 2;
            // 
            // _privateKeyBox
            // 
            this._privateKeyBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._privateKeyBox.Controls.Add(this._privateKeyDesc2);
            this._privateKeyBox.Controls.Add(this._privateKeyDesc);
            this._privateKeyBox.Controls.Add(this._processStrongName);
            this._privateKeyBox.Controls.Add(this._privateKeyPathChoose);
            this._privateKeyBox.Controls.Add(this._privateKeyPath);
            this._privateKeyBox.Controls.Add(this._strongNameLabel);
            this._privateKeyBox.Location = new System.Drawing.Point(4, 337);
            this._privateKeyBox.Name = "_privateKeyBox";
            this._privateKeyBox.Size = new System.Drawing.Size(782, 88);
            this._privateKeyBox.TabIndex = 3;
            this._privateKeyBox.TabStop = false;
            this._privateKeyBox.Text = "1 - Official Strong Naming key";
            // 
            // _privateKeyDesc
            // 
            this._privateKeyDesc.AutoSize = true;
            this._privateKeyDesc.Location = new System.Drawing.Point(299, 49);
            this._privateKeyDesc.Name = "_privateKeyDesc";
            this._privateKeyDesc.Size = new System.Drawing.Size(171, 13);
            this._privateKeyDesc.TabIndex = 4;
            this._privateKeyDesc.Text = "A private key file must be selected.";
            // 
            // _processStrongName
            // 
            this._processStrongName.Enabled = false;
            this._processStrongName.Location = new System.Drawing.Point(6, 45);
            this._processStrongName.Name = "_processStrongName";
            this._processStrongName.Size = new System.Drawing.Size(287, 36);
            this._processStrongName.TabIndex = 5;
            this._processStrongName.Text = "Replace Shared Key with official Strong Name key";
            this._processStrongName.UseVisualStyleBackColor = true;
            this._processStrongName.Click += new System.EventHandler(this._processStrongName_Click);
            // 
            // _privateKeyPathChoose
            // 
            this._privateKeyPathChoose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._privateKeyPathChoose.Location = new System.Drawing.Point(747, 19);
            this._privateKeyPathChoose.Name = "_privateKeyPathChoose";
            this._privateKeyPathChoose.Size = new System.Drawing.Size(29, 20);
            this._privateKeyPathChoose.TabIndex = 3;
            this._privateKeyPathChoose.Text = "...";
            this._privateKeyPathChoose.UseVisualStyleBackColor = true;
            this._privateKeyPathChoose.Click += new System.EventHandler(this._privateKeyPathChoose_Click);
            // 
            // _strongNameLabel
            // 
            this._strongNameLabel.AutoSize = true;
            this._strongNameLabel.Location = new System.Drawing.Point(18, 23);
            this._strongNameLabel.Name = "_strongNameLabel";
            this._strongNameLabel.Size = new System.Drawing.Size(82, 13);
            this._strongNameLabel.TabIndex = 4;
            this._strongNameLabel.Text = "Private .snk file:";
            this._strongNameLabel.Click += new System.EventHandler(this._strongNameLabel_Click);
            // 
            // _outputBox
            // 
            this._outputBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._outputBox.Controls.Add(this._allOutputs);
            this._outputBox.Location = new System.Drawing.Point(4, 4);
            this._outputBox.Name = "_outputBox";
            this._outputBox.Size = new System.Drawing.Size(782, 327);
            this._outputBox.TabIndex = 4;
            this._outputBox.TabStop = false;
            this._outputBox.Text = "All Outputs";
            // 
            // _digitalSignatureBox
            // 
            this._digitalSignatureBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._digitalSignatureBox.Controls.Add(this._pfxDescription);
            this._digitalSignatureBox.Controls.Add(this._pfxPathLabel);
            this._digitalSignatureBox.Controls.Add(this._processSignTool);
            this._digitalSignatureBox.Controls.Add(this._pfxFilePathFind);
            this._digitalSignatureBox.Controls.Add(this._pfxFilePath);
            this._digitalSignatureBox.Controls.Add(this._signToolPathLabel);
            this._digitalSignatureBox.Controls.Add(this._signToolFind);
            this._digitalSignatureBox.Controls.Add(this._signToolPath);
            this._digitalSignatureBox.Location = new System.Drawing.Point(4, 431);
            this._digitalSignatureBox.Name = "_digitalSignatureBox";
            this._digitalSignatureBox.Size = new System.Drawing.Size(782, 125);
            this._digitalSignatureBox.TabIndex = 5;
            this._digitalSignatureBox.TabStop = false;
            this._digitalSignatureBox.Text = "2 - Digital Signature (Authenticode)";
            // 
            // _pfxDescription
            // 
            this._pfxDescription.AutoSize = true;
            this._pfxDescription.Location = new System.Drawing.Point(301, 87);
            this._pfxDescription.Name = "_pfxDescription";
            this._pfxDescription.Size = new System.Drawing.Size(213, 13);
            this._pfxDescription.TabIndex = 4;
            this._pfxDescription.Text = "SignTool.exe will be run on each output file.";
            // 
            // _pfxPathLabel
            // 
            this._pfxPathLabel.AutoSize = true;
            this._pfxPathLabel.Location = new System.Drawing.Point(18, 52);
            this._pfxPathLabel.Name = "_pfxPathLabel";
            this._pfxPathLabel.Size = new System.Drawing.Size(109, 13);
            this._pfxPathLabel.TabIndex = 7;
            this._pfxPathLabel.Text = "Authenticode .pfx file:";
            // 
            // _processSignTool
            // 
            this._processSignTool.Enabled = false;
            this._processSignTool.Location = new System.Drawing.Point(8, 75);
            this._processSignTool.Name = "_processSignTool";
            this._processSignTool.Size = new System.Drawing.Size(287, 36);
            this._processSignTool.TabIndex = 5;
            this._processSignTool.Text = "Digitally Sign files with Authenticode";
            this._processSignTool.UseVisualStyleBackColor = true;
            this._processSignTool.Click += new System.EventHandler(this._processSignTool_Click);
            // 
            // _pfxFilePathFind
            // 
            this._pfxFilePathFind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._pfxFilePathFind.Location = new System.Drawing.Point(747, 49);
            this._pfxFilePathFind.Name = "_pfxFilePathFind";
            this._pfxFilePathFind.Size = new System.Drawing.Size(29, 20);
            this._pfxFilePathFind.TabIndex = 6;
            this._pfxFilePathFind.Text = "...";
            this._pfxFilePathFind.UseVisualStyleBackColor = true;
            this._pfxFilePathFind.Click += new System.EventHandler(this._pfxFilePathFind_Click);
            // 
            // _pfxFilePath
            // 
            this._pfxFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._pfxFilePath.Location = new System.Drawing.Point(127, 49);
            this._pfxFilePath.Name = "_pfxFilePath";
            this._pfxFilePath.ReadOnly = true;
            this._pfxFilePath.Size = new System.Drawing.Size(614, 20);
            this._pfxFilePath.TabIndex = 5;
            // 
            // _signToolPathLabel
            // 
            this._signToolPathLabel.AutoSize = true;
            this._signToolPathLabel.Location = new System.Drawing.Point(18, 26);
            this._signToolPathLabel.Name = "_signToolPathLabel";
            this._signToolPathLabel.Size = new System.Drawing.Size(96, 13);
            this._signToolPathLabel.TabIndex = 4;
            this._signToolPathLabel.Text = "SignTool.exe path:";
            // 
            // _signToolFind
            // 
            this._signToolFind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._signToolFind.Location = new System.Drawing.Point(747, 23);
            this._signToolFind.Name = "_signToolFind";
            this._signToolFind.Size = new System.Drawing.Size(29, 20);
            this._signToolFind.TabIndex = 3;
            this._signToolFind.Text = "...";
            this._signToolFind.UseVisualStyleBackColor = true;
            this._signToolFind.Click += new System.EventHandler(this._signToolFind_Click);
            // 
            // _signToolPath
            // 
            this._signToolPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._signToolPath.Location = new System.Drawing.Point(127, 23);
            this._signToolPath.Name = "_signToolPath";
            this._signToolPath.ReadOnly = true;
            this._signToolPath.Size = new System.Drawing.Size(614, 20);
            this._signToolPath.TabIndex = 2;
            // 
            // _privateKeyDesc2
            // 
            this._privateKeyDesc2.AutoSize = true;
            this._privateKeyDesc2.Location = new System.Drawing.Point(299, 67);
            this._privateKeyDesc2.Name = "_privateKeyDesc2";
            this._privateKeyDesc2.Size = new System.Drawing.Size(42, 13);
            this._privateKeyDesc2.TabIndex = 6;
            this._privateKeyDesc2.Text = "<more>";
            // 
            // StrongNameAndSignPage
            // 
            this.Controls.Add(this._digitalSignatureBox);
            this.Controls.Add(this._outputBox);
            this.Controls.Add(this._privateKeyBox);
            this.Name = "StrongNameAndSignPage";
            this.Size = new System.Drawing.Size(789, 560);
            this._privateKeyBox.ResumeLayout(false);
            this._privateKeyBox.PerformLayout();
            this._outputBox.ResumeLayout(false);
            this._digitalSignatureBox.ResumeLayout(false);
            this._digitalSignatureBox.PerformLayout();
            this.ResumeLayout(false);

}

        private System.Windows.Forms.ListView _allOutputs;
        private System.Windows.Forms.ColumnHeader _columnSolutionPath;
        private System.Windows.Forms.TextBox _privateKeyPath;
        private System.Windows.Forms.GroupBox _privateKeyBox;
        private System.Windows.Forms.Label _privateKeyDesc;
        private System.Windows.Forms.Button _privateKeyPathChoose;
        private System.Windows.Forms.GroupBox _outputBox;
        private System.Windows.Forms.Button _processStrongName;
        private System.Windows.Forms.Label _strongNameLabel;
        private System.Windows.Forms.GroupBox _digitalSignatureBox;
        private System.Windows.Forms.Label _pfxDescription;
        private System.Windows.Forms.Label _pfxPathLabel;
        private System.Windows.Forms.Button _processSignTool;
        private System.Windows.Forms.Button _pfxFilePathFind;
        private System.Windows.Forms.TextBox _pfxFilePath;
        private System.Windows.Forms.Label _signToolPathLabel;
        private System.Windows.Forms.Button _signToolFind;
        private System.Windows.Forms.TextBox _signToolPath;
        private System.Windows.Forms.Label _privateKeyDesc2;


    }
}
