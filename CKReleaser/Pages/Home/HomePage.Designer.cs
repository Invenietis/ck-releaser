#region LGPL License
/*----------------------------------------------------------------------------
* This file (CKReleaser\CKReleaser\Pages\Home\HomePage.Designer.cs) is part of CiviKey. 
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
    partial class HomePage
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
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("ListViewGroup", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("ListViewGroup", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("ListViewGroup", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("zszszszs", 0);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("szszszszszszszededed e d ed e de d e d  ed ededed eded e d e de d e d e d ed e d " +
        "ed e d ed de d e de d e de  edszszs", 2);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("aaaaaaaa", 3);
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("waaarrrrning", 1);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HomePage));
            this._logs = new System.Windows.Forms.ListView();
            this._logImage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._logImageList = new System.Windows.Forms.ImageList(this.components);
            this._refresh = new System.Windows.Forms.Button();
            this._toolTip = new System.Windows.Forms.ToolTip(this.components);
            this._validationSummary = new System.Windows.Forms.TextBox();
            this._openFixes = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _logs
            // 
            this._logs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._logs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this._logImage});
            listViewGroup1.Header = "ListViewGroup";
            listViewGroup1.Name = "listViewGroup1";
            listViewGroup2.Header = "ListViewGroup";
            listViewGroup2.Name = "listViewGroup2";
            listViewGroup3.Header = "ListViewGroup";
            listViewGroup3.Name = "listViewGroup3";
            this._logs.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2,
            listViewGroup3});
            this._logs.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            listViewItem3.Group = listViewGroup1;
            this._logs.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4});
            this._logs.Location = new System.Drawing.Point(3, 32);
            this._logs.MultiSelect = false;
            this._logs.Name = "_logs";
            this._logs.Size = new System.Drawing.Size(925, 397);
            this._logs.SmallImageList = this._logImageList;
            this._logs.TabIndex = 0;
            this._logs.UseCompatibleStateImageBehavior = false;
            this._logs.View = System.Windows.Forms.View.Details;
            // 
            // _logImage
            // 
            this._logImage.Text = "";
            this._logImage.Width = 1024;
            // 
            // _logImageList
            // 
            this._logImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_logImageList.ImageStream")));
            this._logImageList.TransparentColor = System.Drawing.Color.Transparent;
            this._logImageList.Images.SetKeyName(0, "Trace.png");
            this._logImageList.Images.SetKeyName(1, "Info.png");
            this._logImageList.Images.SetKeyName(2, "Warning.png");
            this._logImageList.Images.SetKeyName(3, "Error.png");
            this._logImageList.Images.SetKeyName(4, "Fatal.png");
            // 
            // _refresh
            // 
            this._refresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._refresh.Location = new System.Drawing.Point(853, 3);
            this._refresh.Name = "_refresh";
            this._refresh.Size = new System.Drawing.Size(75, 23);
            this._refresh.TabIndex = 4;
            this._refresh.Text = "Refresh";
            this._toolTip.SetToolTip(this._refresh, "Restart Folder analysis.");
            this._refresh.UseVisualStyleBackColor = true;
            this._refresh.Click += new System.EventHandler(this._refresh_Click);
            // 
            // _validationSummary
            // 
            this._validationSummary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._validationSummary.Location = new System.Drawing.Point(3, 3);
            this._validationSummary.Multiline = true;
            this._validationSummary.Name = "_validationSummary";
            this._validationSummary.ReadOnly = true;
            this._validationSummary.Size = new System.Drawing.Size(722, 23);
            this._validationSummary.TabIndex = 5;
            this._validationSummary.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // _openFixes
            // 
            this._openFixes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._openFixes.Location = new System.Drawing.Point(731, 3);
            this._openFixes.Name = "_openFixes";
            this._openFixes.Size = new System.Drawing.Size(107, 23);
            this._openFixes.TabIndex = 6;
            this._openFixes.Text = "View {0} fixe(s)...";
            this._openFixes.UseVisualStyleBackColor = true;
            this._openFixes.Click += new System.EventHandler(this._openFixes_Click);
            // 
            // HomePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._openFixes);
            this.Controls.Add(this._validationSummary);
            this.Controls.Add(this._refresh);
            this.Controls.Add(this._logs);
            this.Name = "HomePage";
            this.Size = new System.Drawing.Size(931, 432);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView _logs;
        private System.Windows.Forms.ColumnHeader _logImage;
        private System.Windows.Forms.ImageList _logImageList;
        private System.Windows.Forms.Button _refresh;
        private System.Windows.Forms.ToolTip _toolTip;
        private System.Windows.Forms.TextBox _validationSummary;
        private System.Windows.Forms.Button _openFixes;
    }
}
