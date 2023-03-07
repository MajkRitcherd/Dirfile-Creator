﻿// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 07/03/2023     \\

using Dirfile_lib.Utilities.Validation;

namespace Dirfile_lib_TEST.UtilitiesTests.RegexTests
{
    /// <summary>
    /// Tests <see cref="ArgumentParser"/> class.
    /// </summary>
    [TestClass]
    public class ArgumentParserTests
    {
        /// <summary>
        /// Tests argument string using <see cref="ArgumentParser"/> class.
        /// </summary>
        [TestMethod]
        public void TestArgumentParser()
        {
            var data = this.PrepareTestData();

            foreach (var testData in data)
            {
                var valid = ArgumentParser.Instance.IsValid(testData.Key);
                Assert.AreEqual(testData.Value, valid, $"[CASE]: {testData.Key}");
            }
        }

        /// <summary>
        /// Prepares test data for test.
        /// </summary>
        /// <returns>Test data.</returns>
        private Dictionary<string, bool> PrepareTestData()
        {
            return new Dictionary<string, bool>()
            {
                // Valid examples
                { "\\testDir", true },
                { "\\testDir :> testDir2", true },
                { "\\testDir > testFile1.txt", true },
                { "\\testDir.ahoj > testDir2\\ testFile1.txt", true },
                { "\\testDir > testFile1.txt > testFile2.txt", true },
                { "\\testDir > testDir2 > testDir3\\ testFile1.txt > testFile2.txt :> testFile3.csv", true },
                { "\\testDir :> testFile1.avi > aa\\ testFile2.csv > testFile3.png :> testFile4.pdf", true },
                {
                    "\\testDir :> testFile1.avi > aa\\ testFile2.csv > testFile3.png :> testFile4.pdf > testDir2\\" +
                    " testDir3\\ testFile5.log > testFile6.cpp :> testFile7.c :> testFile8.java > testDir4\\ > testFile9.php > testFile10.jpeg" +
                    " :> testDir5 > testDir6 > testDir7\\ testFile11.pptx",
                    true
                },

                // Invalid examples
                { "\\", false },
                { "testDir", false },
                { "\\\\testDir", false },
                { "> testFile1.txt", false },
                { "\\> testFile1.txt", false },
                { "\\testDir >> testDir2", false },
                { "\\testDir ?> testFile.txt", false },
                { "\\testDir.ahoj > testDir2/ testFile1.txt", false },
                { "\\testDir :> testFile1.avi > aa\\ testFile2.csv > testFile3.png ?> testFile4.pdf", false },
                {
                    "\\testDir :> testFile1.avi > aa\\ testFile2.csv > testFile3.png :> testFile4.pdf > testDir2\\" +
                    " testDir3\\ testFile5.log > testFile6.cpp :> testFile7.c :> testFile8.java > testDir4\\ > testFile9.php > testFile10.jpeg" +
                    " :> testDir5 > testDir6 > testDir7\\ /testFile11.pptx",
                    false
                }
            };
        }
    }
}