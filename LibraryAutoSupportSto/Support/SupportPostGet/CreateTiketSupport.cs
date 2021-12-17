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
        /// Ответ с Support
        /// </summary>
        private HttpWebResponse Response { get; set; }
        /// <summary>
        /// Параметры
        /// </summary>
        private byte[] DatesBytes { get; set; }
        /// <summary>
        /// Параметры файла
        /// </summary>
        private byte[] DatesBytesFile { get; set; }
        /// <summary>
        /// Файл отчета
        /// </summary>
        private byte[] File { get; set; }
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
        /// Шаг загрузки файла
        /// </summary>
        public string Step3UploadFile = "https://support.tax.nalog.ru/include/fileupload.php";
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
        /// <param name="urlSteps">Url Шага</param>
        /// <param name="isModel">IsModel POST GET</param>
        /// <param name="datesBytesParameters">Параметры на GET пусто</param>
        public void Steps(string urlSteps, string isModel, byte[] datesBytesParameters = null)
        {
            Dispose();
            Request = (HttpWebRequest)WebRequest.Create(urlSteps);
            Request.ContentType = File != null ? "multipart/form-data; boundary=----WebKitFormBoundaryTwIa3JXXvh1tOcrg" : "application/x-www-form-urlencoded";
            Request.CookieContainer = Сookie;
            byte[] endFile = null;
            Request.Method = isModel;
            if (isModel != "GET")
            {
                Request.KeepAlive = true;
                Request.Referer = "https://support.tax.nalog.ru/requests/";
                Request.Host = "support.tax.nalog.ru";
                Request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.45 Safari/537.36";
                Request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                Request.Accept = "application/json, text/javascript, */*; q=0.01";
                Request.Headers.Add("Accept-Encoding", "gzip, deflate, br");
                Request.Headers.Add("Accept-Language", "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7");
                Request.Headers.Add("Origin", "https://support.tax.nalog.ru");
                Request.Headers.Add("sec-ch-ua", "Not A;Brand\";v=\"99\", \"Chromium\"; v=\"96\", \"Google Chrome\"; v=\"96\"");
                Request.Headers.Add("sec-ch-ua-mobile", "?0");
                Request.Headers.Add("sec-ch-ua-platform",  "Windows");
                Request.Headers.Add("Sec-Fetch-Dest", "empty");
                Request.Headers.Add("Sec-Fetch-Mode", "cors");
                Request.Headers.Add("Sec-Fetch-Site", "same-origin");
                if (File != null)
                {
                    endFile = Encoding.UTF8.GetBytes("\r\n------WebKitFormBoundaryTwIa3JXXvh1tOcrg");
                    Request.ContentLength = datesBytesParameters.Length + File.Length + endFile.Length;
                }
                else
                {
                    Request.ContentLength = datesBytesParameters.Length;
                }
                using (var stream = Request.GetRequestStream())
                {
                    stream.Write(datesBytesParameters, 0, datesBytesParameters.Length);
                    if (File != null)
                    {
                        stream.Write(File, 0, File.Length);
                        stream.Write(endFile ?? throw new InvalidOperationException("Окончание файла не задано!"), 0, endFile.Length);
                    }
                }
            }
            File = null;
            Response = (HttpWebResponse) Request.GetResponse();
        }
        /// <summary>
        /// Генерация параметров для последующих шагов
        /// </summary>
        /// <param name="findNode">Модель параметров</param>
        /// <param name="modelParameter">Модель параметров</param>
        /// <param name="isStep3">Параметры шага 3</param>
        private void GenerateParameterResponse(string findNode, TemplateSupport1[] modelParameter=null, bool isStep3 = false)
        {
            if (Response.StatusCode == HttpStatusCode.OK)
            {
                string inputParameter;
                DatesBytes = null;
                string inputParameterStep3 = null;
                DatesBytesFile = null;
                File = null;
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
                    if (isStep3)
                    {
                        inputParameterStep3 = formNode.Elements("input").Where(type => type.GetAttributeValue("type", "hidden") == "hidden").Cast<HtmlNode>()
                            .Select(elem => $"------WebKitFormBoundaryTwIa3JXXvh1tOcrg\r\nContent-Disposition: form-data; name=\"{elem.GetAttributeValue("name", "default")}\"\r\n\r\n{elem.GetAttributeValue("value", "default")}")
                            .Aggregate((element, next) => element + (string.IsNullOrWhiteSpace(element) ? string.Empty : "\r\n") + next);
                    }
                    readStream.Close();
                    receiveStream.Close();
                }

                if (isStep3)
                {
                    foreach (var templateSupportAndParameterSupport in modelParameter)
                    {
                        inputParameterStep3 += $"\r\n------WebKitFormBoundaryTwIa3JXXvh1tOcrg\r\n{templateSupportAndParameterSupport.NameGuidParametr}\r\n\r\n";
                        File = templateSupportAndParameterSupport.ParameterStep3;
                    }
                    DatesBytesFile = Encoding.UTF8.GetBytes(inputParameterStep3);
                }
                else
                {
                    if (modelParameter != null)
                    {
                        inputParameter = modelParameter.Aggregate(inputParameter, (current, templateSupportAndParameterSupport) => current + $"&{templateSupportAndParameterSupport.NameGuidParametr}={WebUtility.UrlEncode(templateSupportAndParameterSupport.Parametr)}");
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
            GenerateParameterResponse("//form[@action='/requests/create.php?step=1']", modelSupport.TemplateSupport.Where(param => param.NameStepSupport == "Step1").ToArray());
            Steps(Step1Post, "POST",DatesBytes);
            GenerateParameterResponse("//form[@action='/requests/create.php?step=2']", modelSupport.TemplateSupport.Where(param => param.NameStepSupport == "Step2").ToArray());
            Steps(Step2Post, "POST", DatesBytes);
            GenerateParameterResponse("//form[@action='/requests/create.php?step=3']", modelSupport.TemplateSupport.Where(param => param.NameStepSupport == "Step3").ToArray(),true);
            if (DatesBytesFile != null)
            {
                Steps(Step3UploadFile, "POST", DatesBytesFile);
            }
            Steps(Step3Post, "POST", DatesBytes);
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
