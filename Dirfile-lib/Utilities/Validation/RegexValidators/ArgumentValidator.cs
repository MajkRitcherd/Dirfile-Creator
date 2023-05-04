// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 07/03/2023     \\

using System;
using System.Text.RegularExpressions;
using RegexConsts = Dirfile_lib.Core.Constants.RegexPatterns;

namespace Dirfile_lib.Utilities.Validation
{
    /// <summary>
    /// Parses string with arguments.
    /// </summary>
    internal class ArgumentValidator : AbstractRegexValidator
    {
        /// <summary>
        /// Lazily instantiate a new instance of the <see cref="ArgumentValidator"/> class.
        /// </summary>
        internal static readonly Lazy<ArgumentValidator> _Validator = new Lazy<ArgumentValidator>(() => new ArgumentValidator());

        /// <summary>
        /// Gets the <see cref="ArgumentValidator"/> instance.
        /// </summary>
        internal static ArgumentValidator Instance => _Validator.Value;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentValidator"/> class.
        /// </summary>
        internal ArgumentValidator()
            : base()
        {
        }

        /// <inheritdoc/>
        internal override void Initialize()
        {
            this._Pattern = RegexConsts.ArgumentsPattern;
            this._Regex = new Regex(this._Pattern);
        }
    }
}