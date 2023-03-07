﻿// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 07/03/2023     \\

using Dirfile_lib.Utilities.Validation;

namespace Dirfile_lib_TEST.UtilitiesTests.RegexTests
{
    /// <summary>
    /// Tests <see cref="PathValidator"/> class.
    /// </summary>
    [TestClass]
    public class PathValidatorTests
    {
        /// <summary>
        /// Test paths for Path validator.
        /// </summary>
        private readonly List<string> _paths = new()
        {
            "C:",
            Directory.GetCurrentDirectory() + "\\Test_Lol",
            Directory.GetCurrentDirectory() + "\\Test",
            Directory.GetCurrentDirectory() + "\\Test\\file1.txt",
            Directory.GetCurrentDirectory() + "\\Test\\ss",
            Directory.GetCurrentDirectory() + "\\_Test.csv",
            Directory.GetCurrentDirectory() + "\\Test+Test",
            Directory.GetCurrentDirectory() + "\\Test\\Test/v",
        };

        /// <summary>
        /// Tests path validating using <see cref="PathValidator"/> class.
        /// </summary>
        [TestMethod]
        public void TestValidator()
        {

            foreach (var item in _paths.Select((item, index) => new { item, index }))
            {
                if (item.index < 6)
                    Assert.IsTrue(PathValidator.Instance.IsValid(item.item), $"Path with index: {item.index} was badly classified.");
                else
                    Assert.IsFalse(PathValidator.Instance.IsValid(item.item), $"Path with index: {item.index} was badly classified.");
            }
        }
    }
}