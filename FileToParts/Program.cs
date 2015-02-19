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

            Console.WriteLine("Pobrano {0} plików.", buckets.Sum(b=>b.ListOfFileInfo.Count));

            
            var wrongSizeFileList =
                (from bucket in buckets
                    from filePath in bucket.ListOfFileInfo
                    where filePath.Length > MaxFileSize
                    select filePath).ToList();


            Console.WriteLine("Znaleziono {0} plików, które maja rozmiar większy niż {1}", wrongSizeFileList.Count(), MaxFileSize);

            var error = false;

            foreach (var file in wrongSizeFileList)
            {
                Console.Write("Przetwarzanie pliku {0}...............", file.Name);

                string FileName = file.FullName;
               
                int lastMinor = buckets.Single(b => b.MajorVersion == file.GetBucketNumer()).ListOfFileInfo.Last().GetMinorNumer();

                string tempFileName;

                using (var sr = new StreamReader(FileName))
                {
                    string line;

                    var buffer = MaxFileSize;
                    string fileName = Path.Combine(MainDirectory, Path.GetRandomFileName());
                    tempFileName = fileName;

                    while ((line = sr.ReadLine()) != null)
                    {
                        var size = GetSize(line);
                        buffer -= size;

                        if (size > MaxFileSize)
                        {
                            Console.WriteLine("Linia ma za duży rozmiar.");
                            error = true;
                        }

                        if (buffer < 0)
                        {
                            buffer = MaxFileSize - size;

                            while (File.Exists(fileName))
                            {
                                fileName = GetNextFileName(file, ++lastMinor);
                            }
                        }

                        using (var sw = new StreamWriter(fileName, true, new UTF8Encoding(false)))
                        {
                            sw.WriteLine(line);
                        }
                    }
                }

                File.Delete(FileName);

                File.Copy(tempFileName, FileName);

                File.Delete(tempFileName);

                Console.WriteLine( error?string.Empty: "ok.!");
                error = false;
            }
            
            Console.WriteLine("Do zobaczenia Bogusiu!");
            Console.ReadKey();
        }

        static long GetSize(string s)
        {
            return Encoding.UTF8.GetByteCount(s);
        }

        static string GetNextFileName(FileInfo fileInfo, int number)
        {
            return Path.Combine(MainDirectory, string.Format("{0}{1}{2}", fileInfo.Name.Substring(0, fileInfo.Name.Length - 5), number, fileInfo.Extension));
        }

    }
}
