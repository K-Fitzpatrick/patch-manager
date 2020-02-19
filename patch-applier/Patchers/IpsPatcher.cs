using CodeIsle.LibIpsNet;
using System.IO;

namespace patch_manager.Patchers
{
    /// <summary>
    /// Applies a .ips (International Patching System) patch.
    /// </summary>
    public class IpsPatcher : IPatcher
    {
        private string PatchPath;

        /// <summary>
        /// </summary>
        /// <param name="patchPath"></param>
        public IpsPatcher(string patchPath)
        {
            PatchPath = patchPath;
        }

        /// <summary>
        /// </summary>
        /// <param name="targetStream"></param>
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
