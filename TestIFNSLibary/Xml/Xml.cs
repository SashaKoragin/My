using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web;
using System.Xml.Serialization;
using TestIFNSLibary.Xml.XmlDS;

namespace TestIFNSLibary.Xml
{
    class Xml
    {
        
        public static XmlSerializer Formatter = new XmlSerializer(typeof(Bakcup));
        public bool YesandNo()
        {
                using (FileStream fs = new FileStream(PathJurnalAndUse.Parametr.PathJurnal, FileMode.Open))
                {
                    Bakcup newpeople = (Bakcup)Formatter.Deserialize(fs);
                    return newpeople.Status;
                }
        }

        public DateTime DateBakcup()
        {
                using (FileStream fs = new FileStream(PathJurnalAndUse.Parametr.PathJurnal, FileMode.Open))
            {
                Bakcup newpeople = (Bakcup)Formatter.Deserialize(fs);
                return newpeople.Date;
            }
        }

        public BakcupJurnal[] Jurnal()
        {
            using (FileStream fs = new FileStream(PathJurnalAndUse.Parametr.PathJurnal, FileMode.Open))
            {
                Bakcup newpeople = (Bakcup)Formatter.Deserialize(fs);
                return newpeople.Jurnal;
            }
        }
    }
}
