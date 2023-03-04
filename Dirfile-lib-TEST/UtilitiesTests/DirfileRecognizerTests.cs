// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 01/03/2023     \\

using Dirfile_lib.Utilities;
using CT = Dirfile_lib.Core.Constants.Texts;

namespace Dirfile_lib_TEST.UtilitiesTests
{
    /// <summary>
    /// Tests <see cref="DirfileNameRecognizer"/> class.
    /// </summary>
    [TestClass]
    public class DirfileRecognizerTests
    {
        //private const string Director = "Director";
        //private const string Filer = "Filer";
        /// <summary>
        /// Tests recognizing dirfiles using <see cref="DirfileNameRecognizer"/> class.
        /// </summary>
        [TestMethod]
        public void TestRecognize()
        {
            var recognizer = new DirfileNameRecognizer();
            foreach (var data in this.Expected)
            {
                var res = recognizer.Recognize(data.Key, out string name, out string ext, out bool isExtension);
                Assert.AreEqual(data.Value.Type, res, "");
                Assert.AreEqual(data.Value.Name, name, "");
                Assert.AreEqual(data.Value.ExtensionName, ext, "");
                Assert.AreEqual(data.Value.IsExtension, isExtension, "");
            }
        }

        /// <summary>
        /// Dictionary of expected results.
        /// </summary>
        private readonly Dictionary<string, ExpectedData> Expected = new()
        {
            { "TestFile1.txt", new ExpectedData() { Type = CT.Filer, Name = "TestFile1", ExtensionName = ".txt", IsExtension = true } },
            { "TestFile2.csv", new ExpectedData() { Type = CT.Filer, Name = "TestFile2", ExtensionName = ".csv", IsExtension = true } },
            { "TestFile3.tv.bat", new ExpectedData() { Type = CT.Filer, Name = "TestFile3.tv", ExtensionName = ".bat", IsExtension = true } },
            { "TestDirector", new ExpectedData() { Type = CT.Director, Name = "TestDirector", ExtensionName = string.Empty, IsExtension = false } },
            { "TestDirector2", new ExpectedData() { Type = CT.Director, Name = "TestDirector2", ExtensionName = string.Empty, IsExtension = false } },
            { "TestDirector3.lol", new ExpectedData() { Type = CT.Director, Name = "TestDirector3.lol", ExtensionName = string.Empty, IsExtension = true } },
            { "Test.Director4", new ExpectedData() { Type = CT.Director, Name = "Test.Director4", ExtensionName = string.Empty, IsExtension = true } },
        };
    }

    /// <summary>
    /// Class representing expected data of the result.
    /// </summary>
    internal class ExpectedData
    {
        /// <summary>
        /// Gets or sets the expected type (Filer of Director)
        /// </summary>
        public string? Type { get; set; }

        /// <summary>
        /// Gets or sets the expected name of Filer or Director.
        /// </summary>
        public string? Name { get; set; }
        
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
