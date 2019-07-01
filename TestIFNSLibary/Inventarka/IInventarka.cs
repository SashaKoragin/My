using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;
using EfDatabase.Inventarization.Base;
using EfDatabase.Inventarization.BaseLogica.AddObjectDb;
using EfDatabaseParametrsModel;
using SqlLibaryIfns.Inventarization.ModelParametr;

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
        /// http://localhost:8182/Inventarka/Autification
        /// Авторизация для ресурса Инвентаризация
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/Authorization", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<string> Authorization(User user);

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
        /// Все пользователи 
        /// http://localhost:8182/Inventarka/AllUsers
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AllUsers",  ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<string> AllUsers();

        /// <summary>
        /// Все пользователи 
        /// http://localhost:8182/Inventarka/AllPosition
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AllPosition", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<string> AllPosition();

        /// <summary>
        /// Все принтеры
        /// http://localhost:8182/Inventarka/AllPrinters
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AllPrinters", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<string> AllPrinters();

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
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AddAndEditUser", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        ModelReturn AddAndEditUser(User user);
        /// <summary>
        /// Добавление принтера
        /// http://localhost:8182/Inventarka/AddAndEditPrinter
        /// </summary>
        /// <param name="printer"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AddAndEditPrinter", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        ModelReturn AddAndEditPrinter(EfDatabase.Inventarization.Base.Printer printer);

        /// <summary>
        /// Добавление сканера
        /// http://localhost:8182/Inventarka/AddAndEditScaner
        /// </summary>
        /// <param name="scaner"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AddAndEditScaner", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        ModelReturn AddAndEditScaner(EfDatabase.Inventarization.Base.ScanerAndCamer scaner);

        /// <summary>
        /// Добавление или обновление мфу
        /// http://localhost:8182/Inventarka/AddAndEditMfu
        /// </summary>
        /// <param name="mfu"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AddAndEditMfu", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        ModelReturn AddAndEditMfu(EfDatabase.Inventarization.Base.Mfu mfu);

        /// <summary>
        /// Добавление или обновление Системного блока
        /// http://localhost:8182/Inventarka/AddAndEditSysBlok
        /// </summary>
        /// <param name="sysblock"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AddAndEditSysBlok", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        ModelReturn AddAndEditSysBlok(EfDatabase.Inventarization.Base.SysBlock sysblock);

        /// <summary>
        /// Добавление или обновление Монитора
        /// http://localhost:8182/Inventarka/AddAndEditMonitor
        /// </summary>
        /// <param name="monitor"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AddAndEditMonitor", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        ModelReturn AddAndEditMonitor(Monitor monitor);

        /// <summary>
        /// Добавление или обновление Монитора
        /// http://localhost:8182/Inventarka/AddAndEditOtdel
        /// </summary>
        /// <param name="otdel"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AddAndEditOtdel", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        ModelReturn AddAndEditOtdel(EfDatabase.Inventarization.Base.Otdel otdel);


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
        /// Добавление или обновление Монитора
        /// http://localhost:8182/Inventarka/Invoice
        /// </summary>
        /// <returns></returns>
        [OperationContract]
         [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/Invoice", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
         void GenerateDocuments(EfDatabaseInvoice.Report report);
   }
}
