using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using LibaryDocumentGenerator.ProgrammView.Word.Template.BodyDocument;
using LibaryDocumentGenerator.ProgrammView.Word.Template.FottersDocument;
using LibaryDocumentGenerator.ProgrammView.Word.Template.HeadersDocument;
using LibaryDocumentGenerator.ProgrammView.Word.Template.SettingPage;
using LibaryXMLAuto.ModelXmlSql.Model.FullSetting;
using LibaryXMLAuto.ModelXmlAuto.MigrationReport;
using Single = LibaryDocumentGenerator.ProgrammView.Word.Template.SingleDocument.Single;

namespace LibaryDocumentGenerator.Documents.DocumentMigration
{
    /// <summary>
    /// Генерация документов о миграции
    /// </summary>
   public class DocumentMigration
    {
        private PageSetting SettingPage { get; }
        private HeadersDocuments Headers { get; }
        private Single Single { get; }
        private Fotters Fotters { get; }
        private BodyDocument Body { get; }
        public DocumentMigration()
        {
            SettingPage = new PageSetting();
            Headers = new HeadersDocuments();
            Single = new Single();
            Fotters = new Fotters();
            Body = new BodyDocument();
        }

        /// <summary>
        /// Шаблон писем по миграции НП 
        /// </summary>
        /// <param name="connectionStringTemplate">Строка соединения</param>
        /// <param name="path">Путь к папке сохранения</param>
        /// <param name="model">Модель миграции</param>
        public void MigrationDocument(string connectionStringTemplate, string path, MigrationParse model)
        {

        }
        /// <summary>
        /// Создание документов по Миграции
        /// </summary>
        /// <param name="fullPath">Полный путь к сохранению файла с именем</param>
        /// <param name="documentTemplate">Шаблон документа из БД</param>
        /// <param name="migration">Журнал миграции</param>
        /// <param name="model">Модель с параметрами</param>
        /// <param name="isTemplate">Шаблон ЮЛ или ФЛ</param>
        /// <param name="ifns">ИФНС номер инспекции</param>
        private void GenerateDoc(string fullPath, LibaryXMLAutoReports.FullTemplateSheme.Document documentTemplate, ReportMigration[] migration, MigrationParse model, int isTemplate, string ifns)
        {
            using (WordprocessingDocument package = WordprocessingDocument.Create(fullPath, WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainDocumentPart = package.AddMainDocumentPart();
                DocumentFormat.OpenXml.Wordprocessing.Document doc = new DocumentFormat.OpenXml.Wordprocessing.Document();
                Fotters.FottersAddDocument(mainDocumentPart, documentTemplate.Templates.Stone.Stone4, documentTemplate.Templates.Stone.Stone5);
                doc.Append(SettingPage.AddSetting(mainDocumentPart));
                doc.Append(Headers.HeaderDocumentIfns(documentTemplate, mainDocumentPart, ifns, model.N280, model.Otdel));
                doc.Append(Body.TextDocumentFormatMigration(documentTemplate));
                doc.Append(Body.GenerateMigrationTable(migration, isTemplate));
                doc.Append(Single.AddSingle(documentTemplate));
                mainDocumentPart.Document = doc;
                package.MainDocumentPart.Document.Save();
                package.Close();
            }
        }

    }
}
