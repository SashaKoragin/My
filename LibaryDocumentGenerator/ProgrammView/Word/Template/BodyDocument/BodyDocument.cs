using System.Collections.ObjectModel;
using DocumentFormat.OpenXml.Wordprocessing;
using LibaryDocumentGenerator.ProgrammView.Word.Libary.ParagraphsGenerator;
using LibaryDocumentGenerator.ProgrammView.Word.Libary.TablesGenrerator;
using LibaryXMLAutoModelXmlAuto.MigrationReport;
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
        /// Шаблон текста для Миграции
        /// </summary>
        /// <param name="tamplate">Шаблон 2</param>
        /// <returns></returns>
        public Body TextDocumentFormatMigration(LibaryXMLAutoReports.FullTemplateSheme.Document tamplate)
        {
            Body body = new Body();
            var paragraphGenerate = new RunGenerate();
            var parag = paragraphGenerate.RunParagraphGeneratorStandart(tamplate.Templates.Body.BodyGl1, "26",JustificationValues.Both, 0, "1000");
            parag.Append(paragraphGenerate.RunText(tamplate.Templates.Body.BodyGl2, "26", 1));
            parag.Append(paragraphGenerate.RunText(tamplate.Templates.Body.BodyGl3, "26"));
            body.Append(parag);
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

        /// <summary>
        /// Utythfwbz nf,kbws lkz vbuhfwbb
        /// </summary>
        /// <param name="arrayerror">Масив объектов ReportMigration</param>
        /// <param name="isulandfl">Юл и ИП = 1 ФЛ != 1</param>
        /// <returns></returns>
        public Body GenerateMigrationTable(ReportMigration[] arrayerror, int isulandfl)
        {
            Body body = new Body();
            var table = new TablesGenerate();
            var rows = new RowGenerate();
            var tables = table.CreateTableLine();
            ObservableCollection<TableCell> cellcCollection = new ObservableCollection<TableCell>();
            var paragraphGenerate = new RunGenerate();
            int index = 1;
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("№ п/п", "26", JustificationValues.Center, 1), "100", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorderFull()));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Наименование НП", "26", JustificationValues.Center, 1), "100", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorderFull()));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("ИНН", "26", JustificationValues.Center, 1), "100", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorderFull()));
            if (isulandfl==1)
            {
                cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("КПП", "26", JustificationValues.Center, 1), "100", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorderFull()));
            }
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Причина", "26", JustificationValues.Center, 1), "100", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorderFull()));
            tables.Append(rows.GenerateRow(ref cellcCollection));
            foreach (var param in arrayerror)
            {
                cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(index.ToString(), "22", JustificationValues.Center), "0", TableWidthUnitValues.Auto, "100", "100", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(param.NameOrg, "22", JustificationValues.Center), "0", TableWidthUnitValues.Auto, "100", "100", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                cellcCollection.Add( CellGenerate.GenerateCell( paragraphGenerate.RunParagraphGeneratorStandart(param.Inn, "22", JustificationValues.Center),"0", TableWidthUnitValues.Auto,"100", "100", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                if (isulandfl == 1)
                {
                    cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(param.Kpp, "22", JustificationValues.Center), "0", TableWidthUnitValues.Auto, "100", "100", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                }
                cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(param.Problem, "22", JustificationValues.Both), "0", TableWidthUnitValues.Auto, "100", "100", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                index++;
                tables.Append(rows.GenerateRow(ref cellcCollection));
            }
            body.Append(tables);
            body.Append(paragraphGenerate.LineBreker(2));
            return body;
        }

         
    }
}
