// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 09/03/2022     \\

using System.IO;
using Dirfile_lib.Exceptions;

namespace Dirfile_lib.API.Extraction
{
    /// <summary>
    /// Extracts Filers, Directors and path from given input.
    /// </summary>
    internal class Extractor : BaseExtractor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Extractor"/> class.
        /// </summary>
        public Extractor(SlashMode mode)
            : base(mode)
        {
            this._NormalizedInputString = null;
            this._NormalizedDirectorPath = null;
            this._NormalizedArguments = null;
        }

        /// <summary>
        /// Gets string with arguments from input.
        /// </summary>
        internal string Arguments => (this.ExtractMode == SlashMode.Backward) ? this._NormalizedArguments : this._NormalizedArguments.Replace("\\", "/");

        /// <summary>
        /// Gets the director's path.
        /// </summary>
        internal string DirectorPath => (this.ExtractMode == SlashMode.Backward) ? this._NormalizedDirectorPath : this._NormalizedDirectorPath.Replace("\\", "/");

        /// <summary>
        /// Gets of sets normalized arguments.
        /// </summary>
        private string _NormalizedArguments { get; set; }

        /// <summary>
        /// Gets or sets normalized director path.
        /// </summary>
        private string _NormalizedDirectorPath { get; set; }

        /// <inheritdoc/>
        internal override void Extract(string input)
        {
            if (!IsInputConsistent(input))
                throw new DirfileException($"Input is not consistent: {input}");

            this.InputString = input;

            this.NormalizeInput();
            this.GetDirectorPath();
            this.GetArguments();
        }

        /// <summary>
        /// Extracts arguments.
        /// </summary>
        private void GetArguments() => this._NormalizedArguments = this._NormalizedInputString.Substring(this._NormalizedDirectorPath.Length);

        /// <summary>
        /// Extracts path to existing director.
        /// </summary>
        private void GetDirectorPath() => this._NormalizedArguments = this.GetExistingDirectorPath(this._NormalizedInputString);

        /// <summary>
        /// Finds last directory which exists on drive.
        /// </summary>
        /// <param name="input">Input string.</param>
        /// <returns>Path to the director (existing).</returns>
        private string GetExistingDirectorPath(string input)
        {
            var arrowIndex = input.IndexOf('>');
            int lastSlash = input.LastIndexOf('\\', arrowIndex == -1 ? input.Length - 1 : arrowIndex);
            this._NormalizedDirectorPath = input.Substring(0, lastSlash);

            if (!Directory.Exists(this._NormalizedDirectorPath))
                this.GetExistingDirectorPath(this._NormalizedDirectorPath);

            return this._NormalizedDirectorPath;
        }
    }
}