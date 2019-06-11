using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EfDatabase.Inventarization.Base;
using EfDatabaseParametrsModel;

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
        /// Генерация модели с параметрами
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ModelSelect SqlSelect(ModelSelect model)
        {
            model.LogicaSelect = Inventarization.Database.SqlQuery<EfDatabaseParametrsModel.LogicaSelect>(String.Format(ProcedureSelect, model.ParametrsSelect.Id)).ToList()[0];
            model.Parametrs = Inventarization.Database.SqlQuery<Parametrs>(model.LogicaSelect.SelectedParametr).ToArray();
            return model;
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
