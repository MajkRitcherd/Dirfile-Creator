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
        internal FileAttributes Attributes { get; set; }

        /// <summary>
        /// Gets a value indicating whether it exists.
        /// </summary>
        internal bool Exists { get; set; }

        /// <summary>
        /// Gets the extension.
        /// </summary>
        internal string Extension { get; set; }

        /// <summary>
        /// Gets the fullname (path with name).
        /// </summary>
        internal string FullName { get; set; }

        /// <summary>
        /// Gets link target's path if exists. If it does not exist in Fullname,
        ///     or this instance does not represent a link, returns null.
        /// </summary>
        internal string LinkTarget { get; set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        internal string Name { get; set; }

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        internal string Path { get; set; }

        /// <summary>
        /// Only creates a file or directory.
        /// </summary>
        /// <param name="path">Path.</param>
        internal abstract void Create();

        /// <summary>
        /// Deletes a file or directory.
        /// </summary>
        /// <param name="path">Path</param>
        internal abstract void Delete();

        /// <summary>
        /// Sets metadata of a type T.
        /// </summary>
        /// <typeparam name="T">Type of T.</typeparam>
        /// <param name="info">Directory info or FileInfo.</param>
        protected abstract void SetMetadata<T>(T info);
    }
}