using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TestIFNSLibary.WebSevice.ModelPage;

namespace TestIFNSLibary.WebSevice
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Service : IService
    {

        public string MyMethod(string MyParam)
        {
            return MyParam;
        }

        public Stream Orn()
        {
            var html = Page.PageGlavnay;
            if (WebOperationContext.Current != null)
                WebOperationContext.Current.OutgoingResponse.ContentType = "text/html; charset=utf-8";
            var result = new MemoryStream(Encoding.UTF8.GetBytes(html));
            return result;
        }
        public ActionResult Result()
        {
            return new Page();
        }
    }
}
