using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryDocumentGenerator.Documents.DocumentMigration
{
    /// <summary>
    /// Письмо для отправки в одну из инспекций ФНС
    /// </summary>
    public class Letter
    {
        /// <summary>
        /// Уникальный номер письма на стороне клиента. <remarks>Для предотвращения задваивания</remarks>
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Имя сертификата Лотус автора письма (исполнителя)
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Номер дела. <code>Пример: 21-26</code>
        /// </summary>
        public string File { get; set; }

        /// <summary>
        /// Коды инспекций - получателей <code>new string[] {"7726", "7731};</code>
        /// </summary>
        public string[] DestinationCodes { get; set; }

        /// <summary>
        /// Вложения
        /// </summary>
        public Attachment[] Attachments { get; set; }

        /// <summary>
        /// Подписант письма <remarks>Если Null - вычисляется по номеру дела - название отдела и курирующий зам</remarks>
        /// </summary>
        public string Subscriber { get; set; }
    }

    /// <summary>
    /// Класс для вложения
    /// </summary>
    public class Attachment
    {
        /// <summary>
        /// Расширение. Например, <code>.docx</code>
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// Данные (массив)
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        /// Имя файла-вложения
        /// </summary>
        public string FileName { get; set; }
    }

    /// <summary>
    /// Подтверждение обработки письма
    /// </summary>
    public class Confirmation
    {
        /// <summary>
        /// Уникальный номер письма на стороне клиента. Как передан в <see cref="Letter.Id"/>
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Код ошибки - 0 если всё в порядке
        /// </summary>
        public int ErorCode { get; set; }

        /// <summary>
        /// Lotus document unique id для письма
        /// </summary>
        public string Unid { get; set; }
    }
}
