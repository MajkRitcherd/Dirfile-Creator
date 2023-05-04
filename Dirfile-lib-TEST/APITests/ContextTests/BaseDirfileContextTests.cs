// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 26/04/2023     \\

using Dirfile_lib.API.Context;
using Chars = Dirfile_lib.Core.Constants.DirFile.Characters;

namespace Dirfile_lib_TEST.APITests.ContextTests
{
    /// <summary>
    /// Tests <see cref="BaseDirfileContext"/> class through <see cref="DirfileContext"/>.
    /// </summary>
    [TestClass]
    public class BaseDirfileContextTests
    {
        /// <summary>
        /// Tests <see cref="BaseDirfileContext"/> class through <see cref="DirfileContext"/>.
        /// </summary>
        [TestMethod]
        public void TestBaseDirfileContext()
        {
            try
            {
                using (var baseContext = new DirfileContext(Directory.GetCurrentDirectory()))
                {
                    foreach (var testData in GetTestData().Select((testString, Index) => new { testString, Index }))
                    {
                        if (string.IsNullOrEmpty(testData.testString) && testData.Index != 0)
                            baseContext.SwitchSlashMode();

                        // Director creation
                        if (testData.Index % 3 == 1 && testData.testString.Any(x => x == Chars.BSlash || x == Chars.FSlash))
                            baseContext.CreateDirectorFromAbsolutePath(testData.testString);
                        else if (testData.Index % 3 == 1)
                            baseContext.CreateDirector(testData.testString);

                        // Filer creation
                        if (testData.Index % 3 == 2 && testData.testString.Any(x => x == Chars.BSlash || x == Chars.FSlash))
                            baseContext.CreateFilerFromAbsolutePath(testData.testString);
                        else if (testData.Index % 3 == 2)
                            baseContext.CreateFiler(testData.testString);

                        // Check if created
                        switch (testData.Index % 3)
                        {
                            case 0:
                                if (testData.testString.EndsWith(Chars.BSlash))
                                {
                                    Assert.IsTrue(Directory.Exists(testData.testString[..testData.testString.LastIndexOf(Chars.BSlash)]));
                                    break;
                                }
                                if (testData.testString.EndsWith(Chars.FSlash))
                                {
                                    Assert.IsTrue(Directory.Exists(testData.testString[..testData.testString.LastIndexOf(Chars.FSlash)]));
                                    break;
                                }

                                Assert.IsFalse(Directory.Exists(testData.testString), "Empty string should not create anything!");
                                Assert.IsFalse(File.Exists(testData.testString), "Empty string should not create anything!");
                                break;
                            case 1:
                                Assert.IsTrue(Directory.Exists(testData.testString), $"Directory '{testData.testString}' was not created!");
                                break;
                            case 2:
                                Assert.IsTrue(File.Exists(testData.testString), $"File '{testData.testString}' was not created!");
                                break;
                        }

                        // Director deletion
                        if (testData.Index % 3 == 1 && testData.testString.Any(x => x == Chars.BSlash || x == Chars.FSlash))
                            baseContext.DeleteDirectorFromAbsolutePath(testData.testString);
                        else if (testData.Index % 3 == 1)
                            baseContext.DeleteDirector(testData.testString);

                        // Filer deletion
                        if (testData.Index % 3 == 2 && testData.testString.Any(x => x == Chars.BSlash || x == Chars.FSlash))
                            baseContext.DeleteFilerFromAbsolutePath(testData.testString);
                        else if (testData.Index % 3 == 2)
                            baseContext.DeleteFiler(testData.testString);

                        switch (testData.Index % 3)
                        {
                            case 1:
                                Assert.IsFalse(Directory.Exists(testData.testString), $"Directory '{testData.testString}' should be deleted!");
                                break;
                            case 2:
                                Assert.IsFalse(File.Exists(testData.testString), $"File '{testData.testString}' should be deleted!");
                                break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                if (e.GetType() != typeof(NotImplementedException))
                    Assert.Fail($"'NotimplementedException' was expected! Actual: '{e.GetType().Name}'");
                throw;
            }
        }

        /// <summary>
        /// Gets test data.
        /// </summary>
        /// <returns>List of string to test.</returns>
        private static List<string> GetTestData()
        {
            return new List<string>()
            {
                // Backward
                "",
                "TestDirector",
                "TestFiler.txt",
                Directory.GetCurrentDirectory() + "\\",
                Directory.GetCurrentDirectory() + "\\TestDirector",
                Directory.GetCurrentDirectory() + "\\TestFiler.txt",
            };
        }
    }
}
