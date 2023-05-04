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
    /// Tests <see cref="Extractor"/> class.
    /// </summary>
    [TestClass]
    public class ExtractorTests : BaseExtractingTestClass
    {
        /// <summary>
        /// Gets or sets the <see cref="Extractor"/> class.
        /// </summary>
        private Extractor? _Extractor { get; set; }

        /// <summary>
        /// Gets or sets the slash mode used in inputs.
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
        /// Tests whole <see cref="Extractor"/>.
        /// </summary>
        [TestMethod]
        public override void TestExtracting()
        {
            foreach (var testData in GetExtractorTestData().Select((ExpectedDataByTestString, Index) => new { ExpectedDataByTestString, Index }))
            {
                if (testData.Index >= 5)
                    this._SlashMode = SlashMode.Forward;

                try
                {
                    this._Extractor?.Extract(testData.ExpectedDataByTestString.Key);
                }
                catch (DirfileException)
                {
                    Assert.IsTrue(true);
                    continue;
                }

                if (testData.ExpectedDataByTestString.Value != null)
                {
                    Assert.AreEqual(testData.ExpectedDataByTestString.Value.ExpectedInput, this._Extractor?.ReceivedString, $"Input strings were not the same: {testData.ExpectedDataByTestString.Value.ExpectedInput}");
                    Assert.AreEqual(testData.ExpectedDataByTestString.Value.ExpectedDirectorPath, this._Extractor?.DirectorPath, $"Input strings were not the same: {testData.ExpectedDataByTestString.Value.ExpectedDirectorPath}");
                    Assert.AreEqual(testData.ExpectedDataByTestString.Value.ExpectedArgument, this._Extractor?.ArgumentString, $"Input strings were not the same: {testData.ExpectedDataByTestString.Value.ExpectedArgument}");
                }
            }
        }
    }
}