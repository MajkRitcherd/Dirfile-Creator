// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 26/04/2023     \\

using System.Collections.Generic;
using System.Linq;
using Dirfile_lib.Exceptions;
using ValueConsts = Dirfile_lib.Core.Constants.DefaultValues;
using Chars = Dirfile_lib.Core.Constants.DirFile.Characters;

namespace Dirfile_lib.Utilities.Validation
{
    /// <summary>
    /// <see cref="NameValidator"/> validates string (only name of Director of Filer, not full path)
    ///  and gets Dirfile name, extension and type if it is Filer, Director or emtpy string.
    /// </summary>
    /// <exception cref="DirfileException">Can throw exceptions.</exception>
    internal class NameValidator : AbstractValidator
    {
        /// <summary>
        /// Gets enum finder to find item in enums.
        /// </summary>
        private readonly ExtensionFinder _EnumFinder = new ExtensionFinder();

        /// <summary>
        /// List of invalid characters.
        /// </summary>
        private readonly List<char> _InvalidCharacters = new List<char>()
        {
            Chars.BSlash,
            Chars.Colon,
            Chars.FSlash,
            Chars.RightArrow,
            Chars.Quote,
            '<',
            '*',
            '?',
            '|'
        };

        /// <summary>
        /// List of invalid characters that are forbidden in Dirfile name
        ///  (name can't starts or ends with these symbols).
        /// </summary>
        private readonly List<char> _InvalidStartEnd = new List<char>()
        {
            ' ', '.', '-', '_'
        };

        /// <summary>
        /// Gets dirfile recognizer.
        /// </summary>
        private readonly DirfileRecognizer _Recognizer = new DirfileRecognizer();

        /// <summary>
        /// Initializes a new instance of the <see cref="NameValidator"/> class.
        /// </summary>
        internal NameValidator()
            : base()
        {
            this.DirfileType = null;
            this.DirfileName = null;
        }

        /// <summary>
        /// Gets or sets name of Filer or Director.
        /// </summary>
        internal string DirfileName { get; set; }

        /// <summary>
        /// Gets or sets type, represented as string, of checked string (If it is Filer or Director).
        /// </summary>
        internal string DirfileType { get; set; }

        /// <inheritdoc/>
        internal override void Clean()
        {
            base.Clean();

            this.DirfileType = null;
            this.DirfileName = null;
        }

        /// <summary>
        /// Converts extension from string to Extensions enum.
        /// </summary>
        /// <returns>Dirfile extension.</returns>
        /// <exception cref="DirfileException">Thrown if DirfileType is null.</exception>
        internal object GetExtension()
        {
            if (string.IsNullOrEmpty(this.ExtensionName))
                throw new DirfileException("Extension could not be an empty string.");

            return this._EnumFinder.FindOverEnums(this.ExtensionName);
        }

        /// <inheritdoc/>
        protected override bool Validate(string stringToCheck)
        {
            if (!this.ValidateString(stringToCheck))
                return false;

            this.SetProperties(stringToCheck);
            return true;
        }

        /// <summary>
        /// Validates whether string contains invalid characters.
        /// </summary>
        /// <param name="string">String to validate.</param>
        /// <returns>True if contains invalid characters, otherwise false.</returns>
        private bool ContainsInvalidCharacters(string stringToValidate) => stringToValidate.Any(character => this._InvalidCharacters.Contains(character));

        /// <summary>
        /// Validates whether string starts or ends with a space, period, hyphen or underline
        /// </summary>
        /// <param name="stringToValidate">String to validate.</param>
        /// <returns>True if contains wrong start or end, otherwise false.</returns>
        private bool InvalidStartOrEndOfName(string stringToValidate)
        {
            foreach (var invalidChar in this._InvalidStartEnd)
                if (stringToValidate.StartsWith(invalidChar.ToString()) || stringToValidate.EndsWith(invalidChar.ToString()))
                    return true;

            return false;
        }

        /// <summary>
        /// Validates whether string's length is greater than allowed value.
        /// </summary>
        /// <param name="str">String to validate.</param>
        /// <returns>True if it is greater, otherwise false.</returns>
        private bool IsNameLengthGreater(string stringToValidate) => stringToValidate.Length > ValueConsts.MaxNameLength;

        /// <summary>
        /// Sets properties.
        /// </summary>
        /// <param name="stringToValidate">String to validate.</param>
        private void SetProperties(string stringToValidate)
        {
            this.DirfileType = this._Recognizer.Recognize(stringToValidate, out string dirfileName, out string extensionString, out bool _);
            this.DirfileName = dirfileName;
            this.ExtensionName = extensionString;
        }

        /// <summary>
        /// Validates given string.
        /// </summary>
        /// <param name="stringToValidate">String to validate.</param>
        /// <returns>True if valid, otherwise false.</returns>
        private bool ValidateString(string stringToValidate)
        {
            if (this.ContainsInvalidCharacters(stringToValidate))
            {
                this.ErrorMessage = $"The input string [input: {stringToValidate}] contains invalid character/s. or starts/ends with invalid characters";
                return false;
            }

            if (this.InvalidStartOrEndOfName(stringToValidate))
            {
                this.ErrorMessage = $"The input string [input: {stringToValidate}] starts/ends with invalid characters";
                return false;
            }

            if (this.IsNameLengthGreater(stringToValidate))
            {
                this.ErrorMessage = $"The Dirfile name's length [input: {stringToValidate}] is greater than maximum allowed [30].";
                return false;
            }

            if (string.IsNullOrEmpty(stringToValidate))
            {
                this.ErrorMessage = $"The input string to validate in NameChecker is empty!";
                return false;
            }

            return true;
        }
    }
}