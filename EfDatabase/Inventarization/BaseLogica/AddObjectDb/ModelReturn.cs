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
        public ModelReturn(string message, string guid = null )
        {
            Guid = guid;
            Message = message;
        }
        [DataMember]
        private string Guid { get; set; }
        [DataMember]
        private string Message { get; set; }
    }
}
