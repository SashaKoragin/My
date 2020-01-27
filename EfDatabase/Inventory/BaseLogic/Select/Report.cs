using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EfDatabaseInvoice;
using EfDatabaseXsdBookAccounting;

namespace EfDatabase.Inventory.BaseLogic.Select
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
            AddObjectDb.AddObjectDb adddoc = new AddObjectDb.AddObjectDb();
            select.ReportInvoice(ref report);
            report.Main.Barcode = new Barcode();
            report.Main.Barcode.Id = adddoc.AddDocument(report.ParamRequest.IdNameDocument, report.Main.Received.UserName,report.ParamRequest.IdUsers);
        }

        public void BookInvoce(ref BareCodeBook barecode, BookModels bookModels)
        {
            barecode.NameModel = bookModels.Model;
            AddObjectDb.AddObjectDb adddoc = new AddObjectDb.AddObjectDb();
            barecode.Id = adddoc.AddBookAccounting(bookModels);
        }
    }
}
