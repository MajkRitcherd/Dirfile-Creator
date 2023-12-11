// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 28/04/2023     \\

using System;
using System.Linq;
using Dirfile_lib.Extensions;
using Dirfile_lib.Exceptions;
using TextConsts = Dirfile_lib.Core.Constants.Texts;

namespace Dirfile_lib.Utilities
{
    /// <summary>
    /// <see cref="ExtensionFinder"/> implements IOverEnum to find item in enums.
    /// </summary>
    internal class ExtensionFinder : IOverEnums
    {
        /// <inheritdoc/>
        public object FindOverEnums<TSearch>(TSearch extensionToFind)
        {
            if (typeof(TSearch) != typeof(string) && typeof(TSearch) != typeof(object))
                throw new DirfileException($"T cannot be other than string or object [actual: {extensionToFind.GetType()}]");

            var dirfileExtensionType = typeof(DirfileExtensions);
            var dirfileExtensionProps = dirfileExtensionType.GetMembers();

            // Loops over every member of a class
            foreach (var dirfilePropertyAndIndex in dirfileExtensionProps.Select((Property, Index) => new { Property, Index }))
            {
                if (dirfilePropertyAndIndex.Property.DeclaringType.Name != TextConsts.DirfileProps.DirfileExtensions || dirfilePropertyAndIndex.Property.Name == TextConsts.DirfileProps.Constructor)
                    continue;

                // Finds DirfileExtensions class.
                var dirfileClass = (Type)dirfileExtensionProps.Where(_ => true).ElementAt(dirfilePropertyAndIndex.Index);

                // Loops over enum in DirfileExtensions class and tries to find the given extension.
                foreach (var extensionString in Enum.GetNames(dirfileClass))
                {
                    if (extensionString.Equals(extensionToFind.ToString().Substring(1).ToUpperInvariant(), StringComparison.InvariantCultureIgnoreCase))
                        return Enum.Parse(dirfileClass, extensionString);
                }
            }

            throw new DirfileException($"Extension [{extensionToFind.ToString().Substring(1).ToUpperInvariant()}] was not found in enums.");
        }
    }
}