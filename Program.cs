using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    public class BaseRom
    {
        public string location;
    }

    public class PatchFile
    {
        public BaseRom baseRom;
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
        }
    }
}
