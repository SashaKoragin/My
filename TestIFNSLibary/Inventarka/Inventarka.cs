using System;
using System.Threading.Tasks;
using EfDatabase.Inventarization.Base;
using EfDatabase.Inventarization.BaseLogica.AddObjectDb;
using EfDatabase.Inventarization.BaseLogica.Autorization;
using EfDatabase.Inventarization.BaseLogica.Select;
using EfDatabaseParametrsModel;
using SqlLibaryIfns.Inventarization.ModelParametr;
using SqlLibaryIfns.Inventarization.Select;
using TestIFNSLibary.PathJurnalAndUse;


namespace TestIFNSLibary.Inventarka
{
   public class Inventarka : IInventarka
    {
        /// <summary>
        /// Запрос всех отделов
        /// </summary>
        /// <returns></returns>
        public async Task<string> AllOtdels()
        {
            Select auto = new Select();
            return await Task.Factory.StartNew(() => auto.OtdelAll());
        }
        /// <summary>
        /// Авторизация
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<string> Authorization(User user)
        {
            Autorization auto = new Autorization();
            return await Task.Factory.StartNew(() => auto.Identification(user));
        }
        /// <summary>
        /// Простой запрос к БД
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> SelectAllUsers(ModelParametr model)
        {
            var param = new Parametr();
            SelectInventarization select = new SelectInventarization(param.Inventarization);
            return await Task.Factory.StartNew((() => select.SelectFull(model)));
        }
        /// <summary>
        /// Добавление или обновление пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns></returns>
        public ModelReturn AddAndEditUser(User user)
        {
            AddObjectDb add = new AddObjectDb();
           return add.AddAndEditUser(user);
        }
        /// <summary>
        /// Добавление или обновление принтера
        /// </summary>
        /// <param name="printer">Принтер</param>
        /// <returns></returns>
        public ModelReturn AddAndEditPrinter(Printer printer)
        {
            AddObjectDb add = new AddObjectDb();
            return add.AddAndEditPrinter(printer);
        }
        /// <summary>
        /// Добавление или обновление принтера
        /// </summary>
        /// <param name="scaner">Принтер</param>
        /// <returns></returns>
        public ModelReturn AddAndEditScaner(ScanerAndCamer scaner)
        {
            AddObjectDb add = new AddObjectDb();
            return add.AddAndEditScaner(scaner);
        }
        /// <summary>
        /// Добавление или обновление скнера
        /// </summary>
        /// <param name="mfu"></param>
        /// <returns></returns>
        public ModelReturn AddAndEditMfu(Mfu mfu)
        {
            AddObjectDb add = new AddObjectDb();
            return add.AddAndEditMfu(mfu);
        }
        /// <summary>
        /// Добавление или обновление Системного блока
        /// </summary>
        /// <param name="sysblock"></param>
        /// <returns></returns>
        public ModelReturn AddAndEditSysBlok(SysBlock sysblock)
        {
            AddObjectDb add = new AddObjectDb();
            return add.AddAndEditSysBlok(sysblock);
        }
        /// <summary>
        /// Добавление или обновление Монитора
        /// </summary>
        /// <param name="monitor"></param>
        /// <returns></returns>
        public ModelReturn AddAndEditMonitor(Monitor monitor)
        {
            AddObjectDb add = new AddObjectDb();
            return add.AddAndEditMonitors(monitor);
        }
        /// <summary>
        /// Запрос всех пользователей
        /// </summary>
        /// <returns></returns>
        public async Task<string> AllUsers()
        {
            Select auto = new Select();
            return await Task.Factory.StartNew(() => auto.UsersAll());
        }

        /// <summary>
        /// Запрос всех должностей
        /// </summary>
        /// <returns></returns>
        public async Task<string> AllPosition()
        {

            Select auto = new Select();
            return await Task.Factory.StartNew(() => auto.PositionUsers());
        }
        /// <summary>
        /// Загрузка всех принтеров
        /// </summary>
        /// <returns></returns>
        public async Task<string> AllPrinters()
        {
            Select auto = new Select();
            return await Task.Factory.StartNew(() => auto.Printers());
        }
        /// <summary>
        /// Запрос на все сканеры
        /// </summary>
        /// <returns></returns>
        public async Task<string> AllScaners()
        {
            Select auto = new Select();
            return await Task.Factory.StartNew(() => auto.Scaner());
        }
        /// <summary>
        /// Запрос на все МФУ
        /// </summary>
        /// <returns></returns>
        public async Task<string> AllMfu()
        {
            Select auto = new Select();
            return await Task.Factory.StartNew(() => auto.Mfu());
        }
        /// <summary>
        /// Запрос всех системных блоков
        /// </summary>
        /// <returns></returns>
        public async Task<string> AllSysBlok()
        {
            Select auto = new Select();
            return await Task.Factory.StartNew(() => auto.SysBloks());
        }
        /// <summary>
        /// Запрос всех мониторов
        /// </summary>
        /// <returns></returns>
        public async Task<string> AllMonitors()
        {
            Select auto = new Select();
            return await Task.Factory.StartNew(() => auto.Monitors());
        }

        /// <summary>
        /// Запрос на все CopySave
        /// </summary>
        /// <returns></returns>
        public async Task<string> CopySave()
        {
            Select auto = new Select();
            return await Task.Factory.StartNew(() => auto.CopySave());
        }

        /// <summary>
        /// Загрузка всех производителей
        /// </summary>
        /// <returns></returns>
        public async Task<string> Proizvoditel()
        {
            Select auto = new Select();
            return await Task.Factory.StartNew(() => auto.Proizvoditel());
        }
        /// <summary>
        /// Загрузка всех моделей
        /// </summary>
        /// <returns></returns>
        public async Task<string> Model()
        {
            Select auto = new Select();
            return await Task.Factory.StartNew(() => auto.Model());
        }
        /// <summary>
        /// Загрузка всех кабинетов
        /// </summary>
        /// <returns></returns>
        public async Task<string> Kabinet()
        {
            Select auto = new Select();
            return await Task.Factory.StartNew(() => auto.Kabinet());
        }
        /// <summary>
        /// загрузка всех статусов
        /// </summary>
        /// <returns></returns>
        public async Task<string> Statusing()
        {
            Select auto = new Select();
            return await Task.Factory.StartNew(() => auto.Status());
        }
        /// <summary>
        /// загрузка всех производителей системных блоков
        /// </summary>
        /// <returns></returns>
        public async Task<string> NameSysBlock()
        {
            Select auto = new Select();
            return await Task.Factory.StartNew(() => auto.NameSysBlock());
        }
        /// <summary>
        /// загрузка всех производителей мониторов
        /// </summary>
        /// <returns></returns>
        public async Task<string> NameMonitor()
        {
            Select auto = new Select();
            return await Task.Factory.StartNew(() => auto.NameMonitor());
        }

        public async Task<ModelSelect> GenerateSqlSelect(ModelSelect model)
        {
            SelectSql select = new SelectSql();
            return await Task.Factory.StartNew(() =>  select.SqlSelect(model));
        }
    }
}
