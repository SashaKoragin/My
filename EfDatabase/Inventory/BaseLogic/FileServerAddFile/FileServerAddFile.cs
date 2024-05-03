using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using EfDatabase.Inventory.Base;
using EfDatabase.ReportXml.ModelFileServer;
using LibaryXMLAuto.ReadOrWrite;

namespace EfDatabase.Inventory.BaseLogic.FileServerAddFile
{
    public class FileServerAddFile : IDisposable
    {
        public InventoryContext Inventory { get; set; }
        public LogicaSelect LogicaSelect { get; set; }
        /// <summary>
        /// Путь к ошибочным файлам
        /// </summary>
        public string ErrorXmlFile { get; set; }
        public FileServerAddFile(string errorXmlFile)
        {
            ErrorXmlFile = errorXmlFile;
            Inventory = new InventoryContext();
            LogicaSelect = Inventory.LogicaSelects.First(x => x.Id == 63);
        }

        /// <summary>
        /// Добавление в БД файла с описанием атрибутов для анализа
        /// </summary>
        ///<param name="allFileServer">Файл с атрибутами для раскладки на сервере</param>
        /// <param name="isTruncateTable">Очистить предыдущие результаты и сбросить индекс 0 нет 1 да</param>
        public void AddFileServerToDataBase(AllFileServerModel allFileServer, bool isTruncateTable = false)
        {
            //Как оказывается через MemoryStream завернуть можно!!!
            try
            {
                if (allFileServer != null)
                {
                    using (var buffer = new MemoryStream())
                    {
                        var serializer = new XmlSerializer(typeof(AllFileServerModel));
                        serializer.Serialize(buffer, allFileServer);
                        
                        buffer.Seek(0, SeekOrigin.Begin);
                        using (XmlReader reader = XmlReader.Create(buffer))
                        {
                            Inventory.Database.CommandTimeout = 12000;
                            Inventory.Database.ExecuteSqlCommand(LogicaSelect.SelectUser, new SqlParameter(LogicaSelect.SelectedParametr.Split(',')[0], SqlDbType.Xml) { Value = new SqlXml(reader) },
                                new SqlParameter(LogicaSelect.SelectedParametr.Split(',')[1], SqlDbType.Bit) { Value = isTruncateTable});
                            reader.Close();
                            reader.Dispose();
                            
                        }
                        buffer.Close();
                        buffer.Dispose();
                    }
                }
                else
                {
                    Inventory.Database.CommandTimeout = 12000;
                    Inventory.Database.ExecuteSqlCommand(LogicaSelect.SelectUser, new SqlParameter(LogicaSelect.SelectedParametr.Split(',')[0], SqlDbType.Xml) { Value = DBNull.Value },
                        new SqlParameter(LogicaSelect.SelectedParametr.Split(',')[1], SqlDbType.Bit) { Value = isTruncateTable });
                }
            }
            catch (Exception eX)
            {
                Loggers.LogFileServer.Error(eX);
                if (allFileServer != null)
                {
                    var xmlConvert = new XmlReadOrWrite();
                    xmlConvert.CreateXmlFile(ErrorXmlFile + $"{Guid.NewGuid()}.xml", allFileServer, allFileServer.GetType());
                }
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
                LogicaSelect = null;
                ErrorXmlFile = null;
            }
        }
    }
}
