using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using FileToParts;

namespace TestFileToParts
{
    [TestClass]
    public class Tests
    {
       [TestMethod]
        public void CreateFile()
        {
            var directoryName = @"c:\Bogusia";

            for (int i = 0; i < 1; i++)
            {
                int j = 0;

                for (int k = 0; k < 3; k++)
                {
                    var fileName = Path.Combine(directoryName, string.Format("aaa_{0}_{1}{2}", i, k, ".txt"));

                    using (var sw = new StreamWriter(fileName))
                    {
                        for (int y = 0; y < 2; y++)
                        {
                            sw.WriteLine(string.Format("{0} {1} {2}", i, j++, "sdgdsgsdflssfdsfgdsfgsdgsdgsdgsdgsdgsdgsdgsdgsdkfhsdlkfhasdkljfbnaskldjhsljkfghsdlkjfhsdkljfhlaskdhjsdlkjvnalkjl3255237856y23985"));
                        }

                    };
                }
            }




        }

       [TestMethod]
       public void GetMinor()
       {
           var fm = new FileManager("");

           var rezult = fm.GetMinorNumer(new FileInfo("aaa_1_2.txt"));

           Assert.AreEqual(rezult, 2);
       }

       [TestMethod]
       public void GetMajor()
       {
           var fm = new FileManager("");

           var rezult = fm.GetMajorNumer(new FileInfo("aaa_1_2.txt"));

           Assert.AreEqual(rezult, 1);
       }
    }
}
