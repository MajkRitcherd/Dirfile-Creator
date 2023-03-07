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
    /// <summary>
    /// Parses string with arguments.
    /// </summary>
    internal class ArgumentParser : AbstractRegexValidator
    {
        /// <summary>
        /// 
        /// </summary>
        private static readonly Lazy<ArgumentParser> _Parser = new Lazy<ArgumentParser>(() => new ArgumentParser());

        /// <summary>
        /// 
        /// </summary>
        public static ArgumentParser Instance => _Parser.Value;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentParser"/> class.
        /// </summary>
        private ArgumentParser()
            : base()
        {
        }

        /// <inheritdoc/>
        public override void Initialize()
        {
            this._Pattern = CRP.ArgumentsPattern;
            this._Regex = new Regex(this._Pattern);
        }
    }
}