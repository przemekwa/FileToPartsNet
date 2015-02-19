using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace FileToParts
{
    class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Witaj Bogusiu!");
            
            if (args.Length < 0)
            {
                Console.WriteLine("Brak parametrów!");
                Console.ReadKey();
                return;
            }

            var MaxFileSize = long.Parse(args[1]);
            var MainDirectory = args[0];


            var fileManager = new FileManager(MainDirectory);

            var buckets = fileManager.GetBuckets();

            Console.WriteLine("Pobrano {0} plików.", buckets.Sum(b=>b.FileList.ToList().Count));

            var fileWithWrongSize = (from bucket in buckets
                                     from file in bucket.FileList
                                     where file.Length > MaxFileSize
                                     select file);
            
            Console.WriteLine("Znaleziono {0} plików, które maja rozmiar większy niż {1}", fileWithWrongSize.Count(), MaxFileSize);


            var fileSpliter = new FileSpliter(new FileSpliterOptions
            {
                FileManager = fileManager,
                MaxFileSize = MaxFileSize,
                DirectoryName = MainDirectory,
            });

            fileSpliter.SplitFiles(fileWithWrongSize);
            
            Console.WriteLine("Do zobaczenia Bogusiu!");
            Console.ReadKey();
        }
    }
}
