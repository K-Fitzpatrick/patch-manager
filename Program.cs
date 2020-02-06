using CodeIsle.LibIpsNet;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;

namespace ips_patch_manager
{
    public class Patch
    {
        public string name;
        public string url;
        public string location;
    }

    public class PatchFile
    {
        public string baseRomLocation;
        public string outputRomLocation;
        public List<Patch> patches;
    }

    class Program
    {
        static void Main(string[] args)
        { 
            string text = File.ReadAllText(@"../../patchfile.json");
            var serializer = new JavaScriptSerializer();
            PatchFile patchFile = serializer.Deserialize<PatchFile>(text);

            // Use URL to download patches into a temporary file, updating the location in-memory
            // Then apply the patches in the described order

            Patch(patchFile);
        }

        static void Patch(PatchFile patchFile)
        {
            Patcher patcher = new Patcher();
            using(MemoryStream targetStream = new MemoryStream())
            {
                using(FileStream sourceStream = File.Open(patchFile.baseRomLocation, FileMode.Open, FileAccess.Read))
                {
                    sourceStream.CopyTo(targetStream);
                }

                // empty stream, so targetStream just keeps being reused over and over again
                using(MemoryStream emptyStream = new MemoryStream())
                {
                    foreach(Patch patch in patchFile.patches)
                    {
                        using(FileStream patchStream = File.Open(patch.location, FileMode.Open, FileAccess.Read))
                        {
                            patcher.Patch(patchStream, emptyStream, targetStream);
                        }
                    }
                }

                File.WriteAllBytes(patchFile.outputRomLocation, targetStream.ToArray());
            }
        }
    }
}
