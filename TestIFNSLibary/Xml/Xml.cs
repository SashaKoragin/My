using System;
using System.IO;
using System.Xml.Serialization;
using TestIFNSLibary.PathJurnalAndUse;
using TestIFNSLibary.Xml.XmlDS;

namespace TestIFNSLibary.Xml
{
    class Xml
    {
        Parameter ParametrService = new Parameter();
        public static XmlSerializer Formatter = new XmlSerializer(typeof(Bakcup));
        public bool YesandNo()
        {
            
                using (FileStream fs = new FileStream(ParametrService.PathJurnal, FileMode.Open))
                {
                    Bakcup newpeople = (Bakcup)Formatter.Deserialize(fs);
                    return newpeople.Status;
                }
        }

        public DateTime DateBakcup()
        {
                using (FileStream fs = new FileStream(ParametrService.PathJurnal, FileMode.Open))
            {
                Bakcup newpeople = (Bakcup)Formatter.Deserialize(fs);
                return newpeople.Date;
            }
        }

        public BakcupJurnal[] Jurnal()
        {
            using (FileStream fs = new FileStream(ParametrService.PathJurnal, FileMode.Open))
            {
                Bakcup newpeople = (Bakcup)Formatter.Deserialize(fs);
                return newpeople.Jurnal;
            }
        }
    }
}
