using System;
using System.Collections.Generic;
using System.Linq;
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
        /// <returns></returns>
        public TableRow GenerateRow(ref ObservableCollection<TableCell> tablecells)
        {
            TableRow tableRow = new TableRow();
            foreach (var cell in tablecells)
            {
                tableRow.Append(cell);
            }
            tablecells.Clear();
            return tableRow;
        }
    }
}
