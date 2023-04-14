﻿// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 06/04/2023     \\

using System;
using Dirfile_lib.Core.Dirfiles;
using CT = Dirfile_lib.Core.Constants.Texts;

namespace Dirfile_lib.API
{
    public class DirfileCreator
    {
        /// <summary>
        /// Lazily instantiate a new instance of the <see cref="DirfileCreator"/> class.
        /// </summary>
        private static readonly Lazy<DirfileCreator> _Creator = new Lazy<DirfileCreator>(() => new DirfileCreator());

        /// <summary>
        /// Initializes a new instance of the <see cref="DirfileCreator"/> class.
        /// </summary>
        protected DirfileCreator()
        {
        }

        /// <summary>
        /// Gets the <see cref="DirfileCreator"/> instance.
        /// </summary>
        public static DirfileCreator Instance => _Creator.Value;

        /// <summary>
        /// Creates Director.
        /// </summary>
        /// <param name="path">Path to director.</param>
        /// <param name="dirName">Name of a new director.</param>
        public void CreateDirector(string path, string dirName = "")
        {
            var director = new Director(string.Join(CT.BSlash, path, dirName));
            director.Create();
        }

        /// <summary>
        /// Creates Filer.
        /// </summary>
        /// <param name="path">path to filer.</param>
        /// <param name="filerName">Name of a new filer.</param>
        public void CreateFiler(string path, string filerName = "")
        {
            Filer filer;

            if (!string.IsNullOrEmpty(filerName))
                filer = new Filer(string.Join(CT.BSlash, path, filerName));
            else
                filer = new Filer(path);

            filer.Create();
        }

        /// <summary>
        /// Deletes Director.
        /// </summary>
        /// <param name="path">Path to director.</param>
        /// <param name="dirName">Name of a director to delete.</param>
        public void DeleteDirector(string path, string dirName = "", bool deleteEverything = false)
        {
            var director = new Director(string.Join(CT.BSlash, path, dirName));
            director.Delete(deleteEverything);
        }

        /// <summary>
        /// Deletes Filer.
        /// </summary>
        /// <param name="path">Path to filer.</param>
        /// <param name="filerName">Name of a filer to delete.</param>
        public void DeleteFiler(string path, string filerName = "")
        {
            Filer filer;

            if (!string.IsNullOrEmpty(filerName))
                filer = new Filer(string.Join(CT.BSlash, path, filerName));
            else
                filer = new Filer(path);

            filer.Delete();
        }
    }
}