// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 15/03/2023     \\

using Dirfile_lib.API.Extraction;
using Dirfile_lib.API.Extraction.Modes;
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
            foreach (var ExpectedDataByTestString in GetArgumentExtractorTestData())
            {
                if (ExpectedDataByTestString.Key == "/")
                    _SlashMode = SlashMode.Forward;

                try
                {
                    var argumentExtractor = new ArgumentExtractor(this._SlashMode);
                    argumentExtractor.Extract(ExpectedDataByTestString.Key);

                    if (ExpectedDataByTestString.Value != null)
                    {
                        for (int i = 0; i < ExpectedDataByTestString.Value.OperationsInOrder.Count; i++)
                            Assert.AreEqual(ExpectedDataByTestString.Value.OperationsInOrder[i], argumentExtractor.OperationsInOrder[i], $"Operation in order were not the same! (CASE: {ExpectedDataByTestString.Key})");

                        for (int i = 0; i < ExpectedDataByTestString.Value.ArgumentsByTypeInOrder.Count; i++)
                            Assert.AreEqual(ExpectedDataByTestString.Value.ArgumentsByTypeInOrder[i], argumentExtractor.ArgumentsByTypeInOrder[i], $"Arguments in order were not the same! (CASE: {ExpectedDataByTestString.Key})");
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