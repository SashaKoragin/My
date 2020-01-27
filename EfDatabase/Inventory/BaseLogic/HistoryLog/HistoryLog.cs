using EfDatabase.Inventory.Base;

namespace EfDatabase.Inventory.BaseLogic.HistoryLog
{
   public class HistoryLog
    {
        public InventoryContext Inventory { get; set; }

        public HistoryLog()
        {
            Inventory?.Dispose();
            Inventory = new InventoryContext();
        }
        /// <summary>
        /// Генерация истории 
        /// </summary>
        /// <param name="guidHistory">GUID объекта</param>
        /// <param name="idModel">Уникальный номер модели</param>
        /// <param name="nameModel">Наименование классификации</param>
        /// <param name="idUser">Id пользователя</param>
        /// <param name="oldModel">Старая модель</param>
        /// <param name="newModel">Новая модель</param>
        public void GenerateHistory(string guidHistory,int idModel,string nameModel,int? idUser,string oldModel,string newModel)
        {
            History history = new History()
            {
                IdHistory = guidHistory,
                IdModel = idModel,
                NameModelClass = nameModel,
                IdUser = idUser,
                OldModelColums = oldModel,
                NewModelColums = newModel
            };
            WriteHistory(history);
        }

        /// <summary>
        /// Добавление истории редактирования статуса и т д
        ///</summary>
        private void WriteHistory(History history)
        {
            Inventory.Histories.Add(history);
            Inventory.SaveChanges();
        }
    }
}
