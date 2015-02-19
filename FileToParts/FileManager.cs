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


        public IEnumerable<FileBucket> GetBuckets()
        {
            var result = new List<FileBucket>();

            var files = Directory.GetFiles(pathToDirectory);

            foreach (var filePath in files)
            {
                var fi = new FileInfo(filePath);

                var bucketNumber = fi.GetBucketNumer();

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
