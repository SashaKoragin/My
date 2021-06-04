using LibaryDocumentGenerator.ProgrammView.Word.Template.HeadersDocument;
using LibaryDocumentGenerator.ProgrammView.Word.Template.SettingPage;
using LibaryDocumentGenerator.ProgrammView.Word.Template.BodyDocument;
using LibaryDocumentGenerator.ProgrammView.Word.Template.FottersDocument;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using LibaryXMLAutoReports.ReportsBdk;
using Single = LibaryDocumentGenerator.ProgrammView.Word.Template.SingleDocument.Single;
using System.IO;

namespace LibaryDocumentGenerator.Documents.Template
{
    /// <summary>
    /// Шаблон OutBdk разкладки модели FN71
    /// </summary>
    public class TemplateOutBdk : ITemplate<LibaryXMLAutoReports.FullTemplateSheme.Document>
    {
        public string FullPathDocumentWord { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public void CreateDocument(string path, LibaryXMLAutoReports.FullTemplateSheme.Document template, object obj)
        {
            var data = (FN71)obj;
            using (WordprocessingDocument package = WordprocessingDocument.Create(path, WordprocessingDocumentType.Document))
            {
                CreateWord(package, template, data);
                package.MainDocumentPart.Document.Save();
                package.Close();
            }
        }

        public void CreateWord(WordprocessingDocument package, LibaryXMLAutoReports.FullTemplateSheme.Document template, object obj)
        {
            var data = (FN71)obj;
            MainDocumentPart mainDocumentPart = package.AddMainDocumentPart();
            DocumentFormat.OpenXml.Wordprocessing.Document document = new DocumentFormat.OpenXml.Wordprocessing.Document();
            HeadersDocuments headers = new HeadersDocuments();
            BodyDocument body = new BodyDocument();
            Single single = new Single();
            Fotters fotters = new Fotters();
            PageSetting settingpage = new PageSetting();
            fotters.FottersAddDocument(mainDocumentPart, template);
            document.Append(settingpage.AddSetting(mainDocumentPart));
            document.Append(headers.DocumentsHeaders(template, data.N279, data.N280));
            document.Append(body.TextDocument(template));
            document.Append(body.DocumentIshBdkTableBody(data));
            document.Append(single.AddSingle(template));
            mainDocumentPart.Document = document;
        }

        public Stream FileArray()
        {
            throw new System.NotImplementedException();
        }
    }
}