// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 11/12/2023     \\

using System;
using Dirfile_lib.API.Context;
using Dirfile_lib.API.Extraction.Modes;

namespace Dirfile_Creator_Graphical.Models
{
    /// <summary>
    /// Represents the dirfile model (Uses Dirfile context, etc.).
    /// </summary>
    internal class DirfileModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DirfileModel"/> class.
        /// </summary>
        public DirfileModel()
        {
        }

        /// <summary>
        /// Gets or sets the path mode to be used in context.
        /// </summary>
        public PathMode PathMode { get; set; }

        /// <summary>
        /// Gets or sets the slash mode to be used in context.
        /// </summary>
        public SlashMode SlashMode { get; set; }

        /// <summary>
        /// Gets or sets the input string.
        /// </summary>
        private string InputString { get; set; } = string.Empty;

        /// <summary>
        /// Creates dirfiles from input string.
        /// </summary>
        /// <param name="inputString">Input string.</param>
        public void CreateDirfiles(string inputString, string relativePath)
        {
            this.InputString = inputString;

            switch (this.PathMode)
            {
                case PathMode.Absolute:
                    this.CreateAbsolute();
                    break;

                case PathMode.Relative:
                    this.CreateRelative(relativePath);
                    break;

                default:
                    throw new Exception($"Path mode: '{this.PathMode}' is not defined in Dirfile lib.");
            }
        }

        /// <summary>
        /// Creates dirfiles using absolute dirfile context.
        /// </summary>
        /// <param name="inputString">Input string.</param>
        private void CreateAbsolute()
        {
            using var dirfileContext = new DirfileContext(this.SlashMode);
            dirfileContext.Create(this.InputString);
        }

        /// <summary>
        /// Creates dirfiles using relative dirfile context.
        /// </summary>
        /// <param name="inputString">Input string.</param>
        private void CreateRelative(string relativePath)
        {
            using var dirfileContext = new DirfileContext(relativePath, this.SlashMode);
            dirfileContext.Create(this.InputString);
        }
    }
}