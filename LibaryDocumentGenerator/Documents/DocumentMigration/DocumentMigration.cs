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
        /// <param name="model">Модель миграции</param>\
        /// <param name="postAddress">Модель адреса</param>
        public void MigrationDocument(string connectionStringTemplate, string path, MigrationParse model, string postAddress = null)
        {
            ServiceRestLotus lotus = new ServiceRestLotus();
            SqlLibaryIfns.SqlModelReport.SqlTemplate.ModelTemplate template = new SqlLibaryIfns.SqlModelReport.SqlTemplate.ModelTemplate();
            var setting = new FullSetting { UseTemplate = new UseTemplate() { IdTemplate = 3 } };
            var documentTemplate = template.Template(connectionStringTemplate, setting);
            model.N280 = template.Inspection(connectionStringTemplate, "7746").Inspection.N280;
            var ul46 = model.ReportMigration.Where(code => (code.Kpp ?? string.Empty) != "" && code.Problem.Contains("Запись о налогоплательщике в ЦУН не содержит ОКТМО")).ToArray();
            var fullPath = path + "7746" + "_ЮЛ_" + Constant.WordConstant.Formatword;
            GenerateDoc(fullPath, documentTemplate, ul46, model, 1, "7746");
            setting.UseTemplate.IdTemplate = 2;
            documentTemplate = template.Template(connectionStringTemplate, setting);
            model.ReportMigration.GroupBy(x => x.CodeIfns).ToList().ForEach(key =>
            {
                model.N280 = template.Inspection(connectionStringTemplate, key.Key).Inspection.N280;
                List<ReportMigration[]> report = new List<ReportMigration[]>();
                var fl = model.ReportMigration.Where(code => code.CodeIfns == key.Key && string.IsNullOrWhiteSpace(code.Kpp)).ToArray();
                var ul = model.ReportMigration.Where(code => code.CodeIfns == key.Key && (code.Kpp ?? string.Empty) != "" && !code.Problem.Contains("Запись о налогоплательщике в ЦУН не содержит ОКТМО")).ToArray();
                if (fl.Length >= 1) { report.Add(fl);}
                if (ul.Length >= 1) { report.Add(ul); }
                foreach (var param in report)
                {
                    int isTemplate;
                    string fileName;
                    if (!string.IsNullOrWhiteSpace(param[0].Kpp))
                    {
                        isTemplate = 1;
                        fileName = key.Key + "_ЮЛ_" + Constant.WordConstant.Formatword;
                        fullPath = path + fileName;
                    }
                    else
                    {
                        isTemplate = 2;
                        fileName = key.Key + "_ФЛ_ИП_" + Constant.WordConstant.Formatword;
                        fullPath = path + fileName;
                    }
                    GenerateDoc(fullPath, documentTemplate, param, model, isTemplate, key.Key);
                    //Отправка на сервис в Lotus
                    if (postAddress != null)
                    {
                        Letter modeLetter = new Letter() {Attachments = new Attachment[1],DestinationCodes = new string[1]};
                        modeLetter.Attachments[0] = new Attachment
                        {
                            FileName = fileName,
                            Extension = Constant.WordConstant.Formatword,
                            Data = File.ReadAllBytes(fullPath)
                        };
                        modeLetter.Id = Guid.NewGuid().ToString();
                        modeLetter.Subscriber = documentTemplate.Templates.Stone.Stone3;
                        modeLetter.DestinationCodes[0] = key.Key;
                        modeLetter.File = "07-10";
                        modeLetter.Author = null;
                        lotus.ServicePostLotus(postAddress,modeLetter);
                    }
                }
                report.Clear();
            });
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
                Fotters.FottersAddDocument(mainDocumentPart, documentTemplate);
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
