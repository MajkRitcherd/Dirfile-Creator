// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 01/03/2023     \\

using Dirfile_lib.Core;
using CD = Dirfile_lib.Core.Constants.DefaultValues;

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
            string path = Directory.GetCurrentDirectory() + "\\file1.txt";

            // Test non-existing file using FileInfo
            var fileInfo = new FileInfo(path);
            var dict = new Dictionary<string, object>();
            var filer = new Filer(fileInfo);

            bool result = ICoreTest.CompareProperties(fileInfo, dict, filer, false);
            Assert.IsTrue(result, "[Non-existing file using FileInfo]: Properties are not the same!");

            // Test existing file using FileInfo
            var handle = File.Create(path);
            fileInfo = new FileInfo(path);
            filer = new Filer(fileInfo);
            handle.Close();

            result = ICoreTest.CompareProperties(fileInfo, dict, filer, true);
            Assert.IsTrue(result, "[Existing file using FileInfo]: Properties are not the same!");

            File.Delete(path);

            // Test non-existing file using path
            fileInfo = new FileInfo(Directory.GetCurrentDirectory() + "\\DirFileTest");
            filer = new Filer(path);
            var dir = new Director(Directory.GetCurrentDirectory());
            dict = CreateTestDictionary(false, path, dir);

            result = ICoreTest.CompareProperties(fileInfo, dict, filer, false);
            Assert.IsTrue(result, "[Non-existing file using path]: Properties are not the same!");

            dict.Clear();

            // Test existing file using path
            handle = File.Create(path);
            filer = new Filer(path);
            dict = CreateTestDictionary(true, path, dir);
            handle.Close();

            result = ICoreTest.CompareProperties(fileInfo, dict, filer, true);
            Assert.IsTrue(result, "[Existing file using path]: Properties are not the same!");

            File.Delete(Directory.GetCurrentDirectory() + "\\file1.txt");
        }

        /// <summary>
        /// Creates test dictionary of expected properties for Filer.
        /// </summary>
        /// <param name="fileExist">True if file exists, otherwise false.</param>
        /// <param name="pathToFile">Path to the file.</param>
        /// <param name="directory">Directoror in which the file is in.</param>
        /// <returns></returns>
        private static Dictionary<string, object> CreateTestDictionary(bool fileExist, string pathToFile, Director directory)
        {
            return new Dictionary<string, object>
            {
                { "Attributes", FileAttributes.Archive },
                { "BufferSize", CD.BufferSize },
                { "CreationTime", fileExist ? File.GetCreationTime(pathToFile) : DateTime.MinValue },
                { "CreationTimeUtc", fileExist ? File.GetCreationTimeUtc(pathToFile) : DateTime.MinValue },
                { "Directory", directory },
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