using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using EfDatabase.Inventory.Base;
using LibaryXMLAuto.Inventarization.ModelComparableUserAllSystem;

namespace EfDatabase.Inventory.BaseLogic.ComparableFileAddToDataBase
{
   public class ComparableFileAddToDataBase : IDisposable
   {
       public InventoryContext Inventory { get; set; }

       public LogicaSelect LogicSelect { get; set; }

       public ComparableFileAddToDataBase()
       {
           Inventory = new InventoryContext();
           LogicSelect = Inventory.LogicaSelects.First(x => x.Id == 60);
       }

       /// <summary>
       /// Загрузка моделей пользователей для анализа 
       /// </summary>
       /// <param name="modelsComparableUser">Модель для анализа</param>
       public void AddAndUpdateFullLoadComparableUser(ModelComparableUser modelsComparableUser)
       {
           using (var buffer = new MemoryStream())
           {
               var serializer = new XmlSerializer(typeof(ModelComparableUser));
               serializer.Serialize(buffer, modelsComparableUser);
               buffer.Seek(0, SeekOrigin.Begin);
               using (XmlReader reader = XmlReader.Create(buffer))
               {
                   Inventory.Database.CommandTimeout = 12000;
                   Inventory.Database.ExecuteSqlCommand(LogicSelect.SelectUser, new SqlParameter(LogicSelect.SelectedParametr.Split(',')[0], SqlDbType.Xml) { Value = new SqlXml(reader) });
                   reader.Close();
                   reader.Dispose();
               }
               buffer.Close();
               buffer.Dispose();
           }
       }

       public void Dispose()
       {
           Dispose(true);
       }

       /// <summary>
       /// Disposing
       /// </summary>
       /// <param name="disposing"></param>
       protected virtual void Dispose(bool disposing)
       {
           if (disposing)
           {
               Inventory?.Database.Connection.Close();
               Inventory?.Database.Connection.Dispose();
               Inventory?.Dispose();
               Inventory = null;
               LogicSelect = null;
           }
       }
    }
}
