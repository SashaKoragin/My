using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using EfDatabase.Inventory.Base;
using LibaryDocumentGenerator.ProgrammView.FullDocument;
using LibaryDocumentGenerator.ProgrammView.Word.Template.SettingPage;

namespace LibaryDocumentGenerator.Documents.Template
{
    /// <summary>
    /// Класс генерации наклеек
    /// </summary>
    public class StickerCode : ITemplate<AllTechnic>
    {
        public string FullPathDocumentWord { get; set; }

        public void CreateDocum(string path, AllTechnic template, object obj=null)
        {
            FullPathDocumentWord = path + template.Id + Constant.WordConstant.Formatword;
            using (WordprocessingDocument package = WordprocessingDocument.Create(FullPathDocumentWord, WordprocessingDocumentType.Document))
            {
                CreateWord(package, template, obj);
                package.MainDocumentPart.Document.Save();
                package.Close();
            }
        }

        public void CreateWord(WordprocessingDocument package, AllTechnic template, object obj = null)
        {
            MainDocumentPart mainDocumentPart = package.AddMainDocumentPart();
            DocumentFormat.OpenXml.Wordprocessing.Document document = new DocumentFormat.OpenXml.Wordprocessing.Document();
            ImagePart image = mainDocumentPart.AddImagePart(ImagePartType.Jpeg);
            using (FileStream file = new FileStream(template.Name, FileMode.Open))
            {
                image.FeedData(file);
            }
            PageSetting settingpage = new PageSetting();
            DocumentsFull documentInvoce = new DocumentsFull();
            document.Append(settingpage.DocumentSettingVertical());
            document.Append(documentInvoce.Sticker(template, mainDocumentPart.GetIdOfPart(image)));
            mainDocumentPart.Document = document;
        }

        public Stream FileArray()
        {
            var file = File.ReadAllBytes(FullPathDocumentWord);
            File.Delete(FullPathDocumentWord);
            return new MemoryStream(file);
        }
    }
}
