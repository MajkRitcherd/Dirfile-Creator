// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 05/04/2022     \\

using System.Text.RegularExpressions;
using CT = Dirfile_lib.Core.Constants.Texts;

namespace Dirfile_lib.Utilities.Validation
{
    /// <summary>
    /// Abstract class for custom regex validators.
    /// </summary>
    internal abstract class AbstractRegexValidator : AbstractBaseValidator, IValidation
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
        public abstract void Initialize();

        /// <inheritdoc/>
        public bool IsInvalid(string strToValidate) => !this.IsValid(strToValidate);

        /// <inheritdoc/>
        public bool IsValid(string strToValidate) => this._Regex.IsMatch(strToValidate) && !EndsWithControlCharacter(strToValidate);

        /// <summary>
        /// Checks whether string ends with control character.
        /// </summary>
        /// <param name="strToValidate">String to validate.</param>
        /// <returns>True, if ends with an operator, otherwise false.</returns>
        private bool EndsWithControlCharacter(string strToValidate) => strToValidate.EndsWith(CT.BSlash) || strToValidate.EndsWith(">") || strToValidate.EndsWith(":>");
    }
}