using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

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
