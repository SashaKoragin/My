using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using EfDatabase.Inventory.Base;
using EfDatabaseParametrsModel;
using EfDatabaseXsdSupportNalog;


namespace EfDatabase.Inventory.BaseLogic.Select
{
   public class GenerateParameterSupport : IDisposable
   {
        public InventoryContext Inventory { get; set; }
        /// <summary>
        /// Маршрут до групп пользователей
        /// </summary>
        public string PathDomainGroup { get; set; }
        /// <summary>
        /// Инициализация
        /// </summary>
        public GenerateParameterSupport()
        {
            InitializationSql();
        }
        /// <summary>
        /// Инициализация
        /// </summary>
        /// <param name="groupPath">Наименование группы путь к AD</param>
        public GenerateParameterSupport(string groupPath)
        {
           PathDomainGroup = groupPath;
           InitializationSql();
        }

        /// <summary>
        /// Генерация параметров для запроса на СТО
        /// </summary>
        /// <param name="modelSupport">Модель запроса на сервис</param>
       public void GenerateTemplateUrlParameter(ref ModelParametrSupport modelSupport)
       {
           SelectSql select = new SelectSql();
           ModelSelect model = new ModelSelect { LogicaSelect = select.SqlSelectModel(28) };
           if (modelSupport.IdSysBlock == 0)
           {
               var idUser = modelSupport.IdUser;
               modelSupport.IdSysBlock = Inventory.SysBlocks.Where(x => x.IdUser == idUser).Select(sel=>sel.IdSysBlock).FirstOrDefault();
           }
           modelSupport.TemplateSupport = Inventory.Database.SqlQuery<TemplateSupport1>(model.LogicaSelect.SelectUser,
               new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[0], modelSupport.IdTemplate)).ToArray();
           if (modelSupport?.TemplateSupport != null && modelSupport?.TemplateSupport.Length > 0)
           {
               var description = modelSupport.Discription;
               modelSupport.TemplateSupport.Where(discription => discription.NameGuidParametr == "UF_DESCRIPTION").ToList().ForEach(d=>d.Parametr = description);
               var modelParameterInputStep2 = modelSupport.TemplateSupport.Where(temple =>temple.NameStepSupport == "Step2" && temple.TemplateParametrType != null);
             
               foreach (var templateStep2 in modelParameterInputStep2)
               {
                   if (templateStep2.TemplateParametrType.Equals("CalendarVKS") && modelSupport.IdCalendarVks != 0)
                   {
                       templateStep2.Parametr = Inventory.Database.SqlQuery<string>(templateStep2.SelectParametr,
                           new SqlParameter("CalendarVKS", modelSupport.IdCalendarVks)).FirstOrDefault();
                   }
                   if (templateStep2.TemplateParametrType.Equals("User") && modelSupport.IdUser != 0)
                   {
                       templateStep2.Parametr = Inventory.Database.SqlQuery<string>(templateStep2.SelectParametr,
                               new SqlParameter("IdUser", modelSupport.IdUser)).FirstOrDefault();
                   }
                   if (templateStep2.TemplateParametrType.Equals("SysBlock") &&  modelSupport.IdSysBlock != 0)
                   {
                       templateStep2.Parametr = Inventory.Database.SqlQuery<string>(templateStep2.SelectParametr, 
                              new SqlParameter("IdSysBlock", modelSupport.IdSysBlock)).FirstOrDefault();
                   }
                   if (templateStep2.TemplateParametrType.Equals("Monitors") && modelSupport.IdMonitor != 0)
                   {
                       templateStep2.Parametr = Inventory.Database.SqlQuery<string>(templateStep2.SelectParametr,
                           new SqlParameter("IdMonitor", modelSupport.IdMonitor)).FirstOrDefault();
                   }
                   if (templateStep2.TemplateParametrType.Equals("Printer") &&  modelSupport.IdPrinter != 0)
                   {
                       templateStep2.Parametr = Inventory.Database.SqlQuery<string>(templateStep2.SelectParametr,
                           new SqlParameter("IdPrinter", modelSupport.IdPrinter)).FirstOrDefault();
                   }
                   if (templateStep2.TemplateParametrType.Equals("Mfu") &&  modelSupport.IdMfu != 0)
                   {
                       templateStep2.Parametr = Inventory.Database.SqlQuery<string>(templateStep2.SelectParametr,
                           new SqlParameter("IdMfu", modelSupport.IdMfu)).FirstOrDefault();
                   }
                   if (templateStep2.TemplateParametrType.Equals("Scanner") &&  modelSupport.IdScanner != 0)
                   {
                       templateStep2.Parametr = Inventory.Database.SqlQuery<string>(templateStep2.SelectParametr,
                           new SqlParameter("IdScaner", modelSupport.IdScanner)).FirstOrDefault();
                   }
                   if (templateStep2.TemplateParametrType.Equals("Telephon") && modelSupport.IdTelephon != 0)
                   {
                       templateStep2.Parametr = Inventory.Database.SqlQuery<string>(templateStep2.SelectParametr,
                           new SqlParameter("IdTelephon", modelSupport.IdTelephon)).FirstOrDefault();
                   }
                   if (templateStep2.TemplateParametrType.Equals("DomainUser") && modelSupport.IdUser != 0)
                   {
                       var personnelNumberAndGroupOtdel = Inventory.Database.SqlQuery<string>(templateStep2.SelectParametr,
                           new SqlParameter("IdUser", modelSupport.IdUser)).FirstOrDefault();
                       if (templateStep2.NameParametrType.Equals("Group"))
                       {
                           templateStep2.Parametr = FindGroupAccess(personnelNumberAndGroupOtdel).Aggregate((element, next) => element + (string.IsNullOrWhiteSpace(element) ? string.Empty : ",") + next);
                       }
                       if (templateStep2.NameParametrType.Equals("GroupOtdel"))
                       {
                           templateStep2.Parametr = FindIdentytyGroup(personnelNumberAndGroupOtdel.Replace("№","№ "));
                       }

                       if (templateStep2.NameParametrType.Contains("GroupOtdelBoss"))
                       {
                           templateStep2.Parametr = FindFullPathActiveDirectory(personnelNumberAndGroupOtdel);
                       }
                   }
                   if(templateStep2.TemplateParametrType.Equals("DomainSysBlock") && modelSupport.IdSysBlock != 0)
                   {
                       var nameComputer = Inventory.Database.SqlQuery<string>(templateStep2.SelectParametr, new SqlParameter("IdSysBlock", modelSupport.IdSysBlock)).FirstOrDefault();
                       var parameters = templateStep2.Parametr.Split('/');
                       templateStep2.Parametr = IsCheckComputer(nameComputer) ? parameters[0] : parameters[1];
                   }
                   if (templateStep2.TemplateParametrType.Equals("AnalisysEpo") && modelSupport.IdAnalisysEpo != 0)
                   {
                       templateStep2.Parametr = Inventory.Database.SqlQuery<string>(templateStep2.SelectParametr, new SqlParameter("IdAnalisysEpo", modelSupport.IdAnalisysEpo)).FirstOrDefault();
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
        /// Генерация акта по модели параметров
        /// </summary>
        /// <param name="modelParameterAct">Параметры акта списания техники</param>
        /// <returns></returns>
        public Act[] GenerateParameterAct(ModelSelect modelParameterAct)
        {
            var modelParameter = Inventory.Acts.Where(act => act.IdClasification == modelParameterAct.ParametrsAct.IdClasificationAct).ToArray();
            foreach (var act in modelParameter)
            {
                if (act.ParameterAct.IsExistParameterReplace)
                {
                    act.ParameterAct.Parameter = Inventory.Database.SqlQuery<string>(act.ParameterAct.SelectParameter,
                        new SqlParameter("Id", modelParameterAct.ParametrsAct.IdModelTemplate)).FirstOrDefault();
                }
                else
                {
                    act.ParameterAct.Parameter = Inventory.Database.SqlQuery<string>(act.ParameterAct.SelectParameter).FirstOrDefault();
                }
            }
            return modelParameter;
        }

        /// <summary>
        /// Поиск групп состоящий пользователь 
        /// </summary>
        /// <param name="personnelNumber">Табельный номер</param>
        /// <returns></returns>
        private List<string> FindGroupAccess(string personnelNumber)
        {
            List<string> result = new List<string>();
            if (!string.IsNullOrWhiteSpace(personnelNumber))
            {
                using (var users = new UserPrincipal(new PrincipalContext(ContextType.Domain)))
                {
                    users.SamAccountName = personnelNumber;
                    using (var searcher = new PrincipalSearcher(users))
                    {
                        if (searcher.FindOne() is UserPrincipal user)
                        {
                            var group = user.GetGroups();
                            foreach (var gr in group)
                            {
                                result.Add(gr.Name);
                            }
                        }
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// Проверка компьютера в домене есть или нет
        /// </summary>
        /// <param name="nameComputer">Имя компьютера</param>
        /// <returns></returns>
        private bool IsCheckComputer(string nameComputer)
        {
            using (var computer = new ComputerPrincipal(new PrincipalContext(ContextType.Domain)))
            {
                computer.Name = nameComputer;
                using (var searcher = new PrincipalSearcher(computer))
                {
                    var user = searcher.FindOne() as ComputerPrincipal;
                    if (user != null)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// Поиск полного пути к группе пользователей отдела
        /// </summary>
        /// <param name="personnelNumber">Табельный номер</param>
        /// <returns></returns>
        public string FindFullPathActiveDirectory(string personnelNumber)
        {
            using (var users = new UserPrincipal(new PrincipalContext(ContextType.Domain)))
            {
                users.SamAccountName = personnelNumber;
                using (var searcher = new PrincipalSearcher(users))
                {
                    if (searcher.FindOne() is UserPrincipal user)
                    {
                        var fullPath = user.DistinguishedName.Replace("\\", "").Split(',').Where(x => x.Contains("OU=")).Reverse().Aggregate(
                            (element, next) => element + (string.IsNullOrWhiteSpace(element) ? string.Empty : "/") + next).Replace("OU=", "");
                        return fullPath;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Поиск идентификатора группы отдела
        /// </summary>
        /// <param name="nameGroup">Наименование группы</param>
        /// <returns></returns>
        private string FindIdentytyGroup(string nameGroup)
        {
            using (var domain = new GroupPrincipal(new PrincipalContext(ContextType.Domain, "regions.tax.nalog.ru", PathDomainGroup)))
            {
                domain.Description = nameGroup;
                using (var searcher = new PrincipalSearcher(domain))
                {
                    var group = searcher.FindOne() as GroupPrincipal;
                    if (group != null)
                    {
                        return group.Name;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Проверка параметров для запроса на шаге 2
        /// </summary>
        /// <param name="templateStep2">Шаблон шага 2</param>
        public void IsCheckAllParameter(TemplateSupport[] templateStep2)
        {
            foreach (var templateSupport in templateStep2)
            {
                if (templateSupport.IsImportant)
                {
                    if (string.IsNullOrWhiteSpace(templateSupport.Parametr))
                    {
                        throw new InvalidOperationException($"Фатальная ошибка на шаге 2 отсутствуют главные параметры!!! {templateSupport.HelpParameter} " );
                    }
                }
            }
        }
        /// <summary>
        /// Инициализация Sql БД
        /// </summary>
        private void InitializationSql()
        {
            Inventory?.Dispose();
            Inventory = new InventoryContext();
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
