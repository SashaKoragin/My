using System;
using System.Threading.Tasks;
using Owin;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Owin.Cors;
using System.Linq;

namespace SignalRLibary.SignalR
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            try
            {
                GlobalHost.Configuration.ConnectionTimeout = TimeSpan.FromHours(8);
                GlobalHost.Configuration.DisconnectTimeout = TimeSpan.FromHours(5);
                GlobalHost.HubPipeline.AddModule(new HubError.HubError());
                app.Map("/signalr", map =>
                {
                    map.UseCors(CorsOptions.AllowAll);
                    var hubConfiguration = new HubConfiguration
                    {

                        EnableDetailedErrors = true
                        
                    };
                    map.RunSignalR(hubConfiguration);
                });
               
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
        }
    }

    [HubName("ServiceMessage")]
    //[Authorize]
    public class ServiceMessage : Hub // this "Hub" is hooked by SignalR and is important. 
    {
        private static readonly BasicChatConnect<string> _connections = new BasicChatConnect<string>();
        /// <summary>
        /// Переназначеный класс подключение пользователя
        /// </summary>
        /// <returns></returns>
        public override Task OnConnected()
        {
            try
            {
                string name = Context.QueryString["user"];
                string guid = Context.QueryString["guid"];
                
                _connections.Add(name+guid, Context.ConnectionId);
                Welcome("Добро пожаловать пользователь: " + name, Context.ConnectionId);
                Loggers.Log4NetLogger.Info(new Exception("Подключился пользователь: Имя - " + name+ " Номер - "+ guid));
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
            string name = Context.QueryString["user"];
            string guid = Context.QueryString["guid"];
            Loggers.Log4NetLogger.Info(new Exception("Отключился пользователь: Имя - " + name + " Номер - " + guid));
            _connections.Remove(name+guid, Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }


        /// <summary>
        /// Переподключение
        /// </summary>
        /// <returns></returns>
        public override Task OnReconnected()
        {
            string name = Context.QueryString["user"];
            string guid = Context.QueryString["guid"];
            if (!_connections.GetConnections(name+guid).Contains(Context.ConnectionId))
            {
                _connections.Add(name + guid, Context.ConnectionId);
            }
            Loggers.Log4NetLogger.Info(new Exception("Переподключился пользователь: Имя - " + name + " Номер - " + guid));
            return base.OnReconnected();
        }
        public void Chat(ChatMessage message)
        {
            Clients.All.SendMessageAll(message);
        }

        public void Welcome(string welcomeuser, string conectionId)
        {
            Clients.Client(conectionId).Welcome(welcomeuser);
        }

        public static async Task SqlServer(string usernameguid, string message)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<ServiceMessage>();
            var conId = _connections.GetConnections(usernameguid);
            foreach (var id in conId)
            {
              await context.Clients.Client(id).SqlServer(message);
            }
        }
    }
    /// <summary>
    /// Модель сообщений
    /// </summary>
    public class ChatMessage
    {
        /// <summary>
        /// Сообщение пользователя
        /// </summary>
        /// <param name="who"></param>
        /// <param name="message"></param>
        public ChatMessage(string who, string message)
        {
            Who = who;
            Message = message;
        }
        /// <summary>
        /// Кто отправил
        /// </summary>
        public string Who;
        /// <summary>
        /// Сообщение
        /// </summary>
        public string Message;
    }

}

