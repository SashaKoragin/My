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
        /// Запрос всех пользователей
        /// </summary>
        /// <returns></returns>
        public string UsersAll()
        {
            SerializeJson json = new SerializeJson();
            //var queryable = from Users in Inventarization.Users
            //                join rules in Inventarization.Rules on Users.IdRule equals rules.IdRule into left
            //                from Rules in left.DefaultIfEmpty()
            //                join otdel in Inventarization.Otdels on Users.IdOtdel equals otdel.IdOtdel into left1
            //                from Otdel in left1.DefaultIfEmpty()
            //                join position in Inventarization.Positions on Users.IdPosition equals position.IdPosition into left2
            //                from Position in left2.DefaultIfEmpty()
            //                select
            //                new { Users };
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

