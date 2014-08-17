namespace CK.Releaser.Info
{
    partial class LaunchReleasePanel
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
            this._copyToRelease = new System.Windows.Forms.Button();
            this._linkReleaseFolder = new System.Windows.Forms.LinkLabel();
            this._linkLabelStart = new System.Windows.Forms.Label();
            this._linkLabelEnd = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _copyToRelease
            // 
            this._copyToRelease.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._copyToRelease.Location = new System.Drawing.Point(490, 3);
            this._copyToRelease.Name = "_copyToRelease";
            this._copyToRelease.Size = new System.Drawing.Size(116, 23);
            this._copyToRelease.TabIndex = 0;
            this._copyToRelease.Text = "Copy to \\Release...";
            this._copyToRelease.UseVisualStyleBackColor = true;
            this._copyToRelease.Click += new System.EventHandler(this._copyToRelease_Click);
            // 
            // _linkReleaseFolder
            // 
            this._linkReleaseFolder.AutoSize = true;
            this._linkReleaseFolder.Location = new System.Drawing.Point(157, 8);
            this._linkReleaseFolder.Name = "_linkReleaseFolder";
            this._linkReleaseFolder.Size = new System.Drawing.Size(51, 13);
            this._linkReleaseFolder.TabIndex = 1;
            this._linkReleaseFolder.TabStop = true;
            this._linkReleaseFolder.Text = "\\Release";
            this._linkReleaseFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._linkReleaseFolder_LinkClicked);
            // 
            // _linkLabelStart
            // 
            this._linkLabelStart.AutoSize = true;
            this._linkLabelStart.Location = new System.Drawing.Point(3, 8);
            this._linkLabelStart.Name = "_linkLabelStart";
            this._linkLabelStart.Size = new System.Drawing.Size(160, 13);
            this._linkLabelStart.TabIndex = 2;
            this._linkLabelStart.Text = "The release files are copied into ";
            // 
            // _linkLabelEnd
            // 
            this._linkLabelEnd.AutoSize = true;
            this._linkLabelEnd.Location = new System.Drawing.Point(205, 8);
            this._linkLabelEnd.Name = "_linkLabelEnd";
            this._linkLabelEnd.Size = new System.Drawing.Size(36, 13);
            this._linkLabelEnd.TabIndex = 2;
            this._linkLabelEnd.Text = "folder.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Any existing content will be deleted.";
            // 
            // LaunchReleasePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this._linkReleaseFolder);
            this.Controls.Add(this._linkLabelEnd);
            this.Controls.Add(this._linkLabelStart);
            this.Controls.Add(this._copyToRelease);
            this.Name = "LaunchReleasePanel";
            this.Size = new System.Drawing.Size(609, 45);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _copyToRelease;
        private System.Windows.Forms.LinkLabel _linkReleaseFolder;
        private System.Windows.Forms.Label _linkLabelStart;
        private System.Windows.Forms.Label _linkLabelEnd;
        private System.Windows.Forms.Label label1;
    }
}
