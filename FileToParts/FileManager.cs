using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FileToParts
{
    public class FileManager : IFileManager
    {
        readonly string pathToDirectory;

        IEnumerable<FileBucket> buckets;

        public FileManager(string pathToDirectory)
        {
            this.pathToDirectory = pathToDirectory;
        }

        public int GetHighstMinorNumber(FileInfo fi)
        {
            if (buckets == null)
            {
                this.GetBuckets();
            }

            return this.GetMinorNumer(buckets.Single(b => b.MajorVersion == this.GetMajorNumer(fi)).FileList.Last());
        }

        public  int GetMinorNumer(FileInfo fi)
        {
            var bucket = fi.Name.Substring(fi.Name.LastIndexOf('_') + 1, 1);

            int result = 0;

            int.TryParse(bucket, out result);

            return result;
        }

        public int GetMajorNumer(FileInfo fi)
        {
            var bucket = fi.Name.Substring(fi.Name.IndexOf('_') + 1, 1);

            int result = 0;

            int.TryParse(bucket, out result);

            return result;
        }

        public  long GetSize(string s)
        {
            return Encoding.UTF8.GetByteCount(s);
        }

        public  string GetNextFileName(FileInfo fileInfo, int number)
        {
            return Path.Combine(pathToDirectory, string.Format("{0}{1}{2}", fileInfo.Name.Substring(0, fileInfo.Name.Length - 5), number, fileInfo.Extension));
        }

        
        public IEnumerable<FileBucket> GetBuckets()
        {
            var result = new List<FileBucket>();

            var files = Directory.GetFiles(pathToDirectory);

            foreach (var filePath in files)
            {
                var fi = new FileInfo(filePath);

                var bucketNumber = this.GetMajorNumer(fi);

                if (result.Any(b => b.MajorVersion == bucketNumber))
                {
                    var test = result.First(b => b.MajorVersion == bucketNumber);
                    test.FileList.Add(fi);
                }
                else
                {
                    result.Add(new FileBucket
                    {
                        FileList = new List<FileInfo>
                        {
                            fi
                        },
                        MajorVersion = bucketNumber
                    });
                }
            }

            return result;
        }
    }
}
