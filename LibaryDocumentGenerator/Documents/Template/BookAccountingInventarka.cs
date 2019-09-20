using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using EfDatabaseXsdBookAccounting;
using LibaryDocumentGenerator.ProgrammView.FullDocument;
using LibaryDocumentGenerator.ProgrammView.Word.Template.SettingPage;

namespace LibaryDocumentGenerator.Documents.Template
{
    public class BookAccountingInventarka : ITemplate<EfDatabaseXsdBookAccounting.Book>
    {
        /// <summary>
        /// Полный путь к книге на сервере
        /// </summary>
        public string Fullpathdocumentword { get; set; }


        /// <summary>
        /// Выгрузка и удаление файла книги отчета
        /// </summary>
        /// <returns></returns>
        public Stream FileArray()
        {
            var file = File.ReadAllBytes(Fullpathdocumentword);
            File.Delete(Fullpathdocumentword);
            return new MemoryStream(file);
        }

        public void CreateDocum(string path, Book template, object obj)
        {
            Fullpathdocumentword = path + template.BareCodeBook.NameModel + Constant.WordConstant.Formatword;
            using (WordprocessingDocument package = WordprocessingDocument.Create(Fullpathdocumentword, WordprocessingDocumentType.Document))
            {
                CreateWord(package, template, obj);
                package.MainDocumentPart.Document.Save();
                package.Close();
            }
        }

        public void CreateWord(WordprocessingDocument package, Book template, object obj)
        {
            MainDocumentPart mainDocumentPart = package.AddMainDocumentPart();
            DocumentFormat.OpenXml.Wordprocessing.Document document = new DocumentFormat.OpenXml.Wordprocessing.Document();
            ImagePart image = mainDocumentPart.AddImagePart(ImagePartType.Jpeg);
            using (FileStream file = new FileStream(template.BareCodeBook.FullPathSave, FileMode.Open))
            {
                image.FeedData(file);
            }
            PageSetting settingpage = new PageSetting();
            DocumentsFull documentInvoce = new DocumentsFull();
            document.Append(settingpage.DocumentSettingVertical());
            document.Append(documentInvoce.BookAccounting(template, mainDocumentPart.GetIdOfPart(image)));
            mainDocumentPart.Document = document;
        }
    }
}
