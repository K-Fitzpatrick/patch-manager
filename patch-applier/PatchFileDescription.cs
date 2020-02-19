using System.Collections.Generic;

namespace patch_applier
{
    namespace PatchFileDescription
    {
        /// <summary>
        /// Describes how an acquired zipfile should be handled.
        /// </summary>
        public class ZipPatchInfo
        {
            /// <summary>
            /// Required. Attempt will be made to extract this specific file into the target location.
            /// </summary>
            public string fileName;
        }

        /// <summary>
        /// An object that describes how a Patch should be acquired.
        /// Must specify either a URL, or a location on-disk.
        /// </summary>
        public class Patch
        {
            /// <summary>
            /// The name of the the patch.
            /// Will be used in conjuction with `fileType` to name the acquired file.
            /// </summary>
            public string name;
            /// <summary>
            /// A URL to somewhere on the internet.
            /// Will be used to download a file.
            /// </summary>
            public string url;
            /// <summary>
            /// A local path to the file to acquire.
            /// Relative and absolute paths are accepted.
            /// </summary>
            public string location;
            /// <summary>
            /// Defaults to "IPS".
            /// Used to name the acquired file.
            /// </summary>
            public string filetype = "ips";
            /// <summary>
            /// Defaults to true.
            /// If true, re-download from source.
            /// If false, use previously downloaded copy. Throw if not available.
            /// </summary>
            public bool? update;
            /// <summary>
            /// Optional information, for zipfiles.
            /// The extracted file will be acquried, and the zipfile discarded.
            /// </summary>
            public ZipPatchInfo zipPatchInfo;
        }

        /// <summary>
        /// Describes how a series of Patches should be acquired and applied to a base file.
        /// Designed to be deserialized into.
        /// </summary>
        public class PatchFile
        {
            /// <summary>
            /// The path to the Base Rom.
            /// Original file is left untouched.
            /// </summary>
            public string baseRomLocation;
            /// <summary>
            /// The path to the output.
            /// File will be created/overwritten.
            /// </summary>
            public string outputRomLocation;
            /// <summary>
            /// The path to a directory that all files will be ulitmately acquired into.
            /// </summary>
            public string patchfileLockDirectory;
            /// <summary>
            /// List of patches to acquire.
            /// </summary>
            public List<Patch> patches;
        }
    }
}
