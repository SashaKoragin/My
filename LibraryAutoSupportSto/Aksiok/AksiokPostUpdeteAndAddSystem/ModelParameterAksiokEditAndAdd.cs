using System.Collections.Generic;

namespace LibraryAutoSupportSto.Aksiok.AksiokPostUpdeteAndAddSystem
{
    public class ModelParameterAksiokEditAndAdd
    {
        public ParametersUrlModel[] ModelParametersAksiok =
        {
            //Общее обновление
            new ParametersUrlModel()
            {
                Url = "https://aksiok.dpc.tax.nalog.ru/api/EquipmentCard/update",
                Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9",
                ContentType = "multipart/form-data; boundary=----WebKitFormBoundarya7QqhhYfHVhNL92g",
                Headers = new Dictionary<string, string>()
                {
                    {"Cache-Control", "max-age=0"},
                    {"Accept-Encoding", "gzip, deflate, br"},
                    {"Accept-Language", "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7"},
                    {"Origin", "https://aksiok.dpc.tax.nalog.ru"},
                    {"sec-ch-ua", "Not A; Brand\";v=\"99\", \"Chromium\"; v=\"102\", \"Google Chrome\"; v=\"102\""},
                    {"sec-ch-ua-mobile", "?0"},
                    {"sec-ch-ua-platform", "Windows"},
                    {"Sec-Fetch-Dest", "iframe"},
                    {"Sec-Fetch-Mode", "navigate"},
                    {"Sec-Fetch-Site", "same-origin"},
                    {"Sec-Fetch-User", "?1" },
                    {"Upgrade-Insecure-Requests", "1"},
                },
                Parameters = "------WebKitFormBoundarya7QqhhYfHVhNL92g\r\n" +
                              "Content-Disposition: form-data; name=\"records\"\r\n\r\n"+
                              "[{records}]\r\n"+
                              "------WebKitFormBoundarya7QqhhYfHVhNL92g\r\n"+
                              "Content-Disposition: form-data; name=\"EquipmentType\"\r\n\r\n"+
                              "{EquipmentTypeId}\r\n"+
                              "------WebKitFormBoundarya7QqhhYfHVhNL92g\r\n"+
                              "Content-Disposition: form-data; name=\"Producer\"\r\n\r\n"+
                              "{ProducerId}\r\n"+
                              "------WebKitFormBoundarya7QqhhYfHVhNL92g\r\n"+
                              "Content-Disposition: form-data; name=\"EquipmentModel\"\r\n\r\n"+
                              "{EquipmentModelId}\r\n"+
                              "------WebKitFormBoundarya7QqhhYfHVhNL92g\r\n"+
                              "Content-Disposition: form-data; name=\"SerialNumber\"\r\n\r\n"+
                              "{SerialNumber}\r\n"+
                              "------WebKitFormBoundarya7QqhhYfHVhNL92g\r\n"+
                              "Content-Disposition: form-data; name=\"ServiceNumber\"\r\n\r\n"+
                              "{ServiceNumber}\r\n"+
                              "------WebKitFormBoundarya7QqhhYfHVhNL92g\r\n"+
                              "Content-Disposition: form-data; name=\"InventoryNumber\"\r\n\r\n"+
                              "{InventoryNumber}\r\n"+
                              "------WebKitFormBoundarya7QqhhYfHVhNL92g\r\n"+
                              "Content-Disposition: form-data; name=\"IndividualServiceNumber\"\r\n\r\n"+
                              "{IndividualServiceNumber}\r\n"+
                              "------WebKitFormBoundarya7QqhhYfHVhNL92g\r\n"+
                              "Content-Disposition: form-data; name=\"YearOfIssue\"\r\n\r\n"+
                              "{YearOfIssue}\r\n"+
                              "------WebKitFormBoundarya7QqhhYfHVhNL92g\r\n"+
                              "Content-Disposition: form-data; name=\"ExploitationStartYear\"\r\n\r\n"+
                              "{ExploitationStartYear}\r\n"+
                              "------WebKitFormBoundarya7QqhhYfHVhNL92g\r\n"+
                              "Content-Disposition: form-data; name=\"Guarantee\"\r\n\r\n"+
                              "{Guarantee}\r\n"+
                              "------WebKitFormBoundarya7QqhhYfHVhNL92g\r\n"+
                              "Content-Disposition: form-data; name=\"Comment\"\r\n\r\n"+
                              "{Comment}\r\n"+
                              "------WebKitFormBoundarya7QqhhYfHVhNL92g\r\n"+
                              "Content-Disposition: form-data; name=\"IsKit\"\r\n\r\n"+
                              "{IsKit}\r\n"+
                              "------WebKitFormBoundarya7QqhhYfHVhNL92g\r\n"+
                              "Content-Disposition: form-data; name=\"ServiceStatus\"\r\n\r\n"+
                              "{ServiceStatus}\r\n"+
                              "------WebKitFormBoundarya7QqhhYfHVhNL92g\r\n"+
                              "Content-Disposition: form-data; name=\"DeliveryContract\"\r\n\r\n"+
                              "{DeliveryContractId}\r\n"+
                              "------WebKitFormBoundarya7QqhhYfHVhNL92g\r\n"+
                              "Content-Disposition: form-data; name=\"ContractOnSto\"\r\n\r\n"+
                              "{ContractOnStoId}\r\n"+
                              "------WebKitFormBoundarya7QqhhYfHVhNL92g\r\n"+
                              "Content-Disposition: form-data; name=\"EquipmentState\"\r\n\r\n"+
                              "{EquipmentState}\r\n"+
                              "------WebKitFormBoundarya7QqhhYfHVhNL92g\r\n"+
                              "Content-Disposition: form-data; name=\"EquipmentStateSto\"\r\n\r\n"+
                              "{EquipmentStateSto}\r\n"+
                              "------WebKitFormBoundarya7QqhhYfHVhNL92g\r\n"+
                              "Content-Disposition: form-data; name=\"ComputerName\"\r\n\r\n"+
                              "{ComputerName}\r\n"+
                              "------WebKitFormBoundarya7QqhhYfHVhNL92g\r\n"+
                              "Content-Disposition: form-data; name=\"ExpertiseStatus\"\r\n\r\n"+
                              "{ExpertiseStatus}\r\n"+
                              "------WebKitFormBoundarya7QqhhYfHVhNL92g\r\n"+
                              "Content-Disposition: form-data; name=\"ExpertiseFile\"; filename=\"{NameFileExpertise}\"\r\n"+
                              "Content-Type: {TypeFileExpertise}\r\n\r\n"+
                              "{ExpertiseFiles}\r\n"+
                              "------WebKitFormBoundarya7QqhhYfHVhNL92g\r\n"+
                              "Content-Disposition: form-data; name=\"File\"; filename=\"{NameFileAkt}\"\r\n"+
                              "Content-Type: {TypeFileAkt}\r\n\r\n"+
                              "{FileAkt}\r\n"+
                              "------WebKitFormBoundarya7QqhhYfHVhNL92g--"
            },
            //Обновление дополнительных параметров
            new ParametersUrlModel()
            {
                Url = "https://aksiok.dpc.tax.nalog.ru/api/EquipmentCardAddChar/update?_dc=1656321164823",
                Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9",
                ContentType = "application/json",
                Headers = new Dictionary<string, string>()
                {
                    {"Accept-Encoding", "gzip, deflate, br"},
                    {"Accept-Language", "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7"},
                    {"X-Requested-With", "XMLHttpRequest"},
                    {"Origin", "https://aksiok.dpc.tax.nalog.ru"},
                    {"sec-ch-ua", "Not A; Brand\";v=\"99\", \"Chromium\"; v=\"102\", \"Google Chrome\"; v=\"102\""},
                    {"sec-ch-ua-mobile", "?0"},
                    {"sec-ch-ua-platform", "Windows"},
                    {"Sec-Fetch-Dest", "empty"},
                    {"Sec-Fetch-Mode", "cors"},
                    {"Sec-Fetch-Site", "same-origin"}
                },
                Parameters = "{\"records\":[{records}],\"id\":{Id}}"
            },
            new ParametersUrlModel()
            {
                Url = "https://aksiok.dpc.tax.nalog.ru/api/EquipmentKit/FormKit",
                Accept = "*/*",
                ContentType = "application/json",
                Headers = new Dictionary<string, string>()
                {
                    {"Accept-Encoding", "gzip, deflate, br"},
                    {"Accept-Language", "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7"},
                    {"X-Requested-With", "XMLHttpRequest"},
                    {"Origin", "https://aksiok.dpc.tax.nalog.ru"},
                    {"sec-ch-ua", "Not A; Brand\";v=\"99\", \"Chromium\"; v=\"102\", \"Google Chrome\"; v=\"102\""},
                    {"sec-ch-ua-mobile", "?0"},
                    {"sec-ch-ua-platform", "Windows"},
                    {"Sec-Fetch-Dest", "empty"},
                    {"Sec-Fetch-Mode", "cors"},
                    {"Sec-Fetch-Site", "same-origin"}
                },
                Parameters = "{\"ids\":[{IdFirst},{IdTwo}]}"
            },
            new ParametersUrlModel()
            {
                Url = "https://aksiok.dpc.tax.nalog.ru/action/FileUpload/Download?id={idFile}&inlineExtensions=",
                Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9",
                ContentType = null
            }
        };

    }

   public class ParametersUrlModel
   {
       public string Url { get; set; }

       public string Accept { get; set; }

       public string ContentType { get; set; }

       public Dictionary<string,string> Headers { get; set; }

       public string Parameters { get; set; }
   }
}
