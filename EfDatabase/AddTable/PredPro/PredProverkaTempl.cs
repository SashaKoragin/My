using System;
using System.IO;
using System.Linq;
using Ifns51.Risk;

namespace EfDatabase.AddTable.PredPro
{
    /// <summary>
    /// Класс для предпроверки
    /// </summary>
   public class PredProverkaTempl : IDisposable
    {
        public RisksContext Risk { get; set; }
        public PredProverkaTempl()
        {
            Risk?.Dispose();
            Risk = new RisksContext();
        }
        /// <summary>
        /// Таблица шаблонов
        /// </summary>
        /// <param name="num">Ун шаблона</param>
        /// <returns></returns>
        private DocTemplate Template(int num)
        {
            return Risk.DocTemplates.Where(number => number.Id == num).Select(number => number).FirstOrDefault();
        }
        /// <summary>
        /// Шаблоны возврат
        /// </summary>
        /// <param name="num">Ун шаблона</param>
        /// <returns></returns>
        public Stream DonloadTemplate(int num)
        {
            return new MemoryStream(Template(num).Templ); 
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
