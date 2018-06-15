using System.ServiceProcess;

namespace TestIFNSService
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new TestService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}