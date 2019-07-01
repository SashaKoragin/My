using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using EfDatabase.Inventarization.Base;
using EfDatabaseInvoice;
using EfDatabaseParametrsModel;
using LibaryXMLAuto.ModelXmlSql.ConvertModel.DesirializationSql;
using LibaryXMLAutoReports.ReportsBdk;


namespace EfDatabase.Inventarization.BaseLogica.Select
{
   public class SelectSql : IDisposable
   {
       public static string ProcedureSelect = "Exec [dbo].[InventarizationCommandSelectWcfToSql] {0}";
        public InventarizationContext Inventarization { get; set; }

        public SelectSql()
        {
            Inventarization?.Dispose();
            
            Inventarization = new InventarizationContext();
        }
        /// <summary>
        /// Генерация модели с параметрами для дальнейших выборок
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ModelSelect SqlSelect(ModelSelect model)
        {
            model.LogicaSelect = SqlSelectModel(model.ParametrsSelect.Id);
            model.Parametrs = Inventarization.Database.SqlQuery<Parametrs>(model.LogicaSelect.SelectedParametr).ToArray();
            return model;
        }

        /// <summary>
        /// Модель отчета из БД которая отправится в Open Xml
        /// </summary>
        /// <param name="report">Параметры отчета Report</param>
        /// <returns></returns>
       public void ReportInvoce(ref EfDatabaseInvoice.Report report)
        {
            ModelSelect model = new ModelSelect {LogicaSelect = SqlSelectModel(report.ParamRequest.IdSelect)};
            report.Main.Formed = Inventarization.Database.SqlQuery<Formed>(model.LogicaSelect.SelectUser,
                new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[0],1),
                new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[1], report.ParamRequest.IdOut),
                new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[2], report.ParamRequest.IdUsers)).ToList()[0];
            report.Main.Received = Inventarization.Database.SqlQuery<Received>(model.LogicaSelect.SelectUser,
                new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[0], 2),
                new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[1], report.ParamRequest.IdOut),
                new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[2], report.ParamRequest.IdUsers)).ToList()[0];
            report.Body = Inventarization.Database.SqlQuery<Body>(model.LogicaSelect.SelectUser,
                new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[0], 3),
                new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[1], report.ParamRequest.IdOut),
                new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[2], report.ParamRequest.IdUsers)).ToArray();
        }





        /// <summary>
        /// Выборка модели для манипуляции
        /// </summary>
        /// <param name="id">Параметр индекса в таблицы</param>
        /// <returns></returns>
       private EfDatabaseParametrsModel.LogicaSelect SqlSelectModel(int id)
       {
            return Inventarization.Database.SqlQuery<EfDatabaseParametrsModel.LogicaSelect>(String.Format(ProcedureSelect, id)).ToList()[0];
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