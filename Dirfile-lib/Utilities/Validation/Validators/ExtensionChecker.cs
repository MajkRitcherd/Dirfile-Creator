// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 07/03/2023     \\

using System;
using System.Linq;
using Dirfile_lib.Core;
using CT = Dirfile_lib.Core.Constants.Texts;

namespace Dirfile_lib.Utilities.Validation
{
    /// <summary>
    /// <see cref="ExtensionChecker"/> checks whether extension is valid.
    /// </summary>
    internal class ExtensionChecker : AbstractValidator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExtensionChecker"/> class.
        /// </summary>
        public ExtensionChecker()
            : base()
        {
        }

        /// <inheritdoc/>
        protected override bool Validate(string strToCheck)
        {
            var dirType = typeof(DirfileExtensions);
            var dirProps = dirType.GetMembers();

            // Loops over every member of a class
            foreach (var prop in dirProps.Select((value, index) => new { value, index }))
            {
                if (prop.value.DeclaringType.Name != CT.Props.DirfileExtensions || prop.value.Name == CT.Props.Constructor)
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