using DocumentFormat.OpenXml.Wordprocessing;

namespace LibaryDocumentGenerator.Documents.Word.Generate.UseDocument.Libary.Tables
{
    class Tables
    {
        /// <summary>
        /// Создает таблицу с линиями
        /// </summary>
        /// <returns></returns>
        public Table CreateTableLine()
        {
            Table table = new Table();
            TableProperties tableProperties = new TableProperties();
            TableStyle tableStyle = new TableStyle() { Val ="TableGrid" };
            TableWidth tableWidth = new TableWidth() { Width = "5000", Type = TableWidthUnitValues.Auto };
            tableProperties.Append(tableStyle);
            tableProperties.Append(tableWidth);
            table.Append(tableProperties);
            return table;
        }
    }
}
