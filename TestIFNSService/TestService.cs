using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceProcess;
using Microsoft.Owin;
using Microsoft.Owin.Hosting;
using TestIFNSLibary.Service;
using SignalRLibary.SignalR;


[assembly: OwinStartup(typeof(Startup))]
namespace TestIFNSService
{
    public partial class TestService : ServiceBase
    {
        public TestService()
        {
            InitializeComponent();
        }

        public IDisposable MyServerSignalR { get; set; }
        public ServiceHost ServiceHost { get; set; }
        public ServiceHost ServiceRest { get; set; }
        public ServiceHost Inventarization { get; set; }

        protected override void OnStart(string[] args)
        {
            var url = "http://+:8059";
            MyServerSignalR =  WebApp.Start(url);
            ServiceRest = new WebServiceHost(typeof(TestIFNSLibary.ServiceRest.ServiceRest));
            Inventarization = new WebServiceHost(typeof(TestIFNSLibary.Inventarka.Inventarka));
            ServiceHost = new ServiceHost(typeof(CommandDbf));
            ServiceRest.Open();
            ServiceHost.Open();
            Inventarization.Open();
            var timeEvent = new InventoryProcess.StartProcessInventory.ProcessStart.TimeEventProcess();
            Loggers.LogFileServer.Info(new Exception($"Запустили сервер Инвентаризации!"));
            Loggers.Log4NetLogger.Info(new Exception($"Запустили сервер Инвентаризации!"));
        }

        protected override void OnStop()
        {
            if (ServiceHost != null)
            {
                ServiceHost.Close();
                ServiceHost = null;
            }
            if (ServiceRest != null)
            {
                ServiceRest.Close();
                ServiceRest = null;
            }
            if (Inventarization != null)
            {
                Inventarization.Close();
                Inventarization = null;
            }
            MyServerSignalR?.Dispose();
            Loggers.LogFileServer.Info(new Exception($"Остановили сервер Инвентаризации!"));
            Loggers.Log4NetLogger.Info(new Exception($"Остановили сервер Инвентаризации!"));
        }
    }
}
