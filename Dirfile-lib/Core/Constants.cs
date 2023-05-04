﻿// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 24/04/2023     \\

namespace Dirfile_lib.Core.Constants
{
    /// <summary>
    /// Declaration of default values constants
    /// </summary>
    internal static class DefaultValues
    {
        internal const int BufferSize = 4096;
        internal const int MaxNameLength = 30;
    }

    /// <summary>
    /// Declaration of regex patterns.
    /// </summary>
    internal static class RegexPatterns
    {
        internal const string ArgumentsPattern = "^\\\\(?>[^\\\\|<|>|*|/|:|?|\\\\||\"\"]{1,32}([\\\\|>|\"]|(:>|:\"))?)+$";
        internal const string DirfileNormalizedPathPattern = @"^(?:[\w]\:|\\)(\\[^\\|<|>|*|/|:|?|\\||""]+[^ ])*$";
        internal const string DirfilePathPattern = @"^(?:[\w]\:|/)(/[^\\|<|>|*|/|:|?|\\||""]+[^ ])*$";
    }

    /// <summary>
    /// Declaration of text constants.
    /// </summary>
    internal static class Texts
    {
        /// <summary>
        /// Text constants related to properties.
        /// </summary>
        internal static class Props
        {
            internal const string Constructor = ".ctor";
            internal const string Directory = "Directory";
            internal const string DirfileExtensions = "DirfileExtensions";
            internal const string Exists = "Exists";
            internal const string Length = "Length";
            internal const string Parent = "Parent";
            internal const string Root = "Root";
        }
    }

    /// <summary>
    /// Dirfile constants
    /// </summary>
    internal static class DirFile
    {
        /// <summary>
        /// Character constants.
        /// </summary>
        internal static class Characters
        {
            internal const char BSlash = '\\';
            
            internal const char FSlash = '/';
            
            internal const char Colon = ':';
            
            internal const char RightArrow = '>';

            internal const char Quote = '\"';
        }

        /// <summary>
        /// Holds dirfile operation constants.
        /// </summary>
        internal static class Operations
        {
            /// <summary>
            /// Start of initial text;
            /// </summary>
            internal const string StartOfText = ":\"";

            /// <summary>
            /// End of inital text.
            /// </summary>
            internal const string EndOfText = "\"";

            /// <summary>
            /// Director change to child.
            /// </summary>
            internal const string Change = "\\";

            /// <summary>
            /// Next dirfile to create.
            /// </summary>
            internal const string Next = ">";

            /// <summary>
            /// Director change to parent.
            /// </summary>
            internal const string Prev = ":>";
        }

        /// <summary>
        /// Represents Dirfile types.
        /// </summary>
        internal static class Types
        {
            /// <summary>
            /// Director type.
            /// </summary>
            internal const string Director = "Director";

            /// <summary>
            /// Filer type.
            /// </summary>
            internal const string Filer = "Filer";

            /// <summary>
            /// Init text type.
            /// </summary>
            internal const string InitText = "InitText";
        }
    }
}