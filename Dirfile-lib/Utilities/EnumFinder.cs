// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 01/03/2023     \\

using System;
using System.Linq;
using Dirfile_lib.Core;
using Dirfile_lib.Exceptions;

namespace Dirfile_lib.Utilities
{
    /// <summary>
    /// <see cref="EnumFinder"/> implements IOverEnum to find item in enums.
    /// </summary>
    internal class EnumFinder : IOverEnums
    {
        /// <inheritdoc/>
        public object FindOverEnums<TSearch>(TSearch searchObj)
        {
            if (typeof(TSearch) != typeof(string) && typeof(TSearch) != typeof(object))
                throw new DirfileException("T cannot be other than string or object");

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
                    if (item.Equals(searchObj.ToString().Substring(1).ToUpperInvariant(), StringComparison.InvariantCultureIgnoreCase))
                        return Enum.Parse(member, item);
                }
            }

            throw new DirfileException("Extension was not found in enums.");
        }
    }
}
