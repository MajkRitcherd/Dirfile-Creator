// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 06/04/2023     \\

using System;
using System.Linq;
using Dirfile_lib.API.Extraction;
using Dirfile_lib.API.Extraction.Modes;
using Dirfile_lib.Exceptions;
using CT = Dirfile_lib.Core.Constants.Texts;

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
        /// Initializes a new instance of the <see cref="DirfileContext"/> class. <br />
        ///     - (initial) current directory: {Project file location}\bin\{Configuration}\net6.0 <br />
        ///     - (initial) slash mode: BACKWARDS. <br />
        ///     - (initial) path mode: ABSOLUTE.
        /// </summary>
        public DirfileContext()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DirfileContext"/> class. <br />
        ///     - (initial) current directory: {Project file location}\bin\{Configuration}\net6.0 <br />
        ///     - (initial) slash mode: BACKWARDS. <br />
        ///     - (initial) path mode: ABSOLUTE.
        /// </summary>
        /// <param name="slashMode">Slash mode to use.</param>
        public DirfileContext(SlashMode slashMode)
            : base(slashMode, PathMode.Absolute)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DirfileContext"/> class. <br />
        ///     - (initial) current directory: PATH. <br />
        ///     - (initial) slash mode: BACKWARDS. <br />
        ///     - (initial) path mode: RELATIVE.
        /// </summary>
        /// <param name="path">Path to director to work from (like relative path).</param>
        /// <param name="slashMode">Slash mode to use.</param>
        public DirfileContext(string path, SlashMode slashMode = SlashMode.Backward)
            : base(path, slashMode, PathMode.Relative)
        {
        }

        /// <summary>
        /// Gets or sets the argument extractor.
        /// </summary>
        internal ArgumentExtractor ArgumentExtractor { get; set; }

        /// <summary>
        /// Creates Directors and Filers. <br />
        ///     - Also changes current directory to directory from absolute path in input (only in mode ABSOLUTE).
        /// </summary>
        /// <param name="input">Input string.</param>
        public void Create(string input)
        {
            if (this.PathMode == PathMode.Absolute)
            {
                this.Extractor.Extract(input);
                this.DirectorChange(this.Extractor.DirectorPath);

                this.CreateImpl(this.Extractor.Arguments);
            }
            else
                this.CreateImpl(input);
        }

        /// <inheritdoc/>
        protected override void Initialize(string path, SlashMode slashMode, PathMode pathMode)
        {
            this.Extractor = new Extractor(slashMode);
            this.Extractor.Extract(path);
            this.ArgumentExtractor = new ArgumentExtractor(slashMode);

            if (string.IsNullOrEmpty(this.Extractor.Arguments))
                base.Initialize(path, slashMode, pathMode);
            else
            {
                this.ArgumentExtractor.Extract(this.Extractor.Arguments);
                base.Initialize(this.Extractor.DirectorPath, slashMode, pathMode);
            }
        }

        /// <summary>
        /// Creates filers and directors from given input. Can be either arguments (works from relative path)
        ///     or can be arguments with path (like absolute path).
        /// </summary>
        /// <param name="input"></param>
        private void CreateImpl(string input)
        {
            this.ArgumentExtractor.Extract(input);

            foreach (var arg in this.ArgumentExtractor.ArgumentsInOrder.Select((val, ind) => new { val, ind }))
            {
                var slash = this.SlashMode == SlashMode.Backward ? CT.BSlash : CT.FSlash;

                Action<string> func = null;
                if (arg.ind == 0)
                {
                    func = this.GetDirfileFunc(arg.val.Key);
                    func(arg.val.Value);
                    continue;
                }

                switch (this.ArgumentExtractor.OperationsInOrder[arg.ind - 1])
                {
                    case CT.BSlash:
                        this.DirectorChange(this.CurrentDirectorPath + slash + this.ArgumentExtractor.ArgumentsInOrder[arg.ind - 1].Value);
                        this.GetDirfileFunc(arg.val.Key).Invoke(arg.val.Value);
                        break;

                    case ">":
                        this.GetDirfileFunc(arg.val.Key).Invoke(arg.val.Value);
                        break;

                    case ":>":
                        this.DirectorChange(this.CurrentDirectorPath.Substring(0, this.CurrentDirectorPath.LastIndexOf(slash)));
                        this.GetDirfileFunc(arg.val.Key).Invoke(arg.val.Value);
                        break;
                }
            }
        }

        /// <summary>
        /// Gets function from BaseDirfileContext based on whether its type is 'Director' or 'Filer'
        ///     is absolute path or not, is creation function or deletion function.
        /// </summary>
        /// <param name="type">Type of dirfile.</param>
        /// <param name="isPath">Use absolute path or not.</param>
        /// <param name="isCreate">Use creation function or deletion function.</param>
        /// <returns>Function with desired type, isPath and isCreate.</returns>
        /// <exception cref="DirfileException">Throws exception when no suitable function is found.</exception>
        private Action<string> GetDirfileFunc(string type, bool isPath = false, bool isCreate = true)
        {
            switch (type)
            {
                case CT.Director:
                    if (isCreate && !isPath)
                        return this.CreateDirector;
                    else if (isCreate)
                        return this.CreateDirectorPath;
                    else if (!isPath)
                        return this.DeleteDirector;
                    else
                        return this.DeleteDirectorPath;

                case CT.Filer:
                    if (isCreate && !isPath)
                        return this.CreateFiler;
                    else if (isCreate)
                        return this.CreateFilerPath;
                    else if (!isPath)
                        return this.DeleteFiler;
                    else
                        return this.DeleteFilerPath;
            }

            throw new DirfileException("No suitable function was found!");
        }
    }
}