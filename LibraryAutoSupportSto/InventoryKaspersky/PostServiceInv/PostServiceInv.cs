using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EfDatabase.Inventory.BaseLogic.KasperskyAddInventory;
using EfDatabase.ReportXml.XsdXmlModelKasperskyApi;
using KasperskyXsdModel;
using KasperskyXsdModel.ModelKaspersky.ShemeViewReport;
using LibaryXMLAuto.ReadOrWrite.SerializationJson;

namespace LibraryAutoSupportSto.InventoryKaspersky.PostServiceInv
{
    public class PostServiceInv : IDisposable
    {
        /// <summary>
        /// Бд для обновления или добавления в БД
        /// </summary>
        private readonly KasperskyAddInventory kasperskyAddInventory = new KasperskyAddInventory();
        /// <summary>
        /// Полный адрес для инвентаризации
        /// </summary>
        public string ServiceInvKaspersky { get; set; }
        /// <summary>
        /// Дополнительный адрес для инвентаризации
        /// </summary>
        public string ServiceInvKasperskyAllHostDrivers { get; set; }

        public string ParameterKasperskyModel { get; set; }

        /// <summary>
        /// Запрос на Касперского
        /// </summary>
        private HttpWebRequest Request { get; set; }
        /// <summary>
        /// Ответ с Касперского
        /// </summary>
        private HttpWebResponse Response { get; set; }


        public PostServiceInv(string serviceInvKaspersky, string parameterKasperskyModel, string serviceInvKasperskyAllHostDrivers)
        {
            ServiceInvKaspersky = serviceInvKaspersky;
            ParameterKasperskyModel = parameterKasperskyModel;
            ServiceInvKasperskyAllHostDrivers = serviceInvKasperskyAllHostDrivers;
        }
        /// <summary>
        /// 
        /// </summary>
        public void StartKasperskyToInventory()
        {
            try
            {
                var json = new SerializeJson();
                var result = GetDataKaspersky().Result;
                var resultAllDrivers = GetAllHostAllDrivers().Result;
                kasperskyAddInventory.AddInventoryKaspersky(ConvertKasperskyToModelInventory(json.JsonDeserializeObjectClassModel<FullShemeReportView<HWInvStorageSrvViewName>>(result)), json.JsonDeserializeObjectClassModel<PublicModelWeb>(resultAllDrivers));
            }
            catch (Exception ex)
            {
                Loggers.Log4NetLogger.Error(ex);
            }
        }
        /// <summary>
        /// Получение инвентаризации касперского
        /// </summary>
        /// <returns></returns>
        private  Task<string> GetDataKaspersky()
        {
            var task = Task.Factory.StartNew(() =>
            {
                var datesBytes = Encoding.UTF8.GetBytes(ParameterKasperskyModel);
                Request = (HttpWebRequest)WebRequest.Create(ServiceInvKaspersky);
                Request.Timeout = 120000;
                Request.Method = "POST";
                Request.ContentType = "application/json";
                Request.ContentLength = datesBytes.Length;
                using (var stream = Request.GetRequestStream())
                {
                    stream.Write(datesBytes, 0, datesBytes.Length);
                }
                Response = (HttpWebResponse)Request.GetResponse();
                string resultServer;
                using (StreamReader rdr = new StreamReader(Response.GetResponseStream() ?? throw new InvalidOperationException($"Ошибка запроса в {ServiceInvKaspersky} вернул NULL!")))
                {
                    resultServer = rdr.ReadToEnd();
                }
                return resultServer;
            });
            task.ConfigureAwait(true);
            return task;
        }
        /// <summary>
        /// Получение дополнительных данных для инвентаризации
        /// </summary>
        /// <returns></returns>
        private Task<string> GetAllHostAllDrivers()
        {
            var task = Task.Factory.StartNew(() =>
            {
                Request = (HttpWebRequest)WebRequest.Create(ServiceInvKasperskyAllHostDrivers);
                Request.Timeout = 120000;
                Request.Method = "POST";
                Request.ContentType = "application/json";
                Request.ContentLength = 0;
                Response = (HttpWebResponse)Request.GetResponse();
                string resultServer;
                using (StreamReader rdr = new StreamReader(Response.GetResponseStream() ?? throw new InvalidOperationException($"Ошибка запроса в {ServiceInvKaspersky} вернул NULL!")))
                {
                    resultServer = rdr.ReadToEnd();
                }
                return resultServer;
            });
            task.ConfigureAwait(true);
            return task;
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

        /// <summary>
        /// Dispose 
        /// </summary>
        public void Dispose()
        {
            Response.Close();
            Response.Dispose();
            kasperskyAddInventory.Dispose();
        }
    }
}
