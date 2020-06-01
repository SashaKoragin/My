using System;
using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using LibaryDocumentGenerator.ProgrammView.FullDocument;
using LibaryDocumentGenerator.ProgrammView.Word.Template.SettingPage;

using LibaryXMLAutoModelXmlSql.PreCheck.ModelCard;

namespace LibaryDocumentGenerator.Documents.Template
{
   public class ReportNote : ITemplate<CardFaceUl>
    {
        /// <summary>
        /// Полный путь к книге на сервере
        /// </summary>
        public string FullPathDocumentWord { get; set; }

        /// <summary>
        /// Выгрузка и удаление файла докладной записки ЮЛ
        /// </summary>
        /// <returns></returns>
        public Stream FileArray()
        {
            var file = File.ReadAllBytes(FullPathDocumentWord);
            File.Delete(FullPathDocumentWord);
            return new MemoryStream(file);
        }


        public void CreateDocum(string path, CardFaceUl template, object obj)
        {
            FullPathDocumentWord = path + template.FaceUl.Inn + Constant.WordConstant.Formatword;
            using (WordprocessingDocument package = WordprocessingDocument.Create(FullPathDocumentWord, WordprocessingDocumentType.Document))
            {
                CreateWord(package, template, obj);
                package.MainDocumentPart.Document.Save();
                package.Close();
            }
        }

        public void CreateWord(WordprocessingDocument package, CardFaceUl template, object obj)
        {
            MainDocumentPart mainDocumentPart = package.AddMainDocumentPart();
            DocumentFormat.OpenXml.Wordprocessing.Document document = new DocumentFormat.OpenXml.Wordprocessing.Document();
            PageSetting settingPage = new PageSetting();
            DocumentsPreChek documentInvoice = new DocumentsPreChek();
            document.Append(settingPage.DocumentSettingVertical(new PageMargin() { Top = 1135, Right = 567, Bottom = 567, Left = 1135 }));
            document.Append(documentInvoice.GenerateReportNote(template));
            mainDocumentPart.Document = document;
        }
    }
}
