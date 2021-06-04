using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using EfDatabase.Inventory.Base;
using LibaryXMLAuto.ReadOrWrite;
using LibaryXMLAuto.ReadOrWrite.SerializationJson;
using LogicaSelect = EfDatabaseParametrsModel.LogicaSelect;


namespace EfDatabase.Inventory.SqlModelSelect
{
   public class FullSelectModelInventory : IDisposable
    {
        public InventoryContext Inventory { get; set; }

        public FullSelectModelInventory()
        {
            Inventory?.Dispose();
            Inventory = new InventoryContext();
        }


        /// <summary>
        /// Это для инвентаризации выборка
        /// </summary>
        /// <param name="logic">Логика</param>
        /// <returns></returns>
        public string SqlModelInventory<T>(LogicaSelect logic)
        {
            SerializeJson serializeJson = new SerializeJson();
            object result;
            string resultModel;
            if (logic.IsResultXml)
            {
                var xml = new XmlReadOrWrite();
                Inventory.Database.CommandTimeout = 120000;
                result = Inventory.Database.SqlQuery<string>(logic.SelectUser).ToArray();
                var resultXml = (T) xml.ReadXmlText(string.Join("", (string[])result), typeof(T));
                resultModel = serializeJson.JsonLibrary(resultXml, "dd.MM.yyyy HH:mm");
            }
            else
            {
                result = Inventory.Database.SqlQuery<T>(logic.SelectUser).ToList();
                dynamic expand = new ExpandoObject();
                AddProperty(expand, typeof(T).Name, result); 
                resultModel = serializeJson.JsonLibrary(expand, "dd.MM.yyyy HH:mm");
            }
            return resultModel;
        }

        /// <summary>
        /// Добавление в ExpandoObject динамического названия типа
        /// </summary>
        /// <param name="expando">Динамический объект ExpandoObject</param>
        /// <param name="propertyName">Наименование параметра </param>
        /// <param name="propertyValue">Объект прикрепляемый к модели</param>
        private void AddProperty(ExpandoObject expando, string propertyName, object propertyValue)
        {
            var expandoDict = expando as IDictionary<string, object>;
            if (expandoDict.ContainsKey(propertyName))
                expandoDict[propertyName] = propertyValue;
            else
                expandoDict.Add(propertyName, propertyValue);
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
