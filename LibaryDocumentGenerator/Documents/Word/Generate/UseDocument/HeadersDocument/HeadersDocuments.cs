using System.Collections.ObjectModel;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using DocumentFormat.OpenXml.Wordprocessing;
using LibaryDocumentGenerator.Documents.Word.Generate.UseDocument.Libary.Page;
using LibaryDocumentGenerator.Documents.Word.Generate.UseDocument.Libary.Paragraphs;
using LibaryDocumentGenerator.Documents.Word.Generate.UseDocument.Libary.Tables;

namespace LibaryDocumentGenerator.Documents.Word.Generate.UseDocument.HeadersDocument
{
    /// <summary>
    /// Заголовки шаблона
    /// </summary>
   public class HeadersDocuments
    {
        /// <summary>
        /// Создание Шапки документа ИФНС 51 Шаблон №1 без герба
        /// </summary>
        /// <param name="tamplate">Шаблон шапки</param>
        /// <param name="n279">Номер инспекции</param>
        /// <param name="n280">Наименование плательщика</param>
        /// <returns></returns>
        public Body DocumentsHeaders(LibaryXMLAutoReports.TemplateSheme.Template tamplate, string n279 = null, string n280 = null)
        {
          Body body = new Body();

          Table table = new Table();
          RowTableXml rows = new RowTableXml();
          ObservableCollection<TableCell> cellcCollection = new ObservableCollection<TableCell>();
          ObservableCollection<Paragraph> paragraphcCollection = new ObservableCollection<Paragraph>();
          GenerateRun paragraphGenerate = new GenerateRun();
            paragraphcCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart(tamplate.Headers.TextHeade1, "20", JustificationValues.Center,1));
            paragraphcCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart(tamplate.Headers.TextHeade2, "20", JustificationValues.Center));
            paragraphcCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart(tamplate.Headers.TextHeade3, "20", JustificationValues.Center));
            paragraphcCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart(tamplate.Headers.TextHeade4, "20", JustificationValues.Center,1));
            paragraphcCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart(tamplate.Headers.TextHeade5, "16", JustificationValues.Center,1));
            paragraphcCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart(tamplate.Headers.TextHeade6, "16", JustificationValues.Center,1));
            paragraphcCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart(tamplate.Headers.TextHeade7, "16", JustificationValues.Center));
            paragraphcCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart(tamplate.Headers.TextHeade8, "16", JustificationValues.Center));
            paragraphcCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart(tamplate.Headers.TextHeade9, "16", JustificationValues.Center));
            paragraphcCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart(tamplate.Headers.TextHeade10, "16", JustificationValues.Center));
            cellcCollection.Add(CellsTablesXml.GenerateCell(ref paragraphcCollection, "100", TableWidthUnitValues.Auto,"0","200", TableVerticalAlignmentValues.Top, null,3));
            paragraphcCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart());
            paragraphcCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart(n280, "26", JustificationValues.Center, 0,"0", true));
            paragraphcCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart(n279, "26", JustificationValues.Center));
            cellcCollection.Add(CellsTablesXml.GenerateCell(ref paragraphcCollection, "5500", TableWidthUnitValues.Dxa,"1500","0",TableVerticalAlignmentValues.Center));
            table.Append(rows.GenerateRow(ref cellcCollection));
            cellcCollection.Add(CellsTablesXml.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(),"0",TableWidthUnitValues.Nil,"800","0", TableVerticalAlignmentValues.Top, CellBorders.GenerateBorder()));
            cellcCollection.Add(CellsTablesXml.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("№", "24", JustificationValues.Center), "0", TableWidthUnitValues.Nil));
            cellcCollection.Add(CellsTablesXml.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Nil, "0", "1500",TableVerticalAlignmentValues.Top, CellBorders.GenerateBorder()));
            cellcCollection.Add(CellsTablesXml.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Nil));
            table.Append(rows.GenerateRow(ref cellcCollection));
            cellcCollection.Add(CellsTablesXml.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("На №", "24", JustificationValues.Center), "0", TableWidthUnitValues.Auto));
            cellcCollection.Add(CellsTablesXml.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto,"0","0",TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorder(),2));
            cellcCollection.Add(CellsTablesXml.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Nil));
            table.Append(rows.GenerateRow(ref cellcCollection));
            body.Append(table);
            body.Append(paragraphGenerate.LineBreker(3));
          return body;

        }

    }
}
