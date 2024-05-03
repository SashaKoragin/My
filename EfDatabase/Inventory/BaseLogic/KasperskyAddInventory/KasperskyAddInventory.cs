using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Xml;
using EfDatabase.Inventory.Base;
using EfDatabase.ReportXml.XsdXmlModelKasperskyApi;
using LibaryXMLAuto.ReadOrWrite;
using KasperskyXsdModel;

namespace EfDatabase.Inventory.BaseLogic.KasperskyAddInventory
{
    public class KasperskyAddInventory : IDisposable
    {
        public InventoryContext Inventory { get; set; }
        public LogicaSelect LogicaSelect { get; set; }

        public KasperskyAddInventory()
        {
            Inventory?.Dispose();
            Inventory = new InventoryContext();
            Inventory.Database.CommandTimeout = 120000;
            LogicaSelect = Inventory.LogicaSelects.First(x => x.Id == 72);
        }

        /// <summary>
        /// Обновление или добавление в таблицу модель оборудования АКСИОК
        /// </summary>
        /// <param name="kasperskyModel">Модель инвентаризации касперского</param>
        /// <param name="kasperskyModelWeb">Модель дополнительных характеристик компьютера</param>
        public void AddInventoryKaspersky(KasperskyModel kasperskyModel, PublicModelWeb kasperskyModelWeb)
        {
            XmlReadOrWrite xmlConvert = new XmlReadOrWrite();
            Inventory.Database.ExecuteSqlCommand(LogicaSelect.SelectUser,
                new SqlParameter(LogicaSelect.SelectedParametr.Split(',')[0], SqlDbType.Xml)
                {
                    Value = kasperskyModel == null ? SqlXml.Null : new SqlXml(new XmlTextReader(xmlConvert.ClassToXml(kasperskyModel, kasperskyModel.GetType()), XmlNodeType.Document, null)),

                },
                new SqlParameter(LogicaSelect.SelectedParametr.Split(',')[1], SqlDbType.Xml)
                {
                    Value = kasperskyModelWeb == null ? SqlXml.Null : new SqlXml(new XmlTextReader(xmlConvert.ClassToXml(kasperskyModelWeb, kasperskyModelWeb.GetType()), XmlNodeType.Document, null)),

                });
        }


        /// <summary>
        /// Disposing
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Inventory?.Dispose();
                Inventory = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }


}
