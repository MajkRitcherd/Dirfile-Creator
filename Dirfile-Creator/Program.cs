using System;
using Dirfile_lib.API.Extraction;
using Dirfile_lib.Utilities.Validation;

namespace Creator
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Hellou Dirfile-Creator");

            Console.WriteLine(PathValidator.Instance.IsValid(Directory.GetCurrentDirectory().Replace('\\', '/')));

            Console.ReadLine();
        }
    }
}