﻿using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using EfDatabaseXsdSupportNalog;
using HtmlAgilityPack;

namespace LibraryAutoSupportSto.Support.SupportPostGet
{
    /// <summary>
    /// Класс генерации заявки на СТП ФКУ
    /// </summary>
   public class CreateTiсketSupport :IDisposable
    {
        /// <summary>
        /// Запрос
        /// </summary>
        private HttpWebRequest Request { get; set; }
        /// <summary>
        /// Ответ с СТО Support
        /// </summary>
        private HttpWebResponse Response { get; set; }

        private byte[] DatesBytes { get; set; }
        /// <summary>
        /// Куки сайта
        /// </summary>
        private CookieContainer Сookie { get; set; }
        /// <summary>
        /// Авторизация на сайте Support
        /// </summary>
        private readonly string _autorizationSupport = "https://support.tax.nalog.ru/?login=yesbackurl=%2F&AUTH_FORM=Y&TYPE=AUTH&USER_LOGIN={0}&USER_PASSWORD={1}&Login=null";

        /// <summary>
        /// Шаг 1 для запроса
        /// </summary>
        public string Step1Post = "https://support.tax.nalog.ru/requests/create.php?step=1";

        /// <summary>
        /// Шаг 2 для запроса
        /// </summary>
        public string Step2Post = "https://support.tax.nalog.ru/requests/create.php?step=2";

        /// <summary>
        /// Шаг 3 для запроса
        /// </summary>
        public string Step3Post = "https://support.tax.nalog.ru/requests/create.php?step=3";

        public CreateTiсketSupport(string login, string password)
        {
            Request = (HttpWebRequest)WebRequest.Create(_autorizationSupport.Replace("{0}",login).Replace("{1}",password));
            Request.CookieContainer = new CookieContainer();
            Request.Method = "POST";
            Response = (HttpWebResponse)Request.GetResponse();
            Сookie = new CookieContainer();
            Сookie.Add(Response.Cookies);
        }
        /// <summary>
        /// Подготовительный шаг
        /// </summary>
        public void StepTraining()
        {
            Request = (HttpWebRequest)WebRequest.Create(Step1Post);
            Request.CookieContainer = Сookie;
            Response = (HttpWebResponse)Request.GetResponse();
            Сookie.Add(Response.Cookies);
        }
        /// <summary>
        /// Шаги для запросов на выполнение
        /// </summary>
        public void Steps(string urlSteps)
        {
            Request = (HttpWebRequest)WebRequest.Create(urlSteps);
            Request.ContentType = "application/x-www-form-urlencoded";
            Request.ContentLength = DatesBytes.Length;
            Request.CookieContainer = Сookie;
            Request.Method = "POST";
            using (var stream = Request.GetRequestStream())
            {
                stream.Write(DatesBytes, 0, DatesBytes.Length);

            }
            Response = (HttpWebResponse)Request.GetResponse();
        }


        /// <summary>
        /// Генерация параметров для последующих шагов
        /// </summary>
        /// <param name="findNode">Модель параметров</param>
        /// <param name="modelParameter">Модель параметров</param>
        public void GenerateParameterResponse(string findNode, TemplateSupport[] modelParameter=null)
        {
            if (Response.StatusCode == HttpStatusCode.OK)
            {
                string inputParameter = null;
                DatesBytes = null;
                using (var receiveStream = Response.GetResponseStream())
                {
                    StreamReader readStream;
                    readStream = String.IsNullOrWhiteSpace(Response.CharacterSet) ? new StreamReader(receiveStream) : new StreamReader(receiveStream, Encoding.GetEncoding(Response.CharacterSet));
                    string data = readStream.ReadToEnd();
                    HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                    document.LoadHtml(data);
                    HtmlNode formNode = document.DocumentNode.SelectSingleNode(findNode);
                    inputParameter = formNode.Elements("input").Where(type => type.GetAttributeValue("type", "hidden") == "hidden").Cast<HtmlNode>()
                        .Select(elem => $"{elem.GetAttributeValue("name", "default")}={elem.GetAttributeValue("value", "default")}")
                        .Aggregate((element, next) => element + (string.IsNullOrWhiteSpace(element) ? string.Empty : "&") + next);
                    readStream.Close();
                    receiveStream.Close();
                }
                if (modelParameter != null)
                {
                    foreach (var templateSupportAndParameterSupport in modelParameter)
                    {
                        inputParameter += $"&{templateSupportAndParameterSupport.NameGuidParametr}={WebUtility.UrlEncode(templateSupportAndParameterSupport.Parametr)}";
                    }
                }
                DatesBytes = Encoding.ASCII.GetBytes(inputParameter);
            }
        }
        /// <summary>
        /// Возврат результата с шага 3
        /// </summary>
        public string ReturnResponseWebStep3()
        {
            string data = null;
            if (Response.StatusCode == HttpStatusCode.OK)
            {
                using (var receiveStream = Response.GetResponseStream())
                {
                    StreamReader readStream;
                    readStream = String.IsNullOrWhiteSpace(Response.CharacterSet) ? new StreamReader(receiveStream) : new StreamReader(receiveStream, Encoding.GetEncoding(Response.CharacterSet));
                    data = readStream.ReadToEnd();
                    
                    readStream.Close();
                    receiveStream.Close();
                }
            }
            return data;
        }
        /// <summary>
        /// Dispose 
        /// </summary>
        public void Dispose()
        {
            Request = null;
            DatesBytes = null;
            Сookie = null;
            Response.Close(); 
            Response.Dispose();
        }
    }
}