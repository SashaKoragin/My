using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using EfDatabaseInvoice;
using LibaryDocumentGenerator.ProgrammView.Word.Template.SettingPage;
using LibaryDocumentGenerator.ProgrammView.FullDocument;


namespace LibaryDocumentGenerator.Documents.Template
{
   public class InvoiceInventarka : ITemplate<Report>
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

        public void CreateDocument(string path, Report template, object obj = null)
        {
            FullPathDocumentWord = path +template.Main.Received.UserName + Constant.WordConstant.Formatword;
            using (WordprocessingDocument package = WordprocessingDocument.Create(FullPathDocumentWord, WordprocessingDocumentType.Document))
            {
                CreateWord(package, template, obj);
                package.MainDocumentPart.Document.Save();
                package.Close();
            }
        }

        public void CreateWord(WordprocessingDocument package, Report template, object obj = null)
        {
            MainDocumentPart mainDocumentPart = package.AddMainDocumentPart();
            DocumentFormat.OpenXml.Wordprocessing.Document document = new DocumentFormat.OpenXml.Wordprocessing.Document();
            ImagePart image = mainDocumentPart.AddImagePart(ImagePartType.Jpeg);
            using (FileStream file = new FileStream(template.Main.Barcode.PathBarcode, FileMode.Open))
            {
                image.FeedData(file);
            }
            PageSetting settingpage = new PageSetting();
            DocumentsFull documentInvoce = new DocumentsFull();
            document.Append(settingpage.ParametrPageHorizont());
            document.Append(documentInvoce.DocumentsBook(template, mainDocumentPart.GetIdOfPart(image)));
            mainDocumentPart.Document = document;
        }
    }
}
