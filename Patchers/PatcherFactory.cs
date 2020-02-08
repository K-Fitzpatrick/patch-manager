using System;
using System.IO;

namespace ips_patch_manager.Patchers
{
    public static class PatcherFactory
    {
        public static IPatcher CreatePatcher(string patchPath)
        {
            if(Path.GetExtension(patchPath).Equals(".ips", StringComparison.OrdinalIgnoreCase))
            {
                return new IpsPatcher(patchPath);
            }

            throw new NotImplementedException($"Unhandled file type on path '{patchPath}'.");
        }
    }
}
