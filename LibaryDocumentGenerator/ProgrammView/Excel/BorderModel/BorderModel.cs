using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;

namespace LibaryDocumentGenerator.ProgrammView.Excel.BorderModel
{
    public class BorderModel
    {


        /// <summary>
        /// Полная граница одинарная линия цвет черный
        /// </summary>
        /// <returns></returns>
        public Border GenerateStandardFullBorder()
        {
            Border border = new Border();
            LeftBorder leftBorder = new LeftBorder() { Style = BorderStyleValues.Thin };
            Color color = new Color() { Indexed = (UInt32Value)64U };

            leftBorder.Append(color);

            RightBorder rightBorder = new RightBorder() { Style = BorderStyleValues.Thin };
            Color color1 = new Color() { Indexed = (UInt32Value)64U };

            rightBorder.Append(color1);

            TopBorder topBorder = new TopBorder() { Style = BorderStyleValues.Thin };
            Color color2 = new Color() { Indexed = (UInt32Value)64U };

            topBorder.Append(color2);

            BottomBorder bottomBorder = new BottomBorder() { Style = BorderStyleValues.Thin };
            Color color3 = new Color() { Indexed = (UInt32Value)64U };

            bottomBorder.Append(color3);
            DiagonalBorder diagonalBorder = new DiagonalBorder();

            border.Append(leftBorder);
            border.Append(rightBorder);
            border.Append(topBorder);
            border.Append(bottomBorder);
            border.Append(diagonalBorder);
            return border;
        }
        /// <summary>
        /// Нижнее подчеркивание
        /// </summary>
        /// <returns></returns>
        public Border GenerateBottomBorder()
        {
            Border border = new Border();
            BottomBorder bottomBorder = new BottomBorder() { Style = BorderStyleValues.Thin };
            Color color3 = new Color() { Indexed = (UInt32Value)64U };
            bottomBorder.Append(color3);
            border.Append(bottomBorder);
            return border;
        }


        /// <summary>
        /// Генерация разных полных границ
        /// </summary>
        /// <returns></returns>
        /// <param name="left">Левая граница</param>
        /// <param name="right">Правая граница</param>
        /// <param name="top">Верхняя граница</param>
        /// <param name="bottom">Нижняя граница</param>
        public Border GenerateStandardFullBorderSetting(BorderStyleValues left, BorderStyleValues right, BorderStyleValues top, BorderStyleValues bottom)
        {
            Border border = new Border();
            LeftBorder leftBorder = new LeftBorder() { Style = left };
            Color color = new Color() { Indexed = (UInt32Value)64U };

            leftBorder.Append(color);

            RightBorder rightBorder = new RightBorder() { Style = right };
            Color color1 = new Color() { Indexed = (UInt32Value)64U };

            rightBorder.Append(color1);

            TopBorder topBorder = new TopBorder() { Style = top };
            Color color2 = new Color() { Indexed = (UInt32Value)64U };

            topBorder.Append(color2);

            BottomBorder bottomBorder = new BottomBorder() { Style = bottom };
            Color color3 = new Color() { Indexed = (UInt32Value)64U };

            bottomBorder.Append(color3);
            border.Append(leftBorder);
            border.Append(rightBorder);
            border.Append(topBorder);
            border.Append(bottomBorder);
            return border;
        }

    }
}
