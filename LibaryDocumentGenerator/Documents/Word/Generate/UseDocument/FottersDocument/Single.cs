using System.Collections.ObjectModel;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using DocumentFormat.OpenXml.Wordprocessing;
using LibaryDocumentGenerator.Documents.Word.Generate.UseDocument.Libary.Page;
using LibaryDocumentGenerator.Documents.Word.Generate.UseDocument.Libary.Paragraphs;
using LibaryDocumentGenerator.Documents.Word.Generate.UseDocument.Libary.Tables;

namespace LibaryDocumentGenerator.Documents.Word.Generate.UseDocument.FottersDocument
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
            RowTableXml rows = new RowTableXml();
            ObservableCollection<TableCell> cellcCollection = new ObservableCollection<TableCell>();
            ObservableCollection<Paragraph> paragraphcCollection = new ObservableCollection<Paragraph>();
            GenerateRun paragraphGenerate = new GenerateRun();
            paragraphcCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart(tamplate.Stone.Stone1, "26"));
            paragraphcCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart(tamplate.Stone.Stone2, "26"));
            cellcCollection.Add(CellsTablesXml.GenerateCell(ref paragraphcCollection, "5000", TableWidthUnitValues.Dxa));
            paragraphcCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart(tamplate.Stone.Stone3, "26"));
            cellcCollection.Add(CellsTablesXml.GenerateCell(ref paragraphcCollection, "100", TableWidthUnitValues.Auto,"2400"));
            table.Append(rows.GenerateRow(ref cellcCollection));
            body.Append(table);
            return body;
        }


    }
}
