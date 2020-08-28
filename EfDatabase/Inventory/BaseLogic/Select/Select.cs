using EfDatabase.Inventory.Base;
using System;
using System.Linq;
using LibaryXMLAuto.ReadOrWrite.SerializationJson;

namespace EfDatabase.Inventory.BaseLogic.Select
{
   public class Select : IDisposable
    {
        public InventoryContext Inventory { get; set; }

        public Select()
        {
            Inventory?.Dispose();
            Inventory = new InventoryContext();
        }

        /// <summary>
        /// Запрос на список отделов
        /// </summary>
        /// <returns></returns>
        public string DepartmentAll()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.Otdels);
        }

        /// <summary>
        /// Загрузка всех ролей пользователя для выбора!
        /// </summary>
        /// <returns></returns>
        public string RuleAll()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.Rules);
        }
        /// <summary>
        /// Запрос всех пользователей
        /// </summary>
        /// <returns></returns>
        public string UsersAll()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.Users);

        }
        /// <summary>
        /// Запрос всех должностей
        /// </summary>
        /// <returns></returns>
        public string PositionUsers()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.Positions);
        }

        /// <summary>
        /// Запрос всех принтеров с БД
        /// </summary>
        /// <returns></returns>
        public string Printers()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.Printers.Where(x=>x.IdStatus!=31));
        }
        /// <summary>
        /// Выгрузка всех Коммутаторов
        /// </summary>
        /// <returns></returns>
        public string Swithes()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.Swithes.Where(x => x.IdStatus != 31));
        }

        public string ModelSwitch()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.ModelSwithes);
        }

        /// <summary>
        /// Запрос всех сканеров с БД
        /// </summary>
        /// <returns></returns>
        public string Scaner()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.ScanerAndCamers.Where(x => x.IdStatus != 31));
        }

        /// <summary>
        /// Запрос всех МФУ с БД
        /// </summary>
        /// <returns></returns>
        public string Mfu()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.Mfus.Where(x => x.IdStatus != 31));
        }

        /// <summary>
        /// Запрос всех Системных блоков с БД
        /// </summary>
        /// <returns></returns>
        public string SysBloks()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.SysBlocks.Where(x => x.IdStatus != 31));
        }

        /// <summary>
        /// Запрос всех Мониторов с БД
        /// </summary>
        /// <returns></returns>
        public string Monitors()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.Monitors.Where(x => x.IdStatus != 31));
        }

        /// <summary>
        /// Запрос всех CopySave с БД
        /// </summary>
        /// <returns></returns>
        public string CopySave()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.CopySaves);
        }

        /// <summary>
        /// Запрос всех производителей с БД
        /// </summary>
        /// <returns></returns>
        public string Proizvoditel()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.FullProizvoditels);
        }

        /// <summary>
        /// Запрос всех кабинетов с БД
        /// </summary>
        /// <returns></returns>
        public string Kabinet()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.Kabinets);
        }

        /// <summary>
        /// Запрос всех моделей с БД
        /// </summary>
        /// <returns></returns>
        public string Model()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.FullModels);
        }

        /// <summary>
        /// Запрос всех статусов с БД
        /// </summary>
        /// <returns></returns>
        public string Status()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.Statusings);
        }

        /// <summary>
        /// Запрос всех наименований системных блоков
        /// </summary>
        /// <returns></returns>
        public string NameSysBlock()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.NameSysBlocks);
        }

        /// <summary>
        /// Запрос всех наименований монитора
        /// </summary>
        /// <returns></returns>
        public string NameMonitor()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.NameMonitors);
        }
        /// <summary>
        /// Выгрузка телефонов
        /// </summary>
        /// <returns></returns>
        public string Telephon()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.Telephons.Where(x => x.IdStatus != 31));
        }
        /// <summary>
        /// Выгрузка типов серверов
        /// </summary>
        /// <returns></returns>
        public string TypeServer()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.TypeServers);
        }
        /// <summary>
        /// Выгрузка бесперебойников
        /// </summary>
        /// <returns></returns>
        public string BlockPower()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.BlockPowers.Where(x => x.IdStatus != 31));
        }
        /// <summary>
        /// Все серверное оборудование
        /// </summary>
        /// <returns></returns>
        public string ServerEquipment()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.ServerEquipments.Where(x => x.IdStatus != 31));
        }
        /// <summary>
        /// Все модели серверного оборудования
        /// </summary>
        /// <returns></returns>
        public string ModelSeverEquipment()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.ModelSeverEquipments);
        }
        /// <summary>
        /// Все производители серверного оборудования
        /// </summary>
        /// <returns></returns>
        public string ManufacturerSeverEquipment()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.ManufacturerSeverEquipments);
        }

        /// <summary>
        /// Выгрузка поставщиков
        /// </summary>
        /// <returns></returns>
        public string Supply()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibary(Inventory.Supplies);
        }
        /// <summary>
        /// Выгрузка моделей бесперебойных блоков
        /// </summary>
        /// <returns></returns>
        public string ModelBlockPower()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.ModelBlockPowers);
        }

        /// <summary>
        /// Выгрузка производителей бесперебойных блоков
        /// </summary>
        /// <returns></returns>
        public string ProizvoditelBlockPower()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.ProizvoditelBlockPowers);
        }
        /// <summary>
        /// Статистика актулизации пользователей
        /// </summary>
        /// <returns></returns>
        public string ActualsUsersKladr()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.UsersIsActualsStats);
        }
        /// <summary>
        /// Получение всей классификации
        /// </summary>
        /// <returns></returns>
        public string AllClasification()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.Classifications);
        }
        /// <summary>
        /// Все идентификаторы пользователей
        /// </summary>
        /// <returns></returns>
        public string AllMailIdentifier()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.MailIdentifiers);
        }
        /// <summary>
        /// Все группы пользователей
        /// </summary>
        /// <returns></returns>
        public string AllMailGroup()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.MailGroups);
        }
        /// <summary>
        /// Запрос на все шаблоны СТО для заявок
        /// </summary>
        /// <returns></returns>
        public string AllTemplate()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.FullTemplateSupports);
        }
        /// <summary>
        /// Запрос на технику претендующую на QR code
        /// </summary>
        /// <param name="serialNumber">Серийный номер</param>
        /// <returns></returns>
        public AllTechnic SelecTechnic(string serialNumber)
        {
            try
            {
                return Inventory.AllTechnics.FirstOrDefault(x => x.SerNum == serialNumber);
            }
            catch(Exception ex)
            {
                Loggers.Log4NetLogger.Error(ex);
            }
            return null;
        }

        /// <summary>
        /// Dispose
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

