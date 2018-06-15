using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TestIFNSLibary.PostRequest.Face
{
    [DataContract]
   public class FaceAdd
    {
        [DataMember(Name = "N1Old")]
        public int N1Old { get; set; }
        [DataMember(Name = "N1New")]
        public int N1New { get; set; }
    }
}
