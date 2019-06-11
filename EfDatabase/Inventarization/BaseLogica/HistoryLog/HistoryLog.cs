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
        /// <param name="user">Id поьзователя</param>
        /// <param name="process">Описание процесса</param>
        public void GenerateHistory(string guidhistory,int user,string process)
        {
            History history = new History()
            {
                IdHistory = guidhistory,
                IdUser = user,
                UserProcess = process
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
