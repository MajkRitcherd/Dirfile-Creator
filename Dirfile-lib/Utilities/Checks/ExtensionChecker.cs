// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 28/02/2023     \\

using System;
using System.Linq;
using Dirfile_lib.Core;

namespace Dirfile_lib.Utilities.Checks
{
    /// <summary>
    /// <see cref="ExtensionChecker"/> checks whether extension is valid.
    /// </summary>
    internal class ExtensionChecker : AbstractChecker
    {
        /// <inheritdoc/>
        protected override bool Check(string strToCheck)
        {
            var dirType = typeof(DirfileExtensions);
            var dirProps = dirType.GetMembers();

            // Loops over every member of a class
            foreach (var prop in dirProps.Select((value, index) => new { value, index }))
            {
                if (prop.value.DeclaringType.Name != "DirfileExtensions" || prop.value.Name == ".ctor")
                    continue;

                var member = (Type)dirProps.Where(_ => true).ElementAt(prop.index);

                // Loops over every enum defined in that class
                foreach (var item in Enum.GetNames(member))
                {
                    if (("." + item.ToString()).Equals(strToCheck, StringComparison.InvariantCultureIgnoreCase))
                        return true;
                }
            }

            return false;
        }
    }
}
