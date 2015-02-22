
// Copyright (c) 2015 Przemek Walkowski

namespace FileToParts
{
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Interface do file manager
    /// </summary>
    public interface IFileManager
    {
        /// <summary>
        /// Get ten minor number
        /// </summary>
        /// <param name="fi">File info</param>
        /// <returns>Minor number </returns>
        int GetMinorNumer(FileInfo fi);

        /// <summary>
        /// Get ten major number
        /// </summary>
        /// <param name="fi">File info</param>
        /// <returns>Major number</returns>
        int GetMajorNumer(FileInfo fi);

        /// <summary>
        /// Get highest minor number from buckets.
        /// </summary>
        /// <param name="fi">File info</param>
        /// <returns>Highest minor number for new file</returns>
        int GetHighestMinorNumber(FileInfo fi);

        /// <summary>
        /// Get size of string.
        /// </summary>
        /// <param name="s">String to check</param>
        /// <returns>Size of string byte</returns>
        long GetSize(string s);

        /// <summary>
        /// Get the next file name.
        /// </summary>
        /// <param name="fileInfo">File info</param>
        /// <param name="number">Number that file name should contain</param>
        /// <returns>File name</returns>
        string GetNextFileName(FileInfo fileInfo, int number);

        /// <summary>
        /// Get bucket of files
        /// </summary>
        /// <returns>List od file bucket</returns>
        IEnumerable<FileBucket> GetBuckets();
    }
}
