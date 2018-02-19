using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TestIFNSLibary.WebSevice
{
    [ServiceContract]
    public interface IService
    {

        [WebGet(UriTemplate = "MyMethod/{MyParam}")]
        [OperationContract]
        string MyMethod(string MyParam);

        [WebInvoke(Method = "GET", UriTemplate = "Orn")]
        [OperationContract]
        Stream Orn();

        [WebInvoke(Method = "GET", UriTemplate = "Result")]
        [OperationContract]
        [HttpGet]
        ActionResult Result();
    }
}
