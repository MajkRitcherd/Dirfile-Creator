// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 15/03/2023     \\

using Dirfile_lib.API.Extraction;
using Dirfile_lib.Exceptions;

namespace Dirfile_lib_TEST.APITests.ExtractTests
{
    /// <summary>
    /// Tests <see cref="ArgumentExtractor"/> class.
    /// </summary>
    [TestClass]
    public class ArgumentExtractorTests : BaseExtractingTestClass
    {
        /// <summary>
        /// Gets or sets slash mode.
        /// </summary>
        private SlashMode _SlashMode { get; set; }

        /// <inheritdoc/>
        [TestInitialize]
        public override void Init()
        {
            _SlashMode = SlashMode.Backward;
        }

        /// <summary>
        /// Tests whole <see cref="ArgumentExtractor"/>.
        /// </summary>
        [TestMethod]
        public override void TestExtracting()
        {
            foreach (var data in PrepareArgExtractorTestData())
            {
                if (data.Key == "/")
                    _SlashMode = SlashMode.Forward;

                try
                {
                    var extractor = new ArgumentExtractor(this._SlashMode);
                    extractor.Extract(data.Key);

                    if (data.Value != null)
                    {
                        for (int i = 0; i < data.Value.OperationsInOrder.Count; i++)
                            Assert.AreEqual(data.Value.OperationsInOrder[i], extractor.OperationsInOrder[i], $"Operation in order were not the same! (CASE: {data.Key})");

                        for (int i = 0; i < data.Value.ArgumentsInOrder.Count; i++)
                            Assert.AreEqual(data.Value.ArgumentsInOrder[i], extractor.ArgumentsInOrder[i], $"Arguments in order were not the same! (CASE: {data.Key})");
                    }
                }
                catch (DirfileException)
                {
                    Assert.IsTrue(true);
                    continue;
                }
            }
        }
    }
}