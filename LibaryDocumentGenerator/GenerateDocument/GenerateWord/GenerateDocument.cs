using System;
using LibaryDocumentGenerator.Documents.Document;
using LibaryXMLAuto.ModelXmlSql.Model.FullSetting;
using LibaryDocumentGenerator.Documents.Model;
using LibaryDocumentGenerator.Documents.Template;
using LibaryXMLAutoReports.ReportsBdk;

namespace LibaryDocumentGenerator.GenerateDocument.GenerateWord
{
    /// <summary>
    /// Класс генерации документа
    /// </summary>
   public class GenerateDocument
    {
        /// <summary>
        /// Генерация документа OutBdk
        /// </summary>
        /// <param name="connectionstringtemplate">Строка соединения с шаблоном</param>
        /// <param name="connectionstringtaxes">Строка соединения с данными</param>
        /// <param name="path">Путь сохранения файлов</param>
        /// <param name="setting">Настройки</param>
        public void GenerateOutBdk(string connectionstringtemplate, string connectionstringtaxes, string path, FullSetting setting)
        {
            try
            {
                var document = new Doc
                {
                    Template = new TemplateOutBdk(),
                    Model = new OutBdkModel(connectionstringtemplate, connectionstringtaxes, path, setting)
                };
                document.DocumentOutBdk();
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
        }
    }
}
