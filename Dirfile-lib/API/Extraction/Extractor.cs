// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 22/03/2022     \\

using System.IO;
using Dirfile_lib.Exceptions;

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
        public Extractor(SlashMode mode)
            : base(mode)
        {
            this._NormalizedInputString = null;
            this.NormalizedDirectorPath = null;
            this.NormalizedArguments = null;
        }

        /// <summary>
        /// Gets string with arguments from input.
        /// </summary>
        internal string Arguments => (this.ExtractMode == SlashMode.Backward) ? this.NormalizedArguments : this.NormalizedArguments.Replace("\\", "/");

        /// <summary>
        /// Gets the director's path.
        /// </summary>
        internal string DirectorPath => (this.ExtractMode == SlashMode.Backward) ? this.NormalizedDirectorPath : this.NormalizedDirectorPath.Replace("\\", "/");

        /// <summary>
        /// Gets of sets normalized arguments.
        /// </summary>
        internal string NormalizedArguments { get; private set; }

        /// <summary>
        /// Gets or sets normalized director path.
        /// </summary>
        internal string NormalizedDirectorPath { get; private set; }

        /// <inheritdoc/>
        public override void Extract(string input)
        {
            if (!this.IsInputConsistent(input))
                throw new DirfileException($"Input is not consistent: {input}");

            this.InputString = input;

            this.NormalizeInput();
            this.GetDirectorPath();
            this.GetArguments();
        }

        /// <summary>
        /// Extracts arguments.
        /// </summary>
        private void GetArguments() => this.NormalizedArguments = this._NormalizedInputString.Substring(this.NormalizedDirectorPath.Length);

        /// <summary>
        /// Extracts path to existing director.
        /// </summary>
        private void GetDirectorPath() => this.NormalizedDirectorPath = this.GetExistingDirectorPath(this._NormalizedInputString);

        /// <summary>
        /// Finds last directory which exists on drive.
        /// </summary>
        /// <param name="input">Input string.</param>
        /// <returns>Path to the director (existing).</returns>
        private string GetExistingDirectorPath(string input)
        {
            var arrowIndex = input.IndexOf('>');

            if (arrowIndex == -1 && Directory.Exists(this._NormalizedInputString))
                return this._NormalizedInputString;

            int lastSlash = input.LastIndexOf('\\', arrowIndex == -1 ? input.Length - 1 : arrowIndex);
            this.NormalizedDirectorPath = input.Substring(0, lastSlash);

            if (!Directory.Exists(this.NormalizedDirectorPath))
                this.GetExistingDirectorPath(this.NormalizedDirectorPath);

            return this.NormalizedDirectorPath;
        }
    }
}