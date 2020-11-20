using System;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Wordprocessing;

namespace LibaryDocumentGenerator.ProgrammView.Word.Libary.TablesGenrerator
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
        /// <param name="lineValues">Линия какая должна быть нарисована</param>
        /// <param name="size">Размер нарисованной линии</param>
        /// <param name="space">Расстояние от линии до текста</param>
        /// <param name="color">Цвет линии</param>
        /// <returns></returns>
        public static TableCellBorders GenerateBorderFull(BorderValues lineValues = BorderValues.Single, int size = 4, int space = 0, string color = "000000")
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bordersSelect">Выбор границ для ячейки</param>
        /// <param name="lineValues">Линия какая должна быть нарисована</param>
        /// <param name="size">Размер нарисованной линии</param>
        /// <param name="space">Расстояние от линии до текста</param>
        /// <param name="color">Цвет линии</param>
        /// <returns></returns>
        public static TableCellBorders SelectGenerateBorder(SelectTableCellBorders bordersSelect, BorderValues lineValues = BorderValues.Single, int size = 4, int space = 0, string color = "000000")
        {
            var borderCell = new TableCellBorders();
            var allBorder = new List<BorderType>
            {
                new TopBorder()
                {
                    Val = BorderValues.Single,
                    Color = "000000",
                    Size = Convert.ToUInt32(4),
                    Space = Convert.ToUInt32(0)
                },
                new LeftBorder()
                {
                    Val = BorderValues.Single,
                    Color = "000000",
                    Size = Convert.ToUInt32(4),
                    Space = Convert.ToUInt32(0)
                },
                new RightBorder()
                {
                    Val = BorderValues.Single,
                    Color = "000000",
                    Size = Convert.ToUInt32(4),
                    Space = Convert.ToUInt32(0)
                },
                new BottomBorder()
                {
                    Val = BorderValues.Single,
                    Color = "000000",
                    Size = Convert.ToUInt32(4),
                    Space = Convert.ToUInt32(0)
                }
            };
            allBorder.ForEach(x =>
            {
                var name = x.GetType();
                if (bordersSelect.ToString().Contains(name.Name))
                {
                    borderCell.Append(x);
                }
            });
            return borderCell;
        }
        /// <summary>
        /// Границы
        /// </summary>
        public enum  SelectTableCellBorders
        {
            TopBorder = 0,
            BottomBorder = 1,
            RightBorder = 2,
            LeftBorder = 3,
            LeftBorderOrRightBorder = 4,
            LeftBorderOrTopBorder = 5,
            LeftBorderOrBottomBorder = 6,
            RightBorderOrBottomBorder = 7,
            RightBorderOrTopBorder = 8,
            LeftBorderOrRightBorderOrTopBorder = 9,
            LeftBorderOrRightBorderOrBottomBorder = 10,
            LeftBorderOrBottomBorderOrTopBorder = 11,
            RightBorderOrBottomBorderOrTopBorder = 12,
        }
    }
}
