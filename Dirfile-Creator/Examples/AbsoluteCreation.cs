// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 15/03/2024     \\

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
        /// Runs simple examples (using Absolute directory path) with backward slash mode, see methods
        /// For more information aboud available operations, see Documentation.
        /// </summary>
        public static void Run_BackwardMode()
        {
            // No need to pass path as for relative context
            using (var dirfileContext = new DirfileContext())
            {
                var workingDirectory = Directory.GetCurrentDirectory();

                Example1_DirectoryCreation(dirfileContext, workingDirectory);

                Example2_FilerCreation(dirfileContext, workingDirectory);

                Example3_DirectoryChangeInto(dirfileContext, workingDirectory);

                Example4_DirectoryChangeBack(dirfileContext, workingDirectory);

                Example5_FileInitialText(dirfileContext, workingDirectory);

                Example6_ComplexCreation(dirfileContext, workingDirectory);
            }
        }

        /// <summary>
        /// Runs simple examples (using Absolute directory path) with forward slash mode, see methods.
        /// For more information aboud available operations, see Documentation.
        /// </summary>
        public static void Run_ForwardMode()
        {
            // No need to pass path as for relative context
            using (var dirfileContext = new DirfileContext(Mode.Forward))
            {
                var workingDirectory = Directory.GetCurrentDirectory().Replace("\\", "/");

                Example1_DirectoryCreation(dirfileContext, workingDirectory, Mode.Forward);

                Example2_FilerCreation(dirfileContext, workingDirectory, Mode.Forward);

                Example3_DirectoryChangeInto(dirfileContext, workingDirectory, Mode.Forward);

                Example4_DirectoryChangeBack(dirfileContext, workingDirectory, Mode.Forward);

                Example5_FileInitialText(dirfileContext, workingDirectory, Mode.Forward);

                Example6_ComplexCreation(dirfileContext, workingDirectory, Mode.Forward);
            }
        }

        #region Individual examples

        /// <summary>
        /// Example how to create directory.<br/>
        /// 2 ways - using <b>CreateDirector</b> or <b>Create</b> method.
        /// </summary>
        /// <param name="context">Dirfile context.</param>
        /// <param name="absoluteWorkingPath">Absolute path to working directory.</param>
        /// <param name="slashMode">Slash mode.</param>
        private static void Example1_DirectoryCreation(DirfileContext context, string absoluteWorkingPath, Mode slashMode = Mode.Backward)
        {
            var slash = GetSlash(slashMode);

            // Notice the difference between parameters of CreateDirector and Create method
            // Create method requires to use slash and also be in a form of an absolute path
            // CreateDirector does not require slash and cannot have absolute path as parameter

            // Using CreateDirector method (Create and then delete)
            context.CreateDirector("testDir1");
            context.DeleteDirector("testDir1");

            // Using Create method
            context.Create($"{absoluteWorkingPath}{slash}testDir1");
            context.DeleteDirector("testDir1");
        }

        /// <summary>
        /// Example how to create file.<br/>
        /// 2 ways - using <b>CreateFiler</b> or <b>Create</b> method.
        /// </summary>
        /// <param name="context">Dirfile context.</param>
        /// <param name="absoluteWorkingPath">Absolute path to working directory.</param>
        /// <param name="slashMode">Slash mode.</param>
        private static void Example2_FilerCreation(DirfileContext context, string absoluteWorkingPath, Mode slashMode = Mode.Backward)
        {
            var slash = GetSlash(slashMode);

            // Notice the difference between parameters of CreateFiler and Create method
            // CreateFiler does not require to use back/forward slash (based on slash mode)
            // Create method requires to use slash

            // Using CreateFiler method
            context.CreateFiler("testFile1.txt");
            context.DeleteFiler("testFile1.txt");

            // Using Create method
            context.Create($"{absoluteWorkingPath}{slash}testFile1.csv");
            context.DeleteFiler("testFile1.csv");

            // Using Create method with multiple Filers
            // Creates all 3 Files in the same directory
            context.Create($"{absoluteWorkingPath}{slash}testFile1.csv > testFile2.txt > testFile3.xlsx");
            context.DeleteFiler("testFile1.csv");
            context.DeleteFiler("testFile2.txt");
            context.DeleteFiler("testFile3.xlsx");

            // To see all available file extensions that can be created, check the DirfileExtensions class.
        }

        /// <summary>
        /// Example how to change current directory in Create method.
        /// </summary>
        /// <param name="context">Dirfile context.</param>
        /// <param name="absoluteWorkingPath">Absolute path to working directory.</param>
        /// <param name="slashMode">Slash mode.</param>
        private static void Example3_DirectoryChangeInto(DirfileContext context, string absoluteWorkingPath, Mode slashMode = Mode.Backward)
        {
            var slash = GetSlash(slashMode);

            // It creates 'testDir1' in absolute working path (given as argument to the DirfileContext)
            // Then it changes its actual directory to the create directory 'testDir1' and
            //  creates the 'testDir2' directory inside the directory 'testDir1'.
            context.Create($"{absoluteWorkingPath}{slash}testDir1 {slash} testDir2");

            // Deletes recursively all Dirfiles in this 'testDir1' and also deletes the 'testDir1' directory.
            var directory = slashMode == Mode.Forward ? Directory.GetCurrentDirectory().Replace('\\', '/') : Directory.GetCurrentDirectory();

            context.ChangeCurrentDirector(directory);
            context.DeleteDirectorRecursive("testDir1");
        }

        /// <summary>
        /// Example how to change current directory back to parent one.
        /// </summary>
        /// <param name="context">Dirfile context.</param>
        /// <param name="absoluteWorkingPath">Absolute path to working directory.</param>
        /// <param name="slashMode">Slash mode.</param>
        private static void Example4_DirectoryChangeBack(DirfileContext context, string absoluteWorkingPath, Mode slashMode = Mode.Backward)
        {
            var slash = GetSlash(slashMode);

            // Creates 'testDir1', inside 'testDir1' creates 'testDir2'
            //  Then using ':>' operation we are going to change the current
            //  working directory to parent and create 'testDir3'
            // In the end, 'testDir1' and 'testDir3' are in the same directory,
            //  'testDir2' is in the 'testDir1' directory.
            context.Create($"{absoluteWorkingPath}{slash}testDir1 {slash} testDir2 :> testDir3");

            context.DeleteDirectorRecursive("testDir1");
            context.DeleteDirector("testDir3");
        }

        /// <summary>
        /// Example how to create File with initialized text.
        /// </summary>
        /// <param name="context">Dirfile context.</param>
        /// <param name="absoluteWorkingPath">Absolute path to working directory.</param>
        /// <param name="slashMode">Slash mode.</param>
        private static void Example5_FileInitialText(DirfileContext context, string absoluteWorkingPath, Mode slashMode = Mode.Backward)
        {
            var slash = GetSlash(slashMode);

            // Creates 'testFile1.txt' and writes 'Initial text inside file' into the file.
            //  NOTE: :" - Denotes Start of text
            //        "  - Denotes End of text
            context.Create($"{absoluteWorkingPath}{slash}testFile1.txt :\"Initial text inside file\"");

            context.DeleteFiler("testFile1.txt");
        }

        /// <summary>
        /// Example of a little bit more complex Creation.
        /// </summary>
        /// <param name="context">Dirfile context.</param>
        /// <param name="absoluteWorkingPath">Absolute path to working directory.</param>
        /// <param name="slashMode">Slash mode.</param>
        private static void Example6_ComplexCreation(DirfileContext context, string absoluteWorkingPath, Mode slashMode = Mode.Backward)
        {
            var slash = GetSlash(slashMode);

            // Creates 'testDir1' and 'testFile2.txt' in the current working directory
            //  Creates 'testDir2' and 'testFile1.txt' in the 'testDir1' directory
            //  Both text files are initialized with text
            context.Create($"{absoluteWorkingPath} {slash}testDir1 {slash}testDir2 > testFile1.txt :\"Initial file text 1\" :> testFile2.txt :\"Initial file text 2\"");

            context.DeleteDirectorRecursive("testDir1");
        }

        #endregion

        /// <summary>
        /// Gets slash character based on slash mode.
        /// </summary>
        /// <param name="slashMode">Slash mode.</param>
        /// <returns>Slash character.</returns>
        private static char GetSlash(Mode slashMode)
        {
            return slashMode == Mode.Forward ? '/' : '\\';
        }
    }
}
