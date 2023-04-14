// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 06/04/2023     \\

using System;
using System.IO;
using Dirfile_lib.API.Extraction;
using Dirfile_lib.API.Extraction.Modes;
using Dirfile_lib.Core.Dirfiles;
using Dirfile_lib.Exceptions;
using Dirfile_lib.Utilities.Validation;
using CT = Dirfile_lib.Core.Constants.Texts;

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
            this.Initialize(Directory.GetCurrentDirectory(), SlashMode.Backward, PathMode.Absolute);
        }

        protected BaseDirfileContext(SlashMode slashMode = SlashMode.Backward, PathMode pathMode = PathMode.Relative)
        {
            this.Initialize(slashMode == SlashMode.Backward ? Directory.GetCurrentDirectory() : Directory.GetCurrentDirectory().Replace(CT.BSlash, CT.FSlash), slashMode, pathMode);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseDirfileContext"/> class.
        /// </summary>
        /// <param name="path">Path to director to work from (like relative path).</param>
        /// <param name="slashMode">Slash mode to use.</param>
        protected BaseDirfileContext(string path, SlashMode slashMode, PathMode pathMode)
        {
            this.Initialize(path, slashMode, pathMode);
        }

        /// <summary>
        /// Gets or sets the current director path of the context.
        /// </summary>
        internal string CurrentDirectorPath => this.SlashMode == SlashMode.Backward ? this.CurrentDirector.Path : this.CurrentDirector.Path.Replace(CT.BSlash, CT.FSlash);

        /// <summary>
        /// Gets or sets the Disk manager.
        /// </summary>
        internal DiskManager DiskManager { get; set; }

        /// <summary>
        /// Gets or sets the Extractor.
        /// </summary>
        internal Extractor Extractor { get; set; }

        /// <summary>
        /// Gets or sets the path mode.
        /// </summary>
        internal PathMode PathMode { get; set; }

        /// <summary>
        /// Gets or sets the slash mode.
        /// </summary>
        internal SlashMode SlashMode { get; set; }

        /// <summary>
        /// Gets or sets the disposed flag.
        /// </summary>
        private bool _Disposed { get; set; }

        /// <summary>
        /// Gets or sets the current director of the context.
        /// </summary>
        private Director CurrentDirector { get; set; }

        /// <summary>
        /// Creates Director.
        /// </summary>
        /// <param name="dirName">Name of a new director.</param>
        public virtual void CreateDirector(string dirName) => DirfileCreator.Instance.CreateDirector(this.NormalizeString(this.CurrentDirectorPath), dirName);

        /// <summary>
        /// Creates Director.
        /// </summary>
        /// <param name="path">Path to the director to create.</param>
        public virtual void CreateDirectorPath(string path) => DirfileCreator.Instance.CreateDirector(this.NormalizeString(path));

        /// <summary>
        /// Creates Filer.
        /// </summary>
        /// <param name="dirName">Name of a new filer.</param>
        public virtual void CreateFiler(string dirName) => DirfileCreator.Instance.CreateFiler(this.NormalizeString(this.CurrentDirectorPath), dirName);

        /// <summary>
        /// Creates Filer.
        /// </summary>
        /// <param name="path">Path to the filer to create.</param>
        public virtual void CreateFilerPath(string path) => DirfileCreator.Instance.CreateFiler(this.NormalizeString(path));

        /// <summary>
        /// Deletes Director.
        /// </summary>
        /// <param name="dirName">Name of a director to delete.</param>
        public virtual void DeleteDirector(string dirName) => DirfileCreator.Instance.DeleteDirector(this.NormalizeString(this.CurrentDirectorPath), dirName);

        /// <summary>
        /// Deletes Director.
        /// </summary>
        /// <param name="path">Path to the director to delete.</param>
        public virtual void DeleteDirectorPath(string path) => DirfileCreator.Instance.DeleteDirector(this.NormalizeString(path));

        /// <summary>
        /// Deletes Director.
        /// </summary>
        /// <param name="path">Path to the director to delete.</param>
        public virtual void DeleteDirectorPathRecursive(string path) => DirfileCreator.Instance.DeleteDirector(this.NormalizeString(path), deleteEverything: true);

        /// <summary>
        /// Deletes Director recursively.
        /// </summary>
        /// <param name="dirName">Name of a director to delete.</param>
        public virtual void DeleteDirectorRecursive(string dirName) => DirfileCreator.Instance.DeleteDirector(this.NormalizeString(this.CurrentDirectorPath), dirName, true);

        /// <summary>
        /// Deletes Filer.
        /// </summary>
        /// <param name="dirName">Name of a filer to delete.</param>
        public virtual void DeleteFiler(string dirName) => DirfileCreator.Instance.DeleteFiler(this.NormalizeString(this.CurrentDirectorPath), dirName);

        /// <summary>
        /// Deletes Filer.
        /// </summary>
        /// <param name="path">Path to the filer to delete.</param>
        public virtual void DeleteFilerPath(string path) => DirfileCreator.Instance.DeleteFiler(this.NormalizeString(path));

        /// <summary>
        /// Changes the actual director and sets current path.
        /// </summary>
        /// <param name="newPath">New path of director.</param>
        /// <returns>Current path.</returns>
        public virtual void DirectorChange(string newPath)
        {
            if (PathValidator.Instance.IsInvalid(newPath))
                throw new DirfileException($"Invalid path when changing director: {newPath}");

            if (this.SlashMode == SlashMode.Forward)
                newPath = newPath.Replace(CT.FSlash, CT.BSlash);

            this.CurrentDirector = new Director(newPath);
            this.DiskManager.ChangeDrive(this.CurrentDirectorPath.Substring(0, 1));
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing,
        ///     releasing, or resetting unmanaged resources.
        /// </summary>
        /// <exception cref="DirfileException">Thrown if method is not rewritten!</exception>
        public void Dispose()
        {
            Dispose(disposing: false);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Switches path mode.
        /// </summary>
        public void SwitchPathMode()
        {
            switch (this.PathMode)
            {
                case PathMode.Absolute:
                    this.PathMode = PathMode.Relative;
                    break;

                case PathMode.Relative:
                    this.PathMode = PathMode.Absolute;
                    break;
            }
        }

        /// <summary>
        /// Switches slash mode.
        /// </summary>
        public void SwitchSlashMode()
        {
            PathValidator.Instance.SwitchSlashMode();
            this.Extractor.SwitchExtractMode();
            this.SlashMode = PathValidator.Instance.SlashMode;
        }

        /// <summary>
        /// Disposes context.
        /// </summary>
        /// <param name="disposing">True, if disposing, otherwise false.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_Disposed)
            {
                if (!disposing)
                {
                    this._Disposed = true;
                }
            }
        }

        /// <summary>
        /// Initializes a context.
        /// </summary>
        protected virtual void Initialize(string path, SlashMode slashMode, PathMode pathMode)
        {
            this.SlashMode = slashMode;
            this.PathMode = pathMode;
            this.Extractor = new Extractor(this.SlashMode);

            if (this.SlashMode != SlashMode.Backward)
                PathValidator.Instance.SwitchSlashMode();

            if (PathValidator.Instance.IsInvalid(path))
                throw new DirfileException($"Invalid path when initializing: {path}");

            this.Extractor.Extract(path);

            this.CurrentDirector = new Director(this.Extractor.NormalizedDirectorPath);
            this.DiskManager = new DiskManager(this.Extractor.NormalizedDirectorPath.Substring(0, 1));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string NormalizeString(string str)
        {
            if (this.SlashMode == SlashMode.Backward)
                return str;
            else
                return str.Replace(CT.FSlash, CT.BSlash);
        }
    }
}