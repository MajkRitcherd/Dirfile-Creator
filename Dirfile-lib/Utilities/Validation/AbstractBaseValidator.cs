// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 28/04/2022     \\

namespace Dirfile_lib.Utilities.Validation
{
    /// <summary>
    /// Base abstract class for validators.
    /// </summary>
    internal abstract class AbstractBaseValidator : IValidation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractBaseValidator"/> class.
        /// </summary>
        protected AbstractBaseValidator()
        {
            this.ErrorMessage = null;
        }

        /// <summary>
        /// Gets or sets error message if validation was unsuccessful.
        /// </summary>
        internal string ErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets whether validation was successful or not.
        /// </summary>
        internal bool? IsSuccess { get; set; }

        /// <summary>
        /// Gets whether validation was unsuccessful.
        /// </summary>
        internal bool? IsUnsuccess => !this.IsSuccess;

        /// <summary>
        /// Cleans members of a class.
        /// </summary>
        internal virtual void Clean()
        {
            this.ErrorMessage = null;
            this.IsSuccess = null;
        }

        /// <inheritdoc/>
        public abstract bool IsValid(string stringToValidate);

        /// <inheritdoc/>
        public virtual bool IsInvalid(string stringToValidate) => !this.IsValid(stringToValidate);
    }
}