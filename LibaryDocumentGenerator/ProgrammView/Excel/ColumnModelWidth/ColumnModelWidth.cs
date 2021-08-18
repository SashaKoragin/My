using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;

namespace LibaryDocumentGenerator.ProgrammView.Excel.ColumnModelWidth
{
   public class ColumnModelWidth
   {
       /// <summary>
       /// Формула расчета Ширины ячейки 
       /// </summary>
       /// <param name="pixel">Пиксель</param>
       /// <returns></returns>
       public static DoubleValue FormulaWidth(int pixel)
       {
           return 0.14294118 * pixel;
       }

        /// <summary>
        /// Колонки отчета по табельным номерам 42 колонки размеры
        /// </summary>
        public List<DoubleValue> ListColumnWidthReportCard = new List<DoubleValue>()
        {
            FormulaWidth(22),
            FormulaWidth(94),
            FormulaWidth(55),
            FormulaWidth(18),
            FormulaWidth(110),
            FormulaWidth(9),
            FormulaWidth(22),
            FormulaWidth(22),
            FormulaWidth(22),
            FormulaWidth(22),
            FormulaWidth(22),
            FormulaWidth(22),
            FormulaWidth(22),
            FormulaWidth(22),
            FormulaWidth(22),
            FormulaWidth(22),
            FormulaWidth(22),
            FormulaWidth(22),
            FormulaWidth(22),
            FormulaWidth(22),
            FormulaWidth(22),
            FormulaWidth(32),
            FormulaWidth(22),
            FormulaWidth(22),
            FormulaWidth(22),
            FormulaWidth(22),
            FormulaWidth(22),
            FormulaWidth(22),
            FormulaWidth(22),
            FormulaWidth(22),
            FormulaWidth(22),
            FormulaWidth(22),
            FormulaWidth(22),
            FormulaWidth(22),
            FormulaWidth(22),
            FormulaWidth(22),
            FormulaWidth(22),
            FormulaWidth(22),
            FormulaWidth(33),
            FormulaWidth(34),
            FormulaWidth(21),
            FormulaWidth(21),
        };
   }
}
