using System;
using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using LibaryDocumentGenerator.ProgrammView.FullDocument;
using LibaryDocumentGenerator.ProgrammView.Word.Template.SettingPage;

namespace LibaryDocumentGenerator.Documents.Template
{
    public class DocCodePdf417 : ITemplate<EfDatabaseAutomation.Automation.BaseLogica.AutoLogicInventory.ModelBarcode.Barcode[]>
    {
        public string FullPathDocumentWord { get; set; }
        public Stream FileArray()
        {
            var file = File.ReadAllBytes(FullPathDocumentWord);
            File.Delete(FullPathDocumentWord);
            return new MemoryStream(file);
        }

        public void CreateDocument(string path, EfDatabaseAutomation.Automation.BaseLogica.AutoLogicInventory.ModelBarcode.Barcode[] template, object obj = null)
        {
            FullPathDocumentWord = path + Constant.WordConstant.FormatWord;
            using (WordprocessingDocument package = WordprocessingDocument.Create(FullPathDocumentWord, WordprocessingDocumentType.Document))
            {
                CreateWord(package, template, obj);
                package.MainDocumentPart.Document.Save();
                package.Close();
            }
        }

        public void CreateWord(WordprocessingDocument package, EfDatabaseAutomation.Automation.BaseLogica.AutoLogicInventory.ModelBarcode.Barcode[] template, object obj = null)
        {
            MainDocumentPart mainDocumentPart = package.AddMainDocumentPart();
            DocumentFormat.OpenXml.Wordprocessing.Document document = new DocumentFormat.OpenXml.Wordprocessing.Document();
            PageSetting settingPage = new PageSetting();
            DocumentsFull documentInvoce = new DocumentsFull();
            document.Append(settingPage.AddDocumentZebraPrinter());
            document.Append(documentInvoce.CreatePdf417(template, mainDocumentPart));
            mainDocumentPart.Document = document;
        }
    }
}
