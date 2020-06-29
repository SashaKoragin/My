using System;
using System.Threading.Tasks;
using System.Timers;
using SqlLibaryIfns.PingIp;
using TestIFNSLibary.PathJurnalAndUse;

namespace TestIFNSLibary.TimeEvent
{
    /// <summary>
    /// Класс для автоматических заданий
    /// </summary>
   public class TimeEvent
    {
        /// <summary>
        /// Конструктор класса
        /// </summary>
        public TimeEvent()
        {
           var selectdomaincomputers = new Timer()
            {
                Interval = 60000,
                Enabled = true,
                AutoReset = true
            };
            selectdomaincomputers.Elapsed += FindHostNameIp;
            selectdomaincomputers.Start();
        }
        /// <summary>
        /// Автоматическая задача сбора Ip Адресов пляшем от домена поиск и ping
        ///  Задача поиска компьютеров и добавление их в БД
        /// </summary>
        public async void FindHostNameIp(object sender, EventArgs e)
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    Parametr parametr = new Parametr();
                    DateTime date = DateTime.Now;
                    if (date.Hour == parametr.Hours && date.Minute == parametr.Minutes)
                    {
                        try
                        {
                          PingIp ping = new PingIp();
                          ping.FindIpHost(parametr.PathDomainComputer, parametr.FindWorkStations);
                        }
                        catch (Exception exception)
                        {
                            Loggers.Log4NetLogger.Error(exception);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Loggers.Log4NetLogger.Error(ex);
                }
            });
        }
    }
}
