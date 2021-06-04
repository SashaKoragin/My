using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using EfDatabase.Inventory.Base;
using EfDatabaseInvoice;
using EfDatabaseParametrsModel;
using EfDatabaseXsdLotusUser;
using LibaryXMLAuto.ModelServiceWcfCommand.ModelPathReport;
using LibaryXMLAuto.ReadOrWrite;
using LibaryXMLAuto.ModelXmlAuto.MigrationReport;
using LibaryXMLAutoModelXmlAuto.OtdelRuleUsers;
using Otdel = LibaryXMLAutoModelXmlAuto.OtdelRuleUsers.Otdel;
using RuleTemplate = LibaryXMLAutoModelXmlAuto.OtdelRuleUsers.RuleTemplate;


namespace EfDatabase.Inventory.BaseLogic.Select
{
    public static class DateTimeExtensions
    {
        public static DateTime AddWorkdays(this DateTime originalDate, int workDays)
        {
            DateTime tmpDate = originalDate;
            while (workDays > 0)
            {
                tmpDate = tmpDate.AddDays(1);
                if (tmpDate.DayOfWeek < DayOfWeek.Saturday &&
                    tmpDate.DayOfWeek > DayOfWeek.Sunday &&
                    !tmpDate.IsHoliday())
                    workDays--;
            }
            while (workDays < 0)
            {
                tmpDate = tmpDate.AddDays(-1);
                if (tmpDate.DayOfWeek < DayOfWeek.Saturday &&
                    tmpDate.DayOfWeek > DayOfWeek.Sunday &&
                    !tmpDate.IsHoliday())
                    workDays++;
            }
            return tmpDate;
        }

        public static bool IsHoliday(this DateTime originalDate)
        {
            SelectSql select = new SelectSql();
            var isHoliday = select.IsHolidays(originalDate);
            select.Dispose();
            return isHoliday;
        }
    }

    public class SelectSql : IDisposable
   {
       public static string ProcedureSelect = "Exec [dbo].[InventarizationCommandSelectWcfToSql] {0}";
        public InventoryContext Inventory { get; set; }

        public SelectSql()
        {
            Inventory?.Dispose();

            Inventory = new InventoryContext();
        }
        /// <summary>
        /// Получение всех api из БД
        /// </summary>
        public ServiceModelInventory[] GetServiceApi()
        {
            return Inventory.ServiceModelInventories.ToArray();
        }

        public bool IsHolidays(DateTime dateTime)
        {
           return Inventory.Rb_Holidays.Any(x => x.DateTime_Holiday == dateTime && x.IS_HOLIDAY);
        }
        /// <summary>
        /// Генерация модели с параметрами для пользовательских выборок
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ModelSelect SqlSelect(ModelSelect model)
        {
            try
            {
                model.LogicaSelect = SqlSelectModel(model.ParametrsSelect.Id);
                if (model.LogicaSelect.SelectedParametr != null)
                {
                    model.Parametrs = Inventory.Database.SqlQuery<Parametrs>(model.LogicaSelect.SelectedParametr).ToArray();
                }
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
                template.SenderUsers.Security = Inventory.Database.SqlQuery<Security>(model.LogicaSelect.SelectUser, new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[0], 3),
                          new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[1], "Отдел информационной безопасности"),
                          new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[2], DBNull.Value)).FirstOrDefault();
                template.SenderUsers.ItOtdel = Inventory.Database.SqlQuery<ItOtdel>(model.LogicaSelect.SelectUser, new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[0], 4),
                          new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[1], "Отдел информатизации"),
                          new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[2], DBNull.Value)).FirstOrDefault();
                template.SenderUsers.SenderRukovodstvo = Inventory.Database.SqlQuery<SenderRukovodstvo>(model.LogicaSelect.SelectUser, new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[0], 5),
                          new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[1], "Руководство"),
                          new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[2], DBNull.Value)).FirstOrDefault();
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
        /// <param name="userRule">Данные ролей из АИС 3</param>
        /// <param name="sqlSelect">Запрос к БД для выборки данных</param>
        public void UserRuleModel(ref RuleTemplate template, UserRules userRule, ModelSelect sqlSelect)
        {
            //Группируем по номеру, отделу, дате и назначению
            var groupElement = userRule.User.Where(x => x.Number != "Скрипт").SelectMany(x => x.Rule, (u, r) => new
                {
                    u, r
                }).GroupBy(x => new {x.r.Pushed, x.u.Dates, x.u.Number, x.u.Otdel })
                .Select(x => new
                {
                    x.Key.Number, 
                    x.Key.Dates, 
                    x.Key.Otdel, 
                    x.Key.Pushed
                }).ToList();
            int i = 0;
            foreach (var gr in groupElement)
            {
                if (template.Otdel == null)
                {
                    template.Otdel = new Otdel[groupElement.Count];
                }
                template.Otdel[i] = Inventory.Database.SqlQuery<Otdel>(
                                        sqlSelect.LogicaSelect.SelectUser,
                                        new SqlParameter(sqlSelect.LogicaSelect.SelectedParametr.Split(',')[0], 1),
                                        new SqlParameter(sqlSelect.LogicaSelect.SelectedParametr.Split(',')[1],
                                            gr.Otdel.Replace("№ ", "№")),
                                        new SqlParameter(sqlSelect.LogicaSelect.SelectedParametr.Split(',')[2],
                                            gr.Number)).FirstOrDefault() ??
                                    new Otdel()
                                    {
                                        Number = gr.Number, NameOtdel = "Ошибка в наименование отдела Кадры,AD,АИС3",
                                        RnameOtdel = "Ошибка в наименование отдела Кадры,AD,АИС3",
                                        SmallName = "Отсутствует", NamePosition = "Отсутствует"
                                    };
                template.Otdel[i].Dates = Convert.ToDateTime(gr.Dates);
                template.Otdel[i].DateStatement = Convert.ToDateTime(gr.Dates).AddWorkdays(-1);
                var user = userRule.User.SelectMany(x => x.Rule, (u, r) => new
                                          { u, r }).Where(userRole => 
                                                   (userRole.u.Dates == gr.Dates) &&
                                                   (userRole.u.Number == gr.Number) &&
                                                   (userRole.r.Pushed == gr.Pushed) &&
                                                   (userRole.u.Otdel.Replace("№ ", "№") == gr.Otdel.Replace("№ ", "№"))
                 ).Select(mussel => new
                {
                    mussel.u.Dates,
                    mussel.u.Fio,
                    mussel.u.SysName,
                    mussel.u.Dolj,
                    Otdel = mussel.u.Otdel.Replace("№ ", "№"),
                    mussel.u.Number
                }).Distinct().ToList();
                int j = 0;
                foreach (var userRole in user)
                {

                    var roleAll = userRule.User.Where(u =>
                            u.Dates == userRole.Dates && u.Dolj == userRole.Dolj && u.Otdel.Replace("№ ", "№") == userRole.Otdel &&
                            u.Fio == userRole.Fio && u.SysName == userRole.SysName && u.Number == userRole.Number).
                        Select(x => x.Rule.Where(r => r.Pushed == gr.Pushed)).Aggregate((element, next) => element.Concat(next).ToArray()).ToArray();

                    if (template.Otdel[i].Users == null)
                    {
                        template.Otdel[i].Users = new LibaryXMLAutoModelXmlAuto.OtdelRuleUsers.Users[user.Count];
                    }
                    template.Otdel[i].Users[j] = Inventory.Database.SqlQuery<LibaryXMLAutoModelXmlAuto.OtdelRuleUsers.Users>(sqlSelect.LogicaSelect.SelectUser, new SqlParameter(sqlSelect.LogicaSelect.SelectedParametr.Split(',')[0], 2),
                                                     new SqlParameter(sqlSelect.LogicaSelect.SelectedParametr.Split(',')[1], userRole.SysName.Split('@')[0]),
                                                     new SqlParameter(sqlSelect.LogicaSelect.SelectedParametr.Split(',')[2], DBNull.Value)).FirstOrDefault() ??
                                                     new LibaryXMLAutoModelXmlAuto.OtdelRuleUsers.Users()
                                                     {
                                                          Name = userRole.Fio, NamePosition = userRole.Dolj, IpAdress = null,
                                                          Tabel = $"regions\\{userRole.SysName.Split('@')[0]}", NumberKabinet = null,
                                                          RuleTemplate = null
                                                     };
                    template.Otdel[i].Users[j].RuleTemplate = roleAll.Select(elem=> $"{elem.Types}: {elem.Name} {((string.IsNullOrWhiteSpace(elem.DateFinish) || elem.Pushed == "Отзыв") ? null :" - " + elem.DateFinish)}").Aggregate(
                        (element, next) => element + (string.IsNullOrWhiteSpace(element) ? string.Empty : ", ") + next);
                    template.Otdel[i].Users[j].Pushed = roleAll[0].Pushed;
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
       public void ReportInvoice(ref EfDatabaseInvoice.Report report)
        {
            try
            {
                ModelSelect model = new ModelSelect {LogicaSelect = SqlSelectModel(report.ParamRequest.IdSelect)};
                report.Main.Formed = Inventory.Database.SqlQuery<Formed>(model.LogicaSelect.SelectUser,
                    new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[0],1),
                    new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[1], report.ParamRequest.IdOut),
                    new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[2], report.ParamRequest.IdUsers)).ToList()[0];
                report.Main.Received = Inventory.Database.SqlQuery<Received>(model.LogicaSelect.SelectUser,
                    new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[0], 2),
                    new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[1], report.ParamRequest.IdOut),
                    new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[2], report.ParamRequest.IdUsers)).ToList()[0];
                report.Body = Inventory.Database.SqlQuery<Body>(model.LogicaSelect.SelectUser,
                    new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[0], 3),
                    new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[1], report.ParamRequest.IdOut),
                    new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[2], report.ParamRequest.IdUsers)).ToArray();
                report.Main.Organization = Inventory.Database.SqlQuery<EfDatabaseInvoice.Organization>(model.LogicaSelect.SelectUser,
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
        /// Модель загрузки Шаблонов в БД по шаблону
        /// </summary>
        /// <typeparam name="T">Шаблон class to xml</typeparam>
        /// <param name="modelTemplate">Шаблон</param>
        /// <param name="idProcedureLoad">УН процедуры загрузки</param>
        /// <param name="idProcessBlock">УН процедуры процесса</param>
        /// <returns></returns>
        public ModelPathReport LoadModelToDataBase<T>(T modelTemplate, int idProcedureLoad, int idProcessBlock)
        {
            var report = new ModelPathReport();
            try
            {
                var isProcessTrue = Inventory.IsProcessCompletes.FirstOrDefault(complete => complete.Id == idProcessBlock);
                if (isProcessTrue == null)
                    throw new InvalidOperationException($"Фатальная ошибка отсутствует процесс Id - {idProcessBlock} в системе!");
                if (isProcessTrue.IsComplete == true)
                {
                    var addObjectDb = new AddObjectDb.AddObjectDb();
                    var task = Task.Run(() =>
                    {
                        try
                        {
                            ModelSelect model = new ModelSelect {LogicaSelect = SqlSelectModel(idProcedureLoad)};
                            XmlReadOrWrite xml = new XmlReadOrWrite();
                            addObjectDb.IsProcessComplete(idProcessBlock, false);
                            Inventory.Database.CommandTimeout = 18000;
                            Inventory.Database.ExecuteSqlCommand(model.LogicaSelect.SelectUser,
                                new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[0], SqlDbType.Xml)
                                {
                                    Value = new SqlXml(new XmlTextReader(xml.ClassToXml(modelTemplate, modelTemplate.GetType()), XmlNodeType.Document, null))
                                });
                        }
                        catch (Exception e)
                        {
                            Loggers.Log4NetLogger.Error(e);
                        }
                    });
                    task.ConfigureAwait(true).GetAwaiter().OnCompleted((() =>
                    {
                        addObjectDb.IsProcessComplete(idProcessBlock, true);
                        addObjectDb.Dispose();
                    }));
                    report.Note = $"{isProcessTrue.NameProcess} запущен!";
                }
                else
                {
                    report.Note = $"{isProcessTrue.NameProcess} уже запущен ожидайте окончание процесса!";
                }
            }
            catch (Exception e)
            {
                report.Note = e.Message;
                Loggers.Log4NetLogger.Error(e);
            }
            return report;
        }

        /// <summary>
        /// Актуализация пользователей модели
        /// </summary>
        /// <param name="isActualizationUser">xml в виде string с актуальными пользователями</param>
        /// <returns></returns>
        public string ActualUserModel(string isActualizationUser)
       {
           try
           {
               ModelSelect model = new ModelSelect {LogicaSelect = SqlSelectModel(5)};
               Inventory.Database.ExecuteSqlCommand(model.LogicaSelect.SelectUser,
                   new SqlParameter(model.LogicaSelect.SelectedParametr, isActualizationUser));
               return "Актуализация произошла успешно!!!";
           }
           catch (Exception exception)
           {
               Loggers.Log4NetLogger.Error(exception);
               return "Во время актуализации произошла ошибка смотрите Log.txt";
           }
       }
        /// <summary>
        /// Актуализация Ip Адресов в БД
        /// </summary>
        /// <returns></returns>
       public string ActualIp()
       {
            try
            {
                ModelSelect model = new ModelSelect { LogicaSelect = SqlSelectModel(19) };
                var resultCommands = new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[0],DBNull.Value) {Direction = ParameterDirection.Output, Size = 8000};
                Inventory.Database.ExecuteSqlCommand(model.LogicaSelect.SelectUser, resultCommands);
                return (string)resultCommands.Value;
            }
            catch (Exception exception)
            {
                Loggers.Log4NetLogger.Error(exception);
                return exception.Message;
            }
       }

        /// <summary>
        /// Снятие статуса по технике 
        /// </summary>
        /// <param name="technic">Техника</param>
        /// <returns></returns>
        public string CheckStatus(AllTechnic technic)
        {
            try
            {
                ModelSelect model = new ModelSelect { LogicaSelect = SqlSelectModel(31) };
                Inventory.Database.ExecuteSqlCommand(model.LogicaSelect.SelectUser,
                    new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[0], technic.Id),
                                 new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[1], technic.Item));
                return "Статус снят!!!";
            }
            catch (Exception exception)
            {
                Loggers.Log4NetLogger.Error(exception);
                return "Во время снятия статуса произошла ошибка смотрите Log.txt";
            }
        }

       /// <summary>
       /// Поиск пользователя идентификатора пользователя в БД Инвентаризация
       /// </summary>
       /// <param name="keyUser">Идентификаторы пользователей</param>
       /// <returns></returns>
       public UserLotus FindUserSqlKey(string[] keyUser)
       {
            try
            {
                UserLotus userLotus = new UserLotus
                {
                    User = (from id in Inventory.MailIdentifiers
                        join user in Inventory.Users on id.IdUser equals user.IdUser
                        join otdel in Inventory.Otdels on user.IdOtdel equals otdel.IdOtdel
                        where keyUser.Any(idUser => idUser.Contains(id.IdentifierUser))
                        select
                            new EfDatabaseXsdLotusUser.User
                            {
                                Name = user.Name,
                                TabelNumber = user.TabelNumber,
                                NameOtdel = otdel.NameOtdel,
                                IdentifierUser = id.IdentifierUser
                            }).ToArray()
                };
                return userLotus;
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
            return null;
       }

        /// <summary>
        /// Поиск пользователя или группы по алгоритму
        /// </summary>
        /// <param name="userDefault">Пользователь по умолчанию</param>
        /// <param name="nameFindTextGroupOnUser">Параметры поиска пользователя или группы по умолчанию возвращает пользователя</param>
        /// <returns></returns>
       public UserLotus FindUserOnUserGroup(UserLotus userDefault, string nameFindTextGroupOnUser)
       {
            if (nameFindTextGroupOnUser != null)
            {
                UserLotus userSql = null;
                if (nameFindTextGroupOnUser.Length <= 2 && Regex.IsMatch(nameFindTextGroupOnUser, @"\d"))
                {
                    //Ищем группу по номеру группы
                    userSql = FindUserGroup(Convert.ToInt32(nameFindTextGroupOnUser));
                }
                //Ищем пользователя
                if (nameFindTextGroupOnUser.Length >= 5)
                {
                   var userFindSender = Regex.Matches(nameFindTextGroupOnUser, @"(0[0-1][0-9]{3})").Cast<Match>().Select(m=>m.Value).ToArray();
                   if (userFindSender.Length > 0)
                   {
                     userSql = FindUserSqlKey(userFindSender);
                   }
                }
                //Если нашли не дефектных пользователей
                if (userSql?.User != null && userSql.User.Length > 0)
                {
                    userDefault = userSql;
                }
                else
                {
                    Loggers.Log4NetLogger.Info(new Exception($"Пользователь или группы с идентификаторами ' {nameFindTextGroupOnUser} ' не найден в БД Sql Инвентаризация идет рассылка на стандартную группу (OIT)"));
                }
            }
            return userDefault;
       }

        /// <summary>
        /// Поиск по группе абонентов
        /// </summary>
        /// <param name="idDepNumber">Номер департамента</param>
        /// <param name="nameGroup">Наименование группы</param>
        /// <returns></returns>
       public UserLotus FindUserGroup(int? idDepNumber = null, string nameGroup = null)
       {
           try
           {
               UserLotus userLotus = new UserLotus();
               ModelSelect model = new ModelSelect { LogicaSelect = SqlSelectModel(26) };
               if (idDepNumber != null)
               {
                   userLotus.User = Inventory.Database.SqlQuery<EfDatabaseXsdLotusUser.User>(model.LogicaSelect.SelectUser,
                              new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[0], idDepNumber),
                                           new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[1], DBNull.Value)).ToArray();
               }
               else
               {
                   userLotus.User = Inventory.Database.SqlQuery<EfDatabaseXsdLotusUser.User>(model.LogicaSelect.SelectUser,
                       new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[0], DBNull.Value),
                       new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[1], nameGroup)).ToArray();
               }
               return userLotus;

           }
           catch (Exception e)
           {
               Loggers.Log4NetLogger.Error(e);
           }
           return null;
        }




        /// <summary>
        /// Выборка модели для манипуляции
        /// </summary>
        /// <param name="id">Параметр индекса в таблицы</param>
        /// <returns></returns>
        public EfDatabaseParametrsModel.LogicaSelect SqlSelectModel(int id)
       {
            return Inventory.Database.SqlQuery<EfDatabaseParametrsModel.LogicaSelect>(String.Format(ProcedureSelect, id)).ToList()[0];
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