using System;
using System.Dynamic;
using System.IO;
using TestIFNSLibary.PathJurnalAndUse;
using TestIFNSLibary.Xml.UpdateXml;

namespace TestIFNSLibary.WebSevice.Bakcup
{
   public class BakcupingDb:IDisposable
    {
        private UpdateXml Xml {get;set;}
        public BakcupingDb()
        {
            Dispose();
            Xml = new UpdateXml();
        }

        /// <summary>
        /// Режим резервного копирования с журналом
        /// </summary>
        /// <param name="work">Удаленная БД</param>
        /// <param name="test">Локальная БД</param>
        /// <param name="jurnal">Журнал логирования </param>
        public void Backup(string work, string test,string jurnal)
        {
            
            try
            {
                Xml.WriteStatus(jurnal, "false");
                foreach (string dirPath in Directory.GetDirectories(work, "*", SearchOption.AllDirectories))
                {
                    Directory.CreateDirectory(dirPath.Replace(work, test));
                }
                foreach (string newPath in Directory.GetFiles(work, "*.*", SearchOption.AllDirectories))
                {
                    File.Copy(newPath, newPath.Replace(work, test), true);
                }
                Xml.WriteStatus(jurnal);
                Xml.WriteStatus(jurnal, "true");
                Xml.BakcupXmlJurnal(jurnal, "true","Сделали копию");
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
                Xml.WriteStatus(jurnal, "true");
                Xml.BakcupXmlJurnal(jurnal, "false", e.Message);
            }
        }

        public void Dispose()
        {
            Xml = null;
        }
    }
}
