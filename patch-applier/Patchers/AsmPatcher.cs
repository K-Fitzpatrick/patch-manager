using System.IO;

namespace patch_manager.Patchers
{
    /// <summary>
    /// Applies a .asm (Assembly) patch.
    /// </summary>
    public class AsmPatcher : IPatcher
    {
        private string PatchPath;

        /// <summary>
        /// </summary>
        /// <param name="patchPath"></param>
        public AsmPatcher(string patchPath)
        {
            PatchPath = patchPath;
        }

        /// <summary>
        /// </summary>
        /// <param name="targetStream"></param>
        public void Patch(Stream targetStream)
        {
            // This is all a big workaround for the fact that xkas has to take a file pointer

            string tempFilePath = Path.GetTempFileName();
            using(FileStream fileStream = new FileStream(tempFilePath, FileMode.Open, FileAccess.Write))
            {
                targetStream.Seek(0, SeekOrigin.Begin);
                targetStream.CopyTo(fileStream);
            }

            string tempOutputLocation = Path.GetTempFileName();

            xkas.cskas.Assemble(PatchPath, tempFilePath, tempOutputLocation, true);

            using(FileStream outputStream = new FileStream(tempOutputLocation, FileMode.Open, FileAccess.Read))
            {
                targetStream.Seek(0, SeekOrigin.Begin);
                outputStream.CopyTo(targetStream);
                targetStream.SetLength(outputStream.Length);
            }

            File.Delete(tempFilePath);
            File.Delete(tempOutputLocation);
        }
    }
}
