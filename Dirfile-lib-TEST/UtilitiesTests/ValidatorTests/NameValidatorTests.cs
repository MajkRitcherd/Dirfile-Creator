// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 26/04/2023     \\

using Dirfile_lib.Utilities.Validation;
using Dirfile_lib.Extensions;
using Dirfile_lib.Exceptions;
using DirfileTypes = Dirfile_lib.Core.Constants.DirFile.Types;

namespace Dirfile_lib_TEST.UtilitiesTests.ValidatorTests
{
    /// <summary>
    /// Tests <see cref="NameValidator"/> class.
    /// </summary>
    [TestClass]
    public class NameValidatorTests
    {
        /// <summary>
        /// Tests dirfile names using <see cref="NameValidator"/> class.
        /// </summary>
        [TestMethod]
        public void TestNameChecker()
        {
            var nameValidator = new NameValidator();

            foreach (var expectedDataByName in GetTestData())
            {
                string dirfileType;
                bool? isValid = null;

                try
                {
                    isValid = nameValidator.IsValid(expectedDataByName.Key);
                    dirfileType = nameValidator.DirfileType;
                }
                catch (DirfileException)
                {
                    dirfileType = string.Empty;
                }

                if (isValid.HasValue && !isValid.Value)
                {
                    Assert.IsNull(dirfileType, $"Checker did not return empty string on {expectedDataByName.Key}");
                    nameValidator.Clean();
                    continue;
                }
                else
                {
                    object? extension;

                    try
                    {
                        extension = nameValidator.GetExtension();
                    }
                    catch (DirfileException)
                    {
                        extension = null;
                    }

                    Assert.AreEqual(expectedDataByName.Value!.DirfileType, dirfileType, $"Expected type was {expectedDataByName.Value!.DirfileType}, Actual type: {dirfileType}");
                    Assert.AreEqual(expectedDataByName.Value.DirfileName, nameValidator.DirfileName, $"Expected name was {expectedDataByName.Value.DirfileName}, Actual type: {nameValidator.DirfileName}");
                    Assert.AreEqual(expectedDataByName.Value.ExtensionName, nameValidator.ExtensionName, $"Expected extension name was {expectedDataByName.Value.ExtensionName}, Actual extension name: {nameValidator.ExtensionName}");
                    Assert.AreEqual(expectedDataByName.Value.Extension, extension, $"Expected extension was {expectedDataByName.Value.Extension}, Actual extension: {extension}");
                }

                nameValidator.Clean();
            }
        }

        /// <summary>
        /// Prepares test data for a test.
        /// </summary>
        /// <returns>Expected data by name.</returns>
        private static Dictionary<string, ExpectedData?> GetTestData()
        {
            return new Dictionary<string, ExpectedData?>()
            {
                {
                    "NewDir",
                    new ExpectedData() { DirfileType = DirfileTypes.Director, DirfileName = "NewDir", ExtensionName = "", Extension = null }
                },
                {
                    "NewFile.txt",
                    new ExpectedData() { DirfileType = DirfileTypes.Filer, DirfileName = "NewFile", ExtensionName = ".txt", Extension = DirfileExtensions.Text.TXT }
                },
                {
                    "New-Director)",
                    new ExpectedData() { DirfileType = DirfileTypes.Director, DirfileName = "New-Director)", ExtensionName = string.Empty, Extension = null }
                },
                {
                    "New.File.Wait.csv",
                    new ExpectedData() { DirfileType = DirfileTypes.Filer, DirfileName = "New.File.Wait", ExtensionName = ".csv", Extension = DirfileExtensions.Data.CSV }
                },
                {
                    "Another-File.txt.txt",
                    new ExpectedData(){ DirfileType = DirfileTypes.Filer, DirfileName = "Another-File.txt", ExtensionName = ".txt", Extension = DirfileExtensions.Text.TXT }
                },
                {
                    "abcdefghijklmnopqrstuvwxyz1234.exe",
                    new ExpectedData() { DirfileType = DirfileTypes.Filer, DirfileName = "abcdefghijklmnopqrstuvwxyz1234", ExtensionName = ".exe", Extension = DirfileExtensions.Executable.EXE}
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
                    new ExpectedData() { DirfileType = DirfileTypes.Director, DirfileName = "NewWrongFile.txxt", ExtensionName = string.Empty, Extension = null }
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
                    new ExpectedData() { DirfileType = DirfileTypes.Filer, DirfileName = "GoodFiler", ExtensionName = ".jpeg", Extension = DirfileExtensions.Image.JPEG }
                },
                {
                    "abcdefghijklmnopqrstuvwxyz12345",
                    null
                }
            };
        }

        /// <summary>
        /// Stores expected data for <see cref="NameValidator"/> class to compare.
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
            public string DirfileName { get; set; } = string.Empty;

            /// <summary>
            /// Gets or sets the type of expected Dirfile.
            /// </summary>
            public string DirfileType { get; set; } = string.Empty;
        }
    }
}