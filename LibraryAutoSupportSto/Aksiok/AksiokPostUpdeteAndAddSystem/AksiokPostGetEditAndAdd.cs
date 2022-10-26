using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using EfDatabase.ModelAksiok.ModelAksiokEditAndAdd;
using LibaryXMLAuto.ReadOrWrite.SerializationJson;

namespace LibraryAutoSupportSto.Aksiok.AksiokPostUpdeteAndAddSystem
{
    public class AksiokPostGetEditAndAdd : IDisposable
    {
        private readonly char[] dictionaryRu = {
                                          'А','а','Б','б','В','в','Г','г','Д','д','Е','е','Ё','ё',
                                          'Ж','ж','З','з','И','и','Й','й','К','к','Л','л','М','м',
                                          'Н','н','О','о','П','п','Р','р','С','с','Т','т','У','у',
                                          'Ф','ф','Х','х','Ц','ц','Ч','ч','Ш','ш','Щ','щ','Ъ','ъ',
                                          'Ь','ь','Ы','ы','Э','э','Ю','ю','Я','я','№','«','»'
                                      };
        private AksiokEditAndAddProcedure AksiokFullDataBaseModel { get; set; }
        /// <summary>
        /// Параметры для АКСИОК
        /// </summary>
        private readonly ModelParameterAksiokEditAndAdd allParameters = new ModelParameterAksiokEditAndAdd();
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
        /// Логин
        /// </summary>
        private string Login { get; set; }
        /// <summary>
        /// Пароль
        /// </summary>
        private string Password { get; set; }
        /// <summary>
        /// АКСИОК синхронизация с сайтом
        /// </summary>
        /// <param name="login">Логин</param>
        /// <param name="password">Пароль</param>
        /// <param name="aksiokModelDataBase">Модель с сервера для отправки в АКСИОК</param>
        public AksiokPostGetEditAndAdd(string login, string password, AksiokEditAndAddProcedure aksiokModelDataBase)
        {
            AksiokFullDataBaseModel = aksiokModelDataBase;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            ServicePointManager.ServerCertificateValidationCallback = (senders, certificate, chain, sslPolicyErrors) =>
            {
                return true;
            };
            Login = login;
            Password = password;
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
        /// Редактирование или добавление модели
        /// </summary>
        /// <param name="parametersUrlModel">Параметры модели</param>
        /// <param name="encoding">Кодировка</param>
        private void PostEditAndAddModel(ParametersUrlModel parametersUrlModel, Encoding encoding)
        {
            DatesBytes = encoding.GetBytes(parametersUrlModel.Parameters);
                Request = (HttpWebRequest)WebRequest.Create(parametersUrlModel.Url);
                Request.Accept = parametersUrlModel.Accept;
                Request.Referer = "https://aksiok.dpc.tax.nalog.ru/";
                Request.KeepAlive = true;
                Request.Credentials = MyCache;
                Request.CookieContainer = Сookies;
                Request.Host = "aksiok.dpc.tax.nalog.ru";
                Request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.45 Safari/537.36";
                Request.ContentType = parametersUrlModel.ContentType;
                foreach (var parametersHeaders in parametersUrlModel.Headers)
                {
                    Request.Headers.Add(parametersHeaders.Key,parametersHeaders.Value);
                }
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
                   
                }
        }

        /// <summary>
        /// Запрос файла с АКСИОКА
        /// </summary>
        /// <param name="parametersUrlModel">Параметры модели</param>
        private UploadFileAksiok GetFileAksiok(ParametersUrlModel parametersUrlModel)
        {
            UploadFileAksiok model;
            Request = (HttpWebRequest)WebRequest.Create(parametersUrlModel.Url);
            Request.Accept = parametersUrlModel.Accept;
            Request.Referer = "https://aksiok.dpc.tax.nalog.ru/";
            Request.KeepAlive = true;
            Request.Credentials = MyCache;
            Request.CookieContainer = Сookies;
            Request.Host = "aksiok.dpc.tax.nalog.ru";
            Request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.45 Safari/537.36";
            Request.Method = "GET";
            Response = (HttpWebResponse)Request.GetResponse();
            if (Response.StatusCode != HttpStatusCode.OK) return null;
            using (MemoryStream ms = new MemoryStream())
            {
                Response.GetResponseStream()?.CopyTo(ms);
                model = new UploadFileAksiok()
                {
                    TypeFile = Response.ContentType,
                    NameFile = new ContentDisposition(Response.Headers["content-disposition"]).FileName,
                    File = ms.ToArray()
                };
            }
            return model;
        }
        /// <summary>
        /// Генерация параметров для редактирования на шаге 1 редактирование
        /// </summary>
        /// <param name="parametersUrlModel">Параметры запроса</param>
        /// <param name="aksiokAddAndEdit">Модель проверки файлов</param>
        /// <returns></returns>
        private ParametersUrlModel GenerateParametersModelStep1Edit(ParametersUrlModel parametersUrlModel, AksiokAddAndEdit aksiokAddAndEdit)
        {
            try
            {
                SerializeJson json = new SerializeJson();
                if (aksiokAddAndEdit.ParametersRequestAksiok.FileAkt != null)
                {
                    var stringsName = aksiokAddAndEdit.ParametersRequestAksiok.FileAkt.NameFile.Split('_');
                    AksiokFullDataBaseModel.AksiokEditPublicModel.ActNumber = stringsName[0];
                    AksiokFullDataBaseModel.AksiokEditPublicModel.ActDate = DateTime.ParseExact(stringsName[1], "dd.MM.yyyy", null);
                    AksiokFullDataBaseModel.AksiokEditPublicModel.ActDateSpecified = true;
                }
                ParametersUrlModel parameters = new ParametersUrlModel
                {
                    Url = parametersUrlModel.Url,
                    Accept = parametersUrlModel.Accept,
                    ContentType = parametersUrlModel.ContentType,
                    Headers = parametersUrlModel.Headers,
                    Parameters = parametersUrlModel.Parameters.Replace("{records}",
                            string.Join("",
                                json.JsonLibrary(AksiokFullDataBaseModel.AksiokEditPublicModel, "yyyy-MM-ddTHH:mm:ss",
                                    false).Select(c => dictionaryRu.Any(ru => ru == c) ? $"\\u{(int) c:x4}" : $"{c}")))
                        .Replace("{EquipmentTypeId}",
                            AksiokFullDataBaseModel.AksiokEditPublicModel.EquipmentType.Id.ToString())
                        .Replace("{ProducerId}", AksiokFullDataBaseModel.AksiokEditPublicModel.Producer.Id.ToString())
                        .Replace("{EquipmentModelId}",
                            AksiokFullDataBaseModel.AksiokEditPublicModel.EquipmentModel.Id.ToString())
                        .Replace("{SerialNumber}", AksiokFullDataBaseModel.AksiokEditPublicModel.SerialNumber)
                        .Replace("{ServiceNumber}", AksiokFullDataBaseModel.AksiokEditPublicModel.ServiceNumber)
                        .Replace("{InventoryNumber}", AksiokFullDataBaseModel.AksiokEditPublicModel.InventoryNumber)
                        .Replace("{IndividualServiceNumber}",
                            AksiokFullDataBaseModel.AksiokEditPublicModel.IndividualServiceNumber)
                        .Replace("{YearOfIssue}", AksiokFullDataBaseModel.AksiokEditPublicModel.YearOfIssue.ToString())
                        .Replace("{ExploitationStartYear}",
                            AksiokFullDataBaseModel.AksiokEditPublicModel.ExploitationStartYear != 0
                                ? AksiokFullDataBaseModel.AksiokEditPublicModel.ExploitationStartYear.ToString()
                                : String.Empty)
                        .Replace("{Guarantee}",
                            AksiokFullDataBaseModel.AksiokEditPublicModel.Guarantee.ToString("dd.MM.yyyy"))
                        .Replace("{Comment}", AksiokFullDataBaseModel.AksiokEditPublicModel.Comment)
                        .Replace("{IsKit}", AksiokFullDataBaseModel.AksiokEditPublicModel.IsKit.ToString())
                        .Replace("{ServiceStatus}", AksiokFullDataBaseModel.AksiokEditPublicModel.ServiceStatus)
                        .Replace("{DeliveryContractId}",
                            AksiokFullDataBaseModel.AksiokEditPublicModel.DeliveryContract.Id.ToString())
                        .Replace("{ContractOnStoId}",
                            AksiokFullDataBaseModel.AksiokEditPublicModel.ContractOnSto != null
                                ? AksiokFullDataBaseModel.AksiokEditPublicModel.ContractOnSto.Id.ToString()
                                : String.Empty)
                        .Replace("{EquipmentState}",
                            AksiokFullDataBaseModel.AksiokEditPublicModel.EquipmentState.ToString())
                        .Replace("{EquipmentStateSto}",
                            AksiokFullDataBaseModel.AksiokEditPublicModel.EquipmentStateSto.ToString())
                        .Replace("{ComputerName}", AksiokFullDataBaseModel.AksiokEditPublicModel.ComputerName)
                        .Replace("{ExpertiseStatus}",
                            AksiokFullDataBaseModel.AksiokEditPublicModel.ExpertiseStatus.ToString())
                        .Replace("{NameFileExpertise}",
                            aksiokAddAndEdit.ParametersRequestAksiok.FileExpertise != null
                                ? aksiokAddAndEdit.ParametersRequestAksiok.FileExpertise.NameFile
                                : String.Empty)
                        .Replace("{TypeFileExpertise}",
                            aksiokAddAndEdit.ParametersRequestAksiok.FileExpertise != null
                                ? aksiokAddAndEdit.ParametersRequestAksiok.FileExpertise.TypeFile
                                : "application/octet-stream")
                        .Replace("{ExpertiseFiles}",
                            aksiokAddAndEdit.ParametersRequestAksiok.FileExpertise != null
                                ? Encoding.Default.GetString(
                                    aksiokAddAndEdit.ParametersRequestAksiok.FileExpertise.File)
                                : String.Empty)
                        .Replace("{NameFileAkt}",
                            aksiokAddAndEdit.ParametersRequestAksiok.FileAkt != null
                                ? aksiokAddAndEdit.ParametersRequestAksiok.FileAkt.NameFile
                                : String.Empty)
                        .Replace("{TypeFileAkt}",
                            aksiokAddAndEdit.ParametersRequestAksiok.FileAkt != null
                                ? aksiokAddAndEdit.ParametersRequestAksiok.FileAkt.TypeFile
                                : "application/octet-stream")
                        .Replace("{FileAkt}",
                            aksiokAddAndEdit.ParametersRequestAksiok.FileAkt != null
                                ? Encoding.Default.GetString(aksiokAddAndEdit.ParametersRequestAksiok.FileAkt.File)
                                : String.Empty)
                };
                return parameters;
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }

            return null;
        }



        /// <summary>
        /// Генерация параметров для редактирования на шаге 2 редактирование
        /// </summary>
        /// <param name="parametersUrlModel">Параметры запроса</param>
        /// <returns></returns>
        private ParametersUrlModel GenerateParametersModelStep2Edit(ParametersUrlModel parametersUrlModel)
        {
            SerializeJson json = new SerializeJson();
            ParametersUrlModel parameters = new ParametersUrlModel
            {
                Url = parametersUrlModel.Url,
                Accept = parametersUrlModel.Accept,
                ContentType = parametersUrlModel.ContentType,
                Headers = parametersUrlModel.Headers,
                Parameters = parametersUrlModel.Parameters.Replace("{records}",
                        string.Join("",
                            json.JsonLibrary(AksiokFullDataBaseModel.PublicModelValueJson, "yyyy-MM-ddTHH:mm:ss", false)
                                .Select(c => dictionaryRu.Any(ru => ru == c) ? $"\\u{(int) c:x4}" : $"{c}")))
                    .Replace("{Id}", AksiokFullDataBaseModel.PublicModelValueJson.Id.ToString())
            };
            return parameters;
        }

        /// <summary>
        /// Генерация параметров для редактирования на шаге 3 редактирование/Добавление комплектность 
        /// </summary>
        /// <param name="parametersUrlModel">Параметры запроса</param>
        /// <param name="idFirst">Ун компьютера</param>
        /// <param name="idTwo">Ун монитора</param>
        /// <returns></returns>
        private ParametersUrlModel GenerateParametersModelStep3Edit(ParametersUrlModel parametersUrlModel, int idFirst, int idTwo)
        {
            ParametersUrlModel parameters = new ParametersUrlModel
            {
                Url = parametersUrlModel.Url,
                Accept = parametersUrlModel.Accept,
                ContentType = parametersUrlModel.ContentType,
                Headers = parametersUrlModel.Headers,
                Parameters = parametersUrlModel.Parameters.Replace("{IdFirst}", idFirst.ToString())
                    .Replace("{IdTwo}", idTwo.ToString())
            };
            return parameters;
        }
        /// <summary>
        /// Генерация параметров для выгрузки файла
        /// </summary>
        /// <param name="parametersUrlModel">Параметры запроса</param>
        /// <param name="idFile">Ун файла</param>
        /// <returns></returns>
        private ParametersUrlModel GenerateParametersUploadFile(ParametersUrlModel parametersUrlModel, long idFile)
        {
            ParametersUrlModel parameters = new ParametersUrlModel
            {
                Url = parametersUrlModel.Url.Replace("{idFile}", idFile.ToString()),
                Accept = parametersUrlModel.Accept,
                ContentType = parametersUrlModel.ContentType,
                Headers = parametersUrlModel.Headers
            };
            return parameters;
        }

        /// <summary>
        /// Запуск процесса Редактирование или добавление модели
        /// </summary>
        /// <param name="aksiokAddAndEdit"></param>
        public string StartEditAndAddAksiok(AksiokAddAndEdit aksiokAddAndEdit)
        {
            try
            {
                if (aksiokAddAndEdit.ParametersModel.ModelRequest == "Edit")
                {
                    PostEditAndAddModel(GenerateParametersModelStep1Edit(allParameters.ModelParametersAksiok[0],aksiokAddAndEdit), Encoding.Default); //Русские буквы так и не побеждены 
                    PostEditAndAddModel(GenerateParametersModelStep2Edit(allParameters.ModelParametersAksiok[1]), Encoding.UTF8);
                }
                else
                {
                   //При добавлении новой записи нужно думать (Пока нет таких записей 20.11.2022)
                }
                if (aksiokAddAndEdit.KitsEquipment?.KitsEquipmentServer != null)
                {
                    PostEditAndAddModel(GenerateParametersModelStep3Edit(allParameters.ModelParametersAksiok[2], aksiokAddAndEdit.KitsEquipment.KitsEquipmentServer[0].Id, aksiokAddAndEdit.KitsEquipment.KitsEquipmentServer[1].Id), Encoding.Default);
                }
                AksiokPostGetSystem.AksiokPostGetSystem aksiokPostGetSystem = new AksiokPostGetSystem.AksiokPostGetSystem(Login, Password);
                aksiokPostGetSystem.PointSynchronizationAksiok(AksiokFullDataBaseModel.AksiokEditPublicModel.Id, AksiokFullDataBaseModel.AksiokEditPublicModel.EpoDocument, AksiokFullDataBaseModel.AksiokEditPublicModel.SerialNumber);
                aksiokPostGetSystem.Dispose();
                return "Обновление и синхронизация данных в АКСИОК прошло Успешно!!!";
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
                return e.Message;
            }
        }
        /// <summary>
        /// Выгрузить файл с сервера (Файл экспертизы, Акт списания)
        /// </summary>
        /// <param name="idFile">Ун файла</param>
        /// <returns></returns>
        public UploadFileAksiok UploadFileAksiok(long idFile)
        {
            return GetFileAksiok(GenerateParametersUploadFile(allParameters.ModelParametersAksiok[3],idFile));
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
