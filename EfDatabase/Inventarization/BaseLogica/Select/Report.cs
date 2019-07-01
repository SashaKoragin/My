using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfDatabase.Inventarization.BaseLogica.Select
{
   public class Report
    {
        /// <summary>
        /// Получение модели из БД
        /// </summary>
        /// <param name="report">Отчет</param>
        public void ReportInvoice(ref EfDatabaseInvoice.Report report)
        {
            SelectSql select = new SelectSql();
            select.ReportInvoce(ref report);
        }
    }
}
