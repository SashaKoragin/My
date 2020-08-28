using System;
using System.Collections.ObjectModel;
using DocumentFormat.OpenXml.Wordprocessing;
using EfDatabase.Inventory.Base;
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
            AddDriwing driving = new AddDriwing();
            Body body = new Body();
            Table table = new Table();
            Table table2 = new Table();
            Table table3 = new Table();
            Table table4 = new Table();
            Table table5 = new Table(new TableProperties(new TableOverlap() {Val = TableOverlapValues.Never}, new TablePositionProperties() { LeftFromText = 180, RightFromText = 180, VerticalAnchor = VerticalAnchorValues.Text, TablePositionY = 1 }));
            var paragraphGenerate = new RunGenerate();
            ObservableCollection<Paragraph> paragraphCollection = new ObservableCollection<Paragraph>();
            ObservableCollection<TableCell> cellCollection = new ObservableCollection<TableCell>();
            var rows = new RowGenerate();
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), CellGenerate.FormulWidthCell(21.5),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Top));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), CellGenerate.FormulWidthCell(2.7),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Top));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Код", "16", JustificationValues.Center), CellGenerate.FormulWidthCell(3.6),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorderFull()));
            table.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.5)));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
            TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Top, CellBorders.GenerateBorderFull(BorderValues.Single, 14)));
            table.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.5)));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(report.Main.Organization.NameOrganization, "16", JustificationValues.Center, 3), "0", TableWidthUnitValues.Auto,
                "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorder()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
            TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Top, CellBorders.GenerateBorderFull(BorderValues.Single, 14)));
            table.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.5)));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("(организация)", "14", JustificationValues.Center), "0", TableWidthUnitValues.Auto,
            "0", "0", TableVerticalAlignmentValues.Top));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto,
                "0", "0", TableVerticalAlignmentValues.Top, CellBorders.GenerateBorderFull(BorderValues.Single, 14)));
            table.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.5)));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Вид операции", "16", JustificationValues.Center), "0", TableWidthUnitValues.Auto));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Передача", "16", JustificationValues.Center), "0", TableWidthUnitValues.Auto,
                "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorderFull(BorderValues.Single, 14)));
            table.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.5)));

            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), CellGenerate.FormulWidthCell(15.3), TableWidthUnitValues.Dxa));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Номер документа", "16", JustificationValues.Center), CellGenerate.FormulWidthCell(2.1), TableWidthUnitValues.Dxa,
            "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Дата составления", "16", JustificationValues.Center), CellGenerate.FormulWidthCell(2.1), TableWidthUnitValues.Dxa,
            "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorderFull()));
            table2.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(1)));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("НАКЛАДНАЯ", "20", JustificationValues.Right, 1), "0", TableWidthUnitValues.Dxa));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("#"+report.Main.Barcode.Id, "20", JustificationValues.Center), "0", TableWidthUnitValues.Dxa,
            "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorderFull(BorderValues.Single, 14)));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(DateTime.Now.ToString("dd.MM.yyyy"), "20", JustificationValues.Center), "0", TableWidthUnitValues.Dxa,
            "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorderFull(BorderValues.Single, 14)));
            table2.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.5)));

            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), CellGenerate.FormulWidthCell(5.8), TableWidthUnitValues.Dxa));

            paragraphCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart("Отправитель", "16", JustificationValues.Center));
            paragraphCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart("(структурное подразделение)", "16", JustificationValues.Center));
            cellCollection.Add(CellGenerate.GenerateCell(ref paragraphCollection, CellGenerate.FormulWidthCell(8.1), TableWidthUnitValues.Dxa,
            "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            paragraphCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart("Получатель", "16", JustificationValues.Center));
            paragraphCollection.Add(paragraphGenerate.RunParagraphGeneratorStandart("(структурное подразделение)", "16", JustificationValues.Center));
            cellCollection.Add(CellGenerate.GenerateCell(ref paragraphCollection, CellGenerate.FormulWidthCell(8.1), TableWidthUnitValues.Dxa,
            "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));

            table3.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.9)));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Dxa));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(report.Main.Formed.NameOtdel, "16", JustificationValues.Center), "0", TableWidthUnitValues.Dxa,
            "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(report.Main.Received.NameOtdel, "16", JustificationValues.Center), "0", TableWidthUnitValues.Dxa,
            "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            table3.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.5)));

            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), CellGenerate.FormulWidthCell(27.8), TableWidthUnitValues.Dxa, "0",
                "0", TableVerticalAlignmentValues.Bottom, null, 8));
            table4.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.2)));

            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Имя пользователя", "20", JustificationValues.Center, 1), "0", TableWidthUnitValues.Auto,
                "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Имя оборудования", "20", JustificationValues.Center, 1), "0", TableWidthUnitValues.Auto,
                "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Наименование модели", "20", JustificationValues.Center, 1), "0", TableWidthUnitValues.Auto,
                "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Категория", "20", JustificationValues.Center, 1), "0", TableWidthUnitValues.Auto,
    "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Серийный номер", "20", JustificationValues.Center, 1), "0", TableWidthUnitValues.Auto,
                "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Сервисный номер", "20", JustificationValues.Center, 1), "0", TableWidthUnitValues.Auto,
                "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Инвентарный номер", "20", JustificationValues.Center, 1), "0", TableWidthUnitValues.Auto,
                "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Кабинет", "20", JustificationValues.Center, 1), "0", TableWidthUnitValues.Auto,
                "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Наименование компьютера", "20", JustificationValues.Center, 1), "0", TableWidthUnitValues.Auto,
                "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Ip Адрес", "20", JustificationValues.Center, 1), "0", TableWidthUnitValues.Auto,
                "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Примечание", "20", JustificationValues.Center, 1), "0", TableWidthUnitValues.Auto,
                "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            table4.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(1)));
            foreach (var bodyTable in report.Body)
            {
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(bodyTable.Name, "20", JustificationValues.Center), "0", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(bodyTable.NameCategory, "20", JustificationValues.Center), "0", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(bodyTable.Model, "20", JustificationValues.Center), "0", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(bodyTable.Category, "20", JustificationValues.Center), "0", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(bodyTable.SerNum, "20", JustificationValues.Center), "0", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(bodyTable.ServiceNum, "20", JustificationValues.Center), "0", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(bodyTable.InventarNumSysBlok, "20", JustificationValues.Center), "0", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(bodyTable.NumberKabinet, "20", JustificationValues.Center), "0", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(bodyTable.NameComputer, "20", JustificationValues.Center), "0", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(bodyTable.IpAdress, "20", JustificationValues.Center), "0", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(bodyTable.Coment, "20", JustificationValues.Center), "0", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                table4.Append(rows.GenerateRow(ref cellCollection));
            }
            var barcode = paragraphGenerate.RunParagraphGeneratorStandart("", "20", JustificationValues.Center, 0, "1000");
            barcode.Append(driving.AddImageToParagraph(relationshipId));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Отпустил:"), CellGenerate.FormulWidthCell(2.2), TableWidthUnitValues.Dxa));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(report.Main.Formed.NamePosition, "20", JustificationValues.Center), CellGenerate.FormulWidthCell(5.5), TableWidthUnitValues.Dxa,
                "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorder()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), CellGenerate.FormulWidthCell(0.2), TableWidthUnitValues.Dxa));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), CellGenerate.FormulWidthCell(3.1), TableWidthUnitValues.Dxa,
                "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorder()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), CellGenerate.FormulWidthCell(0.2), TableWidthUnitValues.Dxa));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(report.Main.Formed.SmallName, "20", JustificationValues.Center), CellGenerate.FormulWidthCell(7.5), TableWidthUnitValues.Dxa,
                "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorder()));

            table5.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.8)));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("должность", "16", JustificationValues.Center), "0", TableWidthUnitValues.Auto));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("подпись", "16", JustificationValues.Center), "0", TableWidthUnitValues.Auto));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("расшифровка подписи", "16", JustificationValues.Center), "0", TableWidthUnitValues.Auto));
            table5.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.4)));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Получил:"), "0", TableWidthUnitValues.Auto));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(report.Main.Received.ChiefPosition, "20", JustificationValues.Center), "0", TableWidthUnitValues.Auto,
                "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorder()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto,
                "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorder()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(report.Main.Received.ChiefSmallName, "20", JustificationValues.Center), "0", TableWidthUnitValues.Auto,
                "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorder()));
            table5.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.8)));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("должность", "16", JustificationValues.Center), "0", TableWidthUnitValues.Auto));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("подпись", "16", JustificationValues.Center), "0", TableWidthUnitValues.Auto));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("расшифровка подписи", "16", JustificationValues.Center), "0", TableWidthUnitValues.Auto));
            table5.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.4)));
            
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
                ObservableCollection<TableCell> cellCollection = new ObservableCollection<TableCell>();
                var rows = new RowGenerate();
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Фамилия Имя Отчество","26",JustificationValues.Center, 1), CellGenerate.FormulWidthCell(5.2), TableWidthUnitValues.Dxa,"0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Должность", "26", JustificationValues.Center, 1),CellGenerate.FormulWidthCell(5.2), TableWidthUnitValues.Dxa, "0", "0",TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Городской номер", "26",JustificationValues.Center, 1), CellGenerate.FormulWidthCell(6), TableWidthUnitValues.Dxa,"0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Внутренний номер", "26",JustificationValues.Center, 1), CellGenerate.FormulWidthCell(3.5), TableWidthUnitValues.Dxa,"0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Кабинет", "26", JustificationValues.Center, 1),CellGenerate.FormulWidthCell(2.2), TableWidthUnitValues.Dxa, "0", "0",TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                table.Append(rows.GenerateRow(ref cellCollection));
                if (telephoneHelp.TelephonHeaders.Length > 0)
                {
                    foreach (var headers in telephoneHelp.TelephonHeaders)
                    {
                        if (headers.Telephon != null)
                        {
                            if (headers.Name.Contains("Окна"))
                            {
                                cellCollection.Add(CellGenerate.GenerateCell( paragraphGenerate.RunParagraphGeneratorStandart(headers.Name, "26", JustificationValues.Center, 1), "0", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 5));
                                table.Append(rows.GenerateRow(ref cellCollection));
                            }
                            foreach (var telephone in headers.Telephon)
                            {
                                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(telephone.Coment, "26"), "0",TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center,CellBorders.GenerateBorderFull()));
                                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center,CellBorders.GenerateBorderFull()));
                                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(telephone.TelephonUndeground,"26",JustificationValues.Center), "0", TableWidthUnitValues.Auto, "0", "0",TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(telephone.Telephon1, "26",JustificationValues.Center), "0", TableWidthUnitValues.Auto, "0", "0",TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(telephone.Kabinet?.NumberKabinet,"26",JustificationValues.Center), "0", TableWidthUnitValues.Auto, "0", "0",TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                                table.Append(rows.GenerateRow(ref cellCollection));
                            }
                        }
                    }
                }
                if (telephoneHelp.TelephonBody.Length > 0)
                {
                 try
                 {
                     foreach (var telephoneBody in telephoneHelp.TelephonBody)
                     {
                         cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(telephoneBody.NameOtdel,"26", JustificationValues.Center,1), "0", TableWidthUnitValues.Auto, "0", "0",TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 5));
                         table.Append(rows.GenerateRow(ref cellCollection));
                         foreach (var users in telephoneBody.Users)
                         {
                             var strName = users.Name.Split(' ');
                             var para = paragraphGenerate.RunParagraphGeneratorStandart(strName[0] + ' '+ strName[1], "24",JustificationValues.Left,0,"0",true);
                             if (strName.Length > 2)
                             {
                                 para.Append(paragraphGenerate.RunText(strName[2], "24"));
                             }
                             cellCollection.Add(CellGenerate.GenerateCell(para, "0",TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center,CellBorders.GenerateBorderFull()));
                             cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(users.Position.NamePosition, "24",JustificationValues.Left,0,"20"),"0", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center,CellBorders.GenerateBorderFull()));
                             cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(users.Telephon?.TelephonUndeground, "24", JustificationValues.Center), "0", TableWidthUnitValues.Auto, "0", "0",TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                             cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(users.Telephon?.Telephon1, "24", JustificationValues.Center), "0", TableWidthUnitValues.Auto, "0", "0",TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                             cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(
                                 users.Telephon != null ? users.Telephon.Kabinet.NumberKabinet : users.NumberKabinet, "24", JustificationValues.Center), "0",TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center,CellBorders.GenerateBorderFull()));
                             table.Append(rows.GenerateRow(ref cellCollection));
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
        /// Генерация книги учета материальных ценностей
        /// </summary>
        /// <param name="book">Книга учета</param>
        /// <param name="relationshipId">Ссылка на штрих код</param>
        /// <returns></returns>
        public Body BookAccounting(Book book, string relationshipId)
        {
            Body body = new Body();
            Table table = new Table();
            Table table1 = new Table();
            Table table2 = new Table();
            Table table3 = new Table();
            AddDriwing driving = new AddDriwing();

            var paragraphGenerate = new RunGenerate();
            var barcode = paragraphGenerate.RunParagraphGeneratorStandart();
            barcode.Append(driving.AddImageToParagraph(relationshipId));
            ObservableCollection<TableCell> cellCollection = new ObservableCollection<TableCell>();
            var rows = new RowGenerate();
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), CellGenerate.FormulWidthCell(1.9),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Top));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), CellGenerate.FormulWidthCell(2.2),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Top));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), CellGenerate.FormulWidthCell(0.8),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Top));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("КНИГА","22",JustificationValues.Center,1), CellGenerate.FormulWidthCell(7),
                TableWidthUnitValues.Dxa,  "1600", "0", TableVerticalAlignmentValues.Center));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), CellGenerate.FormulWidthCell(2.5),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Top));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), CellGenerate.FormulWidthCell(3.6),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center));
            table.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.5)));
            cellCollection.Add(CellGenerate.GenerateCell(barcode, "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, null, 3,1));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("учета материальных ценностей", "22", JustificationValues.Center, 1), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, null, 2));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("КОДЫ", "16", JustificationValues.Center), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            table.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.5)));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, null, 3,2));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Форма по ОКУД", "16", JustificationValues.Right), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, null, 2));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("0504042", "16", JustificationValues.Center), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(BorderValues.Single, 14)));
            table.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.5)));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, null, 3,2));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Дата открытия", "16", JustificationValues.Right), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, null, 2));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Top, CellBorders.GenerateBorderFull(BorderValues.Single, 14)));
            table.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.5)));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, null, 3,2));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Дата закрытия", "16", JustificationValues.Right), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, null, 2));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Top, CellBorders.GenerateBorderFull(BorderValues.Single, 14)));
            table.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.5)));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Учреждение:", "18"), "0",
                TableWidthUnitValues.Auto));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(book.Organization.NameOrganization, "18"), "0",
                TableWidthUnitValues.Auto,"0","0",TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorder(BorderValues.Single, 14),3));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("по ОКПО", "16", JustificationValues.Right), "0",
                TableWidthUnitValues.Auto));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Top, CellBorders.GenerateBorderFull(BorderValues.Single, 14)));
            table.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.5)));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Структурное подразделение:","18"), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Bottom, null, 2));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(book.Organization.NameDepartament, "18"), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorder(BorderValues.Single, 14), 2));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Top, CellBorders.GenerateBorderFull(BorderValues.Single, 14)));
            table.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.5)));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Материально ответственное лицо:", "18"), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Bottom, null, 3));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(book.Organization.NameFace, "18"), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorder(BorderValues.Single, 14)));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Top, CellBorders.GenerateBorderFull(BorderValues.Single, 14)));
            table.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.5)));

            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Склад","16", JustificationValues.Center), CellGenerate.FormulWidthCell(1.4),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(),0,1));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Стелаж", "16", JustificationValues.Center), CellGenerate.FormulWidthCell(1.4),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 1));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Ячейка", "16", JustificationValues.Center), CellGenerate.FormulWidthCell(1.4),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 1));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Единица измерения", "16", JustificationValues.Center), CellGenerate.FormulWidthCell(3.5),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(),2,1));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Цена", "16", JustificationValues.Center), CellGenerate.FormulWidthCell(2),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 1));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Марка", "16", JustificationValues.Center), CellGenerate.FormulWidthCell(1.3),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 1));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Сорт", "16", JustificationValues.Center), CellGenerate.FormulWidthCell(1.8),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 1));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Профиль", "16", JustificationValues.Center), CellGenerate.FormulWidthCell(1.6),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 1));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Размер", "16", JustificationValues.Center), CellGenerate.FormulWidthCell(1.6),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 1));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Норма запаса", "16",JustificationValues.Center), CellGenerate.FormulWidthCell(2),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 1));
            table1.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.5)));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 2));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 2));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 2));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("наименование", "16", JustificationValues.Center), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("код", "16", JustificationValues.Center), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 2));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 2));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 2));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 2));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 2));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 2));
            table1.Append(rows.GenerateRow(ref cellCollection, true));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            table1.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.5)));

            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Наименование материала:"), CellGenerate.FormulWidthCell(4.5),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(book.BareCodeBook.NameModel), CellGenerate.FormulWidthCell(9.2),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorder(BorderValues.Single, 14)));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("номер кода", "20",JustificationValues.Right), CellGenerate.FormulWidthCell(2.3),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("#"+book.BareCodeBook.Id,"20",JustificationValues.Center), CellGenerate.FormulWidthCell(2),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(BorderValues.Single, 14)));
            table2.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.5)));

            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Порядковый номер записи", "18", JustificationValues.Center), CellGenerate.FormulWidthCell(1.4),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 1));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Дата записи", "18", JustificationValues.Center), CellGenerate.FormulWidthCell(1.4),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 1));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Документ", "18", JustificationValues.Center), CellGenerate.FormulWidthCell(4.2),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 2));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("От кого получено (кому отпущено)", "18", JustificationValues.Center), CellGenerate.FormulWidthCell(4.0),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 1));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Приход", "18", JustificationValues.Center), CellGenerate.FormulWidthCell(1.5),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 1));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Расход", "18", JustificationValues.Center), CellGenerate.FormulWidthCell(1.7),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 1));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Остаток", "16", JustificationValues.Center), CellGenerate.FormulWidthCell(1.6),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 1));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Контроль (подпись дата)", "16", JustificationValues.Center), CellGenerate.FormulWidthCell(2.2),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 1));

            table3.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(1)));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 2));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 2));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("дата", "16", JustificationValues.Center), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("номер", "16", JustificationValues.Center), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 2));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 2));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 2));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 2));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 2));
            table3.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.5)));

            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(DateTime.Now.ToString("dd.MM.yyyy"), "18"), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(book.Organization.Supply?.DaysPostavki, "18", JustificationValues.Center), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Поставка " + book.Organization.Supply?.Years, "18"), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(book.Organization.Position.Length.ToString(),"18", JustificationValues.Center), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(book.Organization.Position.Length.ToString(),"18",JustificationValues.Center), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            table3.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.5)));

            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(),9));
            table3.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.5)));
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
                cellCollection.Add(
                   CellGenerate.GenerateCell(
                       paragraphGenerate.RunParagraphGeneratorStandart(i.ToString(), "18",
                           JustificationValues.Center), "0",
                       TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center,
                       CellBorders.GenerateBorderFull()));
                cellCollection.Add(
                    CellGenerate.GenerateCell(
                        paragraphGenerate.RunParagraphGeneratorStandart(DateTime.Now.ToString("dd.MM.yyyy"), "18"),
                        "0",
                        TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center,
                        CellBorders.GenerateBorderFull()));
                cellCollection.Add(
                    CellGenerate.GenerateCell(
                        paragraphGenerate.RunParagraphGeneratorStandart(position.SerNum, "18",
                            JustificationValues.Center), "0",
                        TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center,
                        CellBorders.GenerateBorderFull(), 2));
                cellCollection.Add(
                    CellGenerate.GenerateCell(
                        paragraphGenerate.RunParagraphGeneratorStandart(position.Users?.SmallName, "18"), "0",
                        TableWidthUnitValues.Auto, "200", "0", TableVerticalAlignmentValues.Center,
                        CellBorders.GenerateBorderFull()));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                    TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center,
                    CellBorders.GenerateBorderFull()));
                cellCollection.Add(
                    CellGenerate.GenerateCell(
                        paragraphGenerate.RunParagraphGeneratorStandart(rashod.ToString(), "18",
                            JustificationValues.Center), "0",
                        TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center,
                        CellBorders.GenerateBorderFull()));
                cellCollection.Add(
                    CellGenerate.GenerateCell(
                        paragraphGenerate.RunParagraphGeneratorStandart(ost.ToString(), "18",
                            JustificationValues.Center), "0",
                        TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center,
                        CellBorders.GenerateBorderFull()));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                    TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center,
                    CellBorders.GenerateBorderFull()));
                table3.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.6)));
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
        /// <summary>
        /// Формирование заявок доступа 
        /// </summary>
        /// <param name="templateotdel">Отдел пользователи шаблоны</param>
        /// <param name="senders">Подписанты документа</param>
        /// <returns></returns>
        public Body GenerateRuleUserTemplate(LibaryXMLAutoModelXmlAuto.OtdelRuleUsers.Otdel templateotdel, LibaryXMLAutoModelXmlAuto.OtdelRuleUsers.SenderUsers senders)
        {
            Body body = new Body();
            var paragraphGenerate = new RunGenerate();
                TableProperties property = new TableProperties()
                {
                    TableJustification = new TableJustification() {Val = TableRowAlignmentValues.Right}
                };
                Table table1 = new Table();
                Table table2 = new Table();
                Table table3 = new Table();
                table1.Append(property);
                var para = paragraphGenerate.RunParagraphGeneratorStandart(
                    "Прошу предоставить указанные ниже права доступа следующим сотрудникам ", "22",
                    JustificationValues.Left, 0, "800", false, false, false);
                para.Append(paragraphGenerate.RunText(templateotdel.RnameOtdel + ":", "22", 3));

                ObservableCollection<TableCell> cellCollection = new ObservableCollection<TableCell>();
                var rows = new RowGenerate();
                cellCollection.Add(CellGenerate.GenerateCell(
                    paragraphGenerate.RunParagraphGeneratorStandart("«УТВЕРЖДАЮ»", "22", JustificationValues.Center),
                    "0", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, null, 5));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(),
                    CellGenerate.FormulWidthCell(2.5), TableWidthUnitValues.Dxa));
                table1.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(1)));
                cellCollection.Add(CellGenerate.GenerateCell(
                    paragraphGenerate.RunParagraphGeneratorStandart(senders?.SenderRukovodstvo.RnameOrganization, "20",
                        JustificationValues.Center), "0", TableWidthUnitValues.Auto, "0", "0",
                    TableVerticalAlignmentValues.Center, null, 5));
                table1.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.9)));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(),
                    CellGenerate.FormulWidthCell(1.3), TableWidthUnitValues.Dxa, "0", "0",
                    TableVerticalAlignmentValues.Center));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(),
                    CellGenerate.FormulWidthCell(3.2), TableWidthUnitValues.Dxa, "0", "0",
                    TableVerticalAlignmentValues.Center, CellBorders.GenerateBorder()));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(),
                    CellGenerate.FormulWidthCell(0.2), TableWidthUnitValues.Dxa, "0", "0",
                    TableVerticalAlignmentValues.Center));
                cellCollection.Add(CellGenerate.GenerateCell(
                    paragraphGenerate.RunParagraphGeneratorStandart(senders?.SenderRukovodstvo.SmallName), "0",
                    TableWidthUnitValues.Auto, "0", "0",
                    TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorder()));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(),
                    CellGenerate.FormulWidthCell(1.3), TableWidthUnitValues.Dxa, "0", "0",
                    TableVerticalAlignmentValues.Center));
                table1.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.7)));
                cellCollection.Add(CellGenerate.GenerateCell(
                    paragraphGenerate.RunParagraphGeneratorStandart(
                        "«____»______________" + DateTime.Now.ToString("yyyy") + "г.", "20",
                        JustificationValues.Center), CellGenerate.FormulWidthCell(1.3), TableWidthUnitValues.Dxa, "0",
                    "0",
                    TableVerticalAlignmentValues.Bottom, null, 5));
                table1.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.8)));

                cellCollection.Add(CellGenerate.GenerateCell(
                    paragraphGenerate.RunParagraphGeneratorStandart("№ п/п", "16", JustificationValues.Center),
                    CellGenerate.FormulWidthCell(0.8), TableWidthUnitValues.Dxa, "200", "200",
                    TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                cellCollection.Add(CellGenerate.GenerateCell(
                    paragraphGenerate.RunParagraphGeneratorStandart("Фамилия, имя, отчество", "16",
                        JustificationValues.Center), CellGenerate.FormulWidthCell(2.2), TableWidthUnitValues.Dxa, "200",
                    "200", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                cellCollection.Add(CellGenerate.GenerateCell(
                    paragraphGenerate.RunParagraphGeneratorStandart("Должность", "16", JustificationValues.Center),
                    CellGenerate.FormulWidthCell(2.5), TableWidthUnitValues.Dxa, "200", "200",
                    TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                cellCollection.Add(CellGenerate.GenerateCell(
                    paragraphGenerate.RunParagraphGeneratorStandart("Учетная запись в ЕСК (с указанием домена)", "16",
                        JustificationValues.Center), CellGenerate.FormulWidthCell(2.7), TableWidthUnitValues.Dxa, "200",
                    "200", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                cellCollection.Add(CellGenerate.GenerateCell(
                    paragraphGenerate.RunParagraphGeneratorStandart(
                        "Обоснование необходимости проведения указанного вида работ в соответствии с должностными обязанностями пользователя (ссылка на раздел должностного регламента или иной нормативный документ)",
                        "16", JustificationValues.Center), CellGenerate.FormulWidthCell(3.1), TableWidthUnitValues.Dxa,
                    "200", "200", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                cellCollection.Add(CellGenerate.GenerateCell(
                    paragraphGenerate.RunParagraphGeneratorStandart("Наименование ресурса (подсистемы)", "16",
                        JustificationValues.Center), CellGenerate.FormulWidthCell(2.4), TableWidthUnitValues.Dxa, "200",
                    "200", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                cellCollection.Add(CellGenerate.GenerateCell(
                    paragraphGenerate.RunParagraphGeneratorStandart(
                        "Перечень прав доступа к функциям подсистем (перечень шаблонов, ролей)", "16",
                        JustificationValues.Center), CellGenerate.FormulWidthCell(6), TableWidthUnitValues.Dxa, "200",
                    "200", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                cellCollection.Add(CellGenerate.GenerateCell(
                    paragraphGenerate.RunParagraphGeneratorStandart("Период действия (постоянно или указать интервал)",
                        "16", JustificationValues.Center), CellGenerate.FormulWidthCell(2.2), TableWidthUnitValues.Dxa,
                    "200", "200", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                cellCollection.Add(CellGenerate.GenerateCell(
                    paragraphGenerate.RunParagraphGeneratorStandart(
                        "Контактная информация (номер телефона, номер комнаты)", "16", JustificationValues.Center),
                    CellGenerate.FormulWidthCell(2.2), TableWidthUnitValues.Dxa, "200", "200",
                    TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                cellCollection.Add(CellGenerate.GenerateCell(
                    paragraphGenerate.RunParagraphGeneratorStandart("Примечание", "16", JustificationValues.Center),
                    CellGenerate.FormulWidthCell(2.8), TableWidthUnitValues.Dxa, "200", "200",
                    TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                table2.Append(rows.GenerateRow(ref cellCollection));
                cellCollection.Add(CellGenerate.GenerateCell(
                    paragraphGenerate.RunParagraphGeneratorStandart("1", "16", JustificationValues.Center), "0",
                    TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center,
                    CellBorders.GenerateBorderFull()));
                cellCollection.Add(CellGenerate.GenerateCell(
                    paragraphGenerate.RunParagraphGeneratorStandart("2", "16", JustificationValues.Center), "0",
                    TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center,
                    CellBorders.GenerateBorderFull()));
                cellCollection.Add(CellGenerate.GenerateCell(
                    paragraphGenerate.RunParagraphGeneratorStandart("3", "16", JustificationValues.Center), "0",
                    TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center,
                    CellBorders.GenerateBorderFull()));
                cellCollection.Add(CellGenerate.GenerateCell(
                    paragraphGenerate.RunParagraphGeneratorStandart("4", "16", JustificationValues.Center), "0",
                    TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center,
                    CellBorders.GenerateBorderFull()));
                cellCollection.Add(CellGenerate.GenerateCell(
                    paragraphGenerate.RunParagraphGeneratorStandart("5", "16", JustificationValues.Center), "0",
                    TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center,
                    CellBorders.GenerateBorderFull()));
                cellCollection.Add(CellGenerate.GenerateCell(
                    paragraphGenerate.RunParagraphGeneratorStandart("6", "16", JustificationValues.Center), "0",
                    TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center,
                    CellBorders.GenerateBorderFull()));
                cellCollection.Add(CellGenerate.GenerateCell(
                    paragraphGenerate.RunParagraphGeneratorStandart("7", "16", JustificationValues.Center), "0",
                    TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center,
                    CellBorders.GenerateBorderFull()));
                cellCollection.Add(CellGenerate.GenerateCell(
                    paragraphGenerate.RunParagraphGeneratorStandart("8", "16", JustificationValues.Center), "0",
                    TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center,
                    CellBorders.GenerateBorderFull()));
                cellCollection.Add(CellGenerate.GenerateCell(
                    paragraphGenerate.RunParagraphGeneratorStandart("9", "16", JustificationValues.Center), "0",
                    TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center,
                    CellBorders.GenerateBorderFull()));
                cellCollection.Add(CellGenerate.GenerateCell(
                    paragraphGenerate.RunParagraphGeneratorStandart("10", "16", JustificationValues.Center), "0",
                    TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center,
                    CellBorders.GenerateBorderFull()));
                table2.Append(rows.GenerateRow(ref cellCollection));
                int i = 1;
                foreach (var user in templateotdel.Users)
                {
                    cellCollection.Add(CellGenerate.GenerateCell(
                        paragraphGenerate.RunParagraphGeneratorStandart(i.ToString(), "16", JustificationValues.Center),
                        "0", TableWidthUnitValues.Auto, "50", "0", TableVerticalAlignmentValues.Center,
                        CellBorders.GenerateBorderFull()));
                    cellCollection.Add(CellGenerate.GenerateCell(
                        paragraphGenerate.RunParagraphGeneratorStandart(user.Name, "16"), "0",
                        TableWidthUnitValues.Auto, "50", "0", TableVerticalAlignmentValues.Center,
                        CellBorders.GenerateBorderFull()));
                    cellCollection.Add(CellGenerate.GenerateCell(
                        paragraphGenerate.RunParagraphGeneratorStandart(user.NamePosition, "16"), "0",
                        TableWidthUnitValues.Auto, "50", "0", TableVerticalAlignmentValues.Center,
                        CellBorders.GenerateBorderFull()));
                    cellCollection.Add(CellGenerate.GenerateCell(
                        paragraphGenerate.RunParagraphGeneratorStandart(user.Tabel, "16"), "0",
                        TableWidthUnitValues.Auto, "50", "0", TableVerticalAlignmentValues.Center,
                        CellBorders.GenerateBorderFull()));
                    cellCollection.Add(CellGenerate.GenerateCell(
                        paragraphGenerate.RunParagraphGeneratorStandart(
                            "Выполнение должностных обязанностей в соответствии с п.3 Должностного регламента", "16"),
                        "0", TableWidthUnitValues.Auto, "50", "0", TableVerticalAlignmentValues.Center,
                        CellBorders.GenerateBorderFull()));
                    cellCollection.Add(CellGenerate.GenerateCell(
                        paragraphGenerate.RunParagraphGeneratorStandart("АИС Налог-3", "16",
                            JustificationValues.Center), "0", TableWidthUnitValues.Auto, "50", "0",
                        TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                    cellCollection.Add(CellGenerate.GenerateCell(
                        paragraphGenerate.RunParagraphGeneratorStandart(user.RuleTemplate, "16"), "0",
                        TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center,
                        CellBorders.GenerateBorderFull()));
                    cellCollection.Add(CellGenerate.GenerateCell(
                        paragraphGenerate.RunParagraphGeneratorStandart("постоянно", "16", JustificationValues.Center),
                        "0", TableWidthUnitValues.Auto, "50", "0", TableVerticalAlignmentValues.Center,
                        CellBorders.GenerateBorderFull()));
                    cellCollection.Add(CellGenerate.GenerateCell(
                        paragraphGenerate.RunParagraphGeneratorStandart($"{user.Telephon} {user.NumberKabinet}", "16",
                            JustificationValues.Center), "0", TableWidthUnitValues.Auto, "50", "0",
                        TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                    cellCollection.Add(CellGenerate.GenerateCell(
                        paragraphGenerate.RunParagraphGeneratorStandart(user.Pushed + " " + user.IpAdress, "16",
                            JustificationValues.Center), "0", TableWidthUnitValues.Auto, "50", "0",
                        TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                    table2.Append(rows.GenerateRow(ref cellCollection));
                    i++;
                }

                ///Начальник отдела на кого формируем
                cellCollection.Add(CellGenerate.GenerateCell(
                    paragraphGenerate.RunParagraphGeneratorStandart("Начальник " + templateotdel.RnameOtdel, "18",
                        JustificationValues.Left, 3), "0", TableWidthUnitValues.Auto, "0", "0",
                    TableVerticalAlignmentValues.Center, null, 11));
                table3.Append(rows.GenerateRow(ref cellCollection));
                cellCollection.Add(CellGenerate.GenerateCell(
                    paragraphGenerate.RunParagraphGeneratorStandart("«", "18"), "0", TableWidthUnitValues.Auto));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(),
                    CellGenerate.FormulWidthCell(0.7), TableWidthUnitValues.Dxa, "0", "0",
                    TableVerticalAlignmentValues.Center, CellBorders.GenerateBorder()));
                cellCollection.Add(CellGenerate.GenerateCell(
                    paragraphGenerate.RunParagraphGeneratorStandart("»", "18"), "0", TableWidthUnitValues.Auto));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(),
                    CellGenerate.FormulWidthCell(3.4), TableWidthUnitValues.Dxa, "0", "0",
                    TableVerticalAlignmentValues.Center, CellBorders.GenerateBorder()));
                cellCollection.Add(CellGenerate.GenerateCell(
                    paragraphGenerate.RunParagraphGeneratorStandart(DateTime.Now.ToString("yyyy") + "г", "18"), "0",
                    TableWidthUnitValues.Auto));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(),
                    CellGenerate.FormulWidthCell(0.4), TableWidthUnitValues.Dxa));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(),
                    CellGenerate.FormulWidthCell(3.2), TableWidthUnitValues.Dxa, "0", "0",
                    TableVerticalAlignmentValues.Center, CellBorders.GenerateBorder()));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(),
                    CellGenerate.FormulWidthCell(0.4), TableWidthUnitValues.Dxa));
                cellCollection.Add(CellGenerate.GenerateCell(
                    paragraphGenerate.RunParagraphGeneratorStandart(templateotdel.SmallName, "18",
                        JustificationValues.Center), CellGenerate.FormulWidthCell(3.2), TableWidthUnitValues.Auto, "0",
                    "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorder()));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(),
                    CellGenerate.FormulWidthCell(0.2), TableWidthUnitValues.Dxa));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(),
                    CellGenerate.FormulWidthCell(7.5), TableWidthUnitValues.Auto, "0", "0",
                    TableVerticalAlignmentValues.Center, CellBorders.GenerateBorder()));
                table3.Append(rows.GenerateRow(ref cellCollection));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                    TableWidthUnitValues.Auto));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                    TableWidthUnitValues.Auto));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                    TableWidthUnitValues.Auto));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                    TableWidthUnitValues.Auto));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                    TableWidthUnitValues.Auto));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                    TableWidthUnitValues.Auto));
                cellCollection.Add(CellGenerate.GenerateCell(
                    paragraphGenerate.RunParagraphGeneratorStandart("подпись", "16", JustificationValues.Center), "0",
                    TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Top));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                    TableWidthUnitValues.Auto));
                cellCollection.Add(CellGenerate.GenerateCell(
                    paragraphGenerate.RunParagraphGeneratorStandart("Фамилия, инициалы", "16",
                        JustificationValues.Center), "0", TableWidthUnitValues.Auto, "0", "0",
                    TableVerticalAlignmentValues.Top));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                    TableWidthUnitValues.Auto));
                cellCollection.Add(CellGenerate.GenerateCell(
                    paragraphGenerate.RunParagraphGeneratorStandart("контактный телефон, адрес электронной почты", "16",
                        JustificationValues.Center), "0", TableWidthUnitValues.Auto, "0", "0",
                    TableVerticalAlignmentValues.Top));
                table3.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.3)));

                ///Согласовано
                cellCollection.Add(CellGenerate.GenerateCell(
                    paragraphGenerate.RunParagraphGeneratorStandart("Согласовано:", "18", JustificationValues.Left, 1),
                    "0", TableWidthUnitValues.Auto, "600", "0", TableVerticalAlignmentValues.Top, null, 11));
                table3.Append(rows.GenerateRow(ref cellCollection));

                ///Начальник отдела Безопасности
                if (senders?.Security != null)
                {
                    cellCollection.Add(CellGenerate.GenerateCell(
                        paragraphGenerate.RunParagraphGeneratorStandart("Начальник " + senders.Security.RnameOtdel,
                            "18", JustificationValues.Left, 3), "0", TableWidthUnitValues.Auto, "0", "0",
                        TableVerticalAlignmentValues.Bottom, null, 11));
                    table3.Append(rows.GenerateRow(ref cellCollection));
                    cellCollection.Add(CellGenerate.GenerateCell(
                        paragraphGenerate.RunParagraphGeneratorStandart("«", "18"), "0", TableWidthUnitValues.Auto));
                    cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(),
                        CellGenerate.FormulWidthCell(0.7), TableWidthUnitValues.Dxa, "0", "0",
                        TableVerticalAlignmentValues.Center, CellBorders.GenerateBorder()));
                    cellCollection.Add(CellGenerate.GenerateCell(
                        paragraphGenerate.RunParagraphGeneratorStandart("»", "18"), "0", TableWidthUnitValues.Auto));
                    cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(),
                        CellGenerate.FormulWidthCell(3.4), TableWidthUnitValues.Dxa, "0", "0",
                        TableVerticalAlignmentValues.Center, CellBorders.GenerateBorder()));
                    cellCollection.Add(CellGenerate.GenerateCell(
                        paragraphGenerate.RunParagraphGeneratorStandart(DateTime.Now.ToString("yyyy") + "г", "18"), "0",
                        TableWidthUnitValues.Auto));
                    cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(),
                        CellGenerate.FormulWidthCell(0.4), TableWidthUnitValues.Dxa));
                    cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(),
                        CellGenerate.FormulWidthCell(3.2), TableWidthUnitValues.Dxa, "0", "0",
                        TableVerticalAlignmentValues.Center, CellBorders.GenerateBorder()));
                    cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(),
                        CellGenerate.FormulWidthCell(0.4), TableWidthUnitValues.Dxa));
                    cellCollection.Add(CellGenerate.GenerateCell(
                        paragraphGenerate.RunParagraphGeneratorStandart(senders.Security.SmallName, "18",
                            JustificationValues.Center), CellGenerate.FormulWidthCell(3.2), TableWidthUnitValues.Auto,
                        "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorder()));
                    table3.Append(rows.GenerateRow(ref cellCollection));
                    cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(),
                        "0", TableWidthUnitValues.Auto));
                    cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(),
                        "0", TableWidthUnitValues.Auto));
                    cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(),
                        "0", TableWidthUnitValues.Auto));
                    cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(),
                        "0", TableWidthUnitValues.Auto));
                    cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(),
                        "0", TableWidthUnitValues.Auto));
                    cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(),
                        "0", TableWidthUnitValues.Auto));
                    cellCollection.Add(CellGenerate.GenerateCell(
                        paragraphGenerate.RunParagraphGeneratorStandart("подпись", "16", JustificationValues.Center),
                        "0", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Top));
                    cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(),
                        "0", TableWidthUnitValues.Auto));
                    cellCollection.Add(CellGenerate.GenerateCell(
                        paragraphGenerate.RunParagraphGeneratorStandart("Фамилия, инициалы", "16",
                            JustificationValues.Center), "0", TableWidthUnitValues.Auto, "0", "0",
                        TableVerticalAlignmentValues.Top));
                    table3.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.6)));
                }

                ///Начальник отдела Информатизации
                if (senders?.ItOtdel != null)
                {
                    cellCollection.Add(CellGenerate.GenerateCell(
                        paragraphGenerate.RunParagraphGeneratorStandart(senders.ItOtdel.NamePosition, "18",
                            JustificationValues.Left, 3), "0", TableWidthUnitValues.Auto, "0", "0",
                        TableVerticalAlignmentValues.Bottom, null, 11));
                    table3.Append(rows.GenerateRow(ref cellCollection));
                    cellCollection.Add(CellGenerate.GenerateCell(
                        paragraphGenerate.RunParagraphGeneratorStandart("«", "18"), "0", TableWidthUnitValues.Auto));
                    cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(),
                        CellGenerate.FormulWidthCell(0.7), TableWidthUnitValues.Dxa, "0", "0",
                        TableVerticalAlignmentValues.Center, CellBorders.GenerateBorder()));
                    cellCollection.Add(CellGenerate.GenerateCell(
                        paragraphGenerate.RunParagraphGeneratorStandart("»", "18"), "0", TableWidthUnitValues.Auto));
                    cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(),
                        CellGenerate.FormulWidthCell(3.4), TableWidthUnitValues.Dxa, "0", "0",
                        TableVerticalAlignmentValues.Center, CellBorders.GenerateBorder()));
                    cellCollection.Add(CellGenerate.GenerateCell(
                        paragraphGenerate.RunParagraphGeneratorStandart(DateTime.Now.ToString("yyyy") + "г", "18"), "0",
                        TableWidthUnitValues.Auto));
                    cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(),
                        CellGenerate.FormulWidthCell(0.4), TableWidthUnitValues.Dxa));
                    cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(),
                        CellGenerate.FormulWidthCell(3.2), TableWidthUnitValues.Dxa, "0", "0",
                        TableVerticalAlignmentValues.Center, CellBorders.GenerateBorder()));
                    cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(),
                        CellGenerate.FormulWidthCell(0.4), TableWidthUnitValues.Dxa));
                    cellCollection.Add(CellGenerate.GenerateCell(
                        paragraphGenerate.RunParagraphGeneratorStandart(senders.ItOtdel.SmallName, "18",
                            JustificationValues.Center), CellGenerate.FormulWidthCell(3.2), TableWidthUnitValues.Auto,
                        "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorder()));
                    table3.Append(rows.GenerateRow(ref cellCollection));
                    cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(),
                        "0", TableWidthUnitValues.Auto));
                    cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(),
                        "0", TableWidthUnitValues.Auto));
                    cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(),
                        "0", TableWidthUnitValues.Auto));
                    cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(),
                        "0", TableWidthUnitValues.Auto));
                    cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(),
                        "0", TableWidthUnitValues.Auto));
                    cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(),
                        "0", TableWidthUnitValues.Auto));
                    cellCollection.Add(CellGenerate.GenerateCell(
                        paragraphGenerate.RunParagraphGeneratorStandart("подпись", "16", JustificationValues.Center),
                        "0", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Top));
                    cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(),
                        "0", TableWidthUnitValues.Auto));
                    cellCollection.Add(CellGenerate.GenerateCell(
                        paragraphGenerate.RunParagraphGeneratorStandart("Фамилия, инициалы", "16",
                            JustificationValues.Center), "0", TableWidthUnitValues.Auto, "0", "0",
                        TableVerticalAlignmentValues.Top));
                    table3.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.3)));
                }

                ///Исполнитель
                cellCollection.Add(CellGenerate.GenerateCell(
                    paragraphGenerate.RunParagraphGeneratorStandart("Исполнено:", "18", JustificationValues.Left, 3),
                    "0", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Bottom, null, 11));
                table3.Append(rows.GenerateRow(ref cellCollection));
                cellCollection.Add(CellGenerate.GenerateCell(
                    paragraphGenerate.RunParagraphGeneratorStandart("«", "18"), "0", TableWidthUnitValues.Auto));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(),
                    CellGenerate.FormulWidthCell(0.7), TableWidthUnitValues.Dxa, "0", "0",
                    TableVerticalAlignmentValues.Center, CellBorders.GenerateBorder()));
                cellCollection.Add(CellGenerate.GenerateCell(
                    paragraphGenerate.RunParagraphGeneratorStandart("»", "18"), "0", TableWidthUnitValues.Auto));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(),
                    CellGenerate.FormulWidthCell(3.4), TableWidthUnitValues.Dxa, "0", "0",
                    TableVerticalAlignmentValues.Center, CellBorders.GenerateBorder()));
                cellCollection.Add(CellGenerate.GenerateCell(
                    paragraphGenerate.RunParagraphGeneratorStandart(DateTime.Now.ToString("yyyy") + "г", "18"), "0",
                    TableWidthUnitValues.Auto));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(),
                    CellGenerate.FormulWidthCell(0.4), TableWidthUnitValues.Dxa));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(),
                    CellGenerate.FormulWidthCell(3.2), TableWidthUnitValues.Dxa, "0", "0",
                    TableVerticalAlignmentValues.Center, CellBorders.GenerateBorder()));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(),
                    CellGenerate.FormulWidthCell(0.4), TableWidthUnitValues.Dxa));
                cellCollection.Add(CellGenerate.GenerateCell(
                    paragraphGenerate.RunParagraphGeneratorStandart("", "18", JustificationValues.Center),
                    CellGenerate.FormulWidthCell(3.2), TableWidthUnitValues.Auto, "0", "0",
                    TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorder()));
                table3.Append(rows.GenerateRow(ref cellCollection));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                    TableWidthUnitValues.Auto));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                    TableWidthUnitValues.Auto));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                    TableWidthUnitValues.Auto));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                    TableWidthUnitValues.Auto));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                    TableWidthUnitValues.Auto));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                    TableWidthUnitValues.Auto));
                cellCollection.Add(CellGenerate.GenerateCell(
                    paragraphGenerate.RunParagraphGeneratorStandart("подпись", "16", JustificationValues.Center), "0",
                    TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Top));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                    TableWidthUnitValues.Auto));
                cellCollection.Add(CellGenerate.GenerateCell(
                    paragraphGenerate.RunParagraphGeneratorStandart("Фамилия, инициалы", "16",
                        JustificationValues.Center), "0", TableWidthUnitValues.Auto, "0", "0",
                    TableVerticalAlignmentValues.Top));
                table3.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.3)));

                var para1 =
                    paragraphGenerate.RunParagraphGeneratorStandart("Заявка* ", "20", JustificationValues.Center, 1);
                para1.Append(paragraphGenerate.RunText(templateotdel.Number, "20", 3));
                body.Append(paragraphGenerate.RunParagraphGeneratorStandart("Приложение 2", "24",
                    JustificationValues.Right, 1));
                body.Append(table1);
                body.Append(paragraphGenerate.RunParagraphGeneratorStandart(" ", "10"));
                body.Append(para1);
                body.Append(paragraphGenerate.RunParagraphGeneratorStandart(
                    "на предоставление (изменение прав) доступа к ресурсам ФНС России федерального уровня", "18",
                    JustificationValues.Center, 1, "0", false, false, false));
                body.Append(para);
                body.Append(table2);
                body.Append(paragraphGenerate.RunParagraphGeneratorStandart());
                body.Append(table3);
                return body;
        }

        /// <summary>
        /// Подготовка наклеек для техники
        /// </summary>
        /// <param name="report">Техника из БД</param>
        /// <param name="relationshipId">Ссылка на сгенерированый QR Code</param>
        /// <returns></returns>
        public Body Sticker(AllTechnic report, string relationshipId)
        {
            AddDriwing driving = new AddDriwing();
            var paragraphGenerate = new RunGenerate();
            Table table = new Table();
            ObservableCollection<TableCell> cellCollection = new ObservableCollection<TableCell>();
            var rows = new RowGenerate();
            Body body = new Body();
            var barcode = paragraphGenerate.RunParagraphGeneratorStandart("");
            barcode.Append(driving.AddImageToParagraph(relationshipId, 800000L, 800000L));

            cellCollection.Add(CellGenerate.GenerateCell(barcode, "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.SelectGenerateBorder(CellBorders.SelectTableCellBorders.LeftBorderOrTopBorder), 0, 1));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart($"{report.Item}: {report.NameManufacturer} {report.NameModel}", "20", JustificationValues.Left, 1), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.SelectGenerateBorder(CellBorders.SelectTableCellBorders.RightBorderOrTopBorder), 2));
            table.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.5)));

            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.SelectGenerateBorder(CellBorders.SelectTableCellBorders.LeftBorder), 0, 2));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart($"s/n: {report.SerNum}", "20", JustificationValues.Left, 1), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.SelectGenerateBorder(CellBorders.SelectTableCellBorders.RightBorder), 2));
            table.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.5)));
            
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.SelectGenerateBorder(CellBorders.SelectTableCellBorders.LeftBorder), 0, 2));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart($"Инв.: {report.InventarNum}", "20", JustificationValues.Left, 1), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.SelectGenerateBorder(CellBorders.SelectTableCellBorders.RightBorder), 2));
            table.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.5)));
           
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.SelectGenerateBorder(CellBorders.SelectTableCellBorders.LeftBorder), 0, 2));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart($"Kaб.: {report.NumberKabinet}", "20", JustificationValues.Left, 1), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.SelectGenerateBorder(CellBorders.SelectTableCellBorders.RightBorder), 2));
            table.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.5)));
           
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.SelectGenerateBorder(CellBorders.SelectTableCellBorders.LeftBorderOrBottomBorder), 0, 2));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart($"User: {report.Users}", "20", JustificationValues.Left, 1), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.SelectGenerateBorder(CellBorders.SelectTableCellBorders.RightBorderOrBottomBorder), 2));
            table.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.5)));

            body.Append(table);
            return body;
        }
    }
}

