using System;
using System.IO;
using TestIFNSLibary.Xml.UpdateXml;

namespace TestIFNSLibary.WebSevice.Bakcup
{
   public class BakcupingDb
    {


        /// <summary>
        /// Режим резервного копирования с журналом
        /// </summary>
        /// <param name="WorkDb">Удаленная БД</param>
        /// <param name="TestDb">Локальная БД</param>
        public void Backup(string WorkDb, string TestDb)
        {
            UpdateXml xml = new UpdateXml();
            try
            {
                xml.WriteStatus(PathJurnalAndUse.Parametr.PathJurnal, "false");
                foreach (string dirPath in Directory.GetDirectories(WorkDb, "*", SearchOption.AllDirectories))
                {
                    Directory.CreateDirectory(dirPath.Replace(WorkDb, TestDb));
                }
                foreach (string newPath in Directory.GetFiles(WorkDb, "*.*", SearchOption.AllDirectories))
                {
                    File.Copy(newPath, newPath.Replace(WorkDb, TestDb), true);
                }
                xml.WriteStatus(PathJurnalAndUse.Parametr.PathJurnal);
                xml.WriteStatus(PathJurnalAndUse.Parametr.PathJurnal, "true");
                xml.BakcupXmlJurnal(PathJurnalAndUse.Parametr.PathJurnal,"true","Сделали копию");
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
                xml.WriteStatus(PathJurnalAndUse.Parametr.PathJurnal, "true");
                xml.BakcupXmlJurnal(PathJurnalAndUse.Parametr.PathJurnal, "false", e.Message);
            }
        }
    }
}
