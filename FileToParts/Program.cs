
// Copyright (c) 2015 Przemek Walkowski

namespace FileToParts
{
    using System;
    using System.Linq;

    class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Witaj Bogusiu!");
            
            if (args.Length != 3)
            {
                Console.WriteLine("Brak parametrów!");
                Console.WriteLine("Poprawne wywołanie to: FileToParts.exe [Katalog docelowy] [Rozmiar] [Wyrazenie Regularne]");
                Console.ReadKey();
               return;
            }

            var MaxFileSize = long.Parse(args[1]);
            var MainDirectory = args[0];
            var SearchPatern = args[2];


            var fileManager = new FileManager(MainDirectory, SearchPatern);

            var buckets = fileManager.GetBuckets().ToList();

            Console.WriteLine("Pobrano {0} plików.", buckets.Sum(b=>b.FileList.ToList().Count));

            var fileWithWrongSize = (from bucket in buckets
                from file in bucket.FileList
                where file.Length > MaxFileSize
                select file).ToList();
            
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
