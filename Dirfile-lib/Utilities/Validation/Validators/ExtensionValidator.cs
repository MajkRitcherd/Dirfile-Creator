// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 28/04/2023     \\

using System;
using System.Linq;
using Dirfile_lib.Extensions;
using TextConsts = Dirfile_lib.Core.Constants.Texts;

namespace Dirfile_lib.Utilities.Validation
{
    /// <summary>
    /// <see cref="ExtensionValidator"/> validates whether extension is valid or not.
    /// </summary>
    internal class ExtensionValidator : AbstractValidator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExtensionValidator"/> class.
        /// </summary>
        internal ExtensionValidator()
            : base()
        {
        }

        /// <inheritdoc/>
        protected override bool Validate(string stringToCheck)
        {
            var dirileType = typeof(DirfileExtensions);
            var dirfileProps = dirileType.GetMembers();

            // Loops over every member of a class
            foreach (var dirfilePropertyAndIndex in dirfileProps.Select((property, index) => new { property, index }))
            {
                if (dirfilePropertyAndIndex.property.DeclaringType.Name != TextConsts.Props.DirfileExtensions || dirfilePropertyAndIndex.property.Name == TextConsts.Props.Constructor)
                    continue;

                var dirfileClass = (Type)dirfileProps.Where(_ => true).ElementAt(dirfilePropertyAndIndex.index);

                // Loops over every enum defined in that class
                foreach (var extensionString in Enum.GetNames(dirfileClass))
                {
                    if (("." + extensionString.ToString()).Equals(stringToCheck, StringComparison.InvariantCultureIgnoreCase))
                        return true;
                }
            }

            return false;
        }
    }
}