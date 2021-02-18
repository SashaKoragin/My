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
using LibaryXMLAuto.ReadOrWrite;
using LibaryXMLAutoModelXmlAuto.MigrationReport;
using LibaryXMLAutoModelXmlAuto.OtdelRuleUsers;
using Otdel = LibaryXMLAutoModelXmlAuto.OtdelRuleUsers.Otdel;



namespace EfDatabase.Inventory.BaseLogic.Select
{
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
        /// Генерация модели с параметрами для пользовательских выборок
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ModelSelect SqlSelect(ModelSelect model)
        {
            try
            {
                model.LogicaSelect = SqlSelectModel(model.ParametrsSelect.Id);
                model.Parametrs = Inventory.Database.SqlQuery<Parametrs>(model.LogicaSelect.SelectedParametr).ToArray();
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
            var groupElement = userRule.User.Where(x=>x.Number!="Скрипт").Select(x => new
                { x.Dates, 
                  x.Number, 
                  Otdel = x.Otdel.Replace("№ ", "№")
                }).GroupBy(x => new {x.Dates, x.Number, x.Otdel }).Select(x => new {x.Key.Number, x.Key.Dates, x.Key.Otdel }).ToList();
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
                template.Otdel[i].Dates = gr.Dates;
                var user = userRule.User.Where(userRole => (userRole.Dates == gr.Dates) && (userRole.Number == gr.Number) && (userRole.Otdel.Replace("№ ", "№") == gr.Otdel)).Select(u => new
                {
                    u.Dates,
                    u.Fio,
                    u.SysName,
                    u.Dolj,
                    Otdel = u.Otdel.Replace("№ ", "№"),
                    u.Number
                }).Distinct().ToList();
                int j = 0;
                foreach (var userRole in user)
                {
                    var roleAll = userRule.User.Where(u =>
                                  u.Dates == userRole.Dates && u.Dolj == userRole.Dolj && u.Otdel.Replace("№ ", "№") == userRole.Otdel &&
                                  u.Fio == userRole.Fio && u.SysName == userRole.SysName && u.Number == userRole.Number).
                                  Select(x => x.Rule).Aggregate((element, next) => element.Concat(next).ToArray());
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
                    template.Otdel[i].Users[j].RuleTemplate = roleAll.Select(elem=> $"{elem.Types}: {elem.Name}").Aggregate(
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
        /// Загрузка шаблонов в Базу данных
        /// </summary>
        /// <param name="infoTemplate">Модель шаблонов</param>
        public void LoadTemplateDataBase(InfoRuleTemplate infoTemplate)
        {
            ModelSelect model = new ModelSelect { LogicaSelect = SqlSelectModel(37) };
            XmlReadOrWrite xml = new XmlReadOrWrite();
            var addObjectDb = new AddObjectDb.AddObjectDb();
            addObjectDb.IsProcessComplete(2,false);
            Inventory.Database.CommandTimeout = 18000;
            Inventory.Database.ExecuteSqlCommand(model.LogicaSelect.SelectUser, 
                new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[0], SqlDbType.Xml)
                {
                    Value = new SqlXml(new XmlTextReader(xml.ClassToXml(infoTemplate, typeof(InfoRuleTemplate)),
                    XmlNodeType.Document, null))
                });
            addObjectDb.IsProcessComplete(2, true);
            addObjectDb.Dispose();
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
                ModelSelect model = new ModelSelect { LogicaSelect = SqlSelectModel(32) };
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