// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 13/03/2023     \\

namespace Dirfile_lib_TEST.CoreTests
{
    /// <summary>
    /// Test class for <see cref="Director"/> class.
    /// </summary>
    internal class DirectorTestClass : ICoreTest
    {
        /// <summary>
        /// Directory test path
        /// </summary>
        private static readonly string _TestPath = Directory.GetCurrentDirectory() + "\\TestDirectory";

        /// <summary>
        /// Tests creating and deleting directories.
        /// </summary>
        public static void TestDirectorCreateAndDelete()
        {
            // CASE 1 - Test creating and deleting directory using DirectoryInfo constructor
            //              - non-existing directory
            var directoryInfo = new DirectoryInfo(_TestPath);
            var director = new Director(directoryInfo);

            director.Create();
            Assert.IsTrue(Directory.Exists(director.FullName), "Directory was not created!");
            director.Delete();
            Assert.IsFalse(Directory.Exists(director.FullName), "Directory was not deleted!");

            // CASE 2 - Test creating and deleting using directory DirectoryInfo constructor
            //              - existing directory
            director.Create();

            directoryInfo = new DirectoryInfo(_TestPath);
            director = new Director(directoryInfo);

            director.Create();
            Assert.IsTrue(Directory.Exists(director.FullName));
            director.Delete();
            Assert.IsFalse(Directory.Exists(director.FullName));

            // CASE 3 - Test creating and deleting using directory path constructor
            //              - non-existing directory
            director = new Director(_TestPath);

            director.Create();
            Assert.IsTrue(Directory.Exists(director.FullName));
            director.Delete();
            Assert.IsFalse(Directory.Exists(director.FullName));

            // CASE 4 - Test creating and deleting directory using path constructor
            //              - existing directory
            director.Create();

            director = new Director(_TestPath);

            director.Create();
            Assert.IsTrue(Directory.Exists(director.FullName));
            director.Delete();
            Assert.IsFalse(Directory.Exists(director.FullName));
        }

        /// <summary>
        /// Tests if properties of directories are set correctly.
        /// </summary>
        public static void TestDirectorPropertySet()
        {
            TestRootDirectory();

            // Test non-existing directory using DirectoryInfo
            var dirInfo = new DirectoryInfo(_TestPath);
            var dict = new Dictionary<string, object>();
            var director = new Director(dirInfo);

            bool result = ICoreTest.CompareProperties(dirInfo, dict, director, false);
            Assert.IsTrue(result, "Properties are not the same!");

            // Test existing directory using DirectoryInfo
            Directory.CreateDirectory(_TestPath);
            dirInfo = new DirectoryInfo(_TestPath);
            director = new Director(dirInfo);

            result = ICoreTest.CompareProperties(dirInfo, dict, director, true);
            Assert.IsTrue(result, "Properties are not the same!");
            Directory.Delete(_TestPath);

            // Test non-existing directory using path
            dirInfo = new DirectoryInfo(Directory.GetCurrentDirectory() + "\\DirFileTest");
            director = new Director(_TestPath);
            var parentDir = new Director(Directory.GetCurrentDirectory());
            var rootDir = new Director("C:\\");
            dict = CreateTestDictionary(false, _TestPath, rootDir, parentDir);

            result = ICoreTest.CompareProperties(dirInfo, dict, director, false);
            Assert.IsTrue(result, "Properties are not the same!");
            dict.Clear();

            // Test existing directory using path
            Directory.CreateDirectory(_TestPath);
            director = new Director(_TestPath);
            dict = CreateTestDictionary(true, _TestPath, rootDir, parentDir);

            result = ICoreTest.CompareProperties(dirInfo, dict, director, true);
            Assert.IsTrue(result, "Properties are not the same!");

            Directory.Delete(_TestPath);
        }

        /// <summary>
        /// Tests if properties of a root directory are set correctly.
        /// </summary>
        private static void TestRootDirectory()
        {
            // Test root directory without '\' character
            var rootPath = "C:";
            var director = new Director(rootPath);
            var dirInfo = new DirectoryInfo(Directory.GetCurrentDirectory() + "\\DirFileTest");

            bool result = ICoreTest.CompareProperties(dirInfo, GetRootDirectory(), director, true);
            Assert.IsTrue(result, $"Properties of a root '{rootPath}' are wrong!");

            // Test root directory with '\' character
            rootPath += "\\";
            director = new Director(rootPath);

            result = ICoreTest.CompareProperties(dirInfo, GetRootDirectory(), director, true);
            Assert.IsTrue(result, $"Properties of a root '{rootPath}' are wrong!");
        }

        /// <summary>
        /// Creates test dictionary of expected properties for Director.
        /// </summary>
        /// <param name="dictExist">True if directory exists, otherwise false.</param>
        /// <param name="pathToDict">Path to the directory.</param>
        /// <param name="root">Root director.</param>
        /// <param name="parent">Parent director.</param>
        /// <returns>Dictionary of expected properties.</returns>
        private static Dictionary<string, object> CreateTestDictionary(bool dictExist, string pathToDict, Director root, Director parent)
        {
            return new Dictionary<string, object>()
            {
                { "Attributes", FileAttributes.Directory },
                { "CreationTime", dictExist ? Directory.GetCreationTime(pathToDict) : DateTime.MinValue },
                { "CreationTimeUtc", dictExist ? Directory.GetCreationTimeUtc(pathToDict) : DateTime.MinValue },
                { "Exists", dictExist },
                { "Extension", string.Empty },
                { "FullName", _TestPath },
                { "LastAccessTime", dictExist ? Directory.GetLastAccessTime(pathToDict) : DateTime.MinValue },
                { "LastAccessTimeUtc", dictExist ? Directory.GetLastAccessTimeUtc(pathToDict) : DateTime.MinValue },
                { "LastWriteTime", dictExist ? Directory.GetLastWriteTime(pathToDict) : DateTime.MinValue },
                { "LastWriteTimeUtc", dictExist ? Directory.GetLastWriteTimeUtc(pathToDict) : DateTime.MinValue },
                { "LinkTarget", null },
                { "Name", "TestDirectory" },
                { "Root", root },
                { "Parent", parent },
                { "Path", _TestPath }
            };
        }

        /// <summary>
        /// Gets properties of a root path of 'C:\'.
        /// </summary>
        private static Dictionary<string, object> GetRootDirectory()
        {
            return new Dictionary<string, object>()
            {
                { "Attributes", FileAttributes.Hidden | FileAttributes.System | FileAttributes.Directory },
                { "CreationTime", Directory.GetCreationTime("C:\\") },
                { "CreationTimeUtc", Directory.GetCreationTimeUtc("C:\\") },
                { "Exists", true },
                { "Extension", null },
                { "FullName", "C:\\" },
                { "LastAccessTime", Directory.GetLastAccessTime("C:\\") },
                { "LastAccessTimeUtc", Directory.GetLastAccessTimeUtc("C:\\") },
                { "LastWriteTime", Directory.GetLastWriteTime("C:\\") },
                { "LastWriteTimeUtc", Directory.GetLastWriteTimeUtc("C:\\") },
                { "LinkTarget", null },
                { "Name", "C:\\" },
                { "Root", null },
                { "Parent", null },
                { "Path", "C:\\" }
            };
        }
    }
}
