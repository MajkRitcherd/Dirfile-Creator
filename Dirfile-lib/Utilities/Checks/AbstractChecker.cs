// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 21/02/2023     \\

namespace Dirfile_lib.Utilities.Checks
{
    /// <summary>
    /// Abstract class for <see cref="ExtensionChecker"/> and <see cref="NameChecker"/> classes.
    /// </summary>
    internal abstract class AbstractChecker
    {
        /// <summary>
        /// Gets or sets whether check was successful or not.
        /// </summary>
        public bool SuccessfulCheck { get; private set; }

        /// <summary>
        /// Gets or sets extension of a Filer.
        /// </summary>
        public string DirfileExtension { get; protected set; }

        /// <summary>
        /// Gets or sets error message if check was unsuccessful.
        /// </summary>
        public string ErrorMsg { get; set; }

        /// <summary>
        /// Sets whether check was valid or not.
        /// </summary>
        /// <param name="strToCheck">String to check.</param>
        /// <returns>True if string is valid, otherwise false.</returns>
        public virtual bool IsValid(string strToCheck) => this.SuccessfulCheck = this.Check(strToCheck);

        /// <summary>
        /// Checks whether given string is valid or not.
        /// </summary>
        /// <param name="strToCheck">String to check.</param>
        /// <returns>True if string is valid, otherwise false.</returns>
        protected abstract bool Check(string strToCheck);

        /// <summary>
        /// Cleans members.
        /// </summary>
        public virtual void Clean()
        {
            this.ErrorMsg = null;
            this.DirfileExtension = null;
        }
    }
}