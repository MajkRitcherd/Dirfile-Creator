// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 19/11/2022     \\

using System.IO;

namespace Dirfile_lib.Core.Abstraction
{
    /// <summary>
    /// Abstract class that holds metadata.
    /// </summary>
    internal abstract class AbstractMetadata : MetadataTimes
    {
        /// <summary>
        /// Gets or sets the attributes.
        /// </summary>
        public FileAttributes Attributes { get; set; }

        /// <summary>
        /// Gets a value indicating whether it exists.
        /// </summary>
        public bool Exists { get; protected set; }

        /// <summary>
        /// Gets the extension.
        /// </summary>
        public string Extension { get; protected set; }

        /// <summary>
        /// Gets the fullname (path with name).
        /// </summary>
        public string FullName { get; protected set; }

        /// <summary>
        /// Gets link target's path if exists. If it does not exist in Fullname,
        ///     or this instance does not represent a link, returns null.
        /// </summary>
        public string LinkTarget { get; protected set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Only creates a file or directory.
        /// </summary>
        /// <param name="path">Path.</param>
        public abstract void Create();

        /// <summary>
        /// Deletes a file or directory.
        /// </summary>
        /// <param name="path">Path</param>
        public abstract void Delete();

        /// <summary>
        /// Sets metadata of a type T.
        /// </summary>
        /// <typeparam name="T">Type of T.</typeparam>
        /// <param name="info">Directory info or FileInfo.</param>
        protected abstract void SetMetadata<T>(T info);
    }
}