
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using LibaryDocumentGenerator.ProgrammView.Word.Template.DocMatCen;
using LibaryDocumentGenerator.ProgrammView.Word.Template.SettingPage;


namespace LibaryDocumentGenerator.Documents.Template
{
    public class TemplateDocMatCen : ITemplate
    {
        public void CreateDocum(string path, LibaryXMLAutoReports.FullTemplateSheme.Document template, object obj)
        {
            var data = obj;
            using (WordprocessingDocument package = WordprocessingDocument.Create(path, WordprocessingDocumentType.Document))
            {
                CreateWord(package, template, data);
                package.MainDocumentPart.Document.Save();
                package.Close();
            }
        }

        public void CreateWord(WordprocessingDocument package, LibaryXMLAutoReports.FullTemplateSheme.Document template, object obj)
        {
            MainDocumentPart mainDocumentPart = package.AddMainDocumentPart();
            DocumentFormat.OpenXml.Wordprocessing.Document document = new DocumentFormat.OpenXml.Wordprocessing.Document();
            DocMatCen doc = new DocMatCen();
            PageSetting settingpage = new PageSetting();
            document.Append(settingpage.AddSetting(mainDocumentPart));
            document.Append(doc.GenerateDocMatCen());
            //HeadersDocuments headers = new HeadersDocuments();
            //BodyDocument body = new BodyDocument();
            //Single single = new Single();
            //Fotters fotters = new Fotters();
            //PageSetting settingpage = new PageSetting();
            //fotters.FottersAddDocument(mainDocumentPart, template);
            //document.Append(settingpage.AddSetting(mainDocumentPart, data.FN1723_2.Length));
            //document.Append(headers.DocumentsHeaders(template, data.N279, data.N280));
            //document.Append(body.DocumentIshBdkTableBody(data));
            //document.Append(single.AddSingle(template));
            mainDocumentPart.Document = document;
        }
    }
}
