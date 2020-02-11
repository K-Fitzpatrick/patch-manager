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

        void HandleAsarFailure(string msg)
        {
            System.Exception e = new System.Exception(msg);
            e.Data.Add("AsarWarnings", Asar.getwarnings());
            e.Data.Add("AsarErrors", Asar.geterrors());
            throw e;
        }

        public void Patch(Stream targetStream)
        {
            targetStream.Position = 0;
            var bytes = new byte[targetStream.Length];
            targetStream.Read(bytes, 0, (int)targetStream.Length);

            if(!Asar.init())
            {
                HandleAsarFailure("Asar failed to initialize");
            }
            if(!Asar.patch(PatchPath, ref bytes))
            {
                HandleAsarFailure("Asar failed to patch");
            }
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
