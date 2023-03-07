// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 22/02/2023     \\

using Dirfile_lib.Utilities.Validation;
using Dirfile_lib.Core;
using Dirfile_lib.Exceptions;
using CT = Dirfile_lib.Core.Constants.Texts;

namespace Dirfile_lib_TEST.UtilitiesTests.ValidatorTests
{
    /// <summary>
    /// Tests <see cref="Checker"/> class.
    /// </summary>
    [TestClass]
    public class NameCheckerTests
    {
        /// <summary>
        /// Tests checker if it works properly.
        /// </summary>
        [TestMethod]
        public void TestNameChecker()
        {
            // Test cases for Checker class
            var testData = PrepareTestData();
            var checker = new NameChecker();

            foreach (var data in testData)
            {
                string returned;
                bool? valid = null;

                try
                {
                    valid = checker.IsValid(data.Key);
                    returned = checker.DirfileType;
                }
                catch (DirfileException)
                {
                    returned = string.Empty;
                }

                if (valid.HasValue && !valid.Value)
                {
                    Assert.IsNull(returned, $"Checker did not return empty string on {data.Key}");
                    checker.Clean();
                    continue;
                }
                else
                {
                    object? ext;

                    try
                    {
                        ext = checker.GetExtension();
                    }
                    catch (DirfileException)
                    {
                        ext = null;
                    }

                    Assert.AreEqual(data.Value.Type, returned, $"Checker returned wrongly on {data.Key}");
                    Assert.AreEqual(data.Value.Name, checker.DirfileName, $"Checker returned wrongly Name on {data.Key}");
                    Assert.AreEqual(data.Value.ExtensionName, checker.DirfileExtension, $"Checker returned wrongly ExtensionName on {data.Key}");
                    Assert.AreEqual(data.Value.Extension, ext, $"Checker returned wrongly Extension on {data.Key}");
                }

                checker.Clean();
            }
        }

        /// <summary>
        /// Prepares test data for a test.
        /// </summary>
        /// <returns>Dictionary of string (passed input, e.g. from input as passed parameter) and expected result.</returns>
        private static Dictionary<string, ExpectedData?> PrepareTestData()
        {
            return new Dictionary<string, ExpectedData?>()
            {
                {
                    "NewDir",
                    new ExpectedData() { Type = CT.Director, Name = "NewDir", ExtensionName = "", Extension = null }
                },
                {
                    "NewFile.txt",
                    new ExpectedData() { Type = CT.Filer, Name = "NewFile", ExtensionName = ".txt", Extension = DirfileExtensions.Text.TXT }
                },
                {
                    "New-Director)",
                    new ExpectedData() { Type = CT.Director, Name = "New-Director)", ExtensionName = string.Empty, Extension = null }
                },
                {
                    "New.File.Wait.csv",
                    new ExpectedData() { Type = CT.Filer, Name = "New.File.Wait", ExtensionName = ".csv", Extension = DirfileExtensions.Data.CSV }
                },
                {
                    "Another-File.txt.txt",
                    new ExpectedData(){ Type = CT.Filer, Name = "Another-File.txt", ExtensionName = ".txt", Extension = DirfileExtensions.Text.TXT }
                },
                {
                    "abcdefghijklmnopqrstuvwxyz1234.exe",
                    new ExpectedData() { Type = CT.Filer, Name = "abcdefghijklmnopqrstuvwxyz1234", ExtensionName = ".exe", Extension = DirfileExtensions.Executable.EXE}
                },
                {
                    " WrongDirector",
                    null
                },
                {
                    "WrongFiler.txt ",
                    null
                },
                {
                    ".WrongDirector",
                    null
                },
                {
                    "WrongFiler.txt.",
                    null
                },
                {
                    "_WrongDirector",
                    null
                },
                {
                    "WrongFiler.txt_",
                    null
                },
                {
                    "_WrongDirector.",
                    null
                },
                {
                    "NewWrongFile.txxt",
                    new ExpectedData() { Type = CT.Director, Name = "NewWrongFile.txxt", ExtensionName = string.Empty, Extension = null }
                },
                {
                    " -WrongDirector",
                    null
                },
                {
                    "Wrong/Filer",
                    null
                },
                {
                    "GoodFiler.jpeg",
                    new ExpectedData() { Type = CT.Filer, Name = "GoodFiler", ExtensionName = ".jpeg", Extension = DirfileExtensions.Image.JPEG }
                },
                {
                    "abcdefghijklmnopqrstuvwxyz12345",
                    null
                }
            };
        }

        /// <summary>
        /// Stores expected data for <see cref="Checker"/> class to compare.
        /// </summary>
        private class ExpectedData
        {
            /// <summary>
            /// Gets or sets the Extension of expected Dirfile.
            /// </summary>
            public Enum? Extension { get; set; }

            /// <summary>
            /// Gets or sets the ExtensionName of expected Dirfile.
            /// </summary>
            public string ExtensionName { get; set; } = string.Empty;

            /// <summary>
            /// Gets or sets the name of expected Dirfile.
            /// </summary>
            public string Name { get; set; } = string.Empty;

            /// <summary>
            /// Gets or sets the type of expected Dirfile.
            /// </summary>
            public string Type { get; set; } = string.Empty;
        }
    }
}