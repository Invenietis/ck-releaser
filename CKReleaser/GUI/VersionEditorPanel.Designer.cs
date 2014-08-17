namespace CK.Releaser.GUI
{
    partial class VersionEditorPanel
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
            this._buildMetaData = new System.Windows.Forms.TextBox();
            this._buildMetaDataLabel = new System.Windows.Forms.Label();
            this._preRelease = new System.Windows.Forms.TextBox();
            this._preReleaseLabel = new System.Windows.Forms.Label();
            this._patchLabel = new System.Windows.Forms.Label();
            this._minorLabel = new System.Windows.Forms.Label();
            this._majorLabel = new System.Windows.Forms.Label();
            this._patch = new System.Windows.Forms.NumericUpDown();
            this._minor = new System.Windows.Forms.NumericUpDown();
            this._major = new System.Windows.Forms.NumericUpDown();
            this._separatorPreRelease = new System.Windows.Forms.Label();
            this._separatorBuildMetaData = new System.Windows.Forms.Label();
            this._fromReleaseBox = new System.Windows.Forms.GroupBox();
            this._sourceBox = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this._patch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._minor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._major)).BeginInit();
            this.SuspendLayout();
            // 
            // _buildMetaData
            // 
            this._buildMetaData.Location = new System.Drawing.Point(362, 27);
            this._buildMetaData.Name = "_buildMetaData";
            this._buildMetaData.Size = new System.Drawing.Size(119, 20);
            this._buildMetaData.TabIndex = 19;
            // 
            // _buildMetaDataLabel
            // 
            this._buildMetaDataLabel.AutoSize = true;
            this._buildMetaDataLabel.Location = new System.Drawing.Point(382, 52);
            this._buildMetaDataLabel.Name = "_buildMetaDataLabel";
            this._buildMetaDataLabel.Size = new System.Drawing.Size(78, 13);
            this._buildMetaDataLabel.TabIndex = 18;
            this._buildMetaDataLabel.Text = "Build Metadata";
            // 
            // _preRelease
            // 
            this._preRelease.Location = new System.Drawing.Point(233, 28);
            this._preRelease.Name = "_preRelease";
            this._preRelease.Size = new System.Drawing.Size(119, 20);
            this._preRelease.TabIndex = 16;
            // 
            // _preReleaseLabel
            // 
            this._preReleaseLabel.AutoSize = true;
            this._preReleaseLabel.Location = new System.Drawing.Point(256, 52);
            this._preReleaseLabel.Name = "_preReleaseLabel";
            this._preReleaseLabel.Size = new System.Drawing.Size(65, 13);
            this._preReleaseLabel.TabIndex = 12;
            this._preReleaseLabel.Text = "Pre Release";
            // 
            // _patchLabel
            // 
            this._patchLabel.AutoSize = true;
            this._patchLabel.Location = new System.Drawing.Point(162, 52);
            this._patchLabel.Name = "_patchLabel";
            this._patchLabel.Size = new System.Drawing.Size(35, 13);
            this._patchLabel.TabIndex = 13;
            this._patchLabel.Text = "Patch";
            // 
            // _minorLabel
            // 
            this._minorLabel.AutoSize = true;
            this._minorLabel.Location = new System.Drawing.Point(93, 52);
            this._minorLabel.Name = "_minorLabel";
            this._minorLabel.Size = new System.Drawing.Size(33, 13);
            this._minorLabel.TabIndex = 14;
            this._minorLabel.Text = "Minor";
            // 
            // _majorLabel
            // 
            this._majorLabel.AutoSize = true;
            this._majorLabel.Location = new System.Drawing.Point(21, 52);
            this._majorLabel.Name = "_majorLabel";
            this._majorLabel.Size = new System.Drawing.Size(33, 13);
            this._majorLabel.TabIndex = 15;
            this._majorLabel.Text = "Major";
            // 
            // _patch
            // 
            this._patch.Location = new System.Drawing.Point(151, 29);
            this._patch.Name = "_patch";
            this._patch.Size = new System.Drawing.Size(64, 20);
            this._patch.TabIndex = 9;
            this._patch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // _minor
            // 
            this._minor.Location = new System.Drawing.Point(81, 29);
            this._minor.Name = "_minor";
            this._minor.Size = new System.Drawing.Size(64, 20);
            this._minor.TabIndex = 10;
            this._minor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // _major
            // 
            this._major.Location = new System.Drawing.Point(11, 30);
            this._major.Name = "_major";
            this._major.Size = new System.Drawing.Size(64, 20);
            this._major.TabIndex = 11;
            this._major.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // _separatorPreRelease
            // 
            this._separatorPreRelease.AutoSize = true;
            this._separatorPreRelease.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._separatorPreRelease.Location = new System.Drawing.Point(219, 32);
            this._separatorPreRelease.Name = "_separatorPreRelease";
            this._separatorPreRelease.Size = new System.Drawing.Size(11, 13);
            this._separatorPreRelease.TabIndex = 17;
            this._separatorPreRelease.Text = "-";
            // 
            // _separatorBuildMetaData
            // 
            this._separatorBuildMetaData.AutoSize = true;
            this._separatorBuildMetaData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._separatorBuildMetaData.Location = new System.Drawing.Point(350, 31);
            this._separatorBuildMetaData.Name = "_separatorBuildMetaData";
            this._separatorBuildMetaData.Size = new System.Drawing.Size(14, 13);
            this._separatorBuildMetaData.TabIndex = 20;
            this._separatorBuildMetaData.Text = "+";
            // 
            // _fromReleaseBox
            // 
            this._fromReleaseBox.Location = new System.Drawing.Point(226, 3);
            this._fromReleaseBox.Name = "_fromReleaseBox";
            this._fromReleaseBox.Size = new System.Drawing.Size(262, 72);
            this._fromReleaseBox.TabIndex = 21;
            this._fromReleaseBox.TabStop = false;
            this._fromReleaseBox.Text = "From Info Release";
            // 
            // _sourceBox
            // 
            this._sourceBox.Location = new System.Drawing.Point(3, 3);
            this._sourceBox.Name = "_sourceBox";
            this._sourceBox.Size = new System.Drawing.Size(219, 72);
            this._sourceBox.TabIndex = 22;
            this._sourceBox.TabStop = false;
            this._sourceBox.Text = "From Source";
            // 
            // VersionEditorPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._buildMetaData);
            this.Controls.Add(this._buildMetaDataLabel);
            this.Controls.Add(this._preRelease);
            this.Controls.Add(this._preReleaseLabel);
            this.Controls.Add(this._patchLabel);
            this.Controls.Add(this._minorLabel);
            this.Controls.Add(this._majorLabel);
            this.Controls.Add(this._patch);
            this.Controls.Add(this._minor);
            this.Controls.Add(this._major);
            this.Controls.Add(this._separatorPreRelease);
            this.Controls.Add(this._separatorBuildMetaData);
            this.Controls.Add(this._fromReleaseBox);
            this.Controls.Add(this._sourceBox);
            this.Name = "VersionEditorPanel";
            this.Size = new System.Drawing.Size(491, 79);
            ((System.ComponentModel.ISupportInitialize)(this._patch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._minor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._major)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _buildMetaData;
        private System.Windows.Forms.Label _buildMetaDataLabel;
        private System.Windows.Forms.TextBox _preRelease;
        private System.Windows.Forms.Label _preReleaseLabel;
        private System.Windows.Forms.Label _patchLabel;
        private System.Windows.Forms.Label _minorLabel;
        private System.Windows.Forms.Label _majorLabel;
        private System.Windows.Forms.NumericUpDown _patch;
        private System.Windows.Forms.NumericUpDown _minor;
        private System.Windows.Forms.NumericUpDown _major;
        private System.Windows.Forms.Label _separatorPreRelease;
        private System.Windows.Forms.Label _separatorBuildMetaData;
        private System.Windows.Forms.GroupBox _fromReleaseBox;
        private System.Windows.Forms.GroupBox _sourceBox;
    }
}
