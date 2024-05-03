using System;
using System.IO;
using System.Net;
using LibaryXMLAuto.Inventarization.ModelComparableUserAllSystem;
using LibaryXMLAuto.ReadOrWrite;
using LibaryXMLAuto.ReadOrWrite.SerializationJson;

namespace EfDatabase.Inventory.ComparableSystem.ComparableLotusNotes
{
    /// <summary>
    /// Класс сбора данных из Lotus Notes из сервиса ServiceOutlook по ссылке
    /// http://77068-app065:8585/ServiceOutlook/AllUsersLotusNotes
    /// </summary>
    public class ComparableLotusNotes
    {
        private string UrlLotusNotes { get; set; }


        public ComparableLotusNotes(string urlLotus)
        {
            UrlLotusNotes = urlLotus;
        }

        /// <summary>
        /// Загрузка моделей из Lotus Notes
        /// </summary>
        public ModelComparableUser DownloadModelLotusNotes()
        {
            var json = new SerializeJson();
            var request = (HttpWebRequest)WebRequest.Create(UrlLotusNotes);
            request.Method = "GET";
            request.ContentType = "application/json";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string resultServer;
            using (StreamReader rdr = new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException($"Ошибка запроса в {UrlLotusNotes} вернул NULL!")))
            {
                resultServer = rdr.ReadToEnd();
            }
            response.Dispose();
            return json.JsonDeserializeObjectClassModel<ModelComparableUser>(resultServer);
        }

    }
}
