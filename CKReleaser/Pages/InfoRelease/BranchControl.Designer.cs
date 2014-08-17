namespace CK.Releaser.Info
{
    partial class BranchControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BranchControl));
            this._createInfoRelease = new System.Windows.Forms.Button();
            this._folderPath = new System.Windows.Forms.LinkLabel();
            this._warningEditCurrent = new System.Windows.Forms.Label();
            this._imageList = new System.Windows.Forms.ImageList(this.components);
            this._releaseDataBox = new System.Windows.Forms.GroupBox();
            this._baseReleaseDataEditor = new CK.Releaser.Info.BaseReleaseDataEditor();
            this._launchReleasePanel = new CK.Releaser.Info.LaunchReleasePanel();
            this._launchReleaseBox = new System.Windows.Forms.GroupBox();
            this._releaseDataBox.SuspendLayout();
            this._launchReleaseBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // _createInfoRelease
            // 
            this._createInfoRelease.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._createInfoRelease.Location = new System.Drawing.Point(580, 3);
            this._createInfoRelease.Name = "_createInfoRelease";
            this._createInfoRelease.Size = new System.Drawing.Size(135, 30);
            this._createInfoRelease.TabIndex = 0;
            this._createInfoRelease.Text = "Create Info Release";
            this._createInfoRelease.UseVisualStyleBackColor = true;
            this._createInfoRelease.Click += new System.EventHandler(this._createInfoRelease_Click);
            // 
            // _folderPath
            // 
            this._folderPath.AutoSize = true;
            this._folderPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._folderPath.Location = new System.Drawing.Point(4, 3);
            this._folderPath.Name = "_folderPath";
            this._folderPath.Size = new System.Drawing.Size(48, 16);
            this._folderPath.TabIndex = 5;
            this._folderPath.TabStop = true;
            this._folderPath.Text = "<path>";
            this._folderPath.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._folderPath_LinkClicked);
            // 
            // _warningEditCurrent
            // 
            this._warningEditCurrent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._warningEditCurrent.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._warningEditCurrent.ImageIndex = 0;
            this._warningEditCurrent.ImageList = this._imageList;
            this._warningEditCurrent.Location = new System.Drawing.Point(7, 19);
            this._warningEditCurrent.Name = "_warningEditCurrent";
            this._warningEditCurrent.Size = new System.Drawing.Size(460, 29);
            this._warningEditCurrent.TabIndex = 8;
            this._warningEditCurrent.Text = "This information does not apply to the current commit since a dedicated info exis" +
    "ts for it.";
            this._warningEditCurrent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _imageList
            // 
            this._imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_imageList.ImageStream")));
            this._imageList.TransparentColor = System.Drawing.Color.Transparent;
            this._imageList.Images.SetKeyName(0, "Warning.png");
            // 
            // _releaseDataBox
            // 
            this._releaseDataBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._releaseDataBox.Controls.Add(this._baseReleaseDataEditor);
            this._releaseDataBox.Location = new System.Drawing.Point(9, 51);
            this._releaseDataBox.Name = "_releaseDataBox";
            this._releaseDataBox.Size = new System.Drawing.Size(708, 230);
            this._releaseDataBox.TabIndex = 45;
            this._releaseDataBox.TabStop = false;
            this._releaseDataBox.Text = "Release Data (default for this branch)";
            // 
            // _baseReleaseDataEditor
            // 
            this._baseReleaseDataEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._baseReleaseDataEditor.Location = new System.Drawing.Point(6, 19);
            this._baseReleaseDataEditor.Name = "_baseReleaseDataEditor";
            this._baseReleaseDataEditor.Size = new System.Drawing.Size(696, 205);
            this._baseReleaseDataEditor.TabIndex = 0;
            // 
            // _launchReleasePanel
            // 
            this._launchReleasePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._launchReleasePanel.Location = new System.Drawing.Point(4, 15);
            this._launchReleasePanel.Name = "_launchReleasePanel";
            this._launchReleasePanel.Size = new System.Drawing.Size(698, 37);
            this._launchReleasePanel.TabIndex = 46;
            // 
            // _launchReleaseBox
            // 
            this._launchReleaseBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._launchReleaseBox.Controls.Add(this._launchReleasePanel);
            this._launchReleaseBox.Location = new System.Drawing.Point(9, 287);
            this._launchReleaseBox.Name = "_launchReleaseBox";
            this._launchReleaseBox.Size = new System.Drawing.Size(706, 63);
            this._launchReleaseBox.TabIndex = 47;
            this._launchReleaseBox.TabStop = false;
            this._launchReleaseBox.Text = "Release";
            // 
            // BranchControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._launchReleaseBox);
            this.Controls.Add(this._warningEditCurrent);
            this.Controls.Add(this._folderPath);
            this.Controls.Add(this._createInfoRelease);
            this.Controls.Add(this._releaseDataBox);
            this.Name = "BranchControl";
            this.Size = new System.Drawing.Size(720, 354);
            this._releaseDataBox.ResumeLayout(false);
            this._launchReleaseBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _createInfoRelease;
        private System.Windows.Forms.LinkLabel _folderPath;
        private System.Windows.Forms.Label _warningEditCurrent;
        private System.Windows.Forms.ImageList _imageList;
        private System.Windows.Forms.GroupBox _releaseDataBox;
        private BaseReleaseDataEditor _baseReleaseDataEditor;
        private LaunchReleasePanel _launchReleasePanel;
        private System.Windows.Forms.GroupBox _launchReleaseBox;
    }
}
