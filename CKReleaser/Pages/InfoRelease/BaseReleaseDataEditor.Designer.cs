namespace CK.Releaser.Info
{
    partial class BaseReleaseDataEditor
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
            this._saveData = new System.Windows.Forms.Button();
            this._buildMetaData = new System.Windows.Forms.TextBox();
            this._preRelease = new System.Windows.Forms.TextBox();
            this._infoNotesLabel = new System.Windows.Forms.Label();
            this._versionLabel = new System.Windows.Forms.Label();
            this._infoNotes = new System.Windows.Forms.TextBox();
            this._versionFromSource = new System.Windows.Forms.Label();
            this._separatorPreRelease = new System.Windows.Forms.Label();
            this._buildMetaDataLabel = new System.Windows.Forms.Label();
            this._preReleaseLabel = new System.Windows.Forms.Label();
            this._fromReleaseBox = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // _saveData
            // 
            this._saveData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._saveData.Location = new System.Drawing.Point(403, 21);
            this._saveData.Name = "_saveData";
            this._saveData.Size = new System.Drawing.Size(465, 27);
            this._saveData.TabIndex = 48;
            this._saveData.Text = "Save";
            this._saveData.UseVisualStyleBackColor = true;
            this._saveData.Click += new System.EventHandler(this._saveData_Click);
            // 
            // _buildMetaData
            // 
            this._buildMetaData.Location = new System.Drawing.Point(267, 16);
            this._buildMetaData.Name = "_buildMetaData";
            this._buildMetaData.Size = new System.Drawing.Size(119, 20);
            this._buildMetaData.TabIndex = 53;
            this._buildMetaData.TextChanged += new System.EventHandler(this._any_TextChanged);
            // 
            // _preRelease
            // 
            this._preRelease.Location = new System.Drawing.Point(138, 17);
            this._preRelease.Name = "_preRelease";
            this._preRelease.Size = new System.Drawing.Size(119, 20);
            this._preRelease.TabIndex = 50;
            this._preRelease.TextChanged += new System.EventHandler(this._any_TextChanged);
            // 
            // _infoNotesLabel
            // 
            this._infoNotesLabel.AutoSize = true;
            this._infoNotesLabel.Location = new System.Drawing.Point(-3, 72);
            this._infoNotesLabel.Name = "_infoNotesLabel";
            this._infoNotesLabel.Size = new System.Drawing.Size(38, 13);
            this._infoNotesLabel.TabIndex = 45;
            this._infoNotesLabel.Text = "Notes:";
            // 
            // _versionLabel
            // 
            this._versionLabel.AutoSize = true;
            this._versionLabel.Location = new System.Drawing.Point(-3, 21);
            this._versionLabel.Name = "_versionLabel";
            this._versionLabel.Size = new System.Drawing.Size(45, 13);
            this._versionLabel.TabIndex = 46;
            this._versionLabel.Text = "Version:";
            // 
            // _infoNotes
            // 
            this._infoNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._infoNotes.Location = new System.Drawing.Point(41, 69);
            this._infoNotes.Multiline = true;
            this._infoNotes.Name = "_infoNotes";
            this._infoNotes.Size = new System.Drawing.Size(827, 328);
            this._infoNotes.TabIndex = 47;
            this._infoNotes.TextChanged += new System.EventHandler(this._any_TextChanged);
            // 
            // _versionFromSource
            // 
            this._versionFromSource.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._versionFromSource.Location = new System.Drawing.Point(40, 17);
            this._versionFromSource.Name = "_versionFromSource";
            this._versionFromSource.Size = new System.Drawing.Size(90, 23);
            this._versionFromSource.TabIndex = 55;
            this._versionFromSource.Text = "14.154.2255";
            this._versionFromSource.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _separatorPreRelease
            // 
            this._separatorPreRelease.AutoSize = true;
            this._separatorPreRelease.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._separatorPreRelease.Location = new System.Drawing.Point(126, 21);
            this._separatorPreRelease.Name = "_separatorPreRelease";
            this._separatorPreRelease.Size = new System.Drawing.Size(11, 13);
            this._separatorPreRelease.TabIndex = 51;
            this._separatorPreRelease.Text = "-";
            // 
            // _buildMetaDataLabel
            // 
            this._buildMetaDataLabel.AutoSize = true;
            this._buildMetaDataLabel.Location = new System.Drawing.Point(287, 41);
            this._buildMetaDataLabel.Name = "_buildMetaDataLabel";
            this._buildMetaDataLabel.Size = new System.Drawing.Size(78, 13);
            this._buildMetaDataLabel.TabIndex = 52;
            this._buildMetaDataLabel.Text = "Build Metadata";
            // 
            // _preReleaseLabel
            // 
            this._preReleaseLabel.AutoSize = true;
            this._preReleaseLabel.Location = new System.Drawing.Point(161, 41);
            this._preReleaseLabel.Name = "_preReleaseLabel";
            this._preReleaseLabel.Size = new System.Drawing.Size(65, 13);
            this._preReleaseLabel.TabIndex = 49;
            this._preReleaseLabel.Text = "Pre Release";
            // 
            // _fromReleaseBox
            // 
            this._fromReleaseBox.Location = new System.Drawing.Point(131, -1);
            this._fromReleaseBox.Name = "_fromReleaseBox";
            this._fromReleaseBox.Size = new System.Drawing.Size(266, 62);
            this._fromReleaseBox.TabIndex = 54;
            this._fromReleaseBox.TabStop = false;
            this._fromReleaseBox.Text = "From Info Release";
            // 
            // BaseReleaseDataEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._saveData);
            this.Controls.Add(this._buildMetaData);
            this.Controls.Add(this._preRelease);
            this.Controls.Add(this._infoNotesLabel);
            this.Controls.Add(this._versionLabel);
            this.Controls.Add(this._infoNotes);
            this.Controls.Add(this._versionFromSource);
            this.Controls.Add(this._separatorPreRelease);
            this.Controls.Add(this._buildMetaDataLabel);
            this.Controls.Add(this._preReleaseLabel);
            this.Controls.Add(this._fromReleaseBox);
            this.Name = "BaseReleaseDataEditor";
            this.Size = new System.Drawing.Size(871, 399);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _saveData;
        private System.Windows.Forms.TextBox _buildMetaData;
        private System.Windows.Forms.TextBox _preRelease;
        private System.Windows.Forms.Label _infoNotesLabel;
        private System.Windows.Forms.Label _versionLabel;
        private System.Windows.Forms.TextBox _infoNotes;
        private System.Windows.Forms.Label _versionFromSource;
        private System.Windows.Forms.Label _separatorPreRelease;
        private System.Windows.Forms.Label _buildMetaDataLabel;
        private System.Windows.Forms.Label _preReleaseLabel;
        private System.Windows.Forms.GroupBox _fromReleaseBox;
    }
}
