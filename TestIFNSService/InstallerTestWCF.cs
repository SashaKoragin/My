using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace TestIFNSService
{
    [RunInstaller(true)]
    public partial class InstallerTestWcf : Installer
    {
        public InstallerTestWcf()
        {
            var process = new ServiceProcessInstaller();
            process.Account = ServiceAccount.LocalSystem;
            var service = new ServiceInstaller();
            service.ServiceName = "ServiceTestIFNS";
            service.Description = "Служба для TestIfns!!!";
            Installers.Add(process);
            Installers.Add(service);
        }
    }
}
