// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 03/03/2022     \\

using System.IO;
using System.Linq;
using Dirfile_lib.Exceptions;

namespace Dirfile_lib.API.Extraction
{
    /// <summary>
    /// Mode of allowed slashes in path string.
    /// </summary>
    internal enum SlashMode
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
    /// Extracts Filers, Directors and path from given input.
    /// </summary>
    internal class Extractor
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Extractor"/> class.
        /// </summary>
        public Extractor(SlashMode mode)
        {
            this._NormalizedInputString = string.Empty;
            this._NormalizedDirectorPath = string.Empty;
            this._NormalizedArguments = string.Empty;
            this.ExtractMode = mode;
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
        /// Gets or sets the extract mode;
        /// </summary>
        internal SlashMode ExtractMode { get; set; }

        /// <summary>
        /// Gets the input string.
        /// </summary>
        internal string InputString => (this.ExtractMode == SlashMode.Backward) ? this._NormalizedInputString : this._NormalizedInputString.Replace("\\", "/");

        /// <summary>
        /// Gets of sets normalized arguments.
        /// </summary>
        private string _NormalizedArguments { get; set; }

        /// <summary>
        /// Gets or sets normalized director path.
        /// </summary>
        private string _NormalizedDirectorPath { get; set; }

        /// <summary>
        /// Gets or sets normalized input.
        /// </summary>
        private string _NormalizedInputString { get; set; }

        /// <summary>
        /// Extract the input string.
        /// </summary>
        /// <param name="input">Input string to extract.</param>
        public void ExtractInput(string input)
        {
            if (!IsInputConsistent(input))
                throw new DirfileException($"Input is not consistent: {input}");

            this._NormalizedInputString = input;

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

        /// <summary>
        /// Checks whether passed input is consistent, i.e. have only '/' or '\'.
        /// </summary>
        /// <param name="input">Input string to check.</param>
        /// <returns>True, if consistent, otherwise false.</returns>
        private bool IsInputConsistent(string input)
        {
            if (this.ExtractMode == SlashMode.Forward)
                return !(input.Where(ch => ch == '\\').Any()) && input.Where(ch => ch == '/').Count() > 0;
            else
                return input.Where(ch => ch == '\\').Count() > 0 && !(input.Where(ch => ch == '/').Any());
        }

        /// <summary>
        /// Normalizes the input string, so it workds only with '\' character.
        /// </summary>
        private void NormalizeInput() => this._NormalizedInputString = this._NormalizedInputString.Replace("/", "\\");
    }
}