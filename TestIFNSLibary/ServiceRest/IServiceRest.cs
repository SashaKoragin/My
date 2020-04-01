using System;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;
using LibaryXMLAuto.ModelServiceWcfCommand.AngularModel;
using LibaryXMLAuto.ModelServiceWcfCommand.ModelPathReport;
using LibaryXMLAuto.ModelXmlSql.Model.FullSetting;
using LibaryXMLAuto.Reports.FullTemplateSheme;
using LibaryXMLAutoModelXmlAuto.MigrationReport;
using LibaryXMLAutoModelXmlSql.Model.FaceError;
using TestIFNSLibary.PostRequest.Face;

namespace TestIFNSLibary.ServiceRest
{
    [ServiceContract(SessionMode = SessionMode.NotAllowed)]
    interface IServiceRest
    {
        [OperationContract(AsyncPattern = true)]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AuthServiceDomain", 
            ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        IAsyncResult BeginSampleMethod(FullSetting setting, AsyncCallback callback, object asyncState);

        ModelUser EndSampleMethod(IAsyncResult result);

        /// <summary>
        /// Получение данных на сайт по средством пост запроса на сервис WCF
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/SqlFaceError", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedResponse)]
        Task<Face> SqlFaceError();
        /// <summary>
        /// Метод добавления лица
        /// </summary>
        /// <param name="face">Модель лиц</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/SqlFaceAdd", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        string AddFace(FaceAdd face);
        /// <summary>
        /// Метод для исключения лица из списка на слияние
        /// </summary>
        /// <param name="id">Номер лица</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/SqlFaceDel", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        string FaceDel(int id);
        /// <summary>
        /// Выполнение процедур в зависимости от setting
        /// </summary>
        /// <param name="setting"></param>
        /// <returns>Возврат сообщения с сервера SQL</returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/StoreProcedureReshenie", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<string> StoreProcedure(FullSetting setting);
        /// <summary>
        /// Загрузка файла на компьютер с сервера
        /// </summary>
        /// <param name="filename">Имя файла</param>
        /// <returns>Файл в птоке</returns>
        [OperationContract]
        [WebGet(UriTemplate = "/ReportFile/{fileName}")]
        Task<Stream> DonloadFile(string filename);
        /// <summary>
        /// Загрузка данных по БДК
        /// </summary>
        /// <param name="setting">Параметры</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/LoadBdk", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<string> LoaderBdk(FullSetting setting);
        /// <summary>
        /// Процедуры БДК
        /// </summary>
        /// <param name="setting">Параметры</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/ProcedureBdk", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<string> StoreProcedureBdk(FullSetting setting);
        /// <summary>
        /// Создание шаблона для исходящих сообщений БДК
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/StartOpenXmlTest", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        string StartNewOpenXmlTemplate(FullSetting setting);
        /// <summary>
        /// Процедуры Предпроверки
        /// </summary>
        /// <param name="setting">Параметры</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/ProcedureSoprovod", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<string> StoreProcedureSoprovod(FullSetting setting);
        /// <summary>
        /// Процедуры Камерального отдела №5
        /// </summary>
        /// <param name="setting">Параметры</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/StoreProcedureKam5", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<Stream> StoreProcedureKam5(FullSetting setting);
        /// <summary>
        /// Создание шаблона для выборок данных
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/ServiceCommand", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<string> ModelServiceCommand(FullSetting setting);
        /// <summary>
        /// Общаяя схема передачи данных на сайт
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/ModelSqlSelect", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<string> ModelSqlSelect(AngularModel command);
        /// <summary>
        /// Общаяя схема передачи файла по средством Angular
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AngularFileDonload", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<Stream> AngularDonloadFile(AngularModelFileDonload angular);
        /// <summary>
        /// Добавление шаблона в БД
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AddTemplate", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<string> AngularAddTemplate(AngularTemplate angular);
        /// <summary>
        /// Создание процессов КРСБ
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/CreteKrsb", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<string> AngularCreateKrsb(FullSetting setting);
        /// <summary>
        /// Выполнения процедур по КРСБ
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/ProcedureAnalizKrsb", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<string> StoreProcedureKrsb(FullSetting setting);
        /// <summary>
        /// Сервуры и компьютеры
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/ServerList", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<string> ServerList(FullSetting setting);
        /// <summary>
        /// Сервис отчетов по ошибкам о миграции НП
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/MigrationError", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<ModelPathReport> MigrationReports(MigrationParse json);

        /// <summary>
        /// Автоматическое формирование заявок на открытие доступа
        /// http://localhost:8181/ServiceRest/GenerateTemplateRule
        /// </summary>
        /// <param name="userRule">Данные с ролями от программы AutomatAis3Full </param>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/GenerateTemplateRule", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<ModelPathReport> GenerateTemplateRule(UserRules userRule);
    }
}
