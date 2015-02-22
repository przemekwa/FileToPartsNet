
// Copyright (c) 2015 Przemek Walkowski

namespace FileToParts
{
    using System.Collections.Generic;
    using System.IO;
    
    /// <summary>
    /// Class for bucket for file
    /// </summary>
    public class FileBucket
    {
        /// <summary>
        /// Gets or sets list of files in bucket
        /// </summary>
        public List<FileInfo> FileList { get; set; }

        /// <summary>
        /// Gets or sets major version of bucket
        /// </summary>
        public int MajorVersion { get; set; }
    }
}
