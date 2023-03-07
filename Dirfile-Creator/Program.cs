using System;
using Dirfile_lib.Utilities.Validation;

namespace Creator
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Hellou Dirfile-Creator");

            Console.WriteLine(ArgumentParser.Instance.IsValid(@"\testDir"));

            Console.ReadLine();
        }
    }
}