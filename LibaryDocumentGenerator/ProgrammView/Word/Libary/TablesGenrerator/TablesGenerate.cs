using DocumentFormat.OpenXml.Wordprocessing;

namespace LibaryDocumentGenerator.ProgrammView.Word.Libary.TablesGenrerator
{
   public class TablesGenerate
    {
        /// <summary>
        /// Создает таблицу с линиями
        /// </summary>
        /// <returns></returns>
        public Table CreateTableLine()
        {
            Table table = new Table();
            TableProperties tableProperties = new TableProperties();
            TableStyle tableStyle = new TableStyle() { Val = "TableGrid" };
            TableWidth tableWidth = new TableWidth() { Width = "5000", Type = TableWidthUnitValues.Auto };
            tableProperties.Append(tableStyle);
            tableProperties.Append(tableWidth);
            table.Append(tableProperties);
            return table;
        }
    }
}
