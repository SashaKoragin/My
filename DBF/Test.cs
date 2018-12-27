using System;
using System.Data;
using System.Web;
using System.Windows;
using AcceptPTPRcazn;
using RcptOFK;
using RDUved12;
using ReceptionOFK5;
namespace DBF
{
   public class Test
    {
        public void Start()
        {
            AcceptPTPRcazn.clsAcceptPTPRClass  dll= new clsAcceptPTPRClass();
            dll.AcceptPTPR()
            RcptOFK.cWorkClass clas = new cWorkClass();
            RDUved12.axdUved12Class uved = new axdUved12Class();
            RDUved12.cMakeProtocolClass prot = new cMakeProtocolClass();
            RDUved12.cRcptFileClass cl = new cRcptFileClass();
            ReceptionOFK5.cConvertClass v = new cConvertClass();
            ReceptionOFK5.cWPClass f = new cWPClass();
            ReceptionOFK5.clsBDPDClass r = new clsBDPDClass;
            

        }
    }
}
