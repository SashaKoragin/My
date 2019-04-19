using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using EfDatabase.Inventarization.Base;
using SqlLibaryIfns.Inventarization.ModelParametr;

namespace TestIFNSLibary.Inventarka
{
   [ServiceContract]
   public interface IInventarka
   {
        /// <summary>
        /// http://localhost:8182/Inventarka/Autification
        /// Авторизация для ресурса Инвентаризация
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/Authorization", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<string> Authorization(User user);

        /// <summary>
        /// Выбор всех пользователей
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AllUsers", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
       Task<string> SelectAllUsers(ModelParametr model);
   }
}
