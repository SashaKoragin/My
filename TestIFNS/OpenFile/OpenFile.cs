using System.Diagnostics;
namespace TestIFNSTools.OpenFile
{
    public class OpenFile
    {
        public void Openxls(string fileName)
        {
            var startInfo = new ProcessStartInfo(fileName)
            {
                  UseShellExecute = true,
                  
            };
            Process.Start(startInfo);
        }
    }
}
