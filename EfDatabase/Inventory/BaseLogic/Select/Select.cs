using EfDatabase.Inventory.Base;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using EfDatabase.FilterModel;
using EfDatabase.SettingModelInventory;
using EfDatabase.XsdInventoryRuleAndUsers;
using EfDatabaseParametrsModel;
using EfDatabaseXsdQrCodeModel;
using LibaryXMLAuto.ReadOrWrite.SerializationJson;
using FullModel = EfDatabase.Inventory.Base.FullModel;
using SysBlock = EfDatabase.Inventory.Base.SysBlock;


namespace EfDatabase.Inventory.BaseLogic.Select
{
   public class Select : IDisposable
    {
        public InventoryContext Inventory { get; set; }

        public Select()
        {
            Inventory?.Dispose();
            Inventory = new InventoryContext();
        }
        /// <summary>
        /// Глобальные настройки организации
        /// </summary>
        /// <returns></returns>
        public Organization SettingOrganization()
        {
           return  Inventory.Organizations.FirstOrDefault();
        }
        /// <summary>
        /// Выгрузка падежей отделов для отчетов
        /// </summary>
        /// <returns></returns>
        public string SettingDepartmentCase()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.Database.SqlQuery<SettingDepartmentCase>("Select " +
                                                                                                 "Otdel.IdOtdel, " +
                                                                                                 "Otdel.NameOtdel, " +
                                                                                                 "OtdelPadeg.InameOtdel, " +
                                                                                                 "OtdelPadeg.RnameOtdel, " +
                                                                                                 "OtdelPadeg.DnameOtdel, " +
                                                                                                 "OtdelPadeg.VnameOtdel, " +
                                                                                                 "OtdelPadeg.PnameOtdel, " +
                                                                                                 "OtdelPadeg.TnameOtdel From Otdel "+
                                                                                                 "Left Join OtdelPadeg on Otdel.IdOtdel = OtdelPadeg.IdOtdel").ToArray());
        }
        /// <summary>
        /// Получение всех отчетов сравнения ЭПО
        /// </summary>
        /// <returns></returns>
        public string GetModelReportAnalysisEpo()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(
                    Inventory.AnalysisEpoAndInventarkas
                        .Select(sel =>
                            new
                            {
                                Id = sel.Id,
                                IsPrint = sel.IsPrint,
                                NameInfoReport = sel.NameInfoReport
                            }
                        ).ToArray());
        }
        /// <summary>
        /// Регламенты отделов для заявок
        /// </summary>
        /// <returns></returns>
        public string SettingDepartmentRegulations()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.Database.SqlQuery<RegulationsDepartment>("Select Otdel.IdOtdel, Otdel.NameOtdel, DepartmentRegulations.Regulations From Otdel " +
                                                                                                     "Left Join DepartmentRegulations on Otdel.IdOtdel = DepartmentRegulations.IdOtdel").ToArray());
        }

        /// <summary>
        /// Получение\выгрузка всех праздничных дней
        /// </summary>
        /// <returns></returns>
        public string GetHolidays()
        { 
            SerializeJson json = new SerializeJson();
            return json.JsonLibrary(Inventory.Rb_Holidays);
        }

        /// <summary>
        /// Запрос на список отделов
        /// </summary>
        /// <returns></returns>
        public string DepartmentAll()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.Otdels);
        }

        /// <summary>
        /// Загрузка всех ролей пользователя для выбора!
        /// </summary>
        /// <returns></returns>
        public string RuleAll()
        {
            SerializeJson json = new SerializeJson();
           
            return json.JsonLibaryIgnoreDate(Inventory.Rules);
        }
        /// <summary>
        /// Запрос всех пользователей
        /// </summary>
        /// <param name="filterActual">Фильтр</param>
        /// <returns></returns>
        public string UsersAll(AllUsersFilters filterActual)
        {
            SerializeJson json = new SerializeJson();
            if (filterActual.FilterActual.IsFilter)
            {
                var usersIsStatus = Inventory.Users.Where(user => user.StatusUser != null).Where(u => u.StatusUser.IdStatusUser != 4).ToArray();
                DeleteOwnerUserPhone(ref usersIsStatus);
                return json.JsonLibaryIgnoreDate(usersIsStatus);
            }
            var users = Inventory.Users.ToArray();
            DeleteOwnerUserPhone(ref users);
            return json.JsonLibaryIgnoreDate(users);
        }
        /// <summary>
        /// Удаление владельцев телефонов
        /// </summary>
        /// <param name="users"></param>
        private void DeleteOwnerUserPhone(ref User[] users)
        {
            foreach (var user in users)
            {
                if (user.Telephon != null)
                {
                    user.Telephon.User = null;
                }
            }
        }



        /// <summary>
        /// Запрос всех должностей
        /// </summary>
        /// <returns></returns>
        public string PositionUsers()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.Positions);
        }

        /// <summary>
        /// Запрос всех принтеров с БД
        /// </summary>
        /// <returns></returns>
        public string Printers()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.Printers.Where(x=> !x.WriteOffSign));
        }
        /// <summary>
        /// Выгрузка всех Коммутаторов
        /// </summary>
        /// <returns></returns>
        public string Swithes()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.Swithes.Where(x => !x.WriteOffSign));
        }

        public string ModelSwitch()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.ModelSwithes);
        }

        /// <summary>
        /// Запрос всех сканеров с БД
        /// </summary>
        /// <returns></returns>
        public string Scaner()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.ScanerAndCamers.Where(x => !x.WriteOffSign));
        }

        /// <summary>
        /// Запрос всех МФУ с БД
        /// </summary>
        /// <returns></returns>
        public string Mfu()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.Mfus.Where(x => !x.WriteOffSign));
        }

        /// <summary>
        /// Запрос всех Системных блоков с БД
        /// </summary>
        /// <returns></returns>
        public string SysBloks()
        {
            SerializeJson json = new SerializeJson();
            var sysBlock = Inventory.SysBlocks.Where(x => !x.WriteOffSign).ToArray();
            DeleteChief(ref sysBlock);
            return json.JsonLibaryIgnoreDate(sysBlock);
        }

        private void DeleteChief(ref SysBlock[] sys)
        {
            foreach (var sy in sys)
            {
                if (sy.User != null)
                {
                    sy.User.Otdel.User = null;
                    if (sy.User.Telephon != null)
                    {
                        sy.User.Telephon.User = null;
                    }
                }
            }
        }

        /// <summary>
        /// Запрос всех Мониторов с БД
        /// </summary>
        /// <returns></returns>
        public string Monitors()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.Monitors.Where(x => !x.WriteOffSign));
        }
        /// <summary>
        /// Разное оборудование
        /// </summary>
        /// <returns></returns>
        public string OtherAll()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.OtherAlls.Where(x => !x.WriteOffSign));
        }
        /// <summary>
        /// Все модели разного оборудования
        /// </summary>
        /// <returns></returns>
        public string AllModelOther()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.ModelOthers);
        }
        /// <summary>
        /// Все типы разного оборудования
        /// </summary>
        /// <returns></returns>
        public string AllTypeOther()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.TypeOthers);
        }
        /// <summary>
        /// Все производители разного оборудования
        /// </summary>
        /// <returns></returns>
        public string AllProizvoditelOther()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.ProizvoditelOthers);
        }
        /// <summary>
        /// Запрос всех CopySave с БД
        /// </summary>
        /// <returns></returns>
        public string CopySave()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.CopySaves);
        }

        /// <summary>
        /// Запрос всех производителей с БД
        /// </summary>
        /// <returns></returns>
        public string Proizvoditel()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.FullProizvoditels);
        }

        /// <summary>
        /// Запрос всех кабинетов с БД
        /// </summary>
        /// <returns></returns>
        public string Kabinet()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.Kabinets);
        }

        /// <summary>
        /// Запрос всех моделей с БД
        /// </summary>
        /// <returns></returns>
        public string Model()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.FullModels);
        }
        /// <summary>
        /// Все категории телефона для справочника
        /// </summary>
        /// <returns></returns>
        public string CategoryPhoneHeader()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.CategoryPhoneHeaders);
        }

        /// <summary>
        /// Все модели принтеров
        /// </summary>
        /// <returns></returns>
        public FullModel[] AllFullModel()
        {
           return Inventory.FullModels.Where(x=>x.IdClasification == 1 || x.IdClasification == 3).ToArray();
        }
        /// <summary>
        /// Получение всех серийных номеров прикладных аппаратов в БД
        /// </summary>
        /// <returns></returns>
        public List<string> AllSerNumber()
        {
            return Inventory.AllTechnics.Where(x => x.Item == "Принтер" || x.Item == "МФУ").Select(sel => sel.SerNum.Trim()).ToList();
        }
        /// <summary>
        /// Получения по табельному номеру Id пользователя
        /// </summary>
        /// <param name="personnelNumber">Табельный номер</param>
        /// <returns></returns>
        public int SelectIdUser(string personnelNumber)
        {
            return Inventory.Users.First(user => user.TabelNumber == personnelNumber).IdUser;
        }
        /// <summary>
        /// Получение найденного экземпляра оборудования в PrintServer для создания заявки
        /// </summary>
        /// <param name="serNumber">Ser\number</param>
        /// <returns></returns>
        public AllTechnic TypeModelAndIdSelect(string serNumber)
        {
            return Inventory.AllTechnics.FirstOrDefault(x => x.SerNum == serNumber && x.AutoSupport == 1);
        }

        /// <summary>
        /// Запрос всех статусов с БД
        /// </summary>
        /// <returns></returns>
        public string Status()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.Statusings);
        }

        /// <summary>
        /// Запрос всех наименований системных блоков
        /// </summary>
        /// <returns></returns>
        public string NameSysBlock()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.NameSysBlocks);
        }

        /// <summary>
        /// Запрос всех наименований монитора
        /// </summary>
        /// <returns></returns>
        public string AllNameMonitor()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.NameMonitors);
        }
        /// <summary>
        /// Выгрузка телефонов
        /// </summary>
        /// <returns></returns>
        public string Telephon()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.Telephons.Where(x => !x.WriteOffSign));
        }
        /// <summary>
        /// Выгрузка типов серверов
        /// </summary>
        /// <returns></returns>
        public string TypeServer()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.TypeServers);
        }
        /// <summary>
        /// Выгрузка бесперебойников
        /// </summary>
        /// <returns></returns>
        public string BlockPower()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.BlockPowers.Where(x => !x.WriteOffSign));
        }
        /// <summary>
        /// Все серверное оборудование
        /// </summary>
        /// <returns></returns>
        public string ServerEquipment()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.ServerEquipments.Where(x => !x.WriteOffSign));
        }
        /// <summary>
        /// Все модели серверного оборудования
        /// </summary>
        /// <returns></returns>
        public string ModelSeverEquipment()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.ModelSeverEquipments);
        }
        /// <summary>
        /// Получение ресурсов для заявки
        /// </summary>
        /// <returns></returns>
        public string GetResourceIt()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.ResourceIts);
        }
        /// <summary>
        /// Получение задач для заявки АИС 3
        /// </summary>
        /// <returns></returns>
        public string GetTaskAis3()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.TaskAis3);
        }
        /// <summary>
        /// Получение журнала заявок на различные ресурсы
        /// </summary>
        /// <returns></returns>
        public string GetJournalAis3()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.JournalAis3);
        }
        /// <summary>
        /// Все производители серверного оборудования
        /// </summary>
        /// <returns></returns>
        public string ManufacturerSeverEquipment()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.ManufacturerSeverEquipments);
        }

        /// <summary>
        /// Выгрузка поставщиков
        /// </summary>
        /// <returns></returns>
        public string Supply()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibrary(Inventory.Supplies);
        }
        /// <summary>
        /// Выгрузка моделей бесперебойных блоков
        /// </summary>
        /// <returns></returns>
        public string ModelBlockPower()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.ModelBlockPowers);
        }

        /// <summary>
        /// Выгрузка производителей бесперебойных блоков
        /// </summary>
        /// <returns></returns>
        public string ProizvoditelBlockPower()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.ProizvoditelBlockPowers);
        }
        /// <summary>
        /// Статистика актулизации пользователей
        /// </summary>
        /// <returns></returns>
        public string ActualsUsersKladr()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.UsersIsActualsStats);
        }
        /// <summary>
        /// Получение всей классификации
        /// </summary>
        /// <returns></returns>
        public string AllClasification()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.Classifications);
        }
        /// <summary>
        /// Все идентификаторы пользователей
        /// </summary>
        /// <returns></returns>
        public string AllMailIdentifier()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.MailIdentifiers);
        }
        /// <summary>
        /// Все группы пользователей
        /// </summary>
        /// <returns></returns>
        public string AllMailGroup()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.MailGroups);
        }
        /// <summary>
        /// Запрос на все шаблоны СТО для заявок
        /// </summary>
        /// <returns></returns>
        public string AllTemplate()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.FullTemplateSupports);
        }
        /// <summary>
        /// Все токены ключи в БД
        /// </summary>
        /// <returns></returns>
        public string AllToken()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.Tokens.Where(x => !x.WriteOffSign));
        }
        /// <summary>
        /// Запрос на технику претендующую на QR code
        /// </summary>
        /// <param name="serialNumber">Серийный номер</param>
        /// <param name="isAll"></param>
        /// <returns></returns>
        public List<AllTechnic> SelectTechnical(string serialNumber, bool isAll = false)
        {
            return isAll ? Inventory.AllTechnics.Where(x => !x.WriteOffSign).ToList() : Inventory.AllTechnics.Where(x => x.SerNum == serialNumber).ToList();
        }
        /// <summary>
        /// Отбираем все или конкретный
        /// </summary>
        /// <param name="numberOffice">Номер кабинета</param>
        /// <param name="isAll">Если true запрашиваем все</param>
        public QrCodeOffice SelectOffice(string numberOffice, bool isAll = false)
        {
            var modelOfficeList = new QrCodeOffice
            {
                Kabinet = isAll
                    ? Inventory.Database.SqlQuery<EfDatabaseXsdQrCodeModel.Kabinet>($"Select * From Kabinet").ToArray()
                    : Inventory.Database
                        .SqlQuery<EfDatabaseXsdQrCodeModel.Kabinet>(
                            $"Select * From Kabinet Where NumberKabinet ='{numberOffice}'").ToArray()
            };
            return modelOfficeList;
        }
        /// <summary>
        /// Получение данных для личного кабинета пользователя инвенторизации
        /// </summary>
        /// <param name="idUser">Ун пользователя</param>
        /// <returns></returns>
        public string AllTechicsLkInventory(int idUser)
        {
            SerializeJson json = new SerializeJson();
            SelectSql sql = new SelectSql();
            ModelSelect model = new ModelSelect { LogicaSelect = sql.SqlSelectModel(33) };
            var idDepartment = Inventory.Database.SqlQuery<int>(model.LogicaSelect.SelectUser,
                new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[0], idUser)).FirstOrDefault();
            sql.Dispose();
            if (idDepartment != 0)
            {
                return json.JsonLibaryIgnoreDate(Inventory.AllTechnics.Where(x => x.IdOtdel == idDepartment)); 
            }
            return json.JsonLibaryIgnoreDate(Inventory.AllTechnics.Where(x => x.IdUser == idUser));
        }
        /// <summary>
        /// Загрузка всего департамента для ЛК 
        /// </summary>
        /// <param name="idUser">УН пользователя</param>
        /// <returns></returns>
        public string AllUsersDepartmentLk(int idUser)
        {
            SerializeJson json = new SerializeJson();
            var idDepartment = Inventory.Users.FirstOrDefault(user => user.IdUser == idUser);
            if (idDepartment != null && (idDepartment.IdOtdel != null || idDepartment.IdOtdel != 0))
            {
               return json.JsonLibaryIgnoreDate(Inventory.Users.Where(user => user.IdOtdel == idDepartment.IdOtdel).Where(user => user.StatusUser != null).Where(u => u.StatusUser.IdStatusUser != 4).ToList());
            }
            return null;
        }

        /// <summary>
        /// Выгрузка ролей пользователя
        /// </summary>
        /// <param name="idUser">УН пользователя</param>
        /// <returns></returns>
        public RuleUsers[] AllRuleUser(int idUser)
        {
            SelectSql sql = new SelectSql();
            ModelSelect model = new ModelSelect { LogicaSelect = sql.SqlSelectModel(34) };
            sql.Dispose();
            var ruleAndUser = Inventory.Database.SqlQuery<RuleUsers>(model.LogicaSelect.SelectUser,
                new SqlParameter(model.LogicaSelect.SelectedParametr.Split(',')[0], idUser)).ToArray();
            return ruleAndUser;
        }
        /// <summary>
        /// Запрос на процесс
        /// </summary>
        /// <param name="idTask">Ун</param>
        /// <returns></returns>
        public bool IsBeginTask(int idTask)
        {
            var isComplete = Inventory.EventProcesses.FirstOrDefault(x => x.Id == idTask)?.IsComplete;
            return isComplete != null && (bool)isComplete;
        }
        /// <summary>
        /// Вытаскивание процесса а по ID
        /// </summary>
        /// <param name="isProcess">Номер процесса</param>
        /// <returns></returns>
        public EventProcess SelectProcess(int isProcess)
        {
            return Inventory.EventProcesses.FirstOrDefault(x => x.Id == isProcess);
        }
        /// <summary>
        /// Вытаскиваем параметры задачи параметры через ;
        /// </summary>
        /// <param name="idEvent">Ун параметров</param>
        /// <returns></returns>
        public EventProcess SelectEvent(int idEvent)
        {
            return Inventory.EventProcesses.FirstOrDefault(x => x.Id == idEvent);
        }
        /// <summary>
        /// Все Ip Серверов в БД для отчета пинга
        /// </summary>
        /// <returns></returns>
        public List<AllIpServerSelect> AllIpServerSelectDataBase()
        {
            return Inventory.AllIpServerSelects.ToList();
        }

        /// <summary>
        /// Запрос всех параметров для процесса
        /// </summary>
        /// <returns></returns>
        public string AllEventProcessParameter()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.EventProcesses, "dd.MM.yyyy HH:mm");
        }
        /// <summary>
        /// Вытаскиваем все категории
        /// </summary>
        /// <returns></returns>
        public string AllFullСategories()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.Database.SqlQuery<FullСategory>("Select * From FullСategory"));
        }
        /// <summary>
        /// Вытаскиваем все типы
        /// </summary>
        /// <returns></returns>
        public string AllEquipmentType()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.Database.SqlQuery<EquipmentType>("Select * From EquipmentType"));
        }
        /// <summary>
        /// Вытаскиваем все производители
        /// </summary>
        /// <returns></returns>
        public string AllProducer()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.Database.SqlQuery<Producer>("Select * From Producer"));
        }
        /// <summary>
        /// Вытаскиваем все модели
        /// </summary>
        /// <returns></returns>
        public string AllEquipmentModel()
        {
            SerializeJson json = new SerializeJson();
            return json.JsonLibaryIgnoreDate(Inventory.Database.SqlQuery<EquipmentModel>("Select * From EquipmentModel"));
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

