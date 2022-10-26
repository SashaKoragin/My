using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EfDatabase.Inventory.BaseLogic.AksiokAddAndUpdateObjectDb;
using EfDatabase.Inventory.ReportXml.ModelAksiok;
using EfDatabase.ModelAksiok.Aksiok;
using LibraryAutoSupportSto.Aksiok.AksiokPostGetSystem;


namespace LibaryDocumentGeneratorTestsTemplate.Aksiok
{
    [TestClass()]
   public class TestAksiokToModel
   {
       private char[] dictionaryRu = {
           'А','а','Б','б','В','в','Г','г','Д','д','Е','е','Ё','ё',
           'Ж','ж','З','з','И','и','Й','й','К','к','Л','л','М','м',
           'Н','н','О','о','П','п','Р','р','С','с','Т','т','У','у',
           'Ф','ф','Х','х','Ц','ц','Ч','ч','Ш','ш','Щ','щ','Ъ','ъ',
           'Ь','ь','Ы','ы','Э','э','Ю','ю','Я','я','№','«','»'
       };
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
            var stringsName = "11.11.2021";
            var y = DateTime.ParseExact(stringsName, "dd.MM.yyyy", null);
        }

        [TestMethod()]
        public void TestDeserializationAksiok2()
        {
            try
            {


            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            ServicePointManager.ServerCertificateValidationCallback = (senders, certificate, chain, sslPolicyErrors) =>
            {
                return true;
            };
            var MyCache = new CredentialCache();
            MyCache.Add(new Uri("https://aksiok.dpc.tax.nalog.ru"), "Negotiate", new NetworkCredential("7751-00-099", "Qwerty123!"));

            Request = (HttpWebRequest)WebRequest.Create("https://aksiok.dpc.tax.nalog.ru/ ");
            Request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9";
            Request.KeepAlive = true;
            Request.Credentials = MyCache;
            Request.Host = "aksiok.dpc.tax.nalog.ru";
            Request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.45 Safari/537.36";
            Request.Method = "GET";
            Response = (HttpWebResponse)Request.GetResponse();
            var cookie = Response.Headers[HttpResponseHeader.SetCookie].Split('=');
            var  Сookies = new CookieContainer();
            Сookies.Add(new Cookie(cookie[0], cookie[1].Split(';')[0], "/", "dpc.tax.nalog.ru"));
                ;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            //var userLogin = "7751-00-099";
            //var passwordUser = "Qwerty12345678!";
            //var aksiok = new AksiokPostGetSystem(userLogin, passwordUser);
            //aksiok.StartUpdateAksiok();
        }
    }
}
