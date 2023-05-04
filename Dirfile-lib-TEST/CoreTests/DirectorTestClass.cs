// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 16/03/2023     \\

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
            var directoryInfo = new DirectoryInfo(_TestPath);
            var expectedDirectorProperties = new Dictionary<string, object?>();
            var director = new Director(directoryInfo);

            bool arePropertiesEqual = ICoreTest.CompareProperties(directoryInfo, expectedDirectorProperties, director, false);
            Assert.IsTrue(arePropertiesEqual, "Properties are not the same!");

            // Test existing directory using DirectoryInfo
            Directory.CreateDirectory(_TestPath);
            directoryInfo = new DirectoryInfo(_TestPath);
            director = new Director(directoryInfo);

            arePropertiesEqual = ICoreTest.CompareProperties(directoryInfo, expectedDirectorProperties, director, true);
            Assert.IsTrue(arePropertiesEqual, "Properties are not the same!");
            Directory.Delete(_TestPath);

            // Test non-existing directory using path
            directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory() + "\\DirFileTest");
            director = new Director(_TestPath);
            var parentDirector = new Director(Directory.GetCurrentDirectory());
            var rootDirector = new Director("C:\\");
            expectedDirectorProperties = GetExpectedProperties(false, _TestPath, rootDirector, parentDirector);

            arePropertiesEqual = ICoreTest.CompareProperties(directoryInfo, expectedDirectorProperties, director, false);
            Assert.IsTrue(arePropertiesEqual, "Properties are not the same!");
            expectedDirectorProperties.Clear();

            // Test existing directory using path
            Directory.CreateDirectory(_TestPath);
            director = new Director(_TestPath);
            expectedDirectorProperties = GetExpectedProperties(true, _TestPath, rootDirector, parentDirector);

            arePropertiesEqual = ICoreTest.CompareProperties(directoryInfo, expectedDirectorProperties, director, true);
            Assert.IsTrue(arePropertiesEqual, "Properties are not the same!");

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
            var directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory() + "\\DirFileTest");

            bool arePropertiesEqual = ICoreTest.CompareProperties(directoryInfo, GetRootDirectoryProperties(), director, true);
            Assert.IsTrue(arePropertiesEqual, $"Properties of a root '{rootPath}' are wrong!");

            // Test root directory with '\' character
            rootPath += "\\";
            director = new Director(rootPath);

            arePropertiesEqual = ICoreTest.CompareProperties(directoryInfo, GetRootDirectoryProperties(), director, true);
            Assert.IsTrue(arePropertiesEqual, $"Properties of a root '{rootPath}' are wrong!");
        }

        /// <summary>
        /// Gets expected properties.
        /// </summary>
        /// <param name="directoryExists">True if directory exists, otherwise false.</param>
        /// <param name="pathToDirectory">Path to the directory.</param>
        /// <param name="rootDirector">Root director.</param>
        /// <param name="parentDirector">Parent director.</param>
        /// <returns>Property by property name.</returns>
        private static Dictionary<string, object?> GetExpectedProperties(bool directoryExists, string pathToDirectory, Director rootDirector, Director parentDirector)
        {
            return new Dictionary<string, object?>()
            {
                { "Attributes", FileAttributes.Directory },
                { "CreationTime", directoryExists ? Directory.GetCreationTime(pathToDirectory) : DateTime.MinValue },
                { "CreationTimeUtc", directoryExists ? Directory.GetCreationTimeUtc(pathToDirectory) : DateTime.MinValue },
                { "Exists", directoryExists },
                { "Extension", string.Empty },
                { "FullName", _TestPath },
                { "LastAccessTime", directoryExists ? Directory.GetLastAccessTime(pathToDirectory) : DateTime.MinValue },
                { "LastAccessTimeUtc", directoryExists ? Directory.GetLastAccessTimeUtc(pathToDirectory) : DateTime.MinValue },
                { "LastWriteTime", directoryExists ? Directory.GetLastWriteTime(pathToDirectory) : DateTime.MinValue },
                { "LastWriteTimeUtc", directoryExists ? Directory.GetLastWriteTimeUtc(pathToDirectory) : DateTime.MinValue },
                { "LinkTarget", null },
                { "Name", "TestDirectory" },
                { "Root", rootDirector },
                { "Parent", parentDirector },
                { "Path", _TestPath }
            };
        }

        /// <summary>
        /// Gets properties of a root directory 'C:\'.
        /// </summary>
        /// <returns>Property by property name.</returns>
        private static Dictionary<string, object?> GetRootDirectoryProperties()
        {
            return new Dictionary<string, object?>()
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
