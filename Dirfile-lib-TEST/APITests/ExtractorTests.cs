// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 15/03/2023     \\

using Dirfile_lib.API.Extraction;
using Dirfile_lib.Exceptions;

namespace Dirfile_lib_TEST.APITests
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
            foreach (var testData in PrepareExtractorTestData().Select((value, index) => new { value, index }))
            {
                if (testData.index >= 5)
                    this._SlashMode = SlashMode.Forward;

                try
                {
                    this._Extractor?.Extract(testData.value.Key);
                }
                catch (DirfileException)
                {
                    Assert.IsTrue(true);
                    continue;
                }

                if (testData.value.Value != null)
                {
                    Assert.AreEqual(testData.value.Value.ExpInput, this._Extractor?.InputString, $"Input strings were not the same: {testData.value.Value.ExpInput}");
                    Assert.AreEqual(testData.value.Value.ExpDirectorPath, this._Extractor?.DirectorPath, $"Input strings were not the same: {testData.value.Value.ExpDirectorPath}");
                    Assert.AreEqual(testData.value.Value.ExpArgument, this._Extractor?.Arguments, $"Input strings were not the same: {testData.value.Value.ExpArgument}");
                }
            }
        }
    }
}