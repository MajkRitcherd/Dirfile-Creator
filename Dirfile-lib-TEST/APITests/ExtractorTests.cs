// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 14/02/2023     \\

using Dirfile_lib.API.Extraction;

namespace Dirfile_lib_TEST.APITests
{
    /// <summary>
    /// Tests <see cref="Extractor"/> class.
    /// </summary>
    [TestClass]
    public class ExtractorTests
    {
        /// <summary>
        /// Tests whole <see cref="Extractor"/>.
        /// </summary>
        [TestMethod]
        public void TestExtractor()
        {
            Extractor extractor = new Extractor(SlashMode.Forward);
            Assert.Fail("Test is not implemented!");
        }
    }
}