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
        /// <param name="work">Удаленная БД</param>
        /// <param name="test">Локальная БД</param>
        public void Backup(string work, string test)
        {
            UpdateXml xml = new UpdateXml();
            try
            {
                xml.WriteStatus(PathJurnalAndUse.Parametr.PathJurnal, "false");
                foreach (string dirPath in Directory.GetDirectories(work, "*", SearchOption.AllDirectories))
                {
                    Directory.CreateDirectory(dirPath.Replace(work, test));
                }
                foreach (string newPath in Directory.GetFiles(work, "*.*", SearchOption.AllDirectories))
                {
                    File.Copy(newPath, newPath.Replace(work, test), true);
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
