using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ifns51.Risk;

namespace EfDatabase.AddTable.AddKrsb
{
   public class AddKrsb : IDisposable
    {
        public RisksContext Risk { get; set; }
        public AddKrsb()
        {
            Risk?.Dispose();
            Risk = new RisksContext();
        }
        /// <summary>
        /// Создание процессов в БД
        /// </summary>
        /// <param name="idDelo">Лист значений</param>
        public void CreateDelo(List<string> idDelo)
        {
            List<DelaPriemObu> delo = new List<DelaPriemObu>();
            try
            {
                foreach (var delostr in idDelo)
                {
                    delo.Add(new DelaPriemObu() {IdDeloObu = delostr});
                }
                Risk.DelaPriemObus.AddRange(delo);
                Risk.SaveChanges();
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
        }

        /// <summary>
        /// Создание дел для анализа КРСБ
        /// </summary>
        /// <param name="idDelo">Лист значений</param>
        public void CreateDeloAnalizKrsb(List<string> idDelo)
        {
            List<Delo> delo = new List<Delo>();
            try
            {
                foreach (var delostr in idDelo)
                {
                    if (YesDataId(int.Parse(delostr)))
                    {
                        delo.Add(new Delo() { D3979 = int.Parse(delostr), Status1Priem = 0, Status1Analiz = 1 });
                    }
                }
                Risk.Deloes.AddRange(delo);
                Risk.SaveChanges();
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
        }

        /// <summary>
        /// Проверка есть ли такое значение в таблице или нет чтобы не создавать дубли
        /// </summary>
        /// <param name="delo">Ун дела</param>
        /// <returns></returns>
        private bool YesDataId(int delo)
        {
          var model = Risk.Deloes.SingleOrDefault(x => x.D3979 == delo);
            if (model == null)
            {
                return true;
            }
            return false;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Risk?.Dispose();
                Risk = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
