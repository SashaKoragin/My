using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Xml;
using EfDatabase.Inventory.Base;
using LibaryXMLAuto.ReadOrWrite;
using LogicaSelect = EfDatabase.Inventory.Base.LogicaSelect;

namespace EfDatabase.Inventory.BaseLogic.AksiokAddAndUpdateObjectDb
{
   public class AksiokAddAndUpdateObjectDb : IDisposable
    {

        public InventoryContext Inventory { get; set; }
        public LogicaSelect LogicaSelect { get; set; }
        public AksiokAddAndUpdateObjectDb()
        {
            Inventory?.Dispose();
            Inventory = new InventoryContext();
            Inventory.Database.CommandTimeout = 120000;
            LogicaSelect = Inventory.LogicaSelects.First(x => x.Id == 53);
        }
        /// <summary>
        /// Обновление или добавление в таблицу модель оборудования АКСИОК
        /// </summary>
        /// <param name="equipmentModels">Модель оборудования</param>
        /// <param name="typeXml">Тип разборки xml</param>
        /// <param name="idType">Ун типа</param>
        /// <param name="idProduct">Ун продукта</param>
        public void AddAndUpdateFullLoadAksiok<T>(T equipmentModels, string typeXml, int idType = 0, int idProduct = 0)
        {
            XmlReadOrWrite xmlConvert = new XmlReadOrWrite();
            Inventory.Database.ExecuteSqlCommand(LogicaSelect.SelectUser,
                    new SqlParameter(LogicaSelect.SelectedParametr.Split(',')[0], SqlDbType.Xml) { Value = equipmentModels == null ? SqlXml.Null : new SqlXml(new XmlTextReader(xmlConvert.ClassToXml(equipmentModels, equipmentModels.GetType()), XmlNodeType.Document, null)) },
                    new SqlParameter(LogicaSelect.SelectedParametr.Split(',')[1], SqlDbType.VarChar) { Value = typeXml },
                    new SqlParameter(LogicaSelect.SelectedParametr.Split(',')[2], SqlDbType.Int) { Value = idType },
                    new SqlParameter(LogicaSelect.SelectedParametr.Split(',')[3], SqlDbType.Int) { Value = idProduct });
        }
        /// <summary>
        /// Обновление комплектности на скомплектованный моделях 
        /// </summary>
        /// <param name="idFirstAksiokMonitors">Ун компьютера</param>
        /// <param name="idTwoAksiokSysBloks">Ун монитора</param>
        /// <param name="isKit">Комплектность true/false</param>
        public void UpdateKitsEquipmentAksiok(int idFirstAksiokMonitors, int idTwoAksiokSysBloks, bool isKit)
        {
            var updateKit = Inventory.LogicaSelects.First(x => x.Id == 73);
            Inventory.Database.ExecuteSqlCommand(updateKit.SelectUser,
                new SqlParameter(updateKit.SelectedParametr.Split(',')[0], SqlDbType.Int) {Value = idFirstAksiokMonitors },
                             new SqlParameter(updateKit.SelectedParametr.Split(',')[1], SqlDbType.Int) {Value = idTwoAksiokSysBloks },
                             new SqlParameter(updateKit.SelectedParametr.Split(',')[2], SqlDbType.Bit) { Value = isKit });
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
