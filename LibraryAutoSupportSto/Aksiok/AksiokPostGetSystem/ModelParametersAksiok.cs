
namespace LibraryAutoSupportSto.Aksiok.AksiokPostGetSystem
{

    public class AllParameters
    {
        public ModelParametersAksiok[] ModelParametersAksiok =
        {
            new ModelParametersAksiok()
            {
                IndexExecute = 1,
                UrlAksiok = "https://aksiok.dpc.tax.nalog.ru/api/Producer/List?_dc={DateTime}",
                ParametersAksiok = "{\"records\":[],\"actualDate\":\"{Date}\",\"page\":1,\"start\":0,\"limit\":25,\"sort\":[{\"property\":\"Code\",\"direction\":\"ASC\"}]}",
                ModelUpdateSql  = "Producer"
            }, 
            new ModelParametersAksiok()
            {
                IndexExecute = 2,
                UrlAksiok = "https://aksiok.dpc.tax.nalog.ru/api/EquipmentType/list?_dc={DateTime}",
                ParametersAksiok = "{\"records\":[],\"actualDate\":\"{Date}\",\"page\":1,\"start\":0,\"limit\":25,\"sort\":[{\"property\":\"Code\",\"direction\":\"ASC\"}]}",
                ModelUpdateSql  = "EquipmentType"
            },
            new ModelParametersAksiok()
            {
                IndexExecute = 3,
                UrlAksiok = "https://aksiok.dpc.tax.nalog.ru/api/EquipmentModel/ListTree?_dc={DateTime}",
                ParametersAksiok = "{\"records\":[],\"node\":\"{typeId}\",\"nodeLevel\":0,\"rootNode\":null,\"actualDate\":\"{Date}\",\"page\":1,\"start\":0,\"limit\":25,\"id\":\"{typeId}\"}",
                ModelUpdateSql  = null
            },
            new ModelParametersAksiok()
            {
                IndexExecute = 4,
                UrlAksiok = "https://aksiok.dpc.tax.nalog.ru/api/EquipmentModel/ListTree?_dc={DateTime}",
                ParametersAksiok = "{\"records\":[],\"node\":\"{productId}\",\"nodeLevel\":1,\"rootNode\":\"{typeId}\",\"actualDate\":\"{Date}\",\"page\":1,\"start\":0,\"limit\":25,\"id\":\"{productId}\"}",
                ModelUpdateSql  = "EquipmentModel",
            },
            new ModelParametersAksiok()
            {
                IndexExecute = 5,
                UrlAksiok = "https://aksiok.dpc.tax.nalog.ru/api/EpoRegistry/List?_dc={DateTime}",
                ParametersAksiok = "{\"records\":[],\"showArchive\":false,\"page\":1,\"start\":0,\"limit\":500}",
                ModelUpdateSql  = "ModelDocumentType"
            },
            new ModelParametersAksiok()
            {
                IndexExecute = 6,
                UrlAksiok = "https://aksiok.dpc.tax.nalog.ru/api/EquipmentCard/ListEquipments?_dc={DateTime}",
                ParametersAksiok = "{\"records\":[],\"epoDocumentId\":\"{modelDocumentTypeId}\",\"showEliminatedEquipment\":false,\"page\":{page},\"start\":{start},\"limit\":{limit}}",
                ModelUpdateSql  = null
            },
            new ModelParametersAksiok()
            {
                IndexExecute = 7,
                UrlAksiok = "https://aksiok.dpc.tax.nalog.ru/api/EquipmentCard/get?_dc={DateTime}",
                ParametersAksiok = "{\"records\":[],\"id\":\"{modelDocumentId}\"}",
                ModelUpdateSql  = null
            },
            new ModelParametersAksiok()
            {
                IndexExecute = 8,
                UrlAksiok = "https://aksiok.dpc.tax.nalog.ru/api/EquipmentCardAddChar/get?_dc={DateTime}",
                ParametersAksiok = "{\"records\":[],\"id\":\"{modelDocumentId}\"}",
                ModelUpdateSql  = "ValueCharacteristicJson"
            },
            new ModelParametersAksiok(){
                IndexExecute = 9,
                UrlAksiok = "https://aksiok.dpc.tax.nalog.ru/api/EpoContract/ListBySto?_dc={DateTime}",
                ParametersAksiok = "{\"records\":[],\"page\":1,\"start\":0,\"limit\":1000}",
                ModelUpdateSql  = "ContractOnSto"
            },
            new ModelParametersAksiok(){
                IndexExecute = 10,
                UrlAksiok = "https://aksiok.dpc.tax.nalog.ru/api/EquipmentCard/ListEpoContractOnStage?_dc={DateTime}",
                ParametersAksiok = "{\"records\":[],\"EpoDocument\":{modelDocumentId},\"page\":1,\"start\":0,\"limit\":500}",
                ModelUpdateSql  = "DeliveryContract"
            },
            new ModelParametersAksiok()
            {
                IndexExecute = 11,
                UrlAksiok = null,
                ParametersAksiok = null,
                ModelUpdateSql  = "FinishProcess"
            },
            //Фильтрованная модель по Серийному номеру
            new ModelParametersAksiok()
            {
                IndexExecute = 12,
                UrlAksiok = "https://aksiok.dpc.tax.nalog.ru/api/EquipmentCard/ListEquipments?_dc={DateTime}",
                ParametersAksiok = "{\"records\":[],\"epoDocumentId\":\"{modelDocumentTypeId}\",\"showEliminatedEquipment\":false,\"dataFilter\":{\"Group\":2,\"Filters\":[{\"DataIndex\":\"SerialNumber\",\"Value\":\"{serialNumber}\",\"Operand\":6}]},\"page\":{page},\"start\":{start},\"limit\":{limit}}",
                ModelUpdateSql  = null
            },
            new ModelParametersAksiok()
            {
                IndexExecute = 12,
                UrlAksiok = "https://aksiok.dpc.tax.nalog.ru/api/EquipmentCard/ListEquipments?_dc={DateTime}",
                ParametersAksiok = "{\"records\":[],\"epoDocumentId\":\"{modelDocumentTypeId}\",\"showEliminatedEquipment\":false,\"dataFilter\":{\"Group\":2,\"Filters\":[{\"DataIndex\":\"SerialNumber\",\"Value\":\"{serialNumber}\",\"Operand\":6}]},\"page\":{page},\"start\":{start},\"limit\":{limit}}",
                ModelUpdateSql  = null
            },
            new ModelParametersAksiok()
            {
                IndexExecute = 13,
                UrlAksiok = "https://aksiok.dpc.tax.nalog.ru/api/EpoContract/list?_dc={DateTime}",
                ParametersAksiok = "{\"records\":[],\"page\":{page},\"start\":{start},\"limit\":{limit}}",
                ModelUpdateSql  = null
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
