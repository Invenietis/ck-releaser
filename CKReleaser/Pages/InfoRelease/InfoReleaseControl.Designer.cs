namespace CK.Releaser.Info
{
    partial class InfoReleaseControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InfoReleaseControl));
            this._folderPath = new System.Windows.Forms.LinkLabel();
            this._imageList = new System.Windows.Forms.ImageList(this.components);
            this._releaseDataBox = new System.Windows.Forms.GroupBox();
            this._baseReleaseDataEditor = new CK.Releaser.Info.BaseReleaseDataEditor();
            this._launchReleasePanel = new CK.Releaser.Info.LaunchReleasePanel();
            this._launchReleaseBox = new System.Windows.Forms.GroupBox();
            this._releaseDataBox.SuspendLayout();
            this._launchReleaseBox.SuspendLayout();
            this.SuspendLayout();
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
            this._releaseDataBox.Location = new System.Drawing.Point(7, 32);
            this._releaseDataBox.Name = "_releaseDataBox";
            this._releaseDataBox.Size = new System.Drawing.Size(753, 205);
            this._releaseDataBox.TabIndex = 46;
            this._releaseDataBox.TabStop = false;
            this._releaseDataBox.Text = "Release Data";
            // 
            // _baseReleaseDataEditor
            // 
            this._baseReleaseDataEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._baseReleaseDataEditor.Location = new System.Drawing.Point(6, 19);
            this._baseReleaseDataEditor.Name = "_baseReleaseDataEditor";
            this._baseReleaseDataEditor.Size = new System.Drawing.Size(741, 180);
            this._baseReleaseDataEditor.TabIndex = 0;
            // 
            // _launchReleasePanel
            // 
            this._launchReleasePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._launchReleasePanel.Location = new System.Drawing.Point(4, 15);
            this._launchReleasePanel.Name = "_launchReleasePanel";
            this._launchReleasePanel.Size = new System.Drawing.Size(745, 37);
            this._launchReleasePanel.TabIndex = 46;
            // 
            // _launchReleaseBox
            // 
            this._launchReleaseBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._launchReleaseBox.Controls.Add(this._launchReleasePanel);
            this._launchReleaseBox.Location = new System.Drawing.Point(7, 243);
            this._launchReleaseBox.Name = "_launchReleaseBox";
            this._launchReleaseBox.Size = new System.Drawing.Size(753, 63);
            this._launchReleaseBox.TabIndex = 48;
            this._launchReleaseBox.TabStop = false;
            this._launchReleaseBox.Text = "Release";
            // 
            // InfoReleaseControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._launchReleaseBox);
            this.Controls.Add(this._releaseDataBox);
            this.Controls.Add(this._folderPath);
            this.Name = "InfoReleaseControl";
            this.Size = new System.Drawing.Size(763, 309);
            this._releaseDataBox.ResumeLayout(false);
            this._launchReleaseBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel _folderPath;
        private System.Windows.Forms.ImageList _imageList;
        private System.Windows.Forms.GroupBox _releaseDataBox;
        private BaseReleaseDataEditor _baseReleaseDataEditor;
        private LaunchReleasePanel _launchReleasePanel;
        private System.Windows.Forms.GroupBox _launchReleaseBox;
    }
}
