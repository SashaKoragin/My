using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibaryDocumentGeneratorTestsTemplate.SoapBynary
{
    [TestClass]
    class TestClass
    {
        [TestMethod]
        public void Test()
        {

                    NBFSNet NBFS = new NBFSNet();
                    var filetext = File.ReadAllText("C:\\Users\\7751-00-099\\Desktop\\binary.txt");
                    var t = Convert.ToBase64String(
                        NBFS.EncodeBinaryXML(
                            System.Text.ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(filetext))));

        }
    }
}
