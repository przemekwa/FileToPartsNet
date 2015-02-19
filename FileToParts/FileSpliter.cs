using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FileToParts
{
    public class FileSpliter
    {
        private FileSpliterOptions splitOptions;

        public FileSpliter(FileSpliterOptions fileSpliterOptions)
        {
            this.splitOptions = fileSpliterOptions;
        }

        public void SplitFiles(IEnumerable<FileInfo> filesToSplit)
        {
            foreach (var file in filesToSplit)
            {
                Console.Write("Przetwarzanie pliku {0}...............", file.Name);

                string FileName = file.FullName;

                int fileVer = 0;

                string tempFileName;

                using (var sr = new StreamReader(FileName))
                {
                    string line;

                    var buffer = splitOptions.MaxFileSize;
                    string fileName = Path.Combine(splitOptions.DirectoryName, Path.GetRandomFileName());
                    tempFileName = fileName;
                   

                    while ((line = sr.ReadLine()) != null)
                    {
                        var size = splitOptions.FileManager.GetSize(line);
                        buffer -= size;

                        if (size > splitOptions.MaxFileSize)
                        {
                            Console.Write("Linia ma za duży rozmiar.");
                        }

                        if (buffer < 0)
                        {
                            buffer = splitOptions.MaxFileSize - size;

                            while (File.Exists(fileName))
                            {
                                fileName = splitOptions.FileManager.GetNextFileName(file, ++fileVer);
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

                Console.WriteLine("ok.", file.Name);
            }

        }
    }
}
