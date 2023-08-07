// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 07/08/2023     \\

using Dirfile_lib.API.Context;
using Mode = Dirfile_lib.API.Extraction.Modes.SlashMode;

namespace Dirfile_Creator.Examples
{
    /// <summary>
    /// Examples of creating Directors and filers from Command line using DirfileContext with absolute path.
    /// </summary>
    internal static class AbsoluteCreation
    {
        /// <summary>
        /// The simplest example of creating a new directory.
        /// For more information aboud available operations, see Documentation.
        /// </summary>
        public static void RunExampleOne()
        {
            // No need to pass path as for relative context
            using (var dirfileContext = new DirfileContext())
            {
                dirfileContext.CreateDirector("testDirectory1");

                dirfileContext.Create("\\testDirectory2");
            }
        }

        /// <summary>
        /// More advanced example.
        /// For more information aboud available operations, see Documentation.
        /// </summary>
        public static void RunExampleTwo()
        {
            string createDirfiles = "/testDirectory3/testFile1.txt :> testDirectory4/testFile2.json";

            using (var dirfileContext = new DirfileContext(Mode.Forward)) // Possible to pass SlashMode
            {
                dirfileContext.Create(createDirfiles);

                // Also it is possible to switch between PathModes using, see Documentation.
                dirfileContext.SwitchPathMode();
            }
        }
    }
}
