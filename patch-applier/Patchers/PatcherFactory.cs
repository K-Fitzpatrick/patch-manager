using System;
using System.IO;

namespace patch_manager.Patchers
{
    /// <summary>
    /// Creates the correct Patcher object for a given path.
    /// </summary>
    public static class PatcherFactory
    {
        /// <summary>
        /// Creates the correct Patcher object for a given path.
        /// Constructed based on file extension.
        /// Throws if file extension is unknown.
        /// </summary>
        public static IPatcher CreatePatcher(string patchPath)
        {
            if(Path.GetExtension(patchPath).Equals(".ips", StringComparison.OrdinalIgnoreCase))
            {
                return new IpsPatcher(patchPath);
            }
            else if(Path.GetExtension(patchPath).Equals(".asm", StringComparison.OrdinalIgnoreCase))
            {
                return new AsmPatcher(patchPath);
            }

            throw new NotImplementedException($"Unhandled file type on path '{patchPath}'.");
        }
    }
}
