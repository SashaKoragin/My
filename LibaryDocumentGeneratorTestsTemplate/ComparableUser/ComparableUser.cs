using System;
using System.IO;
using System.Net;
using LibaryXMLAuto.Inventarization.ModelComparableUserAllSystem;
using LibaryXMLAuto.ReadOrWrite.SerializationJson;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibaryDocumentGeneratorTestsTemplate.ComparableUser
{
    [TestClass()]
    public class ComparableUser
    {

        [TestMethod()]
        public void TestAdLoad()
        {
            var json = new SerializeJson();
            var request = (HttpWebRequest)WebRequest.Create("http://77068-app065:8585/ServiceOutlook/AllUsersLotusNotes");
            request.Method = "GET";
            request.ContentType = "application/json";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string resultServer;
            using (StreamReader rdr = new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException($"Ошибка запроса в вернул NULL!")))
            {
                resultServer = rdr.ReadToEnd();
            }
            response.Dispose();
            var t = (ModelComparableUser)json.JsonDeserializeObjectClass<ModelComparableUser>(resultServer);
        }

    }
}
