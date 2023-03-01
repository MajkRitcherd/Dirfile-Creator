// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 01/03/2023     \\

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Dirfile_lib.Core;
using Dirfile_lib.Exceptions;
using CD = Dirfile_lib.Core.Constants.DefaultValues;

[assembly: InternalsVisibleTo("Dirfile-lib-TEST")]

namespace Dirfile_lib.Utilities.Checks
{
    /// <summary>
    /// <see cref="NameChecker"/> checks string (only name of Director of Filer, not full path) 
    ///  and gets Dirfile name, extension and type if it is Filer, Director or emtpy string.
    /// </summary>
    /// <exception cref="DirfileException">Can throw exceptions.</exception>
    internal class NameChecker : AbstractChecker, IOverEnums
    {
        /// <summary>
        /// List of invalid characters.
        /// </summary>
        private readonly List<char> _InvalidCharacters = new List<char>()
        {
            '\\', '<', '>', '*', '?', '/','"', ':', '|'
        };

        /// <summary>
        /// List of invalid characters that are forbidden in Dirfile name 
        ///  (name can't starts or ends with these symbols).
        /// </summary>
        private readonly List<char> _InvalidStartEnd = new List<char>()
        {
            ' ', '.', '-', '_'
        };

        /// <summary>
        /// Gets enum finder to find item in enums.
        /// </summary>
        private readonly EnumFinder _EnumFinder = new EnumFinder();

        /// <summary>
        /// Gets dirfile recognizer.
        /// </summary>
        private readonly DirfileNameRecognizer _Recognizer = new DirfileNameRecognizer();

        /// <summary>
        /// Gets or sets type, represented as string, of checked string (If it is Filer or Director).
        /// </summary>
        public string DirfileType { get; protected set; }

        /// <summary>
        /// Gets or sets name of Filer or Director.
        /// </summary>
        public string DirfileName { get; protected set; }

        /// <inheritdoc/>
        protected override bool Check(string strToCheck)
        {
            if (!this.ValidateString(strToCheck))
                return false;

            this.SetProps(strToCheck);
            return true;
        }

        /// <summary>
        /// Converts extension from string to Extensions enum.
        /// </summary>
        /// <returns>Extension from Extensions.</returns>
        /// <exception cref="DirfileException">Thrown if DirfileType is null.</exception>
        public object GetExtension()
        {
            if (string.IsNullOrEmpty(this.DirfileExtension))
                throw new DirfileException("Extension could not be an empty string.");

            return this._EnumFinder.FindOverEnums(this.DirfileExtension);
        }

        /// <inheritdoc/>
        public override void Clean()
        {
            base.Clean();

            this.DirfileType = null;
            this.DirfileName = null;
        }

        /// <summary>
        /// Checks whether string contains invalid characters.
        /// </summary>
        /// <param name="str">String to check.</param>
        /// <returns>True if contains invalid characters, otherwise false.</returns>
        private bool ContainsInvalidCharacters(string str) => str.Any(s => this._InvalidCharacters.Contains(s));

        /// <summary>
        /// Checks whether string starts or ends with a space, period, hyphen or underline
        /// </summary>
        /// <param name="str">String to check.</param>
        /// <returns>True if contains wrong start or end, otherwise false.</returns>
        private bool InvalidStartOrEndOfName(string str)
        {
            foreach (var invalidChar in this._InvalidStartEnd)
                if (str.StartsWith(invalidChar.ToString()) || str.EndsWith(invalidChar.ToString()))
                    return true;

            return false;
        }

        /// <summary>
        /// Checkes whether string's length is greater than allowed value.
        /// </summary>
        /// <param name="str">String to check.</param>
        /// <returns>True if it is greater, otherwise false.</returns>
        private bool IsNameLengthGreater(string strToCheck) => strToCheck.Length > CD.MaxNameLength;

        /// <summary>
        /// Validates given string.
        /// </summary>
        /// <param name="strToCheck">String to check.</param>
        /// <returns>True if valid, otherwise false.</returns>
        private bool ValidateString(string strToCheck)
        {
            if (this.ContainsInvalidCharacters(strToCheck))
            {
                this.ErrorMsg = "The input string contains invalid character/s. or starts/ends with invalid characters";
                return false;
            }

            if (this.InvalidStartOrEndOfName(strToCheck))
            {
                this.ErrorMsg = "The input string starts/ends with invalid characters";
                return false;
            }

            if (this.IsNameLengthGreater(strToCheck))
            {
                this.ErrorMsg = "The DirfileName's length is greater than maximum allowed [30].";
                return false;
            }

            return true;
        }

        /// <summary>
        /// Sets properties.
        /// </summary>
        /// <param name="strToCheck">String to check.</param>
        private void SetProps(string strToCheck)
        {
            this.DirfileType = this._Recognizer.Recognize(strToCheck, out string name, out string ext, out bool isExtension);
            this.DirfileName = name;
            this.DirfileExtension = ext;
        }

        /// <inheritdoc/>
        public object FindOverEnums<TSearch>(TSearch strToFind)
        {
            if (typeof(TSearch) != typeof(string) || typeof(TSearch) != typeof(object))
                throw new DirfileException("T cannot be other than string or object");

            var dirType = typeof(DirfileExtensions);
            var dirProps = dirType.GetMembers();

            // Loops over every member of a class
            foreach (var prop in dirProps.Select((value, index) => new { value, index }))
            {
                if (prop.value.DeclaringType.Name != "DirfileExtensions" || prop.value.Name == ".ctor")
                    continue;

                var member = (Type)dirProps.Where(_ => true).ElementAt(prop.index);

                // Loops over every enum defined in that class
                foreach (var item in Enum.GetNames(member))
                {
                    if (item.Equals(strToFind.ToString().Substring(1).ToUpperInvariant(), StringComparison.InvariantCultureIgnoreCase))
                    {
                        return Enum.Parse(member, item);
                    }
                }
            }

            throw new DirfileException("Extension was not found in enums.");
        }
    }
}
