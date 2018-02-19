using System.ServiceModel;
using System.ServiceProcess;

namespace TestIFNSService
{
    public partial class TestService : ServiceBase
    {
        public TestService()
        {
            InitializeComponent();
        }

        public ServiceHost Servicehost;
        public ServiceHost Servicehost1;

        protected override void OnStart(string[] args)
        {
            if (Servicehost != null)
            {
                Servicehost.Close();
                Servicehost1.Close();
            }
            Servicehost = new ServiceHost(typeof(TestIFNSLibary.CommandDbf));
            Servicehost1 = new ServiceHost(typeof(TestIFNSLibary.WebSevice.Service));
            Servicehost1.Open();
            Servicehost.Open();
        }

        protected override void OnStop()
        {
            if (Servicehost != null)
            {
                Servicehost.Close();
                Servicehost1.Close();
                Servicehost = null;
                Servicehost1 = null;
            }
        }
    }
}
