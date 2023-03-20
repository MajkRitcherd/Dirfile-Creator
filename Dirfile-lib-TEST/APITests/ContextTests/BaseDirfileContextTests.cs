// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 20/03/2023     \\

using Dirfile_lib.API.Context;

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
                using (var baseCtx = new DirfileContext(Directory.GetCurrentDirectory()))
                {
                    foreach (var testData in GetTestData().Select((value, index) => new { value, index }))
                    {
                        if (string.IsNullOrEmpty(testData.value) && testData.index != 0)
                            baseCtx.SwitchSlashMode();

                        // Director creation
                        if (testData.index % 3 == 1 && testData.value.Any(x => x == '\\' || x == '/'))
                            baseCtx.CreateDirectorPath(testData.value);
                        else if (testData.index % 3 == 1)
                            baseCtx.CreateDirector(testData.value);

                        // Filer creation
                        if (testData.index % 3 == 2 && testData.value.Any(x => x == '\\' || x == '/'))
                            baseCtx.CreateFilerPath(testData.value);
                        else if (testData.index % 3 == 2)
                            baseCtx.CreateFiler(testData.value);

                        // Check if created
                        switch (testData.index % 3)
                        {
                            case 0:
                                if (testData.value.EndsWith("\\"))
                                {
                                    Assert.IsTrue(Directory.Exists(testData.value[..testData.value.LastIndexOf("\\")]));
                                    break;
                                }
                                if (testData.value.EndsWith('/'))
                                {
                                    Assert.IsTrue(Directory.Exists(testData.value[..testData.value.LastIndexOf("/")]));
                                    break;
                                }

                                Assert.IsFalse(Directory.Exists(testData.value), "Empty string should not create anything!");
                                Assert.IsFalse(File.Exists(testData.value), "Empty string should not create anything!");
                                break;
                            case 1:
                                Assert.IsTrue(Directory.Exists(testData.value), $"Directory '{testData.value}' was not created!");
                                break;
                            case 2:
                                Assert.IsTrue(File.Exists(testData.value), $"File '{testData.value}' was not created!");
                                break;
                        }

                        // Director deletion
                        if (testData.index % 3 == 1 && testData.value.Any(x => x == '\\' || x == '/'))
                            baseCtx.DeleteDirectorPath(testData.value);
                        else if (testData.index % 3 == 1)
                            baseCtx.DeleteDirector(testData.value);

                        // Filer deletion
                        if (testData.index % 3 == 2 && testData.value.Any(x => x == '\\' || x == '/'))
                            baseCtx.DeleteFilerPath(testData.value);
                        else if (testData.index % 3 == 2)
                            baseCtx.DeleteFiler(testData.value);

                        switch (testData.index % 3)
                        {
                            case 1:
                                Assert.IsFalse(Directory.Exists(testData.value), $"Directory '{testData.value}' should be deleted!");
                                break;
                            case 2:
                                Assert.IsFalse(File.Exists(testData.value), $"File '{testData.value}' should be deleted!");
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
