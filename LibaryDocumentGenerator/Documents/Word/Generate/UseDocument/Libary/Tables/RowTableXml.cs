using System.Collections.ObjectModel;
using DocumentFormat.OpenXml.Wordprocessing;


namespace LibaryDocumentGenerator.Documents.Word.Generate.UseDocument.Libary.Tables
{
   public class RowTableXml
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
