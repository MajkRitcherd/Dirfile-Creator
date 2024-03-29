﻿// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 15/03/2024     \\

using System.Collections.Generic;
using System.Linq;
using Dirfile_lib.API.Extraction.Modes;
using Dirfile_lib.Exceptions;
using Dirfile_lib.Utilities.Validation;
using Chars = Dirfile_lib.Core.Constants.DirFile.Characters;
using DirfileOperations = Dirfile_lib.Core.Constants.DirFile.Operations;
using DirfileType = Dirfile_lib.Core.Constants.DirFile.Types;

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
        internal List<KeyValuePair<string, string>> ArgumentsByTypeInOrder = new List<KeyValuePair<string, string>>();

        /// <summary>
        /// Gets or sets list which holds operations in order.
        /// </summary>
        internal List<string> OperationsInOrder = new List<string>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentExtractor"/> class.
        /// </summary>
        /// <param name="slashMode">Slash mode to use.</param>
        /// <param name="argumentString">String consisting of dirfiles to create, e.g. '\TestDir > testFile.txt:"I am inside a file" > testDir2'.</param>
        internal ArgumentExtractor(SlashMode slashMode, string argumentString = "")
            : base(slashMode)
        {
            this.NameChecker = new NameValidator();

            if (!string.IsNullOrEmpty(argumentString))
                this.Extract(argumentString);
        }

        /// <summary>
        /// Gets <see cref="NameChecker"/> instance.
        /// </summary>
        internal NameValidator NameChecker { get; private set; }

        /// <summary>
        /// Gets <see cref="ArgumentValidator"/> instance.
        /// </summary>
        private ArgumentValidator _Parser { get; set; } = ArgumentValidator.Instance;

        /// <summary>
        /// Gets or sets temporary list.
        /// </summary>
        private string _TemporaryString { get; set; }

        /// <inheritdoc/>
        internal override void Extract(string inputString)
        {
            this.ArgumentsByTypeInOrder.Clear();
            this.OperationsInOrder.Clear();
            this.ReceivedString = inputString;

            this.NormalizeInput();

            if (this._Parser.IsInvalid(this._NormalizedInputString))
                throw new DirfileException($"Argument string '{inputString}' is not valid!");

            this._TemporaryString = this._NormalizedInputString.Trim(Chars.BSlash);
            this.ExtractInternal();
        }

        /// <summary>
        /// Extracts one argument from argument string.
        /// </summary>
        /// <returns>String argument.</returns>
        private string ExtractArgumentAndOperations(out int startIndexOfArgumentText)
        {
            string argumentText;
            var zeroIndex = 0;
            startIndexOfArgumentText = this._TemporaryString.Trim().IndexOfAny(new char[] { Chars.BSlash, Chars.RightArrow, Chars.Colon, Chars.Quote }, zeroIndex);

            if (this.OperationsInOrder.Count > 0 && this.OperationsInOrder.Last() == DirfileOperations.StartOfText)
            {
                this.OperationsInOrder.Add(this._TemporaryString[startIndexOfArgumentText].ToString());
                var indexInitialization = this._TemporaryString.Trim().IndexOfAny(new char[] { Chars.BSlash, Chars.RightArrow, Chars.Colon, Chars.Quote }, zeroIndex);

                return this._TemporaryString.Substring(0, indexInitialization).Trim();
            }

            if (startIndexOfArgumentText > -1)
            {
                if (this._TemporaryString[startIndexOfArgumentText] == Chars.Colon)
                    this.OperationsInOrder.Add(this._TemporaryString[startIndexOfArgumentText].ToString() + this._TemporaryString[startIndexOfArgumentText + 1].ToString());
                else
                    this.OperationsInOrder.Add(this._TemporaryString[startIndexOfArgumentText].ToString());
            }

            if (startIndexOfArgumentText == -1)
                argumentText = this._TemporaryString;
            else
                argumentText = this._TemporaryString.Substring(zeroIndex, startIndexOfArgumentText - zeroIndex);

            return argumentText.Trim();
        }

        /// <summary>
        /// Extracts inital text from inputString.
        /// </summary>
        /// <exception cref="DirfileException">Throws exception either if initialization text is not closed or is not followed by Dirfile operation.</exception>
        private void ExtractInitialText()
        {
            if (this.OperationsInOrder.Last() == DirfileOperations.EndOfText)
            {
                this.ArgumentsByTypeInOrder.Add(new KeyValuePair<string, string>(DirfileType.InitText, this.NameChecker.DirfileName));
            }
            else
                throw new DirfileException($"Initialization text must be closed with character [{DirfileOperations.EndOfText}]!");
        }

        /// <summary>
        /// Extracts the input string.
        /// </summary>
        /// <exception cref="DirfileException">Throws exception when one of arguments name is not valid.</exception>
        private void ExtractInternal()
        {
            // Loop over every argument in a string and
            //  decide whether it is of a type 'Filer' or 'Director' or 'InitText'
            while (!string.IsNullOrEmpty(this._TemporaryString))
            {
                var argumentText = this.ExtractArgumentAndOperations(out int startIndexOfArgumentText);

                if (this.NameChecker.IsInvalid(argumentText))
                    throw new DirfileException($"{this.NameChecker.ErrorMessage}");

                if (this.OperationsInOrder.Count > 1 && this.OperationsInOrder.ElementAt(this.OperationsInOrder.Count - 2) == DirfileOperations.StartOfText)
                {
                    this.ExtractInitialText();

                    string operation = this.GetOperationAfterEndOfText(ref startIndexOfArgumentText);

                    if (!string.IsNullOrEmpty(operation))
                        this.OperationsInOrder.Add(operation);
                }
                else
                {
                    switch (this.NameChecker.DirfileType)
                    {
                        case DirfileType.Director:
                            this.ArgumentsByTypeInOrder.Add(new KeyValuePair<string, string>(DirfileType.Director, this.NameChecker.DirfileName));
                            break;

                        case DirfileType.Filer:
                            this.ArgumentsByTypeInOrder.Add(new KeyValuePair<string, string>(DirfileType.Filer, this.NameChecker.DirfileName + this.NameChecker.ExtensionName));
                            break;
                    }
                }

                this.RemoveArgumentAndOperations(startIndexOfArgumentText);
                this.NameChecker.Clean();
            }
        }

        /// <summary>
        /// Gets new argument string from index.
        /// </summary>
        /// <param name="startIndex">Index of a new substring.</param>
        /// <returns>Argument string.</returns>
        private string GetArgumentSubstring(int startIndex)
        {
            if (startIndex == -1)
                return string.Empty;

            if (this._TemporaryString[startIndex] == Chars.Colon)
                return this._TemporaryString.Substring(startIndex + 2);
            else
                return this._TemporaryString.Substring(startIndex + 1);
        }

        /// <summary>
        /// Gets operation followed after end of text operation.
        /// </summary>
        /// <param name="startIndexOfArgumentText">Start index of argument text.</param>
        /// <returns>Operation or string.Empty.</returns>
        private string GetOperationAfterEndOfText(ref int startIndexOfArgumentText)
        {
            string temporarySubstring = this._TemporaryString.Substring(startIndexOfArgumentText + 1).Trim();

            if (string.IsNullOrEmpty(temporarySubstring))
                return string.Empty;

            var nextCharacter = temporarySubstring?.First();

            string operation;
            if (nextCharacter == Chars.Colon)
                operation = $"{nextCharacter}{temporarySubstring.ElementAt(1)}";
            else
                operation = nextCharacter.ToString();

            startIndexOfArgumentText = this._TemporaryString.IndexOf(operation);

            if (this.IsOperationAfterEndOfText(operation))
                return operation;

            return string.Empty;
        }

        /// <summary>
        /// Checks whether given operation is allowed after the end of text operation.
        /// </summary>
        /// <param name="operation">Operation.</param>
        /// <returns>True, if operation is allowed, otherwise false.</returns>
        private bool IsOperationAfterEndOfText(string operation)
        {
            return operation == DirfileOperations.Change ||
                operation == DirfileOperations.Prev ||
                operation == DirfileOperations.Next;
        }

        /// <summary>
        /// Removes argument from argument string.
        /// </summary>
        /// <param name="startIndex">Index of a new substring.</param>
        private void RemoveArgumentAndOperations(int startIndex) => this._TemporaryString = this.GetArgumentSubstring(startIndex).Trim();
    }
}