using System.Collections.ObjectModel;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using DocumentFormat.OpenXml.Wordprocessing;
using LibaryDocumentGenerator.Documents.Word.Generate.UseDocument.Libary.Page;
using LibaryDocumentGenerator.Documents.Word.Generate.UseDocument.Libary.Paragraphs;
using LibaryDocumentGenerator.Documents.Word.Generate.UseDocument.Libary.Tables;
using LibaryXMLAutoReports.ReportsBdk;

namespace LibaryDocumentGenerator.Documents.Word.Generate.UseDocument.BodyDocument
{
   public class BodyDocument
   {
        /// <summary>
        /// Генерация абзатца по полю BodyGl1
        /// </summary>
        /// <param name="tamplate">Шаблон</param>
        /// <returns></returns>
        public Body TextDocument(LibaryXMLAutoReports.TemplateSheme.Template tamplate)
        {
            Body body = new Body();
            GenerateRun paragraphGenerate = new GenerateRun();
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart(tamplate.Body.BodyGl1, "26", JustificationValues.Both, 0, "1000"));
            return body;
        }




        /// <summary>
        /// Документ на исходящее письмо генерация таблицы
        /// </summary>
        /// <returns></returns>
        public Body DocumentIshBdkTableBody(FN71 parametr)
       {
            Body body = new Body();
            Tables table = new Tables();
            RowTableXml rows = new RowTableXml();
            var tables = table.CreateTableLine();
            ObservableCollection<TableCell> cellcCollection = new ObservableCollection<TableCell>();
            GenerateRun paragraphGenerate = new GenerateRun();
            cellcCollection.Add(CellsTablesXml.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Дата отправки", "26", JustificationValues.Center, 1),"100",TableWidthUnitValues.Auto,"0","0",TableVerticalAlignmentValues.Bottom,CellBorders.GenerateBorderFull()));
            cellcCollection.Add(CellsTablesXml.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Наименование контейнера", "26", JustificationValues.Center, 1), "100", TableWidthUnitValues.Auto, "0","0",TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorderFull()));
            cellcCollection.Add(CellsTablesXml.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("GUID", "26", JustificationValues.Center, 1), "100", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorderFull()));
            tables.Append(rows.GenerateRow(ref cellcCollection));
            foreach (var var in parametr.FN1723_2)
            {
                cellcCollection.Add(CellsTablesXml.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(var.D85.ToString("dd-MM-yyyy"), "22", JustificationValues.Center), "1900", TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorderFull()));
                cellcCollection.Add(CellsTablesXml.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(var.D981, "22", JustificationValues.Center), "2100", TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorderFull()));
                cellcCollection.Add(CellsTablesXml.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(var.GUID, "22", JustificationValues.Center), "4500", TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorderFull()));
                tables.Append(rows.GenerateRow(ref cellcCollection));
            }
            body.Append(tables);
            body.Append(paragraphGenerate.LineBreker(2));
            return body;
       }
   }
}
