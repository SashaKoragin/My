using System.Collections.ObjectModel;
using DocumentFormat.OpenXml.Wordprocessing;
using LibaryDocumentGenerator.ProgrammView.Word.Libary.ParagraphsGenerator;
using LibaryDocumentGenerator.ProgrammView.Word.Libary.TablesGenrerator;
using LibaryXMLAutoReports.ReportsBdk;

namespace LibaryDocumentGenerator.ProgrammView.Word.Template.BodyDocument
{
   public class BodyDocument
    {
        /// <summary>
        /// Генерация абзатца по полю BodyGl1
        /// </summary>
        /// <param name="tamplate">Шаблон</param>
        /// <returns></returns>
        public Body TextDocument(LibaryXMLAutoReports.FullTemplateSheme.Document tamplate)
        {
            Body body = new Body();
            var paragraphGenerate = new RunGenerate();
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart(tamplate.Templates.Body.BodyGl1, "26", JustificationValues.Both, 0, "1000"));
            return body;
        }

        /// <summary>
        /// Документ на исходящее письмо генерация таблицы
        /// </summary>
        /// <returns></returns>
        public Body DocumentIshBdkTableBody(FN71 parametr)
        {
            Body body = new Body();
            var table = new TablesGenerate();
            var rows = new RowGenerate();
            var tables = table.CreateTableLine();
            ObservableCollection<TableCell> cellcCollection = new ObservableCollection<TableCell>();
            var paragraphGenerate = new RunGenerate();
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Дата отправки", "26", JustificationValues.Center, 1), "100", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorderFull()));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Наименование контейнера", "26", JustificationValues.Center, 1), "100", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorderFull()));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("GUID", "26", JustificationValues.Center, 1), "100", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorderFull()));
            tables.Append(rows.GenerateRow(ref cellcCollection));
            foreach (var var in parametr.FN1723_2)
            {
                cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(var.D85.ToString("dd-MM-yyyy"), "22", JustificationValues.Center), "1900", TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorderFull()));
                cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(var.D981, "22", JustificationValues.Center), "2100", TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorderFull()));
                cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(var.GUID, "22", JustificationValues.Center), "4500", TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorderFull()));
                tables.Append(rows.GenerateRow(ref cellcCollection));
            }
            body.Append(tables);
            body.Append(paragraphGenerate.LineBreker(2));
            return body;
        }
    }
}
