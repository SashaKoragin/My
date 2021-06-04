using System.IO;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using EfDatabase.Inventory.Base;
using LibaryDocumentGenerator.ProgrammView.FullDocument;
using LibaryDocumentGenerator.ProgrammView.Word.Template.SettingPage;

namespace LibaryDocumentGenerator.Documents.Template
{
    /// <summary>
    /// Класс создания акта
    /// </summary>
   public class TemplateAct : ITemplate<Act[]>
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


        public void CreateDocument(string path, Act[] template, object obj = null)
        {
            FullPathDocumentWord = path + template.Where(act => act.ParameterAct.ClassParameterTemplate.NameClassParameter == "Head").Select(act => act.ParameterAct.Parameter).FirstOrDefault() + Constant.WordConstant.Formatword;
            using (WordprocessingDocument package = WordprocessingDocument.Create(FullPathDocumentWord, WordprocessingDocumentType.Document))
            {
                CreateWord(package, template, obj);
                package.MainDocumentPart.Document.Save();
                package.Close();
            }
        }

        public void CreateWord(WordprocessingDocument package, Act[] template, object obj = null)
        {
            MainDocumentPart mainDocumentPart = package.AddMainDocumentPart();
            DocumentFormat.OpenXml.Wordprocessing.Document document = new DocumentFormat.OpenXml.Wordprocessing.Document();
            PageSetting settingPage = new PageSetting();
            DocumentsFull documentInvoice = new DocumentsFull();
            document.Append(settingPage.DocumentSettingVertical(new PageMargin() { Top = 567, Right = 963, Bottom = 567, Left = 1020 }));
            document.Append(documentInvoice.Act(template));
            mainDocumentPart.Document = document;
        }
    }
}
