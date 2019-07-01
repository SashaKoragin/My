using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using EfDatabaseInvoice;
using LibaryDocumentGenerator.ProgrammView.Word.Template.HeadersDocument;
using LibaryDocumentGenerator.ProgrammView.Word.Template.SettingPage;
using LibaryDocumentGenerator.ProgrammView.FullDocument;

namespace LibaryDocumentGenerator.Documents.Template
{
   public class InvoiceInventarka : ITemplate<Report>
    {
        public void CreateDocum(string path, Report template, object obj)
        {
            string fullpath = path +template.Main.Received.UserName + Constant.WordConstant.Formatword;
            using (WordprocessingDocument package = WordprocessingDocument.Create(fullpath, WordprocessingDocumentType.Document))
            {
                CreateWord(package, template, obj);
                package.MainDocumentPart.Document.Save();
                package.Close();
            }
        }

        public void CreateWord(WordprocessingDocument package, Report template, object obj)
        {
            MainDocumentPart mainDocumentPart = package.AddMainDocumentPart();
            DocumentFormat.OpenXml.Wordprocessing.Document document = new DocumentFormat.OpenXml.Wordprocessing.Document();
            PageSetting settingpage = new PageSetting();
            DocumentsFull documentInvoce = new DocumentsFull();
            document.Append(settingpage.ParametrPageHorizont());
            document.Append(documentInvoce.DocumentsBook(template));
            mainDocumentPart.Document = document;
        }
    }
}
