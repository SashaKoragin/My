using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Spreadsheet;

namespace LibaryDocumentGenerator.Documents.Word.Generate.UseDocument.Libary.Tables
{
    /// <summary>
    /// Границы ячеек
    /// </summary>
   public class StyleCells
    {
        /// <summary>
        /// Нарисовать границы на ячейки
        /// </summary>
        /// <returns></returns>
        public static Border StileBordersAllLine()
        {
            Border border = new Border();
            LeftBorder leftBorder = new LeftBorder();
            RightBorder rightBorder = new RightBorder();
            TopBorder topBorder = new TopBorder();
            BottomBorder bottomBorder = new BottomBorder();
            border.Append(leftBorder);
            border.Append(rightBorder);
            border.Append(topBorder);
            border.Append(bottomBorder);
            return border;
        }
    }
}
