﻿// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 17/03/2023     \\

using System;
using System.Text.RegularExpressions;
using Dirfile_lib.API.Extraction.Modes;
using RegexConsts = Dirfile_lib.Core.Constants.RegexPatterns;

namespace Dirfile_lib.Utilities.Validation
{
    // Validates whole path to the Director of Filer.
    internal class PathValidator : AbstractRegexValidator
    {
        /// <summary>
        /// Lazily instantiate a new instance of the <see cref="PathValidator"/> class.
        /// </summary>
        private static readonly Lazy<PathValidator> _Validator = new Lazy<PathValidator>(() => new PathValidator());

        /// <summary>
        /// Initializes a new instance of the <see cref="PathValidator"/> class.
        /// </summary>
        private PathValidator()
            : base()
        {
        }

        /// <summary>
        /// Gets the <see cref="PathValidator"/> instance.
        /// </summary>
        internal static PathValidator Instance => _Validator.Value;

        /// <summary>
        /// Gets or sets the slash mode.
        /// </summary>
        internal SlashMode SlashMode { get; set; } = SlashMode.Backward;

        /// <inheritdoc/>
        internal override void Initialize()
        {
            this._Pattern = RegexConsts.DirfileNormalizedPathPattern;
            this._Regex = new Regex(this._Pattern);
        }

        /// <summary>
        /// Switches slash mode between '\' and '/'.
        /// </summary>
        internal void SwitchSlashMode()
        {
            switch (this.SlashMode)
            {
                case SlashMode.Backward:
                    this.SlashMode = SlashMode.Forward;
                    this.ChangePattern();
                    break;

                case SlashMode.Forward:
                    this.SlashMode = SlashMode.Backward;
                    this.ChangePattern();
                    break;
            }
        }

        /// <summary>
        /// Changes the regex pattern.
        /// </summary>
        private void ChangePattern()
        {
            switch (this.SlashMode)
            {
                case SlashMode.Backward:
                    this._Pattern = RegexConsts.DirfileNormalizedPathPattern;
                    break;

                case SlashMode.Forward:
                    this._Pattern = RegexConsts.DirfilePathPattern;
                    break;
            }

            this._Regex = new Regex(this._Pattern);
        }
    }
}