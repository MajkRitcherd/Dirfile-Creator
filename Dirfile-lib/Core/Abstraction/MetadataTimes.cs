// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 20/11/2022     \\

using System;

namespace Dirfile_lib.Core.Abstraction
{
    internal class MetadataTimes
    {
        /// <summary>
        /// Gets or sets time of creation.
        /// </summary>
        internal DateTime CreationTime { get; set; }

        /// <summary>
        /// Gets or sets time of creation in UTC.
        /// </summary>
        internal DateTime CreationTimeUtc { get; set; }

        /// <summary>
        /// Gets or sets last access time.
        /// </summary>
        internal DateTime LastAccessTime { get; set; }

        /// <summary>
        /// Gets or sets last access time in UTC.
        /// </summary>
        internal DateTime LastAccessTimeUtc { get; set; }

        /// <summary>
        /// Gets or sets last write time.
        /// </summary>
        internal DateTime LastWriteTime { get; set; }

        /// <summary>
        /// Gets or sets last write time in UTC.
        /// </summary>
        internal DateTime LastWriteTimeUtc { get; set; }

        /// <summary>
        /// Gets time from last save.
        /// </summary>
        /// <returns>Time from last save.</returns>
        internal TimeSpan NoSaveTime()
        {
            return this.LastAccessTime.Subtract(this.LastWriteTime);
        }
    }
}