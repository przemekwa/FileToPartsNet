using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FileToParts
{
    public class FileManager
    {
        readonly string pathToDirectory;

        public FileManager(string pathToDirectory)
        {
            this.pathToDirectory = pathToDirectory;
        }

        public static int GetBucketNumer(FileInfo fi)
        {
            var bucket = fi.Name.Substring(fi.Name.IndexOf('_') + 1, 1);

            int result = 0;

            int.TryParse(bucket, out result);

            return result;
        }


        public IEnumerable<FileBucket> GetBuckets()
        {
            var result = new List<FileBucket>();

            var files = Directory.GetFiles(pathToDirectory);

            foreach (var filePath in files)
            {
                var fi = new FileInfo(filePath);

                var bucketNumber = FileManager.GetBucketNumer(fi);

                if (result.Any(b => b.MajorVersion == bucketNumber))
                {
                    result.First(b => b.MajorVersion == bucketNumber).ListOfFileInfo.Add(fi);
                }
                else
                {
                    result.Add(new FileBucket
                    {
                        ListOfFileInfo = new List<FileInfo>
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
