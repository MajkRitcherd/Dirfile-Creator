// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 20/12/2022     \\

using System.Text.RegularExpressions;

namespace Dirfile_lib.Utilities.Checks
{
    // Validates whole path to the Director of Filer.
    internal static class PathValidator
    {
        /// <summary>
        /// Gets regex to validate path to the director.
        /// </summary>
        private static readonly Regex _DirectorRegex = new Regex(@"^(?:[\w]\:|\\)(\\[a-zA-Z_\-\s0-9\.]+)*$");

        /// <summary>
        /// Validates path.
        /// </summary>
        /// <param name="path">Path to the director or filer.</param>
        /// <returns>True, if path is valid, otherwise false.</returns>
        public static bool IsValid(string path)
        {
            return IsValidInternal(path);
        }

        /// <summary>
        /// Does validation of path.
        /// </summary>
        /// <param name="path">Path to the director or filer.</param>
        /// <returns>True, if path is valid, otherwise false.</returns>
        private static bool IsValidInternal(string path)
        {
            return _DirectorRegex.IsMatch(path);
        }
    }
}