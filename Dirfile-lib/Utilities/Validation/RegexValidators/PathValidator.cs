// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 07/03/2023     \\

using System;
using System.Text.RegularExpressions;
using CRP = Dirfile_lib.Core.Constants.RegexPatterns;

namespace Dirfile_lib.Utilities.Validation
{
    // Validates whole path to the Director of Filer.
    internal class PathValidator : AbstractRegexValidator
    {
        /// <summary>
        /// 
        /// </summary>
        private static readonly Lazy<PathValidator> _Validator = new Lazy<PathValidator>(() => new PathValidator());

        /// <summary>
        /// 
        /// </summary>
        public static PathValidator Instance => _Validator.Value;

        /// <summary>
        /// Initializes a new instance of the <see cref="PathValidator"/> class.
        /// </summary>
        private PathValidator()
            : base()
        {
        }

        /// <inheritdoc/>
        public override void Initialize()
        {
            this._Pattern = CRP.DirfilePathPattern;
            this._Regex = new Regex(this._Pattern);
        }
    }
}