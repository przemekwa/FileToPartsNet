// -----------------------------------------------------------------------
// <copyright file="Extension.cs" company="Bank Zachodni WBK S.A.">
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
    public static  class Extension
    {
               public static int GetMinorNumer(this FileInfo fi)
        {
            var bucket = fi.Name.Substring(fi.Name.LastIndexOf('_') + 1, 1);

            int result = 0;

            int.TryParse(bucket, out result);

            return result;
        }

        

    }
}
