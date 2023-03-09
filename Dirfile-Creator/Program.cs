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

            //Console.WriteLine(ArgumentParser.Instance.IsValid(@"\testDir"));

            Console.ReadLine();

            Console.WriteLine("Hellou Dirfile-Creator");

            var input = Directory.GetCurrentDirectory() + @"\testDir > testFile.txt > testDir2\test  Dir3\testFile2.csv :> testFile3.avi :> testLastFile.cpp";
            input = input.Replace("\\", "/");

            var extractor = new Extractor(SlashMode.Forward);
            extractor.Extract(input);
            var argExtractor = new ArgumentExtractor(extractor.Arguments, SlashMode.Forward);

            Console.WriteLine($"Input was: '{input}'");
            Console.WriteLine($"Extracted path: '{extractor.DirectorPath}'");
            Console.WriteLine($"Extracted argument string: '{extractor.Arguments}'");

            Console.WriteLine($"Individual Directors:");
            foreach (var item in argExtractor.DirectorArguments)
            {
                Console.WriteLine($"'{item}'");
            }

            Console.WriteLine($"Individual Directors:");
            foreach (var item in argExtractor.FilerArguments)
            {
                Console.WriteLine($"'{item}'");
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            foreach (var item in argExtractor.ArgumentsInOrder)
            {
                Console.WriteLine(item.Key + "       " + item.Value);
            }

            Console.WriteLine();
            foreach (var item in argExtractor.OperationsInOrder)
            {
                Console.WriteLine(item);
            }

            Console.ReadLine();
        }
    }
}