using System;
using System.IO;
using System.Net;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EfDatabase.Inventory.BaseLogic.AksiokAddAndUpdateObjectDb;
using EfDatabase.Inventory.ReportXml.ModelAksiok;
using EfDatabase.ModelAksiok.Aksiok;
using LibraryAutoSupportSto.Aksiok.AksiokPostGetSystem;
using Newtonsoft.Json;


namespace LibaryDocumentGeneratorTestsTemplate.Aksiok
{
    [TestClass()]
   public class TestAksiokToModel
   {

        public HttpWebRequest Request { get; set; }

        /// <summary>
        /// Ответ с Support
        /// </summary>
        private HttpWebResponse Response { get; set; }

        /// <summary>
        /// Параметры файла
        /// </summary>
        private byte[] DatesBytesFile { get; set; }

        [TestMethod()]
        public void TestDeserializationAksiok()
        {
            var ac = new AksiokAddAndUpdateObjectDb();
            DataAksiokFullSchemes<EfDatabase.ModelAksiok.Aksiok.EquipmentType[]> allType = null;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            ServicePointManager.ServerCertificateValidationCallback = (senders, certificate, chain, sslPolicyErrors) =>
            {
                return true;
            };
            CredentialCache myCache = new CredentialCache();
            myCache.Add(new Uri("https://aksiok.dpc.tax.nalog.ru"), "Negotiate", new NetworkCredential("7751-00-099", "Qwerty12345678!"));
            var cookies = new CookieContainer();
            cookies.Add(new Cookie(".ASPXAUTH", "585B2C7DA8479B5A1A355A9F3CED47C3BDE9539CD6924311DDAAC368C951513252E0F2C706D11BF4E3FE31B2869A374962E555235370CED6DD63BD067095D3CA6CCF470768E7C61FB6C796A080FDE124CBB359D96133240B1983198864BE9836763DD7C13122BDE0703A5A760F3ACAD8FF62F43EF367170C1617A3DC956EBA471A7F81BC7F76C5B4C9B643C4AF5559F7B81100F25E81BB997A02BC99985B2F57D45A6F4FBAAD9B43D9A0EC7CDCB6C96C2AC2FB6C677AB2DAB99DC56CDF7D0CD9398671643E630F126A26230990F07DEA19DB6FC52AE3C450598FB3BAF75C3C9CD18B6E3AF61482DE5E4006BB3BDC5E909B1945D775E57D10837CBF2138E7F742255B522AEFBB296ABA7553CC5FC89AA0D97951003D431095E4D10B7C9F9BAF761D62E45475E9A99394FE4D3789A7D88E5429B1280A5EA9859C0446A1C6E74FFA6282CF4309A11F30DF0320343F40BC0D97F640E635637A8618E7EF35B8E43774C1305E4B3BC5FA7DA6A237F19452082C95229D90566C2BE802ACEF1EDB8EFBC199EC9D55C9F7C28064CFAB322930BE5454B12B99E430BF7E5B37F3678D5720AD", "/", "dpc.tax.nalog.ru"));

            //Справочник производитель
            DatesBytesFile = Encoding.UTF8.GetBytes("{\"records\":[],\"actualDate\":\"2022-04-19T00: 00:00\",\"page\":1,\"start\":0,\"limit\":25,\"sort\":[{\"property\":\"Code\",\"direction\":\"ASC\"}]}");
            Request = (HttpWebRequest)WebRequest.Create("https://aksiok.dpc.tax.nalog.ru/api/Producer/List?_dc=1650353905544");
            Request.Accept = "*/*";
            Request.KeepAlive = true;
            Request.Credentials = myCache;
            Request.CookieContainer = cookies;
            Request.Host = "aksiok.dpc.tax.nalog.ru";
            Request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.45 Safari/537.36";
            Request.ContentType = "application/json";
            Request.Headers.Add("Accept-Encoding", "gzip, deflate, br");
            Request.Headers.Add("Accept-Language", "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7");
            Request.Headers.Add("X-Requested-With", "XMLHttpRequest");
            Request.Headers.Add("Origin", "https://aksiok.dpc.tax.nalog.ru/");
            Request.Headers.Add("sec-ch-ua", "Not A;Brand\";v=\"99\", \"Chromium\"; v=\"96\", \"Google Chrome\"; v=\"96\"");
            Request.Headers.Add("sec-ch-ua-mobile", "?0");
            Request.Headers.Add("sec-ch-ua-platform", "Windows");
            Request.Headers.Add("Sec-Fetch-Dest", "empty");
            Request.Headers.Add("Sec-Fetch-Mode", "cors");
            Request.Headers.Add("Sec-Fetch-Site", "same-origin");
            Request.Method = "POST";
            Request.ContentLength = DatesBytesFile.Length;
            using (var stream = Request.GetRequestStream())
            {
                stream.Write(DatesBytesFile, 0, DatesBytesFile.Length);
            }
            Response = (HttpWebResponse)Request.GetResponse();
            cookies.Add(Response.Cookies);
            if (Response.StatusCode == HttpStatusCode.OK)
            {
                using (var receiveStream = Response.GetResponseStream())
                {
                    StreamReader readStream;
                    readStream = String.IsNullOrWhiteSpace(Response.CharacterSet)
                        ? new StreamReader(receiveStream)
                        : new StreamReader(receiveStream, Encoding.GetEncoding(Response.CharacterSet));
                    string data = readStream.ReadToEnd();
                    var dataModelServerAksiok = Newtonsoft.Json.JsonConvert.DeserializeObject<DataAksiokFullSchemes<EfDatabase.ModelAksiok.Aksiok.Producer[]>>(data);

                    ac.AddAndUpdateFullLoadAksiok(dataModelServerAksiok.Data, "Producer");
                }
            }
            //Справочник типы оборудования
            DatesBytesFile = Encoding.UTF8.GetBytes("{\"records\":[],\"actualDate\":\"2022-04-19T00: 00:00\",\"page\":1,\"start\":0,\"limit\":25,\"sort\":[{\"property\":\"Code\",\"direction\":\"ASC\"}]}");
            Request = (HttpWebRequest)WebRequest.Create("https://aksiok.dpc.tax.nalog.ru/api/EquipmentType/list?_dc=1650354780285");
            Request.Accept = "*/*";
            Request.KeepAlive = true;
            Request.Credentials = myCache;
            Request.CookieContainer = cookies;
            Request.Host = "aksiok.dpc.tax.nalog.ru";
            Request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.45 Safari/537.36";
            Request.ContentType = "application/json";
            Request.Headers.Add("Accept-Encoding", "gzip, deflate, br");
            Request.Headers.Add("Accept-Language", "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7");
            Request.Headers.Add("X-Requested-With", "XMLHttpRequest");
            Request.Headers.Add("Origin", "https://aksiok.dpc.tax.nalog.ru/");
            Request.Headers.Add("sec-ch-ua", "Not A;Brand\";v=\"99\", \"Chromium\"; v=\"96\", \"Google Chrome\"; v=\"96\"");
            Request.Headers.Add("sec-ch-ua-mobile", "?0");
            Request.Headers.Add("sec-ch-ua-platform", "Windows");
            Request.Headers.Add("Sec-Fetch-Dest", "empty");
            Request.Headers.Add("Sec-Fetch-Mode", "cors");
            Request.Headers.Add("Sec-Fetch-Site", "same-origin");
            Request.Method = "POST";
            Request.ContentLength = DatesBytesFile.Length;
            using (var stream = Request.GetRequestStream())
            {
                stream.Write(DatesBytesFile, 0, DatesBytesFile.Length);
            }
            Response = (HttpWebResponse)Request.GetResponse();
            cookies.Add(Response.Cookies);
            if (Response.StatusCode == HttpStatusCode.OK)
            {
                using (var receiveStream = Response.GetResponseStream())
                {
                    StreamReader readStream;
                    readStream = String.IsNullOrWhiteSpace(Response.CharacterSet)
                        ? new StreamReader(receiveStream)
                        : new StreamReader(receiveStream, Encoding.GetEncoding(Response.CharacterSet));
                    string data = readStream.ReadToEnd();
                    allType = Newtonsoft.Json.JsonConvert.DeserializeObject<DataAksiokFullSchemes<EfDatabase.ModelAksiok.Aksiok.EquipmentType[]>>(data);
                    ac.AddAndUpdateFullLoadAksiok(allType.Data, "EquipmentType");
                }
            }

            DataAksiokFullSchemes<EfDatabase.ModelAksiok.Aksiok.Producer[]> product = null;
            //Справочник модель оборудования
            foreach (var type in allType.Data)
            {
                //Сначала запрос типов (Продуктов)
                DatesBytesFile = Encoding.UTF8.GetBytes("{\"records\":[],\"node\":" + type.Id + ",\"nodeLevel\":0,\"rootNode\":null,\"actualDate\":\"2022-04-19T00: 00:00\",\"page\":1,\"start\":0,\"limit\":25,\"id\":" + type.Id + "}");
                Request = (HttpWebRequest)WebRequest.Create("https://aksiok.dpc.tax.nalog.ru/api/EquipmentModel/ListTree?_dc=1650356158346 ");
                Request.Accept = "*/*";
                Request.KeepAlive = true;
                Request.Credentials = myCache;
                Request.CookieContainer = cookies;
                Request.Host = "aksiok.dpc.tax.nalog.ru";
                Request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.45 Safari/537.36";
                Request.ContentType = "application/json";
                Request.Headers.Add("Accept-Encoding", "gzip, deflate, br");
                Request.Headers.Add("Accept-Language", "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7");
                Request.Headers.Add("X-Requested-With", "XMLHttpRequest");
                Request.Headers.Add("Origin", "https://aksiok.dpc.tax.nalog.ru/");
                Request.Headers.Add("sec-ch-ua", "Not A;Brand\";v=\"99\", \"Chromium\"; v=\"96\", \"Google Chrome\"; v=\"96\"");
                Request.Headers.Add("sec-ch-ua-mobile", "?0");
                Request.Headers.Add("sec-ch-ua-platform", "Windows");
                Request.Headers.Add("Sec-Fetch-Dest", "empty");
                Request.Headers.Add("Sec-Fetch-Mode", "cors");
                Request.Headers.Add("Sec-Fetch-Site", "same-origin");
                Request.Method = "POST";
                Request.ContentLength = DatesBytesFile.Length;
                using (var stream = Request.GetRequestStream())
                {
                    stream.Write(DatesBytesFile, 0, DatesBytesFile.Length);
                }
                Response = (HttpWebResponse)Request.GetResponse();
                cookies.Add(Response.Cookies);
                if (Response.StatusCode == HttpStatusCode.OK)
                {
                    using (var receiveStream = Response.GetResponseStream())
                    {
                        StreamReader readStream;
                        readStream = String.IsNullOrWhiteSpace(Response.CharacterSet)
                            ? new StreamReader(receiveStream)
                            : new StreamReader(receiveStream, Encoding.GetEncoding(Response.CharacterSet));
                        string data = readStream.ReadToEnd();
                        product = Newtonsoft.Json.JsonConvert.DeserializeObject<DataAksiokFullSchemes<EfDatabase.ModelAksiok.Aksiok.Producer[]>>(data);
                    }
                }

                foreach (var p in product.Data)
                {
                    //Сначала запрос типов (Продуктов)
                    DatesBytesFile = Encoding.UTF8.GetBytes("{\"records\":[],\"node\":" + p.Id + ",\"nodeLevel\":1,\"rootNode\":" + type.Id + ",\"actualDate\":\"2022-04-19T00:00:00\",\"page\":1,\"start\":0,\"limit\":25,\"id\":" + p.Id + "}");
                    Request = (HttpWebRequest)WebRequest.Create("https://aksiok.dpc.tax.nalog.ru/api/EquipmentModel/ListTree?_dc=1650356158346 ");
                    Request.Accept = "*/*";
                    Request.KeepAlive = true;
                    Request.Credentials = myCache;
                    Request.CookieContainer = cookies;
                    Request.Host = "aksiok.dpc.tax.nalog.ru";
                    Request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.45 Safari/537.36";
                    Request.ContentType = "application/json";
                    Request.Headers.Add("Accept-Encoding", "gzip, deflate, br");
                    Request.Headers.Add("Accept-Language", "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7");
                    Request.Headers.Add("X-Requested-With", "XMLHttpRequest");
                    Request.Headers.Add("Origin", "https://aksiok.dpc.tax.nalog.ru/");
                    Request.Headers.Add("sec-ch-ua", "Not A;Brand\";v=\"99\", \"Chromium\"; v=\"96\", \"Google Chrome\"; v=\"96\"");
                    Request.Headers.Add("sec-ch-ua-mobile", "?0");
                    Request.Headers.Add("sec-ch-ua-platform", "Windows");
                    Request.Headers.Add("Sec-Fetch-Dest", "empty");
                    Request.Headers.Add("Sec-Fetch-Mode", "cors");
                    Request.Headers.Add("Sec-Fetch-Site", "same-origin");
                    Request.Method = "POST";
                    Request.ContentLength = DatesBytesFile.Length;
                    using (var stream = Request.GetRequestStream())
                    {
                        stream.Write(DatesBytesFile, 0, DatesBytesFile.Length);
                    }
                    Response = (HttpWebResponse)Request.GetResponse();
                    cookies.Add(Response.Cookies);
                    if (Response.StatusCode == HttpStatusCode.OK)
                    {
                        using (var receiveStream = Response.GetResponseStream())
                        {
                            StreamReader readStream;
                            readStream = String.IsNullOrWhiteSpace(Response.CharacterSet)
                                ? new StreamReader(receiveStream)
                                : new StreamReader(receiveStream, Encoding.GetEncoding(Response.CharacterSet));
                            string data = readStream.ReadToEnd();
                            var dataModelServerAksiok = Newtonsoft.Json.JsonConvert.DeserializeObject<DataAksiokFullSchemes<EfDatabase.ModelAksiok.Aksiok.EquipmentModel[]>>(data);
                            ac.AddAndUpdateFullLoadAksiok(dataModelServerAksiok.Data, "EquipmentModel", type.Id, p.Id);
                        }
                    }
                }
            }




            DataAksiokFullSchemes<EfDatabase.ModelAksiok.Aksiok.ModelDocumentType[]> modelDocumentType = null;
            DataAksiokFullSchemes<EfDatabase.ModelAksiok.Aksiok.ModelDocument[]> modelDocument = null;
            DatesBytesFile = Encoding.UTF8.GetBytes("{\"records\":[],\"page\":1,\"start\":0,\"limit\":1000}");
            Request = (HttpWebRequest)WebRequest.Create("https://aksiok.dpc.tax.nalog.ru/api/EpoRegistry/List?_dc=1649674857153");
            Request.Accept = "*/*";
            Request.KeepAlive = true;
            Request.Credentials = myCache;
            Request.CookieContainer = cookies;
            Request.Host = "aksiok.dpc.tax.nalog.ru";
            Request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.45 Safari/537.36";
            Request.ContentType = "application/json";
            Request.Headers.Add("Accept-Encoding", "gzip, deflate, br");
            Request.Headers.Add("Accept-Language", "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7");
            Request.Headers.Add("X-Requested-With", "XMLHttpRequest");
            Request.Headers.Add("Origin", "https://aksiok.dpc.tax.nalog.ru/");
            Request.Headers.Add("sec-ch-ua", "Not A;Brand\";v=\"99\", \"Chromium\"; v=\"96\", \"Google Chrome\"; v=\"96\"");
            Request.Headers.Add("sec-ch-ua-mobile", "?0");
            Request.Headers.Add("sec-ch-ua-platform", "Windows");
            Request.Headers.Add("Sec-Fetch-Dest", "empty");
            Request.Headers.Add("Sec-Fetch-Mode", "cors");
            Request.Headers.Add("Sec-Fetch-Site", "same-origin");
            Request.Method = "POST";
            Request.ContentLength = DatesBytesFile.Length;
            using (var stream = Request.GetRequestStream())
            {
                stream.Write(DatesBytesFile, 0, DatesBytesFile.Length);
            }
            Response = (HttpWebResponse)Request.GetResponse();
            cookies.Add(Response.Cookies);
            if (Response.StatusCode == HttpStatusCode.OK)
            {
                using (var receiveStream = Response.GetResponseStream())
                {
                    StreamReader readStream;
                    readStream = String.IsNullOrWhiteSpace(Response.CharacterSet)
                        ? new StreamReader(receiveStream)
                        : new StreamReader(receiveStream, Encoding.GetEncoding(Response.CharacterSet));
                    string data = readStream.ReadToEnd();
                    modelDocumentType = Newtonsoft.Json.JsonConvert.DeserializeObject<DataAksiokFullSchemes<EfDatabase.ModelAksiok.Aksiok.ModelDocumentType[]>>(data);
                    ac.AddAndUpdateFullLoadAksiok(modelDocumentType.Data, "ModelDocumentType");
                }
            }


            foreach (var p in modelDocumentType.Data)
            {
                DatesBytesFile = Encoding.UTF8.GetBytes("{\"records\":[],\"epoDocumentId\":" + p.Id + ",\"page\":1,\"start\":0,\"limit\":500}");
                Request = (HttpWebRequest)WebRequest.Create("https://aksiok.dpc.tax.nalog.ru/api/EquipmentCard/ListEquipments?_dc=1649676212262");
                Request.Accept = "*/*";
                Request.KeepAlive = true;
                Request.Credentials = myCache;
                Request.CookieContainer = cookies;
                Request.Host = "aksiok.dpc.tax.nalog.ru";
                Request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.45 Safari/537.36";
                Request.ContentType = "application/json";
                Request.Headers.Add("Accept-Encoding", "gzip, deflate, br");
                Request.Headers.Add("Accept-Language", "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7");
                Request.Headers.Add("X-Requested-With", "XMLHttpRequest");
                Request.Headers.Add("Origin", "https://aksiok.dpc.tax.nalog.ru/");
                Request.Headers.Add("sec-ch-ua", "Not A;Brand\";v=\"99\", \"Chromium\"; v=\"96\", \"Google Chrome\"; v=\"96\"");
                Request.Headers.Add("sec-ch-ua-mobile", "?0");
                Request.Headers.Add("sec-ch-ua-platform", "Windows");
                Request.Headers.Add("Sec-Fetch-Dest", "empty");
                Request.Headers.Add("Sec-Fetch-Mode", "cors");
                Request.Headers.Add("Sec-Fetch-Site", "same-origin");
                Request.Method = "POST";
                Request.ContentLength = DatesBytesFile.Length;
                using (var stream = Request.GetRequestStream())
                {
                    stream.Write(DatesBytesFile, 0, DatesBytesFile.Length);
                }
                Response = (HttpWebResponse)Request.GetResponse();
                cookies.Add(Response.Cookies);
                if (Response.StatusCode == HttpStatusCode.OK)
                {
                    using (var receiveStream = Response.GetResponseStream())
                    {
                        StreamReader readStream;
                        readStream = String.IsNullOrWhiteSpace(Response.CharacterSet)
                            ? new StreamReader(receiveStream)
                            : new StreamReader(receiveStream, Encoding.GetEncoding(Response.CharacterSet));
                        string data = readStream.ReadToEnd();
                        modelDocument = Newtonsoft.Json.JsonConvert.DeserializeObject<DataAksiokFullSchemes<EfDatabase.ModelAksiok.Aksiok.ModelDocument[]>>(data);
                    }
                }

                foreach (var document in modelDocument.Data)
                {
                    DatesBytesFile = Encoding.UTF8.GetBytes("{\"records\":[],\"id\":" + document.Id + "}");
                    Request = (HttpWebRequest)WebRequest.Create("https://aksiok.dpc.tax.nalog.ru/api/EquipmentCard/get?_dc=1649676459600");
                    Request.Accept = "*/*";
                    Request.KeepAlive = true;
                    Request.Credentials = myCache;
                    Request.CookieContainer = cookies;
                    Request.Host = "aksiok.dpc.tax.nalog.ru";
                    Request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.45 Safari/537.36";
                    Request.ContentType = "application/json";
                    Request.Headers.Add("Accept-Encoding", "gzip, deflate, br");
                    Request.Headers.Add("Accept-Language", "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7");
                    Request.Headers.Add("X-Requested-With", "XMLHttpRequest");
                    Request.Headers.Add("Origin", "https://aksiok.dpc.tax.nalog.ru/");
                    Request.Headers.Add("sec-ch-ua", "Not A;Brand\";v=\"99\", \"Chromium\"; v=\"96\", \"Google Chrome\"; v=\"96\"");
                    Request.Headers.Add("sec-ch-ua-mobile", "?0");
                    Request.Headers.Add("sec-ch-ua-platform", "Windows");
                    Request.Headers.Add("Sec-Fetch-Dest", "empty");
                    Request.Headers.Add("Sec-Fetch-Mode", "cors");
                    Request.Headers.Add("Sec-Fetch-Site", "same-origin");
                    Request.Method = "POST";
                    Request.ContentLength = DatesBytesFile.Length;
                    using (var stream = Request.GetRequestStream())
                    {
                        stream.Write(DatesBytesFile, 0, DatesBytesFile.Length);
                    }
                    Response = (HttpWebResponse)Request.GetResponse();
                    cookies.Add(Response.Cookies);
                    if (Response.StatusCode == HttpStatusCode.OK)
                    {
                        using (var receiveStream = Response.GetResponseStream())
                        {
                            StreamReader readStream;
                            readStream = String.IsNullOrWhiteSpace(Response.CharacterSet)
                                ? new StreamReader(receiveStream)
                                : new StreamReader(receiveStream, Encoding.GetEncoding(Response.CharacterSet));
                            string data = readStream.ReadToEnd();
                            var dataModelServerAksiok = Newtonsoft.Json.JsonConvert.DeserializeObject<DataAksiokFullSchemes<EfDatabase.ModelAksiok.Aksiok.EpoDocument>>(data, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                            ac.AddAndUpdateFullLoadAksiok(dataModelServerAksiok.Data, "EpoDocument", p.Id);
                        }
                    }
                    DatesBytesFile = Encoding.UTF8.GetBytes("{\"records\":[],\"id\":" + document.Id + "}");
                    Request = (HttpWebRequest)WebRequest.Create("https://aksiok.dpc.tax.nalog.ru/api/EquipmentCardAddChar/get?_dc=1649676665906");
                    Request.Accept = "*/*";
                    Request.KeepAlive = true;
                    Request.Credentials = myCache;
                    Request.CookieContainer = cookies;
                    Request.Host = "aksiok.dpc.tax.nalog.ru";
                    Request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.45 Safari/537.36";
                    Request.ContentType = "application/json";
                    Request.Headers.Add("Accept-Encoding", "gzip, deflate, br");
                    Request.Headers.Add("Accept-Language", "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7");
                    Request.Headers.Add("X-Requested-With", "XMLHttpRequest");
                    Request.Headers.Add("Origin", "https://aksiok.dpc.tax.nalog.ru/");
                    Request.Headers.Add("sec-ch-ua", "Not A;Brand\";v=\"99\", \"Chromium\"; v=\"96\", \"Google Chrome\"; v=\"96\"");
                    Request.Headers.Add("sec-ch-ua-mobile", "?0");
                    Request.Headers.Add("sec-ch-ua-platform", "Windows");
                    Request.Headers.Add("Sec-Fetch-Dest", "empty");
                    Request.Headers.Add("Sec-Fetch-Mode", "cors");
                    Request.Headers.Add("Sec-Fetch-Site", "same-origin");
                    Request.Method = "POST";
                    Request.ContentLength = DatesBytesFile.Length;
                    using (var stream = Request.GetRequestStream())
                    {
                        stream.Write(DatesBytesFile, 0, DatesBytesFile.Length);
                    }
                    Response = (HttpWebResponse)Request.GetResponse();
                    cookies.Add(Response.Cookies);
                    if (Response.StatusCode == HttpStatusCode.OK)
                    {
                        using (var receiveStream = Response.GetResponseStream())
                        {
                            StreamReader readStream;
                            readStream = String.IsNullOrWhiteSpace(Response.CharacterSet)
                                ? new StreamReader(receiveStream)
                                : new StreamReader(receiveStream, Encoding.GetEncoding(Response.CharacterSet));
                            string data = readStream.ReadToEnd();
                            var dataModelServerAksiok = JsonConvert.DeserializeObject<DataAksiokFullSchemes<ValueCharacteristicJson>>(data, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                            ac.AddAndUpdateFullLoadAksiok(dataModelServerAksiok.Data, "ValueCharacteristicJson");
                        }
                    }
                }
            }
            ac.AddAndUpdateFullLoadAksiok<ValueCharacteristicJson>(null, "FinishProcess");
        }

        [TestMethod()]
        public void TestDeserializationAksiok2()
        {
            var userLogin = "7751-00-099";
            var passwordUser = "Qwerty12345678!";
            var aksiok = new AksiokPostGetSystem(userLogin, passwordUser);
            aksiok.StartUpdateAksiok();
        }
   }
}
