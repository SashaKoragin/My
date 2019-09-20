using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using EfDatabase.Inventarization.Base;
using EfDatabase.Inventarization.ReportSheme.ReturnModelError;
using EfDatabaseUploadFile;
using EfDatabaseXsdBookAccounting;
using Supply = EfDatabase.Inventarization.Base.Supply;

namespace EfDatabase.Inventarization.BaseLogica.AddObjectDb
{
   public class AddObjectDb : IDisposable
    {
        public InventarizationContext Inventarization { get; set; }

        public AddObjectDb()
        {
            Inventarization?.Dispose();
            Inventarization = new InventarizationContext();
        }


        /// <summary>
        /// Добавление или обновление пользователя
        /// </summary>
        /// <param name="user"></param>
        public ModelReturn AddAndEditUser(User user)
        {
            HistoryLog.HistoryLog log = new HistoryLog.HistoryLog();
            var usersAddadnModified = new User()
            {
                IdUser = user.IdUser,
                Name = user.Name,
                SmallName = user.SmallName,
                IdOtdel = user.IdOtdel,
                IdPosition = user.IdPosition,
                IdRule = user.IdRule,
                TabelNumber = user.TabelNumber,
                IdTelephon = user.IdTelephon,
                NameUser = user.NameUser,
                Passwords = user.Passwords,
                StatusActual = user.StatusActual,
                IdHistory = user.IdHistory,

            };
            try
            {
                if ((from Users in Inventarization.Users
                 where Users.IdUser == usersAddadnModified.IdUser
                 select new {Users}).Any())
               {
                  Inventarization.Entry(usersAddadnModified).State = EntityState.Modified;
                  Inventarization.SaveChanges();
                  log.GenerateHistory(usersAddadnModified.IdHistory, 1, "Обновление данных о пользователе: " + usersAddadnModified.IdUser);
                  return new ModelReturn("Обновили пользователя: " + usersAddadnModified.IdUser);  //"Обновили пользователя: " + user.IdUser;
               }
                  Inventarization.Users.Add(usersAddadnModified);
                  Inventarization.SaveChanges();
                  log.GenerateHistory(usersAddadnModified.IdHistory, 1, "Добавление нового пользователя: " + usersAddadnModified.IdUser);
                  return new ModelReturn("Добавили пользователя: " + usersAddadnModified.IdUser, usersAddadnModified.IdUser, usersAddadnModified.IdHistory);
             }
             catch (Exception e)
             {
                Loggers.Log4NetLogger.Error(e);
             }
             return new ModelReturn("При обновлении/добавлении данных 'Пользователь' по : " + usersAddadnModified.IdOtdel + " произошла ошибка смотри log.txt");
        }
        /// <summary>
        /// Добавление отдела без лога данных проверим как работает
        /// </summary>
        /// <param name="otdel">Отдел</param>
        /// <returns></returns>
        public ModelReturn AddAndEditOtdel(Otdel otdel)
        {
            var otdelAddadnModified = new Otdel()
            {
                IdOtdel = otdel.IdOtdel,
                IdUser = otdel.IdUser,
                NameOtdel = otdel.NameOtdel
            };
            try
            {
              if ((from Otdels in Inventarization.Otdels
                 where Otdels.IdOtdel == otdelAddadnModified.IdOtdel
                 select new { Otdels }).Any())
                {
                  Inventarization.Entry(otdelAddadnModified).State = EntityState.Modified;
                  Inventarization.SaveChanges();
                  return new ModelReturn("Обновили отдел: "+ otdelAddadnModified.IdOtdel);
                }
                  Inventarization.Otdels.Add(otdelAddadnModified);
                  Inventarization.SaveChanges();
                  return new ModelReturn("Добавили отдел: " + otdelAddadnModified.IdOtdel, otdelAddadnModified.IdOtdel);
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
            return new ModelReturn("При обновлении/добавлении данных 'Отдел' по : " + otdelAddadnModified.IdOtdel + " произошла ошибка смотри log.txt");
        }


        /// <summary>
        /// Добавление или обновление Принтера
        /// </summary>
        /// <param name="printer"></param>
        public ModelReturn AddAndEditPrinter(Printer printer)
        {
            HistoryLog.HistoryLog log = new HistoryLog.HistoryLog();
            var printerAddadnModified = new Printer()
            {
                IdPrinter = printer.IdPrinter,
                IdUser = printer.IdUser,
                IdProizvoditel = printer.IdProizvoditel,
                IdModel = printer.IdModel,
                IdNumberKabinet = printer.IdNumberKabinet,
                IdSupply = printer.IdSupply,
                ZavNumber = printer.ZavNumber,
                ServiceNumber = printer.ServiceNumber,
                InventarNumber = printer.InventarNumber,
                IzmInventarNumber = printer.IzmInventarNumber,
                IpAdress = printer.IpAdress,
                Coment = printer.Coment,
                IdStatus = printer.IdStatus,
                IdHistory = printer.IdHistory
            };
            try
            {
              if ((from Printers in Inventarization.Printers
                 where Printers.IdPrinter == printerAddadnModified.IdPrinter
                 select new {Printers}).Any())
                {
                 Inventarization.Entry(printerAddadnModified).State = EntityState.Modified;
                 Inventarization.SaveChanges();
                 log.GenerateHistory(printer.IdHistory, 1, "Обновление данных о пользователе: Пользователь принтер");
                 return new ModelReturn("Обновили принтер: " + printer.IdPrinter);
                }
                Inventarization.Printers.Add(printerAddadnModified);
                Inventarization.SaveChanges();
                log.GenerateHistory(printerAddadnModified.IdHistory, 1, "Добавление нового пользователя: Пользователь принтер");
                return new ModelReturn("Добавили принтер: " + printerAddadnModified.IdPrinter, printerAddadnModified.IdPrinter, printerAddadnModified.IdHistory);
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
            return new ModelReturn("При обновлении/добавлении данных 'Принтер' по : " + printerAddadnModified.IdPrinter + " произошла ошибка смотри log.txt");
        }

        /// <summary>
        /// Добавление или обновление Сканера
        /// </summary>
        /// <param name="scaner"></param>
        public ModelReturn AddAndEditScaner(ScanerAndCamer scaner)
        {
            HistoryLog.HistoryLog log = new HistoryLog.HistoryLog();
            var scanerAddadnModified = new ScanerAndCamer()
            {
                IdScaner = scaner.IdScaner,
                IdUser = scaner.IdUser,
                IdProizvoditel = scaner.IdProizvoditel,
                IdModel = scaner.IdModel,
                IdNumberKabinet = scaner.IdNumberKabinet,
                IdSupply = scaner.IdSupply,
                ZavNumber = scaner.ZavNumber,
                ServiceNumber = scaner.ServiceNumber,
                InventarNumber = scaner.InventarNumber,
                IzmInventarNumber = scaner.IzmInventarNumber,
                IpAdress = scaner.IpAdress,
                Coment = scaner.Coment,
                IdStatus = scaner.IdStatus,
                IdHistory = scaner.IdHistory
            };
            try
            {
              if ((from Scaner in Inventarization.ScanerAndCamers
                     where Scaner.IdScaner == scanerAddadnModified.IdScaner
                     select new{Scaner}).Any())
               {
                Inventarization.Entry(scanerAddadnModified).State = EntityState.Modified;
                Inventarization.SaveChanges();
                log.GenerateHistory(scanerAddadnModified.IdHistory, 1, "Обновление данных о пользователе: Пользователь сканер");
                return new ModelReturn("Обновили сканер: " + scanerAddadnModified.IdScaner);
               }
                Inventarization.ScanerAndCamers.Add(scanerAddadnModified);
                Inventarization.SaveChanges();
                log.GenerateHistory(scanerAddadnModified.IdHistory, 1, "Добавление нового пользователя: Пользователь сканер");
                return new ModelReturn("Добавили сканер: " + scanerAddadnModified.IdScaner, scanerAddadnModified.IdScaner, scanerAddadnModified.IdHistory);
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
            return new ModelReturn("При обновлении/добавлении данных 'Cканер' по : " + scanerAddadnModified.IdScaner + " произошла ошибка смотри log.txt");
        }

        /// <summary>
        /// Добавление или обновление МФУ
        /// </summary>
        /// <param name="mfu"></param>
        public ModelReturn AddAndEditMfu(Mfu mfu)
        {
            HistoryLog.HistoryLog log = new HistoryLog.HistoryLog();
            var mfuAddadnModified = new Mfu()
            {
                IdMfu = mfu.IdMfu,
                IdUser = mfu.IdUser,
                IdProizvoditel = mfu.IdProizvoditel,
                IdModel = mfu.IdModel,
                IdSupply = mfu.IdSupply,
                IdNumberKabinet = mfu.IdNumberKabinet,
                ZavNumber = mfu.ZavNumber,
                ServiceNumber = mfu.ServiceNumber,
                InventarNumber = mfu.InventarNumber,
                IzmInventarNumber = mfu.IzmInventarNumber,
                IpAdress = mfu.IpAdress,
                Coment = mfu.Coment,
                IdCopySave = mfu.IdCopySave,
                IdStatus = mfu.IdStatus,
                IdHistory = mfu.IdHistory,
            };
            try
            {
             if ((from Mfu in Inventarization.Mfus
                  where Mfu.IdMfu == mfuAddadnModified.IdMfu
                  select new {Mfu}).Any())
             {
                Inventarization.Entry(mfuAddadnModified).State = EntityState.Modified;
                Inventarization.SaveChanges();
                log.GenerateHistory(mfuAddadnModified.IdHistory, 1, "Обновление данных о пользователе: Пользователь МФУ");
                return new ModelReturn("Обновили МФУ: " + mfuAddadnModified.IdMfu);
             }
                Inventarization.Mfus.Add(mfuAddadnModified);
                Inventarization.SaveChanges();
                log.GenerateHistory(mfuAddadnModified.IdHistory, 1, "Добавление нового пользователя: Пользователь МФУ");
                return new ModelReturn("Добавили МФУ: " + mfuAddadnModified.IdMfu, mfuAddadnModified.IdMfu, mfuAddadnModified.IdHistory);
             }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
            return new ModelReturn("При обновлении/добавлении данных 'МФУ' по : " + mfuAddadnModified.IdMfu + " произошла ошибка смотри log.txt");
        }

        /// <summary>
        /// Добавление или обновление Системного блока
        /// </summary>
        /// <param name="sysblok"></param>
        public ModelReturn AddAndEditSysBlok(SysBlock sysblok)
        {
            HistoryLog.HistoryLog log = new HistoryLog.HistoryLog();
            var sysblokAddadnModified = new SysBlock()
            {
                IdSysBlock = sysblok.IdSysBlock,
                IdUser = sysblok.IdUser,
                IdModelSysBlock = sysblok.IdModelSysBlock,
                IdNumberKabinet = sysblok.IdNumberKabinet,
                ServiceNum = sysblok.ServiceNum,
                SerNum = sysblok.SerNum,
                IdSupply = sysblok.IdSupply,
                InventarNumSysBlok = sysblok.InventarNumSysBlok,
                NameComputer = sysblok.NameComputer,
                IpAdress = sysblok.IpAdress,
                Coment = sysblok.Coment,
                IdStatus = sysblok.IdStatus,
                IdHistory = sysblok.IdHistory,
            };
            try
            {
              if ((from SysBlocks in Inventarization.SysBlocks
                     where SysBlocks.IdSysBlock == sysblokAddadnModified.IdSysBlock
                     select new {SysBlocks}).Any())
                 {
                  Inventarization.Entry(sysblokAddadnModified).State = EntityState.Modified;
                  Inventarization.SaveChanges();
                  log.GenerateHistory(sysblokAddadnModified.IdHistory, 1, "Обновление данных о системном блоке: Пользователь Системный блок");
                  return new ModelReturn("Обновили Системный блок: " + sysblokAddadnModified.IdSysBlock);
                 }
                  Inventarization.SysBlocks.Add(sysblokAddadnModified);
                  Inventarization.SaveChanges();
                  log.GenerateHistory(sysblokAddadnModified.IdHistory, 1, "Добавление системного блока: Пользователь Системный блок");
                  return new ModelReturn("Добавили Системный блок: " + sysblokAddadnModified.IdSysBlock, sysblokAddadnModified.IdSysBlock, sysblokAddadnModified.IdHistory);
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
            return new ModelReturn("При обновлении/добавлении данных 'Системный блок' по : " + sysblokAddadnModified.IdSysBlock+ " произошла ошибка смотри log.txt");
        }


        /// <summary>
        /// Добавление или обновление Монитора
        /// </summary>
        /// <param name="monitor"></param>
        public ModelReturn AddAndEditMonitors(Monitor monitor)
        {
            HistoryLog.HistoryLog log = new HistoryLog.HistoryLog();
            var monitorAddadnModified = new Monitor()
            {
                IdMonitor = monitor.IdMonitor,
                IdUser = monitor.IdUser,
                IdSupply = monitor.IdSupply,
                IdModelMonitor = monitor.IdModelMonitor,
                IdNumberKabinet = monitor.IdNumberKabinet,
                SerNum = monitor.SerNum,
                InventarNumMonitor = monitor.InventarNumMonitor,
                Coment = monitor.Coment,
                IdStatus = monitor.IdStatus,
                IdHistory = monitor.IdHistory
            };
            try
            {
             if ((from Monitor in Inventarization.Monitors
                 where Monitor.IdMonitor == monitorAddadnModified.IdMonitor
                 select new { Monitor }).Any())
               {
                 Inventarization.Entry(monitorAddadnModified).State = EntityState.Modified;
                 Inventarization.SaveChanges();
                 log.GenerateHistory(monitorAddadnModified.IdHistory, 1, "Обновление данных о мониторе: Пользователь Монитор");
                 return new ModelReturn("Обновили Монитор: " + monitorAddadnModified.IdMonitor);
               }
                 Inventarization.Monitors.Add(monitorAddadnModified);
                 Inventarization.SaveChanges();
                 log.GenerateHistory(monitorAddadnModified.IdHistory, 1, "Добавление монитора: Пользователь Монитор");
                 return new ModelReturn("Добавили Монитор: " + monitorAddadnModified.IdMonitor, monitorAddadnModified.IdMonitor, monitorAddadnModified.IdHistory);
             }
             catch (Exception e)
             {
                Loggers.Log4NetLogger.Error(e);
             }
            return new ModelReturn("При обновлении/добавлении данных 'Монитор' по : " + monitorAddadnModified.IdMonitor + " произошла ошибка смотри log.txt");
        }

        /// <summary>
        /// Добавление или обновление Телефона
        /// </summary>
        /// <param name="telephone"></param>
        public ModelReturn AddAndEditTelephone(Telephon telephone)
        {
            var telephoneAddadnModified = new Telephon()
            {
                IdTelephon = telephone.IdTelephon,
                IdSupply = telephone.IdSupply,
                IdNumberKabinet = telephone.IdNumberKabinet,
                SerNumber = telephone.SerNumber,
                NameTelephone = telephone.NameTelephone,
                Telephon_ = telephone.Telephon_,
                TelephonUndeground = telephone.TelephonUndeground,
                IpTelephon = telephone.IpTelephon,
                MacTelephon = telephone.MacTelephon,
                IdStatus = telephone.IdStatus,
                Coment = telephone.Coment
            };
            try
            {
              if ((from Telephone in Inventarization.Telephons
                 where Telephone.IdTelephon == telephoneAddadnModified.IdTelephon
                 select new { Telephone }).Any())
               {
                  Inventarization.Entry(telephoneAddadnModified).State = EntityState.Modified;
                  Inventarization.SaveChanges();
                  return new ModelReturn("Обновили Телефон: " + telephoneAddadnModified.IdTelephon);
               }

            Inventarization.Telephons.Add(telephoneAddadnModified);
            Inventarization.SaveChanges();
            return new ModelReturn("Добавили Телефон: " + telephoneAddadnModified.IdTelephon, telephoneAddadnModified.IdTelephon);
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
            return new ModelReturn("При обновлении/добавлении данных 'Телефон' по : " + telephoneAddadnModified.IdTelephon + " произошла ошибка смотри log.txt");
        }


        /// <summary>
        /// Добавление или обновление ИБП
        /// </summary>
        /// <param name="blokpower"></param>
        public ModelReturn AddAndEditPowerBlock(BlockPower blokpower)
        {
            HistoryLog.HistoryLog log = new HistoryLog.HistoryLog();
            var blockpoweraddadnModified = new BlockPower()
            {
                IdBlockPowers = blokpower.IdBlockPowers,
                IdUser = blokpower.IdUser,
                IdProizvoditelBP = blokpower.IdProizvoditelBP,
                IdModelBP = blokpower.IdModelBP,
                IdSupply = blokpower.IdSupply,
                IdNumberKabinet = blokpower.IdNumberKabinet,
                ZavNumber = blokpower.ZavNumber,
                ServiceNumber = blokpower.ServiceNumber,
                InventarNumber = blokpower.InventarNumber,
                Coment = blokpower.Coment,
                IdStatus = blokpower.IdStatus,
                IdHistory = blokpower.IdHistory
                
            };
            try
            {
               if ((from BlockPowers in Inventarization.BlockPowers
                    where BlockPowers.IdBlockPowers == blockpoweraddadnModified.IdBlockPowers
                    select new { BlockPowers }).Any())
                  {
                    Inventarization.Entry(blockpoweraddadnModified).State = EntityState.Modified;
                    Inventarization.SaveChanges();
                    log.GenerateHistory(blockpoweraddadnModified.IdHistory, 1, "Обновление данных о ИБП: Пользователь ИБП");
                    return new ModelReturn("Обновили ИБП: " + blockpoweraddadnModified.IdBlockPowers);
                  }
                    Inventarization.BlockPowers.Add(blockpoweraddadnModified);
                    Inventarization.SaveChanges();
                    log.GenerateHistory(blockpoweraddadnModified.IdHistory, 1, "Добавление ИБП: Пользователь ИБП");
                    return new ModelReturn("Добавили Монитор: " + blockpoweraddadnModified.IdModelBP, blockpoweraddadnModified.IdBlockPowers, blockpoweraddadnModified.IdHistory);
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
            return new ModelReturn("При обновлении/добавлении данных 'ИБП' по : " + blockpoweraddadnModified.IdModelBP + " произошла ошибка смотри log.txt");
        }




        /// <summary>
        /// Добавление документа пустого
        /// </summary>
        /// <param name="iddoc">Id Документа шаблона</param>
        /// <param name="nameinfo">Описание документа</param>
        /// <param name="iduser">Ун пользователя на документ может и не быть null</param>
        /// <returns></returns>
        public int AddDocument(int iddoc,string nameinfo,int? iduser)
        {
            var doc = new Document()
            {
                IdNamedocument = iddoc,
                IdUser = iduser,
                InfoUserFile ="Накладная по " + nameinfo,
                IsFileExists = false,
                IsActual = true
            };
            Inventarization.Documents.Add(doc);
            Inventarization.SaveChanges();
            return doc.Id;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bookModels">Модель книг</param>
        /// <returns></returns>
        public int AddBookAccounting(BookModels bookModels)
        {
            var book = new BookAccounting()
            {
                IdNameBook = 2,
                IdKeys = bookModels.Keys,
                IdModel = bookModels.Id,
                ClassName = bookModels.Name,
                NameModel = bookModels.Model,
                IsActual = false

            };
            Inventarization.BookAccountings.Add(book);
            Inventarization.SaveChanges();
            return book.IdBook;
        }

        /// <summary>
        /// Обновление строки данных
        /// </summary>
        /// <param name="upload">Модель данных</param>
        public ModelError UpdateDocument(Upload upload)
        {
            var document = Inventarization.Documents.FirstOrDefault(x => x.Id == upload.IdDocument);
            if (document != null)
            {
                document.Namefile = upload.NameFile;
                document.TypeFile = upload.ExpansionFile;
                document.Document_ = upload.BlobFile;
                document.IsFileExists = true;
                Inventarization.Entry(document).State = EntityState.Modified;
                Inventarization.SaveChanges();
                return  new ModelError()
                {
                    IdDocument = upload.IdDocument,
                    IdError = 0,
                    Messages = "Документ с именем " + upload.NameFile + " сохранен в БД"
                };
            }
            return new ModelError()
            {
                IdDocument = upload.IdDocument,
                IdError = 1,
                Messages = "Документ с Ун " + upload.IdDocument + " отсутствует в БД"
            };
        }
        /// <summary>
        /// Загрузка документа книги учета
        /// </summary>
        /// <param name="upload">Файлы</param>
        /// <returns></returns>
        public ModelError UpdateBook(Upload upload)
        {
            var book = Inventarization.BookAccountings.FirstOrDefault(x => x.IdBook == upload.IdDocument);
            if (book != null)
            {
                book.IsFileExists = true;
                book.NameBook = upload.NameFile;
                book.Book = upload.BlobFile;
                book.TypeFile = upload.ExpansionFile;
                book.IsActual = true;
                foreach (var bookAccounting in Inventarization.BookAccountings.Where(x => x.IdKeys == book.IdKeys && x.IdModel == book.IdModel && x.IdBook != book.IdBook && x.IsActual==true))
                {
                    bookAccounting.IsActual = false;
                }
                Inventarization.Entry(book).State = EntityState.Modified;
                Inventarization.SaveChanges();
                return new ModelError()
                {
                    IdDocument = upload.IdDocument,
                    IdError = 0,
                    Messages = "Документ с именем " + upload.NameFile + " сохранен в БД"
                };
            }
            return new ModelError()
            {
                IdDocument = upload.IdDocument,
                IdError = 1,
                Messages = "Документ с Ун " + upload.IdDocument + " отсутствует в БД"
            };
        }


        /// <summary>
        /// Удаление документа
        /// </summary>
        /// <param name="iddoc">Ун документа</param>
        /// <returns></returns>
        public ModelReturn DeleteDocument(int iddoc)
        {
            Document doc = Inventarization.Documents.FirstOrDefault(docum => docum.Id == iddoc);
            if (doc != null)
            {
                Inventarization.Documents.Remove(doc);
                Inventarization.SaveChanges();
                return new ModelReturn("Удалили документ за номером: " + iddoc);
            }
            return new ModelReturn(null);
        }
        /// <summary>
        /// Загрузка документа на внутреенее перемещение
        /// </summary>
        /// <param name="iddoc"></param>
        /// <returns></returns>
        public Stream LoadDocuments(int iddoc)
        {
            var document = Inventarization.Documents.FirstOrDefault(x => x.Id == iddoc);
            return new MemoryStream(document?.Document_);
        }
        /// <summary>
        /// Загрузка книги учета
        /// </summary>
        /// <param name="iddoc">Ун книги учета</param>
        /// <returns></returns>
        public Stream LoadBook(int iddoc)
        {
            var book = Inventarization.BookAccountings.FirstOrDefault(x => x.IdBook==iddoc);
            return new MemoryStream(book.Book);
        }

        /// <summary>
        /// Добавление или обновление наименование системных блоков
        /// </summary>
        /// <param name="nameSysBlock">Наименование системного блока</param>
        public ModelReturn AddAndEditNameSysBlock(NameSysBlock nameSysBlock)
        {
            var nameSysBlockAddadnModified = new NameSysBlock()
            {
              IdModelSysBlock = nameSysBlock.IdModelSysBlock,
              NameComputer = nameSysBlock.NameComputer
            };
            try
            {
                if ((from NameSysBlocks in Inventarization.NameSysBlocks
                     where NameSysBlocks.IdModelSysBlock == nameSysBlockAddadnModified.IdModelSysBlock
                     select new { NameSysBlocks }).Any())
                {
                    Inventarization.Entry(nameSysBlockAddadnModified).State = EntityState.Modified;
                    Inventarization.SaveChanges();
                    return new ModelReturn("Обновили справочник наименование системных блоков: " + nameSysBlockAddadnModified.IdModelSysBlock);
                }
                Inventarization.NameSysBlocks.Add(nameSysBlockAddadnModified);
                Inventarization.SaveChanges();
                return new ModelReturn("Добавили новое имя системного блока: " + nameSysBlockAddadnModified.IdModelSysBlock, nameSysBlockAddadnModified.IdModelSysBlock);
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
            return new ModelReturn("При обновлении/добавлении данных 'Наименование системного блока' по : " + nameSysBlockAddadnModified.IdModelSysBlock + " произошла ошибка смотри log.txt");
        }

        /// <summary>
        /// Добавление или обновление наименование монитора
        /// </summary>
        /// <param name="nameMonitor">Наименование монитора</param>
        public ModelReturn AddAndEditNameMonitor(NameMonitor nameMonitor)
        {
            var nameMonitorAddadnModified = new NameMonitor()
            {
                IdModelMonitor = nameMonitor.IdModelMonitor,
                Name = nameMonitor.Name,
            };
            try
            {
                if ((from NameMonitors in Inventarization.NameMonitors
                     where NameMonitors.IdModelMonitor == nameMonitorAddadnModified.IdModelMonitor
                     select new { NameMonitors }).Any())
                {
                    Inventarization.Entry(nameMonitorAddadnModified).State = EntityState.Modified;
                    Inventarization.SaveChanges();
                    return new ModelReturn("Обновили справочник наименование мониторов: " + nameMonitorAddadnModified.IdModelMonitor);
                }
                Inventarization.NameMonitors.Add(nameMonitorAddadnModified);
                Inventarization.SaveChanges();
                return new ModelReturn("Добавили новое имя монитора: " + nameMonitorAddadnModified.IdModelMonitor, nameMonitorAddadnModified.IdModelMonitor);
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
            return new ModelReturn("При обновлении/добавлении данных 'Наименование мониторов' по : " + nameMonitorAddadnModified.IdModelMonitor + " произошла ошибка смотри log.txt");
        }

        /// <summary>
        /// Добавление или обновление модели ИБП
        /// </summary>
        /// <param name="nameModelBlokPower">Наименование модели ИБП</param>
        public ModelReturn AddAndEditNameModelBlokPower(ModelBlockPower nameModelBlokPower)
        {
            var nameModelBlokPowerAddadnModified = new ModelBlockPower()
            {
               IdModelBP = nameModelBlokPower.IdModelBP,
               Name = nameModelBlokPower.Name,
            };
            try
            {
                if ((from ModelBlockPowers in Inventarization.ModelBlockPowers
                     where ModelBlockPowers.IdModelBP == nameModelBlokPowerAddadnModified.IdModelBP
                     select new { ModelBlockPowers }).Any())
                {
                    Inventarization.Entry(nameModelBlokPowerAddadnModified).State = EntityState.Modified;
                    Inventarization.SaveChanges();
                    return new ModelReturn("Обновили справочник наименование модели ИБП: " + nameModelBlokPowerAddadnModified.IdModelBP);
                }
                Inventarization.ModelBlockPowers.Add(nameModelBlokPowerAddadnModified);
                Inventarization.SaveChanges();
                return new ModelReturn("Добавили новое имя модели ИБП: " + nameModelBlokPowerAddadnModified.IdModelBP, nameModelBlokPowerAddadnModified.IdModelBP);
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
            return new ModelReturn("При обновлении/добавлении данных 'Наименование модели ИБП' по : " + nameModelBlokPowerAddadnModified.IdModelBP + " произошла ошибка смотри log.txt");
        }

        /// <summary>
        /// Добавление или обновление производителя ИБП
        /// </summary>
        /// <param name="nameProizvoditelBlockPower">Наименование производителя ИБП</param>
        public ModelReturn AddAndEditNameProizvoditelBlockPower(ProizvoditelBlockPower nameProizvoditelBlockPower)
        {
            var nameProizvoditelBlockPowerAddadnModified = new ProizvoditelBlockPower()
            {
                IdProizvoditelBP = nameProizvoditelBlockPower.IdProizvoditelBP,
                Name = nameProizvoditelBlockPower.Name
            };
            try
            {
                if ((from ProizvoditelBlockPowers in Inventarization.ProizvoditelBlockPowers
                     where ProizvoditelBlockPowers.IdProizvoditelBP == nameProizvoditelBlockPowerAddadnModified.IdProizvoditelBP
                     select new { ProizvoditelBlockPowers }).Any())
                {
                    Inventarization.Entry(nameProizvoditelBlockPowerAddadnModified).State = EntityState.Modified;
                    Inventarization.SaveChanges();
                    return new ModelReturn("Обновили справочник наименование производителя ИБП: " + nameProizvoditelBlockPowerAddadnModified.IdProizvoditelBP);
                }
                Inventarization.ProizvoditelBlockPowers.Add(nameProizvoditelBlockPowerAddadnModified);
                Inventarization.SaveChanges();
                return new ModelReturn("Добавили новое имя производителя ИБП: " + nameProizvoditelBlockPowerAddadnModified.IdProizvoditelBP, nameProizvoditelBlockPowerAddadnModified.IdProizvoditelBP);
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
            return new ModelReturn("При обновлении/добавлении данных 'Наименование производителя ИБП' по : " + nameProizvoditelBlockPowerAddadnModified.IdProizvoditelBP + " произошла ошибка смотри log.txt");
        }

        /// <summary>
        /// Добавление или обновление наименование партии
        /// </summary>
        /// <param name="nameSupply">Наименование наименование партии</param>
        public ModelReturn AddAndEditNameSupply(Supply nameSupply)
        {

            var nameSupplyAddadnModified = new Supply()
            {
               IdSupply = nameSupply.IdSupply,
               NameSupply = nameSupply.NameSupply,
               NameKontract = nameSupply.NameKontract,
               DatePostavki = nameSupply.DatePostavki
              
            };
            try
            {
                if ((from Supplies in Inventarization.Supplies
                     where Supplies.IdSupply == nameSupplyAddadnModified.IdSupply
                     select new { Supplies }).Any())
                {
                    Inventarization.Entry(nameSupplyAddadnModified).State = EntityState.Modified;
                    Inventarization.SaveChanges();
                    return new ModelReturn("Обновили справочник наименование Партии: " + nameSupplyAddadnModified.IdSupply);
                }
                Inventarization.Supplies.Add(nameSupplyAddadnModified);
                Inventarization.SaveChanges();
                return new ModelReturn("Добавили новое имя Партии: " + nameSupplyAddadnModified.IdSupply, nameSupplyAddadnModified.IdSupply);
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
            return new ModelReturn("При обновлении/добавлении данных 'Наименование Партии' по : " + nameSupplyAddadnModified.IdSupply + " произошла ошибка смотри log.txt");
        }

        /// <summary>
        /// Добавление или обновление наименование статуса
        /// </summary>
        /// <param name="nameStatus">Наименование наименование статуса</param>
        public ModelReturn AddAndEditNameStatus(Statusing nameStatus)
        {
            var nameStatusingAddadnModified = new Statusing()
            {
                IdStatus = nameStatus.IdStatus,
                Name = nameStatus.Name,
                Color = nameStatus.Color
            };
            try
            {
                if ((from Statusings in Inventarization.Statusings
                     where Statusings.IdStatus == nameStatusingAddadnModified.IdStatus
                     select new { Statusings }).Any())
                {
                    Inventarization.Entry(nameStatusingAddadnModified).State = EntityState.Modified;
                    Inventarization.SaveChanges();
                    return new ModelReturn("Обновили справочник наименование Статуса: " + nameStatusingAddadnModified.IdStatus);
                }
                Inventarization.Statusings.Add(nameStatusingAddadnModified);
                Inventarization.SaveChanges();
                return new ModelReturn("Добавили новое имя Статуса: " + nameStatusingAddadnModified.IdStatus, nameStatusingAddadnModified.IdStatus);
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
            return new ModelReturn("При обновлении/добавлении данных 'Наименование Статуса' по : " + nameStatusingAddadnModified.IdStatus + " произошла ошибка смотри log.txt");
        }

        /// <summary>
        /// Добавление или обновление номера кабинета
        /// </summary>
        /// <param name="nameKabinet">Наименование номера кабинета</param>
        public ModelReturn AddAndEditNameKabinetr(Kabinet nameKabinet)
        {
            var nameKabinetAddadnModified = new Kabinet()
            {
                IdNumberKabinet = nameKabinet.IdNumberKabinet,
                NumberKabinet = nameKabinet.NumberKabinet
            };
            try
            {
                if ((from Kabinets in Inventarization.Kabinets
                     where Kabinets.IdNumberKabinet == nameKabinetAddadnModified.IdNumberKabinet
                     select new { Kabinets }).Any())
                {
                    Inventarization.Entry(nameKabinetAddadnModified).State = EntityState.Modified;
                    Inventarization.SaveChanges();
                    return new ModelReturn("Обновили справочник номера Кабинетов: " + nameKabinetAddadnModified.IdNumberKabinet);
                }
                Inventarization.Kabinets.Add(nameKabinetAddadnModified);
                Inventarization.SaveChanges();
                return new ModelReturn("Добавили новый номер Кабинета: " + nameKabinetAddadnModified.IdNumberKabinet, nameKabinetAddadnModified.IdNumberKabinet);
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
            return new ModelReturn("При обновлении/добавлении данных 'Номер Кабинета' по : " + nameKabinetAddadnModified.IdNumberKabinet + " произошла ошибка смотри log.txt");
        }


        /// <summary>
        /// Добавление или обновление наименование модели принтера(МФУ)
        /// </summary>
        /// <param name="nameFullModel">Наименование наименование модели принтера(МФУ)</param>
        public ModelReturn AddAndEditNameFullModel(FullModel nameFullModel)
        {
            var nameFullModelAddadnModified = new FullModel()
            {
               IdModel = nameFullModel.IdModel,
               NameModel = nameFullModel.NameModel,
               IdClasification = nameFullModel.IdClasification
            };
            try
            {
                if ((from FullModels in Inventarization.FullModels
                     where FullModels.IdModel == nameFullModelAddadnModified.IdModel
                     select new { FullModels }).Any())
                {
                    Inventarization.Entry(nameFullModelAddadnModified).State = EntityState.Modified;
                    Inventarization.SaveChanges();
                    return new ModelReturn("Обновили справочник наименование модели принтера(МФУ): " + nameFullModelAddadnModified.IdModel);
                }
                Inventarization.FullModels.Add(nameFullModelAddadnModified);
                Inventarization.SaveChanges();
                return new ModelReturn("Добавили новое имя модели принтера(МФУ): " + nameFullModelAddadnModified.IdModel, nameFullModelAddadnModified.IdModel);
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
            return new ModelReturn("При обновлении/добавлении данных 'Наименование модели принтера(МФУ)' по : " + nameFullModelAddadnModified.IdModel + " произошла ошибка смотри log.txt");
        }

        /// <summary>
        /// Добавление или обновление наименование классификации принтера(МФУ)
        /// </summary>
        /// <param name="nameClassification">Наименование наименование классификации принтера(МФУ)</param>
        public ModelReturn AddAndEditNameClassification(Classification nameClassification)
        {
            var nameClassificationAddadnModified = new Classification()
            {
               IdClasification = nameClassification.IdClasification,
               NameClass = nameClassification.NameClass
            };
            try
            {
                if ((from Classifications in Inventarization.Classifications
                     where Classifications.IdClasification == nameClassificationAddadnModified.IdClasification
                     select new { Classifications }).Any())
                {
                    Inventarization.Entry(nameClassificationAddadnModified).State = EntityState.Modified;
                    Inventarization.SaveChanges();
                    return new ModelReturn("Обновили справочник наименование классификации принтера(МФУ): " + nameClassificationAddadnModified.IdClasification);
                }
                Inventarization.Classifications.Add(nameClassificationAddadnModified);
                Inventarization.SaveChanges();
                return new ModelReturn("Добавили новое имя классификации принтера(МФУ): " + nameClassificationAddadnModified.IdClasification, nameClassificationAddadnModified.IdClasification);
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
            return new ModelReturn("При обновлении/добавлении данных 'Наименование классификации принтера(МФУ)' по : " + nameClassificationAddadnModified.IdClasification + " произошла ошибка смотри log.txt");
        }

        /// <summary>
        /// Добавление или обновление наименование производителя принтера(МФУ)
        /// </summary>
        /// <param name="nameFullProizvoditel">Наименование наименование производителя принтера(МФУ)</param>
        public ModelReturn AddAndEditNameFullProizvoditel(FullProizvoditel nameFullProizvoditel)
        {
            var nameFullProizvoditelAddadnModified = new FullProizvoditel()
            {
                IdProizvoditel = nameFullProizvoditel.IdProizvoditel,
                NameProizvoditel = nameFullProizvoditel.NameProizvoditel
            };
            try
            {
                if ((from FullProizvoditels in Inventarization.FullProizvoditels
                     where FullProizvoditels.IdProizvoditel == nameFullProizvoditelAddadnModified.IdProizvoditel
                     select new { FullProizvoditels }).Any())
                {
                    Inventarization.Entry(nameFullProizvoditelAddadnModified).State = EntityState.Modified;
                    Inventarization.SaveChanges();
                    return new ModelReturn("Обновили справочник наименование производителя принтера(МФУ): " + nameFullProizvoditelAddadnModified.IdProizvoditel);
                }
                Inventarization.FullProizvoditels.Add(nameFullProizvoditelAddadnModified);
                Inventarization.SaveChanges();
                return new ModelReturn("Добавили новое имя производителя принтера(МФУ): " + nameFullProizvoditelAddadnModified.IdProizvoditel, nameFullProizvoditelAddadnModified.IdProizvoditel);
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
            return new ModelReturn("При обновлении/добавлении данных 'Наименование производителя принтера(МФУ)' по : " + nameFullProizvoditelAddadnModified.IdProizvoditel + " произошла ошибка смотри log.txt");
        }

        /// <summary>
        /// Добавление или обновление CopySave для МФУ
        /// </summary>
        /// <param name="nameCopySave">Наименование CopySave для МФУ</param>
        public ModelReturn AddAndEditNameCopySave(CopySave nameCopySave)
        {
            var nameCopySaveAddadnModified = new CopySave()
            {
                IdCopySave = nameCopySave.IdCopySave,
                NameCopySave = nameCopySave.NameCopySave,
                SerNum = nameCopySave.SerNum,
                InventarNum = nameCopySave.InventarNum
            };
            try
            {
                if ((from CopySaves in Inventarization.CopySaves
                     where CopySaves.IdCopySave == nameCopySaveAddadnModified.IdCopySave
                     select new { CopySaves }).Any())
                {
                    Inventarization.Entry(nameCopySaveAddadnModified).State = EntityState.Modified;
                    Inventarization.SaveChanges();
                    return new ModelReturn("Обновили справочник наименование CopySave для МФУ: " + nameCopySaveAddadnModified.IdCopySave);
                }
                Inventarization.CopySaves.Add(nameCopySaveAddadnModified);
                Inventarization.SaveChanges();
                return new ModelReturn("Добавили новый CopySave для МФУ: " + nameCopySaveAddadnModified.IdCopySave, nameCopySaveAddadnModified.IdCopySave);
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
            return new ModelReturn("При обновлении/добавлении данных 'CopySave для МФУ' по : " + nameCopySaveAddadnModified.IdCopySave + " произошла ошибка смотри log.txt");
        }



        /// <summary>
        /// Dispos
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Inventarization?.Dispose();
                Inventarization = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

    }
}
