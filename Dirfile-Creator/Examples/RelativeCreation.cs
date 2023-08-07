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
    /// Examples of creating Directors and filers from Command line using DirfileContext with relative path.
    /// </summary>
    internal static class RelativeCreation
    {
        /// <summary>
        /// The simplest example of creating a new directory.
        /// For more information aboud available operations, see Documentation.
        /// </summary>
        public static void RunExampleOne()
        {
            // Need to pass path to a desired directory 
            string actualDirectory = Directory.GetCurrentDirectory();

            // 2 ways of creating directory
            using (var dirfileContext = new DirfileContext(actualDirectory, Mode.Backward)) // Specify desired SlashMode
            {
                // First way is to call CreateDirector method, where we specify name of a new director.
                dirfileContext.CreateDirector("testDirectory");

                // Or second way is to call Create method, here we need to use slash, based on our SlashMode, before the name.
                dirfileContext.Create("\\testDirectory2");
            }
        }

        /// <summary>
        /// More advanced example.
        /// For more information aboud available operations, see Documentation.
        /// </summary>
        public static void RunExampleTwo()
        {
            string actualDirectory = Directory.GetCurrentDirectory();
            
            // Creates 2 directories in the same path with files
            var createString = "/testDirectory3/testFile1.txt :> testDirectory4/testFile2.json";

            using (var dirfileContext = new DirfileContext(actualDirectory, Mode.Backward))
            {
                // We can switch our SlashMode later on as
                dirfileContext.SwitchSlashMode(); // Now we'll be using '/'

                // Creates directory called 'testDirectory3' and a file in it called 'testFile1.txt'
                dirfileContext.Create(createString);
            }
        }
    }
}
