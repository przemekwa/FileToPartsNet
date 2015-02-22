
// Copyright (c) 2015 Przemek Walkowski

namespace FileToParts
{
    /// <summary>
    /// Split options
    /// </summary>
    public class FileSpliterOptions
    {
        /// <summary>
        /// Gets or sets directory name with file to split
        /// </summary>
        public string DirectoryName { get; set; }

        /// <summary>
        ///  Gets or sets file manager
        /// </summary>
        public IFileManager FileManager { get; set; }

        /// <summary>
        /// Gets or sets max size file limit
        /// </summary>
        public long MaxFileSize { get; set; }
    }
}
