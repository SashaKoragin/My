using System;
using System.Data;
using System.IO;
using System.Linq.Expressions;
using LibaryDocumentGenerator.ProgrammView.Word.Template.HeadersDocument;
using LibaryDocumentGenerator.ProgrammView.Word.Template.SettingPage;
using LibaryDocumentGenerator.ProgrammView.Word.Template.BodyDocument;
using LibaryDocumentGenerator.ProgrammView.Word.Template.FottersDocument;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using LibaryXMLAutoReports.ReportsBdk;
using Single = LibaryDocumentGenerator.ProgrammView.Word.Template.SingleDocument.Single;


namespace LibaryDocumentGenerator.Documents.Template
{
    /// <summary>
    /// Шаблон OutBdk разкладки модели FN71
    /// </summary>
    public class TemplateOutBdk : ITemplate
    {
        public void CreateDocum(string path, LibaryXMLAutoReports.TemplateSheme.Template template, object obj)
        {
            var data = (FN71)obj;
            using (WordprocessingDocument package = WordprocessingDocument.Create(path, WordprocessingDocumentType.Document))
            {
                CreateWord(package, template, data);
                package.MainDocumentPart.Document.Save();
                package.Close();
            }
        }

        public void CreateWord(WordprocessingDocument package, LibaryXMLAutoReports.TemplateSheme.Template template, object obj)
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
            document.Append(settingpage.AddSetting(mainDocumentPart, data.FN1723_2.Length));
            document.Append(headers.DocumentsHeaders(template, data.N279, data.N280));
            document.Append(body.TextDocument(template));
            document.Append(body.DocumentIshBdkTableBody(data));
            document.Append(single.AddSingle(template));
            mainDocumentPart.Document = document;
        }
    }
}