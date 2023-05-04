// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 24/04/2023     \\

using System;
using Dirfile_lib.Core.Dirfiles;
using Chars = Dirfile_lib.Core.Constants.DirFile.Characters;

namespace Dirfile_lib.API
{
    /// <summary>
    /// <see cref="DirfileCreator"/> creates or deletes dirfiles.
    /// </summary>
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
        /// <param name="directorPath">Path to director.</param>
        /// <param name="directorName">Name of a new director.</param>
        public void CreateDirector(string directorPath, string directorName = "")
        {
            var director = new Director(string.Join(Chars.BSlash.ToString(), directorPath, directorName));
            director.Create();
        }

        /// <summary>
        /// Creates Filer.
        /// </summary>
        /// <param name="filerPath">Path to filer.</param>
        /// <param name="filerName">Name of a new filer.</param>
        public void CreateFiler(string filerPath, string filerName = "")
        {
            Filer filer;

            if (!string.IsNullOrEmpty(filerName))
                filer = new Filer(string.Join(Chars.BSlash.ToString(), filerPath, filerName));
            else
                filer = new Filer(filerPath);

            filer.Create();
        }

        /// <summary>
        /// Deletes Director.
        /// </summary>
        /// <param name="directorPath">Path to director.</param>
        /// <param name="directorName">Name of a director to delete.</param>
        public void DeleteDirector(string directorPath, string directorName = "", bool deleteEverything = false)
        {
            var director = new Director(string.Join(Chars.BSlash.ToString(), directorPath, directorName));
            director.Delete(deleteEverything);
        }

        /// <summary>
        /// Deletes Filer.
        /// </summary>
        /// <param name="filerPath">Path to filer.</param>
        /// <param name="filerName">Name of a filer to delete.</param>
        public void DeleteFiler(string filerPath, string filerName = "")
        {
            Filer filer;

            if (!string.IsNullOrEmpty(filerName))
                filer = new Filer(string.Join(Chars.BSlash.ToString(), filerPath, filerName));
            else
                filer = new Filer(filerPath);

            filer.Delete();
        }
    }
}