// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 26/04/2023     \\

using System;
using System.IO;
using Dirfile_lib.API.Extraction;
using Dirfile_lib.API.Extraction.Modes;
using Dirfile_lib.Core.Dirfiles;
using Dirfile_lib.Exceptions;
using Dirfile_lib.Utilities.Validation;
using Chars = Dirfile_lib.Core.Constants.DirFile.Characters;

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
        internal BaseDirfileContext()
        {
            this.Initialize(Directory.GetCurrentDirectory(), SlashMode.Backward, PathMode.Absolute);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseDirfileContext"/> class.
        /// </summary>
        /// <param name="slashMode">Slash mode to use.</param>
        /// <param name="pathMode">Path mode to use.</param>
        internal BaseDirfileContext(SlashMode slashMode = SlashMode.Backward, PathMode pathMode = PathMode.Relative)
        {
            this.Initialize(slashMode == SlashMode.Backward ? Directory.GetCurrentDirectory() : Directory.GetCurrentDirectory().Replace(Chars.BSlash, Chars.FSlash), slashMode, pathMode);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseDirfileContext"/> class.
        /// </summary>
        /// <param name="initialDirectorPath">Path to director to work from (like relative path).</param>
        /// <param name="slashMode">Slash mode to use.</param>
        internal BaseDirfileContext(string initialDirectorPath, SlashMode slashMode, PathMode pathMode)
        {
            this.Initialize(initialDirectorPath, slashMode, pathMode);
        }

        /// <summary>
        /// Gets or sets the current director path of the context.
        /// </summary>
        internal string CurrentPath => this.SlashMode == SlashMode.Backward ? this.CurrentDirector.Path : this.CurrentDirector.Path.Replace(Chars.BSlash, Chars.FSlash);

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
        /// <param name="nameOfDirector">Name of a new director.</param>
        public virtual void CreateDirector(string nameOfDirector) => DirfileCreator.Instance.CreateDirector(this.NormalizeString(this.CurrentPath), nameOfDirector);

        /// <summary>
        /// Creates Director.
        /// </summary>
        /// <param name="directorPath">Path to the director to create.</param>
        public virtual void CreateDirectorFromAbsolutePath(string directorPath) => DirfileCreator.Instance.CreateDirector(this.NormalizeString(directorPath));

        /// <summary>
        /// Creates Filer.
        /// </summary>
        /// <param name="nameOfFiler">Name of a new filer.</param>
        public virtual void CreateFiler(string nameOfFiler) => DirfileCreator.Instance.CreateFiler(this.NormalizeString(this.CurrentPath), nameOfFiler);

        /// <summary>
        /// Creates Filer.
        /// </summary>
        /// <param name="filerPath">Path to the filer to create.</param>
        public virtual void CreateFilerFromAbsolutePath(string filerPath) => DirfileCreator.Instance.CreateFiler(this.NormalizeString(filerPath));

        /// <summary>
        /// Deletes Director.
        /// </summary>
        /// <param name="nameOfDirector">Name of a director to delete.</param>
        public virtual void DeleteDirector(string nameOfDirector) => DirfileCreator.Instance.DeleteDirector(this.NormalizeString(this.CurrentPath), nameOfDirector);

        /// <summary>
        /// Deletes Director.
        /// </summary>
        /// <param name="directorPath">Path to the director to delete.</param>
        public virtual void DeleteDirectorFromAbsolutePath(string directorPath) => DirfileCreator.Instance.DeleteDirector(this.NormalizeString(directorPath));

        /// <summary>
        /// Deletes Director and its content.
        /// </summary>
        /// <param name="directorPath">Path to the director to delete.</param>
        public virtual void DeleteDirectorFromAbsolutePathRecursive(string directorPath) => DirfileCreator.Instance.DeleteDirector(this.NormalizeString(directorPath), deleteEverything: true);

        /// <summary>
        /// Deletes Director recursively.
        /// </summary>
        /// <param name="nameOfDirector">Name of a director to delete.</param>
        public virtual void DeleteDirectorRecursive(string nameOfDirector) => DirfileCreator.Instance.DeleteDirector(this.NormalizeString(this.CurrentPath), nameOfDirector, true);

        /// <summary>
        /// Deletes Filer.
        /// </summary>
        /// <param name="fileName">Name of a filer to delete.</param>
        public virtual void DeleteFiler(string fileName) => DirfileCreator.Instance.DeleteFiler(this.NormalizeString(this.CurrentPath), fileName);

        /// <summary>
        /// Deletes Filer.
        /// </summary>
        /// <param name="filerPath">Path to the filer to delete.</param>
        public virtual void DeleteFilerFromAbsolutePath(string filerPath) => DirfileCreator.Instance.DeleteFiler(this.NormalizeString(filerPath));

        /// <summary>
        /// Changes the actual director and sets current path.
        /// </summary>
        /// <param name="newPath">New path of director.</param>
        /// <returns>Current path.</returns>
        public virtual void ChangeCurrentDirector(string newPath)
        {
            if (PathValidator.Instance.IsInvalid(newPath))
                throw new DirfileException($"Invalid path when changing director: {newPath}");

            if (this.SlashMode == SlashMode.Forward)
                newPath = newPath.Replace(Chars.FSlash, Chars.BSlash);

            this.CurrentDirector = new Director(newPath);
            this.DiskManager.ChangeDrive(this.CurrentPath.Substring(0, 1));
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
                if (!disposing)
                    this._Disposed = true;
        }

        /// <summary>
        /// Initializes a context.
        /// </summary>
        protected virtual void Initialize(string initialDirectorPath, SlashMode slashMode, PathMode pathMode)
        {
            this.SlashMode = slashMode;
            this.PathMode = pathMode;
            this.Extractor = new Extractor(this.SlashMode);

            if (this.SlashMode != SlashMode.Backward)
                PathValidator.Instance.SwitchSlashMode();

            if (PathValidator.Instance.IsInvalid(initialDirectorPath))
                throw new DirfileException($"Invalid path when initializing: {initialDirectorPath}");

            this.Extractor.Extract(initialDirectorPath);

            this.CurrentDirector = new Director(this.Extractor.NormalizedDirectorPath);
            this.DiskManager = new DiskManager(this.Extractor.NormalizedDirectorPath.Substring(0, 1));
        }

        /// <summary>
        /// Normalizes given string (replaces '/' for '\').
        /// </summary>
        /// <param name="stringToReplace">String to replace.</param>
        /// <returns>Normalized string.</returns>
        private string NormalizeString(string stringToReplace)
        {
            if (this.SlashMode == SlashMode.Backward)
                return stringToReplace;
            else
                return stringToReplace.Replace(Chars.FSlash, Chars.BSlash);
        }
    }
}