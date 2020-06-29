using System;
using System.Linq;
using System.Threading.Tasks;
using EfDatabase.Inventory.Base;
using EfDatabase.Inventory.BaseLogic.AddObjectDb;
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

/*        public override Task Error()*/        /// <summary>
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
        public static void SubscribeUser(EfDatabase.Inventory.Base.User user)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<SignalRinventory>();
            Loggers.Log4NetLogger.Info(new Exception("Модель Пользователя рассылка пошла: " + user.IdUser + " " + user.NameUser));
            SerializeJson json = new SerializeJson();
            context.Clients.All.SubscribeUser(json.JsonLibaryIgnoreDate(user));
        }
        /// <summary>
        /// Удаление пользователя подписка пользователя на удаление
        /// </summary>
        /// <param name="modelUser">Модель пользователей </param>
        public static void SubscribeDeleteUser(ModelReturn<User> modelUser)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<SignalRinventory>();
            context.Clients.All.SubscribeDeleteUser(modelUser);
        }
        /// <summary>
        /// Удаление системного блока подписка пользователя на удаление
        /// </summary>
        /// <param name="modelSysBlock">Модель системных блоков</param>
        public static void SubscribeDeleteSystemUnit(ModelReturn<SysBlock> modelSysBlock)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<SignalRinventory>();
            context.Clients.All.SubscribeDeleteSystemUnit(modelSysBlock);
        }
        /// <summary>
        /// Удаление монитора подписка пользователя на удаление
        /// </summary>
        /// <param name="modelMonitor">Модель мониторов</param>
        public static void SubscribeDeleteMonitor(ModelReturn<Monitor> modelMonitor)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<SignalRinventory>();
            context.Clients.All.SubscribeDeleteMonitor(modelMonitor);
        }

        /// <summary>
        /// Удаление принтера подписка пользователя на удаление
        /// </summary>
        /// <param name="modelPrinter">Модель принтеров</param>
        public static void SubscribeDeletePrinter(ModelReturn<Printer> modelPrinter)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<SignalRinventory>();
            context.Clients.All.SubscribeDeletePrinter(modelPrinter);
        }

        /// <summary>
        /// Удаление сканера или камеры подписка пользователя на удаление
        /// </summary>
        /// <param name="modelScanner">Модель сканеров или камер</param>
        public static void SubscribeDeleteScannerAndCamera(ModelReturn<ScanerAndCamer> modelScanner)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<SignalRinventory>();
            context.Clients.All.SubscribeDeleteScannerAndCamera(modelScanner);
        }

        /// <summary>
        /// Удаление МФУ подписка пользователя на удаление
        /// </summary>
        /// <param name="modelMfu">Модель сканеров или камер</param>
        public static void SubscribeDeleteMfu(ModelReturn<Mfu> modelMfu)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<SignalRinventory>();
            context.Clients.All.SubscribeDeleteMfu(modelMfu);
        }

        /// <summary>
        /// Удаление ИБП подписка пользователя на удаление
        /// </summary>
        /// <param name="modelBlockPower">Модель ИБП</param>
        public static void SubscribeDeleteBlockPower(ModelReturn<BlockPower> modelBlockPower)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<SignalRinventory>();
            context.Clients.All.SubscribeDeleteBlockPower(modelBlockPower);
        }

        /// <summary>
        /// Удаление коммутаторов подписка пользователя на удаление
        /// </summary>
        /// <param name="modelSwitches">Модель Коммутаторов</param>
        public static void SubscribeDeleteSwitch(ModelReturn<Swithe> modelSwitches)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<SignalRinventory>();
            context.Clients.All.SubscribeDeleteSwitch(modelSwitches);
        }

        /// <summary>
        /// Удаление телефонов подписка пользователя на удаление
        /// </summary>
        /// <param name="modelTelephone">Модель Телефонов</param>
        public static void SubscribeDeleteTelephone(ModelReturn<Telephon> modelTelephone)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<SignalRinventory>();
            context.Clients.All.SubscribeDeleteTelephone(modelTelephone);
        }

        /// <summary>
        /// Подписка на изменение Отдел
        /// </summary>
        /// <param name="otdel">Отдел</param>
        public static void SubscribeOtdel(EfDatabase.Inventory.Base.Otdel otdel)
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
        public static void SubscribeTelephone(EfDatabase.Inventory.Base.Telephon telephone)
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
        public static void SubscribePrinter(EfDatabase.Inventory.Base.Printer printer)
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
        public static void SubscribeSwithe(EfDatabase.Inventory.Base.Swithe swithe)
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
        public static void SubscribeScanerAndCamer(EfDatabase.Inventory.Base.ScanerAndCamer scaner)
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
        public static void SubscribeMfu(EfDatabase.Inventory.Base.Mfu mfu)
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
        public static void SubscribeSysBlok(EfDatabase.Inventory.Base.SysBlock sysblok)
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
        public static void SubscribeMonitor(EfDatabase.Inventory.Base.Monitor monitor)
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
        public static void SubscribeBlockPower(EfDatabase.Inventory.Base.BlockPower blokpower)
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
        public static void SubscribeNameSysBlock(EfDatabase.Inventory.Base.NameSysBlock nameSysBlock)
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
        public static void SubscribeNameMonitor(EfDatabase.Inventory.Base.NameMonitor nameMonitor)
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
        public static void SubscribeModelBlockPower(EfDatabase.Inventory.Base.ModelBlockPower nameModelBlokPower)
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
        public static void SubscribeProizvoditelBlockPower(EfDatabase.Inventory.Base.ProizvoditelBlockPower nameProizvoditelBlockPower)
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
        public static void SubscribeSupply(EfDatabase.Inventory.Base.Supply nameSupply)
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
        public static void SubscribeStatusing(EfDatabase.Inventory.Base.Statusing nameStatus)
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
        public static void SubscribeKabinet(EfDatabase.Inventory.Base.Kabinet nameKabinet)
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
        public static void SubscribeFullModel(EfDatabase.Inventory.Base.FullModel nameFullModel)
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
        public static void SubscribeClassification(EfDatabase.Inventory.Base.Classification nameClassification)
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
        public static void SubscribeFullProizvoditel(EfDatabase.Inventory.Base.FullProizvoditel nameFullProizvoditel)
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
        public static void SubscribeCopySave(EfDatabase.Inventory.Base.CopySave nameCopySave)
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
        public static void SubscribeModelSwithe(EfDatabase.Inventory.Base.ModelSwithe modelSwithes)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<SignalRinventory>();
            Loggers.Log4NetLogger.Info(new Exception("Модель Модель Коммутатора рассылка пошла: " + modelSwithes.NameModel));
            SerializeJson json = new SerializeJson();
            context.Clients.All.SubscribeModelSwithe(json.JsonLibaryIgnoreDate(modelSwithes));
        }

        /// <summary>
        /// Подписка на изменение Модель Идентификаторы
        /// </summary>
        /// <param name="modelMailIdentifier">Модель Идентификаторы пользователей</param>
        public static void SubscribeModelMailIdentifier(MailIdentifier modelMailIdentifier)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<SignalRinventory>();
            Loggers.Log4NetLogger.Info(new Exception("Рассылка идентификаторов пользователей пошла: " + modelMailIdentifier.IdentifierUser));
            SerializeJson json = new SerializeJson();
            context.Clients.All.SubscribeModelMailIdentifier(json.JsonLibaryIgnoreDate(modelMailIdentifier));
        }
        /// <summary>
        /// Подписка на изменение Модель Группы пользователей
        /// </summary>
        /// <param name="modelMailGroups">Модель Группы пользователей</param>
        public static void SubscribeModelMailGroups(MailGroup modelMailGroups)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<SignalRinventory>();
            Loggers.Log4NetLogger.Info(new Exception("Рассылка групп пользователей пошла: " + modelMailGroups.IdOtdelNumber));
            SerializeJson json = new SerializeJson();
            context.Clients.All.SubscribeModelMailGroups(json.JsonLibaryIgnoreDate(modelMailGroups));
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
