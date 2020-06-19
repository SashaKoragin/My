using System;
using System.Data.SqlClient;
using System.Linq;
using EfDatabase.Inventory.Base;
using EfDatabaseParametrsModel;
using EfDatabaseXsdSupportNalog;


namespace EfDatabase.Inventory.BaseLogic.Select
{
   public class GenerateParameterSupport : IDisposable
   {
       public InventoryContext Inventory { get; set; }

       public GenerateParameterSupport()
       {
           Inventory?.Dispose();
           Inventory = new InventoryContext();
       }

        /// <summary>
        /// Генерация параметров для запроса на СТО
        /// </summary>
        /// <param name="modelSupport">Модель запроса на сервис</param>
       public void GenerateTemplateUrlParameter(ref ModelParametrSupport modelSupport)
       {
           SelectSql select = new SelectSql();
           ModelSelect model = new ModelSelect { LogicaSelect = select.SqlSelectModel(28) };
           modelSupport.TemplateSupport = Inventory.Database.SqlQuery<TemplateSupport>(model.LogicaSelect.SelectUser,
               new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[0], modelSupport.IdTemplate)).ToArray();
           if (modelSupport?.TemplateSupport != null && modelSupport?.TemplateSupport.Length > 0)
           {
               var description = modelSupport.Discription;
               modelSupport.TemplateSupport.Where(discription => discription.NameGuidParametr == "UF_DESCRIPTION").ToList().ForEach(d=>d.Parametr = description);
               var modelParameterInput = modelSupport.TemplateSupport.Where(temple =>temple.NameStepSupport == "Step2" && temple.TemplateParametrType != null);

               foreach (var template in modelParameterInput)
               {
                   if (template.TemplateParametrType.Equals("User") && modelSupport.IdUser != 0)
                   {
                       template.Parametr = Inventory.Database.SqlQuery<string>(template.SelectParametr,
                               new SqlParameter("IdUser", modelSupport.IdUser)).FirstOrDefault();
                   }
                   if (template.TemplateParametrType.Equals("SysBlock") &&  modelSupport.IdSysBlock != 0)
                   {
                       template.Parametr = Inventory.Database.SqlQuery<string>(template.SelectParametr, 
                              new SqlParameter("IdSysBlock", modelSupport.IdSysBlock)).FirstOrDefault();
                   }
                   if (template.TemplateParametrType.Equals("Monitors") && modelSupport.IdMonitor != 0)
                   {
                       template.Parametr = Inventory.Database.SqlQuery<string>(template.SelectParametr,
                           new SqlParameter("IdMonitor", modelSupport.IdMonitor)).FirstOrDefault();
                   }
                   if (template.TemplateParametrType.Equals("Printer") &&  modelSupport.IdPrinter != 0)
                   {
                       template.Parametr = Inventory.Database.SqlQuery<string>(template.SelectParametr,
                           new SqlParameter("IdPrinter", modelSupport.IdPrinter)).FirstOrDefault();
                   }
                   if (template.TemplateParametrType.Equals("Mfu") &&  modelSupport.IdMfu != 0)
                   {
                       template.Parametr = Inventory.Database.SqlQuery<string>(template.SelectParametr,
                           new SqlParameter("IdMfu", modelSupport.IdMfu)).FirstOrDefault();
                   }
                   if (template.TemplateParametrType.Equals("Scanner") &&  modelSupport.IdScanner != 0)
                   {
                       template.Parametr = Inventory.Database.SqlQuery<string>(template.SelectParametr,
                           new SqlParameter("IdScaner", modelSupport.IdScanner)).FirstOrDefault();
                   }
               }
           }
           else
           {
               throw new InvalidOperationException($"Фатальная ошибка отсутствует шаблон запроса на CTO по id {modelSupport.IdTemplate}!");
           }
           select.Dispose();
        }




       /// <summary>
       /// Dispose
       /// </summary>
       /// <param name="disposing"></param>
       protected virtual void Dispose(bool disposing)
       {
           if (disposing)
           {
               Inventory?.Dispose();
               Inventory = null;
           }
       }

       public void Dispose()
       {
           Dispose(true);
       }
    }
}
