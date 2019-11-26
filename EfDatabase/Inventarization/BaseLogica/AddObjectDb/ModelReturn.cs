using System.Runtime.Serialization;

namespace EfDatabase.Inventarization.BaseLogica.AddObjectDb
{
    [DataContract]
   public class ModelReturn<T> where T:class 
    {
        public ModelReturn(string message, T model = null, int index = 0, string guid = null )
        {
            Guid = guid;
            Index = index;
            Message = message;
            Model = model;
        }
        [DataMember]
        public string Guid { get; set; }

        [DataMember]
        public int Index { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public T Model { get; set;}
    }
}
