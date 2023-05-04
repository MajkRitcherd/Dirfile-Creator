// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 28/04/2022     \\

using System.Text.RegularExpressions;
using DirfileOperations = Dirfile_lib.Core.Constants.DirFile.Operations;

namespace Dirfile_lib.Utilities.Validation
{
    /// <summary>
    /// Abstract class for custom regex validators.
    /// </summary>
    internal abstract class AbstractRegexValidator : AbstractBaseValidator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractRegexValidator"/> class.
        /// </summary>
        protected AbstractRegexValidator()
            : base()
        {
            this.Initialize();
        }

        /// <summary>
        /// Gets or sets pattern used in regex.
        /// </summary>
        protected string _Pattern { get; set; } = null;

        /// <summary>
        /// Gets or sets instance of the <see cref="Regex"/> class.
        /// </summary>
        protected Regex _Regex { get; set; } = null;

        /// <summary>
        /// Initializes instance.
        /// </summary>
        internal abstract void Initialize();

        /// <inheritdoc/>
        public override bool IsValid(string stringToValidate) => this._Regex.IsMatch(stringToValidate) && !EndsWithControlCharacter(stringToValidate);

        /// <summary>
        /// Validates whether string ends with control character.
        /// </summary>
        /// <param name="stringToValidate">String to validate.</param>
        /// <returns>True, if ends with an operator, otherwise false.</returns>
        private bool EndsWithControlCharacter(string stringToValidate)
        {
            return stringToValidate.EndsWith(DirfileOperations.Change) ||
                   stringToValidate.EndsWith(DirfileOperations.Next) ||
                   stringToValidate.EndsWith(DirfileOperations.Prev) ||
                   stringToValidate.EndsWith(DirfileOperations.StartOfText) ||
                   stringToValidate.EndsWith(DirfileOperations.EndOfText);
        }
    }
}