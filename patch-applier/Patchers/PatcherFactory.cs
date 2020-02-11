using System;
using System.IO;

namespace patch_manager.Patchers
{
    public static class PatcherFactory
    {
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
