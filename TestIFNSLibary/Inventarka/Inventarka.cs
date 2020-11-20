using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EfDatabase.Inventory.Base;
using EfDatabase.Inventory.BaseLogic.AddObjectDb;
using EfDatabase.Inventory.BaseLogic.DeleteObjectDb;
using EfDatabase.Inventory.BaseLogic.Login;
using EfDatabase.Inventory.BaseLogic.Select;
using EfDatabase.Inventory.MailLogicLotus;
using EfDatabase.Inventory.ReportXml.ReturnModelError;
using EfDatabase.XsdInventoryRuleAndUsers;
using EfDatabaseParametrsModel;
using EfDatabaseXsdBookAccounting;
using EfDatabaseXsdInventoryAutorization;
using EfDatabaseXsdMail;
using EfDatabaseXsdQrCodeModel;
using EfDatabaseXsdSupportNalog;
using LibaryDocumentGenerator.Barcode;
using LibaryDocumentGenerator.Documents.Template;
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

namespace TestIFNSLibary.Inventarka
{
   public class Inventarka  : IInventarka
   {
       private readonly Parameter _parametrService = new Parameter();

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
        public ModelReturn<EfDatabase.Inventory.Base.Printer> AddAndEditPrinter(EfDatabase.Inventory.Base.Printer printer, string userIdEdit)
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
        public ModelReturn<EfDatabase.Inventory.Base.Swithe> AddAndEditSwith(EfDatabase.Inventory.Base.Swithe swith, string userIdEdit)
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
        public ModelReturn<EfDatabase.Inventory.Base.ScanerAndCamer> AddAndEditScaner(EfDatabase.Inventory.Base.ScanerAndCamer scaner, string userIdEdit)
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
        /// Запрос всех пользователей
        /// </summary>
        /// <returns></returns>
        public async Task<string> AllUsers()
        {
            Select auto = new Select();
            return await Task.Factory.StartNew(() => auto.UsersAll());
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
                var selectfull = new SelectFull();
                TelephoneHelp invoice = new TelephoneHelp();
                return await Task.Factory.StartNew(() =>
                { 
                    telephonehelper.LogicaSelect = select.SqlSelectModel(telephonehelper.ParametrsSelect.Id);
                    invoice.CreateDocum(_parametrService.Report, (EfDatabaseTelephoneHelp.TelephoneHelp)selectfull.GenerateSchemeXsdSql<string,string>(_parametrService.Inventarization, telephonehelper.LogicaSelect), null);
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
        public async Task<Stream> GenerateFileXlsxSqlView(int idView)
        {
            SelectSql select = new SelectSql();
            var selectFull = new SelectFull();
            return await Task.Factory.StartNew(() => selectFull.GenerateStreamToSqlViewFile(_parametrService.Inventarization, 
                @select.SqlSelectModel(idView),
                _parametrService.Report));
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
                    SelectFull selectfull = new SelectFull();
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
                    Book book =(Book) selectfull.GenerateSchemeXsdSql(_parametrService.Inventarization, modelSelect.LogicaSelect,parametr); //Получение данных для модели
                    rep.BookInvoce(ref book.BareCodeBook, bookModels); //Раскладывает в БД
                    barecode.GenerateBookCode(ref book.BareCodeBook, _parametrService.Report);//Генерит Штрихкод на основе раскладки БД
                    bookAccounting.CreateDocum(_parametrService.Report, book, null);
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
        /// <param name="logica"></param>
        /// <returns></returns>
        public async Task<string> SelectXml(LogicaSelect logica)
        {
            var selectfull = new SelectFull();
            return await Task.Factory.StartNew(() => selectfull.SqlModelInventory(_parametrService.Inventarization,logica));
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
                       barecode.GenerateCode(ref report, _parametrService.Report);
                       invoice.CreateDocum(_parametrService.Report, report, null);
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
                identitybarcode.DecodeBarCodePng(ref uploadFileModel.Upload[i], _parametrService.Report);
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
                        Messages = "Не распознался штрихкод на документе " + uploadFileModel.Upload[i].NameFile
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
                var isActualizationUser = sql.XmlString(_parametrService.ConnectImns51, selectFrames.ActualUsers);
                return await Task.Factory.StartNew(() => select.ActualUserModel(isActualizationUser));
            }
            catch (Exception e)
            {
               Loggers.Log4NetLogger.Error(e);
            }
            return "Возникла не предвиденная ошибка смотри Log.txt";
        }
       /// <summary>
       /// Актулизация Ip Адресов в БД
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
               var generate = new GenerateParameterSupport(_parametrService.PathDomainGroup);
               try
               {
                   
                   generate.GenerateTemplateUrlParameter(ref modelSupport);
                   generate.IsCheckAllParameter(modelSupport.TemplateSupport.Where(param => param.NameStepSupport == "Step2").ToArray());
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
                       x.Coment = qrCode.GenerateQrCode(_parametrService.Report + i, templateContent);
                       i++;
                   });
                   sticker.CreateDocum(_parametrService.Report + "QrCodeOffice", technical);
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
                   office.Kabinet.AsEnumerable().Select(x => x).ToList().ForEach(x => x.FullPathPng = qrCode.GenerateQrCode(_parametrService.Report + x.IdNumberKabinet, x.NumberKabinet));
                   stickerQrOffice.CreateDocum(_parametrService.Report + "QrCodeOffice", office);
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
   }
}