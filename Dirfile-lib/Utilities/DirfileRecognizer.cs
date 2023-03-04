// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 01/03/2023     \\

using System.Runtime.CompilerServices;
using Dirfile_lib.Utilities.Checks;
using CT = Dirfile_lib.Core.Constants.Texts;

[assembly: InternalsVisibleTo("Dirfile-Creator-TEST")]

namespace Dirfile_lib.Utilities
{
    /// <summary>
    /// Can recognize if given string is filer or director.
    /// </summary>
    internal class DirfileNameRecognizer
    {
        /// <summary>
        /// Gets <see cref="ExtensionChecker"/> class.
        /// </summary>
        private readonly ExtensionChecker _ExtensionChecker = new ExtensionChecker();

        /// <summary>
        /// Gets or sets the string to recognize.
        /// </summary>
        private string _StringToRecognize { get; set; }

        /// <summary>
        /// Recognizes if given string is Filer or Director.
        /// </summary>
        /// <param name="strToRecognize">Text to check.</param>
        /// <param name="dirfileName">Out parameter: Returns name.</param>
        /// <param name="extensionStr">Out parameter: Returns extension.</param>
        /// <returns>Recognizes if given string is extension or not.</returns>
        public string Recognize(string strToRecognize, out string dirfileName, out string extensionStr, out bool isValidExtension)
        {
            this._StringToRecognize = strToRecognize;
            var dotIndex = this._StringToRecognize.LastIndexOf('.');

            var dirfileType = this.GetData(dotIndex, out dirfileName, out extensionStr, out isValidExtension);
            this._StringToRecognize = null;

            return dirfileType;
        }

        /// <summary>
        /// Gets data from string
        /// </summary>
        /// <param name="index">Index of a '.' in name.</param>
        /// <param name="strToRecognize">String to recognize.</param>
        /// <param name="dirfileName">Name of a Filer or Director.</param>
        /// <param name="extensionString">Extension name.</param>
        /// <param name="isValidExtension">True, if extension is valid, otherwise false.</param>
        /// <returns>Type of a Dirfilte, either Filer or Director.</returns>
        private string GetData(int index, out string dirfileName, out string extensionString, out bool isValidExtension)
        {
            if (index == -1)
            {
                isValidExtension = false;
                dirfileName = this._StringToRecognize;
                extensionString = string.Empty;
                return CT.Director;
            }
            else
            {
                var extension = this._StringToRecognize.Substring(index);
                isValidExtension = this.IsValidExtension(extension);

                if (isValidExtension)
                {
                    dirfileName = this._StringToRecognize.Substring(0, index);
                    extensionString = extension;
                    return CT.Filer;
                }
                else
                {
                    dirfileName = this._StringToRecognize;
                    extensionString = string.Empty;
                    this._StringToRecognize = null;
                    return CT.Director;
                }
            }
        }

        /// <summary>
        /// Checks whether given string is extension or not.
        /// </summary>
        /// <param name="extension">Text to check.</param>
        /// <returns>True if it's extension, otherwise false.</returns>
        private bool IsValidExtension(string extension)
        {
            return this._ExtensionChecker.IsValid(extension);
        }
    }
}