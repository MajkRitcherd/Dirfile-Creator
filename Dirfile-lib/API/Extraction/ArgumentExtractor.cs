// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 14/03/2023     \\

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
        /// Initializes a new instance of the <see cref="ArgumentExtractor"/> class.
        /// </summary>
        /// <param name="arguments"></param>
        internal ArgumentExtractor(string arguments, SlashMode mode)
            : base(mode)
        {
            this.InputString = arguments;
            this.NameChecker = new NameChecker();

            this.NormalizeInput();

            if (!this._Parser.IsValid(this._NormalizedInputString))
                throw new DirfileException($"Argument string '{arguments}' is not valid!");

            this._NormalizedInputString = this._NormalizedInputString.Trim('\\');
            this.Extract(this._NormalizedInputString);
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
            index = this._NormalizedInputString.IndexOfAny(new char[] { '\\', '>', ':' }, startIndex);

            if (index > -1)
            {
                if (this._NormalizedInputString[index] == ':')
                    this.OperationsInOrder.Add(this._NormalizedInputString[index].ToString() + this._NormalizedInputString[index + 1].ToString());
                else
                    this.OperationsInOrder.Add(this._NormalizedInputString[index].ToString());
            }

            if (index == -1)
                argument = this._NormalizedInputString;
            else
                argument = this._NormalizedInputString.Substring(startIndex, index - startIndex);

            return argument.Trim();
        }

        /// <inheritdoc/>
        internal override void Extract(string input)
        {
            // Loop over every argument in a string and
            //  decide whether it if of a type 'Filer' or 'Director'
            while (!string.IsNullOrEmpty(this._NormalizedInputString))
            {
                var arg = this.GetArgument(out int index);
                this.NameChecker.IsValid(arg);

                if (this.NameChecker.Unsuccessful.HasValue && this.NameChecker.Unsuccessful.Value)
                    throw new DirfileException($"Argument '{this.NameChecker.ErrorMsg}' was not valid!");

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

            if (this._NormalizedInputString[index] == ':')
                return this._NormalizedInputString.Substring(index + 2);
            else
                return this._NormalizedInputString.Substring(index + 1);
        }

        /// <summary>
        /// Removes argument from argument string.
        /// </summary>
        /// <param name="index">Index of a new substring.</param>
        private void RemoveArgument(int index) => this._NormalizedInputString = this.GetArgumentSubstring(index).Trim();
    }
}