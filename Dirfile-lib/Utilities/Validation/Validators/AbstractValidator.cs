// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 07/03/2023     \\

namespace Dirfile_lib.Utilities.Validation
{
    /// <summary>
    /// Abstract class for <see cref="ExtensionChecker"/> and <see cref="NameChecker"/> classes.
    /// </summary>
    internal abstract class AbstractValidator : AbstractBaseValidator, IValidation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractValidator"/> class.
        /// </summary>
        protected AbstractValidator()
            : base()
        {
            this.DirfileExtension = null;
        }

        /// <summary>
        /// Gets or sets extension of a Filer.
        /// </summary>
        public string DirfileExtension { get; protected set; }

        /// <inheritdoc/>
        public override void Clean()
        {
            base.Clean();

            this.DirfileExtension = null;
        }

        /// <inheritdoc/>
        public bool IsInvalid(string strToValidate) => !this.IsValid(strToValidate);

        /// <inheritdoc/>
        public bool IsValid(string strToCheck) => (this.Successful = this.Validate(strToCheck)).Value;

        /// <summary>
        /// Checks whether given string is valid or not.
        /// </summary>
        /// <param name="strToCheck">String to check.</param>
        /// <returns></returns>
        protected abstract bool Validate(string strToCheck);
    }
}