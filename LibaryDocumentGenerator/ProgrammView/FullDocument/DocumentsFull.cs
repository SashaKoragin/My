using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Wordprocessing;
using LibaryDocumentGenerator.ProgrammView.Word.Libary.ParagraphsGenerator;
using LibaryDocumentGenerator.ProgrammView.Word.Libary.TablesGenrerator;

namespace LibaryDocumentGenerator.ProgrammView.FullDocument
{
   public class DocumentsFull
    {
        /// <summary>
        /// Накладная на внутреенее перемещение
        /// </summary>
        /// <returns></returns>
        public Body DocumentsBook(EfDatabaseInvoice.Report report)
        {
            Body body = new Body();
            Table table = new Table();
            Table table2 = new Table();
            Table table3 = new Table();
            Table table4 = new Table();
            Table table5 = new Table();
            var paragraphGenerate = new RunGenerate();
            ObservableCollection<Paragraph> paragraphcCollection = new ObservableCollection<Paragraph>();
            ObservableCollection<TableCell> cellcCollection = new ObservableCollection<TableCell>();
            var rows = new RowGenerate();
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), CellGenerate.FormulWidthCell(21.5),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Top));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), CellGenerate.FormulWidthCell(2.7),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Top));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Код", "16", JustificationValues.Center), CellGenerate.FormulWidthCell(3.6),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorderFull()));
            table.Append(rows.GenerateRow(ref cellcCollection, true, rows.FormulHeightRow(0.5)));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
            TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Top, CellBorders.GenerateBorderFull(BorderValues.Single, 14)));
            table.Append(rows.GenerateRow(ref cellcCollection, true, rows.FormulHeightRow(0.5)));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Межрайонная ИФНС России №51 по г. Москве", "16", JustificationValues.Center, 3), "0", TableWidthUnitValues.Auto,
                "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorder()));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
            TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Top, CellBorders.GenerateBorderFull(BorderValues.Single, 14)));
            table.Append(rows.GenerateRow(ref cellcCollection, true, rows.FormulHeightRow(0.5)));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("(организация)", "14", JustificationValues.Center), "0", TableWidthUnitValues.Auto,
            "0", "0", TableVerticalAlignmentValues.Top));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto,
                "0", "0", TableVerticalAlignmentValues.Top, CellBorders.GenerateBorderFull(BorderValues.Single, 14)));
            table.Append(rows.GenerateRow(ref cellcCollection, true, rows.FormulHeightRow(0.5)));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Вид операции", "16", JustificationValues.Center), "0", TableWidthUnitValues.Auto));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Передача", "16", JustificationValues.Center), "0", TableWidthUnitValues.Auto,
                "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorderFull(BorderValues.Single, 14)));
            table.Append(rows.GenerateRow(ref cellcCollection, true, rows.FormulHeightRow(0.5)));

            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), CellGenerate.FormulWidthCell(15.3), TableWidthUnitValues.Dxa));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Номер документа", "16", JustificationValues.Center), CellGenerate.FormulWidthCell(2.1), TableWidthUnitValues.Dxa,
            "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorderFull()));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Дата составления", "16", JustificationValues.Center), CellGenerate.FormulWidthCell(2.1), TableWidthUnitValues.Dxa,
            "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorderFull()));
            table2.Append(rows.GenerateRow(ref cellcCollection, true, rows.FormulHeightRow(1)));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("НАКЛАДНАЯ", "20", JustificationValues.Right, 1), "0", TableWidthUnitValues.Dxa));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("#1", "20", JustificationValues.Center), "0", TableWidthUnitValues.Dxa,
            "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorderFull(BorderValues.Single, 14)));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("22.03.2019", "20", JustificationValues.Center), "0", TableWidthUnitValues.Dxa,
            "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorderFull(BorderValues.Single, 14)));
            table2.Append(rows.GenerateRow(ref cellcCollection, true, rows.FormulHeightRow(0.5)));

            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), CellGenerate.FormulWidthCell(5.8), TableWidthUnitValues.Dxa));

            paragraphcCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart("Отправитель", "16", JustificationValues.Center));
            paragraphcCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart("(структурное подразделение)", "16", JustificationValues.Center));
            cellcCollection.Add(CellGenerate.GenerateCell(ref paragraphcCollection, CellGenerate.FormulWidthCell(8.1), TableWidthUnitValues.Dxa,
            "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            paragraphcCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart("Получатель", "16", JustificationValues.Center));
            paragraphcCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart("(структурное подразделение)", "16", JustificationValues.Center));
            cellcCollection.Add(CellGenerate.GenerateCell(ref paragraphcCollection, CellGenerate.FormulWidthCell(8.1), TableWidthUnitValues.Dxa,
            "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));

            table3.Append(rows.GenerateRow(ref cellcCollection, true, rows.FormulHeightRow(0.9)));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Dxa));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Отдел информатизации", "16", JustificationValues.Center), "0", TableWidthUnitValues.Dxa,
            "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Отдел кадров", "16", JustificationValues.Center), "0", TableWidthUnitValues.Dxa,
            "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            table3.Append(rows.GenerateRow(ref cellcCollection, true, rows.FormulHeightRow(0.5)));

            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), CellGenerate.FormulWidthCell(27.8), TableWidthUnitValues.Dxa, "0",
                "0", TableVerticalAlignmentValues.Bottom, null, 8));
            table4.Append(rows.GenerateRow(ref cellcCollection, true, rows.FormulHeightRow(0.2)));

            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Имя пользователя", "20", JustificationValues.Center, 1), "0", TableWidthUnitValues.Auto,
                "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Имя оборудования", "20", JustificationValues.Center, 1), "0", TableWidthUnitValues.Auto,
                "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Наименование модели", "20", JustificationValues.Center, 1), "0", TableWidthUnitValues.Auto,
                "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Серийный номер", "20", JustificationValues.Center, 1), "0", TableWidthUnitValues.Auto,
                "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Сервисный номер", "20", JustificationValues.Center, 1), "0", TableWidthUnitValues.Auto,
                "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Инвентарный номер", "20", JustificationValues.Center, 1), "0", TableWidthUnitValues.Auto,
                "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Кабинет", "20", JustificationValues.Center, 1), "0", TableWidthUnitValues.Auto,
                "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Наименование компьютера", "20", JustificationValues.Center, 1), "0", TableWidthUnitValues.Auto,
                "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Ip Адрес", "20", JustificationValues.Center, 1), "0", TableWidthUnitValues.Auto,
                "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            table4.Append(rows.GenerateRow(ref cellcCollection, true, rows.FormulHeightRow(1)));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Отпустил:"), CellGenerate.FormulWidthCell(2.2), TableWidthUnitValues.Dxa));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Главный специалист-эксперт", "20", JustificationValues.Center), CellGenerate.FormulWidthCell(5.5), TableWidthUnitValues.Dxa,
                "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorder()));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), CellGenerate.FormulWidthCell(0.2), TableWidthUnitValues.Dxa));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), CellGenerate.FormulWidthCell(3.1), TableWidthUnitValues.Dxa,
                "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorder()));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), CellGenerate.FormulWidthCell(0.2), TableWidthUnitValues.Dxa));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("А.А.Шагаева", "20", JustificationValues.Center), CellGenerate.FormulWidthCell(7.5), TableWidthUnitValues.Dxa,
                "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorder()));
            table5.Append(rows.GenerateRow(ref cellcCollection, true));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("должность", "16", JustificationValues.Center), "0", TableWidthUnitValues.Auto));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("подпись", "16", JustificationValues.Center), "0", TableWidthUnitValues.Auto));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("расшифровка подписи", "16", JustificationValues.Center), "0", TableWidthUnitValues.Auto));
            table5.Append(rows.GenerateRow(ref cellcCollection, true, rows.FormulHeightRow(0.3)));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Получил:"), "0", TableWidthUnitValues.Auto));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Начальник отдела кадров", "20", JustificationValues.Center), "0", TableWidthUnitValues.Auto,
                "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorder()));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto,
                "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorder()));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Т.А.Королёва", "20", JustificationValues.Center), "0", TableWidthUnitValues.Auto,
                "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorder()));
            table5.Append(rows.GenerateRow(ref cellcCollection, true, rows.FormulHeightRow(0.8)));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("должность", "16", JustificationValues.Center), "0", TableWidthUnitValues.Auto));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("подпись", "16", JustificationValues.Center), "0", TableWidthUnitValues.Auto));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("расшифровка подписи", "16", JustificationValues.Center), "0", TableWidthUnitValues.Auto));
            table5.Append(rows.GenerateRow(ref cellcCollection, true, rows.FormulHeightRow(0.3)));
            body.Append(table);
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart());
            body.Append(table2);
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart("на внутреннее перемещение", "20", JustificationValues.Center, 1));
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart());
            body.Append(table3);
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart());
            body.Append(table4);
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart());
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart());
            body.Append(table5);
            return body;
        }
    }
}
