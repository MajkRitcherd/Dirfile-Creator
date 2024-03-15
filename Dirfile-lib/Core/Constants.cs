// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 15/03/2024     \\

namespace Dirfile_lib.Core.Constants
{
    /// <summary>
    /// Declaration of default values constants
    /// </summary>
    internal static class DefaultValues
    {
        /// <summary>
        /// Buffer's default size.
        /// </summary>
        internal const int BufferSize = 4096;
        
        /// <summary>
        /// Maximum filename length.
        /// </summary>
        internal const int MaxNameLength = 30;
    }

    /// <summary>
    /// Declaration of regex patterns.
    /// </summary>
    internal static class RegexPatterns
    {
        /// <summary>
        /// Regex pattern for arguments.
        /// </summary>
        internal const string ArgumentsPattern = @"^\\(?>[^<>:\""/\|\\?*]{1,32}(\\|>|:>|(:""[\w\s]*""){1})?)+$";

        /// <summary>
        /// Regex pattern for Dirfile normalized (using backslash) path.
        /// </summary>
        internal const string DirfileNormalizedPathPattern = @"^(?:[\w]\:|\\)(\\[^\\|<|>|*|/|:|?|\\||""]+[^ ])*$";
        
        /// <summary>
        /// Regex pattern for Dirfile path.
        /// </summary>
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
        internal static class DirfileProps
        {
            /// <summary>
            /// Constructor property of Dirfile as a text.
            /// </summary>
            internal const string Constructor = ".ctor";
            
            /// <summary>
            /// Directory property of Dirfile as a text.
            /// </summary>
            internal const string Directory = "Directory";
            
            /// <summary>
            /// DirfileExtensions property of Dirfile as a text.
            /// </summary>
            internal const string DirfileExtensions = "DirfileExtensions";
            
            /// <summary>
            /// Exists property of Dirfile as a text.
            /// </summary>
            internal const string Exists = "Exists";
            
            /// <summary>
            /// Length property of Dirfile as a text.
            /// </summary>
            internal const string Length = "Length";
            
            /// <summary>
            /// Parent property of Dirfile as a text.
            /// </summary>
            internal const string Parent = "Parent";
            
            /// <summary>
            /// Root property of Dirfile as a text.
            /// </summary>
            internal const string Root = "Root";
        }
    }

    /// <summary>
    /// Dirfile constants
    /// </summary>
    public static class DirFile
    {
        /// <summary>
        /// Character constants.
        /// </summary>
        internal static class Characters
        {
            /// <summary>
            /// Back slash character.
            /// </summary>
            internal const char BSlash = '\\';
            
            /// <summary>
            /// Forward slash character.
            /// </summary>
            internal const char FSlash = '/';
            
            /// <summary>
            /// Colon character.
            /// </summary>
            internal const char Colon = ':';
            
            /// <summary>
            /// Right arrow character.
            /// </summary>
            internal const char RightArrow = '>';

            /// <summary>
            /// Quote character.
            /// </summary>
            internal const char Quote = '\"';
        }

        /// <summary>
        /// Holds dirfile operation constants.
        /// </summary>
        public static class Operations
        {
            /// <summary>
            /// Start of initial text;
            /// </summary>
            public const string StartOfText = ":\"";

            /// <summary>
            /// End of inital text.
            /// </summary>
            public const string EndOfText = "\"";

            /// <summary>
            /// Director change to child.
            /// </summary>
            public const string Change = "\\";

            /// <summary>
            /// Next dirfile to create.
            /// </summary>
            public const string Next = ">";

            /// <summary>
            /// Director change to parent.
            /// </summary>
            public const string Prev = ":>";
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