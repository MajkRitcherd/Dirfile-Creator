// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 14/03/2022     \\

using System.Linq;

namespace Dirfile_lib.API.Extraction
{
    /// <summary>
    /// Mode of allowed slashes in path string.
    /// </summary>
    public enum SlashMode
    {
        /// <summary>
        /// Forward slash.
        /// </summary>
        Forward,

        /// <summary>
        /// Backward slash.
        /// </summary>
        Backward
    }

    /// <summary>
    /// Abstract base class for extractors.
    /// </summary>
    internal abstract class BaseExtractor
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="mode"></param>
        protected BaseExtractor(SlashMode mode)
        {
            this.ExtractMode = mode;
        }

        /// <summary>
        /// Gets or sets Received input.
        /// </summary>
        public string InputString { get; set; }

        /// <summary>
        /// Gets or sets normalized input.
        /// </summary>
        protected string _NormalizedInputString { get; set; }

        /// <summary>
        /// Gets or sets the extract mode.
        /// </summary>
        protected SlashMode ExtractMode { get; set; }

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
        /// Extract the input string.
        /// </summary>
        /// <param name="input">Input string to extract.</param>
        internal abstract void Extract(string input);

        /// <summary>
        /// Normalizes the input string, so it workds only with '\' character.
        /// </summary>
        protected void NormalizeInput() => this._NormalizedInputString = this.InputString.Replace("/", "\\").Trim();
    }
}