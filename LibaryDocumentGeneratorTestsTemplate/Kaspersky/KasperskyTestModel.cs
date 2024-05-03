using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EfDatabase.ReportXml.XsdXmlModelKasperskyApi;
using KasperskyXsdModel;
using KasperskyXsdModel.ModelKaspersky.ShemeViewReport;
using LibaryXMLAuto.Inventarization.ModelComparableUserAllSystem;
using LibaryXMLAuto.ReadOrWrite;
using LibaryXMLAuto.ReadOrWrite.SerializationJson;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibraryDocumentGeneratorTestsTemplate.Kaspersky
{
    [TestClass]
    public class KasperskyTestModel
    {
        [TestMethod]
        public void TestPostAllHostAllDrivers()
        {
            var xml = new XmlReadOrWrite();
            var json = new SerializeJson();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://77068-app065:8565/ServiceKaspersky/PostAllHostAllDrivers");
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = 0;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string resultServer;
            using (StreamReader rdr = new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException($"Ошибка запроса в вернул NULL!")))
            {
                resultServer = rdr.ReadToEnd();
            }

            var modelServer = json.JsonDeserializeObjectClassModel<PublicModelWeb>(resultServer);
           
            xml.CreateXmlFile("D:\\Testing\\Kaspersky\\XmlManufacturerKasperskyAllDevice.xml", modelServer, modelServer.GetType());
        }

        [TestMethod]
        public void TestPostHostAllDriver()
        {
            var xml = new XmlReadOrWrite();
            var json = new SerializeJson();
            var datesBytes = Encoding.UTF8.GetBytes("{\"CountDataBase\":50000,\"MemoView\":[{\"Description\":\"Object id\",\"FilterType\":null,\"IsNot\":false,\"IsValidMemoModel\":true,\"IsVisible\":true,\"Memo\":\"Id\",\"ModelIsEdit\":true,\"TypeMemo\":\"Integer\",\"ValueMemo\":null},{\"Description\":\"See Types of hardware inventory storage\",\"FilterType\":null,\"IsNot\":false,\"IsValidMemoModel\":true,\"IsVisible\":true,\"Memo\":\"Type\",\"ModelIsEdit\":true,\"TypeMemo\":\"Integer\",\"ValueMemo\":null},{\"Description\":\"See Subtypes of hardware inventory storage\",\"FilterType\":null,\"IsNot\":false,\"IsValidMemoModel\":true,\"IsVisible\":true,\"Memo\":\"SubType\",\"ModelIsEdit\":true,\"TypeMemo\":\"Integer\",\"ValueMemo\":null},{\"Description\":\"Timestamp of creation\",\"FilterType\":null,\"IsNot\":false,\"IsValidMemoModel\":true,\"IsVisible\":true,\"Memo\":\"Created\",\"ModelIsEdit\":true,\"TypeMemo\":\"Time\",\"ValueMemo\":null},{\"Description\":\"Timestamp of last scan\",\"FilterType\":null,\"IsNot\":false,\"IsValidMemoModel\":true,\"IsVisible\":true,\"Memo\":\"LastVisible\",\"ModelIsEdit\":true,\"TypeMemo\":\"Time\",\"ValueMemo\":null},{\"Description\":\"Timestamp of write - off\",\"FilterType\":null,\"IsNot\":false,\"IsValidMemoModel\":true,\"IsVisible\":true,\"Memo\":\"WriteOffDate\",\"ModelIsEdit\":true,\"TypeMemo\":\"Time\",\"ValueMemo\":null},{\"Description\":\"Flag of written off object\",\"FilterType\":null,\"IsNot\":false,\"IsValidMemoModel\":true,\"IsVisible\":true,\"Memo\":\"IsWrittenOff\",\"ModelIsEdit\":true,\"TypeMemo\":\"Boolean\",\"ValueMemo\":null},{\"Description\":\"Inventory number\",\"FilterType\":null,\"IsNot\":false,\"IsValidMemoModel\":true,\"IsVisible\":true,\"Memo\":\"InvNum\",\"ModelIsEdit\":true,\"TypeMemo\":\"String\",\"ValueMemo\":null},{\"Description\":\"User name\",\"FilterType\":null,\"IsNot\":false,\"IsValidMemoModel\":true,\"IsVisible\":true,\"Memo\":\"UserName\",\"ModelIsEdit\":true,\"TypeMemo\":\"String\",\"ValueMemo\":null},{\"Description\":\"Objects placement\",\"FilterType\":null,\"IsNot\":false,\"IsValidMemoModel\":true,\"IsVisible\":true,\"Memo\":\"Placement\",\"ModelIsEdit\":true,\"TypeMemo\":\"String\",\"ValueMemo\":null},{\"Description\":\"Object price\",\"FilterType\":null,\"IsNot\":false,\"IsValidMemoModel\":true,\"IsVisible\":true,\"Memo\":\"Price\",\"ModelIsEdit\":true,\"TypeMemo\":\"LongLong\",\"ValueMemo\":null},{\"Description\":\"Purchase timestamp\",\"FilterType\":null,\"IsNot\":false,\"IsValidMemoModel\":true,\"IsVisible\":true,\"Memo\":\"PurchaseDate\",\"ModelIsEdit\":true,\"TypeMemo\":\"Time\",\"ValueMemo\":null},{\"Description\":\"Flag of corporative object\",\"FilterType\":null,\"IsNot\":false,\"IsValidMemoModel\":true,\"IsVisible\":true,\"Memo\":\"Corporative\",\"ModelIsEdit\":true,\"TypeMemo\":\"Boolean\",\"ValueMemo\":null},{\"Description\":\"Objects name\",\"FilterType\":null,\"IsNot\":false,\"IsValidMemoModel\":true,\"IsVisible\":true,\"Memo\":\"Name\",\"ModelIsEdit\":true,\"TypeMemo\":\"String\",\"ValueMemo\":null},{\"Description\":\"Objects description\",\"FilterType\":null,\"IsNot\":false,\"IsValidMemoModel\":true,\"IsVisible\":true,\"Memo\":\"Description\",\"ModelIsEdit\":true,\"TypeMemo\":\"String\",\"ValueMemo\":null},{\"Description\":\"Objects manufacturer\",\"FilterType\":null,\"IsNot\":false,\"IsValidMemoModel\":true,\"IsVisible\":true,\"Memo\":\"Manufacturer\",\"ModelIsEdit\":true,\"TypeMemo\":\"String\",\"ValueMemo\":null},{\"Description\":\"Objects serial number\",\"FilterType\":null,\"IsNot\":false,\"IsValidMemoModel\":true,\"IsVisible\":true,\"Memo\":\"SerialNumber\",\"ModelIsEdit\":true,\"TypeMemo\":\"String\",\"ValueMemo\":null},{\"Description\":\"CPU name\",\"FilterType\":null,\"IsNot\":false,\"IsValidMemoModel\":true,\"IsVisible\":true,\"Memo\":\"CPU\",\"ModelIsEdit\":true,\"TypeMemo\":\"String\",\"ValueMemo\":null},{\"Description\":\"Memory size in MB\",\"FilterType\":null,\"IsNot\":false,\"IsValidMemoModel\":true,\"IsVisible\":true,\"Memo\":\"MemorySize\",\"ModelIsEdit\":true,\"TypeMemo\":\"Integer\",\"ValueMemo\":null},{\"Description\":\"Disk size in GB\",\"FilterType\":null,\"IsNot\":false,\"IsValidMemoModel\":true,\"IsVisible\":true,\"Memo\":\"DiskSize\",\"ModelIsEdit\":true,\"TypeMemo\":\"Integer\",\"ValueMemo\":null},{\"Description\":\"Motherboard name\",\"FilterType\":null,\"IsNot\":false,\"IsValidMemoModel\":true,\"IsVisible\":true,\"Memo\":\"MotherBoard\",\"ModelIsEdit\":true,\"TypeMemo\":\"String\",\"ValueMemo\":null},{\"Description\":\"Object VIDPID\",\"FilterType\":null,\"IsNot\":false,\"IsValidMemoModel\":true,\"IsVisible\":true,\"Memo\":\"VidPid\",\"ModelIsEdit\":true,\"TypeMemo\":\"String\",\"ValueMemo\":null},{\"Description\":\"Removable device capacity in bytes\",\"FilterType\":null,\"IsNot\":false,\"IsValidMemoModel\":true,\"IsVisible\":true,\"Memo\":\"Capacity\",\"ModelIsEdit\":true,\"TypeMemo\":\"Integer\",\"ValueMemo\":null},{\"Description\":\"MAC address(binary)\",\"FilterType\":null,\"IsNot\":false,\"IsValidMemoModel\":true,\"IsVisible\":true,\"Memo\":\"Mac\",\"ModelIsEdit\":true,\"TypeMemo\":\"Binary\",\"ValueMemo\":null},{\"Description\":\"MAC address\",\"FilterType\":null,\"IsNot\":false,\"IsValidMemoModel\":true,\"IsVisible\":true,\"Memo\":\"StrMac\",\"ModelIsEdit\":true,\"TypeMemo\":\"String\",\"ValueMemo\":null},{\"Description\":\"Managed device OS\",\"FilterType\":null,\"IsNot\":false,\"IsValidMemoModel\":true,\"IsVisible\":true,\"Memo\":\"OS\",\"ModelIsEdit\":true,\"TypeMemo\":\"String\",\"ValueMemo\":null},{\"Description\":\"Active directory object id\",\"FilterType\":null,\"IsNot\":false,\"IsValidMemoModel\":true,\"IsVisible\":true,\"Memo\":\"AdObject\",\"ModelIsEdit\":true,\"TypeMemo\":\"String\",\"ValueMemo\":null},{\"Description\":\"Active directory object display name\",\"FilterType\":null,\"IsNot\":false,\"IsValidMemoModel\":true,\"IsVisible\":true,\"Memo\":\"AdObjectDN\",\"ModelIsEdit\":true,\"TypeMemo\":\"String\",\"ValueMemo\":null}],\"ModelFilters\":[{\"IsAndOr\":false,\"NameCondition\":\"Условие - ИЛИ\",\"MemoView\":[{\"Description\":\"Objects name\",\"FilterType\":{\"FilterLogicSymbol\":\" = \",\"FilterLogicText\":\"Equal\",\"FilterTypeIndex\":0,\"FilterTypeText\":\"Равно / Содержит\"},\"IsNot\":false,\"IsValidMemoModel\":false,\"IsVisible\":true,\"Memo\":\"Name\",\"ModelIsEdit\":false,\"TypeMemo\":\"String\",\"ValueMemo\":\"77068-*\"},{\"Description\":\"Objects name\",\"FilterType\":{\"FilterLogicSymbol\":\" = \",\"FilterLogicText\":\"Equal\",\"FilterTypeIndex\":0,\"FilterTypeText\":\"Равно / Содержит\"},\"IsNot\":false,\"IsValidMemoModel\":false,\"IsVisible\":true,\"Memo\":\"Name\",\"ModelIsEdit\":false,\"TypeMemo\":\"String\",\"ValueMemo\":\"i7751-*\"}],\"ModelFilters\":[]}],\"NameSchemeView\":\"HWInvStorageSrvViewName\"}");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://77068-app065:8565/ServiceKaspersky/PostModelInventoryService");
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = datesBytes.Length;
            using (var stream = request.GetRequestStream())
            {
               
                stream.Write(datesBytes, 0, datesBytes.Length);
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string resultServer;
            using (StreamReader rdr = new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException($"Ошибка запроса в вернул NULL!")))
            {
                resultServer = rdr.ReadToEnd();
            }

            var modelServer = ConvertKasperskyToModelInventory(json.JsonDeserializeObjectClassModel<FullShemeReportView<HWInvStorageSrvViewName>>(resultServer));

            xml.CreateXmlFile("D:\\Testing\\Kaspersky\\XmlManufacturerKaspersky.xml", modelServer, modelServer.GetType());
        }

        /// <summary>
        /// Конвертация модели касперского в модель инвентаризации
        /// </summary>
        /// <param name="modelInventoryKaspersky">Модель с сервиса касперского</param>
        /// <returns></returns>
        private KasperskyModel ConvertKasperskyToModelInventory(FullShemeReportView<HWInvStorageSrvViewName> modelInventoryKaspersky)
        {
            var model = new KasperskyModel
            {
                InvStorageSrvView = new InvStorageSrvView[modelInventoryKaspersky.pRecords.KLCSP_ITERATOR_ARRAY.Count]
            };
            var i = 0;
            foreach (var kspIteratorArray in modelInventoryKaspersky.pRecords.KLCSP_ITERATOR_ARRAY.ToArray())
            {
                model.InvStorageSrvView[i] = new InvStorageSrvView
                {
                    Id = Convert.ToInt64(kspIteratorArray.value.Id ?? 0),
                    Name = kspIteratorArray.value.Name,
                    Description = kspIteratorArray.value.Description,
                    Manufacturer = kspIteratorArray.value.Manufacturer,
                    CpuType = kspIteratorArray.value.CPU,
                    CpuMHz = kspIteratorArray.value.CPU != null
                        ? Regex.Match(kspIteratorArray.value.CPU, @"([0-9]+.[0-9]+GHz)").Value
                        : null,
                    MemorySize = kspIteratorArray.value.MemorySize ?? 0,
                    DiskSize = kspIteratorArray.value.DiskSize ?? 0,
                    MotherBoard = kspIteratorArray.value.MotherBoard,
                    Mac = kspIteratorArray.value.StrMac,
                    OperationSystem = kspIteratorArray.value.OS
                };
                i++;
            }
            return model;
        }

    }
}
