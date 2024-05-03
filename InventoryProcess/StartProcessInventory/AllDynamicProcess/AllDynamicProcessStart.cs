using EfDatabase.Inventory.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using EfDatabase.Inventory.BaseLogic.AddObjectDb;
using EfDatabase.Inventory.BaseLogic.FileServerAddFile;
using EfDatabase.Inventory.BaseLogic.ProcessSynchronization;
using EfDatabase.Inventory.BaseLogic.Select;
using EfDatabase.Inventory.ComparableSystem.StartComparable;
using InventoryProcess.StartProcessInventory.ModelParameters;
using LibraryAutoSupportSto.Aksiok.AksiokPostGetSystem;
using LibraryAutoSupportSto.InventoryKaspersky.PostServiceInv;
using LibraryAutoSupportSto.PassportSto.PassportStoPostGet;
using SqlLibaryIfns.PingIp;

namespace InventoryProcess.StartProcessInventory.AllDynamicProcess
{
   public class AllDynamicProcessStart : IDisposable
   {
        /// <summary>
        /// Логин пользователя может быть NULL
        /// </summary>
        private string LoginUser { get; set; }
        /// <summary>
        /// Пароль пользователя может быть NULL
        /// </summary>
        private string PasswordUser { get; set; }
        /// <summary>
        /// Параметры из БД
        /// </summary>
        private ModelAllParametersProcess Parameters { get; set; }

        public AllDynamicProcessStart(string loginUser, string passwordUser, List<ParameterEventProcess> listParameterEventProcesses)
        {
            LoginUser = loginUser;
            PasswordUser = passwordUser;
            if (listParameterEventProcesses == null) return;
            Parameters = (ModelAllParametersProcess)Activator.CreateInstance(typeof(ModelAllParametersProcess));
            foreach (var parameterEventProcess in listParameterEventProcesses)
            {
                try
                {
                    Parameters.GetType().GetProperty(parameterEventProcess.NameParameters)?.SetValue(Parameters, parameterEventProcess.Parameters, null);
                }
                catch (Exception e)
                {
                    Loggers.Log4NetLogger.Error(e);
                }
            }
        }
        /// <summary>
        /// Процесс загрузки Ip Адресов всех компьютеров в НО индекс 1
        /// </summary>
        public void ProcessStartFindAllWorkStationIp()
        {
            PingIp ping = new PingIp();
            ping.FindIpHost(Parameters.Domain, Parameters.DomainIfnsWorkStation, Parameters.DomainIfnsWorkStationFilter);
            Dispose();
        }
      

        /// <summary>
        /// Процесс загрузки данных из ЭПО индекс 4
        /// </summary>
        public void ProcessStartPassportSto()
        {
            var addObjectDb = new AddObjectDb();
            var sto = new PassportStoPostGet(Parameters.LoginSto, Parameters.PasswordSto, Parameters.SaveReport);
            var fullPathXlsx = sto.DownloadReportSto();
            addObjectDb.CreateAndDownloadSto(fullPathXlsx, Parameters.SaveReport, Parameters.XsdReport, Parameters.BulkCopyXmlSto);
            addObjectDb.Dispose();
            Dispose();
        }
        /// <summary>
        /// Процесс мониторинга PrintServer для создание заявки на тонер картридж  индекс 5
        /// </summary>
        public void ProcessStartMonitoringPrinterToner()
        {
            var processFindPrintServer = new ProcessSynchronizationPrintServer(Parameters.PrintServer, Parameters.IpAdressAllPrinter);
            var result = processFindPrintServer.SynchronizationPrintServerStart();
            if (result != null)
            {
                var resultFilter = result.Where(r => r.StatusFindPrintServerAndSynchronization == 1).ToList();
                var selectModel = new Select();
                var addObjectDb = new AddObjectDb();
                var idUser = selectModel.SelectIdUser(Parameters.LoginUserAdminPrintServer);
                //LoadModelSupport loadModelSupport = new LoadModelSupport(Parameters.PathDomainGroup, Parameters.SaveReport);
                foreach (var synchronizationPrintServer in resultFilter)
                {
                    var allSerialNumberModel =
                        selectModel.TypeModelAndIdSelect(synchronizationPrintServer.SerNumberPrintServer);
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
                            //var modelSto = new ModelParametrSupport()
                            //{
                            //    Discription =
                            //        $"Добрый день! Требуется замена {allSerialNumberModel.TypeToner} на {allSerialNumberModel.Item} {allSerialNumberModel.NameModel} в каб. {allSerialNumberModel.NumberKabinet} сер.№ {allSerialNumberModel.SerNum}, сервис.№ {allSerialNumberModel.ServiceNum}, инв.№ {allSerialNumberModel.InventarNum} . Примечание c PrintServer к описанию : {synchronizationPrintServer.DescriptionPrinter}",
                            //    IdUser = idUser,
                            //    Login = Parameters.LoginUserAdminPrintServer,
                            //    Password = Parameters.PasswordUserAdminPrintServer,
                            //    IdTemplate = (allSerialNumberModel.Item == "МФУ") ? 6 : 5
                            //};
                            //switch (allSerialNumberModel.Item)
                            //{
                            //    case "МФУ":
                            //        modelSto.IdMfu = allSerialNumberModel.Id;
                            //        break;
                            //    case "Принтер":
                            //        modelSto.IdPrinter = allSerialNumberModel.Id;
                            //        break;
                            //}

                            //var resultStep3 = loadModelSupport.CreateSupportModelSto(modelSto);
                            synchronizationPrintServer.IsSupportApplication = true;
                            synchronizationPrintServer.DateCreateSupportApplication = DateTime.Now;
                            //if (resultStep3.Step3ResponseSupport != null)
                            //{
                            //    Loggers.Log4NetLogger.Info(new Exception($"Создали автоматически заявку на СТП! Для оборудования {allSerialNumberModel.NameModel} сер.№ {allSerialNumberModel.SerNum}"));
                            //}
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
                Dispose();
            }
        }
        /// <summary>
        /// Запуск процесса загрузки данных в Инвентаризацию индекс 6
        /// </summary>
        public void ProcessStartExportDataAksiokToInventory()
        {
            var aksiok = new AksiokPostGetSystem(LoginUser, PasswordUser);
            aksiok.StartUpdateAksiok();
            Dispose();
        }

        /// <summary>
        /// Запуск процесса сбора данных из систем Lotus, DKS, AD индекс 7
        /// </summary>
        public void ProcessStartComparable()
        {
            var startComparable = new StartComparable(Parameters.ConnectImns51, Parameters.DomainIfnsAll, Parameters.LotusAllUserIMNS, Parameters.Domain);
            startComparable.StartFullModelComparable();
            Dispose();
        }
        /// <summary>
        /// Запуск процесса сбора файлов с файлового сервера индекс 8
        /// </summary>
        public void ProcessStartFindAllFileServer()
        {
            var processFindAllFileServers = new ProcessFindAllFileServers(Parameters.InXmlFile,Parameters.OutXmlFile,Parameters.ErrorXmlFile, Convert.ToInt32(Parameters.CountFilePack), Convert.ToInt32(Parameters.CountParallelProcess));
            processFindAllFileServers.ProcessAllFileServer(Parameters.NameFileServer);
            Dispose();
        }
        /// <summary>
        /// Процесс загрузки данных инвентаризации с касперского
        /// </summary>
        public void ProcessStartServiceInventory()
        {
            var processStartServiceKaspersky = new PostServiceInv(Parameters.ServiceInvKaspersky, Parameters.ParameterKasperskyModel, Parameters.ServiceInvKasperskyAllHostDrivers);
            processStartServiceKaspersky.StartKasperskyToInventory();
            Dispose();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
            var order = 0;
            string[] sizes = {"B", "KB", "MB", "GB", "TB"};
            var sizeFiles = GC.GetTotalMemory(false);
            while (sizeFiles >= 1024 && order < sizes.Length - 1)
            {
                order++;
                sizeFiles /= 1024;
            }
            Loggers.Log4NetLogger.Info(new Exception($"Разрушили объект и освободили память процесса! Memory: {sizeFiles:0.##} {sizes[order]}"));
        }

        ~AllDynamicProcessStart()
        {
            
        }
   }
}
