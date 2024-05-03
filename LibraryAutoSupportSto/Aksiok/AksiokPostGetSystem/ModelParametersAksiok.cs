
namespace LibraryAutoSupportSto.Aksiok.AksiokPostGetSystem
{

    public class AllParameters
    {
        public ModelParametersAksiok[] ModelParametersAksiok =
        {
            new ModelParametersAksiok()
            {
                IndexExecute = 1,
                UrlAksiok = "https://aksiok.dpc.tax.nalog.ru/api/Producer/List?_dc=1650353905544",
                ParametersAksiok = "{\"records\":[],\"actualDate\":\"{Date}\",\"page\":1,\"start\":0,\"limit\":25,\"sort\":[{\"property\":\"Code\",\"direction\":\"ASC\"}]}",
                ModelUpdateSql  = "Producer"
            }, 
            new ModelParametersAksiok()
            {
                IndexExecute = 2,
                UrlAksiok = "https://aksiok.dpc.tax.nalog.ru/api/EquipmentType/list?_dc=1650354780285",
                ParametersAksiok = "{\"records\":[],\"actualDate\":\"{Date}\",\"page\":1,\"start\":0,\"limit\":25,\"sort\":[{\"property\":\"Code\",\"direction\":\"ASC\"}]}",
                ModelUpdateSql  = "EquipmentType"
            },
            new ModelParametersAksiok()
            {
                IndexExecute = 3,
                UrlAksiok = "https://aksiok.dpc.tax.nalog.ru/api/EquipmentModel/ListTree?_dc=1650356158346",
                ParametersAksiok = "{\"records\":[],\"node\":\"{typeId}\",\"nodeLevel\":0,\"rootNode\":null,\"actualDate\":\"{Date}\",\"page\":1,\"start\":0,\"limit\":25,\"id\":\"{typeId}\"}",
                ModelUpdateSql  = null
            },
            new ModelParametersAksiok()
            {
                IndexExecute = 4,
                UrlAksiok = "https://aksiok.dpc.tax.nalog.ru/api/EquipmentModel/ListTree?_dc=1650356158346",
                ParametersAksiok = "{\"records\":[],\"node\":\"{productId}\",\"nodeLevel\":1,\"rootNode\":\"{typeId}\",\"actualDate\":\"{Date}\",\"page\":1,\"start\":0,\"limit\":25,\"id\":\"{productId}\"}",
                ModelUpdateSql  = "EquipmentModel",
            },
            new ModelParametersAksiok()
            {
                IndexExecute = 5,
                UrlAksiok = "https://aksiok.dpc.tax.nalog.ru/api/EpoRegistry/List?_dc=1649674857153",
                ParametersAksiok = "{\"records\":[],\"page\":1,\"start\":0,\"limit\":1000}",
                ModelUpdateSql  = "ModelDocumentType"
            },
            new ModelParametersAksiok()
            {
                IndexExecute = 6,
                UrlAksiok = "https://aksiok.dpc.tax.nalog.ru/api/EquipmentCard/ListEquipments?_dc=1649676212262",
                ParametersAksiok = "{\"records\":[],\"epoDocumentId\":\"{modelDocumentTypeId}\",\"showEliminatedEquipment\":true,\"page\":1,\"start\":0,\"limit\":500}",
                ModelUpdateSql  = null
            },
            new ModelParametersAksiok()
            {
                IndexExecute = 7,
                UrlAksiok = "https://aksiok.dpc.tax.nalog.ru/api/EquipmentCard/get?_dc=1649676459600",
                ParametersAksiok = "{\"records\":[],\"id\":\"{modelDocumentId}\"}",
                ModelUpdateSql  = "EpoDocument"
            },
            new ModelParametersAksiok()
            {
                IndexExecute = 8,
                UrlAksiok = "https://aksiok.dpc.tax.nalog.ru/api/EquipmentCardAddChar/get?_dc=1649676665906",
                ParametersAksiok = "{\"records\":[],\"id\":\"{modelDocumentId}\"}",
                ModelUpdateSql  = "ValueCharacteristicJson"
            },
            new ModelParametersAksiok()
            {
                IndexExecute = 9,
                UrlAksiok = null,
                ParametersAksiok = null,
                ModelUpdateSql  = "FinishProcess"
            },
        };
    }

   public class ModelParametersAksiok
   {

        public int IndexExecute { get; set; }
        /// <summary>
        /// Адрес Аксиока
        /// </summary>
        public string UrlAksiok { get; set; }
        /// <summary>
        /// Параметры АКСИОКА
        /// </summary>
        public string ParametersAksiok { get; set; }
        /// <summary>
        /// Переключатель обновления модели в Sql
        /// </summary>
        public string ModelUpdateSql { get; set; }
   }
}
