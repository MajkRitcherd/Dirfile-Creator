// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 22/03/2023     \\

using System.Collections.Generic;
using Dirfile_lib.Exceptions;
using Dirfile_lib.Utilities.Validation;
using CT = Dirfile_lib.Core.Constants.Texts;

namespace Dirfile_lib.API.Extraction
{
    /// <summary>
    /// Extracts arguments from an argument string.
    /// </summary>
    internal class ArgumentExtractor : BaseExtractor
    {
        /// <summary>
        /// Gets or sets list which holds arguments in order.
        /// </summary>
        internal List<KeyValuePair<string, string>> ArgumentsInOrder = new List<KeyValuePair<string, string>>();

        /// <summary>
        /// Gets or sets list which holds operations in order.
        /// </summary>
        internal List<string> OperationsInOrder = new List<string>();

        /// <summary>
        /// Gets or sets temporary list.
        /// </summary>
        private string _TemporaryString { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentExtractor"/> class.
        /// </summary>
        /// <param name="mode"></param>
        internal ArgumentExtractor(SlashMode mode, string arguments = "")
            : base(mode)
        {
            this.NameChecker = new NameChecker();

            if (!string.IsNullOrEmpty(arguments))
                this.Extract(arguments);
        }

        /// <summary>
        /// Gets <see cref="NameChecker"/> instance.
        /// </summary>
        internal NameChecker NameChecker { get; private set; }

        /// <summary>
        /// Gets <see cref="ArgumentParser"/> instance.
        /// </summary>
        private ArgumentParser _Parser { get; set; } = ArgumentParser.Instance;

        /// <summary>
        /// Extracts one argument from argument string.
        /// </summary>
        /// <returns>String argument.</returns>
        private string GetArgument(out int index)
        {
            string argument;
            var startIndex = 0;
            index = this._TemporaryString.IndexOfAny(new char[] { '\\', '>', ':' }, startIndex);

            if (index > -1)
            {
                if (this._TemporaryString[index] == ':')
                    this.OperationsInOrder.Add(this._TemporaryString[index].ToString() + this._TemporaryString[index + 1].ToString());
                else
                    this.OperationsInOrder.Add(this._TemporaryString[index].ToString());
            }

            if (index == -1)
                argument = this._TemporaryString;
            else
                argument = this._TemporaryString.Substring(startIndex, index - startIndex);

            return argument.Trim();
        }

        /// <inheritdoc/>
        public override void Extract(string input)
        {
            this.ArgumentsInOrder.Clear();
            this.OperationsInOrder.Clear();
            this.InputString = input;

            this.NormalizeInput();

            if (!this._Parser.IsValid(this._NormalizedInputString))
                throw new DirfileException($"Argument string '{input}' is not valid!");

            this._TemporaryString = this._NormalizedInputString.Trim('\\');
            this.ExtractInternal();
        }

        /// <summary>
        /// Extracts the input string.
        /// </summary>
        /// <exception cref="DirfileException">Throws exception when one of arguments name is not valid.</exception>
        private void ExtractInternal()
        {
            // Loop over every argument in a string and
            //  decide whether it if of a type 'Filer' or 'Director'
            while (!string.IsNullOrEmpty(this._TemporaryString))
            {
                var arg = this.GetArgument(out int index);
                this.NameChecker.IsValid(arg);

                if (this.NameChecker.Unsuccessful.HasValue && this.NameChecker.Unsuccessful.Value)
                    throw new DirfileException($"{this.NameChecker.ErrorMsg}");

                switch (this.NameChecker.DirfileType)
                {
                    case CT.Director:
                        ArgumentsInOrder.Add(new KeyValuePair<string, string>(CT.Director, this.NameChecker.DirfileName));
                        break;

                    case CT.Filer:
                        ArgumentsInOrder.Add(new KeyValuePair<string, string>(CT.Filer, this.NameChecker.DirfileName + this.NameChecker.DirfileExtension));
                        break;
                }

                this.RemoveArgument(index);
                this.NameChecker.Clean();
            }
        }

        /// <summary>
        /// Gets new argument string from index.
        /// </summary>
        /// <param name="index">Index of a new substring.</param>
        /// <returns>Argument string.</returns>
        private string GetArgumentSubstring(int index)
        {
            if (index == -1)
                return string.Empty;

            if (this._TemporaryString[index] == ':')
                return this._TemporaryString.Substring(index + 2);
            else
                return this._TemporaryString.Substring(index + 1);
        }

        /// <summary>
        /// Removes argument from argument string.
        /// </summary>
        /// <param name="index">Index of a new substring.</param>
        private void RemoveArgument(int index) => this._TemporaryString = this.GetArgumentSubstring(index).Trim();
    }
}