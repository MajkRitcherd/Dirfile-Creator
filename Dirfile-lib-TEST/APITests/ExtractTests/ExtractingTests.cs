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
            foreach (var testData in PrepareTestData().Select((value, index) => new { value, index }))
            {
                if (testData.index >= 5)
                    this._SlashMode = SlashMode.Forward;

                try
                {
                    this._Extractor?.Extract(testData.value.Key);
                    var argExtractor = new ArgumentExtractor(this._Extractor?.Arguments, this._SlashMode);

                    Assert.AreEqual(testData.value.Value?.ExtExpData?.ExpInput, this._Extractor?.InputString, $"Input strings were not the same: {testData.value.Value?.ExtExpData?.ExpInput}");
                    Assert.AreEqual(testData.value.Value?.ExtExpData?.ExpDirectorPath, this._Extractor?.DirectorPath, $"Input strings were not the same: {testData.value.Value?.ExtExpData?.ExpDirectorPath}");
                    Assert.AreEqual(testData.value.Value?.ExtExpData?.ExpArgument, this._Extractor?.Arguments, $"Input strings were not the same: {testData.value.Value?.ExtExpData?.ExpArgument}");

                    for (int i = 0; i < argExtractor.OperationsInOrder.Count; i++)
                        Assert.AreEqual(testData.value.Value?.ArgExpData?.OperationsInOrder[i], argExtractor.OperationsInOrder[i], $"Operation in order were not the same! (CASE: {testData.value.Value?.ArgExpData?.OperationsInOrder[i]})");

                    for (int i = 0; i < argExtractor.ArgumentsInOrder.Count; i++)
                        Assert.AreEqual(testData.value.Value?.ArgExpData?.ArgumentsInOrder[i], argExtractor.ArgumentsInOrder[i], $"Arguments in order were not the same! (CASE: {testData.value.Value?.ArgExpData?.ArgumentsInOrder[i]})");
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