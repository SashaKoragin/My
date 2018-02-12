using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace FirstWCFWebService
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IFirstWebService" в коде и файле конфигурации.
    [ServiceContract]
    public interface IFirstWebService
    {
        [OperationContract]
        void DoWork();
    }
}
