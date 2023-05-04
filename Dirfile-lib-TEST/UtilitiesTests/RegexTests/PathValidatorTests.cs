// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 26/04/2023     \\

using Dirfile_lib.Utilities.Validation;
using Dirfile_lib.API.Extraction.Modes;
using Chars = Dirfile_lib.Core.Constants.DirFile.Characters;

namespace Dirfile_lib_TEST.UtilitiesTests.RegexTests
{
    /// <summary>
    /// Tests <see cref="PathValidator"/> class.
    /// </summary>
    [TestClass]
    public class PathValidatorTests
    {
        /// <summary>
        /// Tests path validating using <see cref="PathValidator"/> class.
        /// </summary>
        [TestMethod]
        public void TestValidator()
        {
            if (PathValidator.Instance.SlashMode != SlashMode.Backward)
                PathValidator.Instance.SwitchSlashMode();

            foreach (var expectedData in GetTestData().Select((Path, Index) => new { Path, Index }))
            {
                var stringToValidate = expectedData.Path;

                // Valid paths
                if (expectedData.Index < 8)
                {
                    Assert.IsTrue(PathValidator.Instance.IsValid(stringToValidate), $"Path '{stringToValidate}' should be valid.");
                    PathValidator.Instance.SwitchSlashMode();
                    Assert.IsTrue(PathValidator.Instance.IsValid(stringToValidate.Replace(Chars.BSlash, Chars.FSlash)), $"Path '{stringToValidate}' should be valid.");
                }
                else 
                {
                    // Invalid paths
                    Assert.IsFalse(PathValidator.Instance.IsValid(stringToValidate), $"Path '{stringToValidate}' should not be valid.");
                    PathValidator.Instance.SwitchSlashMode();
                    
                    stringToValidate = stringToValidate.Replace(Chars.BSlash, Chars.FSlash);
                    
                    // Replace all occurences of '\' for '/' and replace '/' for '\'
                    if (expectedData.Index >= 10)
                    {
                        var stringToReplace = stringToValidate[stringToValidate.LastIndexOf(Chars.FSlash)..];
                        stringToValidate = string.Concat(stringToValidate.AsSpan(0, stringToValidate.LastIndexOf(Chars.FSlash) - 1), stringToReplace.Replace(Chars.FSlash, Chars.BSlash));
                    }
                    
                    Assert.IsFalse(PathValidator.Instance.IsValid(stringToValidate), $"Path '{stringToValidate}' should not be valid.");
                }

                PathValidator.Instance.SwitchSlashMode();
            }
        }

        /// <summary>
        /// Prepares test data.
        /// </summary>
        /// <returns>List of test strings.</returns>
        private static List<string> GetTestData()
        {
            return new List<string>()
            {
                // Valid 
                "C:",
                Directory.GetCurrentDirectory() + "\\Test_Lol",
                Directory.GetCurrentDirectory() + "\\Test",
                Directory.GetCurrentDirectory() + "\\Test\\file1.txt",
                Directory.GetCurrentDirectory() + "\\Test\\ss",
                Directory.GetCurrentDirectory() + "\\_Test.csv",
                Directory.GetCurrentDirectory() + "\\Test dir",
                Directory.GetCurrentDirectory() + "\\Test+Test",

                // Invalid
                Directory.GetCurrentDirectory() + "\\Test  ",
                Directory.GetCurrentDirectory() + "\\Test*Fail",
                Directory.GetCurrentDirectory() + "\\Test\\Test/v",
                Directory.GetCurrentDirectory() + "\\>Test File /"
            };
        }
    }
}