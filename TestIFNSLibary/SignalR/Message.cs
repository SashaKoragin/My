using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Routing;
using Owin;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;

namespace TestIFNSLibary.SignalR
{
   public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            try
            {
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
    public class ServiceMessage : Hub // this "Hub" is hooked by SignalR and is important. 
    {
        private readonly static BasicChatConnect<string> _connections = new BasicChatConnect<string>();
        /// <summary>
        /// Переназначеный класс подключение пользователя
        /// </summary>
        /// <returns></returns>
        public override Task OnConnected()
        {
            try
            {
            string name = Context.QueryString["user"];
            Clients.All.OnUserConnected(name);
            _connections.Add(name, Context.ConnectionId);
          //  Welcome(name,"Добро пожаловать пользователь: "+ name);
            Loggers.Log4NetLogger.Info(new Exception("Подключился пользователь:" + name));
           
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
            return base.OnConnected();
        }
        /// <summary>
        /// Переназначеный класс отключение пользователя
        /// </summary>
        /// <returns></returns>
        public override Task OnDisconnected(bool stopCalled)
        {
            string name = Context.User.Identity.Name;
            _connections.Remove(name, Context.ConnectionId);
            Loggers.Log4NetLogger.Info(new Exception("Отключился пользователь:" + name));
            return base.OnDisconnected(stopCalled);
        }
        /// <summary>
        /// Переподключение
        /// </summary>
        /// <returns></returns>
        public override Task OnReconnected()
        {
            string name = Context.User.Identity.Name;

            if (!_connections.GetConnections(name).Contains(Context.ConnectionId))
            {
                _connections.Add(name, Context.ConnectionId);
            }

            return base.OnReconnected();
        }

        /// <summary>
        /// Отправка сообщения пользователю
        /// </summary>
        /// <param name="who"></param>
        /// <param name="message"></param>
        public void SendChatMessage(ChatMessage message)
        {
            //string name = Context.User.Identity.Name;

            //foreach (var connectionId in _connections.GetConnections(who))
            //{
            //    Clients.Client(connectionId).addChatMessage(name + ": " + message);
            //}
            Clients.All.OnMessageSent(new ChatMessage("Hannes", "Message"));
        }
        public void Chat(ChatMessage message)
        {
            Clients.All.OnMessageSent(message);
        }
    }

    public class ChatMessage
    {
        public string Who;
        public string Message;
        public ChatMessage(string who,
            string message)
        {
            Who = who;
            Message = message;
        }
    }
}
