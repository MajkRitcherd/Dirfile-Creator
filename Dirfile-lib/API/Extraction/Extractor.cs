// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 24/04/2022     \\

using System.IO;
using Dirfile_lib.API.Extraction.Modes;
using Dirfile_lib.Exceptions;
using Chars = Dirfile_lib.Core.Constants.DirFile.Characters;
using DirfileOperations = Dirfile_lib.Core.Constants.DirFile.Operations;

namespace Dirfile_lib.API.Extraction
{
    /// <summary>
    /// Extracts arguments and path of existing Director from input.
    /// </summary>
    internal class Extractor : BaseExtractor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Extractor"/> class.
        /// </summary>
        /// <param name="mode">Slash mode to use.</param>
        internal Extractor(SlashMode mode)
            : base(mode)
        {
            this._NormalizedInputString = null;
            this.NormalizedDirectorPath = null;
            this.NormalizedArgumentString = null;
        }

        /// <summary>
        /// Gets string with arguments from input.
        /// </summary>
        internal string ArgumentString => (this.ExtractMode == SlashMode.Backward) ? this.NormalizedArgumentString : this.NormalizedArgumentString.Replace(Chars.BSlash, Chars.FSlash);

        /// <summary>
        /// Gets the director's path.
        /// </summary>
        internal string DirectorPath => (this.ExtractMode == SlashMode.Backward) ? this.NormalizedDirectorPath : this.NormalizedDirectorPath.Replace(Chars.BSlash, Chars.FSlash);

        /// <summary>
        /// Gets or sets normalized arguments.
        /// </summary>
        internal string NormalizedArgumentString { get; private set; }

        /// <summary>
        /// Gets or sets normalized director path.
        /// </summary>
        internal string NormalizedDirectorPath { get; private set; }

        /// <inheritdoc/>
        internal override void Extract(string inputString)
        {
            if (!this.IsInputConsistent(inputString))
                throw new DirfileException($"Input is not consistent: {inputString}");

            this.ReceivedString = inputString;

            this.NormalizeInput();
            this.GetDirectorPath();
            this.GetArgumentString();
        }

        /// <summary>
        /// Extracts arguments.
        /// </summary>
        private void GetArgumentString() => this.NormalizedArgumentString = this._NormalizedInputString.Substring(this.NormalizedDirectorPath.Length);

        /// <summary>
        /// Extracts path to existing director.
        /// </summary>
        private void GetDirectorPath() => this.NormalizedDirectorPath = this.GetExistingDirectorPath(this._NormalizedInputString);

        /// <summary>
        /// Finds last directory which exists on drive.
        /// </summary>
        /// <param name="inputString">Input string.</param>
        /// <returns>Path to the director (existing).</returns>
        private string GetExistingDirectorPath(string inputString)
        {
            var arrowIndex = inputString.IndexOf(DirfileOperations.Next);

            if (arrowIndex == -1 && Directory.Exists(this._NormalizedInputString))
                return this._NormalizedInputString;

            int indexOfLastSlash = inputString.LastIndexOf(Chars.BSlash, arrowIndex == -1 ? inputString.Length - 1 : arrowIndex);
            this.NormalizedDirectorPath = inputString.Substring(0, indexOfLastSlash);

            if (!Directory.Exists(this.NormalizedDirectorPath))
                this.GetExistingDirectorPath(this.NormalizedDirectorPath);

            return this.NormalizedDirectorPath;
        }
    }
}