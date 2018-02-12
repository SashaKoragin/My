using System.IO;

namespace CovertTXTtoExcel.ConvertTXTtoEXCEL.GenereteName
{
    class GenerateName
    {

        internal static string Generatenamewhile()
        {
            var i = 0;
            var name = "Отчет-" + i + ".xlsx";
            while (File.Exists(PathOtchet.Configuration.PathOtchet + name))
            {
                i++;
                name = "Отчет-" + i + ".xlsx";
            }
            return name;
        }

        internal static string Generatenamedowhile()
        {
            var i=0;
            const string tmpl = "Отчет-{0}.xlsx";
            string name;
            do{name = string.Format(tmpl, i);
               i++;
            } while (File.Exists(PathOtchet.Configuration.PathOtchet+name));
            return name;
        }
    }
}   