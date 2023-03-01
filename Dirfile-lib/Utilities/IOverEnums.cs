// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 01/03/2023     \\

using Dirfile_lib.Exceptions;

namespace Dirfile_lib.Utilities
{
    /// <summary>
    /// Provides function to find object in Enums of a class.
    /// </summary>
    internal interface IOverEnums
    {
        /// <summary>
        /// Tries to find given argument in Enums.
        /// </summary>
        /// <param name="searchObj">Object to search for, either string or object</param>
        /// <exception cref="DirfileException">Thrown if given argument is other type than expected or when nothing is found.</exception>
        /// <returns>Object found in Enums.</returns>
        object FindOverEnums<TSearch>(TSearch searchObj);
    }
}
