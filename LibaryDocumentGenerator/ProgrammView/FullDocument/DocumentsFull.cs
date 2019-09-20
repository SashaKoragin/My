using System;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Wordprocessing;
using EfDatabaseTelephoneHelp;
using EfDatabaseXsdBookAccounting;
using LibaryDocumentGenerator.ProgrammView.Word.Libary.Drawing;
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
        public Body DocumentsBook(EfDatabaseInvoice.Report report,string relationshipId)
        {
            AddDriwing driwing = new AddDriwing();
            Body body = new Body();
            Table table = new Table();
            Table table2 = new Table();
            Table table3 = new Table();
            Table table4 = new Table();
            Table table5 = new Table(new TableProperties(new TableOverlap() {Val = TableOverlapValues.Never}, new TablePositionProperties() { LeftFromText = 180, RightFromText = 180, VerticalAnchor = VerticalAnchorValues.Text, TablePositionY = 1 }));
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
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(report.Main.Organization.NameOrganization, "16", JustificationValues.Center, 3), "0", TableWidthUnitValues.Auto,
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
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("#"+report.Main.Barcode.Id, "20", JustificationValues.Center), "0", TableWidthUnitValues.Dxa,
            "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorderFull(BorderValues.Single, 14)));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(DateTime.Now.ToString("dd.MM.yyyy"), "20", JustificationValues.Center), "0", TableWidthUnitValues.Dxa,
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
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(report.Main.Formed.NameOtdel, "16", JustificationValues.Center), "0", TableWidthUnitValues.Dxa,
            "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(report.Main.Received.NameOtdel, "16", JustificationValues.Center), "0", TableWidthUnitValues.Dxa,
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
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Категория", "20", JustificationValues.Center, 1), "0", TableWidthUnitValues.Auto,
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
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Примечание", "20", JustificationValues.Center, 1), "0", TableWidthUnitValues.Auto,
                "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            table4.Append(rows.GenerateRow(ref cellcCollection, true, rows.FormulHeightRow(1)));
            foreach (var bodytable in report.Body)
            {
                cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(bodytable.Name, "20", JustificationValues.Center), "0", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(bodytable.NameCategory, "20", JustificationValues.Center), "0", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(bodytable.Model, "20", JustificationValues.Center), "0", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(bodytable.Category, "20", JustificationValues.Center), "0", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(bodytable.SerNum, "20", JustificationValues.Center), "0", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(bodytable.ServiceNum, "20", JustificationValues.Center), "0", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(bodytable.InventarNumSysBlok, "20", JustificationValues.Center), "0", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(bodytable.NumberKabinet, "20", JustificationValues.Center), "0", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(bodytable.NameComputer, "20", JustificationValues.Center), "0", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(bodytable.IpAdress, "20", JustificationValues.Center), "0", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(bodytable.Coment, "20", JustificationValues.Center), "0", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                table4.Append(rows.GenerateRow(ref cellcCollection));
            }
            var barcode = paragraphGenerate.RunParagraphGeneratorStandart("", "20", JustificationValues.Center, 0, "1000");
            barcode.Append(driwing.AddImageToParagraph(relationshipId));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Отпустил:"), CellGenerate.FormulWidthCell(2.2), TableWidthUnitValues.Dxa));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(report.Main.Formed.NamePosition, "20", JustificationValues.Center), CellGenerate.FormulWidthCell(5.5), TableWidthUnitValues.Dxa,
                "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorder()));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), CellGenerate.FormulWidthCell(0.2), TableWidthUnitValues.Dxa));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), CellGenerate.FormulWidthCell(3.1), TableWidthUnitValues.Dxa,
                "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorder()));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), CellGenerate.FormulWidthCell(0.2), TableWidthUnitValues.Dxa));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(report.Main.Formed.SmallName, "20", JustificationValues.Center), CellGenerate.FormulWidthCell(7.5), TableWidthUnitValues.Dxa,
                "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorder()));

            table5.Append(rows.GenerateRow(ref cellcCollection, true, rows.FormulHeightRow(0.8)));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("должность", "16", JustificationValues.Center), "0", TableWidthUnitValues.Auto));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("подпись", "16", JustificationValues.Center), "0", TableWidthUnitValues.Auto));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("расшифровка подписи", "16", JustificationValues.Center), "0", TableWidthUnitValues.Auto));
            table5.Append(rows.GenerateRow(ref cellcCollection, true, rows.FormulHeightRow(0.4)));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Получил:"), "0", TableWidthUnitValues.Auto));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(report.Main.Received.ChiefPosition, "20", JustificationValues.Center), "0", TableWidthUnitValues.Auto,
                "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorder()));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto,
                "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorder()));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(report.Main.Received.ChiefSmallName, "20", JustificationValues.Center), "0", TableWidthUnitValues.Auto,
                "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorder()));
            table5.Append(rows.GenerateRow(ref cellcCollection, true, rows.FormulHeightRow(0.8)));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("должность", "16", JustificationValues.Center), "0", TableWidthUnitValues.Auto));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("подпись", "16", JustificationValues.Center), "0", TableWidthUnitValues.Auto));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("расшифровка подписи", "16", JustificationValues.Center), "0", TableWidthUnitValues.Auto));
            table5.Append(rows.GenerateRow(ref cellcCollection, true, rows.FormulHeightRow(0.4)));
            
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
            body.Append(barcode);
            return body;
        }

        /// <summary>
        /// Генерация телефонного справочника инспенкции
        /// </summary>
        /// <param name="telephoneHelp">Параметры модели из БД для генерации телефонного справочника</param>
        /// <returns></returns>
        public Body DocumentsTelephoneHelp(TelephoneHelp telephoneHelp)
        {
                Body body = new Body();
                Table table = new Table();
                var paragraphGenerate = new RunGenerate();
                ObservableCollection<TableCell> cellcCollection = new ObservableCollection<TableCell>();
                var rows = new RowGenerate();
                cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Фамилия Имя Отчество","26",JustificationValues.Center, 1), CellGenerate.FormulWidthCell(5.2), TableWidthUnitValues.Dxa,"0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Должность", "26", JustificationValues.Center, 1),CellGenerate.FormulWidthCell(5.2), TableWidthUnitValues.Dxa, "0", "0",TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Городской номер", "26",JustificationValues.Center, 1), CellGenerate.FormulWidthCell(6), TableWidthUnitValues.Dxa,"0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Внутренний номер", "26",JustificationValues.Center, 1), CellGenerate.FormulWidthCell(3.5), TableWidthUnitValues.Dxa,"0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Кабинет", "26", JustificationValues.Center, 1),CellGenerate.FormulWidthCell(2.2), TableWidthUnitValues.Dxa, "0", "0",TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                table.Append(rows.GenerateRow(ref cellcCollection));
                if (telephoneHelp.TelephonHeaders.Length > 0)
                {
                    foreach (var headers in telephoneHelp.TelephonHeaders)
                    {
                        if (headers.Telephon != null)
                        {
                            if (headers.Name.Contains("Окна"))
                            {
                                cellcCollection.Add(CellGenerate.GenerateCell( paragraphGenerate.RunParagraphGeneratorStandart(headers.Name, "26", JustificationValues.Center, 1), "0", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 5));
                                table.Append(rows.GenerateRow(ref cellcCollection));
                            }
                            foreach (var telephon in headers.Telephon)
                            {
                                cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(telephon.Coment, "26"), "0",TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center,CellBorders.GenerateBorderFull()));
                                cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center,CellBorders.GenerateBorderFull()));
                                cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(telephon.TelephonUndeground,"26",JustificationValues.Center), "0", TableWidthUnitValues.Auto, "0", "0",TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                                cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(telephon.Telephon1, "26",JustificationValues.Center), "0", TableWidthUnitValues.Auto, "0", "0",TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                                cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(telephon.Kabinet?.NumberKabinet,"26",JustificationValues.Center), "0", TableWidthUnitValues.Auto, "0", "0",TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                                table.Append(rows.GenerateRow(ref cellcCollection));
                            }
                        }
                    }
                }
                if (telephoneHelp.TelephonBody.Length > 0)
                {
                 try
                  {
                    foreach (var bodys in telephoneHelp.TelephonBody)
                    {
                        cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(bodys.NameOtdel,"26", JustificationValues.Center,1), "0", TableWidthUnitValues.Auto, "0", "0",TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 5));
                        table.Append(rows.GenerateRow(ref cellcCollection));
                        foreach (var users in bodys.Users)
                        {
                            var strname = users.Name.Split(' ');
                            var parag = paragraphGenerate.RunParagraphGeneratorStandart(strname[0] + ' '+ strname[1], "24",JustificationValues.Left,0,"0",true);
                            if (strname.Length > 2)
                            {
                                parag.Append(paragraphGenerate.RunText(strname[2], "24"));
                            }
                            cellcCollection.Add(CellGenerate.GenerateCell(parag, "0",TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center,CellBorders.GenerateBorderFull()));
                            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(users.Position.NamePosition, "24",JustificationValues.Left,0,"20"),"0", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center,CellBorders.GenerateBorderFull()));
                            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(users.Telephon?.TelephonUndeground, "24", JustificationValues.Center), "0", TableWidthUnitValues.Auto, "0", "0",TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(users.Telephon?.Telephon1, "24", JustificationValues.Center), "0", TableWidthUnitValues.Auto, "0", "0",TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(
                                users.Telephon != null ? users.Telephon.Kabinet.NumberKabinet : users.NumberKabinet, "24", JustificationValues.Center), "0",TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center,CellBorders.GenerateBorderFull()));
                            table.Append(rows.GenerateRow(ref cellcCollection));
                        }
                    }
                }
                catch (Exception e)
                {
                    Loggers.Log4NetLogger.Error(e);
                }
            }
            body.Append(table);
            return body;
        }

        /// <summary>
        /// Генерация телефонного справочника инспенкции
        /// </summary>
        /// <param name="book">Книга учета</param>
        /// <param name="relationshipId">Ссылка на штрихкод</param>
        /// <returns></returns>
        public Body BookAccounting(Book book, string relationshipId)
        {
            Body body = new Body();
            Table table = new Table();
            Table table1 = new Table();
            Table table2 = new Table();
            Table table3 = new Table();
            AddDriwing driwing = new AddDriwing();

            var paragraphGenerate = new RunGenerate();
            var barcode = paragraphGenerate.RunParagraphGeneratorStandart();
            barcode.Append(driwing.AddImageToParagraph(relationshipId));
            ObservableCollection<TableCell> cellcCollection = new ObservableCollection<TableCell>();
            var rows = new RowGenerate();
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), CellGenerate.FormulWidthCell(1.9),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Top));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), CellGenerate.FormulWidthCell(2.2),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Top));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), CellGenerate.FormulWidthCell(0.8),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Top));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("КНИГА","22",JustificationValues.Center,1), CellGenerate.FormulWidthCell(7),
                TableWidthUnitValues.Dxa,  "1600", "0", TableVerticalAlignmentValues.Center));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), CellGenerate.FormulWidthCell(2.5),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Top));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), CellGenerate.FormulWidthCell(3.6),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center));
            table.Append(rows.GenerateRow(ref cellcCollection, true, rows.FormulHeightRow(0.5)));
            cellcCollection.Add(CellGenerate.GenerateCell(barcode, "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, null, 3,1));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("учета материальных ценностей", "22", JustificationValues.Center, 1), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, null, 2));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("КОДЫ", "16", JustificationValues.Center), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            table.Append(rows.GenerateRow(ref cellcCollection, true, rows.FormulHeightRow(0.5)));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, null, 3,2));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Форма по ОКУД", "16", JustificationValues.Right), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, null, 2));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("0504042", "16", JustificationValues.Center), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(BorderValues.Single, 14)));
            table.Append(rows.GenerateRow(ref cellcCollection, true, rows.FormulHeightRow(0.5)));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, null, 3,2));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Дата открытия", "16", JustificationValues.Right), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, null, 2));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Top, CellBorders.GenerateBorderFull(BorderValues.Single, 14)));
            table.Append(rows.GenerateRow(ref cellcCollection, true, rows.FormulHeightRow(0.5)));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, null, 3,2));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Дата закрытия", "16", JustificationValues.Right), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, null, 2));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Top, CellBorders.GenerateBorderFull(BorderValues.Single, 14)));
            table.Append(rows.GenerateRow(ref cellcCollection, true, rows.FormulHeightRow(0.5)));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Учреждение:", "18"), "0",
                TableWidthUnitValues.Auto));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(book.Organization.NameOrganization, "18"), "0",
                TableWidthUnitValues.Auto,"0","0",TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorder(BorderValues.Single, 14),3));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("по ОКПО", "16", JustificationValues.Right), "0",
                TableWidthUnitValues.Auto));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Top, CellBorders.GenerateBorderFull(BorderValues.Single, 14)));
            table.Append(rows.GenerateRow(ref cellcCollection, true, rows.FormulHeightRow(0.5)));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Структурное подразделение:","18"), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Bottom, null, 2));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(book.Organization.NameDepartament, "18"), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorder(BorderValues.Single, 14), 2));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Top, CellBorders.GenerateBorderFull(BorderValues.Single, 14)));
            table.Append(rows.GenerateRow(ref cellcCollection, true, rows.FormulHeightRow(0.5)));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Материально ответственное лицо:", "18"), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Bottom, null, 3));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(book.Organization.NameFace, "18"), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorder(BorderValues.Single, 14)));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Top, CellBorders.GenerateBorderFull(BorderValues.Single, 14)));
            table.Append(rows.GenerateRow(ref cellcCollection, true, rows.FormulHeightRow(0.5)));

            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Склад","16", JustificationValues.Center), CellGenerate.FormulWidthCell(1.4),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(),0,1));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Стелаж", "16", JustificationValues.Center), CellGenerate.FormulWidthCell(1.4),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 1));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Ячейка", "16", JustificationValues.Center), CellGenerate.FormulWidthCell(1.4),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 1));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Единица измерения", "16", JustificationValues.Center), CellGenerate.FormulWidthCell(3.5),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(),2,1));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Цена", "16", JustificationValues.Center), CellGenerate.FormulWidthCell(2),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 1));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Марка", "16", JustificationValues.Center), CellGenerate.FormulWidthCell(1.3),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 1));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Сорт", "16", JustificationValues.Center), CellGenerate.FormulWidthCell(1.8),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 1));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Профиль", "16", JustificationValues.Center), CellGenerate.FormulWidthCell(1.6),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 1));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Размер", "16", JustificationValues.Center), CellGenerate.FormulWidthCell(1.6),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 1));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Норма запаса", "16",JustificationValues.Center), CellGenerate.FormulWidthCell(2),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 1));
            table1.Append(rows.GenerateRow(ref cellcCollection, true, rows.FormulHeightRow(0.5)));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 2));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 2));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 2));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("наименование", "16", JustificationValues.Center), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("код", "16", JustificationValues.Center), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 2));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 2));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 2));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 2));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 2));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 2));
            table1.Append(rows.GenerateRow(ref cellcCollection, true));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            table1.Append(rows.GenerateRow(ref cellcCollection, true, rows.FormulHeightRow(0.5)));

            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Наименование материала:"), CellGenerate.FormulWidthCell(4.5),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(book.BareCodeBook.NameModel), CellGenerate.FormulWidthCell(9.2),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorder(BorderValues.Single, 14)));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("номер кода", "20",JustificationValues.Right), CellGenerate.FormulWidthCell(2.3),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("#"+book.BareCodeBook.Id,"20",JustificationValues.Center), CellGenerate.FormulWidthCell(2),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(BorderValues.Single, 14)));
            table2.Append(rows.GenerateRow(ref cellcCollection, true, rows.FormulHeightRow(0.5)));

            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Порядковый номер записи", "18", JustificationValues.Center), CellGenerate.FormulWidthCell(1.4),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 1));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Дата записи", "18", JustificationValues.Center), CellGenerate.FormulWidthCell(1.4),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 1));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Документ", "18", JustificationValues.Center), CellGenerate.FormulWidthCell(3.2),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 2));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("От кого получено (кому отпущено)", "18", JustificationValues.Center), CellGenerate.FormulWidthCell(4.6),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 1));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Приход", "18", JustificationValues.Center), CellGenerate.FormulWidthCell(1.9),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 1));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Расход", "18", JustificationValues.Center), CellGenerate.FormulWidthCell(1.7),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 1));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Остаток", "16", JustificationValues.Center), CellGenerate.FormulWidthCell(1.6),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 1));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Контроль (подпись дата)", "16", JustificationValues.Center), CellGenerate.FormulWidthCell(2.2),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 1));

            table3.Append(rows.GenerateRow(ref cellcCollection, true, rows.FormulHeightRow(1)));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 2));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 2));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("дата", "16", JustificationValues.Center), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("номер", "16", JustificationValues.Center), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 2));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 2));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 2));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 2));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 2));
            table3.Append(rows.GenerateRow(ref cellcCollection, true, rows.FormulHeightRow(0.5)));

            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(DateTime.Now.ToString("dd.MM.yyyy"), "18"), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(book.Organization.Supply?.DaysPostavki, "18", JustificationValues.Center), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Поставка " + book.Organization.Supply?.Years, "18"), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(book.Organization.Position.Length.ToString(),"18", JustificationValues.Center), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(book.Organization.Position.Length.ToString(),"18",JustificationValues.Center), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            table3.Append(rows.GenerateRow(ref cellcCollection, true, rows.FormulHeightRow(0.5)));

            cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(),9));
            table3.Append(rows.GenerateRow(ref cellcCollection, true, rows.FormulHeightRow(0.5)));
            var i = 1;
            var ost = book.Organization?.Position.Length;
            var rashod = 1;
            foreach (var position in book.Organization.Position)
            {
                if (position.Users.SmallName != null)
                {
                    ost = ost - 1;
                }
                else
                {
                    rashod = 0;
                }
                cellcCollection.Add(
                   CellGenerate.GenerateCell(
                       paragraphGenerate.RunParagraphGeneratorStandart(i.ToString(), "18",
                           JustificationValues.Center), "0",
                       TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center,
                       CellBorders.GenerateBorderFull()));
                cellcCollection.Add(
                    CellGenerate.GenerateCell(
                        paragraphGenerate.RunParagraphGeneratorStandart(DateTime.Now.ToString("dd.MM.yyyy"), "18"),
                        "0",
                        TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center,
                        CellBorders.GenerateBorderFull()));
                cellcCollection.Add(
                    CellGenerate.GenerateCell(
                        paragraphGenerate.RunParagraphGeneratorStandart(position.SerNum, "18",
                            JustificationValues.Center), "0",
                        TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center,
                        CellBorders.GenerateBorderFull(), 2));
                cellcCollection.Add(
                    CellGenerate.GenerateCell(
                        paragraphGenerate.RunParagraphGeneratorStandart(position.Users?.SmallName, "18"), "0",
                        TableWidthUnitValues.Auto, "200", "0", TableVerticalAlignmentValues.Center,
                        CellBorders.GenerateBorderFull()));
                cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                    TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center,
                    CellBorders.GenerateBorderFull()));
                cellcCollection.Add(
                    CellGenerate.GenerateCell(
                        paragraphGenerate.RunParagraphGeneratorStandart(rashod.ToString(), "18",
                            JustificationValues.Center), "0",
                        TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center,
                        CellBorders.GenerateBorderFull()));
                cellcCollection.Add(
                    CellGenerate.GenerateCell(
                        paragraphGenerate.RunParagraphGeneratorStandart(ost.ToString(), "18",
                            JustificationValues.Center), "0",
                        TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center,
                        CellBorders.GenerateBorderFull()));
                cellcCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                    TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center,
                    CellBorders.GenerateBorderFull()));
                table3.Append(rows.GenerateRow(ref cellcCollection, true, rows.FormulHeightRow(0.6)));
                i++;
            }
            body.Append(table);
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart());
            body.Append(table1);
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart());
            body.Append(table2);
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart());
            body.Append(table3);
            return body;
        }
    }
}
