using System.Collections.Generic;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using LibaryDocumentGenerator.ProgrammView.Word.Template.BodyDocument;
using LibaryDocumentGenerator.ProgrammView.Word.Template.FottersDocument;
using LibaryDocumentGenerator.ProgrammView.Word.Template.HeadersDocument;
using LibaryDocumentGenerator.ProgrammView.Word.Template.SettingPage;
using LibaryXMLAuto.ModelXmlSql.Model.FullSetting;
using LibaryXMLAutoModelXmlAuto.MigrationReport;
using Single = LibaryDocumentGenerator.ProgrammView.Word.Template.SingleDocument.Single;

namespace LibaryDocumentGenerator.Documents.DocumentMigration
{
    /// <summary>
    /// Генерация документов о миграции
    /// </summary>
   public class DocumentMigration
    {
        private PageSetting Settingpage { get; }
        private HeadersDocuments Headers { get; }
        private Single Single { get; }
        private Fotters Fotters { get; }
        private BodyDocument Body { get; }
        public DocumentMigration()
        {
            Settingpage = new PageSetting();
            Headers = new HeadersDocuments();
            Single = new Single();
            Fotters = new Fotters();
            Body = new BodyDocument();
        }

        /// <summary>
        /// Шаблон писем по миграции НП 
        /// </summary>
        /// <param name="connectionstringtemplate">Строка соединения</param>
        /// <param name="path">Путь к папке сохранения</param>
        /// <param name="model">Модель миграции</param>
        public void MigrationDoc(string connectionstringtemplate, string path, MigrationParse model)
        {
            SqlLibaryIfns.SqlModelReport.SqlTemplate.ModelTemplate template = new SqlLibaryIfns.SqlModelReport.SqlTemplate.ModelTemplate();
            var setting = new FullSetting { UseTemplate = new UseTemplate() { IdTemplate = 3 } };
            var documenttemplate = template.Template(connectionstringtemplate, setting);
            model.N280 = template.Inspection(connectionstringtemplate, "7746").Inspection.N280;
            var ul46 = model.ReportMigration.Where(code => (code.Kpp ?? string.Empty) != "" && code.Problem.Contains("Запись о налогоплательщике в ЦУН не содержит ОКТМО")).ToArray();
            var fullpath = path + "7746" + "_ЮЛ_" + Constant.WordConstant.Formatword;
            GenerateDoc(fullpath, documenttemplate, ul46, model, 1, "7746");
            setting.UseTemplate.IdTemplate = 2;
            documenttemplate = template.Template(connectionstringtemplate, setting);
            model.ReportMigration.GroupBy(x => x.CodeIfns).ToList().ForEach(key =>
            {
                model.N280 = template.Inspection(connectionstringtemplate, key.Key).Inspection.N280;
                List<ReportMigration[]> report = new List<ReportMigration[]>();
                var fl = model.ReportMigration.Where(code => code.CodeIfns == key.Key && string.IsNullOrWhiteSpace(code.Kpp)).ToArray();
                var ul = model.ReportMigration.Where(code => code.CodeIfns == key.Key && (code.Kpp ?? string.Empty) != "" && !code.Problem.Contains("Запись о налогоплательщике в ЦУН не содержит ОКТМО")).ToArray();
                if (fl.Length >= 1) { report.Add(fl);}
                if (ul.Length >= 1) { report.Add(ul); }
                foreach (var param in report)
                {
                    int isshablon;
                    if (!string.IsNullOrWhiteSpace(param[0].Kpp))
                    {
                        isshablon = 1;
                         fullpath = path + key.Key+ "_ЮЛ_" + Constant.WordConstant.Formatword;
                    }
                    else
                    {
                        isshablon = 2;
                        fullpath = path + key.Key + "_ФЛ_ИП_" + Constant.WordConstant.Formatword;
                    }
                    GenerateDoc(fullpath, documenttemplate, param, model, isshablon, key.Key);
                }
                report.Clear();
            });
        }
        /// <summary>
        /// Создание документов по Миграции
        /// </summary>
        /// <param name="fullpath">Полный путь к сохранению файла с именем</param>
        /// <param name="documenttemplate">Шаблон документа из БД</param>
        /// <param name="migration">Журнал миграции</param>
        /// <param name="model">Модель с параметрами</param>
        /// <param name="isshablon">Шаблон ЮЛ или ФЛ</param>
        /// <param name="ifns">ИФНС номер инспекции</param>
        private void GenerateDoc(string fullpath,LibaryXMLAutoReports.FullTemplateSheme.Document documenttemplate,ReportMigration[] migration, MigrationParse model, int isshablon,string ifns)
        {
            using (WordprocessingDocument package = WordprocessingDocument.Create(fullpath, WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainDocumentPart = package.AddMainDocumentPart();
                DocumentFormat.OpenXml.Wordprocessing.Document doc = new DocumentFormat.OpenXml.Wordprocessing.Document();
                Fotters.FottersAddDocument(mainDocumentPart, documenttemplate);
                doc.Append(Settingpage.AddSetting(mainDocumentPart));
                doc.Append(Headers.DocumentsHeaders(documenttemplate, ifns, model.N280, model.Otdel));
                doc.Append(Body.TextDocumentFormatMigration(documenttemplate));
                doc.Append(Body.GenerateMigrationTable(migration, isshablon));
                doc.Append(Single.AddSingle(documenttemplate));
                mainDocumentPart.Document = doc;
                package.MainDocumentPart.Document.Save();
                package.Close();
            }
        }

    }
}
