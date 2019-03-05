using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceProcess;
using Microsoft.AspNet.SignalR;
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

        public ServiceHost Servicehost;
        public ServiceHost ServiceRest;

        protected override void OnStart(string[] args)
        {
            if (Servicehost != null)
            {
               Servicehost.Close();
               ServiceRest.Close();
            }
            var url = "http://+:8059";
            WebApp.Start(url);
            ServiceRest = new WebServiceHost(typeof(TestIFNSLibary.ServiceRest.ServiceRest));
            Servicehost = new ServiceHost(typeof(CommandDbf));
            ServiceRest.Open();
            Servicehost.Open();
            var timeEvent = new TestIFNSLibary.TimeEvent.TimeEvent();
        }

        protected override void OnStop()
        {
            if (Servicehost != null)
            {
                Servicehost.Close();
                ServiceRest.Close();
                ServiceRest = null;
                Servicehost = null;
            }
        }
    }
}
