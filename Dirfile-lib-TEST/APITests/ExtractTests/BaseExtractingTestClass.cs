// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 15/03/2023     \\

using CT = Dirfile_lib.Core.Constants.Texts;

namespace Dirfile_lib_TEST.APITests.ExtractTests
{
    public abstract class BaseExtractingTestClass
    {
        /// <summary>
        /// Test initializing.
        /// </summary>
        public abstract void Init();

        /// <summary>
        /// Test extracting.
        /// </summary>
        public abstract void TestExtracting();

        /// <summary>
        /// Prepares test data with expected data.
        /// </summary>
        /// <returns>Dictionary of test data (string with arguments) and expected data.</returns>
        protected static Dictionary<string, ArgumentExtractorExpectedData?> PrepareArgExtractorTestData()
        {
            return new Dictionary<string, ArgumentExtractorExpectedData?>()
            {
                // SlashMode.Backward
                {
                    "\\",
                    null
                },
                {
                    "\\testDir",
                    new ArgumentExtractorExpectedData()
                    {
                        OperationsInOrder = new List<string>() {},
                        ArgumentsInOrder = new List<KeyValuePair<string, string>>()
                        {
                            new KeyValuePair<string, string>(CT.Director, "testDir")
                        }
                    }
                },
                {
                    "\\testDir >",
                    null
                },
                {
                    "\\testDir > testDir2",
                    new ArgumentExtractorExpectedData()
                    {
                        OperationsInOrder = new List<string>() { ">" },
                        ArgumentsInOrder = new List<KeyValuePair<string, string>>()
                        {
                            new KeyValuePair<string, string>(CT.Director, "testDir"),
                            new KeyValuePair<string, string>(CT.Director, "testDir2"),
                        }
                    }
                },
                {
                    "\\testDir > testFile.txt",
                    new ArgumentExtractorExpectedData()
                    {
                        OperationsInOrder = new List<string>() { ">" },
                        ArgumentsInOrder = new List<KeyValuePair<string, string>>()
                        {
                            new KeyValuePair<string, string>(CT.Director, "testDir"),
                            new KeyValuePair<string, string>(CT.Filer, "testFile.txt"),
                        }
                    }
                },
                {
                    "\\testDir > testDir2 :> testDir3",
                    new ArgumentExtractorExpectedData()
                    {
                        OperationsInOrder = new List<string>() { ">", ":>" },
                        ArgumentsInOrder = new List<KeyValuePair<string, string>>()
                        {
                            new KeyValuePair<string, string>(CT.Director, "testDir"),
                            new KeyValuePair<string, string>(CT.Director, "testDir2"),
                            new KeyValuePair<string, string>(CT.Director, "testDir3"),
                        }
                    }
                },
                {
                    "\\testDir > testDir2 #> testDir3",
                    null
                },
                {
                    "\\test dir     >    testFile.csv",
                    new ArgumentExtractorExpectedData()
                    {
                        OperationsInOrder = new List<string>() { ">" },
                        ArgumentsInOrder = new List<KeyValuePair<string, string>>()
                        {
                            new KeyValuePair<string, string>(CT.Director, "test dir"),
                            new KeyValuePair<string, string>(CT.Filer, "testFile.csv"),
                        }
                    }
                },
                {
                    "\\testDir > testDir2\\testFile.csv :> testDir3",
                    new ArgumentExtractorExpectedData()
                    {
                        OperationsInOrder = new List<string>() { ">", "\\", ":>" },
                        ArgumentsInOrder = new List<KeyValuePair<string, string>>()
                        {
                            new KeyValuePair<string, string>(CT.Director, "testDir"),
                            new KeyValuePair<string, string>(CT.Director, "testDir2"),
                            new KeyValuePair<string, string>(CT.Filer, "testFile.csv"),
                            new KeyValuePair<string, string>(CT.Director, "testDir3"),
                        }
                    }
                },
                {
                    "\\testDir > testDir2/testFile.csv :> testDir3",
                    null
                },

                // SlashMode.Forward (Internally works with '\')
                {
                    "/",
                    null
                },
                {
                    "/testDir",
                    new ArgumentExtractorExpectedData()
                    {
                        OperationsInOrder = new List<string>() {},
                        ArgumentsInOrder = new List<KeyValuePair<string, string>>()
                        {
                            new KeyValuePair<string, string>(CT.Director, "testDir")
                        }
                    }
                },
                {
                    "/testDir >",
                    null
                },
                {
                    "/testDir > testDir2",
                    new ArgumentExtractorExpectedData()
                    {
                        OperationsInOrder = new List<string>() { ">" },
                        ArgumentsInOrder = new List<KeyValuePair<string, string>>()
                        {
                            new KeyValuePair<string, string>(CT.Director, "testDir"),
                            new KeyValuePair<string, string>(CT.Director, "testDir2"),
                        }
                    }
                },
                {
                    "/testDir > testFile.txt",
                    new ArgumentExtractorExpectedData()
                    {
                        OperationsInOrder = new List<string>() { ">" },
                        ArgumentsInOrder = new List<KeyValuePair<string, string>>()
                        {
                            new KeyValuePair<string, string>(CT.Director, "testDir"),
                            new KeyValuePair<string, string>(CT.Filer, "testFile.txt"),
                        }
                    }
                },
                {
                    "/testDir > testDir2 :> testDir3",
                    new ArgumentExtractorExpectedData()
                    {
                        OperationsInOrder = new List<string>() { ">", ":>" },
                        ArgumentsInOrder = new List<KeyValuePair<string, string>>()
                        {
                            new KeyValuePair<string, string>(CT.Director, "testDir"),
                            new KeyValuePair<string, string>(CT.Director, "testDir2"),
                            new KeyValuePair<string, string>(CT.Director, "testDir3"),
                        }
                    }
                },
                {
                    "/testDir > testDir2 #> testDir3",
                    null
                },
                {
                    "/test dir     >    testFile.csv",
                    new ArgumentExtractorExpectedData()
                    {
                        OperationsInOrder = new List<string>() { ">" },
                        ArgumentsInOrder = new List<KeyValuePair<string, string>>()
                        {
                            new KeyValuePair<string, string>(CT.Director, "test dir"),
                            new KeyValuePair<string, string>(CT.Filer, "testFile.csv"),
                        }
                    }
                },
                {
                    "/testDir > testDir2/testFile.csv :> testDir3",
                    new ArgumentExtractorExpectedData()
                    {
                        OperationsInOrder = new List<string>() { ">", "\\", ":>" },
                        ArgumentsInOrder = new List<KeyValuePair<string, string>>()
                        {
                            new KeyValuePair<string, string>(CT.Director, "testDir"),
                            new KeyValuePair<string, string>(CT.Director, "testDir2"),
                            new KeyValuePair<string, string>(CT.Filer, "testFile.csv"),
                            new KeyValuePair<string, string>(CT.Director, "testDir3"),
                        }
                    }
                },
                {
                    "/testDir > testDir2\\testFile.csv :> testDir3",
                    null
                }
            };
        }

        /// <summary>
        /// Prepares test data.
        /// </summary>
        /// <returns>Dictionary of test data (input string) and expected data.</returns>
        protected static Dictionary<string, ExtractorExpectedData?> PrepareExtractorTestData()
        {
            var currDir = Directory.GetCurrentDirectory();
            currDir = currDir[..currDir.LastIndexOf('\\')];
            var inputStrings = GetTestInputStrings();

            return new Dictionary<string, ExtractorExpectedData?>()
            {
                {
                    inputStrings.ElementAt(0),
                    new ExtractorExpectedData()
                    {
                        ExpInput = inputStrings.ElementAt(0),
                        ExpDirectorPath = currDir,
                        ExpArgument = string.Empty
                    }
                },
                {
                    inputStrings.ElementAt(1),
                    null
                },
                {
                    inputStrings.ElementAt(2),
                    new ExtractorExpectedData()
                    {
                        ExpInput = inputStrings.ElementAt(2),
                        ExpDirectorPath = currDir,
                        ExpArgument = "\\testDir\\test.txt > testDir2\\test2.csv #> testDir3"
                    }
                },
                {
                    inputStrings.ElementAt(3),
                    new ExtractorExpectedData()
                    {
                        ExpInput = inputStrings.ElementAt(3),
                        ExpDirectorPath = currDir,
                        ExpArgument = "\\testDir\\test.txt > testDir2\\test2.csv :> testDir3"
                    }
                },
                {
                    inputStrings.ElementAt(4),
                    null
                },
                {
                    inputStrings.ElementAt(5),
                    new ExtractorExpectedData()
                    {
                        ExpInput = inputStrings.ElementAt(5),
                        ExpDirectorPath = currDir,
                        ExpArgument = "\\testDir\\test.txt > testDir2\\\\\\test2.csv > test3.cpp"
                    }
                },
                {
                    inputStrings.ElementAt(6),
                    new ExtractorExpectedData()
                    {
                        ExpInput = inputStrings.ElementAt(6),
                        ExpDirectorPath = currDir,
                        ExpArgument = "\\testDir\\test.txt > testDir2\\test2.csv > test3.cpp"
                    }
                },
                {
                    inputStrings.ElementAt(7),
                    null
                },
                {
                    inputStrings.ElementAt(8),
                    null
                },
                {
                    inputStrings.ElementAt(9),
                    new ExtractorExpectedData()
                    {
                        ExpInput = inputStrings.ElementAt(9),
                        ExpDirectorPath = currDir,
                        ExpArgument = "/testDir/test.txt > testDir2/test2.csv > test3.cpp2"
                    }
                },
                {
                    inputStrings.ElementAt(10),
                    new ExtractorExpectedData()
                    {
                        ExpInput = inputStrings.ElementAt(10),
                        ExpDirectorPath = currDir,
                        ExpArgument = "/testDir/test/./txt > testDir2/test2.csv > test3.cpp"
                    }
                },
                {
                    inputStrings.ElementAt(11),
                    new ExtractorExpectedData()
                    {
                        ExpInput = inputStrings.ElementAt(11),
                        ExpDirectorPath = currDir,
                        ExpArgument = "/testDir/test.txt > testDir2/test2.csv > test3.cpp3"
                    }
                },
                {
                    inputStrings.ElementAt(12),
                    new ExtractorExpectedData()
                    {
                        ExpInput = inputStrings.ElementAt(12),
                        ExpDirectorPath = currDir,
                        ExpArgument = "        > "
                    }
                }
            };
        }

        /// <summary>
        /// Prepares test data with expected data.
        /// </summary>
        /// <returns>Dictionary of test data (input string) and expected data.</returns>
        protected static Dictionary<string, ExpectedData?> PrepareTestData()
        {
            var currDir = Directory.GetCurrentDirectory();
            currDir = currDir[..currDir.LastIndexOf('\\')];
            var inputStrings = GetTestInputStrings();

            return new Dictionary<string, ExpectedData?>()
            {
                {
                    inputStrings.ElementAt(0), // currDir
                    new ExpectedData()
                    {
                        ArgExpData = null,
                        ExtExpData = new ExtractorExpectedData()
                        {
                            ExpInput = inputStrings.ElementAt(0),
                            ExpDirectorPath = inputStrings.ElementAt(0),
                            ExpArgument = string.Empty
                        }
                    }
                },
                {
                    inputStrings.ElementAt(1), // currDir + "\\testDir/test.txt > testDir2/test2.csv > test3.cpp1"
                    new ExpectedData()
                    {
                        ArgExpData = null,
                        ExtExpData = null
                    }
                },
                {
                    inputStrings.ElementAt(2), // currDir + "\\testDir\\test.txt > testDir2\\test2.csv #> testDir3"
                    new ExpectedData()
                    {
                        ArgExpData = new ArgumentExtractorExpectedData()
                        {
                            ArgumentsInOrder = new List<KeyValuePair<string, string>>()
                            {
                                new KeyValuePair<string, string>(CT.Director, "testDir"),
                                new KeyValuePair<string, string>(CT.Filer, "test.txt"),
                                new KeyValuePair<string, string>(CT.Director, "testDir2"),
                                new KeyValuePair<string, string>(CT.Director, "test2.csv #"),
                                new KeyValuePair<string, string>(CT.Director, "testDir3")
                            },
                            OperationsInOrder = new List<string>() { "\\", ">", "\\", ">" }
                        },
                        ExtExpData = new ExtractorExpectedData()
                        {
                            ExpInput = inputStrings.ElementAt(2),
                            ExpDirectorPath = currDir,
                            ExpArgument = "\\testDir\\test.txt > testDir2\\test2.csv #> testDir3"
                        }
                    }
                },
                {
                    inputStrings.ElementAt(3), // currDir + "\\testDir\\test.txt > testDir2\\test2.csv :> testDir3"
                    new ExpectedData()
                    {
                        ArgExpData = new ArgumentExtractorExpectedData()
                        {
                            ArgumentsInOrder = new List<KeyValuePair<string, string>>()
                            {
                                new KeyValuePair<string, string>(CT.Director, "testDir"),
                                new KeyValuePair<string, string>(CT.Filer, "test.txt"),
                                new KeyValuePair<string, string>(CT.Director, "testDir2"),
                                new KeyValuePair<string, string>(CT.Filer, "test2.csv"),
                                new KeyValuePair<string, string>(CT.Director, "testDir3")
                            },
                            OperationsInOrder = new List<string>() { "\\", ">", "\\", ":>" }
                        },
                        ExtExpData = new ExtractorExpectedData()
                        {
                            ExpInput = inputStrings.ElementAt(3),
                            ExpDirectorPath = currDir,
                            ExpArgument = "\\testDir\\test.txt > testDir2\\test2.csv :> testDir3"
                        }
                    }
                },
                {
                    inputStrings.ElementAt(4), // currDir + "\\testDir\\test.txt > testDir2/test2.csv > test3.cpp"
                    new ExpectedData()
                    {
                        ArgExpData = null,
                        ExtExpData = null
                    }
                },
                {
                    inputStrings.ElementAt(5), // currDir + "\\testDir\\test.txt > testDir2\\\\\\test2.csv > test3.cpp"
                    new ExpectedData()
                    {
                        ArgExpData = null,
                        ExtExpData = new ExtractorExpectedData()
                        {
                            ExpInput = inputStrings.ElementAt(5),
                            ExpDirectorPath = currDir,
                            ExpArgument = "\\testDir\\test.txt > testDir2\\\\\\test2.csv > test3.cpp"
                        }
                    }
                },
                {
                    inputStrings.ElementAt(6), // currDir + "\\testDir\\test.txt > testDir2\\test2.csv > test3.cpp"
                    new ExpectedData()
                    {
                        ArgExpData = new ArgumentExtractorExpectedData()
                        {
                            ArgumentsInOrder = new List<KeyValuePair<string, string>>()
                            {
                                new KeyValuePair<string, string>(CT.Director, "testDir"),
                                new KeyValuePair<string, string>(CT.Filer, "test.txt"),
                                new KeyValuePair<string, string>(CT.Director, "testDir2"),
                                new KeyValuePair<string, string>(CT.Filer, "test2.csv"),
                                new KeyValuePair<string, string>(CT.Filer, "test3.cpp")
                            },
                            OperationsInOrder = new List<string>() { "\\", ">", "\\", ">" }
                        },
                        ExtExpData = new ExtractorExpectedData()
                        {
                            ExpInput = inputStrings.ElementAt(6),
                            ExpDirectorPath = currDir,
                            ExpArgument = "\\testDir\\test.txt > testDir2\\test2.csv > test3.cpp"
                        }
                    }
                },
                {
                    inputStrings.ElementAt(7), // currDir.Replace("\\", "/") + "\\testDir\\test.txt > testDir2\\test2.csv > test3.cpp"
                    new ExpectedData()
                    {
                        ArgExpData = null,
                        ExtExpData = null
                    }
                },
                {
                    inputStrings.ElementAt(8), // currDir.Replace("\\", "/") + "\\testDir/test.txt > testDir2\\test2.csv #> test3.cpp"
                    new ExpectedData()
                    {
                        ArgExpData = null,
                        ExtExpData = null
                    }
                },
                {
                    inputStrings.ElementAt(9), // currDir.Replace("\\", "/") + "/testDir/test.txt > testDir2/test2.csv > test3.cpp2"
                    new ExpectedData()
                    {
                        ArgExpData = null,
                        ExtExpData = new ExtractorExpectedData()
                        {
                            ExpInput = inputStrings.ElementAt(9),
                            ExpDirectorPath = currDir,
                            ExpArgument = "/testDir/test.txt > testDir2/test2.csv > test3.cpp2"
                        }
                    }
                },
                {
                    inputStrings.ElementAt(10), // currDir.Replace("\\", "/") + "/testDir/test/./txt > testDir2/test2.csv > test3.cpp"
                    new ExpectedData()
                    {
                        ArgExpData = new ArgumentExtractorExpectedData()
                        {
                            ArgumentsInOrder = new List<KeyValuePair<string, string>>()
                            {
                                new KeyValuePair<string, string>(CT.Director, "testDir"),
                                new KeyValuePair<string, string>(CT.Director, "test"),
                                new KeyValuePair<string, string>(CT.Director, "."),
                                new KeyValuePair<string, string>(CT.Director, "txt"),
                                new KeyValuePair<string, string>(CT.Director, "testDir2"),
                                new KeyValuePair<string, string>(CT.Filer, "test2.csv"),
                                new KeyValuePair<string, string>(CT.Filer, "test3.cpp")
                            },
                            OperationsInOrder = new List<string>() { "\\", ">", "\\", ">" }
                        },
                        ExtExpData = new ExtractorExpectedData()
                        {
                            ExpInput = inputStrings.ElementAt(10),
                            ExpDirectorPath = currDir,
                            ExpArgument = "/testDir/test/./txt > testDir2/test2.csv > test3.cpp"
                        }
                    }
                },
                {
                    inputStrings.ElementAt(11), // currDir.Replace("\\", "/") + "/testDir/test.txt > testDir2/test2.csv :> test3.cpp"
                    new ExpectedData()
                    {
                        ArgExpData = new ArgumentExtractorExpectedData()
                        {
                            ArgumentsInOrder = new List<KeyValuePair<string, string>>()
                            {
                                new KeyValuePair<string, string>(CT.Director, "testDir"),
                                new KeyValuePair<string, string>(CT.Filer, "test.txt"),
                                new KeyValuePair<string, string>(CT.Director, "testDir2"),
                                new KeyValuePair<string, string>(CT.Filer, "test2.csv"),
                                new KeyValuePair<string, string>(CT.Filer, "test3.cpp")
                            },
                            OperationsInOrder = new List<string>() { "\\", ">", "\\", ":>" }
                        },
                        ExtExpData = new ExtractorExpectedData()
                        {
                            ExpInput = inputStrings.ElementAt(11),
                            ExpDirectorPath = currDir,
                            ExpArgument = "/testDir/test.txt > testDir2/test2.csv :> test3.cpp3"
                        }
                    }
                },
                {
                    inputStrings.ElementAt(12), // currDir.Replace("\\", "/") + "        > "
                    new ExpectedData()
                    {
                        ArgExpData = null,
                        ExtExpData = new ExtractorExpectedData()
                        {
                            ExpInput = inputStrings.ElementAt(12),
                            ExpDirectorPath = currDir,
                            ExpArgument = "        > "
                        }
                    }
                }
            };
        }

        /// <summary>
        /// Gets test input strings.
        /// </summary>
        /// <returns></returns>
        private static List<string> GetTestInputStrings()
        {
            var currDir = Directory.GetCurrentDirectory();
            currDir = currDir[..currDir.LastIndexOf('\\')];

            return new List<string>()
            {
                currDir,
                currDir + "\\testDir/test.txt > testDir2/test2.csv > test3.cpp1",
                currDir + "\\testDir\\test.txt > testDir2\\test2.csv #> testDir3",
                currDir + "\\testDir\\test.txt > testDir2\\test2.csv :> testDir3",
                currDir + "\\testDir\\test.txt > testDir2/test2.csv > test3.cpp",
                currDir + "\\testDir\\test.txt > testDir2\\\\\\test2.csv > test3.cpp",
                currDir + "\\testDir\\test.txt > testDir2\\test2.csv > test3.cpp",
                currDir.Replace("\\", "/") + "\\testDir\\test.txt > testDir2\\test2.csv > test3.cpp",
                currDir.Replace("\\", "/") + "\\testDir/test.txt > testDir2\\test2.csv #> test3.cpp",
                currDir.Replace("\\", "/") + "/testDir/test.txt > testDir2/test2.csv > test3.cpp2",
                currDir.Replace("\\", "/") + "/testDir/test/./txt > testDir2/test2.csv > test3.cpp",
                currDir.Replace("\\", "/") + "/testDir/test.txt > testDir2/test2.csv > test3.cpp3",
                currDir.Replace("\\", "/") + "        > "
            };
        }

        /// <summary>
        /// Represents expected data for argument extractor.
        /// </summary>
        protected class ArgumentExtractorExpectedData
        {
            /// <summary>
            /// Gets or sets list which holds arguments in order.
            /// </summary>
            public List<KeyValuePair<string, string>> ArgumentsInOrder = new();

            /// <summary>
            /// Gets or sets list which holds operations in order.
            /// </summary>
            public List<string> OperationsInOrder = new();
        }

        /// <summary>
        /// Represents expected data.
        /// </summary>
        protected class ExpectedData
        {
            /// <summary>
            /// Gets or sets <see cref="ArgumentExtractorExpectedData"/> class.
            /// </summary>
            public ArgumentExtractorExpectedData? ArgExpData { get; set; }

            /// <summary>
            /// Gets or sets <see cref="ExtractorExpectedData"/> class.
            /// </summary>
            public ExtractorExpectedData? ExtExpData { get; set; }
        }

        /// <summary>
        /// Represents expected data for extractor.
        /// </summary>
        protected class ExtractorExpectedData
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
}