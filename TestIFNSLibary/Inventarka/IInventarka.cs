using System;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;
using EfDatabase.Inventory.Base;
using EfDatabase.Inventory.BaseLogic.AddObjectDb;
using EfDatabase.Inventory.ReportXml.ReturnModelError;
using EfDatabase.XsdInventoryRuleAndUsers;
using EfDatabaseParametrsModel;
using EfDatabaseXsdBookAccounting;
using EfDatabaseXsdInventoryAutorization;
using EfDatabaseXsdMail;
using EfDatabaseXsdSupportNalog;
using SqlLibaryIfns.Inventory.ModelParametr;
using Printer = EfDatabase.Inventory.Base.Printer;
using ScanerAndCamer = EfDatabase.Inventory.Base.ScanerAndCamer;
using SysBlock = EfDatabase.Inventory.Base.SysBlock;
using Token = EfDatabase.Inventory.Base.Token;
using User = EfDatabase.Inventory.Base.User;

namespace TestIFNSLibary.Inventarka
{
   [ServiceContract]
   public interface IInventarka
   {
       /// <summary>
        /// http://localhost:8182/Inventarka/GenerateSqlSelect
        /// Генерация запросов на клиент
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/GenerateSqlSelect", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<ModelSelect> GenerateSqlSelect(ModelSelect user);

        /// <summary>
        /// http://localhost:8182/Inventarka/GenerateTelephoneHelper
        /// Генерация телефонного справочника инспекции
        /// </summary>
        /// <returns>Телефонный справочник инспекции</returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/GenerateTelephoneHelper", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<Stream> GenerateTelephoneHelper(ModelSelect telephonehelper);
        /// <summary>
        /// Генерация файла из View
        /// </summary>
        /// <param name="idView">Ун выборки View</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, UriTemplate = "/GenerateFileXlsxSqlView?idView={idView}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<Stream> GenerateFileXlsxSqlView(int idView);
        /// <summary>
        /// http://localhost:8182/Inventarka/GenerateBookAccounting
        /// Генерация книги учета материальных ценностей
        /// </summary>
        /// <returns>Книга учета материальных ценностей</returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/GenerateBookAccounting", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<Stream> GenerateBookAccounting(BookModels bookModels);

        /// <summary>
        /// http://localhost:8182/Inventarka/Autification
        /// Авторизация для ресурса Инвентаризация
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/Authorization", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<Autorization> Authorization(Autorization user);

        /// <summary>
        /// Выбор всех пользователей c SQL запроса
        /// http://localhost:8182/Inventarka/SelectDb
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/SelectDb", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
       Task<string> SelectAllUsers(ModelParametr model);
        /// <summary>
        /// Все отделы в БД
        /// http://localhost:8182/Inventarka/AllOtdels
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AllOtdels", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<string> AllOtdels();
        /// <summary>
        /// Все роли в БД
        /// http://localhost:8182/Inventarka/AllRules
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET",RequestFormat = WebMessageFormat.Json,UriTemplate = "/AllRules",ResponseFormat = WebMessageFormat.Json,BodyStyle = WebMessageBodyStyle.Bare)]
        Task<string> AllRules();
        /// <summary>
        /// Роли пользователя
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, UriTemplate = "/RuleAndUsers?idUser={idUser}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<RuleUsers[]> RuleAndUsers(int idUser);

        /// <summary>
        /// Добавление или удаление роли пользователя
        /// </summary>
        /// <param name="ruleUsers">Роль пользователя</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AddAndDeleteRuleUser", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<RuleUsers[]> AddAndDeleteRuleUser(RuleUsers ruleUsers);
        
        /// <summary>
        /// Все пользователи 
        /// http://localhost:8182/Inventarka/AllUsers
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AllUsers",  ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<string> AllUsers();

        /// <summary>
        /// Статистика обраьботки пользователей процедурой
        /// http://localhost:8182/Inventarka/AllActualsProcedureUsers
        /// </summary>
        /// <returns></returns>
       [OperationContract]
       [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AllActualsProcedureUsers", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
       Task<string> AllActualsProcedureUsers();

        /// <summary>
        /// Все пользователи 
        /// http://localhost:8182/Inventarka/AllPosition
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AllPosition", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<string> AllPosition();

        /// <summary>
        /// Все пользователи 
        /// http://localhost:8182/Inventarka/AllClasification
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AllClasification", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<string> AllClasification();


        /// <summary>
        /// Все принтеры
        /// http://localhost:8182/Inventarka/AllPrinters
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AllPrinters", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<string> AllPrinters();

        /// <summary>
        /// Все коммутаторы
        /// http://localhost:8182/Inventarka/AllSwithes
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AllSwithes", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<string> AllSwithes();

        /// <summary>
        /// Все модели коммутаторов
        /// http://localhost:8182/Inventarka/AllModelSwithes
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AllModelSwithes", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<string> AllModelSwithes();

        /// <summary>
        /// Все сканеры
        /// http://localhost:8182/Inventarka/AllScaners
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AllScaners", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<string> AllScaners();

        /// <summary>
        /// Все МФУ
        /// http://localhost:8182/Inventarka/AllMfu
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AllMfu", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<string> AllMfu();

        /// <summary>
        /// Все Системные блоки
        /// http://localhost:8182/Inventarka/AllSysBlok
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AllSysBlok", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<string> AllSysBlok();

        /// <summary>
        /// Все Мониторы
        /// http://localhost:8182/Inventarka/AllMonitors
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AllMonitors", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<string> AllMonitors();

        /// <summary>
        /// Все Производители системных блоков
        /// http://localhost:8182/Inventarka/AllNameSysBlock
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AllNameSysBlock", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<string> NameSysBlock();

        /// <summary>
        /// Все Производители мониторов
        /// http://localhost:8182/Inventarka/AllNameMonitor
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AllNameMonitor", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<string> NameMonitor();

        /// <summary>
        /// Все CopySave
        /// http://localhost:8182/Inventarka/AllCopySave
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AllCopySave", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<string> CopySave();

        /// <summary>
        /// Все производители
        /// http://localhost:8182/Inventarka/AllProizvoditel
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AllProizvoditel", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<string> Proizvoditel();

        /// <summary>
        /// Все производители
        /// http://localhost:8182/Inventarka/AllModel
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AllModel", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<string> Model();
        /// <summary>
        /// Все кабинеты
        /// http://localhost:8182/Inventarka/AllKabinet
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AllKabinet", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<string> Kabinet();
        /// <summary>
        /// Все телефоны
        /// http://localhost:8182/Inventarka/AllTelephon
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AllTelephon", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<string> Telephon();
        /// <summary>
        /// Все бесперебойные блоки
        /// http://localhost:8182/Inventarka/AllBlockPower
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AllBlockPower", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<string> BlockPower();

        /// <summary>
        /// Все серверное оборудование
        /// http://localhost:8182/Inventarka/AllServerEquipment
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AllServerEquipment", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<string> ServerEquipment();
        /// <summary>
        /// Все типы серверного оборудования
        /// http://localhost:8182/Inventarka/AllTypeServer
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AllTypeServer", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<string> TypeServer();
        /// <summary>
        /// Все модели серверного оборудования
        /// http://localhost:8182/Inventarka/AllModelSeverEquipment
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AllModelSeverEquipment", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<string> ModelSeverEquipment();

        /// <summary>
        /// Все производители серверного оборудования
        /// http://localhost:8182/Inventarka/AllManufacturerSeverEquipment
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AllManufacturerSeverEquipment", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<string> ManufacturerSeverEquipment();

        /// <summary>
        /// Все поставщики
        /// http://localhost:8182/Inventarka/AllSupply
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AllSupply", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<string> Supply();
        /// <summary>
        /// Все модели бесперебойных блоков
        /// http://localhost:8182/Inventarka/AllModelBlockPower
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AllModelBlockPower", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<string> ModelBlockPower();
        /// <summary>
        /// Все производители бесперебойных блоков
        /// http://localhost:8182/Inventarka/AllProizvoditelBlockPower
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AllProizvoditelBlockPower", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<string> ProizvoditelBlockPower();
        /// <summary>
        /// Все статусы
        /// http://localhost:8182/Inventarka/AllStatusing
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AllStatusing", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<string> Statusing();
        /// <summary>
        /// Добавление или обновление пользователя
        /// http://localhost:8182/Inventarka/AddAndEditUser
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <param name="userIdEdit">Кто редактировал</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AddAndEditUser?userIdEdit={userIdEdit}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        ModelReturn<User> AddAndEditUser(User user,string userIdEdit);
        /// <summary>
        /// Добавление принтера
        /// http://localhost:8182/Inventarka/AddAndEditPrinter
        /// </summary>
        /// <param name="printer"></param>
        /// <param name="userIdEdit">Кто редактировал</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AddAndEditPrinter?userIdEdit={userIdEdit}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        ModelReturn<Printer> AddAndEditPrinter(Printer printer, string userIdEdit);

        /// <summary>
        /// Добавление Коммутатора
        /// http://localhost:8182/Inventarka/AddAndEditSwith
        /// </summary>
        /// <param name="swith"></param>
        /// <param name="userIdEdit">Кто редактировал</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AddAndEditSwith?userIdEdit={userIdEdit}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        ModelReturn<EfDatabase.Inventory.Base.Swithe> AddAndEditSwith(EfDatabase.Inventory.Base.Swithe swith, string userIdEdit);

        /// <summary>
        /// Добавление модели Коммутатора
        /// http://localhost:8182/Inventarka/AddAndEditModelSwith
        /// </summary>
        /// <param name="modelswith"></param>
        /// <returns></returns>
        [OperationContract]
       [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AddAndEditModelSwith",
           ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
       ModelReturn<EfDatabase.Inventory.Base.ModelSwithe> AddAndEditModelSwith(EfDatabase.Inventory.Base.ModelSwithe modelswith);
        /// <summary>
        /// Добавление сканера
        /// http://localhost:8182/Inventarka/AddAndEditScaner
        /// </summary>
        /// <param name="scaner"></param>
        /// <param name="userIdEdit">Кто редактировал</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AddAndEditScaner?userIdEdit={userIdEdit}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        ModelReturn<EfDatabase.Inventory.Base.ScanerAndCamer> AddAndEditScaner(EfDatabase.Inventory.Base.ScanerAndCamer scaner, string userIdEdit);
        /// <summary>
        /// Добавление или редактирования серверного оборудования
        /// http://localhost:8182/Inventarka/AddAndEditServerEquipment
        /// </summary>
        /// <param name="serverEquipment">Серверное оборудование</param>
        /// <param name="userIdEdit">Кто редактировал</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AddAndEditServerEquipment?userIdEdit={userIdEdit}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        ModelReturn<ServerEquipment> AddAndEditServerEquipment(ServerEquipment serverEquipment, string userIdEdit);
        /// <summary>
        /// Добавление или редактирования модели серверного оборудования
        /// http://localhost:8182/Inventarka/AddAndEditModelSeverEquipment
        /// </summary>
        /// <param name="modelSeverEquipment">Модель серверного оборудования</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AddAndEditModelSeverEquipment", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        ModelReturn<ModelSeverEquipment> AddAndEditModelSeverEquipment(ModelSeverEquipment modelSeverEquipment);

        /// <summary>
        /// Добавление или редактирования производителя серверного оборудования
        /// http://localhost:8182/Inventarka/AddAndEditManufacturerSeverEquipment
        /// </summary>
        /// <param name="manufacturerSeverEquipment">Производитель серверного оборудования</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AddAndEditManufacturerSeverEquipment", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        ModelReturn<ManufacturerSeverEquipment> AddAndEditManufacturerSeverEquipment(ManufacturerSeverEquipment manufacturerSeverEquipment);
        /// <summary>
        /// Добавление или редактирования типа сервера
        /// http://localhost:8182/Inventarka/AddAndEditTypeServer
        /// </summary>
        /// <param name="typeServer">Тип сервера</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AddAndEditTypeServer", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        ModelReturn<TypeServer> AddAndEditTypeServer(TypeServer typeServer);
        /// <summary>
        /// Добавление или обновление мфу
        /// http://localhost:8182/Inventarka/AddAndEditMfu
        /// </summary>
        /// <param name="mfu"></param>
        /// <param name="userIdEdit">Кто редактировал</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AddAndEditMfu?userIdEdit={userIdEdit}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        ModelReturn<EfDatabase.Inventory.Base.Mfu> AddAndEditMfu(EfDatabase.Inventory.Base.Mfu mfu, string userIdEdit);

        /// <summary>
        /// Добавление или обновление Системного блока
        /// http://localhost:8182/Inventarka/AddAndEditSysBlok
        /// </summary>
        /// <param name="sysblock"></param>
        /// <param name="userIdEdit">Кто редактировал</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AddAndEditSysBlok?userIdEdit={userIdEdit}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        ModelReturn<SysBlock> AddAndEditSysBlok(SysBlock sysblock, string userIdEdit);

        /// <summary>
        /// Добавление или обновление Монитора
        /// http://localhost:8182/Inventarka/AddAndEditMonitor
        /// </summary>
        /// <param name="monitor"></param>
        /// <param name="userIdEdit">Кто редактировал</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AddAndEditMonitor?userIdEdit={userIdEdit}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        ModelReturn<Monitor> AddAndEditMonitor(Monitor monitor, string userIdEdit);

        /// <summary>
        /// Добавление или обновление Монитора
        /// http://localhost:8182/Inventarka/AddAndEditOtdel
        /// </summary>
        /// <param name="otdel"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AddAndEditOtdel", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        ModelReturn<EfDatabase.Inventory.Base.Otdel> AddAndEditOtdel(EfDatabase.Inventory.Base.Otdel otdel);


        /// <summary>
        /// Добавление или обновление Монитора
        /// http://localhost:8182/Inventarka/SelectXml
        /// </summary>
        /// <param name="logica"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/SelectXml", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<string> SelectXml(EfDatabaseParametrsModel.LogicaSelect logica);
        /// <summary>
        /// Удаление документа
        /// http://localhost:8182/Inventarka/DeleteDocument
        /// </summary>
        /// <param name="iddoc">Ун документа</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/DeleteDocument", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        string DeleteDocument(int iddoc);

        /// <summary>
        /// Загрузка документа на внутренее перемещение
        /// http://localhost:8182/Inventarka/LoadDocument
        /// </summary>
        /// <param name="iddoc">Ун документа</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/LoadDocument", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Stream LoadDocument(int iddoc);

        /// <summary>
        /// Загрузка книги учета
        /// http://localhost:8182/Inventarka/LoadBook
        /// </summary>
        /// <param name="iddoc">Ун книги учета</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/LoadBook", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Stream LoadBook(int iddoc);


        /// <summary>
        /// Создание накладной на внутреннее перемещение
        /// http://localhost:8182/Inventarka/Invoice
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/Invoice", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<Stream> GenerateDocuments(EfDatabaseInvoice.Report report);

        /// <summary>
        /// Прием файла с сайта 
        /// http://localhost:8182/Inventarka/AddFileDb
        /// </summary>
        /// <param name="uploadFileModel">Модель с файлами tiff/tif</param>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AddFileDb", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        ModelError[] AddFileToDb(EfDatabaseUploadFile.UploadFile uploadFileModel);
        /// <summary>
        /// Актулизация пользователей с БД кадрами
        /// http://localhost:8182/Inventarka/ActualUsers
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/ActualUsers", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<string> ActualUsers();
        /// <summary>
        /// Актулизация пользователей с БД кадрами
        /// http://localhost:8182/Inventarka/ActualComputerIp
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/ActualComputerIp", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<string> ActualComputerIp();
        /// <summary>
        /// Добавление или обновление Телефона
        /// http://localhost:8182/Inventarka/AddAndEditTelephone?userIdEdit={userIdEdit}
        /// Но зачем мне сдесь Id Users SignalR? не понял если только уведомления отсылать в статический класс Возможно!!!
        /// </summary>
        /// <param name="telephon">Телефон</param>
        /// <param name="userIdEdit">Пользователь кто редактировал</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AddAndEditTelephone?userIdEdit={userIdEdit}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        ModelReturn<EfDatabase.Inventory.Base.Telephon> AddAndEditTelephon(EfDatabase.Inventory.Base.Telephon telephon, string userIdEdit);
        /// <summary>
        /// Добавление или обновление Телефона
        /// http://localhost:8182/Inventarka/AddAndEditBlockPower
        /// </summary>
        /// <param name="blockpower">Источник бесперебойного питания</param>
        /// <param name="userIdEdit">Пользователь кто редактировал</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AddAndEditBlockPower?userIdEdit={userIdEdit}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        ModelReturn<EfDatabase.Inventory.Base.BlockPower> AddAndEditBlockPower(EfDatabase.Inventory.Base.BlockPower blockpower, string userIdEdit);
        /// <summary>
        /// Добавление или обновление наименование системного блока
        /// http://localhost:8182/Inventarka/AddAndEditNameSysBlock
        /// </summary>
        /// <param name="nameSysBlock"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AddAndEditNameSysBlock", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        ModelReturn<EfDatabase.Inventory.Base.NameSysBlock> AddAndEditNameSysBlock(EfDatabase.Inventory.Base.NameSysBlock nameSysBlock);

        /// <summary>
        /// Добавление или обновление наименование монитора
        /// http://localhost:8182/Inventarka/AddAndEditNameMonitor
        /// </summary>
        /// <param name="nameMonitor"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AddAndEditNameMonitor", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        ModelReturn<EfDatabase.Inventory.Base.NameMonitor> AddAndEditNameMonitor(EfDatabase.Inventory.Base.NameMonitor nameMonitor);

        /// <summary>
        /// Добавление или обновление наименование модель ИБП
        /// http://localhost:8182/Inventarka/AddAndEditNameModelBlokPower
        /// </summary>
        /// <param name="nameModelBlokPower"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AddAndEditNameModelBlokPower", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        ModelReturn<EfDatabase.Inventory.Base.ModelBlockPower> AddAndEditNameModelBlokPower(EfDatabase.Inventory.Base.ModelBlockPower nameModelBlokPower);

        /// <summary>
        /// Добавление или обновление наименование производителя ИБП
        /// http://localhost:8182/Inventarka/AddAndEditNameProizvoditelBlockPower
        /// </summary>
        /// <param name="nameProizvoditelBlockPower"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AddAndEditNameProizvoditelBlockPower", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        ModelReturn<EfDatabase.Inventory.Base.ProizvoditelBlockPower> AddAndEditNameProizvoditelBlockPower(EfDatabase.Inventory.Base.ProizvoditelBlockPower nameProizvoditelBlockPower);

        /// <summary>
        /// Добавление или обновление наименование наименование партии
        /// http://localhost:8182/Inventarka/AddAndEditNameSupply
        /// </summary>
        /// <param name="nameSupply"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AddAndEditNameSupply", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        ModelReturn<EfDatabase.Inventory.Base.Supply> AddAndEditNameSupply(EfDatabase.Inventory.Base.Supply nameSupply);

        /// <summary>
        /// Добавление или обновление наименование статуса
        /// http://localhost:8182/Inventarka/AddAndEditNameStatus
        /// </summary>
        /// <param name="nameStatus"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AddAndEditNameStatus", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        ModelReturn<Statusing> AddAndEditNameStatus(Statusing nameStatus);

        /// <summary>
        /// Добавление или обновление наименование кабинета
        /// http://localhost:8182/Inventarka/AddAndEditNameKabinet
        /// </summary>
        /// <param name="nameKabinet"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AddAndEditNameKabinet", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        ModelReturn<EfDatabase.Inventory.Base.Kabinet> AddAndEditNameKabinet(EfDatabase.Inventory.Base.Kabinet nameKabinet);

        /// <summary>
        /// Добавление или обновление наименование модели принтера(МФУ)
        /// http://localhost:8182/Inventarka/AddAndEditNameFullModel
        /// </summary>
        /// <param name="nameFullModel"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AddAndEditNameFullModel", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        ModelReturn<EfDatabase.Inventory.Base.FullModel> AddAndEditNameFullModel(EfDatabase.Inventory.Base.FullModel nameFullModel);

        /// <summary>
        /// Добавление или обновление наименование классификации принтера(МФУ)
        /// http://localhost:8182/Inventarka/AddAndEditNameClassification
        /// </summary>
        /// <param name="nameClassification"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AddAndEditNameClassification", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        ModelReturn<Classification> AddAndEditNameClassification(Classification nameClassification);

        /// <summary> 
        /// Добавление или обновление наименование производителя принтера(МФУ) 
        /// http://localhost:8182/Inventarka/AddAndEditNameFullProizvoditel
        /// </summary>
        /// <param name="nameFullProizvoditel"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AddAndEditNameFullProizvoditel", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        ModelReturn<EfDatabase.Inventory.Base.FullProizvoditel> AddAndEditNameFullProizvoditel(EfDatabase.Inventory.Base.FullProizvoditel nameFullProizvoditel);

        /// <summary> 
        /// Добавление или обновление CopySave для МФУ
        /// http://localhost:8182/Inventarka/AddAndEditNameCopySave
        /// </summary>
        /// <param name="nameCopySave"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AddAndEditNameCopySave", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        ModelReturn<EfDatabase.Inventory.Base.CopySave> AddAndEditNameCopySave(EfDatabase.Inventory.Base.CopySave nameCopySave);

        /// <summary>
        /// Удаление не актуальных пользователей
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/DeleteUser?userIdEdit={userIdEdit}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        ModelReturn<User> DeleteUser(User user, string userIdEdit);
        
        /// <summary>
        /// Удаление не актуальных системных блоков
        /// </summary>
        /// <param name="sysBlock">Системный блок</param>
        /// <param name="userIdEdit">Кто удалял</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/DeleteSysBlock?userIdEdit={userIdEdit}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        ModelReturn<SysBlock> DeleteSysBlock(SysBlock sysBlock, string userIdEdit);
        
        /// <summary>
        /// Удаление не актуального серверного оборудования
        /// </summary>
        /// <param name="serverEquipment">Серверное оборудование</param>
        /// <param name="userIdEdit">Кто удалял</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/DeleteServerEquipment?userIdEdit={userIdEdit}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        ModelReturn<ServerEquipment> DeleteServerEquipment(ServerEquipment serverEquipment, string userIdEdit);

        /// <summary>
        /// Удаление не актуальных мониторов
        /// </summary>
        /// <param name="monitor">Монитор</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/DeleteMonitor?userIdEdit={userIdEdit}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        ModelReturn<Monitor> DeleteMonitor(Monitor monitor, string userIdEdit);

        /// <summary>
        /// Удаление не актуальных Принтеров
        /// </summary>
        /// <param name="printer">Принтер</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/DeletePrinter?userIdEdit={userIdEdit}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        ModelReturn<Printer> DeletePrinter(Printer printer, string userIdEdit);

        /// <summary>
        /// Удаление не актуальных Сканеров или Камер
        /// </summary>
        /// <param name="scanner">Сканеров или Камера</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/DeleteScannerAndCamera?userIdEdit={userIdEdit}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        ModelReturn<ScanerAndCamer> DeleteScannerAndCamera(ScanerAndCamer scanner, string userIdEdit);

        /// <summary>
        /// Удаление не актуальных МФУ
        /// </summary>
        /// <param name="mfu">МФУ</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/DeleteMfu?userIdEdit={userIdEdit}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        ModelReturn<EfDatabase.Inventory.Base.Mfu> DeleteMfu(EfDatabase.Inventory.Base.Mfu mfu, string userIdEdit);

        /// <summary>
        /// Удаление не актуальных ИБП
        /// </summary>
        /// <param name="blockPower">ИБП</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/DeleteBlockPower?userIdEdit={userIdEdit}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        ModelReturn<EfDatabase.Inventory.Base.BlockPower> DeleteBlockPower(EfDatabase.Inventory.Base.BlockPower blockPower, string userIdEdit);

        /// <summary>
        /// Удаление не актуальных Коммутаторов
        /// </summary>
        /// <param name="switches">Коммутатор</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/DeleteSwitch?userIdEdit={userIdEdit}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        ModelReturn<EfDatabase.Inventory.Base.Swithe> DeleteSwitch(EfDatabase.Inventory.Base.Swithe switches, string userIdEdit);

        /// <summary>
        /// Удаление не актуальных Телефонов
        /// </summary>
        /// <param name="telephone">Телефон</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/DeleteTelephone?userIdEdit={userIdEdit}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        ModelReturn<EfDatabase.Inventory.Base.Telephon> DeleteTelephone(EfDatabase.Inventory.Base.Telephon telephone, string userIdEdit);
        /// <summary>
        /// Просмотр Body
        /// </summary>
        /// <param name="model">Модель почты</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/VisibilityBodyMail", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<string> VisibilityBodyMail(WebMailModel model);
        /// <summary>
        /// Выгрузка вложения из почты
        /// </summary>
        /// <param name="model">Модель почты</param>
        /// <returns></returns>
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/OutputMail", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        [OperationContract]
        Task<Stream> OutputMail(WebMailModel model);
        /// <summary>
        /// Удаление вложения из почты
        /// </summary>
        /// <param name="model">Модель почты</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/DeleteMail", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<string> DeleteMail(WebMailModel model);

        /// <summary>
        /// Все идентификаторы пользователей
        /// http://localhost:8182/Inventarka/AllMailIdentifies
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AllMailIdentifies", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<string> AllMailIdentifies();

        /// <summary>
        /// Все группы
        /// http://localhost:8182/Inventarka/AllMailGroups
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AllMailGroups", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<string> AllMailGroups();
        /// <summary>
        /// Редактирование идентификатора
        /// http://localhost:8182/Inventarka/AddAndEditMailIdentifies
        /// </summary>
        /// <param name="nameMailIdentifier"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AddAndEditMailIdentifies", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        ModelReturn<MailIdentifier> AddAndEditMailIdentifies(MailIdentifier nameMailIdentifier);
        /// <summary>
        /// Редактирование Группы
        /// http://localhost:8182/Inventarka/AddAndEditMailGroups
        /// </summary>
        /// <param name="nameMailGroups"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AddAndEditMailGroups", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        ModelReturn<MailGroup> AddAndEditMailGroups(MailGroup nameMailGroups);
        /// <summary>
        /// Все шаблоны СТО
        /// http://localhost:8182/Inventarka/AllTemplateSupport
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AllTemplateSupport", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<string> AllTemplateSupport();
        /// <summary>
        /// Данный api создан для генерации и отправки запроса в СТО по ун шаблону
        /// </summary>
        /// <param name="modelSupport">Модель генерации для отправки на СТО</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/ServiceSupport", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<ModelParametrSupport> ServiceSupport(ModelParametrSupport modelSupport);
        /// <summary>
        /// Снять статус техники по Id технике
        /// </summary>
        /// <param name="allTechnical">Техника</param>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/IsCheckStatusNull", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<string> IsCheckStatusNull(AllTechnic allTechnical);
        /// <summary>
        /// Генерация QR code для техники
        /// </summary>
        /// <param name="serialNumber">Серийный номер техники</param>
        /// <param name="isAll">Создать на всю технику</param>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/GenerateQrCodeTechnical?serialNumber={serialNumber}&isAll={isAll}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<Stream> GenerateQrCodeTechnical(string serialNumber, bool isAll = false);
        /// <summary>
        /// Генерация QR Кодов кабинетов
        /// </summary>
        /// <param name="numberOffice">Номер офиса для QR кода</param>
        /// <param name="isAll">Делать на все кабинеты</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/GenerateQrCodeOffice?numberOffice={numberOffice}&isAll={isAll}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<Stream> GenerateQrCodeOffice(string numberOffice, bool isAll=false);

        /// <summary>
        /// Вся техника на пользователя для личного кабинета
        /// http://localhost:8182/Inventarka/AllTechnicsLk
        /// </summary>
        /// <param name="idUser">Ун пользователя</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AllTechnicsLk?idUser={idUser}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<string> AllTechnicsLk(int idUser);
        /// <summary>
        /// Все токены ключи реестр
        /// http://localhost:8182/Inventarka/AllToken
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AllToken", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<string> AllToken();
        /// <summary>
        /// Добавление или обновление Токена ключа
        /// http://localhost:8182/Inventarka/AddAndEditToken?userIdEdit={userIdEdit}
        /// </summary>
        /// <param name="token">Токен ключ</param>
        /// <param name="userIdEdit">Пользователь кто редактировал</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AddAndEditToken?userIdEdit={userIdEdit}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        ModelReturn<Token> AddAndEditToken(Token token, string userIdEdit);
        /// <summary>
        /// Удаление не актуального токена
        /// </summary>
        /// <param name="token">Токен ключ</param>
        /// <param name="userIdEdit">Кто удалял</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/DeleteToken?userIdEdit={userIdEdit}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        ModelReturn<Token> DeleteToken(Token token, string userIdEdit);
        /// <summary>
        /// Проверка по УН процесса запущен ли он или нет
        /// </summary>
        /// <param name="idTask">УН процесса</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, UriTemplate = "/IsBeginTask?userIdEdit={idTask}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<bool> IsBeginTask(int idTask);
   }
}
