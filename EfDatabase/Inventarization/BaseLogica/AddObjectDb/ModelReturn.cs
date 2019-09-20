using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EfDatabase.Inventarization.BaseLogica.AddObjectDb
{
    [DataContract]
   public class ModelReturn
    {
        public ModelReturn(string message,int index = 0, string guid = null )
        {
            Guid = guid;
            Index = index;
            Message = message;
        }
        [DataMember]
        private string Guid { get; set; }

        [DataMember]
        private int Index { get; set; }

        [DataMember]
        private string Message { get; set; }
    }
}
