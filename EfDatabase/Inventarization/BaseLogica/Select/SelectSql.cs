using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using EfDatabase.Inventarization.Base;
using EfDatabaseInvoice;
using EfDatabaseParametrsModel;
using LibaryXMLAutoModelXmlAuto.MigrationReport;
using LibaryXMLAutoModelXmlAuto.OtdelRuleUsers;
using Newtonsoft.Json.Linq;


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
            try
            {
                model.LogicaSelect = SqlSelectModel(model.ParametrsSelect.Id);
                model.Parametrs = Inventarization.Database.SqlQuery<Parametrs>(model.LogicaSelect.SelectedParametr).ToArray();
                return model;
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
                return null;
            }

        }
        /// <summary>
        /// Запрос на лиц кто согласовывает надо добавлять в xml
        /// </summary>
        /// <param name="template">Шаблон возврата</param>
       public ModelSelect SendersUsers(ref RuleTemplate template)
       {
           try
           {
                ModelSelect model = new ModelSelect { LogicaSelect = SqlSelectModel(13) };
                template.SenderUsers.Security = Inventarization.Database.SqlQuery<Security>(model.LogicaSelect.SelectUser, new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[0], 3),
                          new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[1], "Отдел безопасности"),
                          new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[2], DBNull.Value)).ToList()[0];
                template.SenderUsers.ItOtdel = Inventarization.Database.SqlQuery<ItOtdel>(model.LogicaSelect.SelectUser, new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[0], 4),
                          new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[1], "Отдел информатизации"),
                          new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[2], DBNull.Value)).ToList()[0];
                return model; 
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
            return null;
        }
        /// <summary>
        /// Вытягивание данных на отдел и пользователей
        /// </summary>
        /// <param name="template">Шаблон раскладки</param>
        /// <param name="userRule">Спаршенные данные</param>
        /// <param name="sqlselect">Запрос к БД для выборки данных</param>
       public void UserRuleModel(ref RuleTemplate template, UserRules userRule, ModelSelect sqlselect)
        {
            var groupelement = userRule.User.GroupBy(x => new {x.Dates, x.Number,x.Otdel}).Select(x => new {x.Key.Number, x.Key.Dates,x.Key.Otdel}).ToList();
            int i = 0;
            foreach (var gr in groupelement)
            {
                if (template.Otdel == null)
                {
                    template.Otdel = new LibaryXMLAutoModelXmlAuto.OtdelRuleUsers.Otdel[groupelement.Count];
                }
                template.Otdel[i] = Inventarization.Database.SqlQuery<LibaryXMLAutoModelXmlAuto.OtdelRuleUsers.Otdel>(sqlselect.LogicaSelect.SelectUser, new SqlParameter(sqlselect.LogicaSelect.SelectedParametr.Split(',')[0], 1),
                          new SqlParameter(sqlselect.LogicaSelect.SelectedParametr.Split(',')[1], gr.Otdel.Replace("№ ","№")),
                          new SqlParameter(sqlselect.LogicaSelect.SelectedParametr.Split(',')[2], gr.Number)).ToList()[0];
                template.Otdel[i].Dates = gr.Dates;
                var user = userRule.User.Where(userrule =>(userrule.Dates == gr.Dates) && (userrule.Number == gr.Number) && (userrule.Otdel == gr.Otdel)).ToList();
                int j = 0;
                foreach (var userule in user)
                {
                    if (template.Otdel[i].Users == null)
                    {
                        template.Otdel[i].Users = new LibaryXMLAutoModelXmlAuto.OtdelRuleUsers.Users[user.Count];
                    }
                    template.Otdel[i].Users[j] = Inventarization.Database.SqlQuery<LibaryXMLAutoModelXmlAuto.OtdelRuleUsers.Users>(sqlselect.LogicaSelect.SelectUser, new SqlParameter(sqlselect.LogicaSelect.SelectedParametr.Split(',')[0], 2),
                          new SqlParameter(sqlselect.LogicaSelect.SelectedParametr.Split(',')[1], userule.SysName.Split('@')[0]),
                          new SqlParameter(sqlselect.LogicaSelect.SelectedParametr.Split(',')[2], DBNull.Value)).ToList()[0];
                    template.Otdel[i].Users[j].RuleTemplate = userule.Rule.Select(elem=> $"{elem.Types}: {elem.Name}").Aggregate(
                        (element, next) => element + (string.IsNullOrWhiteSpace(element) ? string.Empty : ", ") + next);
                    j++;
                }
                i++;
            }

        }


        /// <summary>
        /// Модель отчета из БД которая отправится в Open Xml
        /// </summary>
        /// <param name="report">Параметры отчета Report</param>
        /// <returns></returns>
       public void ReportInvoce(ref EfDatabaseInvoice.Report report)
        {
            try
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
            report.Main.Organization = Inventarization.Database.SqlQuery<EfDatabaseInvoice.Organization>(model.LogicaSelect.SelectUser,
                new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[0], 4),
                new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[1], report.ParamRequest.IdOut),
                new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[2], report.ParamRequest.IdUsers)).ToList()[0];
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            
            }
        }
        /// <summary>
        /// Актулизация пользователей модели
        /// </summary>
        /// <param name="isNotAktualUser">xml в виде string c неактуальными пользователями</param>
        /// <param name="isAktualUser">xml в виде string с актуальными пользователями</param>
        /// <returns></returns>
       public string ActualUserModel(string isNotAktualUser,string isAktualUser)
       {
           try
           {
               ModelSelect model = new ModelSelect {LogicaSelect = SqlSelectModel(5)};
               Inventarization.Database.ExecuteSqlCommand(model.LogicaSelect.SelectUser,
                   new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[0],isNotAktualUser),
                   new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[1],isAktualUser));
               return "Актулизация произошла успешно!!!";
           }
           catch (Exception exception)
           {
               Loggers.Log4NetLogger.Error(exception);
               return "Во время актулизации произошла ошибка смотрите Log.txt";
           }
       }
        /// <summary>
        /// Актулизация Ip Адресов в БД
        /// </summary>
        /// <returns></returns>
       public string ActualIp()
       {
            try
            {
                ModelSelect model = new ModelSelect { LogicaSelect = SqlSelectModel(19) };
                var resultcommand = new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[0],DBNull.Value) {Direction = ParameterDirection.Output, Size = 8000};
                Inventarization.Database.ExecuteSqlCommand(model.LogicaSelect.SelectUser,resultcommand);
                return (string)resultcommand.Value;
            }
            catch (Exception exception)
            {
                Loggers.Log4NetLogger.Error(exception);
                return exception.Message;
            }
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