using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using patch_applier.PatchFileDescription;
using System.Collections.Generic;
using patch_applier;
using System.Linq;
using System.IO;
using ips_patch_manager.Patchers;

namespace patch_applier_tests.PatchApplierTests
{
    [TestClass]
    public class PatchTests
    {
        readonly string CHANGE_1 = "1.IPS";
        readonly string CHANGE_2 = "2.IPS";
        readonly string CHANGE_3 = "3.IPS";

        readonly string TEST_PATCH_DIR = "TestPatches";
        readonly string BASE_FILE = "base-file.smc";

        Stream RunPatches(List<string> patchPaths)
        {
            string baseFilePath = Path.Combine(TEST_PATCH_DIR, BASE_FILE);
            var patchers = patchPaths.Select(path => PatcherFactory.CreatePatcher(path)).ToList();

            using(Stream baseFile = new FileStream(baseFilePath, FileMode.Open, FileAccess.Read))
            {
                Stream target = new MemoryStream();
                PatchApplier.Patch(patchers, baseFile, target);
                return target;
            }
        }

        List<string> GetPatchPaths(List<string> names)
        {
            return names
                .Select(name => Path.Combine(TEST_PATCH_DIR, name))
                .ToList();
        }

        FileStream OpenFile(string fileName)
        {
            return new FileStream(Path.Combine(TEST_PATCH_DIR, fileName), FileMode.Open, FileAccess.Read);
        }

        bool IsEqual(Stream left, Stream right)
        {
            if(left.Length != right.Length)
                return false;

            left.Position = 0;
            right.Position = 0;

            var leftBytes = new byte[left.Length];
            left.Read(leftBytes, 0, (int)left.Length);
            var rightBytes = new byte[right.Length];
            right.Read(rightBytes, 0, (int)right.Length);

            return leftBytes.SequenceEqual(rightBytes);
        }

        [TestMethod]
        public void NoPatch()
        {
            var patches = GetPatchPaths(new List<string>
            {
            });

            using(Stream expectedResult = OpenFile(BASE_FILE))
            using(Stream actual = RunPatches(patches))
            {
                Assert.IsTrue(IsEqual(expectedResult, actual), "Streams do not match!");
            }
        }

        [TestMethod]
        public void Patch1()
        {
            var patches = GetPatchPaths(new List<string>
            {
                CHANGE_1,
            });

            using(Stream expectedResult = OpenFile("base-file-1.smc"))
            using(Stream actual = RunPatches(patches))
            {
                Assert.IsTrue(IsEqual(expectedResult, actual), "Streams do not match!");
            }
        }

        [TestMethod]
        public void Patch12()
        {
            var patches = GetPatchPaths(new List<string>
            {
                CHANGE_1,
                CHANGE_2,
            });

            using(Stream expectedResult = OpenFile("base-file-1-2.smc"))
            using(Stream actual = RunPatches(patches))
            {
                Assert.IsTrue(IsEqual(expectedResult, actual), "Streams do not match");
            }
        }


        [TestMethod]
        public void PatchingOutOfOrderFails()
        {
            var patches = GetPatchPaths(new List<string>
            {
                CHANGE_2,
                CHANGE_1,
            });

            using(Stream expectedResult = OpenFile("base-file-1-2.smc"))
            using(Stream actual = RunPatches(patches))
            {
                Assert.IsFalse(IsEqual(expectedResult, actual), "Streams match, but they shouldn't");
            }
        }
    }
}
