using CodeIsle.LibIpsNet;
using System.IO;

namespace patch_manager.Patchers
{
    public class IpsPatcher : IPatcher
    {
        private string PatchPath;

        public IpsPatcher(string patchPath)
        {
            PatchPath = patchPath;
        }

        public void Patch(Stream targetStream)
        {
            Patcher patcher = new Patcher();

            using(FileStream patchStream = File.Open(PatchPath, FileMode.Open, FileAccess.Read))
            using(MemoryStream emptyStream = new MemoryStream())
            {
                patcher.Patch(patchStream, emptyStream, targetStream);
            }
        }
    }
}
