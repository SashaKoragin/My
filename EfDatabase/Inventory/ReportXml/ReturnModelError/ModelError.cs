

namespace EfDatabase.Inventory.ReportXml.ReturnModelError
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
