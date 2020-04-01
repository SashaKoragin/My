using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Hubs;

namespace SignalRLibary.HubError
{
   public class HubError : HubPipelineModule
    {
        protected override void OnIncomingError(ExceptionContext exceptionContext, IHubIncomingInvokerContext invokerContext)
        {
            var caller = invokerContext.Hub.Clients.Caller;
            Loggers.Log4NetLogger.Error(exceptionContext.Error);
            caller.ExceptionHandler(exceptionContext.Error.Message);
            Loggers.Log4NetLogger.Error(new Exception($"Кто вызывал {caller}"));
        }
    }
}
