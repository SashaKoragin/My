using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceProcess;
using TestIFNSLibary.Service;

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
            ServiceRest = new WebServiceHost(typeof(TestIFNSLibary.ServiceRest.ServiceRest));
            Servicehost = new ServiceHost(typeof(CommandDbf));
            ServiceRest.Open();
            Servicehost.Open();
            var timerGo = new TimerGo();
            timerGo.TimerStart();
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
