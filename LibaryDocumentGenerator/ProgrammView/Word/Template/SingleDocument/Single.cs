using System.Collections.ObjectModel;
using DocumentFormat.OpenXml.Wordprocessing;
using LibaryDocumentGenerator.ProgrammView.Word.Libary.ParagraphsGenerator;
using LibaryDocumentGenerator.ProgrammView.Word.Libary.TablesGenrerator;

namespace LibaryDocumentGenerator.ProgrammView.Word.Template.SingleDocument
{
   public class Single
    {
        /// <summary>
        /// Поле кто подписывает документ Руководство  
        /// </summary>
        /// <param name="tamplate">Шаблон</param>
        /// <returns></returns>
        public Body AddSingle(LibaryXMLAutoReports.FullTemplateSheme.Document tamplate)
        {
            Body body = new Body();
            Table table = new Table();
            var rows = new RowGenerate();
            ObservableCollection<TableCell> cellcCollection = new ObservableCollection<TableCell>();
            ObservableCollection<Paragraph> paragraphcCollection = new ObservableCollection<Paragraph>();
            var paragraphGenerate = new RunGenerate();
            paragraphcCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart(tamplate.Templates.Stone.Stone1, "26"));
            paragraphcCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart(tamplate.Templates.Stone.Stone2, "26"));
            cellcCollection.Add(CellGenerate.GenerateCell(ref paragraphcCollection, CellGenerate.FormulWidthCell(8.8), TableWidthUnitValues.Dxa));
            paragraphcCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart(tamplate.Templates.Stone.Stone3, "26",JustificationValues.Right));
            cellcCollection.Add(CellGenerate.GenerateCell(ref paragraphcCollection, CellGenerate.FormulWidthCell(9.2), TableWidthUnitValues.Dxa));
            table.Append(rows.GenerateRow(ref cellcCollection));
            body.Append(table);
            return body;
        }
    }
}
