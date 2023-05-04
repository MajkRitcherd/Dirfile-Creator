// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 26/04/2023     \\

using Dirfile_lib.Extensions;
using ValueConsts = Dirfile_lib.Core.Constants.DefaultValues;

namespace Dirfile_lib_TEST.CoreTests
{
    /// <summary>
    /// Test class for <see cref="Filer"/> class.
    /// </summary>
    internal class FilerTestClass : ICoreTest
    {
        /// <summary>
        /// Tests creating and deleting files.
        /// </summary>
        public static void TestFilerCreateAndDelete()
        {
            // CASE 1 - Test creating and deleting file using FileInfo class (File has to exist)
            var fileInfo = new FileInfo(Directory.GetCurrentDirectory() + "\\file1.txt");
            var filer = new Filer(fileInfo);

            filer.Create();
            Assert.IsTrue(filer.Exists, "Filer should be created!");
            filer.Delete();
            Assert.IsFalse(filer.Exists, "Filer should be deleted!");

            // CASE 2 - Test creating and deleting file using path without extension
            var filerv2 = new Filer(Directory.GetCurrentDirectory() + "\\file1");

            filerv2.Create();
            Assert.IsTrue(filerv2.Exists, "Filer should be created!");
            filerv2.Delete();
            Assert.IsFalse(filerv2.Exists, "Filer should be deleted!");

            filerv2.Create(DirfileExtensions.Data.CSV);
            Assert.IsTrue(filerv2.Exists, "Filer should be created!");
            filerv2.Delete();
            Assert.IsFalse(filerv2.Exists, "Filer should be deleted!");

            // CASE 3 - Test creating and deleting file using path with extension
            var filerv3 = new Filer(Directory.GetCurrentDirectory() + "\\file2.txt");

            filerv3.Create();
            Assert.IsTrue(filerv3.Exists, "Filer should be created!");
            filerv3.Delete();
            Assert.IsFalse(filerv3.Exists, "Filer should be deleted!");
        }

        /// <summary>
        /// Tests if properties of files are set correctly.
        /// </summary>
        public static void TestFilerPropertySet()
        {
            string pathToFile = Directory.GetCurrentDirectory() + "\\file1.txt";

            // Test non-existing file using FileInfo
            var fileInfo = new FileInfo(pathToFile);
            var expectedFilerProperties = new Dictionary<string, object?>();
            var filer = new Filer(fileInfo);

            bool arePropertiesEqual = ICoreTest.CompareProperties(fileInfo, expectedFilerProperties, filer, false);
            Assert.IsTrue(arePropertiesEqual, "[Non-existing file using FileInfo]: Properties are not the same!");

            // Test existing file using FileInfo
            var fileHandle = File.Create(pathToFile);
            fileInfo = new FileInfo(pathToFile);
            filer = new Filer(fileInfo);
            fileHandle.Close();

            arePropertiesEqual = ICoreTest.CompareProperties(fileInfo, expectedFilerProperties, filer, true);
            Assert.IsTrue(arePropertiesEqual, "[Existing file using FileInfo]: Properties are not the same!");

            File.Delete(pathToFile);

            // Test non-existing file using path
            fileInfo = new FileInfo(Directory.GetCurrentDirectory() + "\\DirFileTest");
            filer = new Filer(pathToFile);
            var director = new Director(Directory.GetCurrentDirectory());
            expectedFilerProperties = GetExpectedProperties(false, pathToFile, director);

            arePropertiesEqual = ICoreTest.CompareProperties(fileInfo, expectedFilerProperties, filer, false);
            Assert.IsTrue(arePropertiesEqual, "[Non-existing file using path]: Properties are not the same!");

            expectedFilerProperties.Clear();

            // Test existing file using path
            fileHandle = File.Create(pathToFile);
            filer = new Filer(pathToFile);
            expectedFilerProperties = GetExpectedProperties(true, pathToFile, director);
            fileHandle.Close();

            arePropertiesEqual = ICoreTest.CompareProperties(fileInfo, expectedFilerProperties, filer, true);
            Assert.IsTrue(arePropertiesEqual, "[Existing file using path]: Properties are not the same!");

            File.Delete(Directory.GetCurrentDirectory() + "\\file1.txt");
        }

        /// <summary>
        /// Gets expected properties.
        /// </summary>
        /// <param name="fileExist">True if file exists, otherwise false.</param>
        /// <param name="pathToFile">Path to the file.</param>
        /// <param name="director">Directoror in which the file is in.</param>
        /// <returns>Property by property name.</returns>
        private static Dictionary<string, object?> GetExpectedProperties(bool fileExist, string pathToFile, Director director)
        {
            return new Dictionary<string, object?>
            {
                { "Attributes", FileAttributes.Archive },
                { "BufferSize", ValueConsts.BufferSize },
                { "CreationTime", fileExist ? File.GetCreationTime(pathToFile) : DateTime.MinValue },
                { "CreationTimeUtc", fileExist ? File.GetCreationTimeUtc(pathToFile) : DateTime.MinValue },
                { "Directory", director },
                { "DirectoryName", Directory.GetCurrentDirectory() },
                { "Exists", fileExist },
                { "Extension", ".txt" },
                { "FullName", Directory.GetCurrentDirectory() + "\\file1.txt" },
                { "IsReadOnly", false },
                { "LastAccessTime", fileExist ? File.GetLastAccessTime(pathToFile) : DateTime.MinValue },
                { "LastAccessTimeUtc", fileExist ? File.GetLastAccessTimeUtc(pathToFile) : DateTime.MinValue },
                { "LastWriteTime", fileExist ? File.GetLastWriteTime(pathToFile) : DateTime.MinValue },
                { "LastWriteTimeUtc", fileExist ? File.GetLastWriteTimeUtc(pathToFile) : DateTime.MinValue },
                { "Length", 0 },
                { "LinkTarget", null },
                { "Name", "file1.txt" },
                { "Options", FileOptions.None },
                { "Path", Directory.GetCurrentDirectory() + "\\file1" }
            };
        }

    }
}