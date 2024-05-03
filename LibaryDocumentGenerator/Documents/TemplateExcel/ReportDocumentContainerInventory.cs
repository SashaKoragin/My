using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using EfDatabaseAutomation.Automation.BaseLogica.AutoLogicInventory.ModelReportContainer;
using LibaryDocumentGenerator.ProgrammView.Excel.Library;
using LibaryDocumentGenerator.ProgrammView.FullDocumentExcel;

namespace LibaryDocumentGenerator.Documents.TemplateExcel
{
    public class ReportDocumentContainerInventory : ITemplateExcel<ReportDocumentContainer>
    {
        /// <summary>
        /// Полный путь до документа
        /// </summary>
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
        /// <summary>
        /// Создание отчета Excel
        /// </summary>
        /// <param name="path">Путь сохранения</param>
        /// <param name="template">Шаблон</param>
        /// <param name="obj">Прочее</param>
        public void CreateDocument(string path, ReportDocumentContainer template, object obj = null)
        {
            FullPathDocumentWord = path + "Отчет по таре" + Constant.WordConstant.FormatXlsx;
            using (SpreadsheetDocument package = SpreadsheetDocument.Create(FullPathDocumentWord, SpreadsheetDocumentType.Workbook))
            {
                CreateExcel(package, template, obj);
                package.Close();
            }
        }
        /// <summary>
        /// Создание листов Excel
        /// </summary>
        /// <param name="document">Документа Excel</param>
        /// <param name="template">Шаблон</param>
        /// <param name="obj">Прочее</param>
        public void CreateExcel(SpreadsheetDocument document, ReportDocumentContainer template, object obj = null)
        {
            var excel = new ListExcel(document);
            var list1 = excel.CreateExcelList("Отчет по контейнеру");
            var reportExcelUsers = new AllDocumentExcel(excel.Document, excel.WorkBookPart);
            list1.Worksheet = reportExcelUsers.ReportCreateDocumentContainer(template);
            excel.WorkBookPart.Workbook.Save();
        }


    }
}
