using System;
using System.Collections.ObjectModel;
using DocumentFormat.OpenXml.Wordprocessing;

namespace LibaryDocumentGenerator.ProgrammView.Word.Libary.TablesGenrerator
{
   public class RowGenerate
    {
        /// <summary>
        /// Создает сгенерированую строку
        /// </summary>
        /// <param name="tablecells">Массив ячеек для генерации строки</param>
        /// <param name="isHeight">Проставить высоту строки</param>
        /// <param name="valHeight">Высота строки по умолчанию 0,5 см</param>
        /// <returns></returns>
        public TableRow GenerateRow(ref ObservableCollection<TableCell> tablecells, bool isHeight = false, uint valHeight = 175U)
        {
            TableRow tableRow = new TableRow();

            foreach (var cell in tablecells)
            {
                tableRow.Append(cell);
            }

            tablecells.Clear();
            if (isHeight)
            {
                TableRowProperties tableRowProperties = new TableRowProperties();
                TableRowHeight tableRowHeight = new TableRowHeight() { Val = valHeight, HeightType = HeightRuleValues.Exact};
                tableRowProperties.Append(tableRowHeight);
                tableRow.Append(tableRowProperties);
            }
            return tableRow;
        }

        /// <summary>
        /// Формула расчета высоты строки
        /// </summary>
        /// <param name="sm">Сантиметры,милиметры</param>
        /// <returns></returns>
        public uint FormulHeightRow(double sm)
        {
            var height = sm * 567;
            return Convert.ToUInt32(Math.Round(height));
        }
    }
}
