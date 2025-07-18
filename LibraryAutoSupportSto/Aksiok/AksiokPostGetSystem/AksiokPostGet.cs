using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using EfDatabase.Inventory.Base;
using EfDatabase.Inventory.BaseLogic.AksiokAddAndUpdateObjectDb;
using EfDatabase.Inventory.ReportXml.ModelAksiok;
using EfDatabase.ModelAksiok.Aksiok;
using LibaryXMLAutoReports.FullTemplateSheme;
using EpoDocument = EfDatabase.Inventory.Base.EpoDocument;

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
            Request = (HttpWebRequest)WebRequest.Create("https://aksiok.dpc.tax.nalog.ru/ ");
            Request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9";
            Request.KeepAlive = true;
            Request.Credentials = MyCache;
            Request.Host = "aksiok.dpc.tax.nalog.ru";
            Request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.45 Safari/537.36";
            Request.Method = "GET";
            Response = (HttpWebResponse)Request.GetResponse();
            var cookie = Response.Headers[HttpResponseHeader.SetCookie].Split('=');
            Сookies = new CookieContainer();
            Сookies.Add(new Cookie(cookie[0], cookie[1].Split(';')[0], "/", "dpc.tax.nalog.ru"));
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
                        try
                        {
                            PostAksiok<EfDatabase.ModelAksiok.Aksiok.EquipmentModel[]>(GenerateParametersAksiok(AllParameters.ModelParametersAksiok.FirstOrDefault(x => x.IndexExecute == 4), type.Id, producer.Id), type.Id, producer.Id);
                        }
                        catch (Exception e)
                        {
                            Loggers.Log4NetLogger.Error(e);
                            Loggers.Log4NetLogger.Error(new Exception("Ошибки в синхронизации в Ун типа: " + type.Id + " " + type.Name + ", Ун производителя: " + producer.Id + " " + producer.Name + " ."));
                        }
                    }
                }
                var modelDocumentType = PostAksiok<EfDatabase.ModelAksiok.Aksiok.ModelDocumentType[]>(GenerateParametersAksiok(AllParameters.ModelParametersAksiok.FirstOrDefault(x => x.IndexExecute == 5)));
                foreach (var document in modelDocumentType.Data)
                {
                    typeModelError = document.EquipmentTypeName;
                    var modelDocument = PostAksiok<EfDatabase.ModelAksiok.Aksiok.ModelDocument[]>(GenerateParametersAksiok(AllParameters.ModelParametersAksiok.FirstOrDefault(x => x.IndexExecute == 6), 0, 0, document.Id));
                    foreach (var model in modelDocument.Data)
                    {
                        errorResultModelId = model.Id;
                        sNumberModel = model.SerialNumber;
                        var epoDocument = PostAksiok<EfDatabase.ModelAksiok.Aksiok.EpoDocument>(GenerateParametersAksiok(AllParameters.ModelParametersAksiok.FirstOrDefault(x => x.IndexExecute == 7), 0, 0, 0, model.Id), document.Id);
                        epoDocument.Data.CanDelete = model.CanDelete;
                        epoDocument.Data.CanCreateKit = model.CanCreateKit;
                        epoDocument.Data.CanEditKit = model.CanEditKit;
                        epoDocument.Data.CanDisbandKit = model.CanDisbandKit;
                        epoDocument.Data.EquipmentKitId = model.EquipmentKitId;
                        AksiokAddAndUpdateObjectDb.AddAndUpdateFullLoadAksiok(epoDocument.Data, "EpoDocument", document.Id);
                        PostAksiok<EfDatabase.ModelAksiok.Aksiok.ValueCharacteristicJson>(GenerateParametersAksiok(AllParameters.ModelParametersAksiok.FirstOrDefault(x => x.IndexExecute == 8), 0, 0, 0, model.Id));
                    }
                    PostAksiok<EfDatabase.ModelAksiok.Aksiok.ContractSpecification[]>(GenerateParametersAksiok(AllParameters.ModelParametersAksiok.FirstOrDefault(x => x.IndexExecute == 10), 0, 0, 0, document.Id), document.Id);
                }
                PostAksiok<EfDatabase.ModelAksiok.Aksiok.ContractOnSto[]>(GenerateParametersAksiok(AllParameters.ModelParametersAksiok.FirstOrDefault(x => x.IndexExecute == 9)));
                AksiokAddAndUpdateObjectDb.AddAndUpdateFullLoadAksiok<EfDatabase.ModelAksiok.Aksiok.ValueCharacteristicJson>(null, AllParameters.ModelParametersAksiok.FirstOrDefault(x => x.IndexExecute == 11)?.ModelUpdateSql);
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
        /// Актуализация справочников Аксиок для ФКУ
        /// </summary>
        public void StartUpdateDirectoryAksiok()
        {
            Dispose();
        }

        /// <summary>
        /// Точечная синхронизация
        /// </summary>
        /// <param name="idModel">Ун модели</param>
        /// <param name="idDocument">Ун документа</param>
        /// <param name="serialNumber">Серийный номер</param>
        public EfDatabase.ModelAksiok.Aksiok.EpoDocument PointSynchronizationAksiok(int idModel,int idDocument, string serialNumber)
        {
            try
            {
                var modelDocuments = PostAksiok<EfDatabase.ModelAksiok.Aksiok.ModelDocument[]>(GenerateParametersAksiok(AllParameters.ModelParametersAksiok.FirstOrDefault(x => x.IndexExecute == 6), 0, 0, idDocument));
                var modelDocument = modelDocuments.Data.First(model => model.SerialNumber == serialNumber);
                var epoDocument = PostAksiok<EfDatabase.ModelAksiok.Aksiok.EpoDocument>(GenerateParametersAksiok(AllParameters.ModelParametersAksiok.FirstOrDefault(x => x.IndexExecute == 7), 0, 0, 0, idModel), idDocument);
                epoDocument.Data.CanDelete = modelDocument.CanDelete;
                epoDocument.Data.CanCreateKit = modelDocument.CanCreateKit;
                epoDocument.Data.CanEditKit = modelDocument.CanEditKit;
                epoDocument.Data.CanDisbandKit = modelDocument.CanDisbandKit;
                epoDocument.Data.EquipmentKitId = modelDocument.EquipmentKitId;
                AksiokAddAndUpdateObjectDb.AddAndUpdateFullLoadAksiok(epoDocument.Data, "EpoDocument", idDocument);
                PostAksiok<EfDatabase.ModelAksiok.Aksiok.ValueCharacteristicJson>(GenerateParametersAksiok(AllParameters.ModelParametersAksiok.FirstOrDefault(x => x.IndexExecute == 8), 0, 0, 0, idModel));
                AksiokAddAndUpdateObjectDb.AddAndUpdateFullLoadAksiok<EfDatabase.ModelAksiok.Aksiok.ValueCharacteristicJson>(null, AllParameters.ModelParametersAksiok.FirstOrDefault(x => x.IndexExecute == 11)?.ModelUpdateSql);
                return epoDocument.Data;
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
                Loggers.Log4NetLogger.Error(
                    new Exception("Данные не соответствуют формату int в Ун модели: " + idModel + ", Серийный номер: " + serialNumber + ", Категория: " + idDocument + "."));

                Dispose();
            }
            return null;
        }
        /// <summary>
        /// Обновление комплектности на скомплектованный моделях 
        /// </summary>
        /// <param name="idFirst">Ун компьютера</param>
        /// <param name="idTwo">Ун монитора</param>
        /// <param name="isKit">Комплектность true/false</param>
        /// <param name="equipmentKitId"> Уникальный номер комплекта</param>
        public void UpdateKitsEquipment(int idFirst, int idTwo, bool isKit, long? equipmentKitId)
        {
            AksiokAddAndUpdateObjectDb.UpdateKitsEquipmentAksiok(idFirst, idTwo, isKit, equipmentKitId);
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
