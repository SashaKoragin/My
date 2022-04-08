using System;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using EfDatabase.Inventory.BaseLogic.ProcessSynchronization;
using EfDatabase.Inventory.BaseLogic.Select;
using EfDatabaseXsdSupportNalog;
using SqlLibaryIfns.PingIp;

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
            var selectDomainComputers = new Timer() { Interval = 60000, Enabled = true, AutoReset = true };
            selectDomainComputers.Elapsed += FindHostNameIp;
            selectDomainComputers.Start();
            var selectAllPrintServer = new Timer() { Interval = 60000, Enabled = true, AutoReset = true };
            selectAllPrintServer.Elapsed += FindAllPrintServer;
            selectAllPrintServer.Start();
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
                    var selectEvent = new Select();
                    var eventParameters = selectEvent.SelectEvent(1);
                    DateTime date = DateTime.Now;
                    if (date.Hour == eventParameters.HoursX && date.Minute == eventParameters.MinutesX)
                    {
                        try
                        {
                            var parameter = eventParameters.ParametersEvent.Split(';');
                            PingIp ping = new PingIp();
                            ping.FindIpHost(parameter[0].Trim(), parameter[1].Trim(), parameter[2].Trim());
                        }
                        catch (Exception exception)
                        {
                            Loggers.Log4NetLogger.Error(exception);
                        }
                    }
                    selectEvent.Dispose();
                }
                catch (Exception ex)
                {
                    Loggers.Log4NetLogger.Error(ex);
                }
            });
        }
        /// <summary>
        /// Автоматическая задача сбора оборудования из PrintServer
        ///  Задача поиска оборудования из PrintServer и добавление их в БД
        /// </summary>
        public async void FindAllPrintServer(object sender, EventArgs e)
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    var selectEvent = new Select();
                    var eventParameters = selectEvent.SelectEvent(5);
                    DateTime date = DateTime.Now;
                    if (date.Hour == eventParameters.HoursX && date.Minute == eventParameters.MinutesX)
                    {
                        try
                        {
                            var parameter = eventParameters.ParametersEvent.Split(';');
                            var processFindPrintServer  = new ProcessSynchronizationPrintServer(parameter[0].Trim(), parameter[1].Trim());
                            var result =  processFindPrintServer.SynchronizationPrintServerStart();
                            if (result != null)
                            {
                                var resultFilter = result.Where(r => r.StatusFindPrintServerAndSynchronization == 1).ToList();
                                var selectModel = new Select();
                                var addObjectDb = new EfDatabase.Inventory.BaseLogic.AddObjectDb.AddObjectDb();
                                var idUser = selectModel.SelectIdUser(parameter[2].Trim());
                                Inventarka.Inventarka inventory = new Inventarka.Inventarka();
                                foreach (var synchronizationPrintServer in resultFilter)
                                {
                                    var allSerialNumberModel = selectModel.TypeModelAndIdSelect(synchronizationPrintServer.SerNumberPrintServer);
                                    if ((synchronizationPrintServer.IsTonerLow &&
                                         synchronizationPrintServer.HasToner &&
                                         !synchronizationPrintServer.IsSupportApplication && allSerialNumberModel != null)
                                        ^
                                        (!synchronizationPrintServer.IsTonerLow &&
                                         !synchronizationPrintServer.HasToner &&
                                         !synchronizationPrintServer.IsSupportApplication && allSerialNumberModel != null))
                                    {
                                        try
                                        {
                                            //Создать заявку
                                            var modelSto = new ModelParametrSupport()
                                            {
                                                Discription = $"Добрый день! Требуется замена {allSerialNumberModel.TypeToner} на {allSerialNumberModel.Item} {allSerialNumberModel.NameModel} в каб. {allSerialNumberModel.NumberKabinet} сер.№ {allSerialNumberModel.SerNum}, сервис.№ {allSerialNumberModel.ServiceNum}, инв.№ {allSerialNumberModel.InventarNum} . Примечание c PrintServer к описанию : {synchronizationPrintServer.DescriptionPrinter}",
                                                IdUser = idUser,
                                                Login = parameter[2].Trim(),
                                                Password = parameter[3].Trim(),
                                                IdTemplate = (allSerialNumberModel.Item == "МФУ") ? 6 : 5
                                            };
                                            switch (allSerialNumberModel.Item)
                                            {
                                                case "МФУ":
                                                    modelSto.IdMfu = allSerialNumberModel.Id;
                                                    break;
                                                case "Принтер":
                                                    modelSto.IdPrinter = allSerialNumberModel.Id;
                                                    break;
                                            }
                                            var resultStep3 = inventory.ServiceSupport(modelSupport: modelSto);
                                            synchronizationPrintServer.IsSupportApplication = true;
                                            synchronizationPrintServer.DateCreateSupportApplication = DateTime.Now;
                                            if (resultStep3.Result.Step3ResponseSupport != null)
                                            {
                                                 Loggers.Log4NetLogger.Info(new Exception($"Создали автоматически заявку на СТП! Для оборудования {allSerialNumberModel.NameModel} сер.№ {allSerialNumberModel.SerNum}"));
                                            }
                                        }
                                        catch (Exception exception)
                                        {
                                              Loggers.Log4NetLogger.Error(exception);
                                        }
                                    }
                                    if (!synchronizationPrintServer.IsTonerLow && synchronizationPrintServer.HasToner)
                                    {
                                        //Обнулить результат
                                        synchronizationPrintServer.IsSupportApplication = false;
                                        synchronizationPrintServer.DateCreateSupportApplication = null;
                                    }
                                    addObjectDb.UpdateSynchronizationPrintServer(synchronizationPrintServer);
                                }
                                selectModel.Dispose();
                                addObjectDb.Dispose();
                            }
                        }
                        catch (Exception exception)
                        {
                            Loggers.Log4NetLogger.Error(exception);
                        }
                    }
                    selectEvent.Dispose();
                }
                catch (Exception ex)
                {
                    Loggers.Log4NetLogger.Error(ex);
                }
            });
        }
   }
}
