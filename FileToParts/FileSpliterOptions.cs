using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileToParts
{
    public class FileSpliterOptions
    {
        public string DirectoryName { get; set; }
        public IFileManager FileManager { get; set; }
        public long MaxFileSize { get; set; }
    }
}
