using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestIFNSLibary.FileDonload
{
   public class DonloadFile
    {
        public Stream DonloadTreb(string paht)
        {
            return File.OpenRead(paht);
        }

    }
}
