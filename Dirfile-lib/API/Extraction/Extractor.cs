// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 13/12/2022     \\

using System.Collections.Generic;
using System.IO;
using Dirfile_lib.Exceptions;
using Dirfile_lib.Utilities.Checks;

namespace Dirfile_lib.API.Extraction
{
    /// <summary>
    /// Extracts Filers, Directors and path from given input.
    /// </summary>
    internal class Extractor
    {
        /// <summary>
        /// Gets the original path.
        /// </summary>
        internal string OriginalPath { get; private set; }

        /// <summary>
        /// Gets the variable path.
        /// </summary>
        internal string VariablePath { get; private set; }

        /// <summary>
        /// Gets or sets the extract mode;
        /// </summary>
        internal SlashMode ExtractMode { get; set; }

        /// <summary>
        /// Dictionary of SlastMode -> string.
        /// </summary>
        internal readonly Dictionary<SlashMode, string> SlashToString = new Dictionary<SlashMode, string>()
        {
            { SlashMode.Forward, "/" },
            { SlashMode.Backward, "\\" },
        };
        
        /// <summary>
        /// Initializes a new instance of <see cref="Extractor"/> class.
        /// </summary>
        public Extractor(SlashMode mode)
        {
            this.OriginalPath = string.Empty;
            this.VariablePath = string.Empty;
            this.ExtractMode = mode;
        }

        /// <summary>
        /// Extracts path to the director.
        /// </summary>
        /// <param name="str">Input string.</param>
        /// <returns>Path to the director.</returns>
        public string FindExistingPath(string str)
        {
            if (!PathValidator.IsValid(str))
                throw new DirfileException("Wrong path");

            this.OriginalPath = str;

            return this.FindExistringPathInternal(str);
        }

        /// <summary>
        /// Extracts path to the director.
        /// </summary>
        /// <param name="str">Input string.</param>
        /// <returns>Path to the director.</returns>
        private string FindExistringPathInternal(string str)
        {
            var arrowIndex = str.IndexOf('>');
            int lastSlash = 0;

            switch (this.ExtractMode)
            {
                case SlashMode.Forward:
                    lastSlash = str.LastIndexOf('/', arrowIndex == -1 ? str.Length - 1 : arrowIndex);
                    break;
                case SlashMode.Backward:
                    lastSlash = str.LastIndexOf('\\', arrowIndex == -1 ? str.Length - 1 : arrowIndex);
                    break;
                default:
                    break;
            }

            this.VariablePath = str.Substring(0, lastSlash);

            if (!Directory.Exists(this.VariablePath.Replace('/', '\\')))
                this.FindExistringPathInternal(this.VariablePath);

            return this.VariablePath;
        }

        /// <summary>
        /// Extracts all arguments from given string.
        /// </summary>
        /// <param name="str">Input string.</param>
        /// <returns>String with arguments.</returns>
        public string FindArguments()
        {
            if (!string.IsNullOrEmpty(this.OriginalPath))
                return string.Empty;
            else
                return this.OriginalPath.Substring(this.VariablePath.Length);
        }
    }

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
}
