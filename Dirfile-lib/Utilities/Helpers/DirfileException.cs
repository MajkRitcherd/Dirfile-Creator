// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 28/11/2022     \\

using System;

namespace Dirfile_lib.Exceptions
{
    /// <summary>
    /// Exceptions that are thrown during Dirfile execution
    /// </summary>
    internal class DirfileException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DirfileException"/> class.
        /// </summary>
        internal DirfileException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DirfileException"/> class.
        /// </summary>
        /// <param name="message">Message that describes the error.</param>
        internal DirfileException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DirfileException"/> class.
        /// </summary>
        /// <param name="message">Message that describes the error.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        internal DirfileException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}