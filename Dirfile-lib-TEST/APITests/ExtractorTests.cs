// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 03/03/2023     \\

using Dirfile_lib.API.Extraction;
using Dirfile_lib.Exceptions;
using Dirfile_lib_TEST.UtilitiesTests;

namespace Dirfile_lib_TEST.APITests
{
    /// <summary>
    /// Tests <see cref="Extractor"/> class.
    /// </summary>
    [TestClass]
    public class ExtractorTests
    {
        /// <summary>
        /// Gets or sets the <see cref="Extractor"/> class.
        /// </summary>
        private Extractor? _Extractor { get; set; }

        /// <summary>
        /// Gets or sets the slash mode used in inputs.
        /// </summary>
        private SlashMode _SlashMode { get; set; }

        /// <summary>
        /// Test initializing.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            this._SlashMode = SlashMode.Backward;
            this._Extractor = new Extractor(this._SlashMode);
        }

        /// <summary>
        /// Tests whole <see cref="Extractor"/>.
        /// </summary>
        [TestMethod]
        public void TestExtractor()
        {
            foreach (var testData in PrepareTestData().Select((value, index) => new { value, index }))
            {
                if (testData.index >= 5)
                    this._SlashMode = SlashMode.Forward;

                try
                {
                    this._Extractor?.ExtractInput(testData.value.Key);
                }
                catch (DirfileException)
                {
                    Assert.IsTrue(true);
                    continue;
                }

                if (testData.value.Value != null)
                {
                    Assert.AreEqual(testData.value.Value.ExpInput, this._Extractor?.InputString, $"Input strings were not the same: {testData.value.Value.ExpInput}");
                    Assert.AreEqual(testData.value.Value.ExpDirectorPath, this._Extractor?.DirectorPath, $"Input strings were not the same: {testData.value.Value.ExpDirectorPath}");
                    Assert.AreEqual(testData.value.Value.ExpArgument, this._Extractor?.Arguments, $"Input strings were not the same: {testData.value.Value.ExpArgument}");
                }
            }
        }

        /// <summary>
        /// Prepares test data.
        /// </summary>
        /// <returns>Dictionary of strings and expected data.</returns>
        private static Dictionary<string, ExpectedData?> PrepareTestData()
        {
            string currDir = Directory.GetCurrentDirectory();
            currDir = currDir[..currDir.LastIndexOf('\\')];

            return new Dictionary<string, ExpectedData?>()
            {
                {
                    currDir + "\\testDir/test.txt > testDir2/test2.csv > test3.cpp1",
                    null
                },
                {
                    currDir + "\\testDir\\test.txt > testDir2\\test2.csv #> testDir3",
                    new ExpectedData()
                    {
                        ExpInput = currDir + "\\testDir\\test.txt > testDir2\\test2.csv #> testDir3",
                        ExpDirectorPath = currDir,
                        ExpArgument = "\\testDir\\test.txt > testDir2\\test2.csv #> testDir3"
                    }
                },
                {
                    currDir + "\\testDir\\test.txt > testDir2/test2.csv > test3.cpp",
                    null
                },
                {
                    currDir + "\\testDir\\test.txt > testDir2\\\\\\test2.csv > test3.cpp",
                    new ExpectedData()
                    {
                        ExpInput = currDir + "\\testDir\\test.txt > testDir2\\\\\\test2.csv > test3.cpp",
                        ExpDirectorPath = currDir,
                        ExpArgument = "\\testDir\\test.txt > testDir2\\\\\\test2.csv > test3.cpp"
                    }
                },
                {
                    currDir + "\\testDir\\test.txt > testDir2\\test2.csv > test3.cpp",
                    new ExpectedData()
                    {
                        ExpInput = currDir + "\\testDir\\test.txt > testDir2\\test2.csv > test3.cpp",
                        ExpDirectorPath = currDir,
                        ExpArgument = "\\testDir\\test.txt > testDir2\\test2.csv > test3.cpp"
                    }
                },
                {
                    currDir.Replace("\\", "/") + "\\testDir\\test.txt > testDir2\\test2.csv > test3.cpp",
                    null
                },
                {
                    currDir.Replace("\\", "/") + "\\testDir/test.txt > testDir2\\test2.csv #> test3.cpp",
                    null
                },
                {
                    currDir.Replace("\\", "/") + "/testDir/test.txt > testDir2/test2.csv > test3.cpp2",
                    new ExpectedData()
                    {
                        ExpInput = currDir + "/testDir/test.txt > testDir2/test2.csv > test3.cpp2",
                        ExpDirectorPath = currDir,
                        ExpArgument = "/testDir/test.txt > testDir2/test2.csv > test3.cpp2"
                    }
                },
                {
                    currDir.Replace("\\", "/") + "/testDir/test/./txt > testDir2/test2.csv > test3.cpp",
                    new ExpectedData()
                    {
                        ExpInput = currDir + "/testDir/test/./txt > testDir2/test2.csv > test3.cpp",
                        ExpDirectorPath = currDir,
                        ExpArgument = "/testDir/test/./txt > testDir2/test2.csv > test3.cpp"
                    }
                },
                {
                    currDir.Replace("\\", "/") + "/testDir/test.txt > testDir2/test2.csv > test3.cpp3",
                    new ExpectedData()
                    {
                        ExpInput = currDir + "/testDir/test.txt > testDir2/test2.csv > test3.cpp3",
                        ExpDirectorPath = currDir,
                        ExpArgument = "/testDir/test.txt > testDir2/test2.csv > test3.cpp3"
                    }
                }
            };
        }
    }

    /// <summary>
    /// Represents expected data.
    /// </summary>
    public class ExpectedData
    {
        /// <summary>
        /// Gets or sets the expected argument.
        /// </summary>
        public string ExpArgument { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the expected path to director.
        /// </summary>
        public string ExpDirectorPath { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the expected input string.
        /// </summary>
        public string ExpInput { get; set; } = string.Empty;
    }
}