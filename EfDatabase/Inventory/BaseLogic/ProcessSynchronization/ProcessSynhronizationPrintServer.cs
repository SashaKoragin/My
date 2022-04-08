using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Printing;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EfDatabase.Inventory.Base;

namespace EfDatabase.Inventory.BaseLogic.ProcessSynchronization
{
   public class ProcessSynchronizationPrintServer : IDisposable
   {
       private int IndexProcess { get; } = 5;
        /// <summary>
        /// Клиент
        /// </summary>
        private WebClient Client { get; set; }
        /// <summary>
        /// Адрес PrintServer
        /// </summary>
        private string PrintServer { get; }
        /// <summary>
        /// Под сеть  PrintServer
        /// </summary>
        private string UnderTheNetwork { get; }

        public ProcessSynchronizationPrintServer(string printServer, string underTheNetwork)
        {
            PrintServer = printServer;
            UnderTheNetwork = underTheNetwork;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
        }
        /// <summary>
        /// Процесс загрузки данных с PrintServer инспекции
        /// </summary>
        public List<SynchronizationPrintServer> SynchronizationPrintServerStart()
        {
            try
            {
                var selectProcess = new Select.Select();
                var process = selectProcess.SelectProcess(IndexProcess);
                if (process.IsComplete != null && (bool)process.IsComplete)
                {
                    var addObjectDb = new AddObjectDb.AddObjectDb();
                    addObjectDb.IsProcessComplete(IndexProcess, false);
                    var task = Task.Factory.StartNew(() =>
                   {
                       try
                       {
                           var ping = new Ping();
                           var printServer = new PrintServer(PrintServer);
                           var selectModel = new Select.Select();
                           var allModel = selectModel.AllFullModel();
                           var allSerialNumberModel = selectModel.AllSerNumber();
                           var prQueue = printServer.GetPrintQueues();
                           var listPrinters = (from printer in prQueue
                                               select new SynchronizationPrintServer()
                                               {
                                                   DescriptionPrinter = printer.Comment,
                                                   NamePrintServer = printer.Name,
                                                   IpPrintServer = UnderTheNetwork + Regex.Match(printer.QueuePort.Name, @"(\d+)(?!.*\d)").Value,
                                                   HasToner = printer.HasToner,
                                                   IsTonerLow = printer.IsTonerLow
                                               }
                               ).ToList();
                           foreach (var model in allModel)
                           {
                               var findModelName = Regex.Match(model.NameModel, @"(\d+)(?!.*\d)").Value;
                               if (!string.IsNullOrWhiteSpace(findModelName))
                               {
                                   foreach (var printer in listPrinters)
                                   {
                                       if (printer.DescriptionPrinter.Contains(findModelName))
                                       {
                                           printer.FullUrl = string.Format(string.IsNullOrWhiteSpace(model.UrlModel) ? "http://{0}" : model.UrlModel, printer.IpPrintServer);
                                           PingReply pingReply = ping.Send(printer.IpPrintServer);
                                           if (pingReply != null && pingReply.Status == IPStatus.Success)
                                           {
                                               if (findModelName == "7030")
                                               {
                                                   SoapClientPrinter(allSerialNumberModel, printer);
                                                   Dispose();
                                               }
                                               else
                                               {
                                                   ClientSendWebForm(allSerialNumberModel, printer);
                                                   Dispose();
                                               }
                                           }
                                           else
                                           {
                                               printer.IsErrorInfo = "Удаленный IP не пингуется!";
                                               printer.StatusFindPrintServerAndSynchronization = 4;
                                           }
                                       }
                                   }
                               }
                           }
                           addObjectDb.AddListSynchronizationPrintServer(ref listPrinters);
                           selectModel.Dispose();
                           return listPrinters;
                       }
                       catch (Exception e)
                       {
                           Loggers.Log4NetLogger.Error(e);
                       }
                       return null;
                   }, TaskCreationOptions.LongRunning);
                    task.ConfigureAwait(true).GetAwaiter().OnCompleted(() =>
                    {
                        addObjectDb.IsProcessComplete(IndexProcess, true);
                        addObjectDb.Dispose();
                    });
                    return task.Result;
                }
                selectProcess.Dispose();
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
            return null;
        }
        /// <summary>
        /// Запрос на Web интерфейс устройства!
        /// </summary>
        /// <param name="listSerNumber">Список серийных номеров для поиска</param>
        /// <param name="printer">Ссылка на объект</param>
        /// <returns></returns>
        private void ClientSendWebForm(List<string> listSerNumber, SynchronizationPrintServer printer)
        {
            Client = new WebClient();
            Client.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.45 Safari/537.36");
            Client.Headers.Add(HttpRequestHeader.Accept, "text/html,application/xhtml+xml,application/xml,text/xml,*/*;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
            Client.Headers.Add(HttpRequestHeader.ContentLanguage, "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7");
            Client.Headers.Add(HttpRequestHeader.Cookie, "SESSION_ID=8438; statusSelected=1");
            try
            {
                using (var receiveStream = Client.OpenRead(printer.FullUrl))
                {
                    StreamReader readStream = new StreamReader(receiveStream ?? throw new InvalidOperationException());
                    string data = readStream.ReadToEnd();
                    foreach (var serN in listSerNumber)
                    {
                        if (data.Contains(serN))
                        {
                            printer.SerNumberPrintServer = serN;
                            printer.StatusFindPrintServerAndSynchronization = 1;
                            printer.IsErrorInfo = null;
                            break;
                        }
                        printer.IsErrorInfo = "Серийный номер по ссылке не найден!";
                        printer.StatusFindPrintServerAndSynchronization = 2;
                    }
                }
            }
            catch (Exception e)
            {
                printer.IsErrorInfo = e.Message;
                printer.StatusFindPrintServerAndSynchronization = 4;
            }
        }
        /// <summary>
        /// Запрос на Web интерфейс VersaLink B7030 устройства!
        /// </summary>
        /// <param name="listSerNumber">Список серийных номеров для поиска</param>
        /// <param name="printer">Ссылка на объект</param>
        /// <returns></returns>
        private void SoapClientPrinter(List<string> listSerNumber, SynchronizationPrintServer printer)
        {
            var parametersSoap = @"<soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/""><soap:Header><msg:MessageInformation xmlns:msg=""http://www.fujixerox.co.jp/2014/08/ssm/management/message""><msg:MessageExchangeType>RequestResponse</msg:MessageExchangeType><msg:MessageType>Request</msg:MessageType><msg:Action>http://www.fujixerox.co.jp/2003/12/ssm/management/statusConfig#GetAttribute</msg:Action><msg:From><msg:Address>http://www.fujixerox.co.jp/2014/08/ssm/management/soap/epr/client</msg:Address><msg:ReferenceParameters/></msg:From></msg:MessageInformation></soap:Header><soap:Body><cfg:GetAttribute xmlns:cfg=""http://www.fujixerox.co.jp/2003/12/ssm/management/statusConfig""><cfg:Object name=""urn:fujixerox:names:ssm:1.0:management:productInformation"" offset=""0""/><cfg:Object name=""urn:fujixerox:names:ssm:1.0:management:WirelessLANStatus"" offset=""0""/><cfg:Object name=""urn:fujixerox:names:ssm:1.0:management:OperationStatus"" offset=""0""/><cfg:Object name=""urn:fujixerox:names:ssm:1.0:management:ProductName"" offset=""0""/></cfg:GetAttribute></soap:Body></soap:Envelope>";
            Client = new WebClient();
            Client.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.45 Safari/537.36");
            Client.Headers.Add(HttpRequestHeader.Accept, "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
            Client.Headers.Add(HttpRequestHeader.ContentLanguage, "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7");
            Client.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
            Client.Headers.Add(HttpRequestHeader.ContentType, "text/xml; charset=UTF-8");
            Client.Headers.Add("SOAPAction", "http://www.fujixerox.co.jp/2003/12/ssm/management/statusConfig#GetAttribute");
            Client.Headers.Add(HttpRequestHeader.Cookie, "SESSION_ID=8438; statusSelected=1");
            try
            {
                var data = Client.UploadString(printer.FullUrl, "POST", parametersSoap);
                foreach (var serN in listSerNumber)
                {
                    if (data.Contains(serN))
                    {
                        printer.SerNumberPrintServer = serN;
                        printer.StatusFindPrintServerAndSynchronization = 1;
                        printer.IsErrorInfo = null;
                        break;
                    }
                    printer.IsErrorInfo = "Серийный номер по ссылке не найден!";
                    printer.StatusFindPrintServerAndSynchronization = 2;
                }
            }
            catch (Exception e)
            {
                printer.IsErrorInfo = e.Message;
                printer.StatusFindPrintServerAndSynchronization = 4;
            }
        }


        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Client.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
   }
}
