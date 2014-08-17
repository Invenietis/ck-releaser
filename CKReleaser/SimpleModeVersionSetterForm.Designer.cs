namespace CK.Releaser
{
    partial class SimpleModeVersionSetterForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SimpleModeVersionSetterForm));
            this._versionEditorPanel = new CK.Releaser.GUI.VersionEditorPanel();
            this._cancelButton = new System.Windows.Forms.Button();
            this._okButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _versionEditorPanel
            // 
            this._versionEditorPanel.BuildMetaData = "";
            this._versionEditorPanel.PreReleaseEnabled = true;
            this._versionEditorPanel.BuildMetaDataEnabled = true;
            this._versionEditorPanel.FromSourceEnabled = true;
            this._versionEditorPanel.Location = new System.Drawing.Point(13, 13);
            this._versionEditorPanel.Name = "_versionEditorPanel";
            this._versionEditorPanel.PreRelease = "";
            this._versionEditorPanel.Size = new System.Drawing.Size(491, 79);
            this._versionEditorPanel.TabIndex = 0;
            this._versionEditorPanel.Version = ((System.Version)(resources.GetObject("_versionEditorPanel.Version")));
            // 
            // _cancelButton
            // 
            this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cancelButton.Location = new System.Drawing.Point(423, 98);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(75, 23);
            this._cancelButton.TabIndex = 1;
            this._cancelButton.Text = "Cancel";
            this._cancelButton.UseVisualStyleBackColor = true;
            this._cancelButton.Click += new System.EventHandler(this._cancelButton_Click);
            // 
            // _okButton
            // 
            this._okButton.Location = new System.Drawing.Point(333, 98);
            this._okButton.Name = "_okButton";
            this._okButton.Size = new System.Drawing.Size(75, 23);
            this._okButton.TabIndex = 1;
            this._okButton.Text = "OK";
            this._okButton.UseVisualStyleBackColor = true;
            this._okButton.Click += new System.EventHandler(this._okButton_Click);
            // 
            // SimpleModeVersionSetterForm
            // 
            this.AcceptButton = this._okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._cancelButton;
            this.ClientSize = new System.Drawing.Size(510, 132);
            this.Controls.Add(this._okButton);
            this.Controls.Add(this._cancelButton);
            this.Controls.Add(this._versionEditorPanel);
            this.Name = "SimpleModeVersionSetterForm";
            this.Text = "Sets the Version (Simple Versioning mode)";
            this.ResumeLayout(false);

        }

        #endregion

        private GUI.VersionEditorPanel _versionEditorPanel;
        private System.Windows.Forms.Button _cancelButton;
        private System.Windows.Forms.Button _okButton;

    }
}