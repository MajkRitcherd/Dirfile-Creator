// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 02/12/2022     \\

namespace Dirfile_lib_TEST.CoreTests
{
    /// <summary>
    /// Test class which tests Dirfile-lib's CORE folder.
    /// </summary>
    [TestClass]
    public class CoreTests
    {
        /// <summary>
        /// Tests <see cref="Filer"/> and <see cref="Director"/> classes.
        /// </summary>
        [TestMethod]
        public void TestEverything()
        {
            // Calls everything
            DirectorTestClass.TestDirectorCreateAndDelete();
            DirectorTestClass.TestDirectorPropertySet();

            FilerTestClass.TestFilerCreateAndDelete();
            FilerTestClass.TestFilerPropertySet();
        }

        /// <summary>
        /// Tests <see cref="Filer"/> class.
        /// </summary>
        [TestMethod]
        public void TestFiler()
        {
            // Calls only Filer related tests
            FilerTestClass.TestFilerCreateAndDelete();
            FilerTestClass.TestFilerPropertySet();
        }

        /// <summary>
        /// Tests <see cref="Director"/> class.
        /// </summary>
        [TestMethod]
        public void TestDirector()
        {
            // Calls only Director related tests
            DirectorTestClass.TestDirectorCreateAndDelete();
            DirectorTestClass.TestDirectorPropertySet();
        }
    }
}
