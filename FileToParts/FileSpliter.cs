using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FileToParts
{
    public class FileSpliter
    {
        private string directoryName;

        public FileSpliter(string directoryName)
        {
            this.directoryName = directoryName;
        }

        public void SplitFiles(IEnumerable<FileInfo> filesToSplit, FileSpliterOptions fileSpliterOptions)
        {
            foreach (var file in filesToSplit)
            {
                Console.Write("Przetwarzanie pliku {0}...............", file.Name);

                string FileName = file.FullName;

                int lastMinor = 0;

                string tempFileName;

                using (var sr = new StreamReader(FileName))
                {
                    string line;

                    var buffer = fileSpliterOptions.MaxFileSize;
                    string fileName = Path.Combine(directoryName, Path.GetRandomFileName());
                    tempFileName = fileName;

                    while ((line = sr.ReadLine()) != null)
                    {
                        var size = fileSpliterOptions.FileManager.GetSize(line);
                        buffer -= size;

                        if (size > fileSpliterOptions.MaxFileSize)
                        {
                            throw new Exception("Linia ma za duży rozmiar.");
                        }

                        if (buffer < 0)
                        {
                            buffer = fileSpliterOptions.MaxFileSize - size;

                            while (File.Exists(fileName))
                            {
                                fileName = fileSpliterOptions.FileManager.GetNextFileName(file, ++lastMinor);
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

                Console.Write("zakończono", file.Name);
            }

        }
    }
}
