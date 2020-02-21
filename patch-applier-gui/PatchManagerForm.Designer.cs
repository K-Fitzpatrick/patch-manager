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
            this.tbPatchFilePath = new System.Windows.Forms.TextBox();
            this.bPatch = new System.Windows.Forms.Button();
            this.folderBrowserDialogPatchFile = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialogPatchFile = new System.Windows.Forms.OpenFileDialog();
            this.bOpenDirectory = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bOpenFileDialog
            // 
            this.bOpenFileDialog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bOpenFileDialog.Location = new System.Drawing.Point(53, 40);
            this.bOpenFileDialog.Name = "bOpenFileDialog";
            this.bOpenFileDialog.Size = new System.Drawing.Size(75, 26);
            this.bOpenFileDialog.TabIndex = 0;
            this.bOpenFileDialog.Text = "Browse...";
            this.bOpenFileDialog.UseVisualStyleBackColor = true;
            this.bOpenFileDialog.Click += new System.EventHandler(this.bOpenFileDialog_Click);
            // 
            // tbPatchFilePath
            // 
            this.tbPatchFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPatchFilePath.Location = new System.Drawing.Point(12, 12);
            this.tbPatchFilePath.Name = "tbPatchFilePath";
            this.tbPatchFilePath.Size = new System.Drawing.Size(278, 22);
            this.tbPatchFilePath.TabIndex = 1;
            this.tbPatchFilePath.Text = ".\\patchfile.json";
            // 
            // bPatch
            // 
            this.bPatch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bPatch.Location = new System.Drawing.Point(134, 40);
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
            this.bOpenDirectory.Location = new System.Drawing.Point(215, 40);
            this.bOpenDirectory.Name = "bOpenDirectory";
            this.bOpenDirectory.Size = new System.Drawing.Size(75, 26);
            this.bOpenDirectory.TabIndex = 3;
            this.bOpenDirectory.Text = "Open";
            this.bOpenDirectory.UseVisualStyleBackColor = true;
            this.bOpenDirectory.Click += new System.EventHandler(this.bOpenDirectory_Click);
            // 
            // PatchManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(302, 78);
            this.Controls.Add(this.bOpenDirectory);
            this.Controls.Add(this.bPatch);
            this.Controls.Add(this.tbPatchFilePath);
            this.Controls.Add(this.bOpenFileDialog);
            this.MinimumSize = new System.Drawing.Size(320, 125);
            this.Name = "PatchManagerForm";
            this.Text = "Apply Patches";
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
    }
}

