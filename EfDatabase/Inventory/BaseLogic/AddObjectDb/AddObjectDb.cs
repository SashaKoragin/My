using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using EfDatabase.Inventory.Base;
using EfDatabase.Inventory.ReportXml.ReturnModelError;
using EfDatabaseUploadFile;
using EfDatabaseXsdBookAccounting;
using Supply = EfDatabase.Inventory.Base.Supply;

namespace EfDatabase.Inventory.BaseLogic.AddObjectDb
{
   public class AddObjectDb : IDisposable
    {
        public InventoryContext Inventory { get; set; }

        public AddObjectDb()
        {
            Inventory?.Dispose();
            Inventory = new InventoryContext();
        }


        /// <summary>
        /// Добавление или обновление пользователя
        /// </summary>
        /// <param name="user"></param>
        /// <param name="idUser">Ун пользователя</param>
        public ModelReturn<User> AddAndEditUser(User user,int? idUser)
        {
            var log = new HistoryLog.HistoryLog();
            var usersAddAndModified = new User()
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
                using (var context = new InventoryContext())
                {
                    var modelDb = from users in context.Users where users.IdUser == user.IdUser select new {Users = users};
                    if (modelDb.Any())
                    {
                        Inventory.Entry(usersAddAndModified).State = EntityState.Modified;
                        Inventory.SaveChanges();
                        log.GenerateHistory(usersAddAndModified.IdHistory, usersAddAndModified.IdUser, "Пользователь",idUser, 
                            "Нет смысла отслеживать проходит через синхронизацию",
                            "Нет смысла отслеживать проходит через синхронизацию");
                        return new ModelReturn<User>("Обновили пользователя: " + usersAddAndModified.IdUser, user);
                    }
                }
                Inventory.Users.Add(usersAddAndModified);
                Inventory.SaveChanges();
                  user.IdUser = usersAddAndModified.IdUser;
                  user.IdHistory = usersAddAndModified.IdHistory;
                  log.GenerateHistory(usersAddAndModified.IdHistory, usersAddAndModified.IdUser, "Пользователь", idUser, "Нет смысла отслеживать проходит через синхронизацию", "Нет смысла отслеживать проходит через синхронизацию");
                return new ModelReturn<User>("Добавили пользователя: " + usersAddAndModified.IdUser, user, usersAddAndModified.IdUser, usersAddAndModified.IdHistory);
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
            return new ModelReturn<User>("При обновлении/добавлении данных 'Пользователь' по : " + usersAddAndModified.IdOtdel + " произошла ошибка смотри log.txt");
        }
        /// <summary>
        /// Добавление отдела без лога данных проверим как работает
        /// </summary>
        /// <param name="department">Отдел</param>
        /// <returns></returns>
        public ModelReturn<Otdel> AddAndEditDepartment(Otdel department)
        {
            var departmentAddAndModified = new Otdel()
            {
                IdOtdel = department.IdOtdel,
                IdUser = department.IdUser,
                NameOtdel = department.NameOtdel
            };
            try
            {
              if ((from departmentSelect in Inventory.Otdels
                 where departmentSelect.IdOtdel == departmentAddAndModified.IdOtdel
                 select new { Otdels = departmentSelect }).Any())
                {
                  Inventory.Entry(departmentAddAndModified).State = EntityState.Modified;
                  Inventory.SaveChanges();
                  return new ModelReturn<Otdel>("Обновили отдел: "+ departmentAddAndModified.IdOtdel, department);
                }
              Inventory.Otdels.Add(departmentAddAndModified);
              Inventory.SaveChanges();
              department.IdOtdel = departmentAddAndModified.IdOtdel;
              return new ModelReturn<Otdel>("Добавили отдел: " + departmentAddAndModified.IdOtdel, department, departmentAddAndModified.IdOtdel);
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
            return new ModelReturn<Otdel>("При обновлении/добавлении данных 'Отдел' по : " + departmentAddAndModified.IdOtdel + " произошла ошибка смотри log.txt");
        }


        /// <summary>
        /// Добавление или обновление Принтера
        /// </summary>
        /// <param name="printer"></param>
        /// <param name="idUser">Ун пользователя</param>
        public ModelReturn<Printer> AddAndEditPrinter(Printer printer, int? idUser)
        {
            HistoryLog.HistoryLog log = new HistoryLog.HistoryLog();
            var printerAddAndModified = new Printer()
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
              var newModel = $"Пользователь: {printer.User?.Name}; Кабинет: {printer.Kabinet?.NumberKabinet}; Комментарий: {printer.Coment}; Статус: {printer.Statusing?.Name}";
              using (var context = new InventoryContext())
              {
                var modelDb = from printers in context.Printers where printers.IdPrinter == printer.IdPrinter select new {Printers = printers};
                if (modelDb.Any())
                {
                    var oldModel = $"Пользователь: {modelDb.First().Printers?.User?.Name}; Кабинет: {modelDb.First().Printers?.Kabinet?.NumberKabinet}; Комментарий: {modelDb.First().Printers.Coment}; Статус: {modelDb.First().Printers?.Statusing?.Name}";
                    Inventory.Entry(printerAddAndModified).State = EntityState.Modified;
                    Inventory.SaveChanges();
                    log.GenerateHistory(printer.IdHistory, printer.IdPrinter, "Принтер", idUser,
                       oldModel,
                       newModel);
                    return new ModelReturn<Printer>("Обновили принтер: " + printer.IdPrinter, printer);
                }
              }
              Inventory.Printers.Add(printerAddAndModified);
              Inventory.SaveChanges();
              printer.IdPrinter = printerAddAndModified.IdPrinter;
              printer.IdHistory = printerAddAndModified.IdHistory;
              log.GenerateHistory(printer.IdHistory, printer.IdPrinter, "Принтер", idUser, "Отсутствует модель при добавлении нового устройства", newModel);
              return new ModelReturn<Printer>("Добавили принтер: " + printerAddAndModified.IdPrinter, printer, printerAddAndModified.IdPrinter, printerAddAndModified.IdHistory);
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
            return new ModelReturn<Printer>("При обновлении/добавлении данных 'Принтер' по : " + printerAddAndModified.IdPrinter + " произошла ошибка смотри log.txt");
        }
        /// <summary>
        /// Добавление или обновление Коммутатора
        /// </summary>
        /// <param name="swithe"></param>
        /// <param name="idUser">Ун пользователя</param>
        public ModelReturn<Swithe> AddAndEditSwiths(Swithe swithe, int? idUser)
        {
            HistoryLog.HistoryLog log = new HistoryLog.HistoryLog();
            var switheAddadnModified = new Swithe()
            {
                IdSwithes = swithe.IdSwithes,
                IdUser = swithe.IdUser,
                IdModelSwithes = swithe.IdModelSwithes,
                IdNumberKabinet = swithe.IdNumberKabinet,
                IdSupply = swithe.IdSupply,
                ServiceNum = swithe.ServiceNum,
                SerNum = swithe.SerNum,
                InventarNum = swithe.InventarNum,
                Coment = swithe.Coment,
                IdStatus = swithe.IdStatus,
                IdHistory = swithe.IdHistory
            };
            try
            {
                var newmodel = $"Пользователь: {swithe.User?.Name}; Кабинет: {swithe.Kabinet?.NumberKabinet}; Коментарий: {swithe.Coment}; Статус: {swithe.Statusing?.Name}";
                using (var context = new InventoryContext())
                {
                    var modeldb = from Swithes in context.Swithes where Swithes.IdSwithes == swithe.IdSwithes select new {Swithes};
                    if (modeldb.Any())
                    {
                        var oldmodel = $"Пользователь: {modeldb.First().Swithes?.User?.Name}; Кабинет: {modeldb.First().Swithes?.Kabinet?.NumberKabinet}; Коментарий: {modeldb.First().Swithes.Coment}; Статус: {modeldb.First().Swithes?.Statusing?.Name}";
                        Inventory.Entry(switheAddadnModified).State = EntityState.Modified;
                        Inventory.SaveChanges();
                        log.GenerateHistory(swithe.IdHistory, swithe.IdSwithes, "Коммутатор", idUser,
                            oldmodel,
                            newmodel);
                        return new ModelReturn<Swithe>("Обновили коммутатор: " + swithe.IdSwithes, swithe);
                    }
                }
                Inventory.Swithes.Add(switheAddadnModified);
                Inventory.SaveChanges();
                swithe.IdSwithes = switheAddadnModified.IdSwithes;
                swithe.IdHistory = switheAddadnModified.IdHistory;
                log.GenerateHistory(swithe.IdHistory, swithe.IdSwithes, "Коммутатор", idUser,
                       $"Отсутствует модель при добавлении нового устройства", newmodel);
                return new ModelReturn<Swithe>("Добавили Коммутатор: " + switheAddadnModified.IdSwithes, swithe, switheAddadnModified.IdSwithes, switheAddadnModified.IdHistory);
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
            return new ModelReturn<Swithe>("При обновлении/добавлении данных 'Коммутатор' по : " + switheAddadnModified.IdSwithes + " произошла ошибка смотри log.txt");
        }

        /// <summary>
        /// Добавление или обновление Сканера
        /// </summary>
        /// <param name="scaner"></param>
        /// <param name="idUser">Ун пользователя</param>
        public ModelReturn<ScanerAndCamer> AddAndEditScaner(ScanerAndCamer scaner, int? idUser)
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
                var newmodel = $"Пользователь: {scaner.User?.Name}; Кабинет: {scaner.Kabinet?.NumberKabinet}; Коментарий: {scaner.Coment}; Статус: {scaner.Statusing?.Name}";
                using (var context = new InventoryContext())
                {
                    var modeldb = from Scaner in context.ScanerAndCamers where Scaner.IdScaner == scaner.IdScaner select new {Scaner};
                    if (modeldb.Any())
                    {
                        var oldmodel = $"Пользователь: {modeldb.First().Scaner?.User?.Name}; Кабинет: {modeldb.First().Scaner?.Kabinet?.NumberKabinet}; Коментарий: {modeldb.First().Scaner.Coment}; Статус: {modeldb.First().Scaner?.Statusing?.Name}";
                        Inventory.Entry(scanerAddadnModified).State = EntityState.Modified;
                        Inventory.SaveChanges();
                        log.GenerateHistory(scaner.IdHistory, scaner.IdScaner, "Сканер или камера", idUser,
                            oldmodel,
                            newmodel);
                        return new ModelReturn<ScanerAndCamer>("Обновили сканер: " + scanerAddadnModified.IdScaner, scaner);
                    }
                }
                Inventory.ScanerAndCamers.Add(scanerAddadnModified);
                Inventory.SaveChanges();
                scaner.IdScaner = scanerAddadnModified.IdScaner;
                scaner.IdHistory = scanerAddadnModified.IdHistory;
                log.GenerateHistory(scaner.IdHistory, scaner.IdScaner, "Сканер или камера", idUser,
                        $"Отсутствует модель при добавлении нового устройства",
                        newmodel);
                return new ModelReturn<ScanerAndCamer>("Добавили сканер: " + scanerAddadnModified.IdScaner, scaner, scanerAddadnModified.IdScaner, scanerAddadnModified.IdHistory);
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
            return new ModelReturn<ScanerAndCamer>("При обновлении/добавлении данных 'Cканер' по : " + scanerAddadnModified.IdScaner + " произошла ошибка смотри log.txt");
        }

        /// <summary>
        /// Добавление или обновление МФУ
        /// </summary>
        /// <param name="mfu"></param>
        /// <param name="idUser">Ун пользователя</param>
        public ModelReturn<Mfu> AddAndEditMfu(Mfu mfu, int? idUser)
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
                var newmodel = $"Пользователь: {mfu.User?.Name}; Кабинет: {mfu.Kabinet?.NumberKabinet}; Коментарий: {mfu.Coment}; Статус: {mfu.Statusing?.Name}";
                using (var context = new InventoryContext())
                {
                    var modeldb = from Mfu in context.Mfus where Mfu.IdMfu == mfu.IdMfu select new {Mfu};
                    if (modeldb.Any())
                    {
                        var oldmodel = $"Пользователь: {modeldb.First().Mfu?.User?.Name}; Кабинет: {modeldb.First().Mfu?.Kabinet?.NumberKabinet}; Коментарий: {modeldb.First().Mfu.Coment}; Статус: {modeldb.First().Mfu?.Statusing?.Name}";
                        Inventory.Entry(mfuAddadnModified).State = EntityState.Modified;
                        Inventory.SaveChanges();
                        log.GenerateHistory(mfu.IdHistory, mfu.IdMfu, "МФУ", idUser,
                            oldmodel,
                            newmodel);
                        return new ModelReturn<Mfu>("Обновили МФУ: " + mfuAddadnModified.IdMfu, mfu);
                    }
                }
                Inventory.Mfus.Add(mfuAddadnModified);
                Inventory.SaveChanges();
                mfu.IdMfu = mfuAddadnModified.IdMfu;
                mfu.IdHistory = mfuAddadnModified.IdHistory;
                log.GenerateHistory(mfu.IdHistory, mfu.IdMfu, "МФУ", idUser,
                    $"Отсутствует модель при добавлении нового устройства",
                    newmodel);
                return new ModelReturn<Mfu>("Добавили МФУ: " + mfuAddadnModified.IdMfu, mfu, mfuAddadnModified.IdMfu, mfuAddadnModified.IdHistory);
             }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
            return new ModelReturn<Mfu>("При обновлении/добавлении данных 'МФУ' по : " + mfuAddadnModified.IdMfu + " произошла ошибка смотри log.txt");
        }

        /// <summary>
        /// Добавление или обновление Системного блока
        /// </summary>
        /// <param name="sysblok"></param>
        /// <param name="idUser">Ун пользователя</param>
        public ModelReturn<SysBlock> AddAndEditSysBlok(SysBlock sysblok, int? idUser)
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
                var newmodel = $"Пользователь: {sysblok.User?.Name} Имя компьютера: {sysblok.NameComputer} Кабинет: {sysblok.Kabinet?.NumberKabinet} Коментарий: {sysblok.Coment} Статус: {sysblok.Statusing?.Name}";
                using (var context = new InventoryContext())
                {
                    var modeldb = from SysBlocks in context.SysBlocks where SysBlocks.IdSysBlock == sysblok.IdSysBlock select new {SysBlocks};
                    if (modeldb.Any())
                    {
                        var oldmodel = $"Пользователь: {modeldb.First().SysBlocks?.User?.Name} Имя компьютера: {modeldb.First().SysBlocks?.NameComputer} Кабинет: {modeldb.First().SysBlocks?.Kabinet?.NumberKabinet} Коментарий: {modeldb.First().SysBlocks.Coment} Статус: {modeldb.First().SysBlocks?.Statusing?.Name}";
                        Inventory.Entry(sysblokAddadnModified).State = EntityState.Modified;
                        Inventory.SaveChanges();
                        log.GenerateHistory(sysblok.IdHistory, sysblok.IdSysBlock, "Системный блок", idUser,
                            oldmodel,
                            newmodel);
                        return new ModelReturn<SysBlock>("Обновили Системный блок: " + sysblokAddadnModified.IdSysBlock, sysblok);
                    }
                }
                Inventory.SysBlocks.Add(sysblokAddadnModified);
                  Inventory.SaveChanges();
                  sysblok.IdSysBlock= sysblokAddadnModified.IdSysBlock;
                  sysblok.IdHistory = sysblokAddadnModified.IdHistory;
                  log.GenerateHistory(sysblok.IdHistory, sysblok.IdSysBlock, "Системный блок", idUser,
                     $"Отсутствует модель при добавлении нового устройства",
                     newmodel);
                  return new ModelReturn<SysBlock>("Добавили Системный блок: " + sysblokAddadnModified.IdSysBlock, sysblok, sysblokAddadnModified.IdSysBlock, sysblokAddadnModified.IdHistory);
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
            return new ModelReturn<SysBlock>("При обновлении/добавлении данных 'Системный блок' по : " + sysblokAddadnModified.IdSysBlock+ " произошла ошибка смотри log.txt");
        }


        /// <summary>
        /// Добавление или обновление Монитора
        /// </summary>
        /// <param name="monitor"></param>
        /// <param name="idUser">Ун пользователя</param>
        public ModelReturn<Monitor> AddAndEditMonitors(Monitor monitor, int? idUser)
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
                var newmodel = $"Пользователь: {monitor.User?.Name}; Кабинет: {monitor.Kabinet?.NumberKabinet}; Коментарий: {monitor.Coment}; Статус: {monitor.Statusing?.Name}";
                using (var context = new InventoryContext())
                {
                    var modeldb = from Monitor in context.Monitors where Monitor.IdMonitor == monitor.IdMonitor select new {Monitor};
                    if (modeldb.Any())
                    {
                        var oldmodel = $"Пользователь: {modeldb.First().Monitor?.User?.Name}; Кабинет: {modeldb.First().Monitor?.Kabinet?.NumberKabinet}; Коментарий: {modeldb.First().Monitor.Coment}; Статус: {modeldb.First().Monitor?.Statusing?.Name}";
                        Inventory.Entry(monitorAddadnModified).State = EntityState.Modified;
                        Inventory.SaveChanges();
                        log.GenerateHistory(monitor.IdHistory, monitor.IdMonitor, "Монитор", idUser,
                            oldmodel,
                            newmodel);
                        return new ModelReturn<Monitor>("Обновили Монитор: " + monitorAddadnModified.IdMonitor, monitor);
                    }
                }
                Inventory.Monitors.Add(monitorAddadnModified);
                 Inventory.SaveChanges();
                 monitor.IdMonitor = monitorAddadnModified.IdMonitor;
                 monitor.IdHistory = monitorAddadnModified.IdHistory;
                 log.GenerateHistory(monitor.IdHistory, monitor.IdMonitor, "Монитор", idUser,
                    $"Отсутствует модель при добавлении нового устройства",
                    newmodel);
                 return new ModelReturn<Monitor>("Добавили Монитор: " + monitorAddadnModified.IdMonitor, monitor, monitorAddadnModified.IdMonitor, monitorAddadnModified.IdHistory);
             }
             catch (Exception e)
             {
                Loggers.Log4NetLogger.Error(e);
             }
            return new ModelReturn<Monitor>("При обновлении/добавлении данных 'Монитор' по : " + monitorAddadnModified.IdMonitor + " произошла ошибка смотри log.txt");
        }

        /// <summary>
        /// Добавление или обновление Телефона
        /// </summary>
        /// <param name="telephone"></param>
        /// <param name="idUser">Ун пользователя</param>
        public ModelReturn<Telephon> AddAndEditTelephone(Telephon telephone, int? idUser)
        {
            HistoryLog.HistoryLog log = new HistoryLog.HistoryLog();
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
                var newmodel = $"Кабинет: {telephone.Kabinet?.NumberKabinet}; Коментарий: {telephone.Coment}; Статус: {telephone.Statusing?.Name}";
                using (var context = new InventoryContext())
                {
                    var modeldb = from Telephone in context.Telephons where Telephone.IdTelephon == telephone.IdTelephon select new {Telephone};
                    if (modeldb.Any())
                    {
                        var oldmodel = $"Кабинет: {modeldb.First().Telephone?.Kabinet?.NumberKabinet}; Коментарий: {modeldb.First().Telephone.Coment}; Статус: {modeldb.First().Telephone?.Statusing?.Name}";
                        Inventory.Entry(telephoneAddadnModified).State = EntityState.Modified;
                        Inventory.SaveChanges();
                        log.GenerateHistory(null, telephone.IdTelephon, "Телефон", idUser,
                            oldmodel,
                            newmodel);
                        return new ModelReturn<Telephon>("Обновили Телефон: " + telephoneAddadnModified.IdTelephon,telephone);
                    }
                }
                Inventory.Telephons.Add(telephoneAddadnModified);
                  Inventory.SaveChanges();
                  telephone.IdTelephon = telephoneAddadnModified.IdTelephon;
                  log.GenerateHistory(null, telephone.IdTelephon, "Телефон", idUser,
                     $"Отсутствует модель при добавлении нового устройства",
                     newmodel);
                 return new ModelReturn<Telephon>("Добавили Телефон: " + telephoneAddadnModified.IdTelephon, telephone, telephoneAddadnModified.IdTelephon);
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
            return new ModelReturn<Telephon>("При обновлении/добавлении данных 'Телефон' по : " + telephoneAddadnModified.IdTelephon + " произошла ошибка смотри log.txt");
        }


        /// <summary>
        /// Добавление или обновление ИБП
        /// </summary>
        /// <param name="blokpower"></param>
        /// <param name="idUser">Ун пользователя</param>
        public ModelReturn<BlockPower> AddAndEditPowerBlock(BlockPower blokpower, int? idUser)
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
                var newmodel = $"Пользователь: {blokpower.User?.Name}; Кабинет: {blokpower.Kabinet?.NumberKabinet}; Коментарий: {blokpower.Coment}; Статус: {blokpower.Statusing?.Name}";
                using (var context = new InventoryContext())
                {
                    var modeldb = from BlockPowers in context.BlockPowers where BlockPowers.IdBlockPowers == blokpower.IdBlockPowers select new {BlockPowers};
                    if (modeldb.Any())
                    {
                        var oldmodel = $"Пользователь: {modeldb.First().BlockPowers?.User?.Name}; Кабинет: {modeldb.First().BlockPowers?.Kabinet?.NumberKabinet}; Коментарий: {modeldb.First().BlockPowers.Coment}; Статус: {modeldb.First().BlockPowers?.Statusing?.Name}";
                        Inventory.Entry(blockpoweraddadnModified).State = EntityState.Modified;
                        Inventory.SaveChanges();
                        log.GenerateHistory(blokpower.IdHistory, blokpower.IdBlockPowers, "ИБП", idUser,
                            oldmodel,
                            newmodel);
                        return new ModelReturn<BlockPower>("Обновили ИБП: " + blockpoweraddadnModified.IdBlockPowers, blokpower);
                    }
                }
                Inventory.BlockPowers.Add(blockpoweraddadnModified);
                    Inventory.SaveChanges();
                    blokpower.IdBlockPowers = blockpoweraddadnModified.IdBlockPowers;
                    blokpower.IdHistory = blockpoweraddadnModified.IdHistory;
                    log.GenerateHistory(blokpower.IdHistory, blokpower.IdBlockPowers, "ИБП", idUser,
                       $"Отсутствует модель при добавлении нового устройства",
                       newmodel);
                    return new ModelReturn<BlockPower>("Добавили Монитор: " + blockpoweraddadnModified.IdModelBP, blokpower, blockpoweraddadnModified.IdBlockPowers, blockpoweraddadnModified.IdHistory);
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
            return new ModelReturn<BlockPower>("При обновлении/добавлении данных 'ИБП' по : " + blockpoweraddadnModified.IdModelBP + " произошла ошибка смотри log.txt");
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
            Inventory.Documents.Add(doc);
            Inventory.SaveChanges();
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
            Inventory.BookAccountings.Add(book);
            Inventory.SaveChanges();
            return book.IdBook;
        }

        /// <summary>
        /// Обновление строки данных
        /// </summary>
        /// <param name="upload">Модель данных</param>
        public ModelError UpdateDocument(Upload upload)
        {
            var document = Inventory.Documents.FirstOrDefault(x => x.Id == upload.IdDocument);
            if (document != null)
            {
                document.Namefile = upload.NameFile;
                document.TypeFile = upload.ExpansionFile;
                document.Document_ = upload.BlobFile;
                document.IsFileExists = true;
                Inventory.Entry(document).State = EntityState.Modified;
                Inventory.SaveChanges();
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
            var book = Inventory.BookAccountings.FirstOrDefault(x => x.IdBook == upload.IdDocument);
            if (book != null)
            {
                book.IsFileExists = true;
                book.NameBook = upload.NameFile;
                book.Book = upload.BlobFile;
                book.TypeFile = upload.ExpansionFile;
                book.IsActual = true;
                foreach (var bookAccounting in Inventory.BookAccountings.Where(x => x.IdKeys == book.IdKeys && x.IdModel == book.IdModel && x.IdBook != book.IdBook && x.IsActual==true))
                {
                    bookAccounting.IsActual = false;
                }
                Inventory.Entry(book).State = EntityState.Modified;
                Inventory.SaveChanges();
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

        ////////////////////////////////
        /// <summary>
        /// Удаление документа
        /// </summary>
        /// <param name="iddoc">Ун документа</param>
        /// <returns></returns>
        public string DeleteDocument(int iddoc)
        {
            Document doc = Inventory.Documents.FirstOrDefault(docum => docum.Id == iddoc);
            if (doc != null)
            {
                Inventory.Documents.Remove(doc);
                Inventory.SaveChanges();
                return "Удалили документ за номером: " + iddoc;
            }
            return null;
        }
        /// <summary>
        /// Загрузка документа на внутреенее перемещение
        /// </summary>
        /// <param name="iddoc"></param>
        /// <returns></returns>
        public Stream LoadDocuments(int iddoc)
        {
            var document = Inventory.Documents.FirstOrDefault(x => x.Id == iddoc);
            return new MemoryStream(document?.Document_);
        }
        /// <summary>
        /// Загрузка книги учета
        /// </summary>
        /// <param name="iddoc">Ун книги учета</param>
        /// <returns></returns>
        public Stream LoadBook(int iddoc)
        {
            var book = Inventory.BookAccountings.FirstOrDefault(x => x.IdBook==iddoc);
            return new MemoryStream(book.Book);
        }

        /// <summary>
        /// Добавление или обновление наименование системных блоков
        /// </summary>
        /// <param name="nameSysBlock">Наименование системного блока</param>
        public ModelReturn<NameSysBlock> AddAndEditNameSysBlock(NameSysBlock nameSysBlock)
        {
            var nameSysBlockAddadnModified = new NameSysBlock()
            {
              IdModelSysBlock = nameSysBlock.IdModelSysBlock,
              NameComputer = nameSysBlock.NameComputer
            };
            try
            {
                if ((from NameSysBlocks in Inventory.NameSysBlocks
                     where NameSysBlocks.IdModelSysBlock == nameSysBlockAddadnModified.IdModelSysBlock
                     select new { NameSysBlocks }).Any())
                {
                    Inventory.Entry(nameSysBlockAddadnModified).State = EntityState.Modified;
                    Inventory.SaveChanges();
                    return new ModelReturn<NameSysBlock>("Обновили справочник наименование системных блоков: " + nameSysBlockAddadnModified.IdModelSysBlock, nameSysBlock);
                }
                Inventory.NameSysBlocks.Add(nameSysBlockAddadnModified);
                Inventory.SaveChanges();
                nameSysBlock.IdModelSysBlock = nameSysBlockAddadnModified.IdModelSysBlock;
                return new ModelReturn<NameSysBlock>("Добавили новое имя системного блока: " + nameSysBlockAddadnModified.IdModelSysBlock, nameSysBlock, nameSysBlockAddadnModified.IdModelSysBlock);
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
            return new ModelReturn<NameSysBlock>("При обновлении/добавлении данных 'Наименование системного блока' по : " + nameSysBlockAddadnModified.IdModelSysBlock + " произошла ошибка смотри log.txt");
        }

        /// <summary>
        /// Добавление или обновление наименование монитора
        /// </summary>
        /// <param name="nameMonitor">Наименование монитора</param>
        public ModelReturn<NameMonitor> AddAndEditNameMonitor(NameMonitor nameMonitor)
        {
            var nameMonitorAddadnModified = new NameMonitor()
            {
                IdModelMonitor = nameMonitor.IdModelMonitor,
                Name = nameMonitor.Name,
            };
            try
            {
                if ((from NameMonitors in Inventory.NameMonitors
                     where NameMonitors.IdModelMonitor == nameMonitorAddadnModified.IdModelMonitor
                     select new { NameMonitors }).Any())
                {
                    Inventory.Entry(nameMonitorAddadnModified).State = EntityState.Modified;
                    Inventory.SaveChanges();
                    return new ModelReturn<NameMonitor>("Обновили справочник наименование мониторов: " + nameMonitorAddadnModified.IdModelMonitor, nameMonitor);
                }
                Inventory.NameMonitors.Add(nameMonitorAddadnModified);
                Inventory.SaveChanges();
                nameMonitor.IdModelMonitor = nameMonitorAddadnModified.IdModelMonitor;
                return new ModelReturn<NameMonitor>("Добавили новое имя монитора: " + nameMonitorAddadnModified.IdModelMonitor, nameMonitor, nameMonitorAddadnModified.IdModelMonitor);
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
            return new ModelReturn<NameMonitor>("При обновлении/добавлении данных 'Наименование мониторов' по : " + nameMonitorAddadnModified.IdModelMonitor + " произошла ошибка смотри log.txt");
        }

        /// <summary>
        /// Добавление или обновление модели ИБП
        /// </summary>
        /// <param name="nameModelBlokPower">Наименование модели ИБП</param>
        public ModelReturn<ModelBlockPower> AddAndEditNameModelBlokPower(ModelBlockPower nameModelBlokPower)
        {
            var nameModelBlokPowerAddadnModified = new ModelBlockPower()
            {
               IdModelBP = nameModelBlokPower.IdModelBP,
               Name = nameModelBlokPower.Name,
            };
            try
            {
                if ((from ModelBlockPowers in Inventory.ModelBlockPowers
                     where ModelBlockPowers.IdModelBP == nameModelBlokPowerAddadnModified.IdModelBP
                     select new { ModelBlockPowers }).Any())
                {
                    Inventory.Entry(nameModelBlokPowerAddadnModified).State = EntityState.Modified;
                    Inventory.SaveChanges();
                    return new ModelReturn<ModelBlockPower>("Обновили справочник наименование модели ИБП: " + nameModelBlokPowerAddadnModified.IdModelBP, nameModelBlokPower);
                }
                Inventory.ModelBlockPowers.Add(nameModelBlokPowerAddadnModified);
                Inventory.SaveChanges();
                nameModelBlokPower.IdModelBP = nameModelBlokPowerAddadnModified.IdModelBP;
                return new ModelReturn<ModelBlockPower>("Добавили новое имя модели ИБП: " + nameModelBlokPowerAddadnModified.IdModelBP, nameModelBlokPower, nameModelBlokPowerAddadnModified.IdModelBP);
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
            return new ModelReturn<ModelBlockPower>("При обновлении/добавлении данных 'Наименование модели ИБП' по : " + nameModelBlokPowerAddadnModified.IdModelBP + " произошла ошибка смотри log.txt");
        }

        /// <summary>
        /// Добавление или обновление производителя ИБП
        /// </summary>
        /// <param name="nameProizvoditelBlockPower">Наименование производителя ИБП</param>
        public ModelReturn<ProizvoditelBlockPower> AddAndEditNameProizvoditelBlockPower(ProizvoditelBlockPower nameProizvoditelBlockPower)
        {
            var nameProizvoditelBlockPowerAddadnModified = new ProizvoditelBlockPower()
            {
                IdProizvoditelBP = nameProizvoditelBlockPower.IdProizvoditelBP,
                Name = nameProizvoditelBlockPower.Name
            };
            try
            {
                if ((from ProizvoditelBlockPowers in Inventory.ProizvoditelBlockPowers
                     where ProizvoditelBlockPowers.IdProizvoditelBP == nameProizvoditelBlockPowerAddadnModified.IdProizvoditelBP
                     select new { ProizvoditelBlockPowers }).Any())
                {
                    Inventory.Entry(nameProizvoditelBlockPowerAddadnModified).State = EntityState.Modified;
                    Inventory.SaveChanges();
                    return new ModelReturn<ProizvoditelBlockPower>("Обновили справочник наименование производителя ИБП: " + nameProizvoditelBlockPowerAddadnModified.IdProizvoditelBP, nameProizvoditelBlockPower);
                }
                Inventory.ProizvoditelBlockPowers.Add(nameProizvoditelBlockPowerAddadnModified);
                Inventory.SaveChanges();
                nameProizvoditelBlockPower.IdProizvoditelBP = nameProizvoditelBlockPowerAddadnModified.IdProizvoditelBP;
                return new ModelReturn<ProizvoditelBlockPower>("Добавили новое имя производителя ИБП: " + nameProizvoditelBlockPowerAddadnModified.IdProizvoditelBP, nameProizvoditelBlockPower, nameProizvoditelBlockPowerAddadnModified.IdProizvoditelBP);
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
            return new ModelReturn<ProizvoditelBlockPower>("При обновлении/добавлении данных 'Наименование производителя ИБП' по : " + nameProizvoditelBlockPowerAddadnModified.IdProizvoditelBP + " произошла ошибка смотри log.txt");
        }

        /// <summary>
        /// Добавление или обновление наименование партии
        /// </summary>
        /// <param name="nameSupply">Наименование наименование партии</param>
        public ModelReturn<Supply> AddAndEditNameSupply(Supply nameSupply)
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
                if ((from Supplies in Inventory.Supplies
                     where Supplies.IdSupply == nameSupplyAddadnModified.IdSupply
                     select new { Supplies }).Any())
                {
                    Inventory.Entry(nameSupplyAddadnModified).State = EntityState.Modified;
                    Inventory.SaveChanges();
                    return new ModelReturn<Supply>("Обновили справочник наименование Партии: " + nameSupplyAddadnModified.IdSupply, nameSupply);
                }
                Inventory.Supplies.Add(nameSupplyAddadnModified);
                Inventory.SaveChanges();
                nameSupply.IdSupply = nameSupplyAddadnModified.IdSupply;
                return new ModelReturn<Supply>("Добавили новое имя Партии: " + nameSupplyAddadnModified.IdSupply, nameSupply, nameSupplyAddadnModified.IdSupply);
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
            return new ModelReturn<Supply>("При обновлении/добавлении данных 'Наименование Партии' по : " + nameSupplyAddadnModified.IdSupply + " произошла ошибка смотри log.txt");
        }

        /// <summary>
        /// Добавление или обновление Модели коммутатора
        /// </summary>
        /// <param name="modelSwithes">Наименование модели коммутатора</param>
        public ModelReturn<ModelSwithe> AddAndEditModelSwithe(ModelSwithe modelSwithes)
        {
            var nameModelSwitheAddadnModified = new ModelSwithe()
            {
                IdModelSwithes = modelSwithes.IdModelSwithes,
                NameModel = modelSwithes.NameModel,
                CountPort = modelSwithes.CountPort
            };
            try
            {
                if ((from ModelSwithes in Inventory.ModelSwithes
                     where ModelSwithes.IdModelSwithes == nameModelSwitheAddadnModified.IdModelSwithes
                     select new { ModelSwithes }).Any())
                {
                    Inventory.Entry(nameModelSwitheAddadnModified).State = EntityState.Modified;
                    Inventory.SaveChanges();
                    return new ModelReturn<ModelSwithe>("Обновили справочник наименование моделей Коммутаторов: " + nameModelSwitheAddadnModified.IdModelSwithes, modelSwithes);
                }
                Inventory.ModelSwithes.Add(nameModelSwitheAddadnModified);
                Inventory.SaveChanges();
                modelSwithes.IdModelSwithes = nameModelSwitheAddadnModified.IdModelSwithes;
                return new ModelReturn<ModelSwithe>("Добавили новую модель Коммутатора: " + nameModelSwitheAddadnModified.IdModelSwithes, modelSwithes, nameModelSwitheAddadnModified.IdModelSwithes);
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
            return new ModelReturn<ModelSwithe>("При обновлении/добавлении данных 'моделей Коммутаторов' по : " + nameModelSwitheAddadnModified.IdModelSwithes + " произошла ошибка смотри log.txt");
        }


        /// <summary>
        /// Добавление или обновление наименование статуса
        /// </summary>
        /// <param name="nameStatus">Наименование наименование статуса</param>
        public ModelReturn<Statusing> AddAndEditNameStatus(Statusing nameStatus)
        {
            var nameStatusingAddadnModified = new Statusing()
            {
                IdStatus = nameStatus.IdStatus,
                Name = nameStatus.Name,
                Color = nameStatus.Color
            };
            try
            {
                if ((from Statusings in Inventory.Statusings
                     where Statusings.IdStatus == nameStatusingAddadnModified.IdStatus
                     select new { Statusings }).Any())
                {
                    Inventory.Entry(nameStatusingAddadnModified).State = EntityState.Modified;
                    Inventory.SaveChanges();
                    return new ModelReturn<Statusing>("Обновили справочник наименование Статуса: " + nameStatusingAddadnModified.IdStatus, nameStatus);
                }
                Inventory.Statusings.Add(nameStatusingAddadnModified);
                Inventory.SaveChanges();
                nameStatus.IdStatus = nameStatusingAddadnModified.IdStatus;
                return new ModelReturn<Statusing>("Добавили новое имя Статуса: " + nameStatusingAddadnModified.IdStatus, nameStatus, nameStatusingAddadnModified.IdStatus);
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
            return new ModelReturn<Statusing>("При обновлении/добавлении данных 'Наименование Статуса' по : " + nameStatusingAddadnModified.IdStatus + " произошла ошибка смотри log.txt");
        }

        /// <summary>
        /// Добавление или обновление номера кабинета
        /// </summary>
        /// <param name="nameKabinet">Наименование номера кабинета</param>
        public ModelReturn<Kabinet> AddAndEditNameKabinetr(Kabinet nameKabinet)
        {
            var nameKabinetAddadnModified = new Kabinet()
            {
                IdNumberKabinet = nameKabinet.IdNumberKabinet,
                NumberKabinet = nameKabinet.NumberKabinet
            };
            try
            {
                if ((from Kabinets in Inventory.Kabinets
                     where Kabinets.IdNumberKabinet == nameKabinetAddadnModified.IdNumberKabinet
                     select new { Kabinets }).Any())
                {
                    Inventory.Entry(nameKabinetAddadnModified).State = EntityState.Modified;
                    Inventory.SaveChanges();
                    return new ModelReturn<Kabinet>("Обновили справочник номера Кабинетов: " + nameKabinetAddadnModified.IdNumberKabinet, nameKabinet);
                }
                Inventory.Kabinets.Add(nameKabinetAddadnModified);
                Inventory.SaveChanges();
                nameKabinet.IdNumberKabinet = nameKabinetAddadnModified.IdNumberKabinet;
                return new ModelReturn<Kabinet>("Добавили новый номер Кабинета: " + nameKabinetAddadnModified.IdNumberKabinet, nameKabinet, nameKabinetAddadnModified.IdNumberKabinet);
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
            return new ModelReturn<Kabinet>("При обновлении/добавлении данных 'Номер Кабинета' по : " + nameKabinetAddadnModified.IdNumberKabinet + " произошла ошибка смотри log.txt");
        }


        /// <summary>
        /// Добавление или обновление наименование модели принтера(МФУ)
        /// </summary>
        /// <param name="nameFullModel">Наименование наименование модели принтера(МФУ)</param>
        public ModelReturn<FullModel> AddAndEditNameFullModel(FullModel nameFullModel)
        {
            var nameFullModelAddadnModified = new FullModel()
            {
               IdModel = nameFullModel.IdModel,
               NameModel = nameFullModel.NameModel,
               IdClasification = nameFullModel.IdClasification
            };
            try
            {
                if ((from FullModels in Inventory.FullModels
                     where FullModels.IdModel == nameFullModelAddadnModified.IdModel
                     select new { FullModels }).Any())
                {
                    Inventory.Entry(nameFullModelAddadnModified).State = EntityState.Modified;
                    Inventory.SaveChanges();
                    return new ModelReturn<FullModel>("Обновили справочник наименование модели принтера(МФУ): " + nameFullModelAddadnModified.IdModel, nameFullModel);
                }
                Inventory.FullModels.Add(nameFullModelAddadnModified);
                Inventory.SaveChanges();
                nameFullModel.IdModel = nameFullModelAddadnModified.IdModel;
                return new ModelReturn<FullModel>("Добавили новое имя модели принтера(МФУ): " + nameFullModelAddadnModified.IdModel, nameFullModel, nameFullModelAddadnModified.IdModel);
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
            return new ModelReturn<FullModel>("При обновлении/добавлении данных 'Наименование модели принтера(МФУ)' по : " + nameFullModelAddadnModified.IdModel + " произошла ошибка смотри log.txt");
        }

        /// <summary>
        /// Добавление или обновление наименование классификации принтера(МФУ)
        /// </summary>
        /// <param name="nameClassification">Наименование наименование классификации принтера(МФУ)</param>
        public ModelReturn<Classification> AddAndEditNameClassification(Classification nameClassification)
        {
            var nameClassificationAddadnModified = new Classification()
            {
               IdClasification = nameClassification.IdClasification,
               NameClass = nameClassification.NameClass
            };
            try
            {
                if ((from Classifications in Inventory.Classifications
                     where Classifications.IdClasification == nameClassificationAddadnModified.IdClasification
                     select new { Classifications }).Any())
                {
                    Inventory.Entry(nameClassificationAddadnModified).State = EntityState.Modified;
                    Inventory.SaveChanges();
                    return new ModelReturn<Classification>("Обновили справочник наименование классификации принтера(МФУ): " + nameClassificationAddadnModified.IdClasification, nameClassification);
                }
                Inventory.Classifications.Add(nameClassificationAddadnModified);
                Inventory.SaveChanges();
                nameClassification.IdClasification = nameClassificationAddadnModified.IdClasification;
                return new ModelReturn<Classification>("Добавили новое имя классификации принтера(МФУ): " + nameClassificationAddadnModified.IdClasification, nameClassification, nameClassificationAddadnModified.IdClasification);
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
            return new ModelReturn<Classification>("При обновлении/добавлении данных 'Наименование классификации принтера(МФУ)' по : " + nameClassificationAddadnModified.IdClasification + " произошла ошибка смотри log.txt");
        }

        /// <summary>
        /// Добавление или обновление наименование производителя принтера(МФУ)
        /// </summary>
        /// <param name="nameFullProizvoditel">Наименование наименование производителя принтера(МФУ)</param>
        public ModelReturn<FullProizvoditel> AddAndEditNameFullProizvoditel(FullProizvoditel nameFullProizvoditel)
        {
            var nameFullProizvoditelAddadnModified = new FullProizvoditel()
            {
                IdProizvoditel = nameFullProizvoditel.IdProizvoditel,
                NameProizvoditel = nameFullProizvoditel.NameProizvoditel
            };
            try
            {
                if ((from FullProizvoditels in Inventory.FullProizvoditels
                     where FullProizvoditels.IdProizvoditel == nameFullProizvoditelAddadnModified.IdProizvoditel
                     select new { FullProizvoditels }).Any())
                {
                    Inventory.Entry(nameFullProizvoditelAddadnModified).State = EntityState.Modified;
                    Inventory.SaveChanges();
                    return new ModelReturn<FullProizvoditel>("Обновили справочник наименование производителя принтера(МФУ): " + nameFullProizvoditelAddadnModified.IdProizvoditel, nameFullProizvoditel);
                }
                Inventory.FullProizvoditels.Add(nameFullProizvoditelAddadnModified);
                Inventory.SaveChanges();
                nameFullProizvoditel.IdProizvoditel = nameFullProizvoditelAddadnModified.IdProizvoditel;
                return new ModelReturn<FullProizvoditel>("Добавили новое имя производителя принтера(МФУ): " + nameFullProizvoditelAddadnModified.IdProizvoditel, nameFullProizvoditel, nameFullProizvoditelAddadnModified.IdProizvoditel);
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
            return new ModelReturn<FullProizvoditel>("При обновлении/добавлении данных 'Наименование производителя принтера(МФУ)' по : " + nameFullProizvoditelAddadnModified.IdProizvoditel + " произошла ошибка смотри log.txt");
        }

        /// <summary>
        /// Добавление или обновление CopySave для МФУ
        /// </summary>
        /// <param name="nameCopySave">Наименование CopySave для МФУ</param>
        public ModelReturn<CopySave> AddAndEditNameCopySave(CopySave nameCopySave)
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
                if ((from CopySaves in Inventory.CopySaves
                     where CopySaves.IdCopySave == nameCopySaveAddadnModified.IdCopySave
                     select new { CopySaves }).Any())
                {
                    Inventory.Entry(nameCopySaveAddadnModified).State = EntityState.Modified;
                    Inventory.SaveChanges();
                    return new ModelReturn<CopySave>("Обновили справочник наименование CopySave для МФУ: " + nameCopySaveAddadnModified.IdCopySave, nameCopySave);
                }
                Inventory.CopySaves.Add(nameCopySaveAddadnModified);
                Inventory.SaveChanges();
                nameCopySave.IdCopySave = nameCopySaveAddadnModified.IdCopySave;
                return new ModelReturn<CopySave>("Добавили новый CopySave для МФУ: " + nameCopySaveAddadnModified.IdCopySave, nameCopySave, nameCopySaveAddadnModified.IdCopySave);
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
            return new ModelReturn<CopySave>("При обновлении/добавлении данных 'CopySave для МФУ' по : " + nameCopySaveAddadnModified.IdCopySave + " произошла ошибка смотри log.txt");
        }
        /// <summary>
        /// Сигнал завершения процесса задачи
        /// </summary>
        /// <param name="idprocess">Ун процесса</param>
        /// <param name="iscomplete">Завершен ли процесс или начат</param>
        public void IsProcessComplete(int idprocess, bool iscomplete)
        {
            var process = Inventory.IsProcessCompletes.FirstOrDefault(x => x.Id == idprocess);
            if (process != null)
            {
                process.IsComplete = iscomplete;
                process.DataStart = iscomplete ? process.DataStart : DateTime.Now;
                process.DataFinish = iscomplete ? DateTime.Now : (DateTime?) null;
            }
               Inventory.Entry(process).State = EntityState.Modified;
               Inventory.SaveChanges();
        }

        /// <summary>
        /// Очистка всех записей и сброса индекса перед добавлением 
        /// </summary>
        public void ClearsHostSynhronization()
        {
            Inventory.ComputerIpAdressSynhronizations.RemoveRange(Inventory.ComputerIpAdressSynhronizations);
            Inventory.Database.ExecuteSqlCommand("DBCC CHECKIDENT ([ComputerIpAdressSynhronization], RESEED, 0)");
        }
        /// <summary>
        /// Добавление записей в таблицу
        /// </summary>
        /// <param name="host">Host</param>
        public void AddHostSynhronization(ComputerIpAdressSynhronization host)
        {
            Inventory.ComputerIpAdressSynhronizations.Add(host);
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
