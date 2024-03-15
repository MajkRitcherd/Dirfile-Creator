// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 06/04/2023     \\

using Dirfile_lib.API.Context;
using Dirfile_lib.API.Extraction.Modes;
using Dirfile_lib.Exceptions;
using Chars = Dirfile_lib.Core.Constants.DirFile.Characters;

namespace Dirfile_lib_TEST.APITests.ContextTests
{
    /// <summary>
    /// Tests <see cref="DirfileContext"/> class.
    /// </summary>
    [TestClass]
    public class DirfileContextTests
    {
        /// <summary>
        /// Initialized text in Filer during test.
        /// </summary>
        private const string TestText = "TEST INITIAL TEXT";

        /// <summary>
        /// Tests <see cref="DirfileContext"/> class.
        /// </summary>
        [TestMethod]
        public void TestDirfileContext()
        {
            using (var context = new DirfileContext(Directory.GetCurrentDirectory()))
            {
                this.TestRelativePathDirfileContext(context);
                this.CleanTestDirectories(context);
                context.SwitchSlashMode();

                this.TestRelativePathDirfileContext(context);
                this.CleanTestDirectories(context);
                context.SwitchSlashMode();
            }

            using (var context = new DirfileContext(SlashMode.Forward))
            {
                this.TestAbsolutePathDirfileContext(context);
                this.CleanTestDirectories(context);
                context.SwitchSlashMode();

                this.TestAbsolutePathDirfileContext(context);
                this.CleanTestDirectories(context);
                context.SwitchSlashMode();
            }
        }

        /// <summary>
        /// Deletes test directories and files.
        /// </summary>
        /// <param name="context">Dirfile context.</param>
        private void CleanTestDirectories(DirfileContext context)
        {
            context.ChangeCurrentDirector(context.SlashMode == SlashMode.Backward ? Directory.GetCurrentDirectory() : Directory.GetCurrentDirectory().Replace(Chars.BSlash, Chars.FSlash));
            context.DeleteDirectorRecursive("TestDir1");
            context.DeleteDirector("TestDir2");
        }

        /// <summary>
        /// Tries to create invalid director or filer.
        /// </summary>
        /// <param name="context">Dirfile context.</param>
        /// <param name="path">Path to director or filer.</param>
        /// <returns>True, if Dirfile exception was thrown, otherwise false.</returns>
        /// <exception cref="Exception">Catch if DirfileException, otherwise throw, because it is not valid exception.</exception>
        private bool CreateInvalidDirectorOrFiler(DirfileContext context, string path = "")
        {
            try
            {
                context.Create(path);
                return false;
            }
            catch (Exception exception)
            {
                if (exception is DirfileException)
                    return true;
                else
                    throw new Exception($"Creating invalid director or filer should throw dirfile exception! Actual was: {exception.Message}");
            }
        }

        /// <summary>
        /// Gets test paths to be tested using <see cref="DirfileContext"/> class.
        /// </summary>
        /// <param name="currentPath">Path of the current working directory.</param>
        /// <param name="slashMode">Slash mode.</param>
        /// <returns>List of test paths.</returns>
        private List<string> GetTestPaths(string currentPath, SlashMode slashMode = SlashMode.Backward)
        {
            var slash = slashMode == SlashMode.Backward ? Chars.BSlash : Chars.FSlash;

            return new List<string>()
            {
                currentPath + $"{slash}TestDir3",
                currentPath + $"{slash}TestDir3{slash}TestDir4",
                currentPath + $"{slash}TestDir3{slash}TestFile1.txt",
                currentPath + $"{slash}TestDir3{slash}TestDir5",
                currentPath + $"{slash}TestDir3{slash}TestDir5{slash}TestFile2.csv",
                currentPath + $"{slash}TestDir3{slash}TestDir5{slash}TestFile3.exe",
                currentPath + $"{slash}TestDir3{slash}TestFile4.xlsx",
                currentPath + $"{slash}TestComplete",
                currentPath + $"{slash}TestComplete.txt",
                currentPath + $"{slash}TestDir3{slash}TestDir5{slash}TestDir6",
                currentPath + $"{slash}TestDir3{slash}TestDir5{slash}TestDir6{slash}TestFile5.cpp",
                currentPath + $"{slash}TestDir3{slash}TestDir5{slash}TestDir6{slash}TestFile6.png",
                currentPath + $"{slash}TestDir3{slash}TestDir5{slash}LastFile.txt",
            };
        }

        /// <summary>
        /// Test uses absolute path everywhere.
        /// </summary>
        private void TestAbsolutePathDirfileContext(DirfileContext context)
        {
            var slash = context.SlashMode == SlashMode.Backward ? Chars.BSlash : Chars.FSlash;

            var currentPath = context.CurrentPath;
            var pathToTestDirectory = currentPath + $"{slash}TestDir1";

            // Create director
            context.Create(pathToTestDirectory);
            Assert.IsTrue(Directory.Exists(pathToTestDirectory), "TestDir1 should be created!");

            // Tries to create a director that already exists and create director
            context.Create($"{pathToTestDirectory} > TestDir2");
            Assert.IsTrue(Directory.Exists(currentPath + $"{slash}TestDir1"), "TestDir1 should exists (Was created in previous call or should create one)!");
            Assert.IsTrue(Directory.Exists(currentPath + $"{slash}TestDir2"), "TestDir2 should be created!");

            // Tries to create invalid director and filer
            Assert.IsTrue(this.CreateInvalidDirectorOrFiler(context, $"{pathToTestDirectory}{slash}testDir3*   "), "Invalid director should not be created!");
            Assert.IsTrue(this.CreateInvalidDirectorOrFiler(context, $"{pathToTestDirectory}{slash}test:Filer1.txt"), "Invalid filer should not be created!");

            // Actual dirfile creation
            var testPaths = this.GetTestPaths(pathToTestDirectory);

            context.Create($"{pathToTestDirectory}{slash}TestDir3{slash}TestDir4 > TestFile1.txt :\"{TestText}\" > TestDir5{slash}TestFile2.csv > TestFile3.exe :> TestFile4.xlsx :> TestComplete > TestComplete.txt");

            Assert.IsTrue(Directory.Exists(testPaths.ElementAt(0)), "TestDir3 is not created inside TestDir1!");
            Assert.IsTrue(Directory.Exists(testPaths.ElementAt(1)), "TestDir4 is not created in TestDir3!");
            Assert.IsTrue(File.Exists(testPaths.ElementAt(2)), "TestFile1.txt is not created in TestDir3!");
            var initializedText = File.ReadAllText(testPaths.ElementAt(2));
            Assert.AreEqual(TestText, initializedText, $"Initialized text is not the same as the one inserted!\nText in Filer: '{initializedText}', should be: {TestText}");
            Assert.IsTrue(Directory.Exists(testPaths.ElementAt(3)), "TestDir5 is not created inside TestDir3!");
            Assert.IsTrue(File.Exists(testPaths.ElementAt(4)), "TestFile2.csv was not created in TestDir5!");
            Assert.IsTrue(File.Exists(testPaths.ElementAt(5)), "TestFile3.exe was not created in TestDir5!");
            Assert.IsTrue(File.Exists(testPaths.ElementAt(6)), "TestFile4.xlsx was not created in TestDir3!");
            Assert.IsTrue(Directory.Exists(testPaths.ElementAt(7)), "TestComplete was not created!");
            Assert.IsTrue(File.Exists(testPaths.ElementAt(8)), "TestComplete.txt was not created!");

            // Create in existing directories
            context.Create($"{pathToTestDirectory}{slash}TestDir3{slash}TestDir5{slash}TestDir6{slash}TestFile5.cpp > TestFile6.png :> LastFile.txt");

            Assert.IsTrue(Directory.Exists(testPaths.ElementAt(0)), "TestDir3 is not created inside TestDir1!");
            Assert.IsTrue(Directory.Exists(testPaths.ElementAt(3)), "TestDir5 is not created inside TestDir3!");
            Assert.IsTrue(Directory.Exists(testPaths.ElementAt(9)), "TestDir6 is not created inside TestDir5!");
            Assert.IsTrue(File.Exists(testPaths.ElementAt(10)), "TestFile5.cpp was not created!");
            Assert.IsTrue(File.Exists(testPaths.ElementAt(11)), "TestFile6.png was not created!");
            Assert.IsTrue(File.Exists(testPaths.ElementAt(12)), "LastFile.txt was not created!");
        }

        /// <summary>
        /// Test uses relative path everywhere.
        /// </summary>
        private void TestRelativePathDirfileContext(DirfileContext context)
        {
            var slash = context.SlashMode == SlashMode.Backward ? Chars.BSlash : Chars.FSlash;

            // Initially is slashMode == backward
            Assert.IsTrue(this.CreateInvalidDirectorOrFiler(context), "Context.Create should not create anything from string.Empty!");

            var currentPath = context.CurrentPath;
            var pathToTestDirectory = currentPath + $"{slash}TestDir1";

            // Create director
            context.Create($"{slash}TestDir1");
            Assert.IsTrue(Directory.Exists(pathToTestDirectory), "TestDir1 should be created!");

            // Director change
            context.ChangeCurrentDirector(currentPath + $"{slash}TestDir1");
            Assert.AreEqual(pathToTestDirectory, context.CurrentPath, $"Directories {pathToTestDirectory} (expected) and {context.CurrentPath} (actual) are not equal!");

            // Tries to create a director that already exists and create director
            context.ChangeCurrentDirector(context.CurrentPath.Remove(context.CurrentPath.LastIndexOf(slash)));
            context.Create($"{slash}TestDir1 > TestDir2");
            Assert.IsTrue(Directory.Exists(currentPath + $"{slash}TestDir1"), "TestDir1 should exists (Was created in previous call or should create one)!");
            Assert.IsTrue(Directory.Exists(currentPath + $"{slash}TestDir2"), "TestDir2 should be created!");

            // Tries to create invalid director and filer
            Assert.IsTrue(this.CreateInvalidDirectorOrFiler(context, $"{slash}testDir3*   "), "Invalid director should not be created!");
            Assert.IsTrue(this.CreateInvalidDirectorOrFiler(context, $"{slash}test:Filer1.txt"), "Invalid filer should not be created!");

            // Actual dirfile creation
            var testPaths = this.GetTestPaths(pathToTestDirectory);

            context.ChangeCurrentDirector(pathToTestDirectory);
            context.Create($"{slash}TestDir3{slash}TestDir4 > TestFile1.txt :\"{TestText}\" > TestDir5{slash}TestFile2.csv > TestFile3.exe :> TestFile4.xlsx :> TestComplete > TestComplete.txt");

            Assert.IsTrue(Directory.Exists(testPaths.ElementAt(0)), "TestDir3 is not created inside TestDir1!");
            Assert.IsTrue(Directory.Exists(testPaths.ElementAt(1)), "TestDir4 is not created in TestDir3!");
            Assert.IsTrue(File.Exists(testPaths.ElementAt(2)), "TestFile1.txt is not created in TestDir3!");
            var initializedText = File.ReadAllText(testPaths.ElementAt(2));
            Assert.AreEqual(TestText, initializedText, $"Initialized text is not the same as the one inserted!\nText in Filer: '{initializedText}', should be: {TestText}");
            Assert.IsTrue(Directory.Exists(testPaths.ElementAt(3)), "TestDir5 is not created inside TestDir3!");
            Assert.IsTrue(File.Exists(testPaths.ElementAt(4)), "TestFile2.csv was not created in TestDir5!");
            Assert.IsTrue(File.Exists(testPaths.ElementAt(5)), "TestFile3.exe was not created in TestDir5!");
            Assert.IsTrue(File.Exists(testPaths.ElementAt(6)), "TestFile4.xlsx was not created in TestDir3!");
            Assert.IsTrue(Directory.Exists(testPaths.ElementAt(7)), "TestComplete was not created!");
            Assert.IsTrue(File.Exists(testPaths.ElementAt(8)), "TestComplete.txt was not created!");

            // Create in existing directories
            context.Create($"{slash}TestDir3{slash}TestDir5{slash}TestDir6{slash}TestFile5.cpp > TestFile6.png :> LastFile.txt");

            Assert.IsTrue(Directory.Exists(testPaths.ElementAt(0)), "TestDir3 is not created inside TestDir1!");
            Assert.IsTrue(Directory.Exists(testPaths.ElementAt(3)), "TestDir5 is not created inside TestDir3!");
            Assert.IsTrue(Directory.Exists(testPaths.ElementAt(9)), "TestDir6 is not created inside TestDir5!");
            Assert.IsTrue(File.Exists(testPaths.ElementAt(10)), "TestFile5.cpp was not created!");
            Assert.IsTrue(File.Exists(testPaths.ElementAt(11)), "TestFile6.png was not created!");
            Assert.IsTrue(File.Exists(testPaths.ElementAt(12)), "LastFile.txt was not created!");
        }
    }
}