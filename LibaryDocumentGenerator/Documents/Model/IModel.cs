using LibaryXMLAuto.ModelXmlSql.Model.FullSetting;

namespace LibaryDocumentGenerator.Documents.Model
{
    public interface IModel
    {
        /// <summary>
        /// Модель Отправки
        /// </summary>
        object Report { get; set; }
        /// <summary>
        /// Сам шаблон
        /// </summary>
        LibaryXMLAutoReports.FullTemplateSheme.Document DocumentTemplate { get; set; }
        /// <summary>
        /// Путь к папке с сохранениями
        /// </summary>
         string PathSave { get; set; }
        /// <summary>
        /// Строка соединения с БД шаблонами выведена в интерфейс для дальнейшей манипуляции
        /// </summary>
        string ConectionStringTemplate { get; set; }
    }
}