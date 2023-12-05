// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 05/12/2023     \\

using Dirfile_lib.API.Extraction.Modes;

namespace Dirfile_Creator_Graphical.Models
{
    /// <summary>
    /// Model for MainWindow.
    /// </summary>
    internal class MainWindowModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowModel"/> class.
        /// </summary>
        public MainWindowModel()
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
    }
}