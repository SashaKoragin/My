using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EfDatabase.Inventory.BaseLogic.AksiokAddAndUpdateObjectDb;
using EfDatabase.Inventory.ReportXml.ModelAksiok;
using EfDatabase.ModelAksiok.Aksiok;
using LibraryAutoSupportSto.Aksiok.AksiokPostGetSystem;


namespace LibraryDocumentGeneratorTestsTemplate.Aksiok
{
    [TestClass()]
   public class TestAksiokToModel
   {
       private char[] dictionaryRu = {
           'А','а','Б','б','В','в','Г','г','Д','д','Е','е','Ё','ё',
           'Ж','ж','З','з','И','и','Й','й','К','к','Л','л','М','м',
           'Н','н','О','о','П','п','Р','р','С','с','Т','т','У','у',
           'Ф','ф','Х','х','Ц','ц','Ч','ч','Ш','ш','Щ','щ','Ъ','ъ',
           'Ь','ь','Ы','ы','Э','э','Ю','ю','Я','я','№','«','»'
       };
        public HttpWebRequest Request { get; set; }

        /// <summary>
        /// Ответ с Support
        /// </summary>
        private HttpWebResponse Response { get; set; }

        /// <summary>
        /// Параметры файла
        /// </summary>
        private byte[] DatesBytesFile { get; set; }

        [TestMethod()]
        public void TestDeserializationAksiok()
        {
            var stringsName = "11.11.2021";
            var y = DateTime.ParseExact(stringsName, "dd.MM.yyyy", null);
        }

        [TestMethod()]
        public void TestDeserializationAksiok2()
        {
            var userLogin = "7751-00-099";
            var passwordUser = "Qwerty123456@";
            var aksiok = new AksiokPostGetSystem(userLogin, passwordUser);
            aksiok.StartUpdateAksiok();
        }
        [TestMethod()]
        public void ParseData()
        {
            try
            {
                //
                DataAksiokAddSchemes<EfDatabase.ModelAksiok.Aksiok.EpoDocument> dataModelServerAksiok = new DataAksiokAddSchemes<EfDatabase.ModelAksiok.Aksiok.EpoDocument>();
                string data = "{\"success\":true,\"IsMessageAggregatorResponse\":true,\"errors\":[],\"warnings\":[],\"notifications\":[],\"data\":[{\"EpoDocument\":82827,\"Id\":1291086,\"ComputerName\":\"\",\"EquipmentType\":{\"Code\":\"32\",\"Name\":\"Программно - аппаратный коммутатор VoIP\",\"Id\":161},\"Producer\":{\"Code\":\"2442\",\"Name\":\"Сател ООО\",\"Id\":2340},\"EquipmentModel\":{\"Code\":\"29204\",\"Name\":\"МикроРТУ - 500\",\"Id\":24061},\"SerialNumber\":\"0013297998\",\"Identifier\":\"00000000000001129500\",\"InventoryNumber\":\"101340000002116\",\"IsKit\":false,\"EquipmentStateSto\":40,\"EquipmentState\":50,\"ActNumber\":\"\",\"ActDate\":null,\"DateOfStatement\":null,\"File\":null,\"ExpertiseStatus\":10,\"ExpertiseFile\":null,\"IsArm\":false,\"IncludedInEqSettings\":false,\"IsSharedUsage\":false,\"IsSmallCost\":false,\"IsOffBalanceAccount\":false,\"ServiceNumber\":\"\",\"IndividualServiceNumber\":\"\",\"NotOnBalance\":null,\"YearOfIssue\":2024,\"ExploitationStartYear\":2024,\"Guarantee\":\"2024-06-20T00:00:00.0000000+03:00\",\"Comment\":\"\",\"ServiceStatus\":\"\",\"DeliveryContract\":null,\"CanDownloadDeliveryContractFiles\":false,\"ContractOnSto\":null,\"CanDownloadContractOnStoFiles\":false,\"OsActualVersion\":null,\"ActualOsVersion\":null,\"Appointment\":null,\"TypeOfUse\":null,\"Division\":null,\"ModelInDeliveryAccordance\":null,\"Building\":null,\"FloorLocation\":null,\"RoomLocation\":null,\"PsVersion\":null,\"IpAddress\":null,\"BoardsNumber\":null,\"FreeBoardsNumber\":null,\"InternalAnalogNumber\":null,\"InternalDigitNumber\":null,\"TypeOfAnalogConnection\":null,\"TypeOfDigitConnection\":null,\"AdminsCount\":null,\"KeyNumber\":null,\"RequiresReplacement\":false}]}";
                dataModelServerAksiok = Newtonsoft.Json.JsonConvert.DeserializeObject<DataAksiokAddSchemes<EfDatabase.ModelAksiok.Aksiok.EpoDocument>>(data);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}
