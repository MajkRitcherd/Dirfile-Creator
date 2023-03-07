// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 07/03/2023     \\

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Dirfile_lib.Core.Abstraction;
using Dirfile_lib.Exceptions;
using Dirfile_lib.Utilities;
using CD = Dirfile_lib.Core.Constants.DefaultValues;
using CT = Dirfile_lib.Core.Constants.Texts;

namespace Dirfile_lib.Core.Dirfiles
{
    /// <summary>
    /// Represents file.
    /// </summary>
    /// <exception cref="DirfileException">Deleting File can throw exception.</exception>
    internal class Filer : AbstractMetadata
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Filer"/> class.
        /// </summary>
        /// <param name="fileInfo">File information.</param>
        public Filer(FileInfo fileInfo)
        {
            this.SetMetadata(fileInfo);

            this.Path = fileInfo.FullName.Remove(fileInfo.FullName.LastIndexOf('.'));
            this.Directory = new Director(this.Path.Remove(this.Path.LastIndexOf('\\')));
            this.DirectoryName = this.Directory.FullName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Filer"/> class.
        /// </summary>
        /// <param name="path">Path to the file.</param>
        /// <param name="bufferSize">Buffer size.</param>
        /// <param name="options">File options.</param>
        public Filer(string path, int bufferSize = CD.BufferSize, FileOptions options = FileOptions.None)
        {
            this.Exists = File.Exists(path);

            if (path.ElementAt(path.Length - 4) == '.')
                this.Extension = path.Substring(path.Length - 4).ToLowerInvariant();

            if (path.LastIndexOf('.') == -1)
                this.Path = path;
            else
                this.Path = path.Remove(path.LastIndexOf('.'));

            if (!string.IsNullOrEmpty(this.Extension))
                this.FullName = this.Path + this.Extension;
            else
                this.FullName = this.Path;

            this.BufferSize = bufferSize;
            this.Options = options;
            this.Directory = new Director(this.Path.Remove(this.Path.LastIndexOf('\\')));

            this.DirectoryName = this.Directory.FullName;

            // Get times if file exists and attributes
            if (this.Exists)
            {
                this.Attributes = File.GetAttributes(this.FullName);
                this.CreationTime = File.GetCreationTime(this.FullName);
                this.CreationTimeUtc = File.GetCreationTimeUtc(this.FullName);
                this.LastAccessTime = File.GetLastAccessTime(this.FullName);
                this.LastAccessTimeUtc = File.GetLastAccessTimeUtc(this.FullName);
                this.LastWriteTime = File.GetLastWriteTime(this.FullName);
                this.LastWriteTimeUtc = File.GetLastWriteTimeUtc(this.FullName);
            }
            else
                this.Attributes = FileAttributes.Archive;

            this.Name = this.Path.Substring(this.Path.LastIndexOf('\\') + 1) + this.Extension;
        }

        /// <summary>
        /// Gets or sets buffer size.
        /// </summary>
        public int BufferSize { get; set; }

        /// <summary>
        /// Gets directory.
        /// </summary>
        public Director Directory { get; private set; }

        /// <summary>
        /// Enum finder to find item in enums.
        /// </summary>
        private readonly EnumFinder EnumFinder = new EnumFinder();

        /// <summary>
        /// Gets directory name.
        /// </summary>
        public string DirectoryName { get; private set; }

        /// <summary>
        /// Gets or sets if file is readonly.
        /// </summary>
        public bool IsReadOnly { get; set; }

        /// <summary>
        /// Gets the byte size of a file.
        /// </summary>
        public long Length { get; private set; }

        /// <summary>
        /// Gets or sets file options.
        /// </summary>
        public FileOptions Options { get; set; }

        /// <summary>
        /// Creates a file.
        /// </summary>
        public override void Create()
        {
            if (string.IsNullOrEmpty(this.Extension))
            {
                this.Create(this.FullName, this.BufferSize, FileOptions.None);
                this.Exists = true;
            }
            else
                this.Create(this.EnumFinder.FindOverEnums(this.Extension)/*(DirfileExtensions)Enum.Parse(typeof(DirfileExtensions), this.Extension.Substring(1).ToUpperInvariant())*/);
        }

        /// <summary>
        /// Creates a file.
        /// </summary>
        /// <param name="name">Name of the file.</param>
        /// <param name="extension">Extension of the file.</param>
        public void Create(object extension)
        {
            // Set times
            this.CreationTime = DateTime.Now;
            this.CreationTimeUtc = DateTime.UtcNow;
            this.LastAccessTime = DateTime.Now;
            this.LastAccessTimeUtc = DateTime.UtcNow;
            this.LastWriteTime = DateTime.Now;
            this.LastWriteTimeUtc = DateTime.UtcNow;

            // Set other attributes
            if (string.IsNullOrEmpty(this.Extension))
                this.Extension = '.' + extension.ToString().ToLowerInvariant();

            this.Path = this.Path;
            this.FullName = this.Path + '.' + extension.ToString().ToLowerInvariant();
            this.Create(this.FullName, this.BufferSize, this.Options);
            this.Exists = true;
        }

        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <exception cref="Exception">Thrown if trying to delete file that does not exist.</exception>
        public override void Delete()
        {
            try
            {
                File.Delete(this.FullName);
                this.Exists = false;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        /// <summary>
        /// Deletes the file specified by path.
        /// </summary>
        /// <param name="path">Path to the file.</param>
        public void Delete(string path)
        {
            File.Delete(path);
        }

        /// <summary>
        /// Creates a file.
        /// </summary>
        /// <param name="path">Path to the file.</param>
        /// <param name="bufferSize">Buffer size.</param>
        /// <param name="options">File options.</param>
        private void Create(string path, int bufferSize, FileOptions options)
        {
            FileStream file = File.Create(path, bufferSize, options);
            file.Close();
        }

        /// <inheritdoc />
        protected override void SetMetadata<T>(T info)
        {
            if (typeof(T) != typeof(FileInfo))
                throw new DirfileException("Only FileInfo can be passed to the Filer.SetMetadata!");

            PropertyInfo[] infoProperties = info.GetType().GetProperties();
            PropertyInfo[] thisProperties = this.GetType().GetProperties();

            // Check if file exists, if not metadata are not set
            foreach (var property in infoProperties)
            {
                if (property.Name == CT.Props.Exists)
                {
                    if (property.GetValue(info).ToString() == false.ToString().ToLowerInvariant())
                    {
                        this.Exists = false;
                        break;
                    }
                }
            }

            // Sets the properties from FileInfo into Filer properties
            foreach (var property in infoProperties)
            {
                foreach (var thisProperty in thisProperties)
                {
                    if (thisProperty.Name == property.Name && thisProperty.Name != CT.Props.Directory)
                    {
                        if (thisProperty.Name == CT.Props.Length && !this.Exists)
                            break;

                        thisProperty.SetValue(this, property.GetValue(info));
                    }
                }
            }
        }
    }
}