using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;

namespace LibaryDocumentGenerator.ProgrammView.Excel.FontModel
{
   public class FontModel
    {

        /// <summary>
        /// Добавление шрифта Times New Roman
        /// </summary>
        /// <returns></returns>
        /// <param name="fontSize">Размер шрифта</param>
        /// <param name="isBold">Жирный шрифт</param>
        public Font GenerateFont(double fontSize = 11D,bool isBold = false)
        {
            Font font = new Font();

            if (isBold)
            {
                Bold bold = new Bold();
                font.Append(bold);
            }
            FontSize fontSize1 = new FontSize() { Val = fontSize };
            Color colorfont = new Color() { Theme = (UInt32Value)1U };
            FontName fontName1 = new FontName() { Val = "Times New Roman" };
            FontFamilyNumbering fontFamilyNumbering1 = new FontFamilyNumbering() { Val = 1 };
            FontCharSet fontCharSet1 = new FontCharSet() { Val = 204 };
            font.Append(fontSize1);
            font.Append(colorfont);
            font.Append(fontName1);
            font.Append(fontFamilyNumbering1);
            font.Append(fontCharSet1);
            return font;
        }

    }
}
