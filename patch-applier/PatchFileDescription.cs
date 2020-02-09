using System.Collections.Generic;

namespace patch_applier
{
    namespace PatchFileDescription
    {
        public class ZipPatchInfo
        {
            public string fileName;
        }

        public class Patch
        {
            public string name;
            public string url;
            public string location;
            public string filetype = "ips";
            public bool? update;
            public ZipPatchInfo zipPatchInfo;
        }

        public class PatchFile
        {
            public string baseRomLocation;
            public string outputRomLocation;
            public string patchfileLockDirectory;
            public List<Patch> patches;
        }
    }
}
