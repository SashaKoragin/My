using EfDatabase.Inventarization.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibaryXMLAuto.ReadOrWrite.SerializationJson;
using LibaryXMLAutoInventarization.Model.ModelSelectAll;

namespace EfDatabase.Inventarization.BaseLogica.Select
{
   public class Select : IDisposable
    {
        public InventarizationContext Inventarization { get; set; }

        public Select()
        {
            Inventarization?.Dispose();
            Inventarization = new InventarizationContext();
        }

        /// <summary>
        /// Запрос на список отделов
        /// </summary>
        /// <returns></returns>
        public string OtdelAll()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventarization.Otdels);
        }

        /// <summary>
        /// Загрузка всех ролей пользователя для выбора!
        /// </summary>
        /// <returns></returns>
        public string RuleAll()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventarization.Rules);
        }
        /// <summary>
        /// Запрос всех пользователей
        /// </summary>
        /// <returns></returns>
        public string UsersAll()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventarization.Users);

        }
        /// <summary>
        /// Запрос всех должностей
        /// </summary>
        /// <returns></returns>
        public string PositionUsers()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventarization.Positions);
        }

        /// <summary>
        /// Запрос всех принтеров с БД
        /// </summary>
        /// <returns></returns>
        public string Printers()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventarization.Printers);
        }
        /// <summary>
        /// Выгрузка всех Коммутаторов
        /// </summary>
        /// <returns></returns>
        public string Swithes()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventarization.Swithes);
        }

        public string ModelSwitch()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventarization.ModelSwithes);
        }

        /// <summary>
        /// Запрос всех сканеров с БД
        /// </summary>
        /// <returns></returns>
        public string Scaner()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventarization.ScanerAndCamers);
        }

        /// <summary>
        /// Запрос всех МФУ с БД
        /// </summary>
        /// <returns></returns>
        public string Mfu()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventarization.Mfus);
        }

        /// <summary>
        /// Запрос всех Системных блоков с БД
        /// </summary>
        /// <returns></returns>
        public string SysBloks()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventarization.SysBlocks);
        }

        /// <summary>
        /// Запрос всех Мониторов с БД
        /// </summary>
        /// <returns></returns>
        public string Monitors()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventarization.Monitors);
        }

        /// <summary>
        /// Запрос всех CopySave с БД
        /// </summary>
        /// <returns></returns>
        public string CopySave()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventarization.CopySaves);
        }

        /// <summary>
        /// Запрос всех производителей с БД
        /// </summary>
        /// <returns></returns>
        public string Proizvoditel()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventarization.FullProizvoditels);
        }

        /// <summary>
        /// Запрос всех кабинетов с БД
        /// </summary>
        /// <returns></returns>
        public string Kabinet()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventarization.Kabinets);
        }

        /// <summary>
        /// Запрос всех моделей с БД
        /// </summary>
        /// <returns></returns>
        public string Model()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventarization.FullModels);
        }

        /// <summary>
        /// Запрос всех статусов с БД
        /// </summary>
        /// <returns></returns>
        public string Status()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventarization.Statusings);
        }

        /// <summary>
        /// Запрос всех наименований системных блоков
        /// </summary>
        /// <returns></returns>
        public string NameSysBlock()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventarization.NameSysBlocks);
        }

        /// <summary>
        /// Запрос всех наименований монитора
        /// </summary>
        /// <returns></returns>
        public string NameMonitor()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventarization.NameMonitors);
        }
        /// <summary>
        /// Выгрузка телефонов
        /// </summary>
        /// <returns></returns>
        public string Telephon()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventarization.Telephons);
        }
        /// <summary>
        /// Выгрузка бесперебойников
        /// </summary>
        /// <returns></returns>
        public string BlockPower()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventarization.BlockPowers);
        }
        /// <summary>
        /// Выгрузка поставщиков
        /// </summary>
        /// <returns></returns>
        public string Supply()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibary(Inventarization.Supplies);
        }
        /// <summary>
        /// Выгрузка моделей бесперебойных блоков
        /// </summary>
        /// <returns></returns>
        public string ModelBlockPower()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventarization.ModelBlockPowers);
        }

        /// <summary>
        /// Выгрузка производителей бесперебойных блоков
        /// </summary>
        /// <returns></returns>
        public string ProizvoditelBlockPower()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventarization.ProizvoditelBlockPowers);
        }
        /// <summary>
        /// Статистика актулизации пользователей
        /// </summary>
        /// <returns></returns>
        public string ActualsUsersKladr()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventarization.UsersIsActualsStats);
        }
        /// <summary>
        /// Получение всей классификации
        /// </summary>
        /// <returns></returns>
        public string AllClasification()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventarization.Classifications);
        }

        /// <summary>
        /// Dispos
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Inventarization?.Dispose();
                Inventarization = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}

