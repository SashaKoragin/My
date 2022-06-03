using System;
using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using EfDatabase.ReportCard;
using LibaryDocumentGenerator.ProgrammView.Excel.Library;
using LibaryDocumentGenerator.ProgrammView.FullDocumentExcel;

namespace LibaryDocumentGenerator.Documents.TemplateExcel
{
    /// <summary>
    /// Документ табель
    /// </summary>
    public class ReportCard : ITemplateExcel<ReportCardModel>
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

        public void CreateDocument(string path, ReportCardModel template, object obj = null)
        {
            FullPathDocumentWord = path + "Табель_"+DateTime.Now.ToString("yyyy-dd-M_HH-mm-ss-fff") + Constant.WordConstant.FormatXlsx;
            using (SpreadsheetDocument package = SpreadsheetDocument.Create(FullPathDocumentWord, SpreadsheetDocumentType.Workbook))
            {
                CreateExcel(package, template, obj);
                package.Close();
            }
        }

        public void CreateExcel(SpreadsheetDocument document, ReportCardModel template, object obj = null)
        {
            ListExcel excel = new ListExcel(document);
            var list1 = excel.CreateExcelList("Табель");
            AllDocumentExcel docCard = new AllDocumentExcel(excel.Document, excel.WorkBookPart);
            list1.Worksheet = docCard.ReportCard(template);
            excel.WorkBookPart.Workbook.Save();
        }
    }
}
