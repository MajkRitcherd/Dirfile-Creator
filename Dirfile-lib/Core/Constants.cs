﻿// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 07/03/2023     \\

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

    internal static class RegexPatterns
    {
        internal const string ArgumentsPattern = "^\\\\(?>[^\\\\|<|>|*|/|:|?|\\\\||\"\"]{1,32}([\\\\|>]|(:>))?)+$";
        internal const string DirfilePathPattern = @"^(?:[\w]\:|\\)(\\[a-zA-Z_\-\s0-9\.]+)*$";
    }

    /// <summary>
    /// Declaration of text constants.
    /// </summary>
    internal static class Texts
    {
        internal const string Director = "Director";
        internal const string Filer = "Filer";

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
}