namespace CK.Releaser
{
    partial class MainVersionSetterForm
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
            this._cancelButton = new System.Windows.Forms.Button();
            this._okButton = new System.Windows.Forms.Button();
            this._patchLabel = new System.Windows.Forms.Label();
            this._minorLabel = new System.Windows.Forms.Label();
            this._majorLabel = new System.Windows.Forms.Label();
            this._patch = new System.Windows.Forms.NumericUpDown();
            this._minor = new System.Windows.Forms.NumericUpDown();
            this._major = new System.Windows.Forms.NumericUpDown();
            this._newVersionLabel = new System.Windows.Forms.Label();
            this._currentInfoLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this._patch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._minor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._major)).BeginInit();
            this.SuspendLayout();
            // 
            // _cancelButton
            // 
            this._cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cancelButton.Location = new System.Drawing.Point(395, 150);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(75, 23);
            this._cancelButton.TabIndex = 1;
            this._cancelButton.Text = "Cancel";
            this._cancelButton.UseVisualStyleBackColor = true;
            this._cancelButton.Click += new System.EventHandler(this._cancelButton_Click);
            // 
            // _okButton
            // 
            this._okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._okButton.Location = new System.Drawing.Point(305, 150);
            this._okButton.Name = "_okButton";
            this._okButton.Size = new System.Drawing.Size(75, 23);
            this._okButton.TabIndex = 1;
            this._okButton.Text = "OK";
            this._okButton.UseVisualStyleBackColor = true;
            this._okButton.Click += new System.EventHandler(this._okButton_Click);
            // 
            // _patchLabel
            // 
            this._patchLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._patchLabel.AutoSize = true;
            this._patchLabel.Location = new System.Drawing.Point(235, 67);
            this._patchLabel.Name = "_patchLabel";
            this._patchLabel.Size = new System.Drawing.Size(35, 13);
            this._patchLabel.TabIndex = 19;
            this._patchLabel.Text = "Patch";
            // 
            // _minorLabel
            // 
            this._minorLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._minorLabel.AutoSize = true;
            this._minorLabel.Location = new System.Drawing.Point(166, 67);
            this._minorLabel.Name = "_minorLabel";
            this._minorLabel.Size = new System.Drawing.Size(33, 13);
            this._minorLabel.TabIndex = 20;
            this._minorLabel.Text = "Minor";
            // 
            // _majorLabel
            // 
            this._majorLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._majorLabel.AutoSize = true;
            this._majorLabel.Location = new System.Drawing.Point(94, 67);
            this._majorLabel.Name = "_majorLabel";
            this._majorLabel.Size = new System.Drawing.Size(33, 13);
            this._majorLabel.TabIndex = 21;
            this._majorLabel.Text = "Major";
            // 
            // _patch
            // 
            this._patch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._patch.Location = new System.Drawing.Point(224, 44);
            this._patch.Name = "_patch";
            this._patch.Size = new System.Drawing.Size(64, 20);
            this._patch.TabIndex = 16;
            this._patch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // _minor
            // 
            this._minor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._minor.Location = new System.Drawing.Point(154, 44);
            this._minor.Name = "_minor";
            this._minor.Size = new System.Drawing.Size(64, 20);
            this._minor.TabIndex = 17;
            this._minor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // _major
            // 
            this._major.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._major.Location = new System.Drawing.Point(84, 45);
            this._major.Name = "_major";
            this._major.Size = new System.Drawing.Size(64, 20);
            this._major.TabIndex = 18;
            this._major.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // _newVersionLabel
            // 
            this._newVersionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._newVersionLabel.AutoSize = true;
            this._newVersionLabel.Location = new System.Drawing.Point(8, 47);
            this._newVersionLabel.Name = "_newVersionLabel";
            this._newVersionLabel.Size = new System.Drawing.Size(70, 13);
            this._newVersionLabel.TabIndex = 22;
            this._newVersionLabel.Text = "New Version:";
            // 
            // _currentInfoLabel
            // 
            this._currentInfoLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._currentInfoLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this._currentInfoLabel.Location = new System.Drawing.Point(6, 9);
            this._currentInfoLabel.Name = "_currentInfoLabel";
            this._currentInfoLabel.Size = new System.Drawing.Size(468, 32);
            this._currentInfoLabel.TabIndex = 23;
            this._currentInfoLabel.Text = "<status>";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(81, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(325, 52);
            this.label1.TabIndex = 24;
            this.label1.Text = "You should increment the:\r\n- MAJOR version for incompatible API changes (breaking" +
    " changes),\r\n- MINOR version for new functionalities without breaking changes,\r\n-" +
    " PATCH version for bug fixes.\r\n";
            // 
            // MainVersionSetterForm
            // 
            this.AcceptButton = this._okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._cancelButton;
            this.ClientSize = new System.Drawing.Size(480, 183);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._currentInfoLabel);
            this.Controls.Add(this._newVersionLabel);
            this.Controls.Add(this._patchLabel);
            this.Controls.Add(this._minorLabel);
            this.Controls.Add(this._majorLabel);
            this.Controls.Add(this._patch);
            this.Controls.Add(this._minor);
            this.Controls.Add(this._major);
            this.Controls.Add(this._okButton);
            this.Controls.Add(this._cancelButton);
            this.Name = "MainVersionSetterForm";
            this.Text = "Sets the main Version";
            ((System.ComponentModel.ISupportInitialize)(this._patch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._minor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._major)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _cancelButton;
        private System.Windows.Forms.Button _okButton;
        private System.Windows.Forms.Label _patchLabel;
        private System.Windows.Forms.Label _minorLabel;
        private System.Windows.Forms.Label _majorLabel;
        private System.Windows.Forms.NumericUpDown _patch;
        private System.Windows.Forms.NumericUpDown _minor;
        private System.Windows.Forms.NumericUpDown _major;
        private System.Windows.Forms.Label _newVersionLabel;
        private System.Windows.Forms.Label _currentInfoLabel;
        private System.Windows.Forms.Label label1;

    }
}