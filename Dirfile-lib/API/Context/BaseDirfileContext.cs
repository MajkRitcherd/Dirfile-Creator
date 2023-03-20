// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 20/03/2023     \\

using System;
using System.IO;
using Dirfile_lib.API.Extraction;
using Dirfile_lib.Core.Dirfiles;
using Dirfile_lib.Exceptions;
using Dirfile_lib.Utilities.Validation;

namespace Dirfile_lib.API.Context
{
    /// <summary>
    /// Base class for Dirfile context.
    /// </summary>
    public class BaseDirfileContext : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseDirfileContext"/> class.
        /// </summary>
        protected BaseDirfileContext()
        {
            this.Initialize(Directory.GetCurrentDirectory(), SlashMode.Backward);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseDirfileContext"/> class.
        /// </summary>
        /// <param name="path">Path to director.</param>
        /// <param name="slashMode">Slash mode to use.</param>
        protected BaseDirfileContext(string path, SlashMode slashMode)
        {
            this.Initialize(path, slashMode);
        }

        /// <summary>
        /// Gets or sets the current direcotr of the context.
        /// </summary>
        internal Director CurrentDirector { get; set; }

        /// <summary>
        /// Gets or sets the Disk manager.
        /// </summary>
        internal DiskManager DiskManager { get; set; }

        /// <summary>
        /// Gets or sets the Extractor.
        /// </summary>
        internal Extractor Extractor { get; set; }

        /// <summary>
        /// Creates Director.
        /// </summary>
        /// <param name="dirName">Name of a new director.</param>
        public virtual void CreateDirector(string dirName) => DirfileCreator.Instance.CreateDirector(this.CurrentDirector.Path, dirName);

        /// <summary>
        /// Creates Director.
        /// </summary>
        /// <param name="path">Path to the director to create.</param>
        public virtual void CreateDirectorPath(string path) => DirfileCreator.Instance.CreateDirector(path);

        /// <summary>
        /// Deletes Director.
        /// </summary>
        /// <param name="dirName">Name of a director to delete.</param>
        public virtual void DeleteDirector(string dirName) => DirfileCreator.Instance.DeleteDirector(this.CurrentDirector.Path, dirName);

        /// <summary>
        /// Deletes Director.
        /// </summary>
        /// <param name="path">Path to the director to delete.</param>
        public virtual void DeleteDirectorPath(string path) => DirfileCreator.Instance.DeleteDirector(path);

        /// <summary>
        /// Creates Filer.
        /// </summary>
        /// <param name="dirName">Name of a new filer.</param>
        public virtual void CreateFiler(string dirName) => DirfileCreator.Instance.CreateFiler(this.CurrentDirector.Path, dirName);

        /// <summary>
        /// Creates Filer.
        /// </summary>
        /// <param name="path">Path to the filer to create.</param>
        public virtual void CreateFilerPath(string path) => DirfileCreator.Instance.CreateFiler(path);

        /// <summary>
        /// Deletes Filer.
        /// </summary>
        /// <param name="dirName">Name of a filer to delete.</param>
        public virtual void DeleteFiler(string dirName) => DirfileCreator.Instance.DeleteFiler(this.CurrentDirector.Path, dirName);

        /// <summary>
        /// Deletes Filer.
        /// </summary>
        /// <param name="path">Path to the filer to delete.</param>
        public virtual void DeleteFilerPath(string path) => DirfileCreator.Instance.DeleteFiler(path);

        /// <summary>
        /// Changes the actual director and sets current path.
        /// </summary>
        /// <param name="newPath">New path of director.</param>
        /// <returns>Current path.</returns>
        public virtual string DirectorChange(string newPath)
        {
            if (PathValidator.Instance.IsInvalid(newPath))
                throw new DirfileException($"Invalid path when changing director: {newPath}");

            this.CurrentDirector = new Director(newPath);
            this.DiskManager.ChangeDrive(this.CurrentDirector.Path.Substring(0, 1));

            return this.CurrentDirector.Path;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing,
        ///     releasing, or resetting unmanaged resources.
        /// </summary>
        /// <exception cref="DirfileException">Thrown if method is not rewritten!</exception>
        public virtual void Dispose()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Switches slash mode.
        /// </summary>
        public void SwitchSlashMode()
        {
            PathValidator.Instance.SwitchSlashMode();
            this.Extractor.SwitchExtractMode();
        }

        /// <summary>
        /// Initializes a context.
        /// </summary>
        protected virtual void Initialize(string path, SlashMode slashMode)
        {
            this.Extractor = new Extractor(slashMode);

            if (slashMode != SlashMode.Backward)
                PathValidator.Instance.SwitchSlashMode();

            if (PathValidator.Instance.IsInvalid(path))
                throw new DirfileException($"Invalid path when initializing: {path}");

            this.Extractor.Extract(path);

            this.CurrentDirector = new Director(this.Extractor.NormalizedDirectorPath);
            this.DiskManager = new DiskManager(this.Extractor.NormalizedDirectorPath.Substring(0, 1));
        }
    }
}