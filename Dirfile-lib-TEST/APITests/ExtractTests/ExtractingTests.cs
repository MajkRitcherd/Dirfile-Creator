// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 22/03/2023     \\

using Dirfile_lib.API.Extraction;
using Dirfile_lib.API.Extraction.Modes;
using Dirfile_lib.Exceptions;

namespace Dirfile_lib_TEST.APITests.ExtractTests
{
    /// <summary>
    /// Tests <see cref="Extractor"/> and <see cref="ArgumentExtractor"/> classes.
    /// </summary>
    [TestClass]
    public class ExtractingTests : BaseExtractingTestClass
    {
        /// <summary>
        /// Gets or sets the <see cref="Extractor"/> class.
        /// </summary>
        private Extractor? _Extractor { get; set; }

        /// <summary>
        /// Gets or sets slash mode.
        /// </summary>
        private SlashMode _SlashMode { get; set; }

        /// <inheritdoc/>
        [TestInitialize]
        public override void Init()
        {
            this._SlashMode = SlashMode.Backward;
            this._Extractor = new Extractor(this._SlashMode);
        }

        /// <summary>
        /// Tests <see cref="Extractor"/> class with <see cref="ArgumentExtractor"/> class.
        /// </summary>
        [TestMethod]
        public override void TestExtracting()
        {
            foreach (var testData in GetExtractingTestData().Select((ExpectedDataByTestString, Index) => new { ExpectedDataByTestString, Index }))
            {
                if (testData.Index >= 5)
                    this._SlashMode = SlashMode.Forward;

                try
                {
                    this._Extractor?.Extract(testData.ExpectedDataByTestString.Key);
                    var argExtractor = new ArgumentExtractor(this._SlashMode);
                    argExtractor.Extract(this._Extractor?.ArgumentString);

                    Assert.AreEqual(
                        testData.ExpectedDataByTestString.Value?.ExtractorExpData?.ExpectedInput, this._Extractor?.ReceivedString, $"Input strings were not the same: {testData.ExpectedDataByTestString.Value?.ExtractorExpData?.ExpectedInput}");

                    Assert.AreEqual(testData.ExpectedDataByTestString.Value?.ExtractorExpData?.ExpectedDirectorPath, this._Extractor?.DirectorPath, $"Input strings were not the same: {testData.ExpectedDataByTestString.Value?.ExtractorExpData?.ExpectedDirectorPath}");

                    Assert.AreEqual(testData.ExpectedDataByTestString.Value?.ExtractorExpData?.ExpectedArgument, this._Extractor?.ArgumentString, $"Input strings were not the same: {testData.ExpectedDataByTestString.Value?.ExtractorExpData?.ExpectedArgument}");

                    for (int i = 0; i < argExtractor.OperationsInOrder.Count; i++)
                        Assert.AreEqual(testData.ExpectedDataByTestString.Value?.ArgumentExpData?.OperationsInOrder[i], argExtractor.OperationsInOrder[i], $"Operation in order were not the same! (CASE: {testData.ExpectedDataByTestString.Value?.ArgumentExpData?.OperationsInOrder[i]})");

                    for (int i = 0; i < argExtractor.ArgumentsByTypeInOrder.Count; i++)
                        Assert.AreEqual(testData.ExpectedDataByTestString.Value?.ArgumentExpData?.ArgumentsByTypeInOrder[i], argExtractor.ArgumentsByTypeInOrder[i], $"Arguments in order were not the same! (CASE: {testData.ExpectedDataByTestString.Value?.ArgumentExpData?.ArgumentsByTypeInOrder[i]})");
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