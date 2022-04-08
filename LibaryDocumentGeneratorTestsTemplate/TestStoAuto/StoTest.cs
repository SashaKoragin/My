using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Printing;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using EfDatabase.Inventory.Base;
using EfDatabase.Inventory.BaseLogic.Select;
using EfDatabaseXsdQrCodeModel;
using EfDatabaseXsdSupportNalog;
using LibaryDocumentGenerator.Barcode;
using LibaryDocumentGenerator.Documents.Template;
using LibaryXMLAuto.ReadOrWrite;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;
using TestIFNSLibary.Inventarka;

namespace LibaryDocumentGeneratorTestsTemplate.TestStoAuto
{
    [TestClass]
   public class StoTest
    {


        [TestMethod]
        public void SelectUsers()
        {

           var Inventory = new InventoryContext();
           try
           {
               var t1 = Inventory.Users.Where(user => user.StatusUser != null)
                   .Where(u => u.StatusUser.IdStatusUser != 4).Where(u1 =>u1.Name == "Яшина Полина Денисовна").ToArray();
               TT(ref t1);

           }
           catch (Exception e)
           {
               Console.WriteLine(e);
               throw;
           }

           Inventory?.Dispose();
        }

        public void TT(ref User[] users)
        {
            foreach (var user in users)
            {
                user.Telephon.User = null;
            }
        }

        [TestMethod]
        public void TestAutoSto()
        {
            DateTime date = DateTime.Now;

                    if (date.Day == 08)
                    {
                        var description = @"Добрый день!" +
                                          "Требуется замена ТОНЕРА на МФУ Xerox VersaLink B7030 в каб.237 сер.№3399683695," +
                                          "серв.№77068-4-403-3399683695," +
                                          "инв.№22-100135(по договоренности с менеджером Денисом).";
                            var modelSto = new ModelParametrSupport()
                            {
                                Discription = description,
                                IdMfu = 50,
                                IdUser = 96,
                                Login = "7751-00-400",
                                Password = "Acr1at1ve$$",
                                IdTemplate = 7
                            };
                            Inventarka inventory = new Inventarka();
                            var models = inventory.ServiceSupport(modelSupport: modelSto);
                            if (models.Result.Step3ResponseSupport != null)
                            {
                               
                            }
                    }
        }
        [TestMethod]
        public void CreateQrCodeOffice()
        {
            Select auto = new Select();
            QrCodeOffice office = auto.SelectOffice("357", true);
            GenerateBarcode qrCode = new GenerateBarcode();
            OfficeStikerCode stickerQrOffice = new OfficeStikerCode();
            //Создание qr кодов
            foreach (var qrCodeOffice in office.Kabinet)
            {
                qrCodeOffice.FullPathPng = qrCode.GenerateQrCode("C:\\Testing\\" + qrCodeOffice.IdNumberKabinet, qrCodeOffice.NumberKabinet);
            }
            stickerQrOffice.CreateDocument("C:\\Testing\\QrCodeOffice", office);
            //Удаление всех png
            foreach (var cabinet in office.Kabinet)
            {
                File.Delete(cabinet.FullPathPng);
            }
        }
        /// <summary>
        /// Тестовый метод паспорт оборудования
        /// </summary>
        [TestMethod]
        public void TestStoPassportToModel()
        {
            var pathXlsx = "D:\\Testing\\Оборудование_в_организации.xlsx";
            DataTable dt = new DataTable();
            using (SpreadsheetDocument spreadSheetDocument = SpreadsheetDocument.Open(pathXlsx, false))
            {
                WorkbookPart workbookPart = spreadSheetDocument.WorkbookPart;
                IEnumerable<Sheet> sheets = spreadSheetDocument.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>();
                string relationshipId = sheets.First().Id.Value;
                WorksheetPart worksheetPart = (WorksheetPart)spreadSheetDocument.WorkbookPart.GetPartById(relationshipId);
                Worksheet workSheet = worksheetPart.Worksheet;
                SheetData sheetData = workSheet.GetFirstChild<SheetData>();
                IEnumerable<Row> rows = sheetData.Descendants<Row>();

                foreach (Cell cell in rows.ElementAt(0))
                {
                    dt.Columns.Add(GetCellValue(spreadSheetDocument, cell));
                }

                foreach (Row row in rows) //this will also include your header row...
                {
                    DataRow tempRow = dt.NewRow();

                    for (int i = 0; i < row.Descendants<Cell>().Count(); i++)
                    {
                        tempRow[i] = GetCellValue(spreadSheetDocument, row.Descendants<Cell>().ElementAt(i));
                    }
                    dt.Rows.Add(tempRow);
                }
            }
            dt.Rows.RemoveAt(0); //...so i'm taking it out here.


            //using (SpreadsheetDocument spreadSheetDocument = SpreadsheetDocument.Open(pathXlsx, true))
            //{

            //    WorkbookPart workbookPart = spreadSheetDocument.WorkbookPart;
            //    IEnumerable<Sheet> sheets = spreadSheetDocument.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>();
            //    string relationshipId = sheets.First().Id.Value;
            //    WorksheetPart worksheetPart = (WorksheetPart)spreadSheetDocument.WorkbookPart.GetPartById(relationshipId);

            //    spreadSheetDocument.Save();
            //    spreadSheetDocument.Close();
            //}

            // var sheetName = "Лист1";
            // XlsxToDataTable xlsxToDataTable = new XlsxToDataTable();
            // var dataTable = xlsxToDataTable.GetDateTableXslx(pathXlsx, sheetName);



            // var connectionString = string.Format($"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={pathXlsx}; Extended Properties=Excel 12.0;");
            // var adapter = new OleDbDataAdapter($"Select * From [{sheetName}$]", connectionString);
            // var ds = new DataSet();
            // adapter.Fill(ds, "STO");
            //// DataTable dataTable = ds.Tables["STO"];
        }

        /// <summary>
        /// Запрос
        /// </summary>
        private  WebClient Client { get; set; }
        /// <summary>
        /// Ответ с Support
        /// </summary>
        private HttpWebResponse Response { get; set; }
        /// <summary>
        /// Параметры
        /// </summary>
        private byte[] DatesBytes { get; set; }
        /// <summary>
        /// Куки сайта
        /// </summary>
        private CookieContainer Сookie { get; set; } = new CookieContainer();

        [TestMethod]
        public void TestPrintServer()
        {

            Select auto = new Select();
            var ping = new Ping();
            XmlReadOrWrite xml = new XmlReadOrWrite();
            var t = auto.AllFullModel();
            var printServer = new PrintServer(@"\\i7751-sys030");
            var prQueue = printServer.GetPrintQueues();
            var allSerNumber = auto.AllSerNumber();
            var listPrinters = (from printer in prQueue
                                select new Printers()
                                {
                                    Description = printer.Description,
                                    Name = printer.Name,
                                    Ip = "10.177.172." + Regex.Match(printer.QueuePort.Name, @"(\d+)(?!.*\d)").Value,
                                    HasToner = printer.HasToner
                                }
                ).ToList();
            //foreach (var fullModel in t)
            //{
            //    var findModel = Regex.Match(fullModel.NameModel, @"(\d+)(?!.*\d)").Value;
            //    foreach (var pr in listPrinters)
            //    {
            //        if (pr.Description.Contains(findModel))
            //        {
            //            pr.ModelId = fullModel.IdModel;
            //            pr.FullUrl =
            //                string.Format(
            //                    string.IsNullOrWhiteSpace(fullModel.UrlModel) ? "http://{0}" : fullModel.UrlModel,
            //                    pr.Ip);
            //            pr.IdClass = fullModel.IdClasification;

            //            PingReply pingReply = null;
            //            pingReply = ping.Send(pr.Ip);
            //            // if (pr.Ip == "10.177.172.149")
            //            //{
            //            if (pingReply.Status == IPStatus.Success)
            //            {
            //                pr.SerialNumber = StepTraining(allSerNumber, pr.FullUrl);
            //                Dispose();
            //            }
            //            else
            //            {
            //                pr.Error = "Удаленный IP не пингуется";
            //            }
            //            // }
            //        }
            //    }
            //}
            //xml.CreateXmlFile("D:\\Test.xml", listPrinters, typeof(List<Printers>));
            //auto.Dispose();
            //var mfuSerNum = new List<string>()
            //{
            //    "3389259968",
            //    "3389258155",
            //    "3389258163",
            //    "5311734336",
            //    "5311734360",
            //    "5311734220",
            //    "5311734263",
            //    "3398312405",
            //    "VNC3X07936"

            //};
            //StepTraining(mfuSerNum, "https://10.177.172.136/DevMgmt/ProductConfigDyn.xml");  // 
        }


        /// <summary>
        /// Подготовительный шаг
        /// </summary>
        private string StepTraining(List<string> listSerNumber, string url)
        {

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            Client = new WebClient();
            Client.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.45 Safari/537.36");
            Client.Headers.Add(HttpRequestHeader.Accept, "text/html,application/xhtml+xml,application/xml,text/xml,*/*;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
        
            Client.Headers.Add(HttpRequestHeader.ContentLanguage, "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7");
            Client.Headers.Add(HttpRequestHeader.Cookie, "SESSION_ID=8438; statusSelected=1");

            //Client.Headers.Add(HttpRequestHeader.Cookie, "sid=sf527d6dc-d9e44097cdac0a96f26458a22c033b65");

            //  XmlTextReader reader = new XmlTextReader(url);

            try
            {
                using (var receiveStream = Client.OpenRead(url))
                {
                    StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8 ?? throw new InvalidOperationException());
                    string data = readStream.ReadToEnd();
                }

                return null;
            }
            catch (Exception e)
            {

            }

            return null;
            //try
            //{
            //    using (var receiveStream = Client.OpenRead(url))
            //    {
            //        StreamReader readStream = new StreamReader(receiveStream);
            //        string data = readStream.ReadToEnd();

            //        foreach (var serN in listSerNumber)
            //        {
            //            if (data.Contains(serN))
            //            {
            //                inputSerNum = serN;
            //            }
            //        }
            //    }
            //    return inputSerNum;
            //}
            //catch (Exception e)
            //{
            //    return e.Message;
            //}

        }

      

        /// <summary>
        /// Dispose 
        /// </summary>
        public void Dispose()
        {
            Client.Dispose();
        }

        [TestMethod]
        public void TestPr()
        {
            string printerName = "i7751-prn130"; 
            string query = string.Format("SELECT * from Win32_Printer WHERE Name LIKE '%{0}'",  printerName);
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            ManagementObjectCollection coll = searcher.Get();
            foreach (ManagementObject printer in coll)
            {
                string portName = printer["PortName"].ToString();
                if (portName.StartsWith("IP_"))
                {
                    Console.WriteLine(string.Format("Printer IP Address: {0}", portName.Substring(3)));
                }
            }


        }

        [DllImport("iphlpapi.dll", ExactSpelling = true)]
        public static extern int SendARP(int destIp, int srcIp, [Out] byte[] pMacAddress, ref int phyAddressLen);

        [TestMethod]
        public void AllIpTelephoneAndMacAdress()
        {
            var ipModel = "10.77.67.{0}";
            var i = 2;
            var listIp = new List<IpAndMac>();
            var xmlWrite = new XmlReadOrWrite();
            while (i<255)
            {
                var mac = new IpAndMac();
                var ipAdress = string.Format(ipModel, i);
                try
                {
                    IPAddress[] address = Dns.GetHostAddresses(ipAdress);
                    byte[] ab = new byte[6];
                    int len = ab.Length;
                    SendARP(BitConverter.ToInt32(address[0].GetAddressBytes(), 0), 0, ab, ref len);
                    string[] macAddressString = new string[(int)len];
                    for (int j = 0; j < len; j++)
                    {
                        macAddressString[i] = ab[i].ToString("x2");
                    }

                    mac.Ip = ipAdress;
                    mac.Mac = string.Join(":", macAddressString);
                }
                catch (Exception exception)
                {
                    mac.Ip = ipAdress;
                    mac.Mac = "Mac не определен";
                }
                i++;
                listIp.Add(mac);
            }
            xmlWrite.CreateXmlFile("D:\\Telephone.xml", listIp, typeof(List<IpAndMac>));
        }



        public static string GetCellValue(SpreadsheetDocument document, Cell cell)
        {
            SharedStringTablePart stringTablePart =
                document.WorkbookPart.SharedStringTablePart;
            string value = cell.CellValue.InnerXml;

            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                return stringTablePart.SharedStringTable.ChildElements[Int32.Parse(value)].InnerText;
            }
            else
            {
                return value;
            }
        }

    }
    }

   public class Printers
   {
       public string Description { get; set; }
       public string Name { get; set; }
       public int ModelId { get; set; }
       public int IdClass { get; set; }
       public string FullUrl { get; set; }
       public string SerialNumber { get; set; }
       public string Ip { get; set; }
       public string Error { get; set; }
       public bool HasToner { get; set; }
   }

public class IpAndMac
{
    public string Ip { get; set; }

    public string Mac { get; set; }
}