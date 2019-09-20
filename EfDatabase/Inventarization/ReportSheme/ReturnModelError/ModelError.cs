using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfDatabase.Inventarization.ReportSheme.ReturnModelError
{
   public class ModelError
    {
        /// <summary>
        /// Id Документа
        /// </summary>
        public int IdDocument { get; set; }
        /// <summary>
        /// Id Ошибки
        /// </summary>
        public int IdError { get; set; }
        /// <summary>
        /// Сообщение об ошибке
        /// </summary>
        public string Messages { get; set; }
    }
}
