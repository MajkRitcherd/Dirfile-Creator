// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 07/03/2022     \\

namespace Dirfile_lib.Utilities.Validation
{
    /// <summary>
    /// Base abstract class for validators.
    /// </summary>
    internal abstract class AbstractBaseValidator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractBaseValidator"/> class.
        /// </summary>
        protected AbstractBaseValidator()
        {
            this.ErrorMsg = null;
        }

        /// <summary>
        /// Gets or sets error message if check was unsuccessful.
        /// </summary>
        public string ErrorMsg { get; set; }

        /// <summary>
        /// Gets or sets whether check was successful or not.
        /// </summary>
        public bool? Successful { get; protected set; }

        /// <summary>
        /// Gets whether check was unsuccessful.
        /// </summary>
        public bool? Unsuccessful => !this.Successful;

        /// <summary>
        /// Cleans members of a class.
        /// </summary>
        public virtual void Clean()
        {
            this.ErrorMsg = null;
            this.Successful = null;
        }
    }
}