// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 17/03/2023     \\

using System;
using System.Collections.Generic;
using System.IO;
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
        internal DiskManager(string newDrive = "C")
        {
            foreach (var drive in this.Drives)
                this.AvailableDrivesByLetterAndLabel.Add(new Tuple<string, string>(drive.Name.Substring(0, 1), drive.VolumeLabel), drive);

            this.ChangeDrive(newDrive);
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
        internal Dictionary<Tuple<string, string>, DriveInfo> AvailableDrivesByLetterAndLabel { get; private set; } = new Dictionary<Tuple<string, string>, DriveInfo>();

        /// <summary>
        /// Changes current drive.
        /// </summary>
        /// <param name="driveName">Drive name. (Use only letter, i.e. 'C', 'D', ... or use its label name, i.e. 'CustomName')</param>
        internal void ChangeDrive(string driveName)
        {
            foreach (var drive in this.AvailableDrivesByLetterAndLabel)
            {
                if (drive.Key.Item1 == driveName || drive.Key.Item2 == driveName)
                {
                    this.CurrentDrive = drive.Value;
                    return;
                }
            }

            throw new DirfileException($"Disk '{driveName}' does not exists!");
        }
    }
}