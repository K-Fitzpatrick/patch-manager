using ips_patch_manager.Patchers;
using patch_applier;
using patch_applier.PatchFileDescription;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;

namespace ips_patch_manager
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = File.ReadAllText(@"../../patchfile.json");
            var serializer = new JavaScriptSerializer();
            PatchFile patchFile = serializer.Deserialize<PatchFile>(text);

            List<string> patchPaths = PatchApplier.LockAllPatches(patchFile);

            var patchers = patchPaths.Select(path => PatcherFactory.CreatePatcher(path));
        }
    }
}
