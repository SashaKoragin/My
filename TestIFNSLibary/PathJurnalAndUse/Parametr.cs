using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

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

        public static string ConnectionString = ConfigurationManager.AppSettings["Connect"];
    }
}
