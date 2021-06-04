using System.IO;
using System.Net;
using System.Text;
using LibaryXMLAuto.ReadOrWrite.SerializationJson;

namespace LibaryDocumentGenerator.Documents.DocumentMigration
{
   public class ServiceRestLotus
    {
        public ServiceRestLotus()
        {

        }

        /// <summary>
        /// Отправка на сервис Lotus для регистрации и согласования
        /// </summary>
        /// <param name="postAddress">Адрес отправки письма</param>
        /// <param name="requestModel">Модель</param>
        public void ServicePostLotus(string postAddress, object requestModel)
        {
            var json = new SerializeJson();
            var js = json.JsonLibrary(requestModel);
            var request = (HttpWebRequest)WebRequest.Create(postAddress);
            request.Method = "POST";
            request.ContentType = "application/json";
            var body = Encoding.UTF8.GetBytes(js);
            request.ContentLength = body.Length;
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(body, 0, body.Length);
            }
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string resultServer;
                    using (StreamReader rdr = new StreamReader(response.GetResponseStream()))
                    {
                        resultServer = rdr.ReadToEnd();
                    }
                    var conf = (Confirmation)json.JsonDeserializeObjectClass<Confirmation>(resultServer);
                }
            }
        }

    }
}
