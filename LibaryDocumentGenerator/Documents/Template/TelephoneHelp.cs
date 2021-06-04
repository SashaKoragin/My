using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using LibaryDocumentGenerator.ProgrammView.FullDocument;
using LibaryDocumentGenerator.ProgrammView.Word.Template.SettingPage;

namespace LibaryDocumentGenerator.Documents.Template
{
   public class TelephoneHelp: ITemplate<EfDatabaseTelephoneHelp.TelephoneHelp>
    {
        /// <summary>
        /// Путь сохранения документа
        /// </summary>
        public string FullPathDocumentWord { get; set; }

        /// <summary>
        /// Выгрузка и удаление файла тедлефонного справочника
        /// </summary>
        /// <returns></returns>
        public Stream FileArray()
        {
            var file = File.ReadAllBytes(FullPathDocumentWord);
            File.Delete(FullPathDocumentWord);
            return new MemoryStream(file);
        }


        /// <summary>
        /// Создание документа справочника
        /// </summary>
        /// <param name="path">Путь сохранения документа</param>
        /// <param name="template">Шаблон</param>
        /// <param name="obj"></param>
        public void CreateDocument(string path, EfDatabaseTelephoneHelp.TelephoneHelp template, object obj)
        {
            FullPathDocumentWord = path + "Телефонный справочник инспекции" + Constant.WordConstant.Formatword;
            using (WordprocessingDocument package = WordprocessingDocument.Create(FullPathDocumentWord, WordprocessingDocumentType.Document))
            {
                CreateWord(package, template, obj);
                package.MainDocumentPart.Document.Save();
                package.Close();
            }
        }
        /// <summary>
        /// Создание xml Word
        /// </summary>
        /// <param name="package"></param>
        /// <param name="template"></param>
        /// <param name="obj"></param>
        public void CreateWord(WordprocessingDocument package, EfDatabaseTelephoneHelp.TelephoneHelp template, object obj)
        {
            MainDocumentPart mainDocumentPart = package.AddMainDocumentPart();
            DocumentFormat.OpenXml.Wordprocessing.Document document = new DocumentFormat.OpenXml.Wordprocessing.Document();
            PageSetting settingpage = new PageSetting();
            DocumentsFull documentInvoce = new DocumentsFull();
            document.Append(settingpage.DocumentSettingVertical());
            document.Append(documentInvoce.DocumentsTelephoneHelp(template));
            mainDocumentPart.Document = document;
        }
    }
}
