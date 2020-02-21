using patch_applier;
using patch_applier.PatchFileDescription;
using patch_manager.Patchers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace patch_applier_gui
{
    public partial class PatchManagerForm : Form
    {
        public PatchManagerForm()
        {
            InitializeComponent();
        }

        private void bOpenFileDialog_Click(object sender, EventArgs e)
        {
            var result = openFileDialogPatchFile.ShowDialog();

            if(result == DialogResult.OK
                && !String.IsNullOrWhiteSpace(openFileDialogPatchFile.FileName))
            {
                tbPatchFilePath.Text = openFileDialogPatchFile.FileName;
            }
        }

        /// <summary>
        /// Patches according to the selected patchfile.
        /// All paths should be relative to the selected file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bPatch_Click(object sender, EventArgs e)
        {
            string patchFilePath = tbPatchFilePath.Text;

            if(String.IsNullOrWhiteSpace(patchFilePath))
            {
                MessageBox.Show("Please select a file");
                return;
            }

            if(!File.Exists(patchFilePath))
            {
                MessageBox.Show("File not found");
                return;
            }

            try
            {
                CreateRom(patchFilePath);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Unexpected error");
                return;
            }
        }

        private void CreateRom(string patchFilePath)
        {
            string text = File.ReadAllText(patchFilePath);
            var serializer = new JavaScriptSerializer();
            PatchFile patchFile = serializer.Deserialize<PatchFile>(text);

            string directory = Path.GetDirectoryName(patchFilePath);
            Directory.SetCurrentDirectory(directory);

            List<string> patchPaths = PatchApplier.LockAllPatches(patchFile.patches, patchFile.patchfileLockDirectory);

            var patchers = patchPaths.Select(path => PatcherFactory.CreatePatcher(path)).ToList();
            PatchApplier.Patch(patchers, patchFile.baseRomLocation, patchFile.outputRomLocation);
        }

        private void bOpenDirectory_Click(object sender, EventArgs e)
        {
            try
            {
                string patchFilePath = tbPatchFilePath.Text;
                string directory = Path.GetDirectoryName(patchFilePath);
                Process.Start(directory);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Unexpected error");
                return;
            }
        }

        private void PatchManagerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Save off application settings like patchfile selection
            Properties.Settings.Default.Save();
        }
    }
}
