using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TestIFNSLibary.WebSevice.Logica
{
   public class Backup
    {
        public string Copy(string pathotkuda, string pathkuda)
        {
            string resul = null;
            var dir = new DirectoryInfo(pathotkuda);
            var files = dir.GetFiles();
            if (!Directory.Exists(pathkuda))
            {
                Directory.CreateDirectory(pathkuda);
            }
            else
            {
                foreach (FileInfo file in files)
                {
                    File.Copy(pathkuda,file.FullName,true);
                }
            }

            return resul;
        }

        [HttpPost]
        public static string Ff()
        {
            string gg = "dddd";
            return gg;
        }

    }
}
