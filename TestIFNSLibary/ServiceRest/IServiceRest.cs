using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;
using LibaryXMLAuto.ModelXmlSql.Model.FullSetting;
using LibaryXMLAutoModelXmlSql.Model.FaceError;
using TestIFNSLibary.PostRequest.Face;

namespace TestIFNSLibary.ServiceRest
{
    [ServiceContract]
    interface IServiceRest 
    {
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
        /// Подгрузка сведений на сайт 
        /// </summary>
        /// <param name="setting">Настройки подкгрузки</param>
        /// <returns>Возврат модели JSON в виде строки</returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/LoadTreb", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<string> LoaderReshenie(FullSetting setting);
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
    }
}
