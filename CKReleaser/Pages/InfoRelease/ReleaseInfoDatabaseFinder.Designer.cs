namespace CK.Releaser.Info
{
    partial class InfoReleaseDatabaseFinder
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
            this._availableList = new System.Windows.Forms.ListView();
            this._availableNameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._availablePathColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._availableGUIDColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._availableHasGitColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._availableBox = new System.Windows.Forms.GroupBox();
            this._cancel = new System.Windows.Forms.Button();
            this._createBox = new System.Windows.Forms.GroupBox();
            this._resultPathLabel = new System.Windows.Forms.Label();
            this._createButton = new System.Windows.Forms.Button();
            this._nameCreate = new System.Windows.Forms.TextBox();
            this._nameCreateLabel = new System.Windows.Forms.Label();
            this._choosePathCreate = new System.Windows.Forms.Button();
            this._pathCreate = new System.Windows.Forms.TextBox();
            this._pathCreateLabel = new System.Windows.Forms.Label();
            this._okButton = new System.Windows.Forms.Button();
            this._availableBox.SuspendLayout();
            this._createBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // _availableList
            // 
            this._availableList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._availableList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this._availableNameColumn,
            this._availablePathColumn,
            this._availableGUIDColumn,
            this._availableHasGitColumn});
            this._availableList.FullRowSelect = true;
            this._availableList.HideSelection = false;
            this._availableList.Location = new System.Drawing.Point(6, 19);
            this._availableList.MultiSelect = false;
            this._availableList.Name = "_availableList";
            this._availableList.Size = new System.Drawing.Size(668, 236);
            this._availableList.TabIndex = 0;
            this._availableList.UseCompatibleStateImageBehavior = false;
            this._availableList.View = System.Windows.Forms.View.Details;
            this._availableList.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this._availableList_ItemSelectionChanged);
            // 
            // _availableNameColumn
            // 
            this._availableNameColumn.Text = "Name";
            this._availableNameColumn.Width = 116;
            // 
            // _availablePathColumn
            // 
            this._availablePathColumn.Text = "Path";
            this._availablePathColumn.Width = 275;
            // 
            // _availableGUIDColumn
            // 
            this._availableGUIDColumn.Text = "Identifier";
            this._availableGUIDColumn.Width = 125;
            // 
            // _availableHasGitColumn
            // 
            this._availableHasGitColumn.Text = "Is Git folder";
            this._availableHasGitColumn.Width = 83;
            // 
            // _availableBox
            // 
            this._availableBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._availableBox.Controls.Add(this._availableList);
            this._availableBox.Location = new System.Drawing.Point(3, 12);
            this._availableBox.Name = "_availableBox";
            this._availableBox.Size = new System.Drawing.Size(680, 261);
            this._availableBox.TabIndex = 1;
            this._availableBox.TabStop = false;
            this._availableBox.Text = "Available Databases";
            // 
            // _cancel
            // 
            this._cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._cancel.Location = new System.Drawing.Point(608, 378);
            this._cancel.Name = "_cancel";
            this._cancel.Size = new System.Drawing.Size(75, 23);
            this._cancel.TabIndex = 2;
            this._cancel.Text = "Cancel";
            this._cancel.UseVisualStyleBackColor = true;
            this._cancel.Click += new System.EventHandler(this._cancel_Click);
            // 
            // _createBox
            // 
            this._createBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._createBox.Controls.Add(this._resultPathLabel);
            this._createBox.Controls.Add(this._createButton);
            this._createBox.Controls.Add(this._nameCreate);
            this._createBox.Controls.Add(this._nameCreateLabel);
            this._createBox.Controls.Add(this._choosePathCreate);
            this._createBox.Controls.Add(this._pathCreate);
            this._createBox.Controls.Add(this._pathCreateLabel);
            this._createBox.Location = new System.Drawing.Point(3, 279);
            this._createBox.Name = "_createBox";
            this._createBox.Size = new System.Drawing.Size(680, 83);
            this._createBox.TabIndex = 3;
            this._createBox.TabStop = false;
            this._createBox.Text = "Create a new one";
            // 
            // _resultPathLabel
            // 
            this._resultPathLabel.AutoSize = true;
            this._resultPathLabel.Location = new System.Drawing.Point(6, 58);
            this._resultPathLabel.Name = "_resultPathLabel";
            this._resultPathLabel.Size = new System.Drawing.Size(145, 13);
            this._resultPathLabel.TabIndex = 6;
            this._resultPathLabel.Text = "Folder \'{0}{1}\' will be created.";
            // 
            // _createButton
            // 
            this._createButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._createButton.Location = new System.Drawing.Point(575, 53);
            this._createButton.Name = "_createButton";
            this._createButton.Size = new System.Drawing.Size(99, 23);
            this._createButton.TabIndex = 5;
            this._createButton.Text = "Create";
            this._createButton.UseVisualStyleBackColor = true;
            this._createButton.Click += new System.EventHandler(this._createButton_Click);
            // 
            // _nameCreate
            // 
            this._nameCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._nameCreate.Location = new System.Drawing.Point(575, 20);
            this._nameCreate.Name = "_nameCreate";
            this._nameCreate.Size = new System.Drawing.Size(99, 20);
            this._nameCreate.TabIndex = 4;
            this._nameCreate.Text = "<name>";
            this._nameCreate.TextChanged += new System.EventHandler(this._nameOrPathCreate_TextChanged);
            // 
            // _nameCreateLabel
            // 
            this._nameCreateLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._nameCreateLabel.AutoSize = true;
            this._nameCreateLabel.Location = new System.Drawing.Point(540, 23);
            this._nameCreateLabel.Name = "_nameCreateLabel";
            this._nameCreateLabel.Size = new System.Drawing.Size(35, 13);
            this._nameCreateLabel.TabIndex = 3;
            this._nameCreateLabel.Text = "Name";
            // 
            // _choosePathCreate
            // 
            this._choosePathCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._choosePathCreate.Location = new System.Drawing.Point(503, 19);
            this._choosePathCreate.Name = "_choosePathCreate";
            this._choosePathCreate.Size = new System.Drawing.Size(29, 21);
            this._choosePathCreate.TabIndex = 2;
            this._choosePathCreate.Text = "...";
            this._choosePathCreate.UseVisualStyleBackColor = true;
            this._choosePathCreate.Click += new System.EventHandler(this._choosePathCreate_Click);
            // 
            // _pathCreate
            // 
            this._pathCreate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._pathCreate.Location = new System.Drawing.Point(36, 20);
            this._pathCreate.Name = "_pathCreate";
            this._pathCreate.ReadOnly = true;
            this._pathCreate.Size = new System.Drawing.Size(461, 20);
            this._pathCreate.TabIndex = 1;
            this._pathCreate.TextChanged += new System.EventHandler(this._nameOrPathCreate_TextChanged);
            // 
            // _pathCreateLabel
            // 
            this._pathCreateLabel.AutoSize = true;
            this._pathCreateLabel.Location = new System.Drawing.Point(6, 23);
            this._pathCreateLabel.Name = "_pathCreateLabel";
            this._pathCreateLabel.Size = new System.Drawing.Size(29, 13);
            this._pathCreateLabel.TabIndex = 0;
            this._pathCreateLabel.Text = "Path";
            // 
            // _okButton
            // 
            this._okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._okButton.Location = new System.Drawing.Point(518, 378);
            this._okButton.Name = "_okButton";
            this._okButton.Size = new System.Drawing.Size(75, 23);
            this._okButton.TabIndex = 2;
            this._okButton.Text = "OK";
            this._okButton.UseVisualStyleBackColor = true;
            this._okButton.Click += new System.EventHandler(this._okButton_Click);
            // 
            // InfoReleaseDatabaseFinder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 406);
            this.Controls.Add(this._createBox);
            this.Controls.Add(this._okButton);
            this.Controls.Add(this._cancel);
            this.Controls.Add(this._availableBox);
            this.Name = "InfoReleaseDatabaseFinder";
            this.Text = "Finds or creates a InfoRelease database";
            this._availableBox.ResumeLayout(false);
            this._createBox.ResumeLayout(false);
            this._createBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView _availableList;
        private System.Windows.Forms.ColumnHeader _availableNameColumn;
        private System.Windows.Forms.ColumnHeader _availablePathColumn;
        private System.Windows.Forms.ColumnHeader _availableGUIDColumn;
        private System.Windows.Forms.GroupBox _availableBox;
        private System.Windows.Forms.Button _cancel;
        private System.Windows.Forms.GroupBox _createBox;
        private System.Windows.Forms.Label _resultPathLabel;
        private System.Windows.Forms.Button _createButton;
        private System.Windows.Forms.TextBox _nameCreate;
        private System.Windows.Forms.Label _nameCreateLabel;
        private System.Windows.Forms.Button _choosePathCreate;
        private System.Windows.Forms.TextBox _pathCreate;
        private System.Windows.Forms.Label _pathCreateLabel;
        private System.Windows.Forms.ColumnHeader _availableHasGitColumn;
        private System.Windows.Forms.Button _okButton;
    }
}