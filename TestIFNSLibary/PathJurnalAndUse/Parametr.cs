using System;
using System.Configuration;
using System.Runtime.Serialization;

namespace TestIFNSLibary.PathJurnalAndUse
{
    [DataContract]
   public class Parametr
    {
        public Parametr()
        {
            ConfigurationManager.RefreshSection(ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).AppSettings.SectionInformation.Name);
        }

        public void SettingEdit(string testDb, string workDb, int hours, int minutes)
        {
           var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["TestDB"].Value = testDb;
            config.AppSettings.Settings["WorkDB"].Value = workDb;
            config.AppSettings.Settings["Hours"].Value = hours.ToString();
            config.AppSettings.Settings["Minutes"].Value = minutes.ToString();
            config.Save(ConfigurationSaveMode.Modified);
        }

        /// <summary>
        /// Путь к журналу
        /// </summary>
        public static string PathJurnal = ConfigurationManager.AppSettings["PathJurnal"];
        /// <summary>
        /// Путь к локальной Базе данных
        /// </summary>
        [DataMember]
        public string TestDB = ConfigurationManager.AppSettings["TestDB"];
        /// <summary>
        /// Путь к удаленной Базе данных
        /// </summary>
        [DataMember]
        public string WorkDB = ConfigurationManager.AppSettings["WorkDB"];
        /// <summary>
        /// Часы
        /// </summary>
        [DataMember]
        public int Hours  = Convert.ToInt32( ConfigurationManager.AppSettings["Hours"]);
        /// <summary>
        /// Минуты
        /// </summary>
        [DataMember]
        public int Minutes  = Convert.ToInt32( ConfigurationManager.AppSettings["Minutes"]);
        /// <summary>
        /// Файл Лога
        /// </summary>
        public static string Log = ConfigurationManager.AppSettings["Log4Net"];
        /// <summary>
        /// Соединение с рабочей Базой данных Taxes51
        /// </summary>
        public static string ConnectionString = ConfigurationManager.AppSettings["Connect"];
        /// <summary>
        /// Соединение с тестовой БД RISK_TEST
        /// </summary>
        public static string ConectTest = ConfigurationManager.AppSettings["ConectTest"];

        /// <summary>
        /// Соединение с рабочей БД BDK77737751000070020000019757
        /// </summary>
        public static string ConectWork = ConfigurationManager.AppSettings["ConectWork"];
        /// <summary>
        /// Сохранение отчетов в автомате
        /// </summary>
        public static string Report = ConfigurationManager.AppSettings["SaveReport"];
        /// <summary>
        /// Путь для массовой печати
        /// </summary>
        public static string ReportMassTemplate = ConfigurationManager.AppSettings["ReportMassTemplate"];
    }
}
