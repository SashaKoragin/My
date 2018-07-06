using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;
using LibaryXMLAuto.ModelXmlSql.Model.Trebovanie;
using LibaryXMLAutoModelXmlSql.Model.FaceError;
using LibaryXMLAutoModelXmlSql.Model.Trebovanie;
using TestIFNSLibary.PostRequest.Face;

namespace TestIFNSLibary.ServiceRest
{
    [ServiceContract]
    interface IServiceRest 
    {
        /// <summary>
        /// Тестовый пост запрос
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "Test", ResponseFormat = WebMessageFormat.Json)]
        FaceAdd Test();
        /// <summary>
        /// Тестовый адрес для запроса JSON не получилось сделать XML
        /// </summary>
        /// <param name="face">Теытовый JSON</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "Test1")]
        List<Test.Test> Test1();
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
        Task<string> StoreProcedure(Setting setting);

        /// <summary>
        /// Подгрузка сведений на сайт 
        /// </summary>
        /// <param name="setting">Настройки подкгрузки</param>
        /// <returns>Возврат модели JSON в виде строки</returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/LoadTreb", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<string> LoaderReshenie(Setting setting);
    }
}
