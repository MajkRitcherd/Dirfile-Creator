// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 18/03/2023     \\

using Dirfile_lib.Utilities.Validation;
using Microsoft.Win32;
using System;

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
            Directory.GetCurrentDirectory() + "\\Test dir",
            Directory.GetCurrentDirectory() + "\\Test+Test",
            Directory.GetCurrentDirectory() + "\\Test  ",
            Directory.GetCurrentDirectory() + "\\Test*Fail",
            Directory.GetCurrentDirectory() + "\\Test\\Test/v",
            Directory.GetCurrentDirectory() + "\\>Test File /"
        };

        /// <summary>
        /// Tests path validating using <see cref="PathValidator"/> class.
        /// </summary>
        [TestMethod]
        public void TestValidator()
        {
            foreach (var item in _paths.Select((item, index) => new { item, index }))
            {
                var strToValidate = item.item;
                if (item.index < 8)
                {
                    Assert.IsTrue(PathValidator.Instance.IsValid(strToValidate), $"Path with index: {item.index} was badly classified.");
                    PathValidator.Instance.SwitchSlashMode();
                    Assert.IsTrue(PathValidator.Instance.IsValid(strToValidate.Replace("\\", "/")));
                }
                else
                {
                    Assert.IsFalse(PathValidator.Instance.IsValid(strToValidate), $"Path with index: {item.index} was badly classified.");
                    PathValidator.Instance.SwitchSlashMode();
                    
                    if (item.index >= 10)
                    {
                        strToValidate = strToValidate.Replace("\\", "/");
                        var strToReplace = strToValidate[strToValidate.LastIndexOf("/")..];
                        strToValidate = string.Concat(strToValidate.AsSpan(0, strToValidate.LastIndexOf("/") - 1), strToReplace.Replace("/", "\\"));
                    }
                    
                    Assert.IsFalse(PathValidator.Instance.IsValid(item.item.Replace("\\", "/")), $"Path with index: {item.index} was badly classified.");
                }

                PathValidator.Instance.SwitchSlashMode();
            }
        }
    }
}