// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 26/04/2023     \\

using Dirfile_lib.Utilities.Validation;
using DirfileType = Dirfile_lib.Core.Constants.DirFile.Types;

namespace Dirfile_lib.Utilities
{
    /// <summary>
    /// Recognizes if given string is filer or director.
    /// </summary>
    internal class DirfileRecognizer
    {
        /// <summary>
        /// Gets <see cref="ExtensionValidator"/> class.
        /// </summary>
        private readonly ExtensionValidator _ExtensionChecker = new ExtensionValidator();

        /// <summary>
        /// Gets or sets the string to recognize.
        /// </summary>
        private string _StringToRecognize { get; set; }

        /// <summary>
        /// Recognizes if given string is Filer or Director.
        /// </summary>
        /// <param name="stringToRecognize">String to recognize.</param>
        /// <param name="dirfileName">Out parameter: Returns name of a Filer or Director.</param>
        /// <param name="extensionString">Out parameter: Returns Extension with dot represented by a string.</param>
        /// <returns>Type of a Dirfile, either Filer or Director.</returns>
        internal string Recognize(string stringToRecognize, out string dirfileName, out string extensionString, out bool isValidExtension)
        {
            this._StringToRecognize = stringToRecognize;
            var lastIndexOfDot = this._StringToRecognize.LastIndexOf('.');

            var dirfileType = this.GetDirfileData(lastIndexOfDot, out dirfileName, out extensionString, out isValidExtension);
            this._StringToRecognize = null;

            return dirfileType;
        }

        /// <summary>
        /// Gets data (type, name, extension name [if existts], extension [if existts]) from string to recognize.
        /// </summary>
        /// <param name="dotIndex">Index of a dot inside a string to recognize.</param>
        /// <param name="dirfileName">Name of a Filer or Director.</param>
        /// <param name="extensionString">Extension with dot represented by a string.</param>
        /// <param name="isValidExtension">True, if extension is valid, otherwise false.</param>
        /// <returns>Type of a Dirfile, either Filer or Director.</returns>
        private string GetDirfileData(int dotIndex, out string dirfileName, out string extensionString, out bool isValidExtension)
        {
            if (dotIndex == -1)
            {
                isValidExtension = false;
                dirfileName = this._StringToRecognize;
                extensionString = string.Empty;
                return DirfileType.Director;
            }
            else
            {
                var extension = this._StringToRecognize.Substring(dotIndex);
                isValidExtension = this.IsValidExtension(extension);

                if (isValidExtension)
                {
                    dirfileName = this._StringToRecognize.Substring(0, dotIndex);
                    extensionString = extension;
                    return DirfileType.Filer;
                }
                else
                {
                    dirfileName = this._StringToRecognize;
                    extensionString = string.Empty;
                    this._StringToRecognize = null;
                    return DirfileType.Director;
                }
            }
        }

        /// <summary>
        /// Validates whether given string is extension or not.
        /// </summary>
        /// <param name="extensionToValidate">Extension to validate.</param>
        /// <returns>True if it's extension, otherwise false.</returns>
        private bool IsValidExtension(string extensionToValidate) => this._ExtensionChecker.IsValid(extensionToValidate);
    }
}