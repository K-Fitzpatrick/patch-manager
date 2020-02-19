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
    /// <summary>
    /// Static class for patching functions
    /// </summary>
    public static class PatchApplier
    {
        /// <summary>
        /// Iterate through patches and acquire into specified location.
        /// </summary>
        /// <param name="patches">A list of patches to acquire</param>
        /// <param name="patchfileLockDirectory">The directory to place the acquired patches</param>
        /// <returns>List of paths; one path per acquired patch</returns>
        public static List<string> LockAllPatches(List<Patch> patches, string patchfileLockDirectory)
        {
            List<string> patchPaths = new List<string>();
            string tempDirectoryPath = Path.Combine(Path.GetTempPath(), "patches.lock");

            try
            {
                if(Directory.Exists(tempDirectoryPath))
                    Directory.Delete(tempDirectoryPath, true);
                var tempDirectory = Directory.CreateDirectory(tempDirectoryPath);

                var lockDirectory = Directory.CreateDirectory(patchfileLockDirectory);
                string lockDirectoryPath = lockDirectory.FullName;

                if(patches.Any(patch => patch.name == null))
                {
                    throw new System.Exception("No name found on patch");
                }

                if(patches.GroupBy(patch => patch.name).Where(group => group.Count() > 1).Any())
                {
                    throw new System.Exception("Duplicate names found");
                }

                foreach(var patch in patches)
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

                if(Directory.Exists(patchfileLockDirectory))
                    Directory.Delete(patchfileLockDirectory, true);
                lockDirectory = Directory.CreateDirectory(patchfileLockDirectory);

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

        /// <summary>
        /// A utility for applying patches.
        /// </summary>
        /// <param name="patchers">Patches to be applied</param>
        /// <param name="baseRomLocation">Path to the base file. Will not be modified</param>
        /// <param name="outputRomLocation">Path to output. Will be created, or overwritten if it exists</param>
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

        /// <summary>
        /// A utility for applying patches.
        /// Accept streams instead of filepaths for more flexibility.
        /// </summary>
        /// <param name="patchers">Patches to be applied</param>
        /// <param name="baseFile">Will not be modified</param>
        /// <param name="outputFile">Will be completely overwritten</param>
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
