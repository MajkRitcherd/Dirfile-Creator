// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 26/04/2023     \\

using Chars = Dirfile_lib.Core.Constants.DirFile.Characters;
using DirfileOperations = Dirfile_lib.Core.Constants.DirFile.Operations;
using DirfileTypes = Dirfile_lib.Core.Constants.DirFile.Types;

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
        protected static Dictionary<string, ArgumentExtractorExpectedData?> GetArgumentExtractorTestData()
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
                        ArgumentsByTypeInOrder = new List<KeyValuePair<string, string>>()
                        {
                            new KeyValuePair<string, string>(DirfileTypes.Director, "testDir")
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
                        OperationsInOrder = new List<string>() { DirfileOperations.Next },
                        ArgumentsByTypeInOrder = new List<KeyValuePair<string, string>>()
                        {
                            new KeyValuePair<string, string>(DirfileTypes.Director, "testDir"),
                            new KeyValuePair<string, string>(DirfileTypes.Director, "testDir2"),
                        }
                    }
                },
                {
                    "\\testDir > testFile.txt",
                    new ArgumentExtractorExpectedData()
                    {
                        OperationsInOrder = new List<string>() { DirfileOperations.Next },
                        ArgumentsByTypeInOrder = new List<KeyValuePair<string, string>>()
                        {
                            new KeyValuePair<string, string>(DirfileTypes.Director, "testDir"),
                            new KeyValuePair<string, string>(DirfileTypes.Filer, "testFile.txt"),
                        }
                    }
                },
                {
                    "\\testDir > testDir2 :> testDir3",
                    new ArgumentExtractorExpectedData()
                    {
                        OperationsInOrder = new List<string>() { DirfileOperations.Next, DirfileOperations.Prev },
                        ArgumentsByTypeInOrder = new List<KeyValuePair<string, string>>()
                        {
                            new KeyValuePair<string, string>(DirfileTypes.Director, "testDir"),
                            new KeyValuePair<string, string>(DirfileTypes.Director, "testDir2"),
                            new KeyValuePair<string, string>(DirfileTypes.Director, "testDir3"),
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
                        OperationsInOrder = new List<string>() { DirfileOperations.Next },
                        ArgumentsByTypeInOrder = new List<KeyValuePair<string, string>>()
                        {
                            new KeyValuePair<string, string>(DirfileTypes.Director, "test dir"),
                            new KeyValuePair<string, string>(DirfileTypes.Filer, "testFile.csv"),
                        }
                    }
                },
                {
                    "\\testDir > testDir2\\testFile.csv :> testDir3",
                    new ArgumentExtractorExpectedData()
                    {
                        OperationsInOrder = new List<string>() { DirfileOperations.Next, DirfileOperations.Change, DirfileOperations.Prev },
                        ArgumentsByTypeInOrder = new List<KeyValuePair<string, string>>()
                        {
                            new KeyValuePair<string, string>(DirfileTypes.Director, "testDir"),
                            new KeyValuePair<string, string>(DirfileTypes.Director, "testDir2"),
                            new KeyValuePair<string, string>(DirfileTypes.Filer, "testFile.csv"),
                            new KeyValuePair<string, string>(DirfileTypes.Director, "testDir3"),
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
                        ArgumentsByTypeInOrder = new List<KeyValuePair<string, string>>()
                        {
                            new KeyValuePair<string, string>(DirfileTypes.Director, "testDir")
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
                        OperationsInOrder = new List<string>() { DirfileOperations.Next },
                        ArgumentsByTypeInOrder = new List<KeyValuePair<string, string>>()
                        {
                            new KeyValuePair<string, string>(DirfileTypes.Director, "testDir"),
                            new KeyValuePair<string, string>(DirfileTypes.Director, "testDir2"),
                        }
                    }
                },
                {
                    "/testDir > testFile.txt",
                    new ArgumentExtractorExpectedData()
                    {
                        OperationsInOrder = new List<string>() { DirfileOperations.Next },
                        ArgumentsByTypeInOrder = new List<KeyValuePair<string, string>>()
                        {
                            new KeyValuePair<string, string>(DirfileTypes.Director, "testDir"),
                            new KeyValuePair<string, string>(DirfileTypes.Filer, "testFile.txt"),
                        }
                    }
                },
                {
                    "/testDir > testDir2 :> testDir3",
                    new ArgumentExtractorExpectedData()
                    {
                        OperationsInOrder = new List<string>() { DirfileOperations.Next, DirfileOperations.Prev },
                        ArgumentsByTypeInOrder = new List<KeyValuePair<string, string>>()
                        {
                            new KeyValuePair<string, string>(DirfileTypes.Director, "testDir"),
                            new KeyValuePair<string, string>(DirfileTypes.Director, "testDir2"),
                            new KeyValuePair<string, string>(DirfileTypes.Director, "testDir3"),
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
                        OperationsInOrder = new List<string>() { DirfileOperations.Next },
                        ArgumentsByTypeInOrder = new List<KeyValuePair<string, string>>()
                        {
                            new KeyValuePair<string, string>(DirfileTypes.Director, "test dir"),
                            new KeyValuePair<string, string>(DirfileTypes.Filer, "testFile.csv"),
                        }
                    }
                },
                {
                    "/testDir > testDir2/testFile.csv :> testDir3",
                    new ArgumentExtractorExpectedData()
                    {
                        OperationsInOrder = new List<string>() { DirfileOperations.Next, DirfileOperations.Change, DirfileOperations.Prev },
                        ArgumentsByTypeInOrder = new List<KeyValuePair<string, string>>()
                        {
                            new KeyValuePair<string, string>(DirfileTypes.Director, "testDir"),
                            new KeyValuePair<string, string>(DirfileTypes.Director, "testDir2"),
                            new KeyValuePair<string, string>(DirfileTypes.Filer, "testFile.csv"),
                            new KeyValuePair<string, string>(DirfileTypes.Director, "testDir3"),
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
        protected static Dictionary<string, ExtractorExpectedData?> GetExtractorTestData()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            currentDirectory = currentDirectory[..currentDirectory.LastIndexOf(Chars.BSlash)];
            var testStrings = GetTestStrings();

            return new Dictionary<string, ExtractorExpectedData?>()
            {
                {
                    testStrings.ElementAt(0),
                    new ExtractorExpectedData()
                    {
                        ExpectedInput = testStrings.ElementAt(0),
                        ExpectedDirectorPath = currentDirectory,
                        ExpectedArgument = string.Empty
                    }
                },
                {
                    testStrings.ElementAt(1),
                    null
                },
                {
                    testStrings.ElementAt(2),
                    new ExtractorExpectedData()
                    {
                        ExpectedInput = testStrings.ElementAt(2),
                        ExpectedDirectorPath = currentDirectory,
                        ExpectedArgument = "\\testDir\\test.txt > testDir2\\test2.csv #> testDir3"
                    }
                },
                {
                    testStrings.ElementAt(3),
                    new ExtractorExpectedData()
                    {
                        ExpectedInput = testStrings.ElementAt(3),
                        ExpectedDirectorPath = currentDirectory,
                        ExpectedArgument = "\\testDir\\test.txt > testDir2\\test2.csv :> testDir3"
                    }
                },
                {
                    testStrings.ElementAt(4),
                    null
                },
                {
                    testStrings.ElementAt(5),
                    new ExtractorExpectedData()
                    {
                        ExpectedInput = testStrings.ElementAt(5),
                        ExpectedDirectorPath = currentDirectory,
                        ExpectedArgument = "\\testDir\\test.txt > testDir2\\\\\\test2.csv > test3.cpp"
                    }
                },
                {
                    testStrings.ElementAt(6),
                    new ExtractorExpectedData()
                    {
                        ExpectedInput = testStrings.ElementAt(6),
                        ExpectedDirectorPath = currentDirectory,
                        ExpectedArgument = "\\testDir\\test.txt > testDir2\\test2.csv > test3.cpp"
                    }
                },
                {
                    testStrings.ElementAt(7),
                    null
                },
                {
                    testStrings.ElementAt(8),
                    null
                },
                {
                    testStrings.ElementAt(9),
                    new ExtractorExpectedData()
                    {
                        ExpectedInput = testStrings.ElementAt(9),
                        ExpectedDirectorPath = currentDirectory,
                        ExpectedArgument = "/testDir/test.txt > testDir2/test2.csv > test3.cpp2"
                    }
                },
                {
                    testStrings.ElementAt(10),
                    new ExtractorExpectedData()
                    {
                        ExpectedInput = testStrings.ElementAt(10),
                        ExpectedDirectorPath = currentDirectory,
                        ExpectedArgument = "/testDir/test/./txt > testDir2/test2.csv > test3.cpp"
                    }
                },
                {
                    testStrings.ElementAt(11),
                    new ExtractorExpectedData()
                    {
                        ExpectedInput = testStrings.ElementAt(11),
                        ExpectedDirectorPath = currentDirectory,
                        ExpectedArgument = "/testDir/test.txt > testDir2/test2.csv > test3.cpp3"
                    }
                },
                {
                    testStrings.ElementAt(12),
                    new ExtractorExpectedData()
                    {
                        ExpectedInput = testStrings.ElementAt(12),
                        ExpectedDirectorPath = currentDirectory,
                        ExpectedArgument = "        > "
                    }
                }
            };
        }

        /// <summary>
        /// Prepares test data with expected data.
        /// </summary>
        /// <returns>Dictionary of test data (input string) and expected data.</returns>
        protected static Dictionary<string, ExpectedData?> GetExtractingTestData()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            currentDirectory = currentDirectory[..currentDirectory.LastIndexOf(Chars.BSlash)];
            var testStrings = GetTestStrings();

            return new Dictionary<string, ExpectedData?>()
            {
                {
                    testStrings.ElementAt(0), // currDir
                    new ExpectedData()
                    {
                        ArgumentExpData = null,
                        ExtractorExpData = new ExtractorExpectedData()
                        {
                            ExpectedInput = testStrings.ElementAt(0),
                            ExpectedDirectorPath = testStrings.ElementAt(0),
                            ExpectedArgument = string.Empty
                        }
                    }
                },
                {
                    testStrings.ElementAt(1), // currDir + "\\testDir/test.txt > testDir2/test2.csv > test3.cpp1"
                    new ExpectedData()
                    {
                        ArgumentExpData = null,
                        ExtractorExpData = null
                    }
                },
                {
                    testStrings.ElementAt(2), // currDir + "\\testDir\\test.txt > testDir2\\test2.csv #> testDir3"
                    new ExpectedData()
                    {
                        ArgumentExpData = new ArgumentExtractorExpectedData()
                        {
                            ArgumentsByTypeInOrder = new List<KeyValuePair<string, string>>()
                            {
                                new KeyValuePair<string, string>(DirfileTypes.Director, "testDir"),
                                new KeyValuePair<string, string>(DirfileTypes.Filer, "test.txt"),
                                new KeyValuePair<string, string>(DirfileTypes.Director, "testDir2"),
                                new KeyValuePair<string, string>(DirfileTypes.Director, "test2.csv #"),
                                new KeyValuePair<string, string>(DirfileTypes.Director, "testDir3")
                            },
                            OperationsInOrder = new List<string>() { DirfileOperations.Change, DirfileOperations.Next, DirfileOperations.Change, DirfileOperations.Next }
                        },
                        ExtractorExpData = new ExtractorExpectedData()
                        {
                            ExpectedInput = testStrings.ElementAt(2),
                            ExpectedDirectorPath = currentDirectory,
                            ExpectedArgument = "\\testDir\\test.txt > testDir2\\test2.csv #> testDir3"
                        }
                    }
                },
                {
                    testStrings.ElementAt(3), // currDir + "\\testDir\\test.txt > testDir2\\test2.csv :> testDir3"
                    new ExpectedData()
                    {
                        ArgumentExpData = new ArgumentExtractorExpectedData()
                        {
                            ArgumentsByTypeInOrder = new List<KeyValuePair<string, string>>()
                            {
                                new KeyValuePair<string, string>(DirfileTypes.Director, "testDir"),
                                new KeyValuePair<string, string>(DirfileTypes.Filer, "test.txt"),
                                new KeyValuePair<string, string>(DirfileTypes.Director, "testDir2"),
                                new KeyValuePair<string, string>(DirfileTypes.Filer, "test2.csv"),
                                new KeyValuePair<string, string>(DirfileTypes.Director, "testDir3")
                            },
                            OperationsInOrder = new List<string>() { DirfileOperations.Change, DirfileOperations.Next, DirfileOperations.Change, DirfileOperations.Prev }
                        },
                        ExtractorExpData = new ExtractorExpectedData()
                        {
                            ExpectedInput = testStrings.ElementAt(3),
                            ExpectedDirectorPath = currentDirectory,
                            ExpectedArgument = "\\testDir\\test.txt > testDir2\\test2.csv :> testDir3"
                        }
                    }
                },
                {
                    testStrings.ElementAt(4), // currDir + "\\testDir\\test.txt > testDir2/test2.csv > test3.cpp"
                    new ExpectedData()
                    {
                        ArgumentExpData = null,
                        ExtractorExpData = null
                    }
                },
                {
                    testStrings.ElementAt(5), // currDir + "\\testDir\\test.txt > testDir2\\\\\\test2.csv > test3.cpp"
                    new ExpectedData()
                    {
                        ArgumentExpData = null,
                        ExtractorExpData = new ExtractorExpectedData()
                        {
                            ExpectedInput = testStrings.ElementAt(5),
                            ExpectedDirectorPath = currentDirectory,
                            ExpectedArgument = "\\testDir\\test.txt > testDir2\\\\\\test2.csv > test3.cpp"
                        }
                    }
                },
                {
                    testStrings.ElementAt(6), // currDir + "\\testDir\\test.txt > testDir2\\test2.csv > test3.cpp"
                    new ExpectedData()
                    {
                        ArgumentExpData = new ArgumentExtractorExpectedData()
                        {
                            ArgumentsByTypeInOrder = new List<KeyValuePair<string, string>>()
                            {
                                new KeyValuePair<string, string>(DirfileTypes.Director, "testDir"),
                                new KeyValuePair<string, string>(DirfileTypes.Filer, "test.txt"),
                                new KeyValuePair<string, string>(DirfileTypes.Director, "testDir2"),
                                new KeyValuePair<string, string>(DirfileTypes.Filer, "test2.csv"),
                                new KeyValuePair<string, string>(DirfileTypes.Filer, "test3.cpp")
                            },
                            OperationsInOrder = new List<string>() { DirfileOperations.Change, DirfileOperations.Next, DirfileOperations.Change, DirfileOperations.Next }
                        },
                        ExtractorExpData = new ExtractorExpectedData()
                        {
                            ExpectedInput = testStrings.ElementAt(6),
                            ExpectedDirectorPath = currentDirectory,
                            ExpectedArgument = "\\testDir\\test.txt > testDir2\\test2.csv > test3.cpp"
                        }
                    }
                },
                {
                    testStrings.ElementAt(7), // currDir.Replace("\\", "/") + "\\testDir\\test.txt > testDir2\\test2.csv > test3.cpp"
                    new ExpectedData()
                    {
                        ArgumentExpData = null,
                        ExtractorExpData = null
                    }
                },
                {
                    testStrings.ElementAt(8), // currDir.Replace("\\", "/") + "\\testDir/test.txt > testDir2\\test2.csv #> test3.cpp"
                    new ExpectedData()
                    {
                        ArgumentExpData = null,
                        ExtractorExpData = null
                    }
                },
                {
                    testStrings.ElementAt(9), // currDir.Replace("\\", "/") + "/testDir/test.txt > testDir2/test2.csv > test3.cpp2"
                    new ExpectedData()
                    {
                        ArgumentExpData = null,
                        ExtractorExpData = new ExtractorExpectedData()
                        {
                            ExpectedInput = testStrings.ElementAt(9),
                            ExpectedDirectorPath = currentDirectory,
                            ExpectedArgument = "/testDir/test.txt > testDir2/test2.csv > test3.cpp2"
                        }
                    }
                },
                {
                    testStrings.ElementAt(10), // currDir.Replace("\\", "/") + "/testDir/test/./txt > testDir2/test2.csv > test3.cpp"
                    new ExpectedData()
                    {
                        ArgumentExpData = new ArgumentExtractorExpectedData()
                        {
                            ArgumentsByTypeInOrder = new List<KeyValuePair<string, string>>()
                            {
                                new KeyValuePair<string, string>(DirfileTypes.Director, "testDir"),
                                new KeyValuePair<string, string>(DirfileTypes.Director, "test"),
                                new KeyValuePair<string, string>(DirfileTypes.Director, "."),
                                new KeyValuePair<string, string>(DirfileTypes.Director, "txt"),
                                new KeyValuePair<string, string>(DirfileTypes.Director, "testDir2"),
                                new KeyValuePair<string, string>(DirfileTypes.Filer, "test2.csv"),
                                new KeyValuePair<string, string>(DirfileTypes.Filer, "test3.cpp")
                            },
                            OperationsInOrder = new List<string>() { DirfileOperations.Change, DirfileOperations.Next, DirfileOperations.Change, DirfileOperations.Next }
                        },
                        ExtractorExpData = new ExtractorExpectedData()
                        {
                            ExpectedInput = testStrings.ElementAt(10),
                            ExpectedDirectorPath = currentDirectory,
                            ExpectedArgument = "/testDir/test/./txt > testDir2/test2.csv > test3.cpp"
                        }
                    }
                },
                {
                    testStrings.ElementAt(11), // currDir.Replace("\\", "/") + "/testDir/test.txt > testDir2/test2.csv :> test3.cpp"
                    new ExpectedData()
                    {
                        ArgumentExpData = new ArgumentExtractorExpectedData()
                        {
                            ArgumentsByTypeInOrder = new List<KeyValuePair<string, string>>()
                            {
                                new KeyValuePair<string, string>(DirfileTypes.Director, "testDir"),
                                new KeyValuePair<string, string>(DirfileTypes.Filer, "test.txt"),
                                new KeyValuePair<string, string>(DirfileTypes.Director, "testDir2"),
                                new KeyValuePair<string, string>(DirfileTypes.Filer, "test2.csv"),
                                new KeyValuePair<string, string>(DirfileTypes.Filer, "test3.cpp")
                            },
                            OperationsInOrder = new List<string>() { DirfileOperations.Change, DirfileOperations.Next, DirfileOperations.Change, DirfileOperations.Prev }
                        },
                        ExtractorExpData = new ExtractorExpectedData()
                        {
                            ExpectedInput = testStrings.ElementAt(11),
                            ExpectedDirectorPath = currentDirectory,
                            ExpectedArgument = "/testDir/test.txt > testDir2/test2.csv :> test3.cpp3"
                        }
                    }
                },
                {
                    testStrings.ElementAt(12), // currDir.Replace("\\", "/") + "        > "
                    new ExpectedData()
                    {
                        ArgumentExpData = null,
                        ExtractorExpData = new ExtractorExpectedData()
                        {
                            ExpectedInput = testStrings.ElementAt(12),
                            ExpectedDirectorPath = currentDirectory,
                            ExpectedArgument = "        > "
                        }
                    }
                }
            };
        }

        /// <summary>
        /// Gets test input strings.
        /// </summary>
        /// <returns></returns>
        private static List<string> GetTestStrings()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            currentDirectory = currentDirectory[..currentDirectory.LastIndexOf(Chars.BSlash)];

            return new List<string>()
            {
                currentDirectory,
                currentDirectory + "\\testDir/test.txt > testDir2/test2.csv > test3.cpp1",
                currentDirectory + "\\testDir\\test.txt > testDir2\\test2.csv #> testDir3",
                currentDirectory + "\\testDir\\test.txt > testDir2\\test2.csv :> testDir3",
                currentDirectory + "\\testDir\\test.txt > testDir2/test2.csv > test3.cpp",
                currentDirectory + "\\testDir\\test.txt > testDir2\\\\\\test2.csv > test3.cpp",
                currentDirectory + "\\testDir\\test.txt > testDir2\\test2.csv > test3.cpp",
                currentDirectory.Replace("\\", "/") + "\\testDir\\test.txt > testDir2\\test2.csv > test3.cpp",
                currentDirectory.Replace("\\", "/") + "\\testDir/test.txt > testDir2\\test2.csv #> test3.cpp",
                currentDirectory.Replace("\\", "/") + "/testDir/test.txt > testDir2/test2.csv > test3.cpp2",
                currentDirectory.Replace("\\", "/") + "/testDir/test/./txt > testDir2/test2.csv > test3.cpp",
                currentDirectory.Replace("\\", "/") + "/testDir/test.txt > testDir2/test2.csv > test3.cpp3",
                currentDirectory.Replace("\\", "/") + "        > "
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
            public List<KeyValuePair<string, string>> ArgumentsByTypeInOrder = new();

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
            public ArgumentExtractorExpectedData? ArgumentExpData { get; set; }

            /// <summary>
            /// Gets or sets <see cref="ExtractorExpectedData"/> class.
            /// </summary>
            public ExtractorExpectedData? ExtractorExpData { get; set; }
        }

        /// <summary>
        /// Represents expected data for extractor.
        /// </summary>
        protected class ExtractorExpectedData
        {
            /// <summary>
            /// Gets or sets the expected argument.
            /// </summary>
            public string ExpectedArgument { get; set; } = string.Empty;

            /// <summary>
            /// Gets or sets the expected path to director.
            /// </summary>
            public string ExpectedDirectorPath { get; set; } = string.Empty;

            /// <summary>
            /// Gets or sets the expected input string.
            /// </summary>
            public string ExpectedInput { get; set; } = string.Empty;
        }
    }
}