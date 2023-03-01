// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 20/12/2022     \\

using System;
using System.IO;
using Dirfile_lib.API.Extraction;
using Dirfile_lib.Core.Dirfiles;
using Dirfile_lib.Exceptions;

namespace Dirfile_lib.API.Context
{
    /// <summary>
    /// Base class for Dirfile context.
    /// </summary>
    public abstract class BaseContext : IDisposable
    {
        /// <summary>
        /// Gets or sets the current path of the context.
        /// </summary>
        public string CurrentPath { get; set; }

        /// <summary>
        /// Gets or sets the current direcotr of the context.
        /// </summary>
        internal Director CurrentDirector { get; set; }

        /// <summary>
        /// Gets or sets the initial path of the context.
        /// </summary>
        public string InitialPath { get; set; }

        /// <summary>
        /// Gets or sets the Extractor.
        /// </summary>
        internal Extractor Extractor { get; set; }

        /// <summary>
        /// Gets or sets the remaining dirfiles to create (if they do not exist)
        /// </summary>
        protected string RemainingDirfiles { get; set; }

        /// <summary>
        /// Gets all available drives.
        /// </summary>
        public DriveInfo[] Disks => DriveInfo.GetDrives();

        /// <summary>
        /// Initializes a context.
        /// </summary>
        protected abstract void Initialize();

        /// <summary>
        /// Changes the actual director and sets current path.
        /// </summary>
        /// <param name="newPath">New path of director.</param>
        /// <returns>Current path.</returns>
        protected virtual string DirChange(string newPath)
        {
            this.CurrentDirector = new Director(newPath);
            this.CurrentPath = this.CurrentDirector.FullName;

            return this.CurrentPath;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, 
        ///     releasing, or resetting unmanaged resources.
        /// </summary>
        /// <exception cref="DirfileException">Thrown if method is not rewritten!</exception>
        public virtual void Dispose()
        {
            throw new DirfileException("This method must be rewritten!");
        }
    }
}
