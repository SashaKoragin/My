using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using EfDatabase.Inventory.BaseLogic.AksiokAddAndUpdateObjectDb;
using EfDatabase.Inventory.ReportXml.ModelAksiok;

namespace LibraryAutoSupportSto.Aksiok.AksiokPostGetSystem
{
   public class AksiokPostGetSystem : IDisposable
    {
        /// <summary>
        /// Бд для обновления или добавления в БД
        /// </summary>
        private readonly AksiokAddAndUpdateObjectDb AksiokAddAndUpdateObjectDb = new AksiokAddAndUpdateObjectDb();
        /// <summary>
        /// Параметры для АКСИОК
        /// </summary>
        private readonly AllParameters AllParameters = new AllParameters();
        /// <summary>
        /// Авторизация сайта
        /// </summary>
        private CredentialCache MyCache { get; }
        /// <summary>
        /// Куки сайта
        /// </summary>
        private CookieContainer Сookies { get; }
        /// <summary>
        /// Параметры на отправку В АКСИОК
        /// </summary>
        private byte[] DatesBytes { get; set; }
        /// <summary>
        /// Запрос на СТО
        /// </summary>
        private HttpWebRequest Request { get; set; }
        /// <summary>
        /// Ответ с СТО
        /// </summary>
        private HttpWebResponse Response { get; set; }
        /// <summary>
        /// АКСИОК синхронизация с сайтом
        /// </summary>
        /// <param name="login">Логин</param>
        /// <param name="password">Пароль</param>
        public AksiokPostGetSystem(string login, string password)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            ServicePointManager.ServerCertificateValidationCallback = (senders, certificate, chain, sslPolicyErrors) =>
            {
                return true;
            };
            MyCache = new CredentialCache();
            MyCache.Add(new Uri("https://aksiok.dpc.tax.nalog.ru"), "Negotiate", new NetworkCredential(login, password));
            Сookies = new CookieContainer();
            Сookies.Add(new Cookie(".ASPXAUTH", "585B2C7DA8479B5A1A355A9F3CED47C3BDE9539CD6924311DDAAC368C951513252E0F2C706D11BF4E3FE31B2869A374962E555235370CED6DD63BD067095D3CA6CCF470768E7C61FB6C796A080FDE124CBB359D96133240B1983198864BE9836763DD7C13122BDE0703A5A760F3ACAD8FF62F43EF367170C1617A3DC956EBA471A7F81BC7F76C5B4C9B643C4AF5559F7B81100F25E81BB997A02BC99985B2F57D45A6F4FBAAD9B43D9A0EC7CDCB6C96C2AC2FB6C677AB2DAB99DC56CDF7D0CD9398671643E630F126A26230990F07DEA19DB6FC52AE3C450598FB3BAF75C3C9CD18B6E3AF61482DE5E4006BB3BDC5E909B1945D775E57D10837CBF2138E7F742255B522AEFBB296ABA7553CC5FC89AA0D97951003D431095E4D10B7C9F9BAF761D62E45475E9A99394FE4D3789A7D88E5429B1280A5EA9859C0446A1C6E74FFA6282CF4309A11F30DF0320343F40BC0D97F640E635637A8618E7EF35B8E43774C1305E4B3BC5FA7DA6A237F19452082C95229D90566C2BE802ACEF1EDB8EFBC199EC9D55C9F7C28064CFAB322930BE5454B12B99E430BF7E5B37F3678D5720AD", "/", "dpc.tax.nalog.ru"));
        }
        /// <summary>
        /// Запрос в Аксиок получения моделей
        /// </summary>
        /// <typeparam name="T">Тип возвращаемого параметра</typeparam>
        /// <param name="parameterAksiok">Параметры АКСИОК</param>
        /// <param name="idType">Ун типа </param>
        /// <param name="idProduct">Ун продукта</param>
        /// <returns></returns>
        private DataAksiokFullSchemes<T> PostAksiok<T>(ModelParametersAksiok parameterAksiok, int idType = 0, int idProduct = 0)
        {
            DataAksiokFullSchemes<T> dataModelServerAksiok = new DataAksiokFullSchemes<T>();
            DatesBytes = Encoding.UTF8.GetBytes(parameterAksiok.ParametersAksiok);
            Request = (HttpWebRequest)WebRequest.Create(parameterAksiok.UrlAksiok);
            Request.Accept = "*/*";
            Request.KeepAlive = true;
            Request.Credentials = MyCache;
            Request.CookieContainer = Сookies;
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
            Request.ContentLength = DatesBytes.Length;
            using (var stream = Request.GetRequestStream())
            {
                stream.Write(DatesBytes, 0, DatesBytes.Length);
            }
            Response = (HttpWebResponse)Request.GetResponse();
            Сookies.Add(Response.Cookies);
            if (Response.StatusCode == HttpStatusCode.OK)
            {
                using (var receiveStream = Response.GetResponseStream())
                {
                    var readStream = String.IsNullOrWhiteSpace(Response.CharacterSet)
                        ? new StreamReader(receiveStream ?? throw new InvalidOperationException())
                        : new StreamReader(receiveStream ?? throw new InvalidOperationException(), Encoding.GetEncoding(Response.CharacterSet));
                    string data = readStream.ReadToEnd();
                    dataModelServerAksiok = Newtonsoft.Json.JsonConvert.DeserializeObject<DataAksiokFullSchemes<T>>(data);
                    if (parameterAksiok.ModelUpdateSql != null)
                    {
                        AksiokAddAndUpdateObjectDb.AddAndUpdateFullLoadAksiok(dataModelServerAksiok.Data, parameterAksiok.ModelUpdateSql, idType, idProduct);
                    }
                   
                }
            }
            return dataModelServerAksiok;
        }
        /// <summary>
        /// Замена параметров запроса 
        /// </summary>
        /// <param name="parameterAksiok">Модель с параметрами АКСИОК</param>
        /// <param name="typeId">Ун типа</param>
        /// <param name="productId">Ун продукта</param>
        /// <param name="modelDocumentTypeId">Ун документа типа</param>
        /// <param name="modelDocumentId">Ун документа</param>
        /// <returns></returns>
        private ModelParametersAksiok GenerateParametersAksiok(ModelParametersAksiok parameterAksiok, int typeId = 0, int productId = 0, int modelDocumentTypeId = 0, int modelDocumentId = 0)
        {
            ModelParametersAksiok model = new ModelParametersAksiok();
            model.IndexExecute = parameterAksiok.IndexExecute;
            model.UrlAksiok = parameterAksiok.UrlAksiok;
            model.ModelUpdateSql = parameterAksiok.ModelUpdateSql;
            model.ParametersAksiok = parameterAksiok.ParametersAksiok.Replace("{Date}", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"))
                .Replace("{typeId}", typeId.ToString())
                .Replace("{productId}", productId.ToString())
                .Replace("{modelDocumentTypeId}", modelDocumentTypeId.ToString())
                .Replace("{modelDocumentId}", modelDocumentId.ToString());
            return model;
        }

        /// <summary>
        /// Метод запуска синхронизации АКСИОК
        /// </summary>
        public void StartUpdateAksiok()
        {
            int errorResultModelId = 0;
            string typeModelError = "";
            string sNumberModel = "";
            try
            {
                PostAksiok<EfDatabase.ModelAksiok.Aksiok.Producer[]>(GenerateParametersAksiok(AllParameters.ModelParametersAksiok.FirstOrDefault(x => x.IndexExecute == 1)));
                var modelType = PostAksiok<EfDatabase.ModelAksiok.Aksiok.EquipmentType[]>(GenerateParametersAksiok(AllParameters.ModelParametersAksiok.FirstOrDefault(x => x.IndexExecute == 2)));
                foreach (var type in modelType.Data)
                {
                    var modelProduct = PostAksiok<EfDatabase.ModelAksiok.Aksiok.Producer[]>(GenerateParametersAksiok(AllParameters.ModelParametersAksiok.FirstOrDefault(x => x.IndexExecute == 3), type.Id));
                    foreach (var producer in modelProduct.Data)
                    {
                        PostAksiok<EfDatabase.ModelAksiok.Aksiok.EquipmentModel[]>(GenerateParametersAksiok(AllParameters.ModelParametersAksiok.FirstOrDefault(x => x.IndexExecute == 4), type.Id, producer.Id), type.Id, producer.Id);
                    }
                }
                var modelDocumentType = PostAksiok<EfDatabase.ModelAksiok.Aksiok.ModelDocumentType[]>(GenerateParametersAksiok(AllParameters.ModelParametersAksiok.FirstOrDefault(x => x.IndexExecute == 5)));
                foreach (var document in modelDocumentType.Data)
                {
                    typeModelError = document.CategoriesTruName;
                    var modelDocument = PostAksiok<EfDatabase.ModelAksiok.Aksiok.ModelDocument[]>(GenerateParametersAksiok(AllParameters.ModelParametersAksiok.FirstOrDefault(x => x.IndexExecute == 6), 0, 0, document.Id));
                    foreach (var model in modelDocument.Data)
                    {
                        errorResultModelId = model.Id;
                        sNumberModel = model.SerialNumber;
                        PostAksiok<EfDatabase.ModelAksiok.Aksiok.EpoDocument>(GenerateParametersAksiok(AllParameters.ModelParametersAksiok.FirstOrDefault(x => x.IndexExecute == 7), 0, 0, 0, model.Id), document.Id);
                        PostAksiok<EfDatabase.ModelAksiok.Aksiok.ValueCharacteristicJson>(GenerateParametersAksiok(AllParameters.ModelParametersAksiok.FirstOrDefault(x => x.IndexExecute == 8), 0, 0, 0, model.Id));
                    }
                }
                AksiokAddAndUpdateObjectDb.AddAndUpdateFullLoadAksiok<EfDatabase.ModelAksiok.Aksiok.ValueCharacteristicJson>(null, AllParameters.ModelParametersAksiok.FirstOrDefault(x => x.IndexExecute == 9)?.ModelUpdateSql);
                Dispose();
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
                Loggers.Log4NetLogger.Error(
                        new Exception("Данные не соответствуют формату int в Ун модели: " + errorResultModelId +", Серийный номер: "+sNumberModel+", Категория: " + typeModelError+"."));

                Dispose();
            }
        }
        /// <summary>
        /// Точечная синхронизация
        /// </summary>
        /// <param name="idModel">Ун модели</param>
        /// <param name="idDocument">Ун документа</param>
        /// <param name="serialNumber">Серийный номер</param>
        public void PointSynchronizationAksiok(int idModel,int idDocument, string serialNumber)
        {
            try
            {
                PostAksiok<EfDatabase.ModelAksiok.Aksiok.EpoDocument>(GenerateParametersAksiok(AllParameters.ModelParametersAksiok.FirstOrDefault(x => x.IndexExecute == 7), 0, 0, 0, idModel), idDocument);
                PostAksiok<EfDatabase.ModelAksiok.Aksiok.ValueCharacteristicJson>(GenerateParametersAksiok(AllParameters.ModelParametersAksiok.FirstOrDefault(x => x.IndexExecute == 8), 0, 0, 0, idModel));
                AksiokAddAndUpdateObjectDb.AddAndUpdateFullLoadAksiok<EfDatabase.ModelAksiok.Aksiok.ValueCharacteristicJson>(null, AllParameters.ModelParametersAksiok.FirstOrDefault(x => x.IndexExecute == 9)?.ModelUpdateSql);
                Dispose();
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
                Loggers.Log4NetLogger.Error(
                    new Exception("Данные не соответствуют формату int в Ун модели: " + idModel + ", Серийный номер: " + serialNumber + ", Категория: " + idDocument + "."));

                Dispose();
            }
        }

        /// <summary>
        /// Dispose 
        /// </summary>
        public void Dispose()
        {
            Response.Close();
            Response.Dispose();
            AksiokAddAndUpdateObjectDb.Dispose();
        }
    }
}
