using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using LibaryDocumentGenerator.ProgrammView.Excel.Library;
using LibaryDocumentGenerator.ProgrammView.FullDocumentExcel;
using LibaryXMLAutoModelXmlSql.PreCheck.ModelCard;

namespace LibaryDocumentGenerator.Documents.TemplateExcel
{
   public class ReportAskNds : ITemplateExcel<CardFaceUl>
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

        public void CreateDocument(string path, CardFaceUl template, object obj = null)
        {
            FullPathDocumentWord = path + "Отчет АСК НДС" + Constant.WordConstant.FormatXlsx;
            using (SpreadsheetDocument package = SpreadsheetDocument.Create(FullPathDocumentWord, SpreadsheetDocumentType.Workbook))
            {
                CreateExcel(package, template, obj);
                package.Close();
            }
        }

        public void CreateExcel(SpreadsheetDocument document, CardFaceUl template, object obj = null)
        {
            ListExcel excel = new ListExcel(document);
            var list1 = excel.CreateExcelList("Контрагенты");
            DocumentExcelReportNds docNds = new DocumentExcelReportNds(excel.Document,excel.WorkBookPart);
            list1.Worksheet = docNds.ReportNds(template, (int)obj);
            excel.WorkBookPart.Workbook.Save();
        }

   }
}
