using ips_patch_manager.Patchers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Script.Serialization;

namespace ips_patch_manager
{
    public class Patch
    {
        public string name;
        public string url;
        public string location;
        public string filetype = "ips";
        public bool? update;
    }

    public class PatchFile
    {
        public string baseRomLocation;
        public string outputRomLocation;
        public string patchfileLockDirectory;
        public List<Patch> patches;
    }

    class Program
    {
        static void Main(string[] args)
        {
            string text = File.ReadAllText(@"../../patchfile.json");
            var serializer = new JavaScriptSerializer();
            PatchFile patchFile = serializer.Deserialize<PatchFile>(text);

            List<string> patchPaths = LockAllPatches(patchFile);

            var patchers = patchPaths.Select(path => PatcherFactory.CreatePatcher(path));
            using(MemoryStream targetStream = new MemoryStream())
            {
                using(FileStream sourceStream = File.Open(patchFile.baseRomLocation, FileMode.Open, FileAccess.Read))
                {
                    sourceStream.CopyTo(targetStream);
                }

                foreach(var patcher in patchers)
                {
                    patcher.Patch(targetStream);
                }

                File.WriteAllBytes(patchFile.outputRomLocation, targetStream.ToArray());
            }
        }

        static List<string> LockAllPatches(PatchFile patchFile)
        {
            List<string> patchPaths = new List<string>();
            string tempDirectoryPath = Path.Combine(Path.GetTempPath(), "patches.lock");

            try
            {
                if(Directory.Exists(tempDirectoryPath))
                    Directory.Delete(tempDirectoryPath, true);
                var tempDirectory = Directory.CreateDirectory(tempDirectoryPath);

                var lockDirectory = Directory.CreateDirectory(patchFile.patchfileLockDirectory);
                string lockDirectoryPath = lockDirectory.FullName;

                if(patchFile.patches.Any(patch => patch.name == null))
                {
                    throw new System.Exception("No name found on patch");
                }

                if(patchFile.patches.GroupBy(patch => patch.name).Where(group => group.Count() > 1).Any())
                {
                    throw new System.Exception("Duplicate names found");
                }

                foreach(var patch in patchFile.patches)
                {
                    string lockPatchLocation = Path.Combine(lockDirectoryPath, $"{patch.name}.{patch.filetype}");
                    string tempPatchLocation = Path.Combine(tempDirectoryPath, $"{patch.name}.{patch.filetype}");

                    if(patch.update ?? true)
                    {
                        if(patch.location != null)
                        {
                            File.Copy(patch.location, tempPatchLocation);
                        }
                        else if(patch.url != null)
                        {
                            using(var client = new WebClient())
                            {
                                client.DownloadFile(patch.url, tempPatchLocation);
                            }
                        }
                        else if(File.Exists(lockPatchLocation))
                        {
                            File.Copy(lockPatchLocation, tempPatchLocation);
                        }
                        else if(!File.Exists(lockPatchLocation))
                        {
                            throw new System.Exception($"No Location or URL specified for '{patch.name}', and patch does not already exist in lock directory");
                        }
                    }
                    else
                    {
                        if(File.Exists(lockPatchLocation))
                        {
                            File.Copy(lockPatchLocation, tempPatchLocation);
                        }
                        else if(!File.Exists(lockPatchLocation))
                        {
                            throw new System.Exception($"Patch '{patch.name}' does not already exist in lock directory, and `update` is `false`");
                        }
                    }

                    patchPaths.Add(lockPatchLocation);
                }

                if(Directory.Exists(patchFile.patchfileLockDirectory))
                    Directory.Delete(patchFile.patchfileLockDirectory, true);
                lockDirectory = Directory.CreateDirectory(patchFile.patchfileLockDirectory);

                foreach(FileInfo file in tempDirectory.GetFiles())
                {
                    File.Move(file.FullName, Path.Combine(lockDirectoryPath, file.Name));
                }

                Directory.Delete(tempDirectoryPath, true);
            }
            catch
            {
                Directory.Delete(tempDirectoryPath, true);
                throw;
            }
            return patchPaths;
        }
    }
}
