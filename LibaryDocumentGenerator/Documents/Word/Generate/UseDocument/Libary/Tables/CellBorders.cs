using System;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;


namespace LibaryDocumentGenerator.Documents.Word.Generate.UseDocument.Libary.Tables
{
   public class CellBorders
    {
        /// <summary>
        /// Стиль нижней линии окрас линия размер линии и т. д.
        /// </summary>
        /// <param name="lineValues">Линия какя должна быть нарисована</param>
        /// <param name="size">Размер нарисованной линии</param>
        /// <param name="space">Растояние от линии до текста</param>
        /// <param name="color">Цвет линии</param>
        /// <returns></returns>
        public static TableCellBorders GenerateBorder(BorderValues lineValues = BorderValues.Single, int size = 4, int space = 0, string color = "000000")
        {
            TableCellBorders bordercell = new TableCellBorders();
            bordercell.Append(new BottomBorder() { Val = lineValues, Color = color, Size = Convert.ToUInt32(size), Space = Convert.ToUInt32(space) });
            return bordercell;
        }
        /// <summary>
        /// Полный рамка линий 
        /// </summary>
        /// <param name="lineValues">Линия какя должна быть нарисована</param>
        /// <param name="size">Размер нарисованной линии</param>
        /// <param name="space">Растояние от линии до текста</param>
        /// <param name="color">Цвет линии</param>
        /// <returns></returns>
        public static TableCellBorders GenerateBorderFull(BorderValues lineValues = BorderValues.Single, int size=4, int space=0, string color = "000000")
        {
            TableCellBorders bordercell = new TableCellBorders();
            LeftBorder leftBorder = new LeftBorder() { Val = lineValues, Color = color, Size = Convert.ToUInt32(size), Space = Convert.ToUInt32(space) };
            RightBorder rightBorder = new RightBorder() { Val = lineValues, Color = color, Size = Convert.ToUInt32(size), Space = Convert.ToUInt32(space) };
            TopBorder topBorder = new TopBorder() { Val = lineValues, Color = color, Size = Convert.ToUInt32(size), Space = Convert.ToUInt32(space) };
            BottomBorder bottomBorder = new BottomBorder() { Val = lineValues, Color = color, Size = Convert.ToUInt32(size), Space = Convert.ToUInt32(space) };
            bordercell.Append(leftBorder);
            bordercell.Append(rightBorder);
            bordercell.Append(topBorder);
            bordercell.Append(bottomBorder);
            return bordercell;
        }
    }
}
