// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 24/04/2023     \\

using System;
using System.Linq;
using Dirfile_lib.API.Extraction;
using Dirfile_lib.API.Extraction.Modes;
using Dirfile_lib.Core.Dirfiles;
using Dirfile_lib.Exceptions;
using Chars = Dirfile_lib.Core.Constants.DirFile.Characters;
using DirfileOperations = Dirfile_lib.Core.Constants.DirFile.Operations;
using DirTypes = Dirfile_lib.Core.Constants.DirFile.Types;

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
        /// <param name="directorPath">Path to the director to work from (like relative path).</param>
        /// <param name="slashMode">Slash mode to use.</param>
        public DirfileContext(string directorPath, SlashMode slashMode = SlashMode.Backward)
            : base(directorPath, slashMode, PathMode.Relative)
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
        /// <param name="inputString">Input string.</param>
        public void Create(string inputString)
        {
            if (this.PathMode == PathMode.Absolute)
            {
                this.Extractor.Extract(inputString);
                this.ChangeCurrentDirector(this.Extractor.DirectorPath);

                this.CreateInternal(this.Extractor.ArgumentString);
            }
            else
                this.CreateInternal(inputString);
        }

        /// <inheritdoc/>
        protected override void Initialize(string initialDirectorPath, SlashMode slashMode, PathMode pathMode)
        {
            this.Extractor = new Extractor(slashMode);
            this.Extractor.Extract(initialDirectorPath);
            this.ArgumentExtractor = new ArgumentExtractor(slashMode);

            if (string.IsNullOrEmpty(this.Extractor.ArgumentString))
                base.Initialize(initialDirectorPath, slashMode, pathMode);
            else
            {
                this.ArgumentExtractor.Extract(this.Extractor.ArgumentString);
                base.Initialize(this.Extractor.DirectorPath, slashMode, pathMode);
            }
        }

        /// <summary>
        /// Creates filers and directors from given input. Can be either arguments (works from relative path)
        ///     or can be arguments with path (like absolute path).
        /// </summary>
        /// <param name="inputString"></param>
        private void CreateInternal(string inputString)
        {
            this.ArgumentExtractor.Extract(inputString);
            var indexOffset = -1;

            foreach (var argumentWithIndex in this.ArgumentExtractor.ArgumentsByTypeInOrder.Select((argument, index) => new { argument, index }))
            {
                var slashMode = this.SlashMode == SlashMode.Backward ? Chars.BSlash : Chars.FSlash;

                if (argumentWithIndex.index == 0)
                {
                    this.GetOperationFunctionByType(argumentWithIndex.argument.Key).Invoke(argumentWithIndex.argument.Value);
                    continue;
                }

                switch (this.ArgumentExtractor.OperationsInOrder.ElementAt(argumentWithIndex.index + indexOffset))
                {
                    case DirfileOperations.Change:
                        this.ChangeCurrentDirector(this.CurrentPath + slashMode + this.ArgumentExtractor.ArgumentsByTypeInOrder[argumentWithIndex.index - 1].Value);
                        this.GetOperationFunctionByType(argumentWithIndex.argument.Key).Invoke(argumentWithIndex.argument.Value);
                        break;

                    case DirfileOperations.Next:
                        this.GetOperationFunctionByType(argumentWithIndex.argument.Key).Invoke(argumentWithIndex.argument.Value);
                        break;

                    case DirfileOperations.Prev:
                        this.ChangeCurrentDirector(this.CurrentPath.Substring(0, this.CurrentPath.LastIndexOf(slashMode)));
                        this.GetOperationFunctionByType(argumentWithIndex.argument.Key).Invoke(argumentWithIndex.argument.Value);
                        break;
                    case DirfileOperations.StartOfText:
                        {
                            if (this.ArgumentExtractor.ArgumentsByTypeInOrder[argumentWithIndex.index - 1].Key != DirTypes.Filer)
                                throw new DirfileException("Text can be initially created only in Filers!");

                            this.WriteInitialTextToFiler(argumentWithIndex.index);
                            indexOffset++;
                            break;
                        }
                }
            }
        }

        /// <summary>
        /// Gets function from BaseDirfileContext based on whether its type is 'Director' or 'Filer'
        ///     is absolute path or not, is creation function or deletion function.
        /// </summary>
        /// <param name="type">Type of dirfile (either Filer or Director).</param>
        /// <param name="isPath">Use absolute path or not.</param>
        /// <param name="isCreate">Use creation function or deletion function.</param>
        /// <returns>Function with desired type, isPath and isCreate.</returns>
        /// <exception cref="DirfileException">Throws exception when no suitable function is found.</exception>
        private Action<string> GetOperationFunctionByType(string type, bool isPath = false, bool isCreate = true)
        {
            switch (type)
            {
                case DirTypes.Director:
                    if (isCreate && !isPath)
                        return this.CreateDirector;
                    else if (isCreate)
                        return this.CreateDirectorFromAbsolutePath;
                    else if (!isPath)
                        return this.DeleteDirector;
                    else
                        return this.DeleteDirectorFromAbsolutePath;

                case DirTypes.Filer:
                    if (isCreate && !isPath)
                        return this.CreateFiler;
                    else if (isCreate)
                        return this.CreateFilerFromAbsolutePath;
                    else if (!isPath)
                        return this.DeleteFiler;
                    else
                        return this.DeleteFilerFromAbsolutePath;
            }

            throw new DirfileException($"No suitable function was found for type: {type}!");
        }

        /// <summary>
        /// Writes initial text into filer.
        /// </summary>
        /// <param name="indexOfInitTextInArguments">Index of InitText in arguments list.</param>
        private void WriteInitialTextToFiler(int indexOfInitTextInArguments)
        {
            string path = this.SlashMode == SlashMode.Forward ? this.CurrentPath.Replace('/', '\\') : this.CurrentPath;

            path += Chars.BSlash + this.ArgumentExtractor.ArgumentsByTypeInOrder.ElementAt(indexOfInitTextInArguments - 1).Value.ToString();

            var filer = new Filer(path);
            filer.WriteString(this.ArgumentExtractor.ArgumentsByTypeInOrder.ElementAt(indexOfInitTextInArguments).Value);
        }
    }
}