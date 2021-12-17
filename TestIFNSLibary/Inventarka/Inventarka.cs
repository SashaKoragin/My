using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EfDatabase.FilterModel;
using EfDatabase.Inventory.Base;
using EfDatabase.Inventory.BaseLogic.AddObjectDb;
using EfDatabase.Inventory.BaseLogic.DeleteObjectDb;
using EfDatabase.Inventory.BaseLogic.Login;
using EfDatabase.Inventory.BaseLogic.Select;
using EfDatabase.Inventory.MailLogicLotus;
using EfDatabase.Inventory.ReportXml.ReturnModelError;
using EfDatabase.Inventory.SqlModelSelect;
using EfDatabase.MemoReport;
using EfDatabase.ReportCard;
using EfDatabase.SettingModelInventory;
using EfDatabase.XsdBookAccounting;
using EfDatabase.XsdInventoryRuleAndUsers;
using EfDatabaseParametrsModel;
using EfDatabaseXsdInventoryAutorization;
using EfDatabaseXsdMail;
using EfDatabaseXsdQrCodeModel;
using EfDatabaseXsdSupportNalog;
using LibaryDocumentGenerator.Barcode;
using LibaryDocumentGenerator.Documents.Template;
using LibaryDocumentGenerator.Documents.TemplateExcel;
using LibaryXMLAuto.ReadOrWrite;
using LibraryAutoSupportSto.PassportSto.PassportStoPostGet;
using SqlLibaryIfns.Inventory.ModelParametr;
using SqlLibaryIfns.Inventory.Select;
using SqlLibaryIfns.SqlSelect.ImnsKadrsSelect;
using SqlLibaryIfns.SqlZapros.SqlConnections;
using SqlLibaryIfns.ZaprosSelectNotParam;
using TestIFNSLibary.PathJurnalAndUse;
using BlockPower = EfDatabase.Inventory.Base.BlockPower;
using CopySave = EfDatabase.Inventory.Base.CopySave;
using FullModel = EfDatabase.Inventory.Base.FullModel;
using FullProizvoditel = EfDatabase.Inventory.Base.FullProizvoditel;
using Kabinet = EfDatabase.Inventory.Base.Kabinet;
using LogicaSelect = EfDatabaseParametrsModel.LogicaSelect;
using Mfu = EfDatabase.Inventory.Base.Mfu;
using ModelBlockPower = EfDatabase.Inventory.Base.ModelBlockPower;
using NameSysBlock = EfDatabase.Inventory.Base.NameSysBlock;
using Otdel = EfDatabase.Inventory.Base.Otdel;
using Printer = EfDatabase.Inventory.Base.Printer;
using ProizvoditelBlockPower = EfDatabase.Inventory.Base.ProizvoditelBlockPower;
using ScanerAndCamer = EfDatabase.Inventory.Base.ScanerAndCamer;
using Supply = EfDatabase.Inventory.Base.Supply;
using Swithe = EfDatabase.Inventory.Base.Swithe;
using SysBlock = EfDatabase.Inventory.Base.SysBlock;
using Telephon = EfDatabase.Inventory.Base.Telephon;
using User = EfDatabase.Inventory.Base.User;
using LibraryAutoSupportSto.Support.SupportPostGet;
using Organization = EfDatabase.Inventory.Base.Organization;
using Type = System.Type;

namespace TestIFNSLibary.Inventarka
{
   public class Inventarka  : IInventarka
   {
       private readonly Parameter parametersService = new Parameter();

       /// <summary>
        /// Запрос всех отделов
        /// </summary>
        /// <returns></returns>
        public async Task<string> AllOtdels()
        {
            Select auto = new Select();
            return await Task.Factory.StartNew(() => auto.DepartmentAll());
        }
        /// <summary>
        /// Авторизация
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<Autorization> Authorization(Autorization user)
        {
            Login auto = new Login();
            return await Task.Factory.StartNew(() =>
            {
                if (user.Login != null)
                {
                    using (PrincipalContext context = new PrincipalContext(ContextType.Domain, null, user.Login, user.Password))
                    {
                        if (context.ValidateCredentials(user.Login, user.Password))
                        {
                            return auto.Identification(user);
                        }
                        user.ErrorAutorization = "Не правильный логин/пароль!!!";
                        return user;
                    }
                }
                user.ErrorAutorization = "Пользователь не введен!!!";
                return user;
            });
        }
        /// <summary>
        /// Простой запрос к БД
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> SelectAllUsers(ModelParametr model)
        {
            var param = new Parameter();
            SelectInventory select = new SelectInventory(param.Inventarization);
            return await Task.Factory.StartNew((() => select.SelectFull(model)));
        }
        /// <summary>
        /// Получение статистики актулизированных пользователей
        /// </summary>
        /// <returns></returns>
        public async Task<string> AllActualsProcedureUsers()
        {
            Select auto = new Select();
            return await Task.Factory.StartNew(() => auto.ActualsUsersKladr());
        }
        /// <summary>
        /// Добавление или обновление пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <param name="userIdEdit">Пользователь кто редактировал</param>
        /// <returns></returns>
        public ModelReturn<User> AddAndEditUser(User user, string userIdEdit)
        {

            AddObjectDb add = new AddObjectDb();
            var model = add.AddAndEditUser(user,SignalRLibary.SignalRinventory.SignalRinventory.GetUser(userIdEdit));
            add.Dispose();
            if (model.Model != null) { SignalRLibary.SignalRinventory.SignalRinventory.SubscribeUser(model.Model); }
            return model;
        }
        /// <summary>
        /// Добавление или обновление принтера
        /// </summary>
        /// <param name="printer">Принтер</param>
        /// <param name="userIdEdit">Пользователь кто редактировал</param>
        /// <returns></returns>
        public ModelReturn<Printer> AddAndEditPrinter(Printer printer, string userIdEdit)
        {
            AddObjectDb add = new AddObjectDb();
            var model = add.AddAndEditPrinter(printer, SignalRLibary.SignalRinventory.SignalRinventory.GetUser(userIdEdit));
            add.Dispose();
            if (model.Model != null) { SignalRLibary.SignalRinventory.SignalRinventory.SubscribePrinter(model.Model); }
            return model;
        }
        /// <summary>
        /// Добавление или обновление Коммутаторов
        /// </summary>
        /// <param name="swith">Коммутатор</param>
        /// <param name="userIdEdit">Пользователь кто редактировал</param>
        /// <returns></returns>
        public ModelReturn<Swithe> AddAndEditSwith(Swithe swith, string userIdEdit)
       {
            AddObjectDb add = new AddObjectDb();
            var model = add.AddAndEditSwiths(swith, SignalRLibary.SignalRinventory.SignalRinventory.GetUser(userIdEdit));
            add.Dispose();
            if (model.Model != null) { SignalRLibary.SignalRinventory.SignalRinventory.SubscribeSwithe(model.Model); }
            return model;
        }

        /// <summary>
        /// Добавление или обновление принтера
        /// </summary>
        /// <param name="scaner">Сканер</param>
        /// <param name="userIdEdit">Пользователь кто редактировал</param>
        /// <returns></returns>
        public ModelReturn<ScanerAndCamer> AddAndEditScaner(ScanerAndCamer scaner, string userIdEdit)
        {
            AddObjectDb add = new AddObjectDb();
            var model = add.AddAndEditScaner(scaner, SignalRLibary.SignalRinventory.SignalRinventory.GetUser(userIdEdit));
            add.Dispose();
            if (model.Model != null) { SignalRLibary.SignalRinventory.SignalRinventory.SubscribeScanerAndCamer(model.Model); }
            return model;
        }
        /// <summary>
        /// Добавление или редактирование серверного оборудования
        /// </summary>
        /// <param name="serverEquipment">Серверное оборудование</param>
        /// <param name="userIdEdit">Пользователь кто редактировал</param>
        /// <returns></returns>
        public ModelReturn<ServerEquipment> AddAndEditServerEquipment(ServerEquipment serverEquipment, string userIdEdit)
        {
            AddObjectDb add = new AddObjectDb();
            var model = add.AddAndEditServerEquipment(serverEquipment, SignalRLibary.SignalRinventory.SignalRinventory.GetUser(userIdEdit));
            add.Dispose();
            if (model.Model != null) { SignalRLibary.SignalRinventory.SignalRinventory.SubscribeServerEquipment(model.Model); }
            return model;
        }
        /// <summary>
        /// Добавление или редактирования модели серверного оборудования
        /// </summary>
        /// <param name="modelSeverEquipment">Модель серверного оборудования</param>
        /// <returns></returns>
        public ModelReturn<ModelSeverEquipment> AddAndEditModelSeverEquipment(ModelSeverEquipment modelSeverEquipment)
        {
            AddObjectDb add = new AddObjectDb();
            var model = add.AddAndEditModelSeverEquipment(modelSeverEquipment);
            add.Dispose();
            if (model.Model != null) { SignalRLibary.SignalRinventory.SignalRinventory.SubscribeModelSeverEquipment(model.Model); }
            return model;
        }
        /// <summary>
        /// Добавление или редактирования производителя серверного оборудования
        /// </summary>
        /// <param name="manufacturerSeverEquipment">Производитель серверного оборудования</param>
        /// <returns></returns>
        public ModelReturn<ManufacturerSeverEquipment> AddAndEditManufacturerSeverEquipment(ManufacturerSeverEquipment manufacturerSeverEquipment)
        {
            AddObjectDb add = new AddObjectDb();
            var model = add.AddAndEditManufacturerSeverEquipment(manufacturerSeverEquipment);
            add.Dispose();
            if (model.Model != null) { SignalRLibary.SignalRinventory.SignalRinventory.SubscribeManufacturerSeverEquipment(model.Model); }
            return model;
        }

        public ModelReturn<TypeServer> AddAndEditTypeServer(TypeServer typeServer)
        {
            AddObjectDb add = new AddObjectDb();
            var model = add.AddAndEditTypeServer(typeServer);
            add.Dispose();
            if (model.Model != null) { SignalRLibary.SignalRinventory.SignalRinventory.SubscribeTypeServer(model.Model); }
            return model;
        }

        /// <summary>
        /// Добавление или обновление скнера
        /// </summary>
        /// <param name="mfu">МФУ</param>
        /// <param name="userIdEdit">Пользователь кто редактировал</param>
        /// <returns></returns>
        public ModelReturn<EfDatabase.Inventory.Base.Mfu> AddAndEditMfu(EfDatabase.Inventory.Base.Mfu mfu, string userIdEdit)
        {
            AddObjectDb add = new AddObjectDb();
            var model = add.AddAndEditMfu(mfu, SignalRLibary.SignalRinventory.SignalRinventory.GetUser(userIdEdit));
            add.Dispose();
            if (model.Model != null) { SignalRLibary.SignalRinventory.SignalRinventory.SubscribeMfu(model.Model); }
            return model;
        }
        /// <summary>
        /// Добавление или обновление Системного блока
        /// </summary>
        /// <param name="sysblock">Системный блок</param>
        /// <param name="userIdEdit">Пользователь кто редактировал</param>
        /// <returns></returns>
        public ModelReturn<EfDatabase.Inventory.Base.SysBlock> AddAndEditSysBlok(EfDatabase.Inventory.Base.SysBlock sysblock, string userIdEdit)
        {
            AddObjectDb add = new AddObjectDb();
            var model = add.AddAndEditSysBlok(sysblock, SignalRLibary.SignalRinventory.SignalRinventory.GetUser(userIdEdit));
            add.Dispose();
            if (model.Model != null) { SignalRLibary.SignalRinventory.SignalRinventory.SubscribeSysBlok(model.Model); }
            return model;
        }
        /// <summary>
        /// Добавление или редактирование отдела
        /// </summary>
        /// <param name="otdel">отдел</param>
        /// <returns></returns>
        public ModelReturn<Otdel> AddAndEditOtdel(Otdel otdel)
        {
            AddObjectDb add = new AddObjectDb();
            var model = add.AddAndEditDepartment(otdel);
            add.Dispose();
            if (model.Model != null) { SignalRLibary.SignalRinventory.SignalRinventory.SubscribeOtdel(model.Model); }
            return model;
        }
        /// <summary>
        /// Добавление или обновление Монитора
        /// </summary>
        /// <param name="monitor"></param>
        /// <param name="userIdEdit">Пользователь кто редактировал</param>
        /// <returns></returns>
        public ModelReturn<Monitor> AddAndEditMonitor(Monitor monitor, string userIdEdit)
        {
            AddObjectDb add = new AddObjectDb();
            var model = add.AddAndEditMonitors(monitor, SignalRLibary.SignalRinventory.SignalRinventory.GetUser(userIdEdit));
            add.Dispose();
            if (model.Model != null) { SignalRLibary.SignalRinventory.SignalRinventory.SubscribeMonitor(model.Model); }
            return model;
        }
        /// <summary>
        /// Выгрузка ролей пользователя по IdUser
        /// </summary>
        /// <returns></returns>
        public async Task<RuleUsers[]> RuleAndUsers(int idUser)
        {
            Select auto = new Select();
            return await Task.Factory.StartNew(() => auto.AllRuleUser(idUser));
        }
        /// <summary>
        /// Выгрузка праздничных дней
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetHoliday()
        {
            Select auto = new Select();
            return await Task.Factory.StartNew(() => auto.GetHolidays());
        }
        /// <summary>
        /// Добавление или редактирование справочника праздничных дней
        /// </summary>
        /// <param name="holidays">Запись о праздничном дне</param>
        /// <param name="userIdEdit">Пользователь вносивший изменения</param>
        /// <returns></returns>
        public ModelReturn<Rb_Holiday> AddAndEditRbHoliday(Rb_Holiday holidays, string userIdEdit)
        {
            holidays.DateTime_Holiday = holidays.DateTime_Holiday.AddHours(3);
            AddObjectDb add = new AddObjectDb();
            var model = add.AddAndEditHoliday(holidays);
            add.Dispose();
            if (model.Model != null) { SignalRLibary.SignalRinventory.SignalRinventory.SubscribeRbHoliday(model.Model); }
            return model;
        }
        /// <summary>
        /// Удаление не актуальных праздничных дней
        /// </summary>
        /// <param name="holidays">Запись о праздничном дне</param>
        /// <param name="userIdEdit">Пользователь вносивший изменения</param>
        /// <returns></returns>
        public ModelReturn<Rb_Holiday> DeleteRbHoliday(Rb_Holiday holidays, string userIdEdit)
        {
            DeleteObjectDb delete = new DeleteObjectDb();
            var model = delete.DeleteHoliday(holidays, SignalRLibary.SignalRinventory.SignalRinventory.GetUser(userIdEdit));
            SignalRLibary.SignalRinventory.SignalRinventory.SubscribeDeleteHoliday(model);
            return model;
        }


        /// <summary>
        /// Удаление или добавление роли пользователя
        /// </summary>
        /// <param name="ruleUsers">Роль пользователя</param>
        /// <returns></returns>
        public async Task<RuleUsers[]> AddAndDeleteRuleUser(RuleUsers ruleUsers)
        {
            AddObjectDb add = new AddObjectDb();
            Select auto = new Select();
            return await Task.Factory.StartNew(() =>
            {
                add.AddAndDeleteRuleUsers(ruleUsers);
                add.Dispose();
                return auto.AllRuleUser((int)ruleUsers.IdUser);
            });
        }
        /// <summary>
        /// Редактирование или добавление настроек организации
        /// </summary>
        /// <param name="organization">Модель организации</param>
        /// <param name="userIdEdit">Пользователь вносивший изменения</param>
        /// <returns></returns>
        public ModelReturn<Organization> AddAndEditOrganization(Organization organization, string userIdEdit)
        {
            AddObjectDb add = new AddObjectDb();
            var model = add.AddAndEditOrganization(organization);
            add.Dispose();
            if (model.Model != null) { SignalRLibary.SignalRinventory.SignalRinventory.SubscribeOrganization(model.Model); }
            return model;
        }
        /// <summary>
        /// Редактирование или добавление настроек падежей отдела 
        /// </summary>
        /// <param name="settingDepartmentCase">падежи отдела</param>
        /// <param name="userIdEdit">Пользователь вносивший изменения</param>
        /// <returns></returns>
        public ModelReturn<SettingDepartmentCase> AddAndEditSettingDepartmentCase(SettingDepartmentCase settingDepartmentCase, string userIdEdit)
        {
            AddObjectDb add = new AddObjectDb();
            var model = add.AddAndEditSettingDepartmentCase(settingDepartmentCase);
            add.Dispose();
            if (model.Model != null) { SignalRLibary.SignalRinventory.SignalRinventory.SubscribeDepartmentCase(model.Model); }
            return model;
        }
        /// <summary>
        /// Обновление или обновление регламентов отдела
        /// </summary>
        /// <param name="regulationsDepartment">Регламент отдела</param>
        /// <param name="userIdEdit">Пользователь вносивший изменения</param>
        /// <returns></returns>
        public ModelReturn<RegulationsDepartment> AddAndEditSettingDepartmentRegulations(RegulationsDepartment regulationsDepartment, string userIdEdit)
        {
            AddObjectDb add = new AddObjectDb();
            var model = add.AddAndEditSettingDepartmentRegulations(regulationsDepartment);
            add.Dispose();
            if (model.Model != null) { SignalRLibary.SignalRinventory.SignalRinventory.SubscribeDepartmentRegulations(model.Model); }
            return model;
        }
        /// <summary>
        /// Глобальные настройки организации
        /// </summary>
        /// <returns></returns>
        public async Task<Organization> SettingOrganization()
        {
            Select auto = new Select();
            return await Task.Factory.StartNew(() => auto.SettingOrganization());
        }
        /// <summary>
        /// Настройки падежей организации
        /// </summary>
        /// <returns></returns>
        public async Task<string> SettingDepartmentCase()
        {
            Select auto = new Select();
            return await Task.Factory.StartNew(() => auto.SettingDepartmentCase());
        }
        /// <summary>
        /// Регламенты отделов 
        /// </summary>
        /// <returns></returns>
        public async Task<string> SettingDepartmentRegulations()
        {
            Select auto = new Select();
            return await Task.Factory.StartNew(() => auto.SettingDepartmentRegulations());
        }
        /// <summary>
        /// Запрос всех пользователей
        /// </summary>
        /// <returns></returns>
        public async Task<string> AllUsers(AllUsersFilters filterActual)
        {
            Select auto = new Select();
            return await Task.Factory.StartNew(() => auto.UsersAll(filterActual));
        }
        /// <summary>
        /// Запрос на все роли
        /// </summary>
        /// <returns></returns>
        public async Task<string> AllRules()
        {
            Select auto = new Select();
            return await Task.Factory.StartNew(() => auto.RuleAll());
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
        /// Выгрузка всех моделей коммутаторов
        /// </summary>
        /// <returns></returns>
        public async Task<string> AllModelSwithes()
        {
            Select auto = new Select();
            return await Task.Factory.StartNew(() => auto.ModelSwitch());
        }

        /// <summary>
        /// Загрузка всех комутаторов
        /// </summary>
        /// <returns></returns>
        public async Task<string> AllSwithes()
        {
            Select auto = new Select();
            return await Task.Factory.StartNew(() => auto.Swithes());
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
        /// Запрос всего разного оборудования
        /// </summary>
        /// <returns></returns>
        public async Task<string> AllOtherAll()
        {
            Select auto = new Select();
            return await Task.Factory.StartNew(() => auto.OtherAll());
        }
        /// <summary>
        /// Все модели разного оборудования
        /// </summary>
        /// <returns></returns>
        public async Task<string> AllModelOther()
        {
            Select auto = new Select();
            return await Task.Factory.StartNew(() => auto.AllModelOther());
        }
        /// <summary>
        /// Все типы разного оборудования
        /// </summary>
        /// <returns></returns>
        public async Task<string> AllTypeOther()
        {
            Select auto = new Select();
            return await Task.Factory.StartNew(() => auto.AllTypeOther());
        }
        /// <summary>
        /// Все производители разного оборудования
        /// </summary>
        /// <returns></returns>
        public async Task<string> AllProizvoditelOther()
        {
            Select auto = new Select();
            return await Task.Factory.StartNew(() => auto.AllProizvoditelOther());
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
        /// <summary>
        /// Генерация выборки на клиенте 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ModelSelect> GenerateSqlSelect(ModelSelect model)
        {
            SelectSql select = new SelectSql();
            return await Task.Factory.StartNew(() =>  select.SqlSelect(model));
        }

        /// <summary>
        /// Генерация телефонного справочника инспекции
        /// </summary>
        /// <param name="telephonehelper"></param>
        /// <returns></returns>
        public async Task<Stream> GenerateTelephoneHelper(ModelSelect telephonehelper)
        {
            try
            {
                SelectSql select = new SelectSql();
                var selectfull = new SelectFull(parametersService.Inventarization);
                TelephoneHelp invoice = new TelephoneHelp();
                return await Task.Factory.StartNew(() =>
                { 
                    telephonehelper.LogicaSelect = select.SqlSelectModel(telephonehelper.ParametrsSelect.Id);
                    invoice.CreateDocument(parametersService.Report, (EfDatabaseTelephoneHelp.TelephoneHelp)selectfull.GenerateSchemeXsdSql<string,string>(telephonehelper.LogicaSelect), null);
                    return invoice.FileArray();
                });
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
            return null;
        }
        /// <summary>
        /// Запрос на get получение файла по телефонам
        /// </summary>
        /// <returns></returns>
        public async Task<Stream> GenerateFileXlsxSqlView(LogicaSelect selectLogic)
        {
            var selectFull = new SelectFull(parametersService.Inventarization);
            return await Task.Factory.StartNew(() => selectFull.GenerateStreamToSqlViewFile(selectLogic.SelectUser, selectLogic.NameReportFile, selectLogic.NameReportList, parametersService.ReportMassTemplate));
        }

        /// <summary>
        /// Генерация книги учета материальных ценностей
        /// </summary>
        /// <param name="bookModels">Модель для процедуры</param>
        /// <returns></returns>
        public async Task<Stream> GenerateBookAccounting(BookModels bookModels)
        {
            try
            {
                return await Task.Factory.StartNew(() =>
                {
                    BookAccountingInventarka bookAccounting = new BookAccountingInventarka();
                    SelectSql select = new SelectSql();
                    Report rep = new Report();
                    GenerateBarcode barecode = new GenerateBarcode();
                    SelectFull selectfull = new SelectFull(parametersService.Inventarization);
                    ModelSelect modelSelect = new ModelSelect
                         {
                           ParametrsSelect = new ParametrsSelect {Id = 12},
                           LogicaSelect = new LogicaSelect() {Id = 12}
                         };
                    modelSelect.LogicaSelect = select.SqlSelectModel(modelSelect.ParametrsSelect.Id);
                    Dictionary<string, string> parametr = new Dictionary<string, string>
                    {
                        {modelSelect.LogicaSelect.SelectedParametr.Split(',')[0], bookModels.Name},
                        {modelSelect.LogicaSelect.SelectedParametr.Split(',')[1], bookModels.Id.ToString()}
                    };
                    Book book =(Book) selectfull.GenerateSchemeXsdSql( modelSelect.LogicaSelect,parametr); //Получение данных для модели
                    book.BareCodeBook = rep.BookInvoce(book.BareCodeBook, bookModels); //Раскладывает в БД
                    barecode.GenerateBookCode(book.BareCodeBook, parametersService.Report);//Генерит Штрихкод на основе раскладки БД
                    bookAccounting.CreateDocument(parametersService.Report, book, null);
                    File.Delete(book.BareCodeBook.FullPathSave);
                    return bookAccounting.FileArray();
                });
            }
            catch (Exception e)
            {
               Loggers.Log4NetLogger.Error(e);
            }
            return null;
        }

        /// <summary>
        /// Выборка
        /// </summary>
        /// <param name="logica">Логика выборки</param>
        /// <returns></returns>
        public async Task<string> SelectXml(LogicaSelect logica)
        {
            return await Task.Factory.StartNew(() =>
            {
                string model = null;
                if (logica.SelectUser != null)
                {
                    Type type = Type.GetType($"{logica.FindNameSpace}, {logica.NameDll}");
                    model = (string)typeof(FullSelectModelInventory).GetMethod("SqlModelInventory")?.MakeGenericMethod(type).Invoke(new FullSelectModelInventory(), new object[] { logica });
                }
                return model;
            });
        }
        /// <summary>
        /// Генерация накладной
        /// </summary>
        public async Task<Stream> GenerateDocuments(EfDatabaseInvoice.Report report)
        {
               try
               {
                return await Task.Factory.StartNew(() =>
                    {
                       Report rep = new Report();
                       InvoiceInventarka invoice = new InvoiceInventarka();
                       GenerateBarcode barecode = new GenerateBarcode();
                       rep.ReportInvoice(ref report);
                       barecode.GenerateCode(ref report, parametersService.Report);
                       invoice.CreateDocument(parametersService.Report, report, null);
                       File.Delete(report.Main.Barcode.PathBarcode);
                       return invoice.FileArray();
                    });
               }
               catch (Exception e)
               {
                   Loggers.Log4NetLogger.Error(e);
               }
            return null;
        }

        public string DeleteDocument(int iddoc)
        {
                AddObjectDb delete = new AddObjectDb();
                return delete.DeleteDocument(iddoc);            
        }
        /// <summary>
        /// Выгрузка документа
        /// </summary>
        /// <param name="iddoc">Ун документа</param>
        /// <returns></returns>
        public Stream LoadDocument(int iddoc)
        {
            AddObjectDb selectdocument = new AddObjectDb();
            return selectdocument.LoadDocuments(iddoc);
        }
        /// <summary>
        /// Выгрузка последней актуальной книги учета 
        /// </summary>
        /// <param name="iddoc">Ун книги учета</param>
        /// <returns></returns>
        public Stream LoadBook(int iddoc)
        {
            AddObjectDb selectdocument = new AddObjectDb();
            return selectdocument.LoadBook(iddoc);
        }


        /// <summary>
        /// Обновление данных по документу по штрих коду
        /// </summary>
        /// <param name="uploadFileModel">Выгруженная модель с клиента</param>
        /// <returns></returns>
        public ModelError[] AddFileToDb(EfDatabaseUploadFile.UploadFile uploadFileModel)
        {
            AddObjectDb add = new AddObjectDb();
            var identitybarcode = new GenerateBarcode();
            ModelError[] error = new ModelError[uploadFileModel.Upload.Length];
            for (int i = 0; i < uploadFileModel.Upload.Length; i++)
            {
                identitybarcode.DecodeBarCodePng(ref uploadFileModel.Upload[i], parametersService.Report);
                if (uploadFileModel.Upload[i].IdDocument != 0)
                {
                    switch (uploadFileModel.ClassFileToServer)
                    {
                        case "Waybill":
                            error[i] = add.UpdateDocument(uploadFileModel.Upload[i]);
                            break;
                        case "Book":
                            error[i] = add.UpdateBook(uploadFileModel.Upload[i]);
                            break;
                    }
                }
                else
                {
                    error[i] = new ModelError()
                    {
                        IdDocument = uploadFileModel.Upload[i].IdDocument,
                        IdError = 1,
                        Messages = "Не распознается штрих код на документе " + uploadFileModel.Upload[i].NameFile
                    };
                }
            }
            add.Dispose();
            return error;
        }
        /// <summary>
        /// Актуализация пользователей
        /// </summary>
        /// <returns></returns>
        public async Task<string> ActualUsers()
        {
            try
            {
                SelectSql select = new SelectSql();
                SqlConnectionType sql = new SqlConnectionType();
                SelectImns selectFrames = new SelectImns();
                var isActualizationUser = sql.XmlString(parametersService.ConnectImns51, selectFrames.ActualUsers);
                return await Task.Factory.StartNew(() => select.ActualUserModel(isActualizationUser));
            }
            catch (Exception e)
            {
               Loggers.Log4NetLogger.Error(e);
            }
            return "Возникла не предвиденная ошибка смотри Log.txt";
        }
        /// <summary>
        /// Актуализация Ip Адресов в БД
        /// </summary>
        /// <returns></returns>
        public async Task<string> ActualComputerIp()
        {
            try
            {
                SelectSql select = new SelectSql();
                return await Task.Factory.StartNew(() => select.ActualIp());
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
                return e.Message;
            }
        }

        /// <summary>
        /// Выгрузка телефонов
        /// </summary>
        /// <returns></returns>
        public async Task<string> Telephon()
        {
           Select auto = new Select();
           return await Task.Factory.StartNew(() => auto.Telephon());
        }
        /// <summary>
        /// Типы серверов
        /// </summary>
        /// <returns></returns>
        public async Task<string> TypeServer()
        {
           Select auto = new Select();
           return await Task.Factory.StartNew(() => auto.TypeServer());
        }


        /// <summary>
        /// Выгрузка бесперебойников
        /// </summary>
        /// <returns></returns>
        public async Task<string> BlockPower()
        {
           Select auto = new Select();
           return await Task.Factory.StartNew(() => auto.BlockPower());
        }
        /// <summary>
        /// Серверное оборудование
        /// </summary>
        /// <returns></returns>
        public async Task<string> ServerEquipment()
        {
           Select auto = new Select();
           return await Task.Factory.StartNew(() => auto.ServerEquipment());
        }
        /// <summary>
        /// Модели серверного оборудования
        /// </summary>
        /// <returns></returns>
        public async Task<string> ModelSeverEquipment()
        {
           Select auto = new Select();
           return await Task.Factory.StartNew(() => auto.ModelSeverEquipment());
        }
        /// <summary>
        /// Получение ресурсов для заявки АИС 3
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetResourceIt()
        {
           Select auto = new Select();
           return await Task.Factory.StartNew(() => auto.GetResourceIt());
        }
        /// <summary>
        /// Получение задач для заявки АИС 3
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetTaskAis3()
        {
           Select auto = new Select();
           return await Task.Factory.StartNew(() => auto.GetTaskAis3());
        }
        /// <summary>
        /// Получение журнала заявок на различные ресурсы
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetJournalAis3()
        {
           Select auto = new Select();
           return await Task.Factory.StartNew(() => auto.GetJournalAis3());
        }
        /// <summary>
        /// Добавление или обновление ресурса для заявки
        /// </summary>
        /// <param name="resourceIt">Ресурс для заявки</param>
        /// <returns></returns>
        public ModelReturn<ResourceIt> AddAndEditResourceIt(ResourceIt resourceIt)
        {
            AddObjectDb add = new AddObjectDb();
            var model = add.AddAndEditResourceIt(resourceIt);
            add.Dispose();
            if (model.Model != null) { SignalRLibary.SignalRinventory.SignalRinventory.SubscribeResourceIt(model.Model); }
            return model;
        }

        /// <summary>
        /// Добавление или обновление задачи для заявки
        /// </summary>
        /// <param name="taskAis3">Задачи для заявки</param>
        /// <returns></returns>
        public ModelReturn<TaskAis3> AddAndEditTaskAis3(TaskAis3 taskAis3)
        {
            AddObjectDb add = new AddObjectDb();
            var model = add.AddAndEditTaskAis3(taskAis3);
            add.Dispose();
            if (model.Model != null) { SignalRLibary.SignalRinventory.SignalRinventory.SubscribeTaskAis3(model.Model); }
            return model;
        }
        /// <summary>
        /// Добавление или удаление записи в журнале доступа
        /// </summary>
        /// <param name="journalAis3">Запись в журнале</param>
        /// <returns></returns>
        public ModelReturn<JournalAis3> AddAndEditJournalAis3(JournalAis3 journalAis3)
        {
            journalAis3.DateTask = journalAis3.DateTask.AddHours(3);
            AddObjectDb add = new AddObjectDb();
            var model = add.AddAndEditJournalAis3(journalAis3);
            add.Dispose();
            if (model.Model != null) { SignalRLibary.SignalRinventory.SignalRinventory.SubscribeJournalAis3(model.Model); }
            return model;
        }
        /// <summary>
        /// производители серверного оборудования
        /// </summary>
        /// <returns></returns>
        public async Task<string> ManufacturerSeverEquipment()
       {
           Select auto = new Select();
           return await Task.Factory.StartNew(() => auto.ManufacturerSeverEquipment());
        }

       /// <summary>
       /// Поставщики
       /// </summary>
       /// <returns></returns>
       public async Task<string> Supply()
       {
           Select auto = new Select();
           return await Task.Factory.StartNew(() => auto.Supply());
       }
       /// <summary>
       /// Все модели системных блоков
       /// </summary>
       /// <returns></returns>
       public async Task<string> ModelBlockPower()
       {
           Select auto = new Select();
           return await Task.Factory.StartNew(() => auto.ModelBlockPower());
       }
       /// <summary>
       /// Все производители системных блоков
       /// </summary>
       /// <returns></returns>
       public async Task<string> ProizvoditelBlockPower()
       {
           Select auto = new Select();
           return await Task.Factory.StartNew(() => auto.ProizvoditelBlockPower());
       }
       /// <summary>
       /// Добавление или обновление телефона
       /// </summary>
       /// <param name="telephon">Телефон</param>
       /// <param name="userIdEdit">Пользователь кто редактировал</param>
       /// <returns></returns>
       public ModelReturn<EfDatabase.Inventory.Base.Telephon> AddAndEditTelephon(EfDatabase.Inventory.Base.Telephon telephon,string userIdEdit)
       {
           AddObjectDb add = new AddObjectDb();
           var model =  add.AddAndEditTelephone(telephon, SignalRLibary.SignalRinventory.SignalRinventory.GetUser(userIdEdit));
           add.Dispose();
            if (model.Model != null){ SignalRLibary.SignalRinventory.SignalRinventory.SubscribeTelephone(model.Model);}
           return model;
       }
       /// <summary>
       /// Добавление или обновление ИБП
       /// </summary>
       /// <param name="blockpower">ИБП</param>
       /// <param name="userIdEdit">Пользователь кто редактировал</param>
       /// <returns></returns>
       public ModelReturn<EfDatabase.Inventory.Base.BlockPower> AddAndEditBlockPower(EfDatabase.Inventory.Base.BlockPower blockpower, string userIdEdit)
       {
           AddObjectDb add = new AddObjectDb();
           var model = add.AddAndEditPowerBlock(blockpower, SignalRLibary.SignalRinventory.SignalRinventory.GetUser(userIdEdit));
           add.Dispose();
            if (model.Model != null) { SignalRLibary.SignalRinventory.SignalRinventory.SubscribeBlockPower(model.Model); }
           return model;
       }

       public ModelReturn<NameSysBlock> AddAndEditNameSysBlock(NameSysBlock nameSysBlock)
       {
           AddObjectDb add = new AddObjectDb();
           var model = add.AddAndEditNameSysBlock(nameSysBlock);
           add.Dispose();
           if (model.Model != null) { SignalRLibary.SignalRinventory.SignalRinventory.SubscribeNameSysBlock(model.Model); }
           return model;
       }

       public ModelReturn<EfDatabase.Inventory.Base.NameMonitor> AddAndEditNameMonitor(EfDatabase.Inventory.Base.NameMonitor nameMonitor)
       {
           AddObjectDb add = new AddObjectDb();
           var model = add.AddAndEditNameMonitor(nameMonitor);
           add.Dispose();
           if (model.Model != null) { SignalRLibary.SignalRinventory.SignalRinventory.SubscribeNameMonitor(model.Model); }

           return model;
       }

       public ModelReturn<ModelBlockPower> AddAndEditNameModelBlokPower(ModelBlockPower nameModelBlokPower)
       {
           AddObjectDb add = new AddObjectDb();
           var model = add.AddAndEditNameModelBlokPower(nameModelBlokPower);
           add.Dispose();
           if (model.Model != null) { SignalRLibary.SignalRinventory.SignalRinventory.SubscribeModelBlockPower(model.Model); }
           return model;
       }

       public ModelReturn<ProizvoditelBlockPower> AddAndEditNameProizvoditelBlockPower(ProizvoditelBlockPower nameProizvoditelBlockPower)
       {
           AddObjectDb add = new AddObjectDb();
           var model = add.AddAndEditNameProizvoditelBlockPower(nameProizvoditelBlockPower);
           add.Dispose();
           if (model.Model != null) { SignalRLibary.SignalRinventory.SignalRinventory.SubscribeProizvoditelBlockPower(model.Model); }
           return model;
       }

        public ModelReturn<Supply> AddAndEditNameSupply(Supply nameSupply)
        {
            AddObjectDb add = new AddObjectDb();
            if (nameSupply.DatePostavki != null)
            {
                nameSupply.DatePostavki = nameSupply.DatePostavki.Value.AddHours(4);
            }
            var model = add.AddAndEditNameSupply(nameSupply);
            add.Dispose();
            if (model.Model != null) { SignalRLibary.SignalRinventory.SignalRinventory.SubscribeSupply(model.Model); }
            return model;
        }

        public ModelReturn<Statusing> AddAndEditNameStatus(Statusing nameStatus)
       {
           AddObjectDb add = new AddObjectDb();
           var model = add.AddAndEditNameStatus(nameStatus);
           add.Dispose();
           if (model.Model != null) { SignalRLibary.SignalRinventory.SignalRinventory.SubscribeStatusing(model.Model); }
           return model;
       }

       public ModelReturn<Kabinet> AddAndEditNameKabinet(Kabinet nameKabinet)
       {
           AddObjectDb add = new AddObjectDb();
           var model = add.AddAndEditNameKabinetr(nameKabinet);
           add.Dispose();
           if (model.Model != null) { SignalRLibary.SignalRinventory.SignalRinventory.SubscribeKabinet(model.Model); }
           return model;
       }

       public ModelReturn<FullModel> AddAndEditNameFullModel(FullModel nameFullModel)
       {
           AddObjectDb add = new AddObjectDb();
           var model = add.AddAndEditNameFullModel(nameFullModel);
           add.Dispose();
           if (model.Model != null) { SignalRLibary.SignalRinventory.SignalRinventory.SubscribeFullModel(model.Model); }
           return model;
       }

       public ModelReturn<Classification> AddAndEditNameClassification(Classification nameClassification)
       {
           AddObjectDb add = new AddObjectDb();
           var model = add.AddAndEditNameClassification(nameClassification);
           add.Dispose();
           if (model.Model != null) { SignalRLibary.SignalRinventory.SignalRinventory.SubscribeClassification(model.Model); }
           return model;
       }
        /// <summary>
        /// Добавление объекта в Разное
        /// </summary>
        /// <param name="otherAll">Разное</param>
        /// <param name="userIdEdit">Ун пользователя</param>
        /// <returns></returns>
       public ModelReturn<OtherAll> AddAndEditOtherAll(OtherAll otherAll, string userIdEdit)
       {
           AddObjectDb add = new AddObjectDb();
           var model = add.AddAndEditOtherAll(otherAll, SignalRLibary.SignalRinventory.SignalRinventory.GetUser(userIdEdit));
           add.Dispose();
           if (model.Model != null) { SignalRLibary.SignalRinventory.SignalRinventory.SubscribeOtherAll(model.Model); }
           return model;
       }
        /// <summary>
        /// Добавление модели разного
        /// </summary>
        /// <param name="modelOther">Наименование модели разного</param>
        /// <returns></returns>
        public ModelReturn<ModelOther> AddAndEditModelOther(ModelOther modelOther)
        {
            AddObjectDb add = new AddObjectDb();
            var model = add.AddAndEditModelOther(modelOther);
            add.Dispose();
            if (model.Model != null) { SignalRLibary.SignalRinventory.SignalRinventory.SubscribeModelOther(model.Model); }
            return model;
        }
        /// <summary>
        /// Добавление типа разного
        /// </summary>
        /// <param name="typeOther">Наименование типа разного</param>
        /// <returns></returns>
        public ModelReturn<TypeOther> AddAndEditTypeOther(TypeOther typeOther)
        {
            AddObjectDb add = new AddObjectDb();
            var model = add.AddAndEditTypeOther(typeOther);
            add.Dispose();
            if (model.Model != null) { SignalRLibary.SignalRinventory.SignalRinventory.SubscribeTypeOther(model.Model); }
            return model;
        }
        /// <summary>
        /// Добавление производителя в разное
        /// </summary>
        /// <param name="proizvoditelOther">Наименование производителя разного</param>
        /// <returns></returns>
        public ModelReturn<ProizvoditelOther> AddAndEditProizvoditelOther(ProizvoditelOther proizvoditelOther)
        {
            AddObjectDb add = new AddObjectDb();
            var model = add.AddAndEditProizvoditelOther(proizvoditelOther);
            add.Dispose();
            if (model.Model != null) { SignalRLibary.SignalRinventory.SignalRinventory.SubscribeProizvoditelOther(model.Model); }
            return model;
        }
        /// <summary>
        /// Удаление не актуального разного оборудования
        /// </summary>
        /// <param name="otherAll">Серверное оборудование</param>
        /// <param name="userIdEdit">Ун пользователя</param>
        public ModelReturn<OtherAll> DeleteOtherAll(OtherAll otherAll, string userIdEdit)
        {
            DeleteObjectDb delete = new DeleteObjectDb();
            var model = delete.DeleteOtherAll(otherAll, SignalRLibary.SignalRinventory.SignalRinventory.GetUser(userIdEdit));
            SignalRLibary.SignalRinventory.SignalRinventory.SubscribeDeleteOtherAll(model);
            return model;
        }

        public ModelReturn<FullProizvoditel> AddAndEditNameFullProizvoditel(FullProizvoditel nameFullProizvoditel)
       {
           AddObjectDb add = new AddObjectDb();
           var model = add.AddAndEditNameFullProizvoditel(nameFullProizvoditel);
           add.Dispose();
           if (model.Model != null) { SignalRLibary.SignalRinventory.SignalRinventory.SubscribeFullProizvoditel(model.Model); }
           return model;
       }

       public ModelReturn<CopySave> AddAndEditNameCopySave(CopySave nameCopySave)
       {
           AddObjectDb add = new AddObjectDb();
           var model = add.AddAndEditNameCopySave(nameCopySave);
           add.Dispose();
           if (model.Model != null) { SignalRLibary.SignalRinventory.SignalRinventory.SubscribeCopySave(model.Model); }
           return model;
       }

       public async Task<string> AllClasification()
       {
           Select auto = new Select();
           return await Task.Factory.StartNew(() => auto.AllClasification());
       }

       public ModelReturn<EfDatabase.Inventory.Base.ModelSwithe> AddAndEditModelSwith(EfDatabase.Inventory.Base.ModelSwithe modelswith)
       {
           AddObjectDb add = new AddObjectDb();
           var model = add.AddAndEditModelSwithe(modelswith);
           add.Dispose();
           if (model.Model != null) { SignalRLibary.SignalRinventory.SignalRinventory.SubscribeModelSwithe(model.Model); }
           return model;
       }

       /// <summary>
       /// Удаление пользователя
       /// </summary>
       /// <param name="user">Пользователь</param>
       /// <param name="userIdEdit">Ун пользователя</param>
       /// <returns></returns>
       public ModelReturn<User> DeleteUser(User user, string userIdEdit)
       {
           DeleteObjectDb delete = new DeleteObjectDb();
           var model = delete.DeleteUser(user, SignalRLibary.SignalRinventory.SignalRinventory.GetUser(userIdEdit));
           SignalRLibary.SignalRinventory.SignalRinventory.SubscribeDeleteUser(model);
           return model;
       }
       /// <summary>
       /// Удаление системных блоков
       /// </summary>
       /// <param name="sysBlock">Системный блок</param>
       /// <param name="userIdEdit">Ун пользователя</param>
       /// <returns></returns>
       public ModelReturn<SysBlock> DeleteSysBlock(SysBlock sysBlock, string userIdEdit)
       {
           DeleteObjectDb delete = new DeleteObjectDb();
           var model = delete.DeleteSystemUnit(sysBlock, SignalRLibary.SignalRinventory.SignalRinventory.GetUser(userIdEdit));
           SignalRLibary.SignalRinventory.SignalRinventory.SubscribeDeleteSystemUnit(model);
           return model;
       }
        /// <summary>
        /// Удаление не актуального серверного оборудования
        /// </summary>
        /// <param name="serverEquipment">Серверное оборудование</param>
        /// <param name="userIdEdit">Ун пользователя</param>
        public ModelReturn<ServerEquipment> DeleteServerEquipment(ServerEquipment serverEquipment, string userIdEdit)
        {
            DeleteObjectDb delete = new DeleteObjectDb();
            var model = delete.DeleteServerEquipment(serverEquipment, SignalRLibary.SignalRinventory.SignalRinventory.GetUser(userIdEdit));
            SignalRLibary.SignalRinventory.SignalRinventory.SubscribeDeleteServerEquipment(model);
            return model;
        }

       /// <summary>
       /// Удаление Мониторов
       /// </summary>
       /// <param name="monitor">Монитор</param>
       /// <param name="userIdEdit">Ун пользователя</param>
       /// <returns></returns>
       public ModelReturn<Monitor> DeleteMonitor(Monitor monitor, string userIdEdit)
       {
           DeleteObjectDb delete = new DeleteObjectDb();
           var model = delete.DeleteMonitor(monitor, SignalRLibary.SignalRinventory.SignalRinventory.GetUser(userIdEdit));
           SignalRLibary.SignalRinventory.SignalRinventory.SubscribeDeleteMonitor(model);
           return model;
       }

       /// <summary>
       /// Удаление принтеров
       /// </summary>
       /// <param name="printer">Принтер</param>
       /// <param name="userIdEdit">Ун пользователя</param>
       /// <returns></returns>
       public ModelReturn<Printer> DeletePrinter(Printer printer, string userIdEdit)
       {
           DeleteObjectDb delete = new DeleteObjectDb();
           var model = delete.DeletePrinter(printer, SignalRLibary.SignalRinventory.SignalRinventory.GetUser(userIdEdit));
           SignalRLibary.SignalRinventory.SignalRinventory.SubscribeDeletePrinter(model);
           return model;
       }
       /// <summary>
       /// Удаление сканеров и камер
       /// </summary>
       /// <param name="scanner">Сканер или камера</param>
       /// <param name="userIdEdit">Пользователь</param>
       /// <returns></returns>
       public ModelReturn<ScanerAndCamer> DeleteScannerAndCamera(ScanerAndCamer scanner, string userIdEdit)
       {
           DeleteObjectDb delete = new DeleteObjectDb();
           var model = delete.DeleteScannerAndCamera(scanner, SignalRLibary.SignalRinventory.SignalRinventory.GetUser(userIdEdit));
           SignalRLibary.SignalRinventory.SignalRinventory.SubscribeDeleteScannerAndCamera(model);
           return model;
       }
       /// <summary>
       /// Удаление МФУ
       /// </summary>
       /// <param name="mfu">МФУ</param>
       /// <param name="userIdEdit">Ун пользователя</param>
       /// <returns></returns>
       public ModelReturn<Mfu> DeleteMfu(Mfu mfu, string userIdEdit)
       {
           DeleteObjectDb delete = new DeleteObjectDb();
           var model = delete.DeleteMfu(mfu, SignalRLibary.SignalRinventory.SignalRinventory.GetUser(userIdEdit));
           SignalRLibary.SignalRinventory.SignalRinventory.SubscribeDeleteMfu(model);
           return model;
       }
       /// <summary>
       /// Удаление ИБП
       /// </summary>
       /// <param name="blockPower">ИБП</param>
       /// <param name="userIdEdit">Ун пользователя</param>
       /// <returns></returns>
       public ModelReturn<BlockPower> DeleteBlockPower(BlockPower blockPower, string userIdEdit)
       {
           DeleteObjectDb delete = new DeleteObjectDb();
           var model = delete.DeleteBlockPower(blockPower, SignalRLibary.SignalRinventory.SignalRinventory.GetUser(userIdEdit));
           SignalRLibary.SignalRinventory.SignalRinventory.SubscribeDeleteBlockPower(model);
           return model;
       }
       /// <summary>
       /// Удаление коммутаторов
       /// </summary>
       /// <param name="switches">Коммутатор</param>
       /// <param name="userIdEdit">Ун пользователя</param>
       /// <returns></returns>
       public ModelReturn<Swithe> DeleteSwitch(Swithe switches, string userIdEdit)
       {
           DeleteObjectDb delete = new DeleteObjectDb();
           var model = delete.DeleteSwitch(switches, SignalRLibary.SignalRinventory.SignalRinventory.GetUser(userIdEdit));
           SignalRLibary.SignalRinventory.SignalRinventory.SubscribeDeleteSwitch(model);
           return model;
       }
       /// <summary>
       /// Удаление телефонов
       /// </summary>
       /// <param name="telephone">Телефон</param>
       /// <param name="userIdEdit">Ун пользователя</param>
       /// <returns></returns>
       public ModelReturn<Telephon> DeleteTelephone(Telephon telephone, string userIdEdit)
       {
           DeleteObjectDb delete = new DeleteObjectDb();
           var model = delete.DeleteTelephone(telephone, SignalRLibary.SignalRinventory.SignalRinventory.GetUser(userIdEdit));
           SignalRLibary.SignalRinventory.SignalRinventory.SubscribeDeleteTelephone(model);
           return model;
       }
       /// <summary>
       /// Просмотр Body
       /// </summary>
       /// <param name="model">Модель почты</param>
       /// <returns></returns>
       public async Task<string> VisibilityBodyMail(WebMailModel model)
       {
           MailLogicLotus mail = new MailLogicLotus();
           return await Task.Factory.StartNew(() => mail.ReturnMailBody(model));
       }
       /// <summary>
       /// Выгрузка файла вложения
       /// </summary>
       /// <param name="model">Модель почты</param>
       /// <returns></returns>
       public async Task<Stream> OutputMail(WebMailModel model)
       {
           MailLogicLotus mail = new MailLogicLotus();
           return await Task.Factory.StartNew(() => mail.OutputMail(model));
       }
       /// <summary>
       /// Удаление письма
       /// </summary>
       /// <param name="model">Модель почты</param>
       /// <returns></returns>
       public async Task<string> DeleteMail(WebMailModel model)
       {
           MailLogicLotus mail = new MailLogicLotus();
           return await Task.Factory.StartNew(() => mail.DeleteMail(model));
       }

       /// <summary>
       /// Все идентификаторы пользователей
       /// </summary>
       /// <returns></returns>
       public async Task<string> AllMailIdentifies()
       {
           Select auto = new Select();
           return await Task.Factory.StartNew(() => auto.AllMailIdentifier());
       }

       /// <summary>
       /// Все идентификаторы пользователей
       /// </summary>
       /// <returns></returns>
       public async Task<string> AllMailGroups()
       {
           Select auto = new Select();
           return await Task.Factory.StartNew(() => auto.AllMailGroup());
       }

       /// <summary>
       /// Обновление идентификаторов пользователя
       /// </summary>
       /// <param name="nameMailIdentifier"></param>
       /// <returns></returns>
       public  ModelReturn<MailIdentifier> AddAndEditMailIdentifies(MailIdentifier nameMailIdentifier)
       {
           AddObjectDb add = new AddObjectDb();
           var model = add.AddAndEditMailIdentifies(nameMailIdentifier);
           if (model.Model != null) { SignalRLibary.SignalRinventory.SignalRinventory.SubscribeModelMailIdentifier(model.Model); }
           return model;
       }
       /// <summary>
       /// Обновление групп для абонентов
       /// </summary>
       /// <param name="nameMailGroups">Группа для абонентов</param>
       /// <returns></returns>
       public ModelReturn<MailGroup> AddAndEditMailGroups(MailGroup nameMailGroups)
       {
           AddObjectDb add = new AddObjectDb();
           var model = add.AddAndEditMailGroup(nameMailGroups);
           if (model.Model != null)
           {
               SignalRLibary.SignalRinventory.SignalRinventory.SubscribeModelMailGroups(model.Model); }
           return model;
       }
       /// <summary>
       /// Шаблоны для СТО по категориям
       /// </summary>
       /// <returns></returns>
       public async Task<string> AllTemplateSupport()
       {
           Select auto = new Select();
           return await Task.Factory.StartNew(() => auto.AllTemplate());
       }


       /// <summary>
       /// Данный api создан для генерации и отправки запроса в СТО по ун шаблону
       /// </summary>
       /// <param name="modelSupport">Модель генерации для отправки на СТО</param>
       /// <returns></returns>
       public async Task<ModelParametrSupport> ServiceSupport(ModelParametrSupport modelSupport)
       {
           return await Task.Factory.StartNew(() =>
           {
               if (modelSupport.IdCalendarVks != 0)
               {
                   var stpCalender = new MailLogicLotus();
                   stpCalender.ModifiedCalender(modelSupport.IdCalendarVks);
                   stpCalender.Dispose();
               }
               var support = new CreateTiсketSupport(modelSupport.Login, modelSupport.Password);
               var generate = new GenerateParameterSupport(parametersService.PathDomainGroup);
               var selectReportPassportTechnique = new SelectReportPassportTechnique(parametersService.Inventarization);
               try
               {
                   
                   generate.GenerateTemplateUrlParameter(ref modelSupport);
                   generate.IsCheckAllParameter(modelSupport.TemplateSupport.Where(param => param.NameStepSupport == "Step2").ToArray());
                   var modelParameterInputStep3 = modelSupport.TemplateSupport.Where(temple => temple.NameStepSupport == "Step3" && temple.TemplateParametrType != null).ToArray();
                   if (modelParameterInputStep3.Length > 0)
                   {
                       selectReportPassportTechnique.CreateStoParametersStep3(ref modelSupport, parametersService.Report);
                   }
                   modelSupport.Step3ResponseSupport = support.CreateFullSupportTax(modelSupport);
                   Loggers.Log4NetLogger.Info(new Exception($"Пользователем {modelSupport.Login} создана заявка по теме {modelSupport.TemplateSupport.FirstOrDefault(description => description.NameGuidParametr == "UF_SERVICE_EXTID")?.HelpParameter}"));
                   return modelSupport;
               }
               catch (Exception ex)
               {
                   Loggers.Log4NetLogger.Error(ex);
                   modelSupport.Error = ex.Message;
                   Loggers.Log4NetLogger.Info(new Exception($"Пользователем {modelSupport.Login} была вызвана ошибка по теме {modelSupport.TemplateSupport.FirstOrDefault(description => description.NameGuidParametr == "UF_SERVICE_EXTID")?.HelpParameter}"));
                   return modelSupport;
               }
               finally
               {
                   generate.Dispose();
                   support.Steps(support.Logon, "GET");
                   support.Dispose();
                   selectReportPassportTechnique.Dispose();
               }
           });
       }
        /// <summary>
        /// Снятие статуса со списанной техники
        /// </summary>
        /// <param name="allTechnical">списанная техника</param>
        /// <returns></returns>
        public async Task<string> IsCheckStatusNull(AllTechnic allTechnical)
       {
           SelectSql selectSql = new SelectSql();
           return await Task.Factory.StartNew(() => selectSql.CheckStatus(allTechnical));
       }
        /// <summary>
        /// Генерация QR Code для инвентаризации
        /// </summary>
        /// <returns></returns>
        /// <param name="serialNumber">Серийный номер техники</param>
        /// <param name="isAll">Генерация на все кабинеты или на один</param>
        public async Task<Stream> GenerateQrCodeTechnical(string serialNumber, bool isAll = false)
        {
           try
           {
               return await Task.Factory.StartNew(() =>
               {
                   var auto = new Select();
                   var i = 0;
                   var sticker = new StickerCode();
                   var technical = auto.SelectTechnical(serialNumber, isAll);
                   if (technical.Count == 0) return null;
                   var qrCode = new GenerateBarcode();
                   technical.ForEach(x => {
                       var templateContent = $"{ x.Item}: { x.NameManufacturer} { x.NameModel}\r\n" +
                                             $"s/n: {x.SerNum}\r\n" +
                                             $"Инв.: {x.InventarNum}\r\n" +
                                             $"Сервис.: {x.ServiceNum}\r\n" +
                                             $"Kaб.: {x.NumberKabinet}\r\n" +
                                             $"User: {x.Name}";
                       x.Coment = qrCode.GenerateQrCode(parametersService.Report + i, templateContent);
                       i++;
                   });
                   sticker.CreateDocument(parametersService.Report + "QrCodeOffice", technical);
                   technical.Select(x => x.Coment).ToList().ForEach(File.Delete);
                   return sticker.FileArray();
               });
           }
           catch (Exception e)
           {
               Loggers.Log4NetLogger.Error(e);
           }
           return null;
        }
        /// <summary>
        /// Генерация QR Кодов на кабинет
        /// </summary>
        /// <param name="numberOffice">Номер кабинета</param>
        /// <param name="isAll">Генерация на все кабинеты или на один</param>
        /// <returns></returns>
       public async Task<Stream> GenerateQrCodeOffice(string numberOffice, bool isAll = false)
       {
           try
           {
               return await Task.Factory.StartNew(() =>
               {
                   var auto = new Select();
                   QrCodeOffice office = auto.SelectOffice(numberOffice,isAll);
                   var qrCode = new GenerateBarcode();
                   var stickerQrOffice = new OfficeStikerCode();
                   //Создание qr кодов
                   if (office.Kabinet == null) return null;
                   office.Kabinet.AsEnumerable().Select(x => x).ToList().ForEach(x => x.FullPathPng = qrCode.GenerateQrCode(parametersService.Report + x.IdNumberKabinet, x.NumberKabinet));
                   stickerQrOffice.CreateDocument(parametersService.Report + "QrCodeOffice", office);
                   office.Kabinet.AsEnumerable().Select(x => x.FullPathPng).ToList().ForEach(File.Delete);
                   return stickerQrOffice.FileArray();
               });
           }
           catch (Exception e)
           {
               Loggers.Log4NetLogger.Error(e);
           }
           return null;
        }
        /// <summary>
        /// Вся техника для личного кабинета
        /// </summary>
        /// <param name="idUser">Ун пользователя</param>
        /// <returns></returns>
        public async Task<string> AllTechnicsLk(int idUser)
        {
            Select auto = new Select();
            return await Task.Factory.StartNew(() => auto.AllTechicsLkInventory(idUser));
        }
        /// <summary>
        /// Все пользователи отдела для генерации служебных записок
        /// </summary>
        /// <param name="idUser">Ун пользователя</param>
        /// <returns></returns>
        public async Task<string> AllUsersDepartmentLk(int idUser)
        {
            Select auto = new Select();
            return await Task.Factory.StartNew(() => auto.AllUsersDepartmentLk(idUser));
        }
        /// <summary>
        /// Выгрузка токенов ключей
        /// </summary>
        /// <returns></returns>
        public async Task<string> AllToken()
        {
            Select auto = new Select();
            return await Task.Factory.StartNew(() => auto.AllToken());
        }

        /// <summary>
        /// Добавление или обновление Токена 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="userIdEdit">Пользователь кто редактировал</param>
        /// <returns></returns>
        public ModelReturn<EfDatabase.Inventory.Base.Token> AddAndEditToken(EfDatabase.Inventory.Base.Token token, string userIdEdit)
        {
            AddObjectDb add = new AddObjectDb();
            var model = add.AddAndEditToken(token, SignalRLibary.SignalRinventory.SignalRinventory.GetUser(userIdEdit));
            add.Dispose();
            if (model.Model != null) { SignalRLibary.SignalRinventory.SignalRinventory.SubscribeToken(model.Model); }
            return model;
        }
        /// <summary>
        /// Удаление не актуального Токена ключа
        /// </summary>
        /// <param name="token">Токен ключ</param>
        /// <param name="userIdEdit">Ун пользователя</param>
        public ModelReturn<EfDatabase.Inventory.Base.Token> DeleteToken(EfDatabase.Inventory.Base.Token token, string userIdEdit)
        {
            DeleteObjectDb delete = new DeleteObjectDb();
            var model = delete.DeleteToken(token, SignalRLibary.SignalRinventory.SignalRinventory.GetUser(userIdEdit));
            SignalRLibary.SignalRinventory.SignalRinventory.SubscribeDeleteToken(model);
            return model;
        }
        /// <summary>
        /// Проверка по УН процесса запущен ли он или нет
        /// </summary>
        /// <param name="idTask">Ун процесса</param>
        /// <returns></returns>
        public async Task<bool> IsBeginTask(int idTask)
        {
            Select auto = new Select();
            return await Task.Factory.StartNew(() => auto.IsBeginTask(idTask));
        }
        /// <summary>
        /// Создание Акта списания техники
        /// </summary>
        /// <param name="modelParameterAct">Параметры акта списания техники</param>
        /// <returns></returns>
        public async Task<Stream> CreateAct(ModelSelect modelParameterAct)
        {
            try
            {
               return await Task.Factory.StartNew(() =>
              {
                    var generate = new GenerateParameterSupport(parametersService.PathDomainGroup);
                    var act = generate.GenerateParameterAct(modelParameterAct);
                    var templateAct = new TemplateAct();
                    templateAct.CreateDocument(parametersService.Report + "Акт списания ", act);
                    return templateAct.FileArray();
                });
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
            return null;
        }
        /// <summary>
        /// Создание журнала доступа АИС 3
        /// </summary>
        /// <param name="year">Год журнала</param>
        /// <param name="idOtdel">УН отдела</param>
        /// <param name="isAllJournal">Логика полный или нет по умолчанию не полный</param>
        /// <returns></returns>
        public async Task<Stream> CreateJournalAis3(int year, int idOtdel, bool isAllJournal)
        {
            try
            {
                return await Task.Factory.StartNew(() =>
                {
                    SelectSql select = new SelectSql();
                    EfDatabase.Journal.AllJournal journal = select.SelectJournalAis3(year, idOtdel, isAllJournal);
                    var templateJournal = new TemplateJournalAis3();
                    templateJournal.CreateDocument(parametersService.Report, journal, null);
                    return templateJournal.FileArray();
                });
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
            return null;
        }
        /// <summary>
        /// Создание табеля
        /// </summary>
        /// <param name="model">Модель табеля</param>
        /// <returns></returns>
        public async Task<Stream> CreateReportCard(ReportCardModel model)
        {
            try
            {
                return await Task.Factory.StartNew(() =>
                {
                    SqlConnectionType sql = new SqlConnectionType();
                    SelectImns selectFrames = new SelectImns();
                    XmlReadOrWrite xml = new XmlReadOrWrite();
                    SelectSql select = new SelectSql();
                    select.SelectCardModelLeader(ref model);
                    var dateParameter = $"{model.SettingParameters.Year}-{model.SettingParameters.Mouth.NumberMouthString}-01";
                    var command = string.Format(selectFrames.UserReportCard, dateParameter, dateParameter, model.SettingParameters.LeaderD.NameDepartment, dateParameter);
                    var userReportCard = sql.XmlString(parametersService.ConnectImns51, command);
                    userReportCard = string.Concat("<SettingParameters>", userReportCard, "</SettingParameters>");
                    model.SettingParameters.UsersReportCard = ((SettingParameters)xml.ReadXmlText(userReportCard, typeof(SettingParameters))).UsersReportCard;
                    foreach (var usersReportCard in model.SettingParameters.UsersReportCard)
                    {
                        var commandVacation = string.Format(selectFrames.ItemVacationNew, usersReportCard.Tab_num, $"{model.SettingParameters.Year}", $"{model.SettingParameters.Year}");
                        var userVacation = sql.XmlString(parametersService.ConnectImns51, commandVacation);
                        userVacation = string.Concat("<UsersReportCard>", userVacation, "</UsersReportCard>");
                        usersReportCard.ItemVacation = ((UsersReportCard)xml.ReadXmlText(userVacation, typeof(UsersReportCard))).ItemVacation;
                        var commandDisability = string.Format(selectFrames.Disability, usersReportCard.Tab_num, $"{model.SettingParameters.Year}");
                        var userDisability = sql.XmlString(parametersService.ConnectImns51, commandDisability);
                        userDisability = string.Concat("<UsersReportCard>", userDisability, "</UsersReportCard>");
                        usersReportCard.Disability = ((UsersReportCard)xml.ReadXmlText(userDisability, typeof(UsersReportCard))).Disability;
                        var commandBusiness = string.Format(selectFrames.Business, usersReportCard.Tab_num, $"{model.SettingParameters.Year}");
                        var userBusiness = sql.XmlString(parametersService.ConnectImns51, commandBusiness);
                        userBusiness = string.Concat("<UsersReportCard>", userBusiness, "</UsersReportCard>");
                        usersReportCard.Business = ((UsersReportCard)xml.ReadXmlText(userBusiness, typeof(UsersReportCard))).Business;
                    }
                    ReportCard report = new ReportCard();
                    report.CreateDocument(parametersService.Report, model);
                    return report.FileArray();
                });
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
            return null;
        }
        /// <summary>
        /// Создание служебных записок для ЛК 
        /// </summary>
        /// <param name="memoReport">Запрос служебной записки</param>
        /// <returns></returns>
        public async Task<Stream> CreateMemoReport(ModelMemoReport memoReport)
        {
            try
            {
                return await Task.Factory.StartNew(() =>
                {
                    SelectSql select = new SelectSql();
                    SelectImns selectFrames = new SelectImns();
                    SqlConnectionType sql = new SqlConnectionType();
                    XmlReadOrWrite xml = new XmlReadOrWrite();
                    MemoReport memo = new MemoReport();
                    select.SelectMemoReport(ref memoReport);
                    var commandOrders = string.Format(selectFrames.LastOrder, memoReport.UserDepartment.SmallTabelNumber);
                    var userOrder = sql.XmlString(parametersService.ConnectImns51, commandOrders);
                    if (userOrder != null)
                    {
                        userOrder = string.Concat("<Orders>", userOrder, "</Orders>");
                        memoReport.UserDepartment.Orders = ((Orders)xml.ReadXmlText(userOrder, typeof(Orders)));
                    }
                    memo.CreateDocument(parametersService.Report, memoReport);
                    return memo.FileArray();
                });
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
                Loggers.Log4NetLogger.Error(new Exception($"Возникла ошибка при создании пользователя {memoReport.SelectParameterDocument.IdUser}, документа {memoReport.SelectParameterDocument.NameDocument}, исполнителя {memoReport.SelectParameterDocument.TabelNumber}"));
            }
            return null;
        }
        /// <summary>
        /// Актуализация данных с СТО
        /// </summary>
        /// <returns></returns>
        public void UpdateDataSto(int idProcess)
        {
            try
            {
                Select auto = new Select();
                var process = auto.SelectProcess(idProcess);
                if (process.IsComplete != null && (bool)process.IsComplete)
                {
                    var addObjectDb = new AddObjectDb();
                    addObjectDb.IsProcessComplete(idProcess, false);
                    var task = Task.Run(() =>
                    {
                        var sto = new PassportStoPostGet(parametersService.LoginSto, parametersService.PasswordSto, parametersService.Report);
                        var fullPathXlsx = sto.DownloadReportSto();
                        addObjectDb.CreateAndDownloadSto(fullPathXlsx, parametersService.Report, parametersService.XsdReport, parametersService.BulkCopyXmlSto);
                    });
                    task.ConfigureAwait(true).GetAwaiter().OnCompleted(()=>
                    {
                        addObjectDb.IsProcessComplete(idProcess, true);
                        addObjectDb.Dispose();
                        SignalRLibary.SignalRinventory.SignalRinventory.SubscribeStatusProcess(new ModelReturn<string>($"{process.NameProcess} завершен!",null,3));
                    });
                    SignalRLibary.SignalRinventory.SignalRinventory.SubscribeStatusProcess(new ModelReturn<string>($"{process.NameProcess} запущен!",null,1) );
                }
                else
                {
                    SignalRLibary.SignalRinventory.SignalRinventory.SubscribeStatusProcess(new ModelReturn<string>($"{process.NameProcess} уже запущен ожидайте окончание процесса!",null,2)  );
                }
                auto.Dispose();
            }
            catch (Exception e)
            {
                SignalRLibary.SignalRinventory.SignalRinventory.SubscribeStatusProcess(new ModelReturn<string>(e.Message));
                Loggers.Log4NetLogger.Error(e);
            }
        }
        /// <summary>
        /// Получение всех отчетов сравнения ЭПО
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetModelReportAnalysisEpo()
        {
            Select auto = new Select();
            return await Task.Factory.StartNew(() => auto.GetModelReportAnalysisEpo());
        }

        /// <summary>
        /// Полный анализ ЭПО с Инвентаризацией
        /// </summary>
        /// <param name="idReport">Уникальные номера отчетов ЭПО</param>
        /// <returns></returns>
        public async Task<Stream> AllReportInventoryAndEpo(int[] idReport)
        {
            var selectReportPassportTechnique = new SelectReportPassportTechnique(parametersService.Inventarization);
            return await Task.Factory.StartNew(() => selectReportPassportTechnique.CreateFullReportEpo(parametersService.Report, idReport));
        }
    }
}