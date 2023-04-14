using Dirfile_lib.API.Context;
using Dirfile_lib.Utilities.Validation;

namespace Creator
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Hellou Dirfile-Creator");

            //Console.WriteLine(PathValidator.Instance.IsValid(Directory.GetCurrentDirectory().Replace('\\', '/')));
            //File.Create(Directory.GetCurrentDirectory() + "\\testFile.cpp");
            using (var ctx = new DirfileContext())
            {
                var dir = Directory.GetCurrentDirectory();
                ctx.Create(@"C:\Users\mikik\Downloads\TestDir1 > TestDir2");
                ctx.SwitchPathMode();
                ctx.DirectorChange(dir);
                ctx.Create("\\TestDir1 > TestDir2");
            }

            Console.ReadLine();
        }
    }
}