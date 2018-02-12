using System.Diagnostics;

namespace CovertTXTtoExcel.ConvertTXTtoEXCEL.Opens
{
    public static class OpenFile
    {
        public static void Openxls(string fileName)
        {
            var startInfo = new ProcessStartInfo(fileName)
            {
                UseShellExecute = true,

            };
            Process.Start(startInfo);
        }
    }
}
