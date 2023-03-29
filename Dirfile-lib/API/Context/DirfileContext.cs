// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 24/03/2023     \\

using System;
using System.Linq;
using Dirfile_lib.API.Extraction;
using Dirfile_lib.Exceptions;
using Dirfile_lib.Utilities.Validation;
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
        /// Initializes a new instance of the <see cref="DirfileContext"/> class.
        /// </summary>
        public DirfileContext()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DirfileContext"/> class.
        /// </summary>
        /// <param name="path">Path to director to work from (like relative path).</param>
        /// <param name="slashMode">Slash mode to use.</param>
        public DirfileContext(string path, SlashMode slashMode = SlashMode.Backward)
            : base(path, slashMode)
        {
        }

        /// <summary>
        /// Gets or sets the argument extractor.
        /// </summary>
        internal ArgumentExtractor ArgumentExtractor { get; set; }

        /// <inheritdoc/>
        protected override void Initialize(string path, SlashMode slashMode)
        {
            this.Extractor = new Extractor(slashMode);
            this.Extractor.Extract(path);
            this.ArgumentExtractor = new ArgumentExtractor(slashMode);

            if (string.IsNullOrEmpty(this.Extractor.Arguments))
                base.Initialize(path, slashMode);
            else
            {
                this.ArgumentExtractor.Extract(this.Extractor.Arguments);
                base.Initialize(this.Extractor.DirectorPath, slashMode);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="input"></param>
        public void Create(string input, bool isPath = false)
        {
            if (isPath)
            {
                this.Extractor.Extract(input);
                this.DirectorChange(this.Extractor.DirectorPath);

                this.CreateImpl(this.Extractor.Arguments);
            }
            else
                this.CreateImpl(input);
        }

        /// <summary>
        /// Creates filers and directors from given input. Can be either arguments (works from relative path)
        ///     or can be arguments with path (like absolute path).
        /// </summary>
        /// <param name="input"></param>
        private void CreateImpl(string input)
        {
            this.ArgumentExtractor.Extract(input);

            if (this.SlashMode == SlashMode.Forward)
                PathValidator.Instance.SwitchSlashMode();

            foreach (var arg in this.ArgumentExtractor.ArgumentsInOrder.Select((val, ind) => new { val, ind }))
            {
                Action<string> func = null;
                if (arg.ind == 0)
                {
                    func = this.GetDirfileFunc(arg.val.Key);
                    func(arg.val.Value);
                    continue;
                }

                switch (this.ArgumentExtractor.OperationsInOrder[arg.ind - 1])
                {
                    case "\\":
                        this.DirectorChange(this.CurrentDirector.Path + "\\" + this.ArgumentExtractor.ArgumentsInOrder[arg.ind - 1].Value);
                        this.GetDirfileFunc(arg.val.Key).Invoke(arg.val.Value);
                        break;

                    case ">":
                        this.GetDirfileFunc(arg.val.Key).Invoke(arg.val.Value);
                        break;

                    case ":>":
                        this.DirectorChange(this.CurrentDirector.Path.Substring(0, this.CurrentDirector.Path.LastIndexOf("\\")));
                        this.GetDirfileFunc(arg.val.Key).Invoke(arg.val.Value);
                        break;
                }
            }

            if (this.SlashMode == SlashMode.Forward)
                PathValidator.Instance.SwitchSlashMode();
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

        /// <inheritdoc/>
        public override void Dispose()
        {
            //base.Dispose();
        }
    }
}