using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EfDatabase.Inventarization.Base;

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
            var qvery = from Users in Inventarization.Users
                where Users.IdUser == user.IdUser
                select new
                {
                    Users
                };
            if (qvery.Any())
            {
                Inventarization.Entry(user).State = EntityState.Modified;
                Inventarization.SaveChanges();
                log.GenerateHistory(user.IdHistory, 1, "Обновление данных о пользователе: " + user.IdUser);
                return new ModelReturn("Обновили пользователя: " + user.IdUser);  //"Обновили пользователя: " + user.IdUser;
            }
            var usersAdd = new User()
            {
                IdUser = user.IdUser,
                IdOtdel = user.IdOtdel,
                Name = user.Name,
                IdPosition = user.IdPosition,
                IdRule = user.IdRule,
                TabelNumber = user.TabelNumber,
                Telephon = user.Telephon,
                TelephonUndeground = user.TelephonUndeground,
                IpTelephon = user.IpTelephon,
                MacTelephon = user.MacTelephon,
                NameUser = user.NameUser,
                Passwords = user.Passwords,
                StatusActual = user.StatusActual,
            };
            Inventarization.Users.Add(usersAdd);
            Inventarization.SaveChanges();
            log.GenerateHistory(usersAdd.IdHistory, 1, "Добавление нового пользователя: " + usersAdd.IdUser);
            return new ModelReturn("Добавили пользователя: " + usersAdd.IdUser, usersAdd.IdHistory);
        }
        /// <summary>
        /// Добавление отдела без лога данных проверим как работает
        /// </summary>
        /// <param name="otdel">Отдел</param>
        /// <returns></returns>
        public ModelReturn AddAndEditOtdel(Otdel otdel)
        {
            otdel.User = null;
            var qvery = from Otdels in Inventarization.Otdels
                where Otdels.IdOtdel == otdel.IdOtdel
                select new {Otdels};
            if (qvery.Any())
            {
                Inventarization.Entry(otdel).State = EntityState.Modified;
                Inventarization.SaveChanges();
                return new ModelReturn("Обновили отдел: "+otdel.IdOtdel);
            }
            var otdelAdd = new Otdel()
            {
                IdOtdel = otdel.IdOtdel,
                IdUser = otdel.IdUser,
                NameOtdel = otdel.NameOtdel,

            };
            Inventarization.Otdels.Add(otdelAdd);
            Inventarization.SaveChanges();
            return new ModelReturn("Добавили отдел: " + otdelAdd.IdOtdel);
        }


        /// <summary>
        /// Добавление или обновление Принтера
        /// </summary>
        /// <param name="printer"></param>
        public ModelReturn AddAndEditPrinter(Printer printer)
        {
            HistoryLog.HistoryLog log = new HistoryLog.HistoryLog();
            var qvery = from Printers in Inventarization.Printers
                        where Printers.IdPrinter == printer.IdPrinter
                        select new
                        {
                            Printers
                        };
            if (qvery.Any())
            {

                Inventarization.Entry(printer).State = EntityState.Modified;
                Inventarization.SaveChanges();
                log.GenerateHistory(printer.IdHistory, 1, "Обновление данных о пользователе: Пользователь принтер");
                return new ModelReturn("Обновили принтер: " + printer.IdPrinter);
            }
            var printerAdd = new Printer()
            {
                IdPrinter = printer.IdPrinter,
                IdUser = printer.IdUser,
                IdProizvoditel = printer.IdProizvoditel,
                IdModel = printer.IdModel,
                IdNumberKabinet = printer.IdNumberKabinet,
                ZavNumber = printer.ZavNumber,
                ServiceNumber = printer.ServiceNumber,
                InventarNumber = printer.InventarNumber,
                IzmInventarNumber = printer.IzmInventarNumber,
                IpAdress = printer.IpAdress,
                Coment = printer.Coment,
                IdStatus = printer.IdStatus
            };
            Inventarization.Printers.Add(printerAdd);
            Inventarization.SaveChanges();
            log.GenerateHistory(printerAdd.IdHistory, 1, "Добавление нового пользователя: Пользователь принтер");
             return new ModelReturn("Добавили принтер: " + printerAdd.IdPrinter, printerAdd.IdHistory);
        }

        /// <summary>
        /// Добавление или обновление Сканера
        /// </summary>
        /// <param name="scaner"></param>
        public ModelReturn AddAndEditScaner(ScanerAndCamer scaner)
        {
            HistoryLog.HistoryLog log = new HistoryLog.HistoryLog();
            var qvery = from Scaner in Inventarization.ScanerAndCamers
                        where Scaner.IdScaner == scaner.IdScaner
                        select new
                        {
                            Scaner
                        };
            if (qvery.Any())
            {

                Inventarization.Entry(scaner).State = EntityState.Modified;
                Inventarization.SaveChanges();
                log.GenerateHistory(scaner.IdHistory, 1, "Обновление данных о пользователе: Пользователь сканер");
                return new ModelReturn("Обновили принтер: " + scaner.IdScaner);
            }
            var scanerAdd = new ScanerAndCamer()
            {
                IdScaner = scaner.IdScaner,
                IdUser = scaner.IdUser,
                IdProizvoditel = scaner.IdProizvoditel,
                IdModel = scaner.IdModel,
                IdNumberKabinet = scaner.IdNumberKabinet,
                ZavNumber = scaner.ZavNumber,
                ServiceNumber = scaner.ServiceNumber,
                InventarNumber = scaner.InventarNumber,
                IzmInventarNumber = scaner.IzmInventarNumber,
                IpAdress = scaner.IpAdress,
                Coment = scaner.Coment,
                IdStatus = scaner.IdStatus
            };
            Inventarization.ScanerAndCamers.Add(scanerAdd);
            Inventarization.SaveChanges();
            log.GenerateHistory(scanerAdd.IdHistory, 1, "Добавление нового пользователя: Пользователь сканер");
            return new ModelReturn("Добавили сканер: " + scanerAdd.IdScaner, scanerAdd.IdHistory);
        }

        /// <summary>
        /// Добавление или обновление МФУ
        /// </summary>
        /// <param name="mfu"></param>
        public ModelReturn AddAndEditMfu(Mfu mfu)
        {
            HistoryLog.HistoryLog log = new HistoryLog.HistoryLog();
            var qvery = from Mfu in Inventarization.Mfus
                        where Mfu.IdMfu == mfu.IdMfu
                        select new
                        {
                            Mfu
                        };
            if (qvery.Any())
            {

                Inventarization.Entry(mfu).State = EntityState.Modified;
                Inventarization.SaveChanges();
                log.GenerateHistory(mfu.IdHistory, 1, "Обновление данных о пользователе: Пользователь МФУ");
                return new ModelReturn("Обновили МФУ: " + mfu.IdMfu);
            }
            var mfuAdd = new Mfu()
            {
                IdMfu = mfu.IdMfu,
                IdUser = mfu.IdUser,
                IdProizvoditel = mfu.IdProizvoditel,
                IdModel = mfu.IdModel,
                IdNumberKabinet = mfu.IdNumberKabinet,
                ZavNumber = mfu.ZavNumber,
                ServiceNumber = mfu.ServiceNumber,
                InventarNumber = mfu.InventarNumber,
                IzmInventarNumber = mfu.IzmInventarNumber,
                IpAdress = mfu.IpAdress,
                Coment = mfu.Coment,
                IdCopySave = mfu.IdCopySave,
                IdStatus = mfu.IdStatus
            };
            Inventarization.Mfus.Add(mfuAdd);
            Inventarization.SaveChanges();
            log.GenerateHistory(mfuAdd.IdHistory, 1, "Добавление нового пользователя: Пользователь МФУ");
            return new ModelReturn("Добавили МФУ: " + mfuAdd.IdMfu, mfuAdd.IdHistory);
        }

        /// <summary>
        /// Добавление или обновление Системного блока
        /// </summary>
        /// <param name="sysblok"></param>
        public ModelReturn AddAndEditSysBlok(SysBlock sysblok)
        {
            HistoryLog.HistoryLog log = new HistoryLog.HistoryLog();
            var qvery = from SysBlocks in Inventarization.SysBlocks
                        where SysBlocks.IdSysBlock == sysblok.IdSysBlock
                        select new
                        {
                            SysBlocks
                        };
            if (qvery.Any())
            {

                Inventarization.Entry(sysblok).State = EntityState.Modified;
                Inventarization.SaveChanges();
                log.GenerateHistory(sysblok.IdHistory, 1, "Обновление данных о системном блоке: Пользователь Системный блок");
                return new ModelReturn("Обновили Системный блок: " + sysblok.IdSysBlock);
            }
            var sysblokAdd = new SysBlock()
            {
                IdSysBlock = sysblok.IdSysBlock,
                IdUser = sysblok.IdUser,
                IdModelSysBlock = sysblok.IdModelSysBlock,
                IdNumberKabinet = sysblok.IdNumberKabinet,
                ServiceNum = sysblok.ServiceNum,
                SerNum = sysblok.SerNum,
                InventarNumSysBlok = sysblok.InventarNumSysBlok,
                NameComputer = sysblok.NameComputer,
                IpAdress = sysblok.IpAdress,
                Coment = sysblok.Coment,
                IdStatus = sysblok.IdStatus
            };
            Inventarization.SysBlocks.Add(sysblokAdd);
            Inventarization.SaveChanges();
            log.GenerateHistory(sysblokAdd.IdHistory, 1, "Добавление системного блока: Пользователь Системный блок");
            return new ModelReturn("Добавили Системный блок: " + sysblokAdd.IdSysBlock, sysblokAdd.IdHistory);
        }


        /// <summary>
        /// Добавление или обновление Монитора
        /// </summary>
        /// <param name="monitor"></param>
        public ModelReturn AddAndEditMonitors(Monitor monitor)
        {
            HistoryLog.HistoryLog log = new HistoryLog.HistoryLog();
            var qvery = from Monitor in Inventarization.Monitors
                        where Monitor.IdMonitor == monitor.IdMonitor
                        select new
                        {
                            Monitor
                        };
            if (qvery.Any())
            {

                Inventarization.Entry(monitor).State = EntityState.Modified;
                Inventarization.SaveChanges();
                log.GenerateHistory(monitor.IdHistory, 1, "Обновление данных о мониторе: Пользователь Монитор");
                return new ModelReturn("Обновили Монитор: " + monitor.IdMonitor);
            }
            var monitorAdd = new Monitor()
            {
                IdMonitor = monitor.IdMonitor,
                IdUser = monitor.IdUser,
                IdModelMonitor = monitor.IdModelMonitor,
                IdNumberKabinet = monitor.IdNumberKabinet,
                SerNum = monitor.SerNum,
                InventarNumMonitor = monitor.InventarNumMonitor,
                Coment = monitor.Coment,
                IdStatus = monitor.IdStatus
            };
            Inventarization.Monitors.Add(monitorAdd);
            Inventarization.SaveChanges();
            log.GenerateHistory(monitorAdd.IdHistory, 1, "Добавление монитора: Пользователь Монитор");
            return new ModelReturn("Добавили Монитор: " + monitorAdd.IdMonitor, monitorAdd.IdHistory);
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
