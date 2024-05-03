using System.Collections.Generic;
using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using EfDatabase.Inventory.Base;
using LibaryDocumentGenerator.ProgrammView.FullDocument;
using LibaryDocumentGenerator.ProgrammView.Word.Template.SettingPage;
using File = System.IO.File;

namespace LibaryDocumentGenerator.Documents.Template
{
    public class DocCode128 : ITemplate<List<AllTechnic>>
    {
        public string FullPathDocumentWord { get; set; }

        public void CreateDocument(string path, List<AllTechnic> template, object obj = null)
        {
            FullPathDocumentWord = path + Constant.WordConstant.FormatWord;
            using (WordprocessingDocument package = WordprocessingDocument.Create(FullPathDocumentWord, WordprocessingDocumentType.Document))
            {
                CreateWord(package, template, obj);
                package.MainDocumentPart.Document.Save();
                package.Close();
            }
        }

        public void CreateWord(WordprocessingDocument package, List<AllTechnic> template, object obj = null)
        {
            MainDocumentPart mainDocumentPart = package.AddMainDocumentPart();
            DocumentFormat.OpenXml.Wordprocessing.Document document = new DocumentFormat.OpenXml.Wordprocessing.Document();
            PageSetting settingPage = new PageSetting();
            DocumentsFull documentInv = new DocumentsFull();
            document.Append(settingPage.AddDocumentZebraPrinter());
            document.Append(documentInv.CreateStickerCode128(template, mainDocumentPart));
            mainDocumentPart.Document = document;
        }

        public Stream FileArray()
        {
            var file = System.IO.File.ReadAllBytes(FullPathDocumentWord);
            File.Delete(FullPathDocumentWord);
            return new MemoryStream(file);
        }
    }
}
