using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibaryXMLAutoReports.ReportsBdk;
using SqlLibaryIfns.SqlModelReport.Bdk;
using LibaryXMLAutoReports.TemplateSheme;

namespace LibaryDocumentGenerator.Documents.Template.ModelFullWord
{
    /// <summary>
    /// модель отправки БДК с Шаблонами 
    /// </summary>
    public class ModelOutBdk
    {
        /// <summary>
        /// Модель Отправки
        /// </summary>
        public Report Report { get; set; }
        /// <summary>
        /// Сам шаблон
        /// </summary>
        public Document DocumentTemplate { get; set; }
        /// <summary>
        /// Путь к папке с сохранениями
        /// </summary>
        public string PathSave { get; set; }
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="connectionstringtemplate">Строка соединения с шаблонами</param>
        /// <param name="connectionstringtaxes">Строка соединения с Данными</param>
        /// <param name="path">Путоь сохранения документов</param>
        public ModelOutBdk(string connectionstringtemplate, string connectionstringtaxes, string path)
        {
            ModelFull modelFull = new ModelFull();
            SqlLibaryIfns.SqlModelReport.SqlTemplate.ModelTemplate template = new SqlLibaryIfns.SqlModelReport.SqlTemplate.ModelTemplate();
            PathSave = path;
            Report = modelFull.ReportBdk(connectionstringtaxes);
            DocumentTemplate = template.Template(connectionstringtemplate);
        }
    }
}
