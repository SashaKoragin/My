using System;
using System.Configuration;
using System.Runtime.Serialization;

namespace TestIFNSLibary.PathJurnalAndUse
{
    [DataContract]
   public class Parametr
    {
        /// <summary>
        /// При инициализации обновляется Конфиг и подтягиваются переменные
        /// </summary>
        public Parametr()
        {
            ConfigurationManager.RefreshSection(ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).AppSettings.SectionInformation.Name);
            PathJurnal = ConfigurationManager.AppSettings["PathJurnal"];
            TestDB = ConfigurationManager.AppSettings["TestDb"];
            WorkDB = ConfigurationManager.AppSettings["WorkDb"];
            Hours = Convert.ToInt32(ConfigurationManager.AppSettings["Hours"]);
            Minutes = Convert.ToInt32(ConfigurationManager.AppSettings["Minutes"]);
            Log = ConfigurationManager.AppSettings["Log4Net"];
            ConnectionString = ConfigurationManager.AppSettings["Connect"];
            ConectTest = ConfigurationManager.AppSettings["ConectTest"];
            ConectWork = ConfigurationManager.AppSettings["ConectWork"];
            Report = ConfigurationManager.AppSettings["SaveReport"];
            ReportMassTemplate = ConfigurationManager.AppSettings["ReportMassTemplate"];
            Inventarization = ConfigurationManager.ConnectionStrings["Inventarization"].ConnectionString;
            ConnectImns51 = ConfigurationManager.AppSettings["ConnectImns51"];
        }

        public void SettingEdit(string testDb, string workDb, int hours, int minutes)
        {
           var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["TestDb"].Value = testDb;
            config.AppSettings.Settings["WorkDb"].Value = workDb;
            config.AppSettings.Settings["Hours"].Value = hours.ToString();
            config.AppSettings.Settings["Minutes"].Value = minutes.ToString();
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
        /// Часы
        /// </summary>
        [DataMember]
        public int Hours { get; set; }
        /// <summary>
        /// Минуты
        /// </summary>
        [DataMember]
        public int Minutes { get; set; }
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
    }
}
