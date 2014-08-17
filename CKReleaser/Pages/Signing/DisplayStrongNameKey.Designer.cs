namespace CK.Releaser
{
    partial class DisplayStrongNameKey
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
            this.label1 = new System.Windows.Forms.Label();
            this._publicKeyToken = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this._publicKey = new System.Windows.Forms.TextBox();
            this._knownText = new System.Windows.Forms.Label();
            this._closeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Public Key Token";
            // 
            // _publicKeyToken
            // 
            this._publicKeyToken.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._publicKeyToken.Location = new System.Drawing.Point(111, 13);
            this._publicKeyToken.Name = "_publicKeyToken";
            this._publicKeyToken.ReadOnly = true;
            this._publicKeyToken.Size = new System.Drawing.Size(442, 20);
            this._publicKeyToken.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Public Key";
            // 
            // _publicKey
            // 
            this._publicKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._publicKey.Location = new System.Drawing.Point(111, 39);
            this._publicKey.Multiline = true;
            this._publicKey.Name = "_publicKey";
            this._publicKey.ReadOnly = true;
            this._publicKey.Size = new System.Drawing.Size(442, 114);
            this._publicKey.TabIndex = 1;
            // 
            // _knownText
            // 
            this._knownText.AutoSize = true;
            this._knownText.Location = new System.Drawing.Point(108, 171);
            this._knownText.Name = "_knownText";
            this._knownText.Size = new System.Drawing.Size(223, 13);
            this._knownText.TabIndex = 2;
            this._knownText.Text = "This is not the SharedKey nor the Official one.";
            // 
            // _closeButton
            // 
            this._closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._closeButton.Location = new System.Drawing.Point(478, 166);
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(75, 23);
            this._closeButton.TabIndex = 3;
            this._closeButton.Text = "Close";
            this._closeButton.UseVisualStyleBackColor = true;
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // DisplayStrongNameKey
            // 
            this.AcceptButton = this._closeButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._closeButton;
            this.ClientSize = new System.Drawing.Size(565, 197);
            this.Controls.Add(this._closeButton);
            this.Controls.Add(this._knownText);
            this.Controls.Add(this._publicKey);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._publicKeyToken);
            this.Controls.Add(this.label1);
            this.Name = "DisplayStrongNameKey";
            this.Text = "Public Signatures";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _publicKeyToken;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox _publicKey;
        private System.Windows.Forms.Label _knownText;
        private System.Windows.Forms.Button _closeButton;
    }
}