using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EfDatabase.Inventarization.Base;

namespace EfDatabase.Inventarization.BaseLogica.HistoryLog
{
   public class HistoryLog
    {
        public InventarizationContext Inventarization { get; set; }

        public HistoryLog()
        {
            Inventarization?.Dispose();
            Inventarization = new InventarizationContext();
        }
        /// <summary>
        /// Генерация истории 
        /// </summary>
        /// <param name="guidhistory">GUID объекта</param>
        /// <param name="idmodel">Уникальный номер модели</param>
        /// <param name="nameModel">Наименование классификации</param>
        /// <param name="iduser">Id поьзователя</param>
        /// <param name="oldmodel">Старая модель</param>
        /// <param name="newmodel">Новая модель</param>
        public void GenerateHistory(string guidhistory,int idmodel,string nameModel,int? iduser,string oldmodel,string newmodel)
        {
            History history = new History()
            {
                IdHistory = guidhistory,
                IdModel = idmodel,
                NameModelClass = nameModel,
                IdUser = iduser,
                OldModelColums = oldmodel,
                NewModelColums = newmodel
            };
            WriteHistory(history);
        }

        /// <summary>
        /// Добавление истории редактирования статуса и т д
        ///</summary>
        private void WriteHistory(History history)
        {
            Inventarization.Histories.Add(history);
            Inventarization.SaveChanges();
        }

        /// <summary>
        /// Dispos
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Inventarization?.Dispose();
                Inventarization = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

    }
}
