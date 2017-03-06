using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Management;

namespace TestIFNSTools
{
    public class OpenFile
    {
        Form1 _owner;
        public OpenFile(Form1 owner)  //Для того чтобы элементы класса Form1 отражались в Arhiv
        {
            _owner = owner;
        }

        public void Openxls(string FileName)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(FileName)
            {
                  UseShellExecute = true,
                  
            };
            Process.Start(startInfo);
        }
    }
}
