using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using EfDatabase.Inventory.Base;
using EfDatabaseXsdMail;

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
            var modelMail = from mail in Inventory.MailLotusOutlookIns
                    where mail.IdMail == idMail
                    select new {Mail = mail};
            return modelMail.Any();
        }
        /// <summary>
        /// Добавление документа
        /// </summary>
        /// <param name="mailIn">Принятое письмо с почты</param>
        public void AddModelMailIn(MailLotusOutlookIn mailIn)
        {
            Inventory.MailLotusOutlookIns.Add(mailIn);
            Inventory.SaveChanges();
            Loggers.Log4NetLogger.Info(new Exception("Сохранили письмо с УН "+ mailIn.IdMail));
        }
        /// <summary>
        /// Отправка письма с почты
        /// </summary>
        /// <param name="mailOut">Отправка письма</param>
        public void AddModelMailOut(MailLotusOutlookOut mailOut)
        {
            Inventory.MailLotusOutlookOuts.Add(mailOut);
            Inventory.SaveChanges();
            Loggers.Log4NetLogger.Info(new Exception("Сохранили письмо с УН " + mailOut.IdMail));
        }

        /// <summary>
        /// Получить описание файла
        /// </summary>
        /// <param name="model">Модель почты</param>
        /// <returns></returns>
        public string ReturnMailBody(WebMailModel model)
        {
            switch (model.NameGroupModel)
            {
                case "MailIn":
                    var mailIn = Inventory.MailLotusOutlookIns.Single(mails => mails.Id == model.Id);
                    return mailIn.Body;
                case "MailOut" :
                    var mailOut = Inventory.MailLotusOutlookOuts.Single(mails => mails.Id == model.Id);
                    return mailOut.Body;
                default:
                    return null;
            }
        }
        /// <summary>
        /// Выгрузка файла вложения
        /// </summary>
        /// <param name="model">Модель почты</param>
        /// <returns></returns>
        public Stream OutputMail(WebMailModel model)
        {
            switch (model.NameGroupModel)
            {
                case "MailIn":
                    var mailIn = Inventory.MailLotusOutlookIns.Single(mails => mails.Id == model.Id);
                    return new MemoryStream(mailIn.FileMail);
                case "MailOut":
                    var mailOut = Inventory.MailLotusOutlookOuts.Single(mails => mails.Id == model.Id);
                    return  new MemoryStream(mailOut.FileMailZip);
                default:
                    return null;
            }
        }
        /// <summary>
        /// Удаление письма
        /// </summary>
        /// <param name="model">Модель почты</param>
        /// <returns></returns>
        public string DeleteMail(WebMailModel model)
        {
            switch (model.NameGroupModel)
            {
                case "MailIn":
                    Inventory.Entry(new MailLotusOutlookIn() { Id = model.Id }).State = EntityState.Deleted;
                    Inventory.SaveChanges();
                    break;
                case "MailOut":
                    Inventory.Entry(new MailLotusOutlookOut() { Id = model.Id }).State = EntityState.Deleted;
                    Inventory.SaveChanges();
                    break;
                default:
                    return null;
            }
            return $"Удаление почты в модели {model.NameGroupModel} под уникальным номером {model.Id} произведено";
        }
    }
}
