// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 17/03/2023     \\

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dirfile_lib.Exceptions;

namespace Dirfile_lib.API
{
    /// <summary>
    /// Disk manager class.
    /// </summary>
    internal class DiskManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DiskManager"/> class.
        /// </summary>
        internal DiskManager(string defaultDrive = "C")
        {
            foreach (var disk in this.Drives)
                this.AvailableDisks.Add(new Tuple<string, string>(disk.Name.Substring(0, 1), disk.VolumeLabel), disk);

            this.ChangeDrive(defaultDrive);
        }

        /// <summary>
        /// Gets all logical drives.
        /// </summary>
        internal DriveInfo[] Drives => DriveInfo.GetDrives();

        /// <summary>
        /// Gets or sets the current set drive.
        /// </summary>
        internal DriveInfo CurrentDrive { get; set; }

        /// <summary>
        /// Gets or sets all logical drives.
        /// </summary>
        internal Dictionary<Tuple<string, string>, DriveInfo> AvailableDisks { get; private set; } = new Dictionary<Tuple<string, string>, DriveInfo>();

        /// <summary>
        /// Changes current drive.
        /// </summary>
        /// <param name="driveName">Drive name. (Use only letter, i.e. 'C', 'D', ... or use its label name, i.e. 'CustomName')</param>
        internal void ChangeDrive(string driveName)
        {
            foreach (var disk in this.AvailableDisks.Select((elem, index) => new { elem, index }))
            {
                if (disk.elem.Key.Item1 == driveName || disk.elem.Key.Item2 == driveName)
                {
                    this.CurrentDrive = disk.elem.Value;
                    return;
                }
            }

            throw new DirfileException("Disk with that name does not exists!");
        }
    }
}