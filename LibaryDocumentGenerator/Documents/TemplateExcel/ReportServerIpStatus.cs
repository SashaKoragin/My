using System;
using System.Collections.Generic;
using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using EfDatabase.Inventory.Base;
using LibaryDocumentGenerator.ProgrammView.Excel.Library;
using LibaryDocumentGenerator.ProgrammView.FullDocumentExcel;


namespace LibaryDocumentGenerator.Documents.TemplateExcel
{
    /// <summary>
    /// Генерация отчета по серверам
    /// </summary>
    public class ReportServerIpStatus : ITemplateExcel<List<AllIpServerSelect>>
    {
        public string FullPathDocumentWord { get; set; }

        /// <summary>
        /// Выгрузка и удаление файла отчета
        /// </summary>
        /// <returns></returns>
        public Stream FileArray()
        {
            var file = File.ReadAllBytes(FullPathDocumentWord);
            File.Delete(FullPathDocumentWord);
            return new MemoryStream(file);
        }

        public void CreateDocument(string path, List<AllIpServerSelect> template, object obj = null)
        {
            FullPathDocumentWord = path + "Статистика доступности_" + DateTime.Now.ToString("yyyy-dd-M_HH-mm-ss") + Constant.WordConstant.FormatXlsx;
            using (SpreadsheetDocument package = SpreadsheetDocument.Create(FullPathDocumentWord, SpreadsheetDocumentType.Workbook))
            {
                CreateExcel(package, template, obj);
                package.Close();
            }
        }

        public void CreateExcel(SpreadsheetDocument document, List<AllIpServerSelect> template, object obj = null)
        {
            ListExcel excel = new ListExcel(document);
            var list1 = excel.CreateExcelList("Сервера");
            AllDocumentExcel docCard = new AllDocumentExcel(excel.Document, excel.WorkBookPart);
            list1.Worksheet = docCard.ReportStatusIpServer(template);
            excel.WorkBookPart.Workbook.Save();
        }
    }
}
