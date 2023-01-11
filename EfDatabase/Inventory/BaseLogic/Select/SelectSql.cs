using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using EfDatabase.Inventory.Base;
using EfDatabase.Journal;
using EfDatabase.MemoReport;
using EfDatabase.ModelAksiok.ModelAksiokEditAndAdd;
using EfDatabase.ReportCard;
using EfDatabase.ReportXml.ModelFileServer;
using EfDatabaseInvoice;
using EfDatabaseParametrsModel;
using EfDatabase.XsdLotusUser;
using LibaryXMLAuto.Inventarization.ModelComparableUserAllSystem;
using LibaryXMLAuto.ModelServiceWcfCommand.ModelPathReport;
using LibaryXMLAuto.ReadOrWrite;
using LibaryXMLAuto.ModelXmlAuto.MigrationReport;
using LibaryXMLAutoModelXmlAuto.OtdelRuleUsers;
using File = EfDatabase.Inventory.Base.File;
using LogicaSelect = EfDatabaseParametrsModel.LogicaSelect;
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
                                                          NameUser = userRole.Fio, NamePosition = userRole.Dolj, IpAdress = null,
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
                var isProcessTrue = Inventory.EventProcesses.FirstOrDefault(complete => complete.IdProcess == idProcessBlock);
                if (isProcessTrue == null)
                    throw new InvalidOperationException($"Фатальная ошибка отсутствует процесс Id - {idProcessBlock} в системе!");
                if (isProcessTrue.IsComplete == true)
                {
                    var addObjectDb = new AddObjectDb.AddObjectDb();
                    var task = Task.Run(() =>
                    {
                            ModelSelect model = new ModelSelect {LogicaSelect = SqlSelectModel(idProcedureLoad)};
                            addObjectDb.IsProcessComplete(idProcessBlock, false);
                            using (var transaction = Inventory.Database.BeginTransaction())
                            {
                                try
                                {
                                    using (var buffer = new MemoryStream())
                                    {
                                        var serializer = new XmlSerializer(modelTemplate.GetType());
                                        serializer.Serialize(buffer, modelTemplate);
                                        buffer.Seek(0, SeekOrigin.Begin);
                                        using (XmlReader reader = XmlReader.Create(buffer))
                                        {
                                            Inventory.Database.CommandTimeout = 18000;
                                            Inventory.Database.ExecuteSqlCommand(model.LogicaSelect.SelectUser, new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[0], SqlDbType.Xml) { Value = new SqlXml(reader) } );
                                            reader.Close();
                                            reader.Dispose();
                                        }
                                        buffer.Close();
                                        buffer.Dispose();
                                    }
                                    transaction.Commit();
                                    Inventory.Dispose();
                                }
                                catch (Exception e)
                                {
                                    transaction.Rollback();
                                    Inventory.Dispose();
                                    Loggers.Log4NetLogger.Error(e);
                                }
                            }
                    });
                    task.ConfigureAwait(true).GetAwaiter().OnCompleted((() =>
                    {
                        addObjectDb.IsProcessComplete(idProcessBlock, true);
                        addObjectDb.Dispose();
                    }));
                    report.Note = $"{isProcessTrue.InfoEvent} запущен!";
                }
                else
                {
                    report.Note = $"{isProcessTrue.InfoEvent} уже запущен ожидайте окончание процесса!";
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
        /// Актуализация с PrintServer
        /// </summary>
        /// <returns></returns>
        public string ActualPrintServer()
        {
            try
            {
                ModelSelect model = new ModelSelect { LogicaSelect = SqlSelectModel(52) };
                var resultCommands = new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[0], DBNull.Value) { Direction = ParameterDirection.Output, Size = 8000 };
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
                            new EfDatabase.XsdLotusUser.User
                            {
                                NameUser = user.NameUser,
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
                   userLotus.User = Inventory.Database.SqlQuery<XsdLotusUser.User>(model.LogicaSelect.SelectUser,
                              new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[0], idDepNumber),
                                           new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[1], DBNull.Value)).ToArray();
               }
               else
               {
                   userLotus.User = Inventory.Database.SqlQuery<XsdLotusUser.User>(model.LogicaSelect.SelectUser,
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
        /// Журнал назначений АИС 3
        /// </summary>
        /// <param name="year">Год назначения</param>
        /// <param name="idOtdel">УН отдела</param>\
        /// <param name="isAllJournal">Параметр журнала</param>
        public AllJournal SelectJournalAis3(int year, int idOtdel, bool isAllJournal)
        {
            try
            {
                AllJournal journal = new AllJournal();
                ModelSelect model = new ModelSelect { LogicaSelect = SqlSelectModel(47) };
                journal.JournalAis3 = Inventory.Database.SqlQuery<Journal.JournalAis3>(model.LogicaSelect.SelectUser,
                    new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[0], year),
                    new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[1], idOtdel),
                    new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[2], isAllJournal)).ToArray();
                return journal;
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
            return null;
        }

        /// <summary>
        /// Выбираем людей подписантов табельных номеров
        /// </summary>
        /// <param name="model">Модель табеля</param>
        /// <returns></returns>
        public void SelectCardModelLeader(ref ReportCardModel model)
        {
            try
            {
                ModelSelect selectModel = new ModelSelect { LogicaSelect = SqlSelectModel(48) };
                if (model.SettingParameters.IdDepartment != 0)
                {
                    model.SettingParameters.TabelNumber = Inventory.Database.SqlQuery<string>(selectModel.LogicaSelect.SelectUser,
                        new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[0], 5),
                        new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[1], DBNull.Value),
                        new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[2], model.SettingParameters.Mouth.NumberMouth),
                        new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[3], model.SettingParameters.IdDepartment)).FirstOrDefault();
                }
                model.SettingParameters.LeaderN = Inventory.Database.SqlQuery<LeaderN>(selectModel.LogicaSelect.SelectUser,
                    new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[0], 1),
                                 new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[1], DBNull.Value),
                                 new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[2], model.SettingParameters.Mouth.NumberMouth),
                                 new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[3], DBNull.Value)).FirstOrDefault();
                model.SettingParameters.LeaderD = Inventory.Database.SqlQuery<LeaderD>(selectModel.LogicaSelect.SelectUser,
                    new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[0], 2),
                    new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[1], model.SettingParameters.TabelNumber),
                    new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[2], model.SettingParameters.Mouth.NumberMouth),
                    new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[3],
                        model.SettingParameters.IdDepartment != 0 ? 
                        (IConvertible)model.SettingParameters.IdDepartment
                        : DBNull.Value)).FirstOrDefault();
                model.SettingParameters.LeaderKadr = Inventory.Database.SqlQuery<LeaderKadr>(selectModel.LogicaSelect.SelectUser,
                    new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[0], 3),
                                 new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[1], DBNull.Value),
                                 new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[2], model.SettingParameters.Mouth.NumberMouth),
                                 new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[3], DBNull.Value)).FirstOrDefault();
                model.SettingParameters.Holidays = Inventory.Database.SqlQuery<Holidays>(selectModel.LogicaSelect.SelectUser,
                    new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[0], 4),
                                 new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[1], DBNull.Value),
                                 new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[2], model.SettingParameters.Mouth.NumberMouth),
                                 new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[3], DBNull.Value)).ToArray();
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
        }
        /// <summary>
        /// Генерация для служебных записок модель
        /// </summary>
        /// <param name="memoReport">Модель для служебных записок</param>
        public void SelectMemoReport(ref ModelMemoReport memoReport)
        {
            try
            {
                ModelSelect selectModel = new ModelSelect { LogicaSelect = SqlSelectModel(49) };
              
                memoReport.UserDepartment = Inventory.Database.SqlQuery<UserDepartment>(selectModel.LogicaSelect.SelectUser,
                    new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[0], 1),
                    new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[1], memoReport.SelectParameterDocument.IdUser),
                    new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[2], DBNull.Value),
                    new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[3], DBNull.Value)).FirstOrDefault();
                memoReport.Executor = Inventory.Database.SqlQuery<Executor>(selectModel.LogicaSelect.SelectUser,
                    new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[0], 2),
                    new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[1], memoReport.SelectParameterDocument.IdUser),
                    new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[2], memoReport.SelectParameterDocument.TabelNumber),
                    new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[3], DBNull.Value)).FirstOrDefault();
                memoReport.BossDepartment = Inventory.Database.SqlQuery<BossDepartment>(selectModel.LogicaSelect.SelectUser,
                    new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[0], 3),
                    new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[1], memoReport.SelectParameterDocument.IdUser),
                    new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[2], memoReport.SelectParameterDocument.TabelNumber),
                    new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[3], DBNull.Value)).FirstOrDefault();
                memoReport.BossAgreed = Inventory.Database.SqlQuery<BossAgreed>(selectModel.LogicaSelect.SelectUser,
                    new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[0], 4),
                    new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[1], memoReport.SelectParameterDocument.IdUser),
                    new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[2], memoReport.SelectParameterDocument.TabelNumber),
                    new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[3], memoReport.SelectParameterDocument.TypeDocument)).FirstOrDefault();
                memoReport.LeaderOrganization = Inventory.Database.SqlQuery<LeaderOrganization>(selectModel.LogicaSelect.SelectUser,
                    new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[0], 5),
                    new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[1], memoReport.SelectParameterDocument.IdUser),
                    new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[2], DBNull.Value),
                    new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[3], DBNull.Value)).FirstOrDefault();
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
        }
        /// <summary>
        /// Возврат дополнительных атрибутов моделей АКСИОК
        /// </summary>
        /// <param name="idModel">Ун интересуем ого объекта</param>
        /// <returns></returns>
        public ModelAksiok.DtoAksiok.ValueCharacteristicJson SelectModelCharacteristicJson(int idModel)
        {
            try
            {
                ModelSelect selectModel = new ModelSelect { LogicaSelect = SqlSelectModel(56) };
                return Inventory.Database.SqlQuery<ModelAksiok.DtoAksiok.ValueCharacteristicJson>(
                        selectModel.LogicaSelect.SelectUser,
                        new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[0], idModel))
                    .FirstOrDefault();
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
            return null;
        }
        /// <summary>
        /// Проверка модели на действия с ней Редактирование или Добавление
        /// </summary>
        /// <param name="aksiokAddAndEdit">Транспортная модель с параметрами</param>
        /// <returns></returns>
        public AksiokAddAndEdit ModelValidation(AksiokAddAndEdit aksiokAddAndEdit)
        {
            try
            {
                ModelSelect selectModel = new ModelSelect { LogicaSelect = SqlSelectModel(57) };
                var idFullCategoria = new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[3], aksiokAddAndEdit.ParametersModel.IdFullCategoria) {Direction = ParameterDirection.Output, SqlDbType = SqlDbType.Int};
                var codeError = new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[4], aksiokAddAndEdit.ParametersModel.CodeError) {Direction = ParameterDirection.Output, SqlDbType = SqlDbType.Int};
                var errorServer = new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[5], aksiokAddAndEdit.ParametersModel.ErrorServer) {Direction = ParameterDirection.Output, Size = 512, SqlDbType = SqlDbType.VarChar};
                var idState = new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[6], aksiokAddAndEdit.ParametersModel.IdState) { Direction = ParameterDirection.Output, SqlDbType = SqlDbType.Int };
                var idStateSto = new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[7], aksiokAddAndEdit.ParametersModel.IdStateSto) { Direction = ParameterDirection.Output, SqlDbType = SqlDbType.Int };
                var idExpertise = new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[8], aksiokAddAndEdit.ParametersModel.IdExpertise) { Direction = ParameterDirection.Output, SqlDbType = SqlDbType.Int };
                var yearOfIssue = new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[9], aksiokAddAndEdit.ParametersModel.IdExpertise) { Direction = ParameterDirection.Output, SqlDbType = SqlDbType.Int };
                var exploitationStartYear = new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[10], aksiokAddAndEdit.ParametersModel.IdExpertise) { Direction = ParameterDirection.Output, SqlDbType = SqlDbType.Int };
                Inventory.Database.ExecuteSqlCommand(selectModel.LogicaSelect.SelectUser,
                    new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[0],
                        aksiokAddAndEdit.ParametersModel.ModelRequest),
                    new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[1],
                        aksiokAddAndEdit.ParametersModel.SerNumber),
                    new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[2],
                        aksiokAddAndEdit.ParametersModel.InventoryNum),
                    idFullCategoria, codeError, errorServer, idState, idStateSto, idExpertise, yearOfIssue, exploitationStartYear
                );
                if (idFullCategoria.Value != DBNull.Value)
                {
                    aksiokAddAndEdit.ParametersModel.IdFullCategoria = (int)idFullCategoria.Value;
                    aksiokAddAndEdit.ParametersModel.IdState = (int)idState.Value;
                    aksiokAddAndEdit.ParametersModel.IdStateSto = (int)idStateSto.Value;
                    aksiokAddAndEdit.ParametersModel.IdExpertise = (int)idExpertise.Value;
                    aksiokAddAndEdit.ParametersModel.YearOfIssue = (int) yearOfIssue.Value;
                    aksiokAddAndEdit.ParametersModel.ExploitationStartYear = (int)exploitationStartYear.Value;
                }
                aksiokAddAndEdit.ParametersModel.CodeError = (int)codeError.Value;
                aksiokAddAndEdit.ParametersModel.ErrorServer = (string)errorServer.Value;
                return aksiokAddAndEdit;
            }
            //В случае исключения
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
                aksiokAddAndEdit.ParametersModel.CodeError = 404;
                aksiokAddAndEdit.ParametersModel.ErrorServer = e.Message;
            }
            return aksiokAddAndEdit;
        }
        /// <summary>
        /// Проверка комплектов оборудования 
        /// </summary>
        /// <param name="kitsEquipment">Параметры для проверки комплектов оборудования</param>
        /// <returns></returns>
        public KitsEquipment ModelValidationKits(KitsEquipment kitsEquipment)
        {
            try
            {
                XmlReadOrWrite xml = new XmlReadOrWrite();
                ModelSelect selectModel = new ModelSelect { LogicaSelect = SqlSelectModel(58) };
                var errorServer = new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[1], kitsEquipment.ErrorServer) { Direction = ParameterDirection.Output, Size = 512, SqlDbType = SqlDbType.VarChar };
                var xmlModel = new  SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[2],null) { Direction = ParameterDirection.Output,  SqlDbType = SqlDbType.Xml };
                Inventory.Database.ExecuteSqlCommand(selectModel.LogicaSelect.SelectUser, new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[0], kitsEquipment.InventoryNum), errorServer, xmlModel
                );
                if (xmlModel.Value != DBNull.Value)
                {
                    kitsEquipment.KitsEquipmentServer = ((KitsEquipment)xml.ReadXmlText((string) xmlModel.Value, typeof(KitsEquipment))).KitsEquipmentServer;
                }
                else
                {
                    kitsEquipment.ErrorServer = (string)errorServer.Value;
                }
            }
            //В случае исключения
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
                kitsEquipment.ErrorServer = e.Message;
            }
            return kitsEquipment;
        }
        /// <summary>
        /// Сбор модели для отпраки на сервер для редактирования
        /// </summary>
        /// <param name="aksiokAddAndEdit">Модель параметров</param>
        /// <returns></returns>
        public AksiokEditAndAddProcedure ReturnModelAksiokEditAndAdd(AksiokAddAndEdit aksiokAddAndEdit)
        {
            try
            {
                var aksiokAddAndEditReturn = new AksiokEditAndAddProcedure();
                var xml = new XmlReadOrWrite();
                var selectModel = new ModelSelect { LogicaSelect = SqlSelectModel(59) };

                var xmlModel = new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[6], null) { Direction = ParameterDirection.Output, SqlDbType = SqlDbType.Xml };
                Inventory.Database.ExecuteSqlCommand(selectModel.LogicaSelect.SelectUser,
                    new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[0], aksiokAddAndEdit.ParametersRequestAksiok.IdType),
                    new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[1], aksiokAddAndEdit.ParametersRequestAksiok.IdProducer),
                    new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[2], aksiokAddAndEdit.ParametersRequestAksiok.IdModel),
                    new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[3], aksiokAddAndEdit.ParametersModel.SerNumber),
                    new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[4], aksiokAddAndEdit.ParametersModel.ModelRequest),
                    new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[5], 1), xmlModel,
                    new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[7], aksiokAddAndEdit.ParametersRequestAksiok.IdState),
                    new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[8], aksiokAddAndEdit.ParametersRequestAksiok.IdStateSto),
                    new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[9], aksiokAddAndEdit.ParametersRequestAksiok.IdExpertise),
                    new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[10], aksiokAddAndEdit.ParametersModel.YearOfIssue),
                    new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[11], aksiokAddAndEdit.ParametersModel.ExploitationStartYear));
                if (xmlModel.Value == DBNull.Value)
                {
                    return null;
                }
                aksiokAddAndEditReturn.AksiokEditPublicModel = ((AksiokEditAndAddProcedure) xml.ReadXmlText((string) xmlModel.Value, typeof(AksiokEditAndAddProcedure))).AksiokEditPublicModel;
                aksiokAddAndEditReturn.PublicModelValueJson = Inventory.Database.SqlQuery<PublicModelValueJson>(selectModel.LogicaSelect.SelectUser,
                    new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[0], aksiokAddAndEdit.ParametersRequestAksiok.IdType),
                    new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[1], aksiokAddAndEdit.ParametersRequestAksiok.IdProducer),
                    new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[2], aksiokAddAndEdit.ParametersRequestAksiok.IdModel),
                    new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[3], aksiokAddAndEdit.ParametersModel.SerNumber),
                    new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[4], aksiokAddAndEdit.ParametersModel.ModelRequest),
                    new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[5], 2), xmlModel,
                    new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[7], aksiokAddAndEdit.ParametersRequestAksiok.IdState),
                    new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[8], aksiokAddAndEdit.ParametersRequestAksiok.IdStateSto),
                    new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[9], aksiokAddAndEdit.ParametersRequestAksiok.IdExpertise),
                    new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[10], aksiokAddAndEdit.ParametersModel.YearOfIssue),
                    new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[11], aksiokAddAndEdit.ParametersModel.ExploitationStartYear)).FirstOrDefault();
                if (aksiokAddAndEditReturn.PublicModelValueJson == null)
                {
                    return null;
                }
                return aksiokAddAndEditReturn;
            }
            //В случае исключения
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
            return null;
        }
        /// <summary>
        /// Выгрузка файла с сервера
        /// </summary>
        /// <param name="idFile">Ун файла</param>
        /// <returns></returns>
        public DownloadFileServer SelectFile(int idFile)
        {
            ModelSelect selectModel = new ModelSelect { LogicaSelect = SqlSelectModel(70) };
            DownloadFileServer downloadFileServer = Inventory.Database.SqlQuery<DownloadFileServer>(selectModel.LogicaSelect.SelectUser,
                new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[0], idFile)).FirstOrDefault();
            if (downloadFileServer != null && System.IO.File.Exists(downloadFileServer.FullPathFile))
            {
                downloadFileServer.FileByte = FileArray(downloadFileServer.FullPathFile);
                return downloadFileServer;
            }

            return downloadFileServer;
        }
        /// <summary>
        /// Выгрузка детализации файла с сервера со всеми авторами
        /// </summary>
        /// <param name="idFile">Ун файла</param>
        /// <returns></returns>
        public ModelFileDetals SelectModelFileDetals(int idFile)
        {
            ModelSelect selectModel = new ModelSelect { LogicaSelect = SqlSelectModel(71) };
            ModelFileDetals modelFile = Inventory.Database.SqlQuery<ModelFileDetals>(selectModel.LogicaSelect.SelectUser,
                new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[0], idFile),
                new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[1], 1)).FirstOrDefault();
            if (modelFile == null) return null;
            modelFile.AllAutorFile = Inventory.Database.SqlQuery<string>(selectModel.LogicaSelect.SelectUser,
                new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[0], idFile),
                new SqlParameter(selectModel.LogicaSelect.SelectedParametr.Split(',')[1], 2)).ToArray();
            return modelFile;

        }


        /// <summary>
        /// Выгрузка файла из АКСИОК
        /// </summary>
        /// <param name="aksiokAddAndEdit">Модель файла</param>
        /// <returns>Ун файла для url</returns>
        public long? SelectFileId(AksiokAddAndEdit aksiokAddAndEdit)
        {
            long? fileId = null;
            switch (aksiokAddAndEdit.ParametersModel.ModelRequest)
            {
                case "ExpertiseFile":
                    fileId = Inventory.EpoDocuments.Where(model=>model.SerialNumber== aksiokAddAndEdit.ParametersModel.SerNumber).Select(x=>x.IdExpertiseFile).FirstOrDefault();
                    break;
                case "FileAct":
                    fileId = Inventory.EpoDocuments.Where(model => model.SerialNumber == aksiokAddAndEdit.ParametersModel.SerNumber).Select(x => x.IdFile).FirstOrDefault();
                    break;
            }
            return fileId;
        }
        /// <summary>
        /// Модель возврата отчета на сервер
        /// </summary>
        /// <typeparam name="T">Тип получаемого объекта</typeparam>
        /// <param name="selectLogic">Sql запрос для получения объекта</param>
        /// <returns></returns>
        public T[] SelectObjectModelSql<T>(LogicaSelect selectLogic)
        {
            try
            {
               return Inventory.Database.SqlQuery<T>(selectLogic.SelectUser).ToArray();
            }
            //В случае исключения
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
        /// Выгрузка и удаление файла отчета
        /// </summary>
        /// <param name="fullPathFile">Полный путь к файлу</param>
        /// <returns></returns>
        private byte[] FileArray(string fullPathFile)
        {
            return System.IO.File.ReadAllBytes(fullPathFile);
        }
        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Inventory.Database.Connection.Close();
                Inventory.Database.Connection.Dispose();
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