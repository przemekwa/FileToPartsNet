
// Copyright (c) 2015 Przemek Walkowski

namespace FileToParts
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Class for file split
    /// </summary>
    public class FileSpliter
    {
        /// <summary>
        /// All split options
        /// </summary>
        private readonly FileSpliterOptions splitOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSpliter" /> class
        /// </summary>
        /// <param name="fileSpliterOptions">All split options</param>
        public FileSpliter(FileSpliterOptions fileSpliterOptions)
        {
            this.splitOptions = fileSpliterOptions;
        }

        /// <summary>
        /// Methods to split files 
        /// </summary>
        /// <param name="filesToSplit">List file info to split</param>
        public void SplitFiles(IEnumerable<FileInfo> filesToSplit)
        {
            foreach (var file in filesToSplit)
            {
                Console.Write("Przetwarzanie pliku {0}...............", file.Name);

                var fileVer = 0;

                string tempFileName;

                var putHeader = false;

                var header = string.Empty;

                using (var sr = new StreamReader(file.FullName))
                {
                    string line;

                    long lineNumber = 0;

                    var buffer = this.splitOptions.MaxFileSize;
                    string fileName = Path.Combine(this.splitOptions.DirectoryName, Path.GetRandomFileName());
                    tempFileName = fileName;
                   
                    while ((line = sr.ReadLine()) != null)
                    {
                        var size = this.splitOptions.FileManager.GetSize(line);
                        buffer -= size;

                        if (lineNumber == 0)
                        {
                            header = line;
                        }

                        if (size > this.splitOptions.MaxFileSize)
                        {
                            Console.Write("Linia ma za duży rozmiar.");
                        }

                        if (buffer < 0)
                        {
                            buffer = this.splitOptions.MaxFileSize - size - this.splitOptions.FileManager.GetSize(header);

                            while (File.Exists(fileName))
                            {
                                fileName = this.splitOptions.FileManager.GetNextFileName(file, ++fileVer);
                            }

                            putHeader = true;
                        }

                        using (var sw = new StreamWriter(fileName, true, new UTF8Encoding(false)))
                        {
                            if (putHeader)
                            {
                                sw.WriteLine(header);
                                putHeader = false;
                            }

                            sw.WriteLine(line);
                        }

                        lineNumber++;
                    }
                }

                File.Delete(file.FullName);

                File.Copy(tempFileName, file.FullName);

                File.Delete(tempFileName);

                Console.WriteLine("ok.");
            }
        }
    }
}
