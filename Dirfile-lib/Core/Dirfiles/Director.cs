// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 24/04/2023     \\

using System;
using System.IO;
using System.Reflection;
using Dirfile_lib.Core.Abstraction;
using Dirfile_lib.Exceptions;
using TextConsts = Dirfile_lib.Core.Constants.Texts;
using Chars = Dirfile_lib.Core.Constants.DirFile.Characters;

namespace Dirfile_lib.Core.Dirfiles
{
    /// <summary>
    /// Represents directory.
    /// </summary>
    internal class Director : AbstractMetadata
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Director"/> class.
        /// </summary>
        /// <param name="directorPath">Path of the directory.</param>
        internal Director(string directorPath)
        {
            // Root name should be followed by '\'
            var lastIndexOfSlash = directorPath.LastIndexOf(Chars.BSlash);

            if (lastIndexOfSlash == -1)
                directorPath += Chars.BSlash;

            if (lastIndexOfSlash == 2)
                this.Initialize(directorPath, true);
            else
                this.Initialize(directorPath, false);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Direcotr"/> class.
        /// </summary>
        /// <param name="directoryInfo">Directory info.</param>
        internal Director(DirectoryInfo directoryInfo)
        {
            this.SetMetadata(directoryInfo);

            this.Path = this.FullName;
            this.Extension = null;

            this.Parent = new Director(this.Path.Substring(0, this.Path.LastIndexOf(Chars.BSlash)));
            this.Root = new Director(this.Path.Substring(0, this.Path.IndexOf(Chars.BSlash)));
        }

        /// <summary>
        /// Gets if current directory is root directory.
        /// </summary>
        internal bool IsRoot { get => this.Parent == null && this.Root == null; }

        /// <summary>
        /// Gets parent directory.
        /// </summary>
        internal Director Parent { get; private set; }

        /// <summary>
        /// Gets root directory.
        /// </summary>
        internal Director Root { get; private set; }

        /// <summary>
        /// Creates a directory.
        /// </summary>
        internal override void Create()
        {
            if (!this.Exists)
                this.Create(this.Path);
        }

        /// <summary>
        /// Deletes directory.
        /// </summary>
        /// <exception cref="Exception">Thrown if directory does not exist.</exception>
        internal override void Delete()
        {
            try
            {
                Directory.Delete(this.FullName);
                this.Exists = false;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        /// <summary>
        /// Deletes directory specified by path.
        /// </summary>
        /// <param name="path">Path to the directory.</param>
        internal void Delete(bool deleteEverything = false)
        {
            try
            {
                Directory.Delete(this.FullName, deleteEverything);
                this.Exists = false;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        /// <summary>
        /// Deletes the directory specified by path, if recursive then it deletes any subdirectories and files.
        /// </summary>
        /// <param name="directorPath">Path to the directory.</param>
        /// <param name="recursive">If true, deletes any subdirectories and files.</param>
        internal void Delete(string directorPath, bool deleteEverything = false)
        {
            Directory.Delete(directorPath, deleteEverything);
        }

        /// <inheritdoc />
        protected override void SetMetadata<T>(T directoryInfo)
        {
            if (typeof(T) != typeof(DirectoryInfo))
                throw new DirfileException("Only DirectoryInfo can be passed to the Director.SetMetadata!");

            PropertyInfo[] directoryInfoProperties = directoryInfo.GetType().GetProperties();
            PropertyInfo[] thisDirectorProperties = this.GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public); // To be able to find internals

            // Check if directory exists, if not metadata are not set
            foreach (var infoProperty in directoryInfoProperties)
            {
                if (infoProperty.Name == TextConsts.DirfileProps.Exists)
                {
                    if (infoProperty.GetValue(directoryInfo).ToString() == false.ToString().ToLowerInvariant())
                        return;
                }
            }

            foreach (var infoProperty in directoryInfoProperties)
            {
                foreach (var thisProperty in thisDirectorProperties)
                {
                    if (thisProperty.Name == infoProperty.Name && thisProperty.Name != TextConsts.DirfileProps.Parent && thisProperty.Name != TextConsts.DirfileProps.Root)
                    {
                        thisProperty.SetValue(this, infoProperty.GetValue(directoryInfo, null));
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Creates a directory.
        /// </summary>
        /// <param name="directorPath">Path to the directory.</param>
        /// <param name="security">Security.</param>
        private void Create(string directorPath)
        {
            var directoryInfo = Directory.CreateDirectory(directorPath);
            this.Parent = new Director(directoryInfo.Parent);
            this.Root = new Director(directoryInfo.Root);
            this.SetMetadata(directoryInfo);
        }

        /// <summary>
        /// Initializes a Director
        /// </summary>
        /// <param name="directorPath">Path of the directoy.</param>
        /// <param name="isRoot">True if root, otherwise false.</param>
        private void Initialize(string directorPath, bool isRoot)
        {
            this.Path = directorPath;

            this.Exists = Directory.Exists(this.Path);
            this.Attributes = FileAttributes.Directory;

            if (this.Exists)
                this.SetMetadata(new DirectoryInfo(this.Path));

            this.FullName = this.Path;
            this.Extension = null;

            var index = this.Path.LastIndexOf(Chars.BSlash);
            var lastIndex = this.Path.LastIndexOf(Chars.BSlash);

            if (index == 2 && lastIndex == 2)
                isRoot = true;

            if (lastIndex != 2)
                this.Parent = new Director(this.Path.Substring(0, this.Path.LastIndexOf(Chars.BSlash)));

            if (index == 2 && !isRoot)
                this.Root = new Director(this.Path);

            if (this.IsRoot)
            {
                this.Root = null;
                this.Parent = null;
                this.Name = this.FullName;
                this.Path = this.FullName;
            }
            else
            {
                this.Root = new Director(this.Path.Substring(0, this.Path.IndexOf(Chars.BSlash)));
                this.Name = this.Path.Substring(lastIndex + 1);
            }
        }
    }
}