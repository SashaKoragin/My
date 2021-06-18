using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using LibaryDocumentGenerator.ProgrammView.FullDocument;
using LibaryDocumentGenerator.ProgrammView.Word.Template.SettingPage;

namespace LibaryDocumentGenerator.Documents.Template
{
   public class TemplateJournalAis3 : ITemplate<EfDatabase.Journal.AllJournal>
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
         //   File.Delete(FullPathDocumentWord);
            return new MemoryStream(file);
        }


        public void CreateDocument(string path, EfDatabase.Journal.AllJournal template, object obj)
        {
            FullPathDocumentWord = path + "Журнал" + Constant.WordConstant.FormatWord;
            using (WordprocessingDocument package = WordprocessingDocument.Create(FullPathDocumentWord, WordprocessingDocumentType.Document))
            {
                CreateWord(package, template, obj);
                package.MainDocumentPart.Document.Save();
                package.Close();
            }
        }

        public void CreateWord(WordprocessingDocument package, EfDatabase.Journal.AllJournal template, object obj)
        {
            MainDocumentPart mainDocumentPart = package.AddMainDocumentPart();
            DocumentFormat.OpenXml.Wordprocessing.Document document = new DocumentFormat.OpenXml.Wordprocessing.Document();
            PageSetting settingPage = new PageSetting();
            DocumentsFull documentInvoice = new DocumentsFull();
            document.Append(settingPage.ParametrPageHorizontEditMargin(new PageMargin() { Top = 1701, Right = 1134, Bottom = 850, Left = 1134 }));
            document.Append(documentInvoice.CreateJournal(template));
            mainDocumentPart.Document = document;
        }
    }
}
