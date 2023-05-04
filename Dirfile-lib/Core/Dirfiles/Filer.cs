// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 26/04/2023     \\

using System;
using System.IO;
using System.Reflection;
using Dirfile_lib.Core.Abstraction;
using Dirfile_lib.Exceptions;
using Dirfile_lib.Utilities;
using ValueConsts = Dirfile_lib.Core.Constants.DefaultValues;
using TextConsts = Dirfile_lib.Core.Constants.Texts;
using Chars = Dirfile_lib.Core.Constants.DirFile.Characters;

namespace Dirfile_lib.Core.Dirfiles
{
    /// <summary>
    /// Represents file.
    /// </summary>
    /// <exception cref="DirfileException">Deleting File can throw exception.</exception>
    internal class Filer : AbstractMetadata
    {
        /// <summary>
        /// Extension finder to find extension inside extension enums.
        /// </summary>
        private readonly ExtensionFinder ExtensionFinder = new ExtensionFinder();

        /// <summary>
        /// Initializes a new instance of the <see cref="Filer"/> class.
        /// </summary>
        /// <param name="fileInfo">File information.</param>
        internal Filer(FileInfo fileInfo)
        {
            this.SetMetadata(fileInfo);

            this.Path = fileInfo.FullName.Remove(fileInfo.FullName.LastIndexOf('.'));
            this.Directory = new Director(this.Path.Remove(this.Path.LastIndexOf(Chars.BSlash)));
            this.DirectoryName = this.Directory.FullName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Filer"/> class.
        /// </summary>
        /// <param name="filerPath">Path to the file.</param>
        /// <param name="bufferSize">Buffer size.</param>
        /// <param name="fileOptions">File options.</param>
        internal Filer(string filerPath, int bufferSize = ValueConsts.BufferSize, FileOptions fileOptions = FileOptions.None)
        {
            this.Exists = File.Exists(filerPath);

            var fileName = filerPath.Substring(filerPath.LastIndexOf(Chars.BSlash) + 1);

            if (fileName.LastIndexOf('.') != -1)
                this.Extension = filerPath.Substring(filerPath.LastIndexOf('.')).ToLowerInvariant();
            else
                this.Extension = null;

            if (filerPath.LastIndexOf('.') == -1)
                this.Path = filerPath;
            else
                this.Path = filerPath.Remove(filerPath.LastIndexOf('.'));

            if (!string.IsNullOrEmpty(this.Extension))
                this.FullName = this.Path + this.Extension;
            else
                this.FullName = this.Path;

            this.BufferSize = bufferSize;
            this.Options = fileOptions;
            this.Directory = new Director(this.Path.Remove(this.Path.LastIndexOf(Chars.BSlash)));

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

            this.Name = this.Path.Substring(this.Path.LastIndexOf(Chars.BSlash) + 1) + this.Extension;
        }

        /// <summary>
        /// Gets or sets buffer size.
        /// </summary>
        internal int BufferSize { get; set; }

        /// <summary>
        /// Gets directory.
        /// </summary>
        internal Director Directory { get; private set; }

        /// <summary>
        /// Gets directory name.
        /// </summary>
        internal string DirectoryName { get; private set; }

        /// <summary>
        /// Gets or sets if file is readonly.
        /// </summary>
        internal bool IsReadOnly { get; set; }

        /// <summary>
        /// Gets the byte size of a file.
        /// </summary>
        internal long Length { get; private set; }

        /// <summary>
        /// Gets or sets file options.
        /// </summary>
        internal FileOptions Options { get; set; }

        /// <summary>
        /// Creates a file.
        /// </summary>
        internal override void Create()
        {
            if (string.IsNullOrEmpty(this.Extension))
            {
                this.Create(this.FullName, this.BufferSize, FileOptions.None);
                this.Exists = true;
            }
            else
                this.Create(this.ExtensionFinder.FindOverEnums(this.Extension));
        }

        /// <summary>
        /// Creates a file.
        /// </summary>
        /// <param name="extension">Extension of the file.</param>
        internal void Create(object extension)
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
        internal override void Delete()
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
        /// <param name="filerPath">Path to the file.</param>
        internal void Delete(string filerPath)
        {
            File.Delete(filerPath);
        }

        /// <summary>
        /// Writes text info Filer.
        /// </summary>
        /// <param name="textToWrite">Text to write.</param>
        internal void WriteString(string textToWrite)
        {
            File.WriteAllText(this.FullName, textToWrite);
        }

        /// <inheritdoc />
        protected override void SetMetadata<T>(T fileInfo)
        {
            if (typeof(T) != typeof(FileInfo))
                throw new DirfileException("Only FileInfo can be passed to the Filer.SetMetadata!");

            PropertyInfo[] fileInfoProperties = fileInfo.GetType().GetProperties();
            PropertyInfo[] thisFilerProperties = this.GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);

            // Check if file exists, if not metadata are not set
            foreach (var infoProperty in fileInfoProperties)
            {
                if (infoProperty.Name == TextConsts.Props.Exists)
                {
                    if (infoProperty.GetValue(fileInfo).ToString() == false.ToString().ToLowerInvariant())
                    {
                        this.Exists = false;
                        break;
                    }
                }
            }

            // Sets the properties from FileInfo into Filer properties
            foreach (var infoProperty in fileInfoProperties)
            {
                foreach (var thisProperty in thisFilerProperties)
                {
                    if (thisProperty.Name == infoProperty.Name && thisProperty.Name != TextConsts.Props.Directory)
                    {
                        if (thisProperty.Name == TextConsts.Props.Length && !this.Exists)
                            break;

                        thisProperty.SetValue(this, infoProperty.GetValue(fileInfo));
                    }
                }
            }
        }

        /// <summary>
        /// Creates a file.
        /// </summary>
        /// <param name="filerPath">Path to the file.</param>
        /// <param name="bufferSize">Buffer size.</param>
        /// <param name="options">File options.</param>
        private void Create(string filerPath, int bufferSize, FileOptions options)
        {
            FileStream file = File.Create(filerPath, bufferSize, options);
            file.Close();
        }
    }
}