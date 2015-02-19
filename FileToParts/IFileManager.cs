using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FileToParts
{
    public interface IFileManager
    {
         int GetMinorNumer(FileInfo fi);
         int GetMajorNumer(FileInfo fi);
         int GetHighstMinorNumber(FileInfo fi);
         long GetSize(string s);
         string GetNextFileName(FileInfo fileInfo, int number);
         IEnumerable<FileBucket> GetBuckets();
    }
}
