// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 15/03/2024     \\

using Dirfile_Creator.Examples;

namespace Creator
{
    /// <summary>
    /// Entry point class of Dirfile-Creator Console application.
    /// </summary>
    public class EntryPoint
    {
        public static void Main()
        {
            // See Run method for further information about the examples.
            RelativeCreation.Run_BackwardMode();

            //RelativeCreation.Run_ForwardMode();

            // Simple example using Absolute path.
            //AbsoluteCreation.Run_BackwardMode();

            // More advanced example using Absolute path.
            //AbsoluteCreation.Run_ForwardMode();
        }
    }
}