using patch_manager.Patchers;
using patch_applier.PatchFileDescription;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;

namespace patch_applier
{
    public class PatchApplier
    {
        public static List<string> LockAllPatches(PatchFile patchFile)
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
                        else if(!File.Exists(lockPatchLocation))
                        {
                            throw new System.Exception($"Patch '{patch.name}' does not already exist in lock directory, and `update` is `false`");
                        }

                        if(patch.zipPatchInfo != null)
                        {
                            // Yes, I know.
                            // I plan on cleaning this up later
                            string tempZipLocation = Path.Combine(tempDirectoryPath, $"{patch.name}.zip");
                            File.Move(tempPatchLocation, tempZipLocation);

                            using(var zip = ZipFile.OpenRead(tempZipLocation))
                            {
                                var patchFileEntry = zip.Entries
                                    .SingleOrDefault(entry => String.Equals(entry.Name, patch.zipPatchInfo.fileName, StringComparison.OrdinalIgnoreCase));

                                if(patchFileEntry == null)
                                {
                                    throw new System.Exception($"File '{patch.zipPatchInfo.fileName}' does not exist in zip file '{lockPatchLocation}'");
                                }

                                patchFileEntry.ExtractToFile(tempPatchLocation, true);
                            }

                            File.Delete(tempZipLocation);
                        }
                    }
                    else
                    {
                        if(!File.Exists(lockPatchLocation))
                        {
                            throw new System.Exception($"Told not to update for '{patch.name}', and patch does not already exist in lock directory");
                        }

                        File.Copy(lockPatchLocation, tempPatchLocation);
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

        public static void Patch(List<IPatcher> patchers, string baseRomLocation, string outputRomLocation)
        {
            using(MemoryStream targetStream = new MemoryStream())
            {
                using(FileStream sourceStream = File.Open(baseRomLocation, FileMode.Open, FileAccess.Read))
                {
                    Patch(patchers, sourceStream, targetStream);
                }
                File.WriteAllBytes(outputRomLocation, targetStream.ToArray());
            }
        }

        public static void Patch(List<IPatcher> patchers, Stream baseFile, Stream outputFile)
        {
            baseFile.CopyTo(outputFile);

            foreach(var patcher in patchers)
            {
                patcher.Patch(outputFile);
            }
        }
    }
}
