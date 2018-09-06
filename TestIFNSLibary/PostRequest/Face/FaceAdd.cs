using System.Runtime.Serialization;

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
