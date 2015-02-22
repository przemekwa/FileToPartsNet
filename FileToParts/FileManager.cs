
// Copyright (c) 2015 Przemek Walkowski

namespace FileToParts
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Class for file manager
    /// </summary>
    public class FileManager : IFileManager
    {
        /// <summary>
        /// Path to directory
        /// </summary>
        private readonly string pathToDirectory;

        /// <summary>
        /// Search pattern. ? and * are valid
        /// </summary>
        private readonly string searchPattern;

        /// <summary>
        /// List of buckets
        /// </summary>
        private IEnumerable<FileBucket> buckets;

        /// <summary>
        ///  Initializes a new instance of the <see cref="FileManager" /> class
        /// </summary>
        /// <param name="pathToDirectory">Path to directory</param>
        /// <param name="searchPattern">Search pattern. ? and * are valid</param>
        public FileManager(string pathToDirectory, string searchPattern)
        {
            this.pathToDirectory = pathToDirectory;
            this.searchPattern = searchPattern;
        }

        /// <summary>
        /// Get highest minor number from buckets.
        /// </summary>
        /// <param name="fi">File info</param>
        /// <returns>Highest minor number for new file</returns>
        public int GetHighestMinorNumber(FileInfo fi)
        {
            if (this.buckets == null)
            {
               this.buckets = this.GetBuckets();
            }

            return this.GetMinorNumer(this.buckets.Single(b => b.MajorVersion == this.GetMajorNumer(fi)).FileList.Last());
        }

        /// <summary>
        /// Get ten minor number
        /// </summary>
        /// <param name="fi">File info</param>
        /// <returns>Minor number </returns>
        public int GetMinorNumer(FileInfo fi)
        {
            var bucket = fi.Name.Substring(fi.Name.LastIndexOf('_') + 1, 1);

            int result;

            int.TryParse(bucket, out result);

            return result;
        }

        /// <summary>
        /// Get ten major number
        /// </summary>
        /// <param name="fi">File info</param>
        /// <returns>Major number</returns>
        public int GetMajorNumer(FileInfo fi)
        {
            var bucket = fi.Name.Substring(fi.Name.IndexOf('_') + 1, 1);

            int result;

            int.TryParse(bucket, out result);

            return result;
        }

        /// <summary>
        /// Get size of string.
        /// </summary>
        /// <param name="s">String to check</param>
        /// <returns>Size of string byte</returns>
        public long GetSize(string s)
        {
            return Encoding.UTF8.GetByteCount(s);
        }

        /// <summary>
        /// Get the next file name.
        /// </summary>
        /// <param name="fileInfo">File info</param>
        /// <param name="number">Number that file name should contain</param>
        /// <returns>File name</returns>
        public string GetNextFileName(FileInfo fileInfo, int number)
        {
            return Path.Combine(this.pathToDirectory, string.Format("{0}{1}{2}", fileInfo.Name.Substring(0, fileInfo.Name.Length - 5), number, fileInfo.Extension));
        }

        /// <summary>
        /// Get bucket of files
        /// </summary>
        /// <returns>List od file bucket</returns>
        public IEnumerable<FileBucket> GetBuckets()
        {
            var result = new List<FileBucket>();

            var files = Directory.GetFiles(this.pathToDirectory, this.searchPattern);

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
