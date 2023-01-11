using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibaryDocumentGeneratorTestsTemplate.KasperskyTest
{
    [TestClass()]
    public class KasperskyApi
    {
        /// <summary>
        /// Авторизация сайта
        /// </summary>
        public CredentialCache MyCache { get; set; }
        /// <summary>
        /// Куки сайта
        /// </summary>
        public CookieContainer Сookies { get; }
        /// <summary>
        /// Параметры на отправку В АКСИОК
        /// </summary>
        public byte[] DatesBytes { get; set; }
        /// <summary>
        /// Запрос на СТО
        /// </summary>
        public HttpWebRequest Request { get; set; }
        /// <summary>
        /// Ответ с СТО
        /// </summary>
        public HttpWebResponse Response { get; set; }
        [TestMethod()]
        public void KasperskyApiStart()
        {
            try
            {
                var login = "REGIONS\\7751-00-099";
                var password = "Qwerty12345@";
                var cert = "C:\\ProgramData\\KasperskyLab\\adminkit\\1103\\klserver.cer";
                var url = "https://i7751-sys017:13299/api/v1.0/Session.StartSession";
                ServicePointManager.SecurityProtocol =
                    SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                ServicePointManager.ServerCertificateValidationCallback =
                    (senders, certificate, chain, sslPolicyErrors) =>
                    {
                        return true;
                    };
                var encoded =
                    System.Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(login + ":" + password));
                MyCache = new CredentialCache();
                MyCache.Add(new Uri(url), "NTLM", new NetworkCredential(login, password));
                Request = (HttpWebRequest) WebRequest.Create(url);
                Request.ContentType = "application/json";
                Request.Method = "POST";
                Request.Headers.Add("Accept-Encoding", "gzip, deflate");
                Request.Credentials = MyCache;
              
                //Request.Headers.Add("Authorization", "Basic " + encoded);
                Response = (HttpWebResponse) Request.GetResponse();
                if (Response.StatusCode == HttpStatusCode.OK)
                {
                    var cookie = Response.Headers[HttpResponseHeader.SetCookie];
                }
            }
            catch (Exception e)
            {
               
            }
        }
    }
}
