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
            // Simple example using Relative path.
            RelativeCreation.RunExampleOne();

            // More advanced example using Relative path.
            RelativeCreation.RunExampleTwo();

            // Simple example using Absolute path.
            AbsoluteCreation.RunExampleOne();

            // More advanced example using Absolute path.
            AbsoluteCreation.RunExampleTwo();
        }
    }
}