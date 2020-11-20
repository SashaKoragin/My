using System;
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
   public class CreateTiсketSupport : IDisposable
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
        private readonly string _autorizationSupport = "https://support.tax.nalog.ru/?login=yesbackurl=%2F&AUTH_FORM=Y&TYPE=AUTH&USER_LOGIN={0}&USER_PASSWORD={1}&Login={2}";
        /// <summary>
        /// Выход из сайта
        /// </summary>
        public string Logon = "https://support.tax.nalog.ru/?logout=yes";
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
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            Request = (HttpWebRequest)WebRequest.Create(_autorizationSupport.Replace("{0}", WebUtility.UrlEncode(login)).Replace("{1}", WebUtility.UrlEncode(password)).Replace("{2}", WebUtility.UrlEncode("Войти")));
            Request.CookieContainer = new CookieContainer();
            Request.ProtocolVersion = HttpVersion.Version11;
            Request.Method = "POST";
            Response = (HttpWebResponse)Request.GetResponse();
            Сookie = new CookieContainer();
            Сookie.Add(Response.Cookies);
        }
        /// <summary>
        /// Подготовительный шаг
        /// </summary>
        private void StepTraining()
        {
            Dispose();
            Request = (HttpWebRequest)WebRequest.Create(Step1Post);
            Request.CookieContainer = Сookie;
            Request.ProtocolVersion = HttpVersion.Version11;
            Request.Method = "GET";
            Response = (HttpWebResponse)Request.GetResponse();
            Сookie.Add(Response.Cookies); //Может быть проблема в этой строке?
        }
        /// <summary>
        /// Шаги для запросов на выполнение
        /// </summary>
        public void Steps(string urlSteps,string isModel)
        {
            Dispose();
            Request = (HttpWebRequest)WebRequest.Create(urlSteps);
            Request.ContentType = "application/x-www-form-urlencoded";
            Request.CookieContainer = Сookie;
            Request.Method = isModel;
            if (isModel != "GET")
            {
                Request.ContentLength = DatesBytes.Length;
                using (var stream = Request.GetRequestStream())
                {
                    stream.Write(DatesBytes, 0, DatesBytes.Length);
                }
            }
            Response = (HttpWebResponse)Request.GetResponse();
        }


        /// <summary>
        /// Генерация параметров для последующих шагов
        /// </summary>
        /// <param name="findNode">Модель параметров</param>
        /// <param name="modelParameter">Модель параметров</param>
        private void GenerateParameterResponse(string findNode, TemplateSupport1[] modelParameter=null)
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
        private string ReturnResponseWebStep3()
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
        /// Создание полной заявки на СТП Support.Tax.Nalog.ru
        /// </summary>
        /// <param name="modelSupport">Модель параметров</param>
        /// <returns></returns>
        public string CreateFullSupportTax(ModelParametrSupport modelSupport)
        {
            StepTraining();
            GenerateParameterResponse("//form[@action='/requests/create.php?step=1']",
                modelSupport.TemplateSupport.Where(param => param.NameStepSupport == "Step1").ToArray());
            Steps(Step1Post, "POST");
            GenerateParameterResponse("//form[@action='/requests/create.php?step=2']",
                modelSupport.TemplateSupport.Where(param => param.NameStepSupport == "Step2").ToArray());
            Steps(Step2Post, "POST");
            GenerateParameterResponse("//form[@action='/requests/create.php?step=3']");
            Steps(Step3Post, "POST");
            return ReturnResponseWebStep3();
        }

        /// <summary>
        /// Dispose 
        /// </summary>
        public void Dispose()
        {
            Response.Close(); 
            Response.Dispose();
        }
    }
}
