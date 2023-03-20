﻿// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 13/12/2022     \\

using System;
using Dirfile_lib.API.Extraction;

namespace Dirfile_lib.API.Context
{
    /// <summary>
    /// This is the MAIN API CLASS which si gonna be called.
    /// <para>Holds current directory, has methods for creating, deleting dirfiles,
    ///   also takes input and does the magic.</para>
    /// </summary>
    public class DirfileContext : BaseDirfileContext
    {
        /// <summary>
        ///
        /// </summary>
        public DirfileContext()
            : base()
        {
        }

        public DirfileContext(string path, SlashMode slashMode = SlashMode.Backward)
            : base(path, slashMode)
        {
        }

        private string SerializePath(string str, bool pathFinder)
        {
            return string.Empty;
            //if (pathFinder)
            //    return this.Extractor.FindExistingPath(str);
            //else
            //    return this.Extractor.FindArguments();
        }

        /// <inheritdoc/>
        protected override void Initialize(string path, SlashMode slashMode)
        {
            base.Initialize(path, slashMode);
            //throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override void Dispose()
        {
            //base.Dispose();
        }
    }
}