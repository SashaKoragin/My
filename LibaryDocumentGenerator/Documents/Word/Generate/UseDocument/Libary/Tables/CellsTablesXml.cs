using System.Collections.ObjectModel;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;
using Border = DocumentFormat.OpenXml.Wordprocessing.Border;
using BottomBorder = DocumentFormat.OpenXml.Wordprocessing.BottomBorder;

namespace LibaryDocumentGenerator.Documents.Word.Generate.UseDocument.Libary.Tables
{
   public class CellsTablesXml
    {
        /// <summary>
        /// Возвращает ячейку с настройками может применятся для создания строки
        /// </summary>
        /// <param name="paragraph">Коллекция параграффов для добавления в ячейку</param>
        /// <param name="number">Количество объединенных ячеек</param>
        /// <param name="width">Длина ячейки</param>
        /// <param name="type">Тип выравнивания</param>
        /// <param name="leftmargin">Левый отступ в пикселях</param>
        /// <param name="rightmargin">Правый отступ в пикселях</param>
        /// <param name="borders">Отражение границы ячейки</param>
        /// <returns></returns>
        public static TableCell GenerateCell(ref ObservableCollection<Paragraph> paragraph, string width, TableWidthUnitValues type, string leftmargin ="0", string rightmargin = "0", TableVerticalAlignmentValues verticalAlignment=TableVerticalAlignmentValues.Bottom, TableCellBorders borders = null, int number = 0)
        {
            TableCell tableCell = new TableCell();
            TableCellProperties tableCellProperties = new TableCellProperties();
            TableCellWidth tableCellWidth = new TableCellWidth() { Width = width, Type = type };
            GridSpan gridSpan = new GridSpan() { Val = number };
            TableCellVerticalAlignment tcVA = new TableCellVerticalAlignment() { Val = verticalAlignment };

            TableCellMargin tableCellMargin = new TableCellMargin();

            if (leftmargin != "0")
            {
                LeftMargin leftMargin = new LeftMargin() { Width = leftmargin, Type = TableWidthUnitValues.Dxa };
                tableCellMargin.Append(leftMargin);
            }

            if (rightmargin != "0")
            {
                RightMargin rightMargin = new RightMargin() { Width = rightmargin, Type = TableWidthUnitValues.Dxa };
                tableCellMargin.Append(rightMargin);
            }
            tableCellProperties.Append(tcVA);
            tableCellProperties.Append(tableCellWidth);
            tableCellProperties.Append(tableCellMargin);
            tableCellProperties.Append(gridSpan);
            if (borders != null)
            {
                tableCellProperties.Append(borders);
            }

            tableCell.Append(tableCellProperties);
            AddParagraphCell(ref tableCell,ref paragraph);

            return tableCell;
        }

        /// <summary>
        /// Возвращает ячейку с настройками может применятся для создания строки
        /// </summary>
        /// <param name="paragraph">1 Параграф для добавления в ячейку</param>
        /// <param name="width">Длина ячейки</param>
        /// <param name="type">Тип выравнивания</param>
        /// <param name="leftmargin">Левый отступ в пикселях</param>
        /// <param name="rightmargin">Правый отступ в пикселях</param>
        /// <param name="borders">Стиль отражение границы ячейки</param>
        /// <param name="number">Количество объединенных ячеек</param>
        /// <returns></returns>
        public static TableCell GenerateCell(Paragraph paragraph, string width, TableWidthUnitValues type, string leftmargin = "0", string rightmargin = "0", TableVerticalAlignmentValues verticalAlignment = TableVerticalAlignmentValues.Bottom, TableCellBorders borders = null, int number = 0)
        {
            TableCell tableCell = new TableCell();
            TableCellProperties tableCellProperties = new TableCellProperties();
            TableCellWidth tableCellWidth = new TableCellWidth() { Width = width, Type = type };
            GridSpan gridSpan = new GridSpan() { Val = number };
            TableCellVerticalAlignment tcVA = new TableCellVerticalAlignment() { Val = verticalAlignment };

            TableCellMargin tableCellMargin = new TableCellMargin();

            if (leftmargin != "0")
            {
                LeftMargin leftMargin = new LeftMargin() { Width = leftmargin, Type = TableWidthUnitValues.Dxa };
                tableCellMargin.Append(leftMargin);
            }

            if (rightmargin != "0")
            {
                RightMargin rightMargin = new RightMargin() { Width = rightmargin, Type = TableWidthUnitValues.Dxa };
                tableCellMargin.Append(rightMargin);
            }
            tableCellProperties.Append(tcVA);
            tableCellProperties.Append(tableCellWidth);
            tableCellProperties.Append(tableCellMargin);
            tableCellProperties.Append(gridSpan);
            if (borders != null)
            {
                tableCellProperties.Append(borders);
            }

            tableCell.Append(tableCellProperties);
            tableCell.Append(paragraph);

            return tableCell;
        }

        /// <summary>
        /// Добавление параграффов в ячейку
        /// </summary>
        /// <param name="cell">Ячейка</param>
        /// <param name="paragraph">Параграф</param>
        public static void AddParagraphCell(ref TableCell cell,ref ObservableCollection<Paragraph> paragraph)
        {
            foreach (var para in paragraph)
            {
                cell.Append(para);
            }
            paragraph.Clear();
        }
    }
}
