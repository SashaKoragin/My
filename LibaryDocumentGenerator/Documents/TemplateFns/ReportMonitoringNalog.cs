using System.Collections.Generic;
using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using EfDatabaseAutomation.Automation.BaseLogica.FnsModel.ModelMonitoringNalog;
using LibaryDocumentGenerator.Documents.TemplateExcel;
using LibaryDocumentGenerator.ProgrammView.Excel.Library;
using LibaryDocumentGenerator.ProgrammView.FullDocumentFns;

namespace LibaryDocumentGenerator.Documents.TemplateFns
{
    public class ReportMonitoringNalog : ITemplateExcel<List<ModelSvodDataBase>>
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

        public void CreateDocument(string path, List<ModelSvodDataBase> template, object obj = null)
        {
            FullPathDocumentWord = path + (string)obj + Constant.WordConstant.FormatXlsx;
            using (SpreadsheetDocument package = SpreadsheetDocument.Create(FullPathDocumentWord, SpreadsheetDocumentType.Workbook))
            {
                CreateExcel(package, template, obj);
                package.Close();
            }
        }

        public void CreateExcel(SpreadsheetDocument document, List<ModelSvodDataBase> template, object obj = null)
        {
            ListExcel excel = new ListExcel(document);
            var list1 = excel.CreateExcelList((string)obj);
            AllDocumentFns docCard = new AllDocumentFns(excel.Document, excel.WorkBookPart);
            list1.Worksheet = docCard.CreateReportMonitoringNalog(template, (string)obj);
            excel.WorkBookPart.Workbook.Save();
        }
    }
}
