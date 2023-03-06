// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 11/01/2023     \\

using System;
using System.IO;
using System.Reflection;
using Dirfile_lib.Core.Abstraction;
using Dirfile_lib.Exceptions;

//[assembly: InternalsVisibleTo("Dirfile-Creator")]

namespace Dirfile_lib.Core.Dirfiles
{
    /// <summary>
    /// Represents directory.
    /// </summary>
    internal class Director : AbstractMetadata
    {
        /// <summary>
        /// Gets if current directory is root directory.
        /// </summary>
        public bool IsRoot { get => this.Parent == null && this.Root == null; }

        /// <summary>
        /// Gets parent directory.
        /// </summary>
        public Director Parent { get; private set; }

        /// <summary>
        /// Gets root directory.
        /// </summary>
        public Director Root { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Director"/> class.
        /// </summary>
        /// <param name="path">Path of the directory.</param>
        public Director(string path)
        {
            if (path.LastIndexOf('\\') == -1)
                this.Initialize(path, true);
            else
                this.Initialize(path, false);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Direcotr"/> class.
        /// </summary>
        /// <param name="info">Directory info.</param>
        public Director(DirectoryInfo info)
        {
            this.SetMetadata(info);

            this.Path = this.FullName;
            this.Extension = null;

            this.Parent = new Director(this.Path.Substring(0, this.Path.LastIndexOf('\\')));
            this.Root = new Director(this.Path.Substring(0, this.Path.IndexOf('\\')));
        }

        /// <summary>
        /// Creates a directory.
        /// </summary>
        public override void Create()
        {
            if (!this.Exists)
                this.Create(this.Path);
        }

        /// <summary>
        /// Deletes directory.
        /// </summary>
        /// <exception cref="Exception">Thrown if directory does not exist.</exception>
        public override void Delete()
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
        public void Delete(string path)
        {
            this.Delete(path);
        }

        /// <summary>
        /// Deletes the directory specified by path, if recursive then it deletes any subdirectories and files.
        /// </summary>
        /// <param name="path">Path to the directory.</param>
        /// <param name="recursive">If true, deletes any subdirectories and files.</param>
        public void Delete(string path, bool recursive = false)
        {
            Directory.Delete(path, recursive);
        }

        /// <summary>
        /// Creates a directory.
        /// </summary>
        /// <param name="path">Path to the directory.</param>
        /// <param name="security">Security.</param>
        private void Create(string path)
        {
            var info = Directory.CreateDirectory(path);
            this.Parent = new Director(info.Parent);
            this.Root = new Director(info.Root);
            this.SetMetadata(info);
        }

        public Director ChangePath(string path)
        {
            return new Director(path);
        }

        /// <summary>
        /// Initializes a Director
        /// </summary>
        /// <param name="path">Path of the directoy.</param>
        /// <param name="root">True if root, otherwise false.</param>
        private void Initialize(string path, bool root)
        {
            this.Path = path;

            this.Exists = Directory.Exists(this.Path);
            this.Attributes = FileAttributes.Directory;

            if (this.Exists)
                this.SetMetadata(new DirectoryInfo(this.Path));

            this.FullName = this.Path;
            this.Extension = null;

            var index = this.Path.LastIndexOf('\\');
            var lastIndex = this.Path.LastIndexOf('\\');

            if (index == -1 && lastIndex == -1)
                root = true;

            if (lastIndex != -1)
                this.Parent = new Director(this.Path.Substring(0, this.Path.LastIndexOf('\\')));

            if (index == -1 && !root)
                this.Root = new Director(this.Path);

            if (this.IsRoot)
            {
                this.Root = null;
                this.Parent = null;
                this.Name = this.FullName;
                this.FullName += "\\";
                this.Path = this.FullName;
            }
            else
            {
                this.Root = new Director(this.Path.Substring(0, this.Path.IndexOf('\\')));
                this.Name = this.Path.Substring(lastIndex + 1);
            }
        }

        /// <inheritdoc />
        protected override void SetMetadata<T>(T info)
        {
            if (typeof(T) != typeof(DirectoryInfo))
                throw new DirfileException("Only DirectoryInfo can be passed to the Director.SetMetadata!");

            PropertyInfo[] infoProperties = info.GetType().GetProperties();
            PropertyInfo[] thisProperties = this.GetType().GetProperties();

            // Check if directory exists, if not metadata are not set
            foreach (var property in infoProperties)
            {
                if (property.Name == "Exists")
                {
                    if (property.GetValue(info).ToString() == false.ToString().ToLowerInvariant())
                        return;
                }
            }

            foreach (var property in infoProperties)
            {
                foreach (var thisProperty in thisProperties)
                {
                    if (thisProperty.Name == property.Name && thisProperty.Name != "Parent" && thisProperty.Name != "Root")
                    {
                        thisProperty.SetValue(this, property.GetValue(info, null));
                        break;
                    }
                }
            }
        }
    }
}