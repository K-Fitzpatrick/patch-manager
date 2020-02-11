using AsarCLR;
using System.IO;

namespace ips_patch_manager.Patchers
{
    public class AsmPatcher : IPatcher
    {
        private string PatchPath;

        public AsmPatcher(string patchPath)
        {
            PatchPath = patchPath;
        }

        public void Patch(Stream targetStream)
        {
            targetStream.Position = 0;
            var bytes = new byte[targetStream.Length];
            targetStream.Read(bytes, 0, (int)targetStream.Length);

            if(!Asar.init())
            {
                throw new System.Exception("Asar didn't initialize for some reason");
            }
            Asar.patch(PatchPath, ref bytes);
            Asar.close();

            using(var resultStream = new MemoryStream(bytes))
            {
                targetStream.Seek(0, SeekOrigin.Begin);
                resultStream.CopyTo(targetStream);
                targetStream.SetLength(resultStream.Length);
            }
        }
    }
}
