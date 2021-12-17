using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using HtmlAgilityPack;

namespace LibraryAutoSupportSto.PassportSto.PassportStoPostGet
{
   public class PassportStoPostGet : IDisposable
    {
        /// <summary>
        /// Логин пользователя
        /// </summary>
        private string LoginUser { get; set; }
        /// <summary>
        /// Пароль пользователя
        /// </summary>
        private string PasswordUser { get; set; }
        /// <summary>
        /// Полный путь сохранения файла с именем полученного от сервера
        /// </summary>
        private string FullPathSaveFileReport { get; set; }
        /// <summary>
        /// Параметры СТО
        /// </summary>
        private ParametersStoPostGet ParametersSto { get; set; }
        /// <summary>
        /// Шаблон поиска элемента 1
        /// </summary>
        private string TemplateFind1 = "//form[@action='./default.aspx?ReturnUrl=%2f']";
        /// <summary>
        /// Шаблон поиска элемента 2
        /// </summary>
        private string TemplateFind2 = "//form[@action='./DivisionSearch.aspx']";
        /// <summary>
        /// Путь сохранения файла
        /// </summary>
        private string PathSaveFileReport { get; set; }
        /// <summary>
        /// Запрос на СТО
        /// </summary>
        private HttpWebRequest Request { get; set; }
        /// <summary>
        /// Ответ с СТО
        /// </summary>
        private HttpWebResponse Response { get; set; }

        private byte[] DatesBytes { get; set; }
        /// <summary>
        /// Куки сайта
        /// </summary>
        private CookieContainer Сookie { get; set; }
        /// <summary>
        /// Сайт СТО для получения страницы авторизации
        /// </summary>
        private readonly string PassportStoAuthorization = "http://10.250.5.3:95/default.aspx?ReturnUrl=%2f";
        /// <summary>
        /// Страница получения данных с СТО
        /// </summary>
        private readonly string PagePassportSto = "http://10.250.5.3:95/Equipments/DivisionSearch.aspx";

        public PassportStoPostGet(string login, string password, string pathSaveFileReport)
        {
            LoginUser = login;
            PasswordUser = password;
            PathSaveFileReport = pathSaveFileReport;
            ParametersSto = new ParametersStoPostGet();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            Request = (HttpWebRequest) WebRequest.Create(PassportStoAuthorization);
            Request.CookieContainer = new CookieContainer();
            Request.ProtocolVersion = HttpVersion.Version11;
            Request.Method = "GET";
            Response = (HttpWebResponse)Request.GetResponse();
            Сookie = new CookieContainer();
            Сookie.Add(Response.Cookies);
            GenerateParameterResponse(TemplateFind1,ParametersSto.ListParametersAuthorization,true);
        }
        /// <summary>
        /// Метод запроса на сервер Get или Post
        /// </summary>
        /// <param name="urlSteps"></param>
        /// <param name="isMethod"></param>
        private void GetAndPost(string urlSteps, string isMethod)
        {
            Dispose();
            Request = (HttpWebRequest)WebRequest.Create(urlSteps);
            Request.ContentType = "application/x-www-form-urlencoded";
            Request.CookieContainer = Сookie;
            Request.Method = isMethod;
            if (isMethod != "GET")
            {
                Request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                Request.Accept = "text/html,application/xhtml+xml,image/jxr,*/*";
                Request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko";
                Request.Headers.Add("Accept-Encoding", "gzip, deflate");
                Request.Headers.Add("Accept-Language", "ru-RU");
                Request.ContentLength = DatesBytes.Length;
                using (var stream = Request.GetRequestStream())
                {
                    stream.Write(DatesBytes, 0, DatesBytes.Length);
                }
            }
            Response = (HttpWebResponse)Request.GetResponse();
        }

        /// <summary>
        /// Генерация параметров для запроса на СТО
        /// </summary>
        /// <param name="findNode">Элемент поиска</param>
        /// <param name="listParametersSto">Словарь параметров для запроса</param>
        /// <param name="isIn">Переключение авторизация или нет</param>
        private void GenerateParameterResponse(string findNode, Dictionary<string, string> listParametersSto, bool isIn = false)
        {
            if (Response.StatusCode == HttpStatusCode.OK)
            {
                string inputParameter;
                DatesBytes = null;
                using (var receiveStream = Response.GetResponseStream())
                {
                    StreamReader readStream;
                    readStream = String.IsNullOrWhiteSpace(Response.CharacterSet) ? new StreamReader(receiveStream) : new StreamReader(receiveStream, Encoding.GetEncoding(Response.CharacterSet));
                    string data = readStream.ReadToEnd();
                    HtmlDocument document = new HtmlDocument();
                    document.LoadHtml(data);
                    HtmlNode formNode = document.DocumentNode.SelectSingleNode(findNode);

                    inputParameter = listParametersSto.Select(d => $"{WebUtility.UrlEncode(d.Key)}={(d.Value.Contains("{") ? d.Value : WebUtility.UrlEncode(d.Value))}")
                        .Aggregate((element, next) => element + (string.IsNullOrWhiteSpace(element) ? string.Empty : "&") + next);

                    inputParameter = inputParameter.Replace("Replace=Replace", formNode.Elements("input")
                        .Where(type => type.GetAttributeValue("type", "hidden") == "hidden")
                        .Cast<HtmlNode>()
                        .Select(elem =>
                            $"{WebUtility.UrlEncode(elem.GetAttributeValue("name", "default"))}={WebUtility.UrlEncode(elem.GetAttributeValue("value", "default"))}")
                        .Aggregate((element, next) =>
                            element + (string.IsNullOrWhiteSpace(element) ? string.Empty : "&") + next));

                    readStream.Close();
                    receiveStream.Close();
                }
                if (isIn)
                {
                    inputParameter = string.Format(inputParameter, WebUtility.UrlEncode(LoginUser), WebUtility.UrlEncode(PasswordUser));
                }
                DatesBytes = Encoding.ASCII.GetBytes(inputParameter);
            }
        }
        /// <summary>
        /// Сохранение файла с сервера
        /// </summary>
        private void SaveFileReport()
        {
            ContentDisposition content = new ContentDisposition(Response.Headers["Content-Disposition"]);
            var fileName = WebUtility.UrlDecode(content.FileName);
            FullPathSaveFileReport = Path.Combine(PathSaveFileReport, fileName);
            if (Response.StatusCode == HttpStatusCode.OK)
            {
                using (var receiveStream = Response.GetResponseStream())
                {
                    MemoryStream ms = new MemoryStream();
                    receiveStream.CopyTo(ms);
                    File.WriteAllBytes(FullPathSaveFileReport, ms.ToArray());
                    receiveStream.Close();
                }
            }
        }
        /// <summary>
        /// Метод выгрузки файла отчета с сайта СТО
        /// </summary>
        /// <returns></returns>
        public string DownloadReportSto()
        {
            GetAndPost(PassportStoAuthorization, "POST"); //Авторизация
            GetAndPost(PagePassportSto, "GET");   //Получение второй страницы
            GenerateParameterResponse(TemplateFind2, ParametersSto.ListParametersReportExcel); //Генерация параметров для запроса
            GetAndPost(PagePassportSto, "POST"); //Запрос на сайт
            SaveFileReport(); //Сохранение файла с сервера
            GetAndPost(PagePassportSto, "GET"); //Получение параметров второй страницы для выхода из сайта
            GenerateParameterResponse(TemplateFind2, ParametersSto.ListParametersExitAuthorization); //Генерация параметров для выхода с сайта
            GetAndPost(PagePassportSto, "POST"); //Запрос на выход с сайт
            Dispose();
            return FullPathSaveFileReport;
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
