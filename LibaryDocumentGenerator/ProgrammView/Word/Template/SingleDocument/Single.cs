using System.Collections.ObjectModel;
using DocumentFormat.OpenXml.Wordprocessing;
using LibaryDocumentGenerator.ProgrammView.Word.Libary.ParagraphsGenerator;
using LibaryDocumentGenerator.ProgrammView.Word.Libary.TablesGenrerator;

namespace LibaryDocumentGenerator.ProgrammView.Word.Template.SingleDocument
{
   public class Single
    {
        /// <summary>
        /// Поле кто подписывает документ 
        /// </summary>
        /// <param name="tamplate">Шаблон</param>
        /// <returns></returns>
        public Body AddSingle(LibaryXMLAutoReports.TemplateSheme.Template tamplate)
        {
            Body body = new Body();
            Table table = new Table();
            var rows = new RowGenerate();
            ObservableCollection<TableCell> cellcCollection = new ObservableCollection<TableCell>();
            ObservableCollection<Paragraph> paragraphcCollection = new ObservableCollection<Paragraph>();
            var paragraphGenerate = new RunGenerate();
            paragraphcCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart(tamplate.Stone.Stone1, "26"));
            paragraphcCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart(tamplate.Stone.Stone2, "26"));
            cellcCollection.Add(CellGenerate.GenerateCell(ref paragraphcCollection, "5000", TableWidthUnitValues.Dxa));
            paragraphcCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart(tamplate.Stone.Stone3, "26"));
            cellcCollection.Add(CellGenerate.GenerateCell(ref paragraphcCollection, "100", TableWidthUnitValues.Auto, "2400"));
            table.Append(rows.GenerateRow(ref cellcCollection));
            body.Append(table);
            return body;
        }
    }
}
