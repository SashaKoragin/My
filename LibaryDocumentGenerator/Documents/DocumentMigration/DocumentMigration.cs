using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
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

        /// <summary>
        /// Шаблон писем по миграции НП 
        /// </summary>
        /// <param name="connectionstringtemplate">Строка соединения</param>
        /// <param name="path">Путь к папке сохранения</param>
        /// <param name="model">Модель миграции</param>
        public void MigrationDoc(string connectionstringtemplate, string path, MigrationParse model)
        {
            PageSetting settingpage = new PageSetting();
            HeadersDocuments headers = new HeadersDocuments();
            Single single = new Single();
            Fotters fotters = new Fotters();
            BodyDocument body = new BodyDocument();
            var setting = new FullSetting { UseTemplate =new UseTemplate() { IdTemplate = 2 }};
            SqlLibaryIfns.SqlModelReport.SqlTemplate.ModelTemplate template = new SqlLibaryIfns.SqlModelReport.SqlTemplate.ModelTemplate();
            var documenttemplate = template.Template(connectionstringtemplate, setting);
            model.ReportMigration.GroupBy(x => x.CodeIfns).ToList().ForEach(key =>
            {
                model.N280 = template.Inspection(connectionstringtemplate, key.Key).Inspection.N280;
                List<ReportMigration[]> report = new List<ReportMigration[]>();
                var fl = model.ReportMigration.Where(code => code.CodeIfns == key.Key && string.IsNullOrWhiteSpace(code.Kpp)).ToArray();
                var ul = model.ReportMigration.Where(code => code.CodeIfns == key.Key && (code.Kpp ?? string.Empty) != "").ToArray();
                if (fl.Length >= 1) { report.Add(fl);}
                if (ul.Length >= 1) { report.Add(ul); }
                foreach (var param in report)
                {
                    string fullpath;
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
                    using (WordprocessingDocument package = WordprocessingDocument.Create(fullpath, WordprocessingDocumentType.Document))
                    {
                        MainDocumentPart mainDocumentPart = package.AddMainDocumentPart();
                        DocumentFormat.OpenXml.Wordprocessing.Document doc = new DocumentFormat.OpenXml.Wordprocessing.Document();
                        fotters.FottersAddDocument(mainDocumentPart, documenttemplate);
                        doc.Append(settingpage.AddSetting(mainDocumentPart));
                        doc.Append(headers.DocumentsHeaders(documenttemplate, key.Key, model.N280, model.Otdel));
                        doc.Append(body.TextDocumentFormatMigration(documenttemplate));
                        doc.Append(body.GenerateMigrationTable(param,isshablon));
                        doc.Append(single.AddSingle(documenttemplate));
                        mainDocumentPart.Document = doc;
                        package.MainDocumentPart.Document.Save();
                        package.Close();
                    }
                }
                report.Clear();
            });
        }
    }
}
