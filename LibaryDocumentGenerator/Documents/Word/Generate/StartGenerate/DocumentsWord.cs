using System;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using LibaryDocumentGenerator.Documents.Template.ModelTemplate;
using LibaryDocumentGenerator.Documents.Word.Generate.UseDocument.BodyDocument;
using LibaryDocumentGenerator.Documents.Word.Generate.UseDocument.FottersDocument;
using LibaryDocumentGenerator.Documents.Word.Generate.UseDocument.HeadersDocument;
using LibaryDocumentGenerator.Documents.Word.Generate.UseDocument.SettingPage;
using LibaryXMLAuto.ModelXmlSql.Model.FullSetting;
using LibaryXMLAutoReports.ReportsBdk;
using Single = LibaryDocumentGenerator.Documents.Word.Generate.UseDocument.FottersDocument.Single;


namespace LibaryDocumentGenerator.Documents.Word.Generate.StartGenerate
{
   public class DocumentsWord
    {

        /// <summary>
        /// Старт сбора шаблона для документа отправки БДК
        /// </summary>
        /// <param name="connectionstringtemplate">Строка соединения с Шаблонами на сервере</param>
        /// <param name="connectionstringtaxes">Строка соединения с данными</param>
        /// <param name="path">Путь сохранения документов</param>
        public void StartWordBdk(string connectionstringtemplate, string connectionstringtaxes, string path, FullSetting setting)
        {
            try
            {
                var model = new ModelTemplateFull();
                var templatemodel = model.ModelOutBdk(connectionstringtemplate, connectionstringtaxes, path, setting);
                if (templatemodel.Report!=null)
                { 
                   foreach (var fn71 in templatemodel.Report.FN71)
                    { 
                      try
                       {
                        string fullpath = templatemodel.PathSave + fn71.N279 + "_CountBDK_" + fn71.FN1723_2.Length +
                                          model.ConstWord().Formatword;
                        CreateDocum(fullpath,templatemodel.DocumentTemplate.NameDocument.Template,fn71);

                       }
                      catch (Exception ex)
                       {
                        Loggers.Log4NetLogger.Error(ex);
                       }
                    }
                }
                else
                {
                    Loggers.Log4NetLogger.Info(new Exception("Нет данных для создания шаблонов писем по данным датам!!! Info FN1723_2.D85"));
                }
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
        }
        /// <summary>
        /// Непосредственное создание докумета на исходящие БДК о на отправки сообщения
        /// </summary>
        /// <param name="path">Путь сохранения</param>
        /// <param name="template">Шаблон</param>
        /// <param name="parametr">Параметры для генерации отчета FN71</param>
        public void CreateDocum(string path, LibaryXMLAutoReports.TemplateSheme.Template template,FN71 parametr = null)
        {
            using (WordprocessingDocument package = WordprocessingDocument.Create(path,WordprocessingDocumentType.Document))
            {
                CreateWord(package,template,parametr);
                package.MainDocumentPart.Document.Save();
                package.Close();
            }
        }

        /// <summary>
        /// Непосредственная сборка шаблона
        /// </summary>
        /// <param name="package">Пакет</param>
        /// <param name="template">Шаблон</param>
        /// <param name="parametr">Параметры</param>
        private void CreateWord(WordprocessingDocument package, LibaryXMLAutoReports.TemplateSheme.Template template,FN71 parametr)
        {
           MainDocumentPart mainDocumentPart = package.AddMainDocumentPart();
           DocumentFormat.OpenXml.Wordprocessing.Document document = new DocumentFormat.OpenXml.Wordprocessing.Document();
           HeadersDocuments headers = new HeadersDocuments();
           BodyDocument body = new BodyDocument();
           Single single = new Single();
           FottersDocument fotters = new FottersDocument();
           SettingPages settingpage = new SettingPages();
           fotters.FottersAddDocument(mainDocumentPart, template);
           document.Append(settingpage.AddSetting(mainDocumentPart));
           document.Append(headers.DocumentsHeaders(template, parametr.N279, parametr.N280));
           document.Append(body.TextDocument(template));
           document.Append(body.DocumentIshBdkTableBody(parametr));
           document.Append(single.AddSingle(template));
           mainDocumentPart.Document = document;
        }
    }
}
