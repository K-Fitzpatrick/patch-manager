namespace patch_applier_gui
{
    partial class PatchManagerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.bOpenFileDialog = new System.Windows.Forms.Button();
            this.bPatch = new System.Windows.Forms.Button();
            this.folderBrowserDialogPatchFile = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialogPatchFile = new System.Windows.Forms.OpenFileDialog();
            this.bOpenDirectory = new System.Windows.Forms.Button();
            this.tbPatchFilePath = new System.Windows.Forms.TextBox();
            this.pButtons = new System.Windows.Forms.Panel();
            this.pButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // bOpenFileDialog
            // 
            this.bOpenFileDialog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bOpenFileDialog.Location = new System.Drawing.Point(229, 7);
            this.bOpenFileDialog.Name = "bOpenFileDialog";
            this.bOpenFileDialog.Size = new System.Drawing.Size(75, 26);
            this.bOpenFileDialog.TabIndex = 0;
            this.bOpenFileDialog.Text = "Browse...";
            this.bOpenFileDialog.UseVisualStyleBackColor = true;
            this.bOpenFileDialog.Click += new System.EventHandler(this.bOpenFileDialog_Click);
            // 
            // bPatch
            // 
            this.bPatch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bPatch.Location = new System.Drawing.Point(310, 7);
            this.bPatch.Name = "bPatch";
            this.bPatch.Size = new System.Drawing.Size(75, 26);
            this.bPatch.TabIndex = 2;
            this.bPatch.Text = "Patch";
            this.bPatch.UseVisualStyleBackColor = true;
            this.bPatch.Click += new System.EventHandler(this.bPatch_Click);
            // 
            // openFileDialogPatchFile
            // 
            this.openFileDialogPatchFile.FileName = "openFileDialogPatchFile";
            // 
            // bOpenDirectory
            // 
            this.bOpenDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bOpenDirectory.Location = new System.Drawing.Point(391, 7);
            this.bOpenDirectory.Name = "bOpenDirectory";
            this.bOpenDirectory.Size = new System.Drawing.Size(75, 26);
            this.bOpenDirectory.TabIndex = 3;
            this.bOpenDirectory.Text = "Open";
            this.bOpenDirectory.UseVisualStyleBackColor = true;
            this.bOpenDirectory.Click += new System.EventHandler(this.bOpenDirectory_Click);
            // 
            // tbPatchFilePath
            // 
            this.tbPatchFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPatchFilePath.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::patch_applier_gui.Properties.Settings.Default, "patchFileSelection", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tbPatchFilePath.Location = new System.Drawing.Point(12, 12);
            this.tbPatchFilePath.Name = "tbPatchFilePath";
            this.tbPatchFilePath.Size = new System.Drawing.Size(347, 22);
            this.tbPatchFilePath.TabIndex = 1;
            this.tbPatchFilePath.Text = global::patch_applier_gui.Properties.Settings.Default.patchFileSelection;
            // 
            // pButtons
            // 
            this.pButtons.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pButtons.Controls.Add(this.bPatch);
            this.pButtons.Controls.Add(this.bOpenFileDialog);
            this.pButtons.Controls.Add(this.bOpenDirectory);
            this.pButtons.Location = new System.Drawing.Point(-108, 40);
            this.pButtons.Name = "pButtons";
            this.pButtons.Size = new System.Drawing.Size(469, 36);
            this.pButtons.TabIndex = 4;
            // 
            // PatchManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 81);
            this.Controls.Add(this.pButtons);
            this.Controls.Add(this.tbPatchFilePath);
            this.MinimumSize = new System.Drawing.Size(389, 128);
            this.Name = "PatchManagerForm";
            this.Text = "Apply Patches";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PatchManagerForm_FormClosed);
            this.pButtons.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bOpenFileDialog;
        private System.Windows.Forms.TextBox tbPatchFilePath;
        private System.Windows.Forms.Button bPatch;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogPatchFile;
        private System.Windows.Forms.OpenFileDialog openFileDialogPatchFile;
        private System.Windows.Forms.Button bOpenDirectory;
        private System.Windows.Forms.Panel pButtons;
    }
}

