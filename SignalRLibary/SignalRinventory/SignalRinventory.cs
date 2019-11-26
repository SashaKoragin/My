using System;
using System.Linq;
using System.Threading.Tasks;
using LibaryXMLAuto.ReadOrWrite.SerializationJson;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using SignalRLibary.SignalR;

namespace SignalRLibary.SignalRinventory
{

   [HubName("SignalRinventory")]
   public class SignalRinventory : Hub // this "Hub" is hooked by SignalR and is important. 
    {
        private static readonly BasicChatConnect<UsersContext> _connections = new BasicChatConnect<UsersContext>();

        /// <summary>
        /// Получения значения пользователя в БД
        /// </summary>
        /// <param name="contextuser"></param>
        /// <returns></returns>
        public static int? GetUser(string contextuser)
        {
            return _connections.GetUsersToContext(contextuser)?.IdUser;
        }
        
        /// <summary>
        /// Переназначеный класс подключение пользователя
        /// </summary>
        /// <returns></returns>
        public override Task OnConnected()
        {
            var user = new UsersContext()
            {
                IdUser = Convert.ToInt32(Context.QueryString["iduser"]),
                Name = Context.QueryString["user"],
                TabelNumbers = Context.QueryString["tabelnumbers"]
            };
            try
            {
                _connections.Add(user, Context.ConnectionId);
                HelloUser("Добро пожаловать пользователь: " + user.Name, Context.ConnectionId);
                Loggers.Log4NetLogger.Info(new Exception("Подключился пользователь: Имя - " + user.Name + " Номер - " + user.TabelNumbers + " Контекст - "+ Context.ConnectionId));
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
            return base.OnConnected();
        }
        /// <summary>
        /// Метод отключение пользователя!!!
        /// </summary>
        /// <param name="stopCalled"></param>
        /// <returns></returns>
        public override Task OnDisconnected(bool stopCalled)
        {
            var user = new UsersContext()
            {
                IdUser = Convert.ToInt32(Context.QueryString["iduser"]),
                Name = Context.QueryString["user"],
                TabelNumbers = Context.QueryString["tabelnumbers"]
            };
            Loggers.Log4NetLogger.Info(new Exception("Отключился пользователь: Имя - " + user.Name + " Номер - " + user.TabelNumbers + " Контекст - " + Context.ConnectionId));
            _connections.Remove(user, Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }

        /// <summary>
        /// Переподключение
        /// </summary>
        /// <returns></returns>
        public override Task OnReconnected()
        {
            var user = new UsersContext()
            {
                IdUser = Convert.ToInt32(Context.QueryString["iduser"]),
                Name = Context.QueryString["user"],
                TabelNumbers = Context.QueryString["tabelnumbers"]
            };
            if (!_connections.GetConnections(user).Contains(Context.ConnectionId))
            {
                _connections.Add(user, Context.ConnectionId);
            }
            Loggers.Log4NetLogger.Info(new Exception("Переподключился пользователь: Имя - " + user.Name + " Номер - " + user.TabelNumbers+" Контекст - " + Context.ConnectionId));
            return base.OnReconnected();
        }


        public void HelloUser(string hellouser, string conectionId)
        {
            Clients.Client(conectionId).HelloUser(hellouser);
        }
        /// <summary>
        /// Подписка на изменение Пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        public static void SubscribeUser(EfDatabase.Inventarization.Base.User user)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<SignalRinventory>();
            Loggers.Log4NetLogger.Info(new Exception("Модель Пользователя рассылка пошла: " + user.IdUser + " " + user.NameUser));
            SerializeJson json = new SerializeJson();
            context.Clients.All.SubscribeUser(json.JsonLibaryIgnoreDate(user));
        }
        /// <summary>
        /// Подписка на изменение Отдел
        /// </summary>
        /// <param name="otdel">Отдел</param>
        public static void SubscribeOtdel(EfDatabase.Inventarization.Base.Otdel otdel)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<SignalRinventory>();
            Loggers.Log4NetLogger.Info(new Exception("Модель Отдела рассылка пошла: " + otdel.IdOtdel + " " + otdel.NameOtdel));
            SerializeJson json = new SerializeJson();
            context.Clients.All.SubscribeOtdel(json.JsonLibaryIgnoreDate(otdel));
        }
        /// <summary>
        /// Подписка на изменение телефона
        /// </summary>
        /// <param name="telephone">Телефон</param>
        public static void SubscribeTelephone(EfDatabase.Inventarization.Base.Telephon telephone)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<SignalRinventory>();
            Loggers.Log4NetLogger.Info(new Exception("Модель Телефона рассылка пошла: "+telephone.IdTelephon+" "+telephone.Coment));
            SerializeJson json = new SerializeJson();
            context.Clients.All.SubscribeTelephone(json.JsonLibaryIgnoreDate(telephone));
        }
        /// <summary>
        /// Подписка на изменение принтера
        /// </summary>
        /// <param name="printer">Принтер</param>
        public static void SubscribePrinter(EfDatabase.Inventarization.Base.Printer printer)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<SignalRinventory>();
            Loggers.Log4NetLogger.Info(new Exception("Модель Принтера рассылка пошла: " + printer.IdPrinter + " " + printer.Coment));
            SerializeJson json = new SerializeJson();
            context.Clients.All.SubscribePrinter(json.JsonLibaryIgnoreDate(printer));
        }
        /// <summary>
        /// Подписка на изменение Коммутаторов
        /// </summary>
        /// <param name="swithe">Коммутатор</param>
        public static void SubscribeSwithe(EfDatabase.Inventarization.Base.Swithe swithe)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<SignalRinventory>();
            Loggers.Log4NetLogger.Info(new Exception("Модель Комутаторов рассылка пошла: " + swithe.IdSwithes+ " " + swithe.Coment));
            SerializeJson json = new SerializeJson();
            context.Clients.All.SubscribeSwithe(json.JsonLibaryIgnoreDate(swithe));
        }
        /// <summary>
        /// Подписка на изменение Сканера и Камер
        /// </summary>
        /// <param name="scaner">Сканер или Камера</param>
        public static void SubscribeScanerAndCamer(EfDatabase.Inventarization.Base.ScanerAndCamer scaner)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<SignalRinventory>();
            Loggers.Log4NetLogger.Info(new Exception("Модель Сканера рассылка пошла: " + scaner.IdScaner + " " + scaner.Coment));
            SerializeJson json = new SerializeJson();
            context.Clients.All.SubscribeScanerAndCamer(json.JsonLibaryIgnoreDate(scaner));
        }
        /// <summary>
        /// Подписка на изменение МФУ
        /// </summary>
        /// <param name="mfu">МФУ</param>
        public static void SubscribeMfu(EfDatabase.Inventarization.Base.Mfu mfu)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<SignalRinventory>();
            Loggers.Log4NetLogger.Info(new Exception("Модель МФУ рассылка пошла: " + mfu.IdMfu + " " + mfu.Coment));
            SerializeJson json = new SerializeJson();
            context.Clients.All.SubscribeMfu(json.JsonLibaryIgnoreDate(mfu));
        }
        /// <summary>
        /// Подписка на изменение Системных блоков
        /// </summary>
        /// <param name="sysblok">Системный блок</param>
        public static void SubscribeSysBlok(EfDatabase.Inventarization.Base.SysBlock sysblok)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<SignalRinventory>();
            Loggers.Log4NetLogger.Info(new Exception("Модель Системного блока рассылка пошла: " + sysblok.IdSysBlock + " " + sysblok.Coment));
            SerializeJson json = new SerializeJson();
            context.Clients.All.SubscribeSysBlok(json.JsonLibaryIgnoreDate(sysblok));
        }
        /// <summary>
        /// Подписка на изменение Мониторов
        /// </summary>
        /// <param name="monitor">Монитор</param>
        public static void SubscribeMonitor(EfDatabase.Inventarization.Base.Monitor monitor)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<SignalRinventory>();
            Loggers.Log4NetLogger.Info(new Exception("Модель Монитора рассылка пошла: " + monitor.IdMonitor + " " + monitor.Coment));
            SerializeJson json = new SerializeJson();
            context.Clients.All.SubscribeMonitor(json.JsonLibaryIgnoreDate(monitor));
        }
        /// <summary>
        /// Подписка на изменение ИБП
        /// </summary>
        /// <param name="blokpower">ИБП</param>
        public static void SubscribeBlockPower(EfDatabase.Inventarization.Base.BlockPower blokpower)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<SignalRinventory>();
            Loggers.Log4NetLogger.Info(new Exception("Модель ИБП рассылка пошла: " + blokpower.IdBlockPowers + " " + blokpower.Coment));
            SerializeJson json = new SerializeJson();
            context.Clients.All.SubscribeBlockPower(json.JsonLibaryIgnoreDate(blokpower));
        }
        /// <summary>
        /// Подписка на изменение Наименование системного блока
        /// </summary>
        /// <param name="nameSysBlock">Наименование системного блока</param>
        public static void SubscribeNameSysBlock(EfDatabase.Inventarization.Base.NameSysBlock nameSysBlock)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<SignalRinventory>();
            Loggers.Log4NetLogger.Info(new Exception("Модель Наименование системного блока рассылка пошла: " + nameSysBlock.NameComputer));
            SerializeJson json = new SerializeJson();
            context.Clients.All.SubscribeNameSysBlock(json.JsonLibaryIgnoreDate(nameSysBlock));
        }
        /// <summary>
        /// Подписка на изменение Наименование монитора
        /// </summary>
        /// <param name="nameMonitor">Наименование монитора</param>
        public static void SubscribeNameMonitor(EfDatabase.Inventarization.Base.NameMonitor nameMonitor)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<SignalRinventory>();
            Loggers.Log4NetLogger.Info(new Exception("Модель Наименование монитора рассылка пошла: " + nameMonitor.Name));
            SerializeJson json = new SerializeJson();
            context.Clients.All.SubscribeNameMonitor(json.JsonLibaryIgnoreDate(nameMonitor));
        }
        /// <summary>
        /// Подписка на изменение Наименование модели ИБП
        /// </summary>
        /// <param name="nameModelBlokPower">Наименование модели ИБП</param>
        public static void SubscribeModelBlockPower(EfDatabase.Inventarization.Base.ModelBlockPower nameModelBlokPower)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<SignalRinventory>();
            Loggers.Log4NetLogger.Info(new Exception("Модель Наименование модели ИБП рассылка пошла: " + nameModelBlokPower.Name));
            SerializeJson json = new SerializeJson();
            context.Clients.All.SubscribeModelBlockPower(json.JsonLibaryIgnoreDate(nameModelBlokPower));
        }
        /// <summary>
        /// Подписка на изменение Наименование производителя ИБП
        /// </summary>
        /// <param name="nameProizvoditelBlockPower">Наименование производителя ИБП</param>
        public static void SubscribeProizvoditelBlockPower(EfDatabase.Inventarization.Base.ProizvoditelBlockPower nameProizvoditelBlockPower)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<SignalRinventory>();
            Loggers.Log4NetLogger.Info(new Exception("Модель Наименование производителя ИБП рассылка пошла: " + nameProizvoditelBlockPower.Name));
            SerializeJson json = new SerializeJson();
            context.Clients.All.SubscribeProizvoditelBlockPower(json.JsonLibaryIgnoreDate(nameProizvoditelBlockPower));
        }
        /// <summary>
        /// Подписка на изменение Поставка-контракт
        /// </summary>
        /// <param name="nameSupply">Поставка-контракт</param>
        public static void SubscribeSupply(EfDatabase.Inventarization.Base.Supply nameSupply)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<SignalRinventory>();
            Loggers.Log4NetLogger.Info(new Exception("Модель Поставка-контракт рассылка пошла: " + nameSupply.NameSupply));
            SerializeJson json = new SerializeJson();
            context.Clients.All.SubscribeSupply(json.JsonLibaryIgnoreDate(nameSupply));
        }
        /// <summary>
        /// Подписка на изменение Статуса
        /// </summary>
        /// <param name="nameStatus">Статуса</param>
        public static void SubscribeStatusing(EfDatabase.Inventarization.Base.Statusing nameStatus)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<SignalRinventory>();
            Loggers.Log4NetLogger.Info(new Exception("Модель Статуса рассылка пошла: " + nameStatus.Name));
            SerializeJson json = new SerializeJson();
            context.Clients.All.SubscribeStatusing(json.JsonLibaryIgnoreDate(nameStatus));
        }
        /// <summary>
        /// Подписка на изменение Кабинета
        /// </summary>
        /// <param name="nameKabinet">Кабинет</param>
        public static void SubscribeKabinet(EfDatabase.Inventarization.Base.Kabinet nameKabinet)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<SignalRinventory>();
            Loggers.Log4NetLogger.Info(new Exception("Модель Кабинета рассылка пошла: " + nameKabinet.NumberKabinet));
            SerializeJson json = new SerializeJson();
            context.Clients.All.SubscribeKabinet(json.JsonLibaryIgnoreDate(nameKabinet));
        }
        /// <summary>
        /// Подписка на изменение Модель принтера,и т д
        /// </summary>
        /// <param name="nameFullModel">Модель</param>
        public static void SubscribeFullModel(EfDatabase.Inventarization.Base.FullModel nameFullModel)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<SignalRinventory>();
            Loggers.Log4NetLogger.Info(new Exception("Модель принтера,и т д рассылка пошла: " + nameFullModel.NameModel));
            SerializeJson json = new SerializeJson();
            context.Clients.All.SubscribeFullModel(json.JsonLibaryIgnoreDate(nameFullModel));
        }
        /// <summary>
        /// Подписка на изменение Классификации оборудования
        /// </summary>
        /// <param name="nameClassification">Классификация</param>
        public static void SubscribeClassification(EfDatabase.Inventarization.Base.Classification nameClassification)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<SignalRinventory>();
            Loggers.Log4NetLogger.Info(new Exception("Модель Классификации рассылка пошла: " + nameClassification.NameClass));
            SerializeJson json = new SerializeJson();
            context.Clients.All.SubscribeClassification(json.JsonLibaryIgnoreDate(nameClassification));
        }
        /// <summary>
        /// Подписка на изменение Производителя
        /// </summary>
        /// <param name="nameFullProizvoditel">Производителя</param>
        public static void SubscribeFullProizvoditel(EfDatabase.Inventarization.Base.FullProizvoditel nameFullProizvoditel)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<SignalRinventory>();
            Loggers.Log4NetLogger.Info(new Exception("Модель Производители рассылка пошла: " + nameFullProizvoditel.NameProizvoditel));
            SerializeJson json = new SerializeJson();
            context.Clients.All.SubscribeFullProizvoditel(json.JsonLibaryIgnoreDate(nameFullProizvoditel));
        }
        /// <summary>
        /// Подписка на изменение CopySave
        /// </summary>
        /// <param name="nameCopySave">CopySave</param>
        public static void SubscribeCopySave(EfDatabase.Inventarization.Base.CopySave nameCopySave)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<SignalRinventory>();
            Loggers.Log4NetLogger.Info(new Exception("Модель CopySave рассылка пошла: " + nameCopySave.NameCopySave));
            SerializeJson json = new SerializeJson();
            context.Clients.All.SubscribeCopySave(json.JsonLibaryIgnoreDate(nameCopySave));
        }
        /// <summary>
        /// Подписка на изменение Модель Коммутатора
        /// </summary>
        /// <param name="modelSwithes">Модель Коммутатора</param>
        public static void SubscribeModelSwithe(EfDatabase.Inventarization.Base.ModelSwithe modelSwithes)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<SignalRinventory>();
            Loggers.Log4NetLogger.Info(new Exception("Модель Модель Коммутатора рассылка пошла: " + modelSwithes.NameModel));
            SerializeJson json = new SerializeJson();
            context.Clients.All.SubscribeModelSwithe(json.JsonLibaryIgnoreDate(modelSwithes));
        }

    }
    public class UsersContext
    {
        /// <summary>
        /// Ун пользователя
        /// </summary>
        public int? IdUser { get; set; }
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Табельный номер
        /// </summary>
        public string TabelNumbers { get; set; }
    }
}
