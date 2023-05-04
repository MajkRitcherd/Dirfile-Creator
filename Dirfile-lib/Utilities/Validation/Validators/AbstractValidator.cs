// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 28/04/2023     \\

namespace Dirfile_lib.Utilities.Validation
{
    /// <summary>
    /// Abstract class for <see cref="ExtensionValidator"/> and <see cref="NameValidator"/> classes.
    /// </summary>
    internal abstract class AbstractValidator : AbstractBaseValidator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractValidator"/> class.
        /// </summary>
        protected AbstractValidator()
            : base()
        {
            this.ExtensionName = null;
        }

        /// <summary>
        /// Gets or sets extension of a Filer.
        /// </summary>
        internal string ExtensionName { get; set; }

        /// <inheritdoc/>
        internal override void Clean()
        {
            base.Clean();

            this.ExtensionName = null;
        }

        /// <inheritdoc/>
        public override bool IsValid(string stringToValidate) => (this.IsSuccess = this.Validate(stringToValidate)).Value;

        /// <summary>
        /// Validates whether given string is valid or not.
        /// </summary>
        /// <param name="stringToValidate">String to validate.</param>
        /// <returns>True, if string is valid, otherwise false.</returns>
        protected abstract bool Validate(string stringToValidate);
    }
}