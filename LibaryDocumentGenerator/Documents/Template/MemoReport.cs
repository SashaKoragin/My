using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using EfDatabase.MemoReport;
using LibaryDocumentGenerator.ProgrammView.FullDocument;
using LibaryDocumentGenerator.ProgrammView.Word.Template.FottersDocument;
using LibaryDocumentGenerator.ProgrammView.Word.Template.SettingPage;

namespace LibaryDocumentGenerator.Documents.Template
{
    /// <summary>
    /// Генерация служебных записок для ОИТ
    /// </summary>
   public class MemoReport : ITemplate<ModelMemoReport>
    {
        public string FullPathDocumentWord { get; set; }
        public Stream FileArray()
        {
            var file = File.ReadAllBytes(FullPathDocumentWord);
            File.Delete(FullPathDocumentWord);
            return new MemoryStream(file);
        }

        public void CreateDocument(string path, ModelMemoReport  template, object obj = null)
        {
            FullPathDocumentWord = path + template.SelectParameterDocument.NameDocument + Constant.WordConstant.FormatWord;
            using (WordprocessingDocument package = WordprocessingDocument.Create(FullPathDocumentWord, WordprocessingDocumentType.Document))
            {
                CreateWord(package, template, obj);
                package.MainDocumentPart.Document.Save();
                package.Close();
            }
        }

        public void CreateWord(WordprocessingDocument package, ModelMemoReport template, object obj = null)
        {
            MainDocumentPart mainDocumentPart = package.AddMainDocumentPart();
            DocumentFormat.OpenXml.Wordprocessing.Document document = new DocumentFormat.OpenXml.Wordprocessing.Document();
            PageSetting settingPage = new PageSetting();
            DocumentsFull documentInvoke = new DocumentsFull();
            if (template.SelectParameterDocument.NumberDocument != 4)
            {
                Fotters footers = new Fotters();
                footers.FottersAddDocument(mainDocumentPart, template.Executor.NameUser, template.Executor.Phone);
                document.Append(settingPage.AddSetting(mainDocumentPart));
                document.Append(documentInvoke.CreateDocMemoReport(template));
            }
            else
            {
                document.Append(settingPage.ParametrPageHorizontEditMargin(new PageMargin() { Top = 300, Right = 794, Bottom = 200, Left = 794, Header = 300, Footer = 700U, Gutter = 0U }));
                document.Append(documentInvoke.CreateDocMemoApplication(template));
            }
            mainDocumentPart.Document = document;
        }
    }
}
