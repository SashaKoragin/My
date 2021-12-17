using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using EfDatabase.Inventory.BaseLogic.Select;
using EfDatabaseXsdQrCodeModel;
using EfDatabaseXsdSupportNalog;
using LibaryDocumentGenerator.Barcode;
using LibaryDocumentGenerator.Documents.Template;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestIFNSLibary.Inventarka;

namespace LibaryDocumentGeneratorTestsTemplate.TestStoAuto
{
    [TestClass]
   public class StoTest
    {

        [TestMethod]
        public void TestAutoSto()
        {
            DateTime date = DateTime.Now;

                    if (date.Day == 08)
                    {
                        var description = @"Добрый день!" +
                                          "Требуется замена ТОНЕРА на МФУ Xerox VersaLink B7030 в каб.237 сер.№3399683695," +
                                          "серв.№77068-4-403-3399683695," +
                                          "инв.№22-100135(по договоренности с менеджером Денисом).";
                            var modelSto = new ModelParametrSupport()
                            {
                                Discription = description,
                                IdMfu = 50,
                                IdUser = 96,
                                Login = "7751-00-400",
                                Password = "Acr1at1ve$$",
                                IdTemplate = 7
                            };
                            Inventarka inventory = new Inventarka();
                            var models = inventory.ServiceSupport(modelSupport: modelSto);
                            if (models.Result.Step3ResponseSupport != null)
                            {
                               
                            }
                    }
        }
        [TestMethod]
        public void CreateQrCodeOffice()
        {
            Select auto = new Select();
            QrCodeOffice office = auto.SelectOffice("357", true);
            GenerateBarcode qrCode = new GenerateBarcode();
            OfficeStikerCode stickerQrOffice = new OfficeStikerCode();
            //Создание qr кодов
            foreach (var qrCodeOffice in office.Kabinet)
            {
                qrCodeOffice.FullPathPng = qrCode.GenerateQrCode("C:\\Testing\\" + qrCodeOffice.IdNumberKabinet, qrCodeOffice.NumberKabinet);
            }
            stickerQrOffice.CreateDocument("C:\\Testing\\QrCodeOffice", office);
            //Удаление всех png
            foreach (var cabinet in office.Kabinet)
            {
                File.Delete(cabinet.FullPathPng);
            }
        }
        /// <summary>
        /// Тестовый метод паспорт оборудования
        /// </summary>
        [TestMethod]
        public void TestStoPassportToModel()
        {
            var pathXlsx = "D:\\Testing\\Оборудование_в_организации.xlsx";
            DataTable dt = new DataTable();
            using (SpreadsheetDocument spreadSheetDocument = SpreadsheetDocument.Open(pathXlsx, false))
            {
                WorkbookPart workbookPart = spreadSheetDocument.WorkbookPart;
                IEnumerable<Sheet> sheets = spreadSheetDocument.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>();
                string relationshipId = sheets.First().Id.Value;
                WorksheetPart worksheetPart = (WorksheetPart)spreadSheetDocument.WorkbookPart.GetPartById(relationshipId);
                Worksheet workSheet = worksheetPart.Worksheet;
                SheetData sheetData = workSheet.GetFirstChild<SheetData>();
                IEnumerable<Row> rows = sheetData.Descendants<Row>();

                foreach (Cell cell in rows.ElementAt(0))
                {
                    dt.Columns.Add(GetCellValue(spreadSheetDocument, cell));
                }

                foreach (Row row in rows) //this will also include your header row...
                {
                    DataRow tempRow = dt.NewRow();

                    for (int i = 0; i < row.Descendants<Cell>().Count(); i++)
                    {
                        tempRow[i] = GetCellValue(spreadSheetDocument, row.Descendants<Cell>().ElementAt(i));
                    }
                    dt.Rows.Add(tempRow);
                }
            }
            dt.Rows.RemoveAt(0); //...so i'm taking it out here.


            //using (SpreadsheetDocument spreadSheetDocument = SpreadsheetDocument.Open(pathXlsx, true))
            //{

            //    WorkbookPart workbookPart = spreadSheetDocument.WorkbookPart;
            //    IEnumerable<Sheet> sheets = spreadSheetDocument.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>();
            //    string relationshipId = sheets.First().Id.Value;
            //    WorksheetPart worksheetPart = (WorksheetPart)spreadSheetDocument.WorkbookPart.GetPartById(relationshipId);

            //    spreadSheetDocument.Save();
            //    spreadSheetDocument.Close();
            //}

            // var sheetName = "Лист1";
            // XlsxToDataTable xlsxToDataTable = new XlsxToDataTable();
            // var dataTable = xlsxToDataTable.GetDateTableXslx(pathXlsx, sheetName);



            // var connectionString = string.Format($"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={pathXlsx}; Extended Properties=Excel 12.0;");
            // var adapter = new OleDbDataAdapter($"Select * From [{sheetName}$]", connectionString);
            // var ds = new DataSet();
            // adapter.Fill(ds, "STO");
            //// DataTable dataTable = ds.Tables["STO"];
        }


        public static string GetCellValue(SpreadsheetDocument document, Cell cell)
        {
            SharedStringTablePart stringTablePart =
                document.WorkbookPart.SharedStringTablePart;
            string value = cell.CellValue.InnerXml;

            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                return stringTablePart.SharedStringTable.ChildElements[Int32.Parse(value)].InnerText;
            }
            else
            {
                return value;
            }
        }

    }
}
