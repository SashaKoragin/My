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
   public class MailLogicLotus : IDisposable
    {
        public InventoryContext Inventory { get; set; }

        /// <summary>
        /// Работа с LOTUS
        /// </summary>
        public MailLogicLotus()
        {
            Inventory = new InventoryContext();
        }
        /// <summary>
        /// Проверка есть ли такой документ в БД
        /// </summary>
        /// <param name="idMail">Ун письма</param>
        /// <returns></returns>
        public bool IsExistsBdMail(string idMail)
        {
            var modelMail = from mail in Inventory.MailLotusOutlookIns
                    where mail.IdMail == idMail
                    select new {Mail = mail};
            return modelMail.Any();
        }
        /// <summary>
        /// Проверка создана ли заявка на СТП 
        /// </summary>
        /// <param name="idCalendar">Ун календаря</param>
        /// <returns></returns>
        public bool IsCheckCreateTicketStp(int idCalendar)
        {
            return Inventory.Calendars.Single(x => x.Id == idCalendar).IsSTP;
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
        /// Добавление календаря в БД
        /// </summary>
        /// <param name="calendarVks">Календарь</param>
        public void AddModelCalendar(Calendar calendarVks)
        {
            Inventory.Calendars.Add(calendarVks);
            Inventory.SaveChanges();
            Loggers.Log4NetLogger.Info(new Exception("Сохранили Календарь ВКС с УН " + calendarVks.IdMail));
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
                case "MailInView":
                    var mailIn = Inventory.MailLotusOutlookIns.Single(mails => mails.IdMail == model.IdMail);
                    return mailIn.Body;
                case "MailOutView":
                    var mailOut = Inventory.MailLotusOutlookOuts.Single(mails => mails.IdMail == model.IdMail);
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
                case "MailInView":
                    var mailIn = Inventory.MailLotusOutlookIns.Single(mails => mails.IdMail == model.IdMail);
                    return new MemoryStream(mailIn.FileMail);
                case "MailOutView":
                    var mailOut = Inventory.MailLotusOutlookOuts.Single(mails => mails.IdMail == model.IdMail);
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
                    using (var context = new InventoryContext())
                    {
                        var mailInDb = from mailIn in context.MailIns
                            where mailIn.IdMail == model.IdMail
                            select new { MailIn = mailIn };
                        if (mailInDb.Any())
                        {
                            Inventory.Entry(new MailLotusOutlookIn() { Id = mailInDb.First().MailIn.Id }).State = EntityState.Deleted;
                            Inventory.SaveChanges();
                        }
                        var calendarDb = from calendar in context.Calendars
                            where calendar.IdMail == model.IdMail
                            select new { Calendars = calendar };
                        if (calendarDb.Any())
                        {
                            Inventory.Entry(new Calendar() { Id = calendarDb.First().Calendars.Id }).State = EntityState.Deleted;
                            Inventory.SaveChanges();
                        }
                    }
                    break;
                case "MailOut":
                    Inventory.Entry(new MailLotusOutlookOut() { IdMail = model.IdMail }).State = EntityState.Deleted;
                    Inventory.SaveChanges();
                    break;
                default:
                    return null;
            }
            return $"Удаление почты в модели {model.NameGroupModel} под уникальным номером {model.IdMail} произведено";
        }
        /// <summary>
        /// Обновление календаря по созданию заявки
        /// </summary>
        /// <param name="idCalendar">Id Календаря</param>
        public void ModifiedCalender(int idCalendar)
        {
            Calendar dbCalendar = Inventory.Calendars.First(x => x.Id == idCalendar);
            dbCalendar.IsSTP = true;
            Inventory.SaveChanges();
        }
        /// <summary>
        /// Disposing
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Inventory?.Dispose();
                Inventory = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }


    }
}
