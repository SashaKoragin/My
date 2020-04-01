using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using EfDatabase.Inventory.Base;

namespace EfDatabase.Inventory.MailLogicLotus
{
    /// <summary>
    /// Логика для писем поступивших на Outlook
    /// </summary>
   public class MailLogicLotus
    {
        public InventoryContext Inventory { get; set; }

        /// <summary>
        /// Работа с LOTUS
        /// </summary>
        public MailLogicLotus()
        {
            Inventory?.Dispose();
            Inventory = new InventoryContext();
        }
        /// <summary>
        /// Проверка есть ли такой документ в БД
        /// </summary>
        /// <param name="idMail"></param>
        /// <returns></returns>
        public bool IsExistsBdMail(string idMail)
        {
            var modelMail = from mail in Inventory.MailOutlooks
                    where mail.IdMail == idMail
                    select new {Mail = mail};
            return modelMail.Any();
        }
        /// <summary>
        /// Добавление документа
        /// </summary>
        /// <param name="mailLogic">Модель документа</param>
        public void AddModel(MailOutlook mailLogic)
        {
            Inventory.MailOutlooks.Add(mailLogic);
            Inventory.SaveChanges();
            Loggers.Log4NetLogger.Info(new Exception("Сохранили письмо с УН "+mailLogic.IdMail));
        }
        /// <summary>
        /// Получить описание файла
        /// </summary>
        /// <param name="idMail">Ун письма</param>
        /// <returns></returns>
        public string ReturnMailBody(int idMail){
            var mail = Inventory.MailOutlooks.Single(mails => mails.Id == idMail);
            return mail.Body;
        }
        /// <summary>
        /// Выгрузка файла вложения
        /// </summary>
        /// <param name="idMail">Ун письма</param>
        /// <returns></returns>
        public Stream OutputMail(int idMail){
            var mail = Inventory.MailOutlooks.Single(mails => mails.Id == idMail);
            return new MemoryStream(mail.FileMail);
        }
        /// <summary>
        /// Удаление письма
        /// </summary>
        /// <param name="idMail">Ун письма</param>
        /// <returns></returns>
        public string DeleteMail(int idMail){
            Inventory.Entry(new MailOutlook() {Id = idMail}).State = EntityState.Deleted;
            Inventory.SaveChanges();
            return $"Удаление почты уникальный номер {idMail} произведено";
        }
    }
}
