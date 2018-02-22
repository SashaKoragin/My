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

        protected override void OnStart(string[] args)
        {
            if (Servicehost != null)
            {
                Servicehost.Close();
            }
            Servicehost = new ServiceHost(typeof(TestIFNSLibary.CommandDbf));
            Servicehost.Open();
        }

        protected override void OnStop()
        {
            if (Servicehost != null)
            {
                Servicehost.Close();
                Servicehost = null;
            }
        }
    }
}
