// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 24/04/2022     \\

using System.Linq;
using Dirfile_lib.API.Extraction.Modes;
using Chars = Dirfile_lib.Core.Constants.DirFile.Characters;

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
        internal SlashMode ExtractMode { get; set; }

        /// <summary>
        /// Gets or sets Received input.
        /// </summary>
        internal string ReceivedString { get; set; }

        /// <summary>
        /// Gets or sets normalized input.
        /// </summary>
        protected string _NormalizedInputString { get; set; }

        /// <summary>
        /// Extracts the input string.
        /// </summary>
        /// <param name="inputString">Input string to extract.</param>
        internal abstract void Extract(string inputString);

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
        /// Checks whether passed input is consistent (has only '/' or '\' based on SlashMode).
        /// </summary>
        /// <param name="stringToCheck">Input string to check.</param>
        /// <returns>True, if consistent, otherwise false.</returns>
        protected bool IsInputConsistent(string stringToCheck)
        {
            if (this.ExtractMode == SlashMode.Forward)
                return !(stringToCheck.Where(ch => ch == Chars.BSlash).Any()) && stringToCheck.Where(ch => ch == Chars.FSlash).Count() > 0;
            else
                return stringToCheck.Where(ch => ch == Chars.BSlash).Count() > 0 && !(stringToCheck.Where(ch => ch == Chars.FSlash).Any());
        }

        /// <summary>
        /// Normalizes the input string, so it works only with '\' character.
        /// </summary>
        protected void NormalizeInput() => this._NormalizedInputString = this.ReceivedString.Replace(Chars.FSlash, Chars.BSlash).Trim();
    }
}