using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using EfDatabase.Inventarization.Base;
using EfDatabase.Inventarization.BaseLogica.AddObjectDb;
using EfDatabase.Inventarization.BaseLogica.Autorization;
using EfDatabase.Inventarization.BaseLogica.Select;
using EfDatabase.Inventarization.ReportSheme.ReturnModelError;
using EfDatabaseParametrsModel;
using EfDatabaseXsdBookAccounting;
using LibaryDocumentGenerator.Barcode;
using LibaryDocumentGenerator.Documents.Template;
using SqlLibaryIfns.Inventarization.ModelParametr;
using SqlLibaryIfns.Inventarization.Select;
using SqlLibaryIfns.SqlSelect.ImnsKadrsSelect;
using SqlLibaryIfns.SqlZapros.SqlConnections;
using SqlLibaryIfns.ZaprosSelectNotParam;
using TestIFNSLibary.PathJurnalAndUse;
using CopySave = EfDatabase.Inventarization.Base.CopySave;
using FullModel = EfDatabase.Inventarization.Base.FullModel;
using FullProizvoditel = EfDatabase.Inventarization.Base.FullProizvoditel;
using Kabinet = EfDatabase.Inventarization.Base.Kabinet;
using LogicaSelect = EfDatabaseParametrsModel.LogicaSelect;
using ModelBlockPower = EfDatabase.Inventarization.Base.ModelBlockPower;
using NameSysBlock = EfDatabase.Inventarization.Base.NameSysBlock;
using Otdel = EfDatabase.Inventarization.Base.Otdel;
using ProizvoditelBlockPower = EfDatabase.Inventarization.Base.ProizvoditelBlockPower;
using Supply = EfDatabase.Inventarization.Base.Supply;
using User = EfDatabase.Inventarization.Base.User;


namespace TestIFNSLibary.Inventarka
{
   public class Inventarka  : IInventarka
   {
       private readonly Parametr _parametrService = new Parametr();
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
            if (model.Model != null) { SignalRLibary.SignalRinventory.SignalRinventory.SubscribeUser(model.Model); }
            return model;
        }
        /// <summary>
        /// Добавление или обновление принтера
        /// </summary>
        /// <param name="printer">Принтер</param>
        /// <param name="userIdEdit">Пользователь кто редактировал</param>
        /// <returns></returns>
        public ModelReturn<EfDatabase.Inventarization.Base.Printer> AddAndEditPrinter(EfDatabase.Inventarization.Base.Printer printer, string userIdEdit)
        {
            AddObjectDb add = new AddObjectDb();
            var model = add.AddAndEditPrinter(printer, SignalRLibary.SignalRinventory.SignalRinventory.GetUser(userIdEdit));
            if (model.Model != null) { SignalRLibary.SignalRinventory.SignalRinventory.SubscribePrinter(model.Model); }
            return model;
        }
        /// <summary>
        /// Добавление или обновление Коммутаторов
        /// </summary>
        /// <param name="swith">Коммутатор</param>
        /// <param name="userIdEdit">Пользователь кто редактировал</param>
        /// <returns></returns>
        public ModelReturn<EfDatabase.Inventarization.Base.Swithe> AddAndEditSwith(EfDatabase.Inventarization.Base.Swithe swith, string userIdEdit)
       {
            AddObjectDb add = new AddObjectDb();
            var model = add.AddAndEditSwiths(swith, SignalRLibary.SignalRinventory.SignalRinventory.GetUser(userIdEdit));
            if (model.Model != null) { SignalRLibary.SignalRinventory.SignalRinventory.SubscribeSwithe(model.Model); }
            return model;
        }

        /// <summary>
        /// Добавление или обновление принтера
        /// </summary>
        /// <param name="scaner">Сканер</param>
        /// <param name="userIdEdit">Пользователь кто редактировал</param>
        /// <returns></returns>
        public ModelReturn<EfDatabase.Inventarization.Base.ScanerAndCamer> AddAndEditScaner(EfDatabase.Inventarization.Base.ScanerAndCamer scaner, string userIdEdit)
        {
            AddObjectDb add = new AddObjectDb();
            var model = add.AddAndEditScaner(scaner, SignalRLibary.SignalRinventory.SignalRinventory.GetUser(userIdEdit));
            if (model.Model != null) { SignalRLibary.SignalRinventory.SignalRinventory.SubscribeScanerAndCamer(model.Model); }
            return model;
        }
        /// <summary>
        /// Добавление или обновление скнера
        /// </summary>
        /// <param name="mfu">МФУ</param>
        /// <param name="userIdEdit">Пользователь кто редактировал</param>
        /// <returns></returns>
        public ModelReturn<EfDatabase.Inventarization.Base.Mfu> AddAndEditMfu(EfDatabase.Inventarization.Base.Mfu mfu, string userIdEdit)
        {
            AddObjectDb add = new AddObjectDb();
            var model = add.AddAndEditMfu(mfu, SignalRLibary.SignalRinventory.SignalRinventory.GetUser(userIdEdit));
            if (model.Model != null) { SignalRLibary.SignalRinventory.SignalRinventory.SubscribeMfu(model.Model); }
            return model;
        }
        /// <summary>
        /// Добавление или обновление Системного блока
        /// </summary>
        /// <param name="sysblock">Системный блок</param>
        /// <param name="userIdEdit">Пользователь кто редактировал</param>
        /// <returns></returns>
        public ModelReturn<EfDatabase.Inventarization.Base.SysBlock> AddAndEditSysBlok(EfDatabase.Inventarization.Base.SysBlock sysblock, string userIdEdit)
        {
            AddObjectDb add = new AddObjectDb();
            var model = add.AddAndEditSysBlok(sysblock, SignalRLibary.SignalRinventory.SignalRinventory.GetUser(userIdEdit));
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
            var model = add.AddAndEditOtdel(otdel);
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
            if (model.Model != null) { SignalRLibary.SignalRinventory.SignalRinventory.SubscribeMonitor(model.Model); }
            return model;
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
                invoice.CreateDocum(_parametrService.Report, (EfDatabaseTelephoneHelp.TelephoneHelp)selectfull.GenerateShemeXsdSql<string,string>(_parametrService.Inventarization, telephonehelper.LogicaSelect), null);
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
                    Book book =(Book) selectfull.GenerateShemeXsdSql(_parametrService.Inventarization, modelSelect.LogicaSelect,parametr); //Получение данных для модели
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
        /// Выгрузка бесперебойников
        /// </summary>
        /// <returns></returns>
        public async Task<string> BlockPower()
        {
            Select auto = new Select();
            return await Task.Factory.StartNew(() => auto.BlockPower());
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
        public ModelReturn<EfDatabase.Inventarization.Base.Telephon> AddAndEditTelephon(EfDatabase.Inventarization.Base.Telephon telephon,string userIdEdit)
        {
            AddObjectDb add = new AddObjectDb();
            var model =  add.AddAndEditTelephone(telephon, SignalRLibary.SignalRinventory.SignalRinventory.GetUser(userIdEdit));
            if(model.Model != null){ SignalRLibary.SignalRinventory.SignalRinventory.SubscribeTelephone(model.Model);}
            return model;
        }
        /// <summary>
        /// Добавление или обновление ИБП
        /// </summary>
        /// <param name="blockpower">ИБП</param>
        /// <param name="userIdEdit">Пользователь кто редактировал</param>
        /// <returns></returns>
        public ModelReturn<EfDatabase.Inventarization.Base.BlockPower> AddAndEditBlockPower(EfDatabase.Inventarization.Base.BlockPower blockpower, string userIdEdit)
        {
            AddObjectDb add = new AddObjectDb();
            var model = add.AddAndEditPowerBlock(blockpower, SignalRLibary.SignalRinventory.SignalRinventory.GetUser(userIdEdit));
            if (model.Model != null) { SignalRLibary.SignalRinventory.SignalRinventory.SubscribeBlockPower(model.Model); }
            return model;
        }

        public ModelReturn<NameSysBlock> AddAndEditNameSysBlock(NameSysBlock nameSysBlock)
        {
            AddObjectDb add = new AddObjectDb();
            var model = add.AddAndEditNameSysBlock(nameSysBlock);
            if (model.Model != null) { SignalRLibary.SignalRinventory.SignalRinventory.SubscribeNameSysBlock(model.Model); }
            return model;
        }

        public ModelReturn<EfDatabase.Inventarization.Base.NameMonitor> AddAndEditNameMonitor(EfDatabase.Inventarization.Base.NameMonitor nameMonitor)
        {
            AddObjectDb add = new AddObjectDb();
            var model = add.AddAndEditNameMonitor(nameMonitor);
            if (model.Model != null) { SignalRLibary.SignalRinventory.SignalRinventory.SubscribeNameMonitor(model.Model); }
            return model;
        }

        public ModelReturn<ModelBlockPower> AddAndEditNameModelBlokPower(ModelBlockPower nameModelBlokPower)
        {
            AddObjectDb add = new AddObjectDb();
            var model = add.AddAndEditNameModelBlokPower(nameModelBlokPower);
            if (model.Model != null) { SignalRLibary.SignalRinventory.SignalRinventory.SubscribeModelBlockPower(model.Model); }
            return model;
        }

        public ModelReturn<ProizvoditelBlockPower> AddAndEditNameProizvoditelBlockPower(ProizvoditelBlockPower nameProizvoditelBlockPower)
        {
            AddObjectDb add = new AddObjectDb();
            var model = add.AddAndEditNameProizvoditelBlockPower(nameProizvoditelBlockPower);
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
            if (model.Model != null) { SignalRLibary.SignalRinventory.SignalRinventory.SubscribeSupply(model.Model); }
            return model;
        }

        public ModelReturn<Statusing> AddAndEditNameStatus(Statusing nameStatus)
        {
            AddObjectDb add = new AddObjectDb();
            var model = add.AddAndEditNameStatus(nameStatus);
            if (model.Model != null) { SignalRLibary.SignalRinventory.SignalRinventory.SubscribeStatusing(model.Model); }
            return model;
        }

        public ModelReturn<Kabinet> AddAndEditNameKabinet(Kabinet nameKabinet)
        {
            AddObjectDb add = new AddObjectDb();
            var model = add.AddAndEditNameKabinetr(nameKabinet);
            if (model.Model != null) { SignalRLibary.SignalRinventory.SignalRinventory.SubscribeKabinet(model.Model); }
            return model;
        }

        public ModelReturn<FullModel> AddAndEditNameFullModel(FullModel nameFullModel)
        {
            AddObjectDb add = new AddObjectDb();
            var model = add.AddAndEditNameFullModel(nameFullModel);
            if (model.Model != null) { SignalRLibary.SignalRinventory.SignalRinventory.SubscribeFullModel(model.Model); }
            return model;
        }

        public ModelReturn<Classification> AddAndEditNameClassification(Classification nameClassification)
        {
            AddObjectDb add = new AddObjectDb();
            var model = add.AddAndEditNameClassification(nameClassification);
            if (model.Model != null) { SignalRLibary.SignalRinventory.SignalRinventory.SubscribeClassification(model.Model); }
            return model;
        }

        public ModelReturn<FullProizvoditel> AddAndEditNameFullProizvoditel(FullProizvoditel nameFullProizvoditel)
        {
            AddObjectDb add = new AddObjectDb();
            var model = add.AddAndEditNameFullProizvoditel(nameFullProizvoditel);
            if (model.Model != null) { SignalRLibary.SignalRinventory.SignalRinventory.SubscribeFullProizvoditel(model.Model); }
            return model;
        }

        public ModelReturn<CopySave> AddAndEditNameCopySave(CopySave nameCopySave)
        {
            AddObjectDb add = new AddObjectDb();
            var model = add.AddAndEditNameCopySave(nameCopySave);
            if (model.Model != null) { SignalRLibary.SignalRinventory.SignalRinventory.SubscribeCopySave(model.Model); }
            return model;
        }

        public async Task<string> AllClasification()
        {
            Select auto = new Select();
            return await Task.Factory.StartNew(() => auto.AllClasification());
        }

        public ModelReturn<EfDatabase.Inventarization.Base.ModelSwithe> AddAndEditModelSwith(EfDatabase.Inventarization.Base.ModelSwithe modelswith)
        {
            AddObjectDb add = new AddObjectDb();
            var model = add.AddAndEditModelSwithe(modelswith);
            if (model.Model != null) { SignalRLibary.SignalRinventory.SignalRinventory.SubscribeModelSwithe(model.Model); }
            return model;
        }
    }
}
