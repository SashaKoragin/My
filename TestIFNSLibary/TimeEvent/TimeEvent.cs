using System;
using System.Threading.Tasks;
using System.Timers;
using EfDatabaseXsdSupportNalog;
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
            var selectDomainComputers = new Timer()
            {
                Interval = 60000,
                Enabled = true,
                AutoReset = true
            };
            selectDomainComputers.Elapsed += FindHostNameIp;
            selectDomainComputers.Start();
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
                    Parameter parameter = new Parameter();
                    DateTime date = DateTime.Now;
                    if (date.Hour == parameter.Hours && date.Minute == parameter.Minutes)
                    {
                        try
                        {
                          PingIp ping = new PingIp();
                          ping.FindIpHost(parameter.PathDomainComputer, parameter.FindWorkStations);
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
        /// <summary>
        /// Метод создание автоматической заявки
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Аргументы</param>
        public async void CreateSto(object sender, EventArgs e)
        {
            await Task.Factory.StartNew(() =>{
                try
                {
                    Parameter parameter = new Parameter();
                    DateTime date = DateTime.Now;
                    if (date.Day == parameter.DayX)
                    {
                        try
                        {
                            var description = @"Добрый день!" +
                                             "Требуется замена ТОНЕРА на МФУ Xerox VersaLink B7030 в каб.237 сер.№3399683695," +
                                             "серв.№77068-4-403-3399683695," +
                                             "инв.№22-100135(по договоренности с менеджером Денисом).";
                            var modelSto = new ModelParametrSupport()
                            {
                                Discription = description, IdMfu = 50, IdUser = 96, Login = parameter.User,
                                Password = parameter.Password,IdTemplate = 7
                            };
                            Inventarka.Inventarka inventory = new Inventarka.Inventarka();
                            var models = inventory.ServiceSupport(modelSupport: modelSto);
                            if (models.Result.Step3ResponseSupport!=null)
                            {
                                Loggers.Log4NetLogger.Info(new Exception("Создали автоматически заявку на СТП!!"));
                            }
                        }
                        catch (Exception exception)
                        {
                            Loggers.Log4NetLogger.Error(exception);
                        }
                    }
                    else
                    {
                        Loggers.Log4NetLogger.Info(new Exception($"Создание заявки не наступило {parameter.DayX} каждого месяца!"));
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
