using System;
using System.Configuration;
using System.Runtime.Serialization;

namespace TestIFNSLibary.PathJurnalAndUse
{
    [DataContract]
   public class Parameter
    {
        /// <summary>
        /// При инициализации обновляется Конфиг и подтягиваются переменные
        /// </summary>
        public Parameter()
        {
            ConfigurationManager.RefreshSection(ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).AppSettings.SectionInformation.Name);
            PathJurnal = ConfigurationManager.AppSettings["PathJurnal"];
            TestDB = ConfigurationManager.AppSettings["TestDb"];
            WorkDB = ConfigurationManager.AppSettings["WorkDb"];
            Log = ConfigurationManager.AppSettings["Log4Net"];
            ConnectionString = ConfigurationManager.AppSettings["Connect"];
            ConectTest = ConfigurationManager.AppSettings["ConectTest"];
            ConectWork = ConfigurationManager.AppSettings["ConectWork"];
            Report = ConfigurationManager.AppSettings["SaveReport"];
            ReportMassTemplate = ConfigurationManager.AppSettings["ReportMassTemplate"];
            Inventarization = ConfigurationManager.ConnectionStrings["Inventarization"].ConnectionString;
            BulkCopyXmlSto = ConfigurationManager.ConnectionStrings["BulkCopyXmlSto"].ConnectionString;
            ConnectImns51 = ConfigurationManager.AppSettings["ConnectImns51"];
            PathDomainGroup = ConfigurationManager.AppSettings["PathDomainGroup"];
            User = ConfigurationManager.AppSettings["User"];
            Password = ConfigurationManager.AppSettings["Password"];
            SendServiceLotus = ConfigurationManager.AppSettings["SendServiceLotus"];
            LoginSto = ConfigurationManager.AppSettings["LoginSto"];
            PasswordSto = ConfigurationManager.AppSettings["PasswordSto"];
            XsdReport = ConfigurationManager.AppSettings["XsdReport"];
        }

        public void SettingEdit(string testDb, string workDb, int hours, int minutes)
        {
           var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["TestDb"].Value = testDb;
            config.AppSettings.Settings["WorkDb"].Value = workDb;
            config.Save(ConfigurationSaveMode.Modified);
        }

        /// <summary>
        /// Путь к журналу
        /// </summary>
        public string  PathJurnal { get; set; }
        /// <summary>
        /// Путь к локальной Базе данных
        /// </summary>
        [DataMember]
        public string TestDB { get; set; }
        /// <summary>
        /// Путь к удаленной Базе данных
        /// </summary>
        [DataMember]
        public string WorkDB { get; set; }
        /// <summary>
        /// Файл Лога
        /// </summary>
        public string Log { get; set; }
        /// <summary>
        /// Соединение с рабочей Базой данных Taxes51
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// Соединение с тестовой БД RISK_TEST
        /// </summary>
        public string ConectTest { get; set; }

        /// <summary>
        /// Соединение с рабочей БД BDK77737751000070020000019757
        /// </summary>
        public string ConectWork { get; set; }
        /// <summary>
        /// Сохранение отчетов в автомате
        /// </summary>
        public string Report { get; set; }
        /// <summary>
        /// Путь для массовой печати
        /// </summary>
        public string ReportMassTemplate { get; set; }
        /// <summary>
        /// Строка соединения с Инвентаризации
        /// </summary>
        public string Inventarization { get; set; }
        /// <summary>
        /// Строка соединения с IMNS51 Кадры
        /// </summary>
        public string ConnectImns51 { get; set; }
        /// <summary>
        /// Группы пользователей поиск
        /// </summary>
        public string PathDomainGroup { get; set; }
        /// <summary>
        /// Логин 
        /// </summary>
        public string User { get; set; }
        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Отправка модели для генерации писем
        /// </summary>
        public string SendServiceLotus { get; set; }
        /// <summary>
        /// Логин для СТО
        /// </summary>
        public string LoginSto { get; set; }
        /// <summary>
        /// Пароль СТО
        /// </summary>
        public string PasswordSto { get; set; }
        /// <summary>
        /// Загрузка данных с СТО
        /// </summary>
        public string BulkCopyXmlSto { get; set; }
        /// <summary>
        /// XSD Схема
        /// </summary>
        public string XsdReport { get; set; }
    }
}
