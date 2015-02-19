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
        private static long MaxFileSize;

        private static string MainDirectory;

        private static void Main(string[] args)
        {
            Console.WriteLine("Witaj Bogusiu!");
            
            if (args.Length < 0)
            {
                Console.WriteLine("Brak parametrów!");
            }

            MaxFileSize = long.Parse(args[1]);
            
            MainDirectory = args[0];


            var fileManager = new FileManager(MainDirectory);

            var buckets = fileManager.GetBuckets();

            Console.WriteLine("Pobrano {0} plików.", buckets.Sum(b=>b.ListOfFileInfo.ToList().Count));

            var fileWithWrongSize = GetFileWithWrongSize(buckets);
            
            Console.WriteLine("Znaleziono {0} plików, które maja rozmiar większy niż {1}", fileWithWrongSize.Count(), MaxFileSize);


            var fileSpliter = new FileSpliter(MainDirectory);

            fileSpliter.SplitFiles(fileWithWrongSize, new FileSpliterOptions
            {
                FileManager = fileManager,
                MaxFileSize = MaxFileSize,
                DirectoryName = MainDirectory,
            });


            
            

           
            
            Console.WriteLine("Do zobaczenia Bogusiu!");
            Console.ReadKey();
        }

        

        static IEnumerable<FileInfo> GetFileWithWrongSize(IEnumerable<FileBucket> fileBucket)
        {
            return (from bucket in fileBucket
                 from filePath in bucket.ListOfFileInfo
                 where filePath.Length > MaxFileSize
                 select filePath);
        }
        

    }
}
