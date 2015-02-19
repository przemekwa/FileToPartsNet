// -----------------------------------------------------------------------
// <copyright file="FileBucket.cs" company="Bank Zachodni WBK S.A.">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.IO;

namespace FileToParts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class FileBucket
    {
        public List<FileInfo> ListOfFileInfo { get; set; }

        public int MajorVersion { get; set; }
    }
}
