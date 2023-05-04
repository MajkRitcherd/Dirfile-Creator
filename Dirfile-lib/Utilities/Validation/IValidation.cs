// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 06/03/2022     \\

namespace Dirfile_lib.Utilities.Validation
{
    /// <summary>
    /// Interface for vaildation.
    /// </summary>
    internal interface IValidation
    {
        /// <summary>
        /// Validates, whether given string is valid or not.
        /// </summary>
        /// <param name="stringToValidate">String to validate.</param>
        /// <returns>True, if valid, otherwise false.</returns>
        bool IsValid(string stringToValidate);

        /// <summary>
        /// Validates, whether given string is invalid or not.
        /// </summary>
        /// <param name="stringToValidate">String to validate.</param>
        /// <returns>True, if invalid, otherwise false.</returns>
        bool IsInvalid(string stringToValidate);
    }
}
