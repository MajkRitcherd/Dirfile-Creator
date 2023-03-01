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
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// Gets or sets time of creation in UTC.
        /// </summary>
        public DateTime CreationTimeUtc { get; set; }

        /// <summary>
        /// Gets or sets last access time.
        /// </summary>
        public DateTime LastAccessTime { get; set; }

        /// <summary>
        /// Gets or sets last access time in UTC.
        /// </summary>
        public DateTime LastAccessTimeUtc { get; set; }

        /// <summary>
        /// Gets or sets last write time.
        /// </summary>
        public DateTime LastWriteTime { get; set; }

        /// <summary>
        /// Gets or sets last write time in UTC.
        /// </summary>
        public DateTime LastWriteTimeUtc { get; set; }

        /// <summary>
        /// Gets time from last save.
        /// </summary>
        /// <returns>Time from last save.</returns>
        public TimeSpan NoSaveTime()
        {
            return this.LastAccessTime.Subtract(this.LastWriteTime);
        }
    }
}