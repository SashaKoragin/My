using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using EfDatabase.Inventory.Base;
using EfDatabase.MemoReport;
using EfDatabase.XsdBookAccounting;
using EfDatabaseTelephoneHelp;
using EfDatabaseXsdQrCodeModel;
using LibaryDocumentGenerator.ProgrammView.Word.Libary.Drawing;
using LibaryDocumentGenerator.ProgrammView.Word.Libary.ParagraphsGenerator;
using LibaryDocumentGenerator.ProgrammView.Word.Libary.TablesGenrerator;
using Organization = EfDatabase.Inventory.Base.Organization;

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
                    "0", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, null, 9));
                table1.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(1)));
                cellCollection.Add(CellGenerate.GenerateCell(
                    paragraphGenerate.RunParagraphGeneratorStandart(senders?.SenderRukovodstvo.RnameOrganization, "20",
                        JustificationValues.Center), "0", TableWidthUnitValues.Auto, "0", "0",
                    TableVerticalAlignmentValues.Center, null, 9));
                table1.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.9)));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(),
                    CellGenerate.FormulWidthCell(1.1), TableWidthUnitValues.Dxa, "0", "0",
                    TableVerticalAlignmentValues.Center));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(),
                    CellGenerate.FormulWidthCell(0.2), TableWidthUnitValues.Dxa, "0", "0",
                    TableVerticalAlignmentValues.Center));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(),
                    CellGenerate.FormulWidthCell(0.7), TableWidthUnitValues.Dxa, "0", "0",
                    TableVerticalAlignmentValues.Center, CellBorders.GenerateBorder()));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(),
                    CellGenerate.FormulWidthCell(0.2), TableWidthUnitValues.Dxa, "0", "0",
                    TableVerticalAlignmentValues.Center, CellBorders.GenerateBorder()));
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
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("«", "18"), "0", TableWidthUnitValues.Auto));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(templateotdel.DateStatement?.ToString("dd"), "20", JustificationValues.Center),
                         "0", TableWidthUnitValues.Auto, "0", "0",
                              TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorder()));
                cellCollection.Add(CellGenerate.GenerateCell( paragraphGenerate.RunParagraphGeneratorStandart("»", "18"), "0", TableWidthUnitValues.Auto));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(CultureInfo.CreateSpecificCulture("ru-Ru").DateTimeFormat.MonthGenitiveNames[templateotdel.DateStatement.Value.Month - 1].ToLower(), "20", JustificationValues.Center),
                         "0", TableWidthUnitValues.Auto, "0", "0",
                              TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorder()));
                cellCollection.Add(CellGenerate.GenerateCell( paragraphGenerate.RunParagraphGeneratorStandart(templateotdel.DateStatement?.ToString("yyyy") + "г", "18"), 
                          "0", TableWidthUnitValues.Auto,"0","0",TableVerticalAlignmentValues.Bottom,null,2));

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
                            templateotdel.Regulations, "16"),
                        "0", TableWidthUnitValues.Auto, "50", "0", TableVerticalAlignmentValues.Center,
                        CellBorders.GenerateBorderFull()));
                    cellCollection.Add(CellGenerate.GenerateCell(
                        paragraphGenerate.RunParagraphGeneratorStandart("АИС Налог-3 ПРОМ", "16",
                            JustificationValues.Center), "0", TableWidthUnitValues.Auto, "50", "0",
                        TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));

                var strName = user.RuleTemplate.Split(',');
                var breaks = true;
                var paraTemplate = paragraphGenerate.RunParagraphGeneratorStandart();
                foreach (var s in strName)
                {
                    if (strName.Last() == s)
                    {
                        breaks = false;
                    }
                    paraTemplate.Append(paragraphGenerate.RunText(s, "16",0, breaks));
                }

                cellCollection.Add(CellGenerate.GenerateCell(
                        paraTemplate, "0",
                        TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center,
                        CellBorders.GenerateBorderFull()));
                    cellCollection.Add(CellGenerate.GenerateCell(
                        paragraphGenerate.RunParagraphGeneratorStandart("постоянно", "16", JustificationValues.Center),
                        "0", TableWidthUnitValues.Auto, "50", "0", TableVerticalAlignmentValues.Center,
                        CellBorders.GenerateBorderFull()));
                    cellCollection.Add(CellGenerate.GenerateCell(
                        paragraphGenerate.RunParagraphGeneratorStandart($"{user.Telephon}, {user.NumberKabinet}", "16",
                            JustificationValues.Center), "0", TableWidthUnitValues.Auto, "50", "0",
                        TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                    cellCollection.Add(CellGenerate.GenerateCell(
                        paragraphGenerate.RunParagraphGeneratorStandart(user.Pushed + " " + user.IpAdress, "16",
                            JustificationValues.Center), "0", TableWidthUnitValues.Auto, "50", "0",
                        TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
                    table2.Append(rows.GenerateRow(ref cellCollection));
                    i++;
                }

                //Начальник отдела на кого формируем
                cellCollection.Add(CellGenerate.GenerateCell(
                    paragraphGenerate.RunParagraphGeneratorStandart("Начальник " + templateotdel.RnameOtdel, "18",
                        JustificationValues.Left, 3), "0", TableWidthUnitValues.Auto, "0", "0",
                    TableVerticalAlignmentValues.Center, null, 11));
                table3.Append(rows.GenerateRow(ref cellCollection));
                cellCollection.Add(CellGenerate.GenerateCell(
                    paragraphGenerate.RunParagraphGeneratorStandart("«", "18"), "0", TableWidthUnitValues.Auto));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(templateotdel.DateStatement?.ToString("dd"), "20", JustificationValues.Center),
                    CellGenerate.FormulWidthCell(0.7), TableWidthUnitValues.Dxa, "0", "0",
                    TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorder()));
                cellCollection.Add(CellGenerate.GenerateCell(
                    paragraphGenerate.RunParagraphGeneratorStandart("»", "18"), "0", TableWidthUnitValues.Auto));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(CultureInfo.CreateSpecificCulture("ru-Ru").DateTimeFormat.MonthGenitiveNames[templateotdel.DateStatement.Value.Month - 1].ToLower(), "20", JustificationValues.Center),
                    CellGenerate.FormulWidthCell(3.4), TableWidthUnitValues.Dxa, "0", "0",
                    TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorder()));
                cellCollection.Add(CellGenerate.GenerateCell(
                    paragraphGenerate.RunParagraphGeneratorStandart(templateotdel.DateStatement?.ToString("yyyy") + "г", "18"), "0",
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
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(templateotdel.Contact, "18",
                        JustificationValues.Center, 3), //Телефон начальника отдела
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

                //Согласовано
                cellCollection.Add(CellGenerate.GenerateCell(
                    paragraphGenerate.RunParagraphGeneratorStandart("Согласовано:", "18", JustificationValues.Left, 1),
                    "0", TableWidthUnitValues.Auto, "600", "0", TableVerticalAlignmentValues.Top, null, 11));
                table3.Append(rows.GenerateRow(ref cellCollection));

            //Начальник отдела отдела информационной безопасности
            if (senders?.Security != null)
            {
                cellCollection.Add(CellGenerate.GenerateCell(
                    paragraphGenerate.RunParagraphGeneratorStandart(senders.Security.RnameOtdel,
                        "18", JustificationValues.Left, 3), "0", TableWidthUnitValues.Auto, "0", "0",
                    TableVerticalAlignmentValues.Bottom, null, 11));
                table3.Append(rows.GenerateRow(ref cellCollection));
                cellCollection.Add(CellGenerate.GenerateCell(
                    paragraphGenerate.RunParagraphGeneratorStandart("«", "18"), "0", TableWidthUnitValues.Auto));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(templateotdel.DateStatement?.ToString("dd"), "20", JustificationValues.Center),
                    CellGenerate.FormulWidthCell(0.7), TableWidthUnitValues.Dxa, "0", "0",
                    TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorder()));
                cellCollection.Add(CellGenerate.GenerateCell(
                    paragraphGenerate.RunParagraphGeneratorStandart("»", "18"), "0", TableWidthUnitValues.Auto));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(CultureInfo.CreateSpecificCulture("ru-Ru").DateTimeFormat.MonthGenitiveNames[templateotdel.DateStatement.Value.Month - 1].ToLower(), "20", JustificationValues.Center),
                    CellGenerate.FormulWidthCell(3.4), TableWidthUnitValues.Dxa, "0", "0",
                    TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorder()));
                cellCollection.Add(CellGenerate.GenerateCell(
                    paragraphGenerate.RunParagraphGeneratorStandart(templateotdel.DateStatement?.ToString("yyyy") + "г", "18"), "0",
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

            //Начальник отдела Информатизации
            //if (senders?.ItOtdel != null)
            //{
            //    cellCollection.Add(CellGenerate.GenerateCell(
            //        paragraphGenerate.RunParagraphGeneratorStandart(senders.ItOtdel.NamePosition, "18",
            //            JustificationValues.Left, 3), "0", TableWidthUnitValues.Auto, "0", "0",
            //        TableVerticalAlignmentValues.Bottom, null, 11));
            //    table3.Append(rows.GenerateRow(ref cellCollection));
            //    cellCollection.Add(CellGenerate.GenerateCell(
            //        paragraphGenerate.RunParagraphGeneratorStandart("«", "18"), "0", TableWidthUnitValues.Auto));
            //    cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(templateotdel.Dates?.ToString("dd"), "20", JustificationValues.Center),
            //        CellGenerate.FormulWidthCell(0.7), TableWidthUnitValues.Dxa, "0", "0",
            //        TableVerticalAlignmentValues.Center, CellBorders.GenerateBorder()));
            //    cellCollection.Add(CellGenerate.GenerateCell(
            //        paragraphGenerate.RunParagraphGeneratorStandart("»", "18"), "0", TableWidthUnitValues.Auto));
            //    cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(templateotdel.Dates?.ToString("MM"), "20", JustificationValues.Center),
            //        CellGenerate.FormulWidthCell(3.4), TableWidthUnitValues.Dxa, "0", "0",
            //        TableVerticalAlignmentValues.Center, CellBorders.GenerateBorder()));
            //    cellCollection.Add(CellGenerate.GenerateCell(
            //        paragraphGenerate.RunParagraphGeneratorStandart(templateotdel.Dates?.ToString("yyyy") + "г", "18"), "0",
            //        TableWidthUnitValues.Auto));
            //    cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(),
            //        CellGenerate.FormulWidthCell(0.4), TableWidthUnitValues.Dxa));
            //    cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(),
            //        CellGenerate.FormulWidthCell(3.2), TableWidthUnitValues.Dxa, "0", "0",
            //        TableVerticalAlignmentValues.Center, CellBorders.GenerateBorder()));
            //    cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(),
            //        CellGenerate.FormulWidthCell(0.4), TableWidthUnitValues.Dxa));
            //    cellCollection.Add(CellGenerate.GenerateCell(
            //        paragraphGenerate.RunParagraphGeneratorStandart(senders.ItOtdel.SmallName, "18",
            //            JustificationValues.Center), CellGenerate.FormulWidthCell(3.2), TableWidthUnitValues.Auto,
            //        "0", "0", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorder()));
            //    table3.Append(rows.GenerateRow(ref cellCollection));
            //    cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(),
            //        "0", TableWidthUnitValues.Auto));
            //    cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(),
            //        "0", TableWidthUnitValues.Auto));
            //    cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(),
            //        "0", TableWidthUnitValues.Auto));
            //    cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(),
            //        "0", TableWidthUnitValues.Auto));
            //    cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(),
            //        "0", TableWidthUnitValues.Auto));
            //    cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(),
            //        "0", TableWidthUnitValues.Auto));
            //    cellCollection.Add(CellGenerate.GenerateCell(
            //        paragraphGenerate.RunParagraphGeneratorStandart("подпись", "16", JustificationValues.Center),
            //        "0", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Top));
            //    cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(),
            //        "0", TableWidthUnitValues.Auto));
            //    cellCollection.Add(CellGenerate.GenerateCell(
            //        paragraphGenerate.RunParagraphGeneratorStandart("Фамилия, инициалы", "16",
            //            JustificationValues.Center), "0", TableWidthUnitValues.Auto, "0", "0",
            //        TableVerticalAlignmentValues.Top));
            //    table3.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.3)));
            //}

            //Исполнитель
            cellCollection.Add(CellGenerate.GenerateCell( paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Bottom, null, 11));
            table3.Append(rows.GenerateRow(ref cellCollection));
            cellCollection.Add(CellGenerate.GenerateCell(
                paragraphGenerate.RunParagraphGeneratorStandart("Исполнено отделом информационной безопасности:", "18", JustificationValues.Left, 3),
                "0", TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Bottom, null, 11));
            table3.Append(rows.GenerateRow(ref cellCollection));
            cellCollection.Add(CellGenerate.GenerateCell(
                paragraphGenerate.RunParagraphGeneratorStandart("«", "18"), "0", TableWidthUnitValues.Auto));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(templateotdel.Dates?.ToString("dd"), "20", JustificationValues.Center),
                CellGenerate.FormulWidthCell(0.7), TableWidthUnitValues.Dxa, "0", "0",
                TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorder()));
            cellCollection.Add(CellGenerate.GenerateCell(
                paragraphGenerate.RunParagraphGeneratorStandart("»", "18"), "0", TableWidthUnitValues.Auto));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(CultureInfo.CreateSpecificCulture("ru-Ru").DateTimeFormat.MonthGenitiveNames[templateotdel.Dates.Value.Month - 1].ToLower(), "20",JustificationValues.Center),
                CellGenerate.FormulWidthCell(3.4), TableWidthUnitValues.Dxa, "0", "0",
                TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorder()));
            cellCollection.Add(CellGenerate.GenerateCell(
                paragraphGenerate.RunParagraphGeneratorStandart(templateotdel.Dates?.ToString("yyyy") + "г", "18"), "0",
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
        /// <param name="mainDocumentPart">Параграф</param>
        /// <param name="countSkip">Генерация столбиков Количество сбросов</param>
        /// <returns></returns>
        public Body Sticker(List<AllTechnic> report, MainDocumentPart mainDocumentPart, int countSkip = 4)
        {
            AddDriwing driving = new AddDriwing();
            var skip = 0;
            ObservableCollection<TableCell> collectionAppendTable;
            var paragraphGenerate = new RunGenerate();
            Table table = new Table();
            
            ObservableCollection<TableCell> cellCollection = new ObservableCollection<TableCell>();
            var rows = new RowGenerate();
            Body body = new Body();
            foreach (var allTechnical in report)
            {
                ImagePart image = mainDocumentPart.AddImagePart(ImagePartType.Jpeg);
                using (FileStream file = new FileStream(allTechnical.Coment, FileMode.Open))
                {
                    image.FeedData(file);
                }
                var template = $"{allTechnical.Item}: {allTechnical.NameManufacturer} {allTechnical.NameModel}\r" +
                               $"s/n: {allTechnical.SerNum}\r" +
                               $"Инв.: {allTechnical.InventarNum}\r" +
                               $"Kaб.: {allTechnical.NumberKabinet}\r" +
                               $"User: {allTechnical.Name}";
                var paragraphSplit = template.Split('\r');
                Paragraph paragraph = new Paragraph();
                foreach (var str in paragraphSplit)
                {
                    paragraph.Append(paragraphGenerate.RunText(str, "20", 1,true));
                }
                var barcode = paragraphGenerate.RunParagraphGeneratorStandart("");
                barcode.Append(driving.AddImageToParagraph(mainDocumentPart.GetIdOfPart(image), 850000L, 850000L));

                cellCollection.Add(CellGenerate.GenerateCell(barcode, "0",
                    TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.SelectGenerateBorder(CellBorders.SelectTableCellBorders.LeftBorderOrBottomBorderOrTopBorder), 2));
                cellCollection.Add(CellGenerate.GenerateCell(paragraph, "0",
                    TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.SelectGenerateBorder(CellBorders.SelectTableCellBorders.RightBorderOrBottomBorderOrTopBorder), 0, 1));
            }
            var data = cellCollection.Skip(skip).Take(countSkip).ToArray();
            while (data.Any())
            {
                skip += countSkip;
                collectionAppendTable = new ObservableCollection<TableCell>(data);
                table.Append(rows.GenerateRow(ref collectionAppendTable, true, rows.FormulHeightRow(2.3)));
                data = cellCollection.Skip(skip).Take(countSkip).ToArray();
            }
            body.Append(table);
            return body;
        }
        /// <summary>
        /// Запись QR кодов кабинетов docx 
        /// </summary>
        /// <param name="report">Отчет</param>
        /// <param name="mainDocumentPart">Параграф</param>
        /// <param name="countSkip">Генерация столбиков Количество сбросов</param>
        /// <returns></returns>
        public Body StickerOffice(QrCodeOffice report, MainDocumentPart mainDocumentPart, int countSkip = 8)
        {
            Body body = new Body();
            var skip = 0;
            AddDriwing driving = new AddDriwing();
            Table table = new Table();
            var rows = new RowGenerate();
            ObservableCollection<TableCell> collectionAppendTable;
            ObservableCollection<TableCell> cellCollection = new ObservableCollection<TableCell>();
            var paragraphGenerate = new RunGenerate();
            foreach (var office in report.Kabinet)
            {
                ImagePart image = mainDocumentPart.AddImagePart(ImagePartType.Jpeg);
                using (FileStream file = new FileStream(office.FullPathPng, FileMode.Open))
                {
                    image.FeedData(file);
                }
                var barcode = paragraphGenerate.RunParagraphGeneratorStandart("");
                barcode.Append(driving.AddImageToParagraph(mainDocumentPart.GetIdOfPart(image), 800000L, 800000L));

                cellCollection.Add(CellGenerate.GenerateCell(barcode, "0",
                    TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.SelectGenerateBorder(CellBorders.SelectTableCellBorders.LeftBorderOrBottomBorderOrTopBorder), 2));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart($"Каб.: {office.NumberKabinet}", "24", JustificationValues.Center, 1), "0",
                    TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.SelectGenerateBorder(CellBorders.SelectTableCellBorders.RightBorderOrBottomBorderOrTopBorder), 0, 1));
            }
            var data = cellCollection.Skip(skip).Take(countSkip).ToArray();
            while (data.Any())
            {
                skip += countSkip;
                collectionAppendTable = new ObservableCollection<TableCell>(data);
                table.Append(rows.GenerateRow(ref collectionAppendTable, true, rows.FormulHeightRow(2.3)));
                data = cellCollection.Skip(skip).Take(countSkip).ToArray();
            }
            //Тоже рабочий цикл
            //var count = Math.Ceiling((decimal)cellCollection.Count / countSkip);
            //for (var i = 0; i < count; i++)
            //{
            //    collectionAppendTable = new ObservableCollection<TableCell>(cellCollection.Skip(skip).Take(countSkip));
            //    table.Append(rows.GenerateRow(ref collectionAppendTable, true, rows.FormulHeightRow(2.3)));
            //    skip += countSkip;
            //}
            body.Append(table);
            return body;
        }

        /// <summary>
        /// Создание акта списания техники
        /// </summary>
        /// <param name="actParameterModel">Модель акта</param>
        /// <returns></returns>
        public Body Act(Act[] actParameterModel)
        {
            Body body = new Body();
            Table table = new Table();
            Table table1 = new Table();
            Table table2 = new Table();
            Table table3 = new Table();
            Table table4 = new Table();
            Table table5 = new Table();
            Table table6 = new Table();
            Table table7 = new Table();
            Table table8 = new Table();
            Table table9 = new Table();
            Table table10 = new Table();
            Table table11 = new Table();
            Table table12 = new Table();
            Table table13 = new Table();
            ObservableCollection<TableCell> cellCollection = new ObservableCollection<TableCell>();
            var rows = new RowGenerate();

            var paragraphGenerate = new RunGenerate();
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart("АКТ", "22", JustificationValues.Center, 1));
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart("технической экспертизы работоспособности оборудования", "22", JustificationValues.Center, 1));
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart($"по заявке {actParameterModel.Where(act => act.ParameterAct.ClassParameterTemplate.NameClassParameter == "Head").Select(act => act.ParameterAct.Parameter).FirstOrDefault()}", "22", JustificationValues.Center, 1));
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart(" ", "10"));
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart($"город Москва                                                                                                                                  {DateTime.Now.ToString("dd.MM.yyyy")} г.","22"));
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart(" ", "10"));
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart(" ", "10"));
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart("Мы, нижеподписавшиеся уполномоченный специалист Филиала ФКУ «Налог-Сервис» ФНС России на проведение технической экспертизы и заместитель директора филиала ФКУ «Налог-Сервис» ФНС России в г. Москве составили настоящий Акт о том, что была проведена техническая экспертиза работоспособности оборудования.","22", JustificationValues.Both,0,"567"));
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart());

            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Место проведения экспертизы оборудования:","22"), CellGenerate.FormulWidthCell(8.1),
                TableWidthUnitValues.Dxa));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(actParameterModel.Where(act => act.ParameterAct.ClassParameterTemplate.NameClassParameter == "Place").Select(act => act.ParameterAct.Parameter).FirstOrDefault(), "22"), CellGenerate.FormulWidthCell(9.3),
                TableWidthUnitValues.Dxa, "100", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorder()));
            table.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.5)));
            body.Append(table);
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart());

            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Наименование оборудования:", "22"), CellGenerate.FormulWidthCell(5.5),
                TableWidthUnitValues.Dxa));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(actParameterModel.Where(act => act.ParameterAct.ClassParameterTemplate.NameClassParameter == "Name").Select(act => act.ParameterAct.Parameter).FirstOrDefault(), "22"), CellGenerate.FormulWidthCell(11.9),
                TableWidthUnitValues.Dxa, "100", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorder()));
            table1.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.5)));
            body.Append(table1);

            body.Append(paragraphGenerate.RunParagraphGeneratorStandart("Организация-производитель (изготовитель)","22"));

            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("или поставщик оборудования:", "22"), CellGenerate.FormulWidthCell(5.4),
                TableWidthUnitValues.Dxa));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(actParameterModel.Where(act => act.ParameterAct.ClassParameterTemplate.NameClassParameter == "Manufacturer").Select(act => act.ParameterAct.Parameter).FirstOrDefault(), "22"), CellGenerate.FormulWidthCell(12.0),
                TableWidthUnitValues.Dxa, "100", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorder()));
            table2.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.5)));
            body.Append(table2);
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart(".", "1"));
           
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Тип оборудования:", "22"), CellGenerate.FormulWidthCell(3.7),
                TableWidthUnitValues.Dxa));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(actParameterModel.Select(act => act.ClasificationAct.NameClasification).FirstOrDefault(), "22"), CellGenerate.FormulWidthCell(13.7),
                TableWidthUnitValues.Dxa, "100", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorder()));

            table3.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.5)));
            body.Append(table3);
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart());
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart());

            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Модель, модификация оборудования:", "22"), CellGenerate.FormulWidthCell(6.8),
                TableWidthUnitValues.Dxa));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(actParameterModel.Where(act => act.ParameterAct.ClassParameterTemplate.NameClassParameter == "Model").Select(act => act.ParameterAct.Parameter).FirstOrDefault(), "22"), CellGenerate.FormulWidthCell(10.6),
                TableWidthUnitValues.Dxa, "100", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorder()));

            table4.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.5)));
            body.Append(table4);
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart());

            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Год выпуска оборудования:", "22"), CellGenerate.FormulWidthCell(4.9),
                TableWidthUnitValues.Dxa));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(actParameterModel.Where(act => act.ParameterAct.ClassParameterTemplate.NameClassParameter == "Year").Select(act => act.ParameterAct.Parameter).FirstOrDefault(), "22"), CellGenerate.FormulWidthCell(2.5),
                TableWidthUnitValues.Dxa, "100", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorder()));

            table5.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.5)));
            body.Append(table5);
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart(".", "1"));

            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Дата ввода в эксплуатацию и дата закрепления за территориальным органом (организацией):", "22"), CellGenerate.FormulWidthCell(15.7),
                TableWidthUnitValues.Dxa));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(actParameterModel.Where(act => act.ParameterAct.ClassParameterTemplate.NameClassParameter == "DateInput").Select(act => act.ParameterAct.Parameter).FirstOrDefault(), "22"), CellGenerate.FormulWidthCell(1.7),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorder()));

            table6.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.5)));
            body.Append(table6);
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart(".", "1"));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Условия использования (эксплуатации):", "22"), CellGenerate.FormulWidthCell(7.0),
                TableWidthUnitValues.Dxa));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Нарушений условий эксплуатации не выявлено", "22"), CellGenerate.FormulWidthCell(10.4),
                TableWidthUnitValues.Dxa, "100", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorder()));

            table7.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.5)));
            body.Append(table7);
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart("Инвентарный, заводской (при ", "22"));
            
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("наличии) номер оборудования:", "22"), CellGenerate.FormulWidthCell(5.4),
                TableWidthUnitValues.Dxa));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(actParameterModel.Where(act => act.ParameterAct.ClassParameterTemplate.NameClassParameter == "Number").Select(act => act.ParameterAct.Parameter).FirstOrDefault(), "22"), CellGenerate.FormulWidthCell(12.0),
                TableWidthUnitValues.Dxa, "100", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorder()));
            table8.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.5)));
            body.Append(table8);
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart());
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart("Техническое состояние:", "22", JustificationValues.Left, 1,"567"));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Устройство неисправно.", "22"), CellGenerate.FormulWidthCell(17.4),
                TableWidthUnitValues.Dxa, "100", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorder()));
            table9.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.5)));
            body.Append(table9);
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart());
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart("В результате осмотра и оценки технического состояния было установлено:", "22", JustificationValues.Left, 1, "567"));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Системный блок не включается. Монитор не включается.", "22"), CellGenerate.FormulWidthCell(17.4),
                TableWidthUnitValues.Dxa, "100", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorder()));
            table10.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(1.23)));
            body.Append(table10);
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart());
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart("Выводы, заключение, рекомендации:", "22", JustificationValues.Left, 1, "567"));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Ремонт экономически нецелесообразен. Дальнейшая эксплуатация невозможна. Данная модель снята с производства. Заказ комплектующих невозможен. Наличие пригодных для эксплуатации деталей, которые могут быть изъяты при утилизации списанного имущества: отсутствуют.", "22"), CellGenerate.FormulWidthCell(17.4),
                TableWidthUnitValues.Dxa, "100", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorder()));
            table11.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(1.44)));
            body.Append(table11);
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart());
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart("Примечание:", "22", JustificationValues.Left, 1));
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart("Определить драгоценные металлы в связи с незначительным их количеством в технике импортного производства не представляется возможным. В технической документации на данное оборудование сведения о наличии драгоценных металлов отсутствуют.", "22",JustificationValues.Both));
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart());

            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Заместитель директора филиала", "22"), CellGenerate.FormulWidthCell(12.0),
                TableWidthUnitValues.Dxa));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), CellGenerate.FormulWidthCell(2.9),
                TableWidthUnitValues.Dxa, "100", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorder()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Ю.С. Губин", "22"), CellGenerate.FormulWidthCell(2.4),
                TableWidthUnitValues.Dxa, "100"));
            table12.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.5)));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("ФКУ «Налог-Сервис» ФНС России", "22"), "0",
                TableWidthUnitValues.Auto));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Подпись", "22"),"0",
                TableWidthUnitValues.Auto, "100"));
            table12.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.5)));
            body.Append(table12);
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart("М.П.", "22", JustificationValues.Center));

            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Уполномоченный специалист", "22"), CellGenerate.FormulWidthCell(12.0),
                TableWidthUnitValues.Dxa));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), CellGenerate.FormulWidthCell(2.9),
                TableWidthUnitValues.Dxa, "100", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorder()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Д.Б. Абанин", "22"), CellGenerate.FormulWidthCell(2.4),
                TableWidthUnitValues.Dxa, "100"));
            table13.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.5)));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Филиал ФКУ «Налог-Сервис» ФНС", "22"), "0",
                TableWidthUnitValues.Auto));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Подпись", "22"), "0",
                TableWidthUnitValues.Auto, "100"));
            table13.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.5)));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("России в г. Москве", "22"), "0",
                TableWidthUnitValues.Auto));
            table13.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.5)));
            body.Append(table13);
            return body;
        }
        /// <summary>
        /// Генерация журнала доступов из АИС 3
        /// </summary>
        /// <param name="template">Шаблон журнала</param>
        /// <returns></returns>
        public Body CreateJournal(EfDatabase.Journal.AllJournal template)
        {
            Body body = new Body();
            Table table = new Table();
            Table table2 = new Table();
            var paragraphGenerate = new RunGenerate();
            ObservableCollection<TableCell> cellCollection = new ObservableCollection<TableCell>();
            var rows = new RowGenerate();
            var para = paragraphGenerate.RunParagraphGeneratorStandart("к Порядку доступа к информационным, программным и аппаратным", "18", JustificationValues.Left, 0, "0", true);
            para.Append(paragraphGenerate.RunText("ресурсам Управления и подведомственных инспекций Федеральной", "18", 0, true));
            para.Append(paragraphGenerate.RunText("налоговой службы по г. Москве, утвержденного приказом УФНС России", "18", 0,true));
            para.Append(paragraphGenerate.RunText("по г. Москве", "18", 0, true));
            para.Append(paragraphGenerate.RunText("от «05»    12     2014 г. № ", "18"));
            para.Append(paragraphGenerate.RunText("440", "18",2));

            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), CellGenerate.FormulWidthCell(15.00),
                TableWidthUnitValues.Dxa));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Приложение № 5", "24"), CellGenerate.FormulWidthCell(10.63),
                TableWidthUnitValues.Dxa, "100"));
            table.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.56)));

            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(),"0",
                TableWidthUnitValues.Auto));
            cellCollection.Add(CellGenerate.GenerateCell(para, "0",
                TableWidthUnitValues.Auto, "100"));
            table.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(2.20)));

            body.Append(table);
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart(" ", "24"));
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart(" ", "24"));
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart(" ", "24"));
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart("Журнал", "24",JustificationValues.Center,1));
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart("регистрации заявок на предоставление доступа к информационным ресурсам ", "24", JustificationValues.Center));
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart(" ", "24"));
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart(" ", "24"));

            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Рег. № заявки","24",JustificationValues.Center,1), CellGenerate.FormulWidthCell(1.71),
                TableWidthUnitValues.Dxa,"0","0",TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0,0, "E6E6E6"));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Наименование отдела ", "24", JustificationValues.Center,1), CellGenerate.FormulWidthCell(4.83),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 0, "E6E6E6"));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Ф.И.О.", "24", JustificationValues.Center,1), CellGenerate.FormulWidthCell(6.27),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 0, "E6E6E6"));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Ресурс", "24", JustificationValues.Center, 1), CellGenerate.FormulWidthCell(2.87),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 0, "E6E6E6"));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Задача/ профиль", "24", JustificationValues.Center, 1), CellGenerate.FormulWidthCell(4.71),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 0, "E6E6E6"));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Дата передачи на исполнение", "24", JustificationValues.Center, 1), CellGenerate.FormulWidthCell(5.25),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 0, "E6E6E6"));
            table2.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(2.25)));
            foreach (var journalRow in template.JournalAis3)
            {
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(journalRow.IdJournal.ToString(), "24"), "0",
                    TableWidthUnitValues.Auto, "100", "100", TableVerticalAlignmentValues.Top, CellBorders.GenerateBorderFull()));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(journalRow.NameOtdel, "24"), "0",
                    TableWidthUnitValues.Auto, "100", "100", TableVerticalAlignmentValues.Top, CellBorders.GenerateBorderFull()));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(journalRow.TaskUser, "24"), "0",
                    TableWidthUnitValues.Auto, "100", "100", TableVerticalAlignmentValues.Top, CellBorders.GenerateBorderFull()));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(journalRow.NameResource, "24"), "0",
                    TableWidthUnitValues.Auto, "100", "100", TableVerticalAlignmentValues.Top, CellBorders.GenerateBorderFull()));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(journalRow.NameTarget, "24"), "0",
                    TableWidthUnitValues.Auto, "100", "100", TableVerticalAlignmentValues.Top, CellBorders.GenerateBorderFull()));
                cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(journalRow.DateTask.ToString("dd.MM.yyyy"), "24"), "0",
                    TableWidthUnitValues.Auto, "100", "100", TableVerticalAlignmentValues.Top, CellBorders.GenerateBorderFull()));
                table2.Append(rows.GenerateRow(ref cellCollection));
            }
            body.Append(table2);
            return body;
        }

        /// <summary>
        /// Генерация служебных записок для ОИТ
        /// </summary>
        /// <returns></returns>
        /// <param name="template">Модель шаблона</param>
        public Body CreateDocMemoReport(ModelMemoReport template)
        {
            Body body = new Body();
            var paragraphGenerate = new RunGenerate();
            var nameOrganization = "Межрайонной ИФНС России №51 по г. Москве";
            var paragraphDepartmentBoss = paragraphGenerate.RunParagraphGeneratorStandart();
            var paragraph = paragraphGenerate.RunParagraphGeneratorStandart();
            var paragraphPrl = paragraphGenerate.RunParagraphGeneratorStandart();
            var paragraphMemo = paragraphGenerate.RunParagraphGeneratorStandart();
            var paragraphBossDepartment = paragraphGenerate.RunParagraphGeneratorStandart(template.BossDepartment.NameDepartment, "22", JustificationValues.Left, 0, "0", true);
            paragraphBossDepartment.Append(paragraphGenerate.RunText(nameOrganization, "22"));
            var paragraphBossDepartmentAgreed = paragraphGenerate.RunParagraphGeneratorStandart(template.BossAgreed.PositionAgreed, "22", JustificationValues.Left, 0, "0", true);
            paragraphBossDepartmentAgreed.Append(paragraphGenerate.RunText(nameOrganization, "22"));

            Table table = new Table();
            Table table2 = new Table();
            Table table3 = new Table();
            ObservableCollection <TableCell> cellCollection = new ObservableCollection<TableCell>();
            var rows = new RowGenerate();
            var paragraphR1 = rows.FormulHeightRow(0.10);
            var paragraphR2 = rows.FormulHeightRow(0.10);

            paragraphDepartmentBoss = template.SelectParameterDocument.TypeDocument == 1 ||
                                      template.SelectParameterDocument.TypeDocument == 3
                ? paragraphGenerate.RunParagraphGeneratorStandart("Начальнику отдела информационной безопасности", "26")
                : paragraphGenerate.RunParagraphGeneratorStandart("Начальнику отдела информационных технологий", "26");
            if (template.SelectParameterDocument.TypeDocument != 2)
            {
                paragraph = paragraphGenerate.RunParagraphGeneratorStandart("к Порядку доступа к информационным, программным ", "18", JustificationValues.Both, 0, "0", true);
                paragraph.Append(paragraphGenerate.RunText("и аппаратным ресурсам Управления и подведомственных ", "18", 0, true));
                paragraph.Append(paragraphGenerate.RunText("Инспекций Федеральной налоговой службы по г. Москве,", "18", 0, true));
                paragraph.Append(paragraphGenerate.RunText(" утвержденного приказом УФНС России по г. Москве", "18", 0, true));
                paragraph.Append(paragraphGenerate.RunText("от «05»    12     2014 г. № ", "18"));
                paragraph.Append(paragraphGenerate.RunText("440", "18", 2));
                paragraphPrl = paragraphGenerate.RunParagraphGeneratorStandart("Приложение № 3", "18", JustificationValues.Right);
                paragraphR1 = rows.FormulHeightRow(0.50);
                paragraphR2 = rows.FormulHeightRow(2.20);
            }

            if (template.SelectParameterDocument.NumberDocument == 3)
            {
                paragraphMemo = paragraphGenerate.RunParagraphGeneratorStandart("Прошу перевести доменную учетную запись ", "22", JustificationValues.Both,0,"800");
                paragraphMemo.Append(paragraphGenerate.RunText($"(на основании приказа инспекции {template.UserDepartment.Orders.Orders_Reestr.LastOrder}) ", "22",3));
                paragraphMemo.Append(paragraphGenerate.RunText("сотрудника:", "22"));
            }

            if (template.SelectParameterDocument.NumberDocument == 2)
            {
                paragraphMemo = paragraphGenerate.RunParagraphGeneratorStandart("Прошу выдать рабочую станцию, телефонный аппарат ", "22", JustificationValues.Both, 0, "800");
                paragraphMemo.Append(paragraphGenerate.RunText("(служебная необходимость) ", "22",3));
                paragraphMemo.Append(paragraphGenerate.RunText("сотруднику:", "22"));
            }

            if (template.SelectParameterDocument.NumberDocument == 1)
            {
                paragraphMemo = paragraphGenerate.RunParagraphGeneratorStandart("Прошу создать доменную учетную запись ", "22", JustificationValues.Both, 0, "800");
                paragraphMemo.Append(paragraphGenerate.RunText("(по необходимости) ", "22", 3));
                paragraphMemo.Append(paragraphGenerate.RunText("сотруднику:", "22"));
            }
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), CellGenerate.FormulWidthCell(10.50),
                TableWidthUnitValues.Dxa));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphPrl, CellGenerate.FormulWidthCell(8.50),
                TableWidthUnitValues.Dxa, "100"));
            table.Append(rows.GenerateRow(ref cellCollection, true, paragraphR1));

            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), CellGenerate.FormulWidthCell(10.50),
                TableWidthUnitValues.Auto));
            cellCollection.Add(CellGenerate.GenerateCell(paragraph, CellGenerate.FormulWidthCell(8.50),
                TableWidthUnitValues.Auto, "100"));
            table.Append(rows.GenerateRow(ref cellCollection, true, paragraphR2));
           
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(template.UserDepartment.NameOtdel,"26"), CellGenerate.FormulWidthCell(10.50),
                TableWidthUnitValues.Auto, "100"));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphDepartmentBoss, CellGenerate.FormulWidthCell(8.50),
                TableWidthUnitValues.Auto, "100"));
            table.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(2.00)));

            body.Append(table);
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart());
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart());
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart());

            body.Append(paragraphGenerate.RunParagraphGeneratorStandart("Служебная записка","28",JustificationValues.Center));
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart());
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart());
            body.Append(paragraphMemo);
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart());

            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Ф.И.О","22",JustificationValues.Center), CellGenerate.FormulWidthCell(2.94),
                TableWidthUnitValues.Dxa,"0","0",TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(),0,0, "F2F2F2"));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Отдел", "22", JustificationValues.Center), CellGenerate.FormulWidthCell(2),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 0, "F2F2F2"));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Должность", "22", JustificationValues.Center), CellGenerate.FormulWidthCell(2.75),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 0, "F2F2F2"));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Табельный №", "22", JustificationValues.Center), CellGenerate.FormulWidthCell(3.50),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 0, "F2F2F2"));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Примечание", "22", JustificationValues.Center), CellGenerate.FormulWidthCell(2.25),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 0, "F2F2F2"));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("№ каб.", "22", JustificationValues.Center), CellGenerate.FormulWidthCell(2),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 0, "F2F2F2"));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("№ телефона", "22", JustificationValues.Center), CellGenerate.FormulWidthCell(2.47),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull(), 0, 0, "F2F2F2"));
            table2.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(1.00)));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(template.UserDepartment.Name,"20",JustificationValues.Center), CellGenerate.FormulWidthCell(2.94),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(template.UserDepartment.NameOtdel, "20", JustificationValues.Center), CellGenerate.FormulWidthCell(2),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(template.UserDepartment.Position, "20", JustificationValues.Center), CellGenerate.FormulWidthCell(2.75),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(template.UserDepartment.SmallTabelNumber, "20", JustificationValues.Center), CellGenerate.FormulWidthCell(3.50),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), CellGenerate.FormulWidthCell(2.25),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(template.UserDepartment.NumberKabinet, "20", JustificationValues.Center), CellGenerate.FormulWidthCell(2),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(template.UserDepartment.Telephone, "20", JustificationValues.Center), CellGenerate.FormulWidthCell(2.47),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            table2.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(2.00)));
            body.Append(table2);
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart());
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart());

            cellCollection.Add(CellGenerate.GenerateCell(paragraphBossDepartment, CellGenerate.FormulWidthCell(10.00),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Top ));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), CellGenerate.FormulWidthCell(4.00),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Top,CellBorders.GenerateBorder()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(template.BossDepartment.SmallName, "22"), CellGenerate.FormulWidthCell(3.91),
                TableWidthUnitValues.Dxa));
            table3.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(1.00)));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart($"«____»______________{DateTime.Now.Year}г", "22"),"0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Top));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto));
            table3.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(1.00)));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Согласовано:", "22",JustificationValues.Left,1), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Bottom));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto));
            table3.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(0.75)));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphBossDepartmentAgreed, CellGenerate.FormulWidthCell(10.00),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Top));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), CellGenerate.FormulWidthCell(4.00),
                TableWidthUnitValues.Dxa, "0", "0", TableVerticalAlignmentValues.Top, CellBorders.GenerateBorder()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(template.BossAgreed.NameAgreed, "22"), CellGenerate.FormulWidthCell(3.91),
                TableWidthUnitValues.Dxa));
            table3.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(1.00)));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart($"«____»______________{DateTime.Now.Year}г", "22"), "0",
                TableWidthUnitValues.Auto, "0", "0", TableVerticalAlignmentValues.Top));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0",
                TableWidthUnitValues.Auto));
            table3.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(1.00)));
            body.Append(table3);
            return body;
        }


        /// <summary>
        /// Генерация заявки для ОИТ для доступа к программным ресурсам
        /// </summary>
        /// <returns></returns>
        /// <param name="template">Модель шаблона</param>
        public Body CreateDocMemoApplication(ModelMemoReport template)
        {
            Body body = new Body();
            Table table = new Table();
            Table table2 = new Table();
            Table table3 = new Table();
            ObservableCollection<TableCell> cellCollection = new ObservableCollection<TableCell>();
            var rows = new RowGenerate();
            var paragraphGenerate = new RunGenerate();
            var nameOrganization = "Межрайонной ИФНС России №51 по г. Москве";
            Paragraph paragraphPrl = paragraphGenerate.RunParagraphGeneratorStandart("Приложение № 7", "18", JustificationValues.Right);
            Paragraph paragraph = paragraphGenerate.RunParagraphGeneratorStandart("к Порядку доступа к информационным, программным и аппаратным ", "18", JustificationValues.Both, 0, "0", true);
            paragraph.Append(paragraphGenerate.RunText("ресурсам Управления и подведомственных инспекций Федеральной ", "18", 0, true));
            paragraph.Append(paragraphGenerate.RunText("налоговой службы по г. Москве,  утвержденного приказом УФНС  ", "18", 0));
            paragraph.Append(paragraphGenerate.RunText("России по г. Москве от «05»  12  2014 г. № 440", "18", 0));
            Paragraph paragraphLeader = paragraphGenerate.RunParagraphGeneratorStandart("«УТВЕРЖДАЮ»", "22", JustificationValues.Left, 0, "0", true);
            paragraphLeader.Append(paragraphGenerate.RunText(template.LeaderOrganization.RnameOrganization, "22", 0, true));
            paragraphLeader.Append(paragraphGenerate.RunText(" ", "22", 0, true));
            paragraphLeader.Append(paragraphGenerate.RunText($"____________________ {template.LeaderOrganization.NameFaceLeader}", "22", 0, true));
            paragraphLeader.Append(paragraphGenerate.RunText($"«____»______________{DateTime.Now.Year}г.", "22"));

            Paragraph paragraphListApp = paragraphGenerate.RunParagraphGeneratorStandart("1.Lotus Notes", "18", JustificationValues.Left, 0, "0", true);
            paragraphListApp.Append(paragraphGenerate.RunText("2.GP-3", "18", 0, true));
            paragraphListApp.Append(paragraphGenerate.RunText("3.ПК СЭОД", "18", 0, true));
            paragraphListApp.Append(paragraphGenerate.RunText("4.СВОД 2000", "18", 0, true));
            paragraphListApp.Append(paragraphGenerate.RunText("5.Консультант+", "18"));

            Paragraph paragraphBossDepartment = paragraphGenerate.RunParagraphGeneratorStandart(template.BossDepartment.NameDepartment, "18", JustificationValues.Left, 0, "0", true);
            paragraphBossDepartment.Append(paragraphGenerate.RunText(nameOrganization, "18"));
            Paragraph paragraphBossDepartmentAgreed = paragraphGenerate.RunParagraphGeneratorStandart(template.BossAgreed.PositionAgreed, "18", JustificationValues.Left, 0, "0", true);
            paragraphBossDepartmentAgreed.Append(paragraphGenerate.RunText(nameOrganization, "18"));

            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), CellGenerate.FormulWidthCell(18.50), TableWidthUnitValues.Dxa));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphPrl, CellGenerate.FormulWidthCell(9.20), TableWidthUnitValues.Dxa));
            table.Append(rows.GenerateRow(ref cellCollection, true));

            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), CellGenerate.FormulWidthCell(18.50), TableWidthUnitValues.Dxa));
            cellCollection.Add(CellGenerate.GenerateCell(paragraph, CellGenerate.FormulWidthCell(9.20), TableWidthUnitValues.Dxa, "100"));
            table.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(1.60)));

            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), "0", TableWidthUnitValues.Auto));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphLeader, "0", TableWidthUnitValues.Auto, "100"));
            table.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(3.50)));
            body.Append(table);

            body.Append(paragraphGenerate.RunParagraphGeneratorStandart("Заявка", "20", JustificationValues.Center,1));
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart("на предоставление доступа к информационным ресурсам Межрайонной ИФНС России №51по г. Москве (территориальный уровень)", "20", JustificationValues.Center));
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart($"следующим сотрудникам {template.UserDepartment.RnameOtdel.ToLower()}", "20", JustificationValues.Center));

            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("№ п/п","16", JustificationValues.Center), CellGenerate.FormulWidthCell(0.80), TableWidthUnitValues.Dxa,"100","100",TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Фамилия, имя, отчество/учетная запись сотрудника", "16",JustificationValues.Center), CellGenerate.FormulWidthCell(2.60), TableWidthUnitValues.Dxa, "100", "100", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Должность", "16", JustificationValues.Center), CellGenerate.FormulWidthCell(2.50), TableWidthUnitValues.Dxa, "100", "100", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Обоснование необходимости проведения указанного вида работ в соответствии с должностными обязанностями сотрудника (ссылка на раздел должностного регламента или иной нормативный документ)", "16", JustificationValues.Center), CellGenerate.FormulWidthCell(7), TableWidthUnitValues.Dxa, "100", "100", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Телефон/комната", "16", JustificationValues.Center), CellGenerate.FormulWidthCell(2.50), TableWidthUnitValues.Dxa, "100", "100", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Ресурс (согласно Перечню)", "16", JustificationValues.Center), CellGenerate.FormulWidthCell(3), TableWidthUnitValues.Dxa, "100", "100", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Задача", "16", JustificationValues.Center), CellGenerate.FormulWidthCell(2.50), TableWidthUnitValues.Dxa, "100", "100", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("IP-адрес рабочей станции", "16", JustificationValues.Center), CellGenerate.FormulWidthCell(2.20), TableWidthUnitValues.Dxa, "100", "100", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Подпись сотрудника", "16", JustificationValues.Center), CellGenerate.FormulWidthCell(2), TableWidthUnitValues.Dxa, "100", "100", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Примечание", "16", JustificationValues.Center), CellGenerate.FormulWidthCell(2.50), TableWidthUnitValues.Dxa, "100", "100", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            table2.Append(rows.GenerateRow(ref cellCollection));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("1", "16", JustificationValues.Center), CellGenerate.FormulWidthCell(0.80), TableWidthUnitValues.Dxa, "100", "100", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("2", "16", JustificationValues.Center), CellGenerate.FormulWidthCell(2.60), TableWidthUnitValues.Dxa, "100", "100", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("3", "16", JustificationValues.Center), CellGenerate.FormulWidthCell(2.50), TableWidthUnitValues.Dxa, "100", "100", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("4", "16", JustificationValues.Center), CellGenerate.FormulWidthCell(7), TableWidthUnitValues.Dxa, "100", "100", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("5", "16", JustificationValues.Center), CellGenerate.FormulWidthCell(2.50), TableWidthUnitValues.Dxa, "100", "100", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("6", "16", JustificationValues.Center), CellGenerate.FormulWidthCell(3), TableWidthUnitValues.Dxa, "100", "100", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("7", "16", JustificationValues.Center), CellGenerate.FormulWidthCell(2.50), TableWidthUnitValues.Dxa, "100", "100", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("8", "16", JustificationValues.Center), CellGenerate.FormulWidthCell(2.20), TableWidthUnitValues.Dxa, "100", "100", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("9", "16", JustificationValues.Center), CellGenerate.FormulWidthCell(2), TableWidthUnitValues.Dxa, "100", "100", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("10", "16", JustificationValues.Center), CellGenerate.FormulWidthCell(2.50), TableWidthUnitValues.Dxa, "100", "100", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            table2.Append(rows.GenerateRow(ref cellCollection));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("1", "18"), CellGenerate.FormulWidthCell(0.80), TableWidthUnitValues.Dxa, "100", "100", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(template.UserDepartment.Name, "18"), CellGenerate.FormulWidthCell(2.60), TableWidthUnitValues.Dxa, "100", "100", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(template.UserDepartment.Position, "18"), CellGenerate.FormulWidthCell(2.50), TableWidthUnitValues.Dxa, "100", "100", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(template.UserDepartment.Regulations, "18"), CellGenerate.FormulWidthCell(7), TableWidthUnitValues.Dxa, "100", "100", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart($"{template.UserDepartment.Telephone}/{template.UserDepartment.NumberKabinet}", "18"), CellGenerate.FormulWidthCell(2.50), TableWidthUnitValues.Dxa, "100", "100", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphListApp, CellGenerate.FormulWidthCell(3), TableWidthUnitValues.Dxa, "100", "100", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Просмотр", "18"), CellGenerate.FormulWidthCell(2.50), TableWidthUnitValues.Dxa, "100", "100", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(template.UserDepartment.IpAdress, "18"), CellGenerate.FormulWidthCell(2.20), TableWidthUnitValues.Dxa, "100", "100", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), CellGenerate.FormulWidthCell(2), TableWidthUnitValues.Dxa, "100", "100", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), CellGenerate.FormulWidthCell(2.50), TableWidthUnitValues.Dxa, "100", "100", TableVerticalAlignmentValues.Center, CellBorders.GenerateBorderFull()));
            table2.Append(rows.GenerateRow(ref cellCollection));
            body.Append(table2);
            body.Append(paragraphGenerate.RunParagraphGeneratorStandart());

            cellCollection.Add(CellGenerate.GenerateCell(paragraphBossDepartment, CellGenerate.FormulWidthCell(11.20), TableWidthUnitValues.Dxa, "200", "200"));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), CellGenerate.FormulWidthCell(7.30), TableWidthUnitValues.Dxa, "200", "200",TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorder()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(template.BossDepartment.SmallName, "18"), CellGenerate.FormulWidthCell(4),TableWidthUnitValues.Dxa, "200", "200"));
            table3.Append(rows.GenerateRow(ref cellCollection));

            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart($"«____»______________{DateTime.Now.Year}г", "18"), CellGenerate.FormulWidthCell(11.20), TableWidthUnitValues.Dxa, "200", "200"));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("(Подпись)", "18",JustificationValues.Center), CellGenerate.FormulWidthCell(7.30), TableWidthUnitValues.Dxa));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), CellGenerate.FormulWidthCell(4), TableWidthUnitValues.Dxa, "200", "200"));
            table3.Append(rows.GenerateRow(ref cellCollection));

            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Согласовано:", "18",JustificationValues.Left,1), CellGenerate.FormulWidthCell(11.20), TableWidthUnitValues.Dxa, "200", "200", TableVerticalAlignmentValues.Center));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), CellGenerate.FormulWidthCell(7.30), TableWidthUnitValues.Dxa));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), CellGenerate.FormulWidthCell(4), TableWidthUnitValues.Dxa));
            table3.Append(rows.GenerateRow(ref cellCollection,true, rows.FormulHeightRow(1.00)));

            cellCollection.Add(CellGenerate.GenerateCell(paragraphBossDepartmentAgreed, CellGenerate.FormulWidthCell(11.20), TableWidthUnitValues.Dxa, "200", "200"));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), CellGenerate.FormulWidthCell(7.30), TableWidthUnitValues.Dxa, "200", "200", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorder()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(template.BossAgreed.NameAgreed, "18"), CellGenerate.FormulWidthCell(4), TableWidthUnitValues.Dxa, "200", "200"));
            table3.Append(rows.GenerateRow(ref cellCollection));

            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart($"«____»______________{DateTime.Now.Year}г", "18"), CellGenerate.FormulWidthCell(11.20), TableWidthUnitValues.Dxa, "200", "200"));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("(Подпись)", "18", JustificationValues.Center), CellGenerate.FormulWidthCell(7.30), TableWidthUnitValues.Dxa));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), CellGenerate.FormulWidthCell(4), TableWidthUnitValues.Dxa, "200", "200"));
            table3.Append(rows.GenerateRow(ref cellCollection));

            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("Отметка об исполнении заявки:", "18", JustificationValues.Left, 1), CellGenerate.FormulWidthCell(11.20), TableWidthUnitValues.Dxa, "200", "200", TableVerticalAlignmentValues.Center));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), CellGenerate.FormulWidthCell(7.30), TableWidthUnitValues.Dxa, "200", "200", TableVerticalAlignmentValues.Bottom, CellBorders.GenerateBorder()));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), CellGenerate.FormulWidthCell(4), TableWidthUnitValues.Dxa));
            table3.Append(rows.GenerateRow(ref cellCollection, true, rows.FormulHeightRow(1.50)));

            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart($"«____»______________{DateTime.Now.Year}г", "18"), CellGenerate.FormulWidthCell(11.20), TableWidthUnitValues.Dxa, "200", "200"));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart("(Подпись)", "18", JustificationValues.Center), CellGenerate.FormulWidthCell(7.30), TableWidthUnitValues.Dxa));
            cellCollection.Add(CellGenerate.GenerateCell(paragraphGenerate.RunParagraphGeneratorStandart(), CellGenerate.FormulWidthCell(4), TableWidthUnitValues.Dxa, "200", "200"));
            table3.Append(rows.GenerateRow(ref cellCollection));

            body.Append(table3);

            return body;
        }
    }
}

