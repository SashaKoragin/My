using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using EfDatabase.ReportXml.ModelComparableUserResult;
using LibaryDocumentGenerator.ProgrammView.Excel.Library;
using LibaryDocumentGenerator.ProgrammView.FullDocumentExcel;

namespace LibaryDocumentGenerator.Documents.TemplateExcel
{
    public class ReportComparableAksiokAndInventoryResult : ITemplateExcel<ComparableCardAksiokAndInventory[]>
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

        public void CreateDocument(string path, ComparableCardAksiokAndInventory[] template, object obj = null)
        {
            FullPathDocumentWord = path + "Разночтения из баз данных" + Constant.WordConstant.FormatXlsx;
            using (SpreadsheetDocument package = SpreadsheetDocument.Create(FullPathDocumentWord, SpreadsheetDocumentType.Workbook))
            {
                CreateExcel(package, template, obj);
                package.Close();
            }
        }

        public void CreateExcel(SpreadsheetDocument document, ComparableCardAksiokAndInventory[] template, object obj = null)
        {
            var excel = new ListExcel(document);
            var list1 = excel.CreateExcelList("Карточка сравнения оборудования");
            var reportExcelUsers = new AllDocumentExcel(excel.Document, excel.WorkBookPart);
            list1.Worksheet = reportExcelUsers.ReportCardAksiokAndInventory(template);
            excel.WorkBookPart.Workbook.Save();
        }

    }
}
