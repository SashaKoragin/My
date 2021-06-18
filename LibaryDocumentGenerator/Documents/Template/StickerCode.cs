using System.Collections.Generic;
using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using EfDatabase.Inventory.Base;
using LibaryDocumentGenerator.ProgrammView.FullDocument;
using LibaryDocumentGenerator.ProgrammView.Word.Template.SettingPage;

namespace LibaryDocumentGenerator.Documents.Template
{
    /// <summary>
    /// Класс генерации наклеек
    /// </summary>
    public class StickerCode : ITemplate<List<AllTechnic>>
    {
        public string FullPathDocumentWord { get; set; }

        public void CreateDocument(string path, List<AllTechnic> template, object obj=null)
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
            DocumentsFull documentInvoce = new DocumentsFull();
            document.Append(settingPage.ParametrPageHorizontEditMargin(new PageMargin() {Left = 500, Right = 500, Bottom = 500, Top = 500 }));
            document.Append(documentInvoce.Sticker(template, mainDocumentPart));
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
