using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using EfDatabaseXsdQrCodeModel;
using LibaryDocumentGenerator.ProgrammView.FullDocument;
using LibaryDocumentGenerator.ProgrammView.Word.Template.SettingPage;

namespace LibaryDocumentGenerator.Documents.Template
{
   public class OfficeStikerCode : ITemplate<QrCodeOffice>
    {

        public string FullPathDocumentWord { get; set; }

        public Stream FileArray()
        {
            var file = File.ReadAllBytes(FullPathDocumentWord);
            File.Delete(FullPathDocumentWord);
            return new MemoryStream(file);
        }

        public void CreateDocument(string path, QrCodeOffice template, object obj = null)
        {
            FullPathDocumentWord = path + Constant.WordConstant.FormatWord;
            using (WordprocessingDocument package = WordprocessingDocument.Create(FullPathDocumentWord, WordprocessingDocumentType.Document))
            {
                CreateWord(package, template, obj);
                package.MainDocumentPart.Document.Save();
                package.Close();
            }
        }
        /// <summary>
        /// Создать документ
        /// </summary>
        /// <param name="package">Пакет</param>
        /// <param name="template">Шаблон</param>
        /// <param name="obj">Объект</param>
        public void CreateWord(WordprocessingDocument package, QrCodeOffice template, object obj = null)
        {
            MainDocumentPart mainDocumentPart = package.AddMainDocumentPart();
            DocumentFormat.OpenXml.Wordprocessing.Document document = new DocumentFormat.OpenXml.Wordprocessing.Document();
            PageSetting settingPage = new PageSetting();
            DocumentsFull documentInvoce = new DocumentsFull();
            document.Append(settingPage.DocumentSettingVertical());
            document.Append(documentInvoce.StickerOffice(template, mainDocumentPart));
            mainDocumentPart.Document = document;
        }
    }
}
