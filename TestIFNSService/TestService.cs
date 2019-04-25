using System.ServiceModel;
using System.ServiceModel.Security;
using System.ServiceModel.Web;
using System.ServiceProcess;
using Microsoft.Owin;
using Microsoft.Owin.Hosting;
using TestIFNSLibary.Service;
using SignalRLibary.SignalR;
using static System.ServiceModel.Security.UserNamePasswordValidationMode;

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
        public ServiceHost Inventarization;

        protected override void OnStart(string[] args)
        {
            if (Servicehost != null)
            {
               Servicehost.Close();
               ServiceRest.Close();
               Inventarization.Close();
            }
            var url = "http://+:8059";
            WebApp.Start(url);
            ServiceRest = new WebServiceHost(typeof(TestIFNSLibary.ServiceRest.ServiceRest));
           
            Inventarization = new WebServiceHost(typeof(TestIFNSLibary.Inventarka.Inventarka));
            Servicehost = new ServiceHost(typeof(CommandDbf));
            ServiceRest.Open();
            Servicehost.Open();
            Inventarization.Open();
            var timeEvent = new TestIFNSLibary.TimeEvent.TimeEvent();
        }

        protected override void OnStop()
        {
            if (Servicehost != null)
            {
                Servicehost.Close();
                ServiceRest.Close();
                Inventarization.Close();
                Inventarization = null;
                ServiceRest = null;
                Servicehost = null;
            }
        }
    }
}
