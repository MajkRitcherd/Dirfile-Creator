// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 26/04/2023     \\

using Dirfile_lib.Utilities;
using DirfileTypes = Dirfile_lib.Core.Constants.DirFile.Types;

namespace Dirfile_lib_TEST.UtilitiesTests
{
    /// <summary>
    /// Tests <see cref="DirfileRecognizer"/> class.
    /// </summary>
    [TestClass]
    public class DirfileRecognizerTests
    {
        /// <summary>
        /// Tests recognizing dirfiles using <see cref="DirfileRecognizer"/> class.
        /// </summary>
        [TestMethod]
        public void TestRecognize()
        {
            var dirfileRecognizer = new DirfileRecognizer();

            foreach (var expectedDataByName in GetTestData())
            {
                var dirfileType = dirfileRecognizer.Recognize(expectedDataByName.Key, out string dirfileName, out string extensionName, out bool isExtension);

                Assert.AreEqual(expectedDataByName.Value.DirfileType, dirfileType, $"Expected type was {expectedDataByName.Value.DirfileType}, Actual type: {dirfileType}!");
                Assert.AreEqual(expectedDataByName.Value.DirfileName, dirfileName, $"Expected name was {expectedDataByName.Value.DirfileName}, Actual name: {dirfileName}!");
                Assert.AreEqual(expectedDataByName.Value.ExtensionName, extensionName, $"Expected extension name was {expectedDataByName.Value.ExtensionName}, Actual extension name: {extensionName}!");
                Assert.AreEqual(expectedDataByName.Value.IsExtension, isExtension, $"IsExtension should be {expectedDataByName.Value.IsExtension}, Actual is: {isExtension}!");
            }
        }

        /// <summary>
        /// Prepares test data.
        /// </summary>
        /// <returns>Expected data by Name.</returns>
        private static Dictionary<string, ExpectedData> GetTestData()
        {
            return new Dictionary<string, ExpectedData>()
            {
                { "TestFile1.txt", new ExpectedData() { DirfileType = DirfileTypes.Filer, DirfileName = "TestFile1", ExtensionName = ".txt", IsExtension = true } },
                { "TestFile2.csv", new ExpectedData() { DirfileType = DirfileTypes.Filer, DirfileName = "TestFile2", ExtensionName = ".csv", IsExtension = true } },
                { "TestFile3.tv.bat", new ExpectedData() { DirfileType = DirfileTypes.Filer, DirfileName = "TestFile3.tv", ExtensionName = ".bat", IsExtension = true } },
                { "TestDirector", new ExpectedData() { DirfileType = DirfileTypes.Director, DirfileName = "TestDirector", ExtensionName = string.Empty, IsExtension = false } },
                { "TestDirector2", new ExpectedData() { DirfileType = DirfileTypes.Director, DirfileName = "TestDirector2", ExtensionName = string.Empty, IsExtension = false } },
                { "TestDirector3.lol", new ExpectedData() { DirfileType = DirfileTypes.Director, DirfileName = "TestDirector3.lol", ExtensionName = string.Empty, IsExtension = false } },
                { "Test.Director4", new ExpectedData() { DirfileType = DirfileTypes.Director, DirfileName = "Test.Director4", ExtensionName = string.Empty, IsExtension = false } }
            };
        }
    }

    /// <summary>
    /// Class representing expected data of the result.
    /// </summary>
    internal class ExpectedData
    {
        /// <summary>
        /// Gets or sets the expected type (Filer of Director).
        /// </summary>
        public string? DirfileType { get; set; }

        /// <summary>
        /// Gets or sets the expected name of Filer or Director.
        /// </summary>
        public string? DirfileName { get; set; }

        /// <summary>
        /// Gets or sets the expected extension name.
        /// </summary>
        public string? ExtensionName { get; set; }

        /// <summary>
        /// Gets or sets whether it is extension or not.
        /// </summary>
        public bool IsExtension { get; set; }
    }
}
