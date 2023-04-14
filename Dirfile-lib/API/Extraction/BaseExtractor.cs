// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 05/04/2022     \\

using System.Linq;
using Dirfile_lib.API.Extraction.Modes;
using CT = Dirfile_lib.Core.Constants.Texts;

namespace Dirfile_lib.API.Extraction
{
    /// <summary>
    /// Abstract base class for extractors.
    /// </summary>
    internal abstract class BaseExtractor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseExtractor"/> class.
        /// </summary>
        /// <param name="mode">Slash mode to use.</param>
        protected BaseExtractor(SlashMode mode)
        {
            this.ExtractMode = mode;
        }

        /// <summary>
        /// Gets or sets the extract mode.
        /// </summary>
        public SlashMode ExtractMode { get; set; }

        /// <summary>
        /// Gets or sets Received input.
        /// </summary>
        public string InputString { get; set; }

        /// <summary>
        /// Gets or sets normalized input.
        /// </summary>
        protected string _NormalizedInputString { get; set; }

        /// <summary>
        /// Extracts the input string.
        /// </summary>
        /// <param name="input">Input string to extract.</param>
        public abstract void Extract(string input);

        /// <summary>
        /// Switches slash mode between '\' and '/'.
        /// </summary>
        internal void SwitchExtractMode()
        {
            switch (this.ExtractMode)
            {
                case SlashMode.Backward:
                    this.ExtractMode = SlashMode.Forward;
                    break;

                case SlashMode.Forward:
                    this.ExtractMode = SlashMode.Backward;
                    break;
            }
        }

        /// <summary>
        /// Checks whether passed input is consistent, i.w. have only '/' or '\' based on SlashMode.
        /// </summary>
        /// <param name="input">Input string to check.</param>
        /// <returns>True, if consistent, otherwise false.</returns>
        protected bool IsInputConsistent(string input)
        {
            if (this.ExtractMode == SlashMode.Forward)
                return !(input.Where(ch => ch == '\\').Any()) && input.Where(ch => ch == '/').Count() > 0;
            else
                return input.Where(ch => ch == '\\').Count() > 0 && !(input.Where(ch => ch == '/').Any());
        }

        /// <summary>
        /// Normalizes the input string, so it workds only with '\' character.
        /// </summary>
        protected void NormalizeInput() => this._NormalizedInputString = this.InputString.Replace(CT.FSlash, CT.BSlash).Trim();
    }
}