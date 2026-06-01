using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using EfDatabase.Inventory.BaseLogic.Select;
using EfDatabase.Inventory.ReportXml.ModelAksiok;
using EfDatabase.ModelAksiok.ModelAksiokEditAndAdd;
using EfDatabaseXsdSupportNalog;
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
        public AksiokPostGetEditAndAdd(string login, string password)
        {
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
        private void PostEditModel(ParametersUrlModel parametersUrlModel, Encoding encoding)
        {
            for (int i = 0; i < 10; i++)
            {
                try
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
                        Request.Headers.Add(parametersHeaders.Key, parametersHeaders.Value);
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
                        return;
                    }
                }
                catch (WebException webEx)
                {
                    var messageError = string.Empty;
                    using (Stream respStream = webEx.Response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(respStream);
                        messageError = reader.ReadToEnd();
                        Loggers.Log4NetLogger.Error(new Exception(messageError));
                    }
                    Loggers.Log4NetLogger.Info(new Exception($"Блокировка запроса! Повтор запроса на редактирование!"));
                    if (i == 9)
                    {
                        throw new InvalidOperationException(messageError);
                    }
                }
            }
        }
        /// <summary>
        /// Добавление новой модели
        /// </summary>
        /// <param name="parametersUrlModel">Параметры модели</param>
        /// <param name="encoding">Кодировка</param>
        public DataAksiokAddSchemes<T> PostAddModel<T>(ParametersUrlModel parametersUrlModel, Encoding encoding)
        {
            DataAksiokAddSchemes<T> dataModelServerAksiok = new DataAksiokAddSchemes<T>();
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
                Request.Headers.Add(parametersHeaders.Key, parametersHeaders.Value);
            }
            Request.Method = "POST";
            Request.ContentLength = DatesBytes.Length;
            using (var stream = Request.GetRequestStream())
            {
                stream.Write(DatesBytes, 0, DatesBytes.Length);
            }

            try
            {
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
                        dataModelServerAksiok = Newtonsoft.Json.JsonConvert.DeserializeObject<DataAksiokAddSchemes<T>>(data);
                    }
                }
            }
            catch (WebException webEx)
            {
                var messageError = string.Empty;
                using (Stream respStream = webEx.Response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(respStream);
                    messageError = reader.ReadToEnd();
                    Loggers.Log4NetLogger.Error(new Exception(messageError));
                }
                throw new InvalidOperationException(messageError);
            }
            return dataModelServerAksiok;
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
                    NameFile = ConvertUnicodeToRussianTextFileName(Response.Headers["Content-Disposition"]),
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
                    //AksiokFullDataBaseModel.AksiokEditPublicModel.ActDateSpecified = true;
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
                        .Replace("{DeliveryContract}",
                            AksiokFullDataBaseModel.AksiokEditPublicModel.DeliveryContract)
                        .Replace("{ContractSpecificationId}",
                            AksiokFullDataBaseModel.AksiokEditPublicModel.ContractSpecification != null
                                ? AksiokFullDataBaseModel.AksiokEditPublicModel.ContractSpecification.Id.ToString()
                                : String.Empty)
                        .Replace("{EmptyContractReason}",
                            AksiokFullDataBaseModel.AksiokEditPublicModel.EmptyContractReason)
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
                        .Replace("{UsefulLife}", AksiokFullDataBaseModel.AksiokEditPublicModel.UsefulLife.ToString())
                        .Replace("{SmoothRetirementDate}", AksiokFullDataBaseModel.AksiokEditPublicModel.SmoothRetirementDate.ToString("dd.MM.yyyy"))
                        .Replace("{ApplyingDate}", AksiokFullDataBaseModel.AksiokEditPublicModel.ApplyingDate.ToString("dd.MM.yyyy"))
                        .Replace("{IsKit}", AksiokFullDataBaseModel.AksiokEditPublicModel.IsKit.ToString())
                        .Replace("{ServiceStatus}", AksiokFullDataBaseModel.AksiokEditPublicModel.ServiceStatus)
                        .Replace("{ContractOnStoId}",
                            AksiokFullDataBaseModel.AksiokEditPublicModel.ContractOnSto != null
                                ? AksiokFullDataBaseModel.AksiokEditPublicModel.ContractOnSto.Id.ToString()
                                : String.Empty)
                        .Replace("{EquipmentState}",
                            AksiokFullDataBaseModel.AksiokEditPublicModel.EquipmentState.ToString())
                        .Replace("{EquipmentStateSto}",
                            AksiokFullDataBaseModel.AksiokEditPublicModel.EquipmentStateSto.ToString())
                        .Replace("{ComputerName}", WebUtility.HtmlDecode(AksiokFullDataBaseModel.AksiokEditPublicModel.ComputerName))
                        .Replace("{ExpertiseStatus}",
                            AksiokFullDataBaseModel.AksiokEditPublicModel.ExpertiseStatus.ToString())
                        .Replace("{NameMaterialStockTransferFile}", String.Empty)
                        .Replace("{TypeMaterialStockTransferFile}", "application/octet-stream")
                        .Replace("{MaterialStockTransferFile}", String.Empty)
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
        /// <param name="equipmentId">Ун карточки оборудования</param>
        /// <returns></returns>
        private ParametersUrlModel GenerateParametersModelStep3Edit(ParametersUrlModel parametersUrlModel, int equipmentId)
        {
            ParametersUrlModel parameters = new ParametersUrlModel
            {
                Url = parametersUrlModel.Url,
                Accept = parametersUrlModel.Accept,
                ContentType = parametersUrlModel.ContentType,
                Headers = parametersUrlModel.Headers,
                Parameters = parametersUrlModel.Parameters.Replace("{EquipmentId}", equipmentId.ToString())
            };
            return parameters;
        }

        /// <summary>
        /// Генерация параметров для редактирования на шаге 3 редактирование - Разукомплектование карточек
        /// </summary>
        /// <param name="parametersUrlModel">Параметры запроса</param>
        /// <param name="equipmentKitId">Ун комплектации карточки</param>
        /// <returns></returns>
        private ParametersUrlModel GenerateParametersModelStep4Edit(ParametersUrlModel parametersUrlModel, long equipmentKitId)
        {
            ParametersUrlModel parameters = new ParametersUrlModel
            {
                Url = parametersUrlModel.Url,
                Accept = parametersUrlModel.Accept,
                ContentType = parametersUrlModel.ContentType,
                Headers = parametersUrlModel.Headers,
                Parameters = parametersUrlModel.Parameters.Replace("{EquipmentKitId}", equipmentKitId.ToString())
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
        /// Запуск процесса Редактирование или добавление модели изменено 27.05.2024 подготовка к массовому добавлению
        /// </summary>
        /// <param name="aksiokAddAndEdit"></param>
        public string StartEditAndAddAksiok(AksiokAddAndEdit aksiokAddAndEdit)
        {
            SelectSql selectSql = new SelectSql();
            AksiokPostGetSystem.AksiokPostGetSystem aksiokPostGetSystem = new AksiokPostGetSystem.AksiokPostGetSystem(Login, Password);
            try
            {
                if (aksiokAddAndEdit.ParametersModel.ModelRequest == "Edit")
                {
                    if (aksiokAddAndEdit.ParametersModel.IsMassEditing)
                    {
                        var groupTechnical = selectSql.SelectFullEditGroupTechnical(aksiokAddAndEdit.ParametersModel.SerNumber);
                        var countCard = 1;
                        foreach (var serialNumber in groupTechnical)
                        {
                            aksiokAddAndEdit.ParametersModel.SerNumber = serialNumber;
                            aksiokAddAndEdit = selectSql.ModelValidation(aksiokAddAndEdit, aksiokAddAndEdit.ParametersModel.IsMassEditFirstModel);
                            if (string.IsNullOrWhiteSpace(aksiokAddAndEdit.ParametersModel.ErrorServer))
                            {
                                try
                                {
                                   AksiokFullDataBaseModel = selectSql.ReturnModelAksiokEditAndAdd(aksiokAddAndEdit, aksiokAddAndEdit.ParametersModel.IsMassEditFirstModel);
                                    if (aksiokAddAndEdit.ParametersModel.IsMassEditFirstModel)
                                    {
                                        PostEditModel(GenerateParametersModelStep1Edit(allParameters.ModelParametersAksiok[0], aksiokAddAndEdit), Encoding.Default); //Верхнюю модель не отредактировать
                                    }
                                    PostEditModel(GenerateParametersModelStep2Edit(allParameters.ModelParametersAksiok[1]), Encoding.UTF8);
                                    aksiokPostGetSystem.PointSynchronizationAksiok(AksiokFullDataBaseModel.PublicModelValueJson.Id, aksiokAddAndEdit.ParametersModel.IdCard, serialNumber);
                                    SignalRLibary.SignalRinventory.SignalRinventory.SubscribeMessageAksiok("Карточка с серийным номером " + serialNumber + " обработана! Общее количество " + countCard);
                                }
                                catch (Exception e)
                                {
                                    Loggers.Log4NetLogger.Error(e);
                                    SignalRLibary.SignalRinventory.SignalRinventory.SubscribeMessageAksiok("Карточка с серийным номером " + serialNumber + " содержит ошибку АКСИОК " + e.Message);
                                }
                                countCard++;
                            }
                        }
                    }
                    else
                    {
                        AksiokFullDataBaseModel = selectSql.ReturnModelAksiokEditAndAdd(aksiokAddAndEdit);
                        if (AksiokFullDataBaseModel == null)
                            throw new InvalidOperationException("Фатальная ошибка процедура не вернула модель данных проверь параметры!");
                        PostEditModel(GenerateParametersModelStep1Edit(allParameters.ModelParametersAksiok[0], aksiokAddAndEdit), Encoding.Default); //Русские буквы так и не побеждены  Encoding.UTF8 и Encoding.Default
                        PostEditModel(GenerateParametersModelStep2Edit(allParameters.ModelParametersAksiok[1]), Encoding.UTF8);
                        if (aksiokAddAndEdit.KitsEquipment.IsCheckedKits) //Скомплектовать true
                        {
                            PostEditModel(GenerateParametersModelStep3Edit(allParameters.ModelParametersAksiok[6], aksiokAddAndEdit.KitsEquipment.KitsEquipmentServer[0].Id), Encoding.Default);
                        }
                        if (aksiokAddAndEdit.KitsEquipment.IsNotCheckedKits) //Разукомплектовать true
                        {
                            PostEditModel(GenerateParametersModelStep4Edit(allParameters.ModelParametersAksiok[7], aksiokAddAndEdit.ParametersModel.EquipmentKitId), Encoding.Default);
                        }
                        var epoDocumentSynchronization = aksiokPostGetSystem.PointSynchronizationAksiok(AksiokFullDataBaseModel.AksiokEditPublicModel.Id, AksiokFullDataBaseModel.AksiokEditPublicModel.EpoDocument, AksiokFullDataBaseModel.AksiokEditPublicModel.SerialNumber);
                        if (aksiokAddAndEdit.KitsEquipment.IsCheckedKits || aksiokAddAndEdit.KitsEquipment.IsNotCheckedKits)
                        {
                            aksiokPostGetSystem.UpdateKitsEquipment(aksiokAddAndEdit.KitsEquipment.KitsEquipmentServer[0].Id, aksiokAddAndEdit.KitsEquipment.KitsEquipmentServer[1].Id, epoDocumentSynchronization.IsKit, epoDocumentSynchronization.EquipmentKitId);
                        }
                    }
                }
                else
                { 
                    if (aksiokAddAndEdit.ParametersModel.IsMassAdding)
                    {
                        var groupTechnical = selectSql.SelectFullAddGroupTechnical(aksiokAddAndEdit.ParametersModel.SerNumber);
                        var countCard = 1;
                        foreach (var serialNumber in groupTechnical)
                        {
                            aksiokAddAndEdit.ParametersModel.SerNumber = serialNumber;
                            aksiokAddAndEdit = selectSql.ModelValidation(aksiokAddAndEdit);
                            if (string.IsNullOrWhiteSpace(aksiokAddAndEdit.ParametersModel.ErrorServer))
                            {
                                
                                AksiokFullDataBaseModel = selectSql.ReturnModelAksiokEditAndAdd(aksiokAddAndEdit, true,false);
                                var epoDocument = PostAddModel<EfDatabase.ModelAksiok.Aksiok.EpoDocument>(GenerateParametersModelStep1Edit(allParameters.ModelParametersAksiok[5], aksiokAddAndEdit), Encoding.Default);
                                Thread.Sleep(8000);
                                AksiokFullDataBaseModel.AksiokEditPublicModel.Id = epoDocument.Data[0].Id; //Возвращаем ID из добавления
                                aksiokPostGetSystem.PointSynchronizationAksiok(AksiokFullDataBaseModel.AksiokEditPublicModel.Id, AksiokFullDataBaseModel.AksiokEditPublicModel.EpoDocument, AksiokFullDataBaseModel.AksiokEditPublicModel.SerialNumber);
                                AksiokFullDataBaseModel = selectSql.ReturnModelAksiokEditAndAdd(aksiokAddAndEdit);
                                PostEditModel(GenerateParametersModelStep2Edit(allParameters.ModelParametersAksiok[1]), Encoding.UTF8);
                                aksiokPostGetSystem.PointSynchronizationAksiok(AksiokFullDataBaseModel.AksiokEditPublicModel.Id, AksiokFullDataBaseModel.AksiokEditPublicModel.EpoDocument, AksiokFullDataBaseModel.AksiokEditPublicModel.SerialNumber);
                                SignalRLibary.SignalRinventory.SignalRinventory.SubscribeMessageAksiok("Карточка с серийным номером "+serialNumber+ " обработана! Общее количество "+ countCard);
                                countCard++;
                            }
                        }
                    }
                    else
                    {
                        AksiokFullDataBaseModel = selectSql.ReturnModelAksiokEditAndAdd(aksiokAddAndEdit, true,false);
                        var epoDocument = PostAddModel<EfDatabase.ModelAksiok.Aksiok.EpoDocument>(GenerateParametersModelStep1Edit(allParameters.ModelParametersAksiok[5], aksiokAddAndEdit), Encoding.Default);
                        AksiokFullDataBaseModel.AksiokEditPublicModel.Id = epoDocument.Data[0].Id; //Возвращаем ID из добавления
                        aksiokPostGetSystem.PointSynchronizationAksiok(AksiokFullDataBaseModel.AksiokEditPublicModel.Id, AksiokFullDataBaseModel.AksiokEditPublicModel.EpoDocument, AksiokFullDataBaseModel.AksiokEditPublicModel.SerialNumber);
                        AksiokFullDataBaseModel = selectSql.ReturnModelAksiokEditAndAdd(aksiokAddAndEdit);
                        PostEditModel(GenerateParametersModelStep2Edit(allParameters.ModelParametersAksiok[1]), Encoding.UTF8);
                        aksiokPostGetSystem.PointSynchronizationAksiok(AksiokFullDataBaseModel.AksiokEditPublicModel.Id, AksiokFullDataBaseModel.AksiokEditPublicModel.EpoDocument, AksiokFullDataBaseModel.AksiokEditPublicModel.SerialNumber);
                    }
                }
                aksiokPostGetSystem.Dispose();
                selectSql.Dispose();
                return "Обновление и синхронизация данных в АКСИОК прошло Успешно!!!";
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
                aksiokPostGetSystem.Dispose();
                selectSql.Dispose();
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
            return GetFileAksiok(GenerateParametersUploadFile(allParameters.ModelParametersAksiok[4],idFile));
        }
        /// <summary>
        /// Перекодирование русский язык
        /// </summary>
        /// <param name="contentDisposition">Строка непонятная</param>
        /// <returns></returns>
        private string ConvertUnicodeToRussianTextFileName(string contentDisposition)
        {
            var match = Regex.Match(contentDisposition, @"filename=""([^""]+)""");
            if (!match.Success)
                return null;
            string brokenName = match.Groups[1].Value;
            byte[] bytes = Encoding.GetEncoding(28591).GetBytes(brokenName);
            return Encoding.UTF8.GetString(bytes);
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
