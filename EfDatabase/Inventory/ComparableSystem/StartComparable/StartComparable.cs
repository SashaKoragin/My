using System;
using System.Collections.Generic;
using EfDatabase.Inventory.Base;
using EfDatabase.Inventory.BaseLogic.ComparableFileAddToDataBase;
using EfDatabase.Inventory.BaseLogic.Select;

namespace EfDatabase.Inventory.ComparableSystem.StartComparable
{
    /// <summary>
    /// Класс сбора данных из разных сервис систем для загрузки в модель для анализа
    /// </summary>
    public class StartComparable
    {
        /// <summary>
        /// Ссылка на Lotus Notes
        /// </summary>
        private string UrlLotusNotes { get; set; }
        /// <summary>
        /// Строка соединения с ДКС
        /// </summary>
        private string ConnectionStringDks { get; set; }

        /// <summary>
        /// Наименование домена
        /// </summary>
        private string Domain { get; set; }
        /// <summary>
        /// Путь к домену
        /// </summary>
        private string DomainIfnsAll { get; set; }
        /// <summary>
        /// Запуск процесса для сравнения всех пользователей из всех систем
        /// </summary>
        /// <param name="connectionStringDks">Строка соединения с ДКС</param>
        /// <param name="domainIfnsAll">Полный путь к домену ИФНС для сбора информации обо всем</param>
        /// <param name="lotusAllUserIMNS">Api GET выгрузки всех пользователей из Lotus Notes</param>
        /// <param name="domain">Центральный домен НО</param>
        public StartComparable(string connectionStringDks, string domainIfnsAll, string lotusAllUserIMNS, string domain)
        {
            ConnectionStringDks = connectionStringDks.Trim();
            UrlLotusNotes = lotusAllUserIMNS;
            Domain = domain;
            DomainIfnsAll = domainIfnsAll;
        }
        /// <summary>
        /// Выгрузка всех пользователей из систем
        /// </summary>
        public void StartFullModelComparable()
        {
            try
            {
                var selectSql = new ComparableFileAddToDataBase();
                var comparableLotusNotes = new ComparableLotusNotes.ComparableLotusNotes(UrlLotusNotes);
                var comparableActiveDirectory = new ComparableActiveDirectory.ComparableActiveDirectory(Domain, DomainIfnsAll);
                var comparableDks = new ComparableDks.ComparableDks(ConnectionStringDks);
                var userModel = comparableLotusNotes.DownloadModelLotusNotes();
                comparableActiveDirectory.SelectModelActiveDirectory(ref userModel);
                comparableDks.SelectModelDks(ref userModel);
                selectSql.AddAndUpdateFullLoadComparableUser(userModel);
                selectSql.Dispose();
                comparableActiveDirectory.Dispose();
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
        }
    }
}
