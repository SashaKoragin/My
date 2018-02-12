using System.Drawing;

namespace CovertTXTtoExcel.ConvertTXTtoEXCEL.IconsCreate
{
    class Icondll
    {

        public static Icon Extract(string f)
        {
           
            var icon = Icon.ExtractAssociatedIcon(f);
            return icon;
        }
    }
}
