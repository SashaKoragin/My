using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using EfDatabaseAutomation.Automation.BaseLogica.SqlSelect.XsdDTOSheme;
using LibaryDocumentGenerator.ProgrammView.FullDocument;
using LibaryDocumentGenerator.ProgrammView.Word.Template.SettingPage;

namespace LibaryDocumentGenerator.Documents.Template
{
   public class ReportStatement : ITemplate<Statement>
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
        /// <summary>
        /// Создание документа выписки
        /// </summary>
        /// <param name="path">Путь к документу</param>
        /// <param name="template">Шаблон</param>
        /// <param name="obj"></param>
       public void CreateDocument(string path, Statement template, object obj = null)
        {
            FullPathDocumentWord = path + template.HeadingStatement[0].NameIndex + Constant.WordConstant.Formatword;
            using (WordprocessingDocument package = WordprocessingDocument.Create(FullPathDocumentWord, WordprocessingDocumentType.Document))
            {
                CreateWord(package, template, obj);
                package.MainDocumentPart.Document.Save();
                package.Close();
            }
        }
        /// <summary>
        /// Создание выписки выгрузки данных 
        /// </summary>
        /// <param name="package">Пакет</param>
        /// <param name="template">Шаблон</param>
        /// <param name="obj">Объект</param>
        public void CreateWord(WordprocessingDocument package, Statement template, object obj = null)
        {
            MainDocumentPart mainDocumentPart = package.AddMainDocumentPart();
            DocumentFormat.OpenXml.Wordprocessing.Document document = new DocumentFormat.OpenXml.Wordprocessing.Document();
            PageSetting settingPage = new PageSetting();
            GenerateStatementPreCheck documentStatement = new GenerateStatementPreCheck();
            document.Append(settingPage.DocumentSettingVertical(new PageMargin() { Top = 1135, Right = 567, Bottom = 567, Left = 1135 }));
            document.Append(documentStatement.GenerateStatementNote(template));
            mainDocumentPart.Document = document;
        }
    }
}
