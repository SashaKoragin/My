using System;
using System.Data;
using System.Data.SqlClient;
using System.ServiceModel;
using System.Threading.Tasks;
using LibaryXMLAutoModelXmlSql.Model.FaceError;
using TestIFNSLibary.PathJurnalAndUse;
using TestIFNSLibary.PostRequest.Face;
using SqlLibaryIfns.SqlEntytiCommand.TaskUse;
using SqlLibaryIfns.SqlZapros.SobytieSql;
using System.IO;
using System.Threading;
using EfDatabase.Inventory.BaseLogic.Select;
using LibaryDocumentGenerator.Documents.DocumentMigration;
using LibaryDocumentGenerator.Documents.Template;
using LibaryDocumentGenerator.GenerateDocument.GenerateWord;
using LibaryXMLAuto.ModelServiceWcfCommand.AngularModel;
using LibaryXMLAuto.ModelXmlSql.Model.FullSetting;
using LibaryXMLAutoModelServiceWcfCommand.TestIfnsService;
using SqlLibaryIfns.ExcelReport.Report;
using SqlLibaryIfns.SqlSelect.ModelSqlFullService;
using SqlLibaryIfns.SqlZapros.SqlConnections;
using SqlLibaryIfns.ZaprosSelectNotParam;
using LibaryDocumentGenerator.DonloadFile.Angular;
using LibaryXMLAuto.ModelServiceWcfCommand.ModelPathReport;
using LibaryXMLAuto.Reports.FullTemplateSheme;
using LibaryXMLAutoModelXmlAuto.MigrationReport;
using LibaryXMLAutoModelXmlAuto.OtdelRuleUsers;


namespace TestIFNSLibary.ServiceRest
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    public class ServiceRest : IServiceRest
    {
        /// <summary>
        /// Параметры конфигурации
        /// </summary>
        readonly Parameter _parameterService = new Parameter();
        /// <summary>
        /// Указатель выполнение задачи загрузки шаблонов по ролям
        /// </summary>
        public static bool IsLoadCompletedDataBaseTemplate = true;
        //Функции для сайта IFNS
        public async Task<Face> SqlFaceError()
        {
            var selectFull = new SelectFull();
            return
                await Task.Factory.StartNew(
                    () =>
                        selectFull.FaceError(_parameterService.ConnectionString,
                            SqlLibaryIfns.SqlSelect.SqlFaceMergin.FaceSelectError.FaceError));
        }

        public string AddFace(FaceAdd face)
        {
            try
            {
                Sobytie sobytie = new Sobytie() {Messages = null};
                using (var con = new SqlConnection(_parameterService.ConnectionString))
                {
                    con.InfoMessage += sobytie.Con_InfoMessage;
                    using (var cmd = new SqlCommand(SqlLibaryIfns.SqlSelect.SqlFaceMergin.FaceSelectError.AddFace, con))
                    {
                        cmd.Parameters.Add("@Old", SqlDbType.Int).Value = Convert.ToInt32(face.N1Old);
                        cmd.Parameters.Add("@New", SqlDbType.Int).Value = Convert.ToInt32(face.N1New);
                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();
                        return sobytie.Messages;
                    }
                }
            }
            catch (SqlException e)
            {
                return e.Message;
            }
        }

        public string FaceDel(int id)
        {
            try
            {
                Sobytie sobytie = new Sobytie {Messages = null};
                using (var con = new SqlConnection(_parameterService.ConnectionString))
                {
                    con.InfoMessage += sobytie.Con_InfoMessage;
                    using (
                        var cmd = new SqlCommand(SqlLibaryIfns.SqlSelect.SqlFaceMergin.FaceSelectError.FaceDelete, con))
                    {
                        cmd.Parameters.Add("@idint", SqlDbType.Int).Value = Convert.ToInt32(id);
                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();
                        return sobytie.Messages;
                    }
                }
            }
            catch (SqlException e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// Выполнение процедуры на сервере Sql в зависимости от настроек Test или Work
        /// </summary>
        /// <param name="seting">Настройки</param>
        /// <returns>Возврат сообщения с сервера SQL</returns>
        public async Task<string> StoreProcedure(FullSetting seting)
        {
            var taskcommand = new TaskResult();
            switch (seting.Db)
            {
                case "Work":
                    return await taskcommand.TaskSqlProcedure(_parameterService.ConectWork, seting);
                case "Test":
                    return await taskcommand.TaskSqlProcedure(_parameterService.ConectTest, seting);
                default:
                    return null;
            }
        }

        /// <summary>
        /// Загрузка файла с сервера по средством C#
        /// </summary>
        /// <param name="filename">Имя файла</param>
        /// <returns></returns>
        public async Task<Stream> DonloadFile(string filename)
        {
            DonloadsFile donloads = new DonloadsFile();
            return await donloads.SelectDonloadsFile(_parameterService.Report, filename, _parameterService.ConectWork);
        }

        /// <summary>
        /// Подгрузка БДК Статистики
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public async Task<string> LoaderBdk(FullSetting setting)
        {
            var selectfull = new SelectFull();
            switch (setting.Db)
            {
                case "Work":
                    var sqlconnect = new SqlConnectionType();
                    return
                        await Task.Factory.StartNew(
                            () =>
                                selectfull.BdkSqlSelect(_parameterService.ConectWork,
                                ((ServiceWcf)
                                    sqlconnect.SelectFullParametrSqlReader(_parameterService.ConectWork,
                                        ModelSqlFullService.ProcedureSelectParametr, typeof(ServiceWcf),
                                        ModelSqlFullService.ParamCommand("7"))).ServiceWcfCommand.Command));
                default:
                    return null;
            }
        }

        /// <summary>
        /// Выполнения процедур по БДК блоку
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public async Task<string> StoreProcedureBdk(FullSetting setting)
        {
            var taskcommand = new TaskResult();
            switch (setting.Db)
            {
                case "Work":
                    return await taskcommand.TaskSqlProcedureBdk(_parameterService.ConectWork, setting);
                default:
                    return null;
            }
        }

        /// <summary>
        /// Выполнения процедур Предпроверки
        /// </summary>
        /// <param name="setting">Настройки</param>
        /// <returns></returns>
        public async Task<string> StoreProcedureSoprovod(FullSetting setting)
        {
            var taskcommand = new TaskResult();
            switch (setting.Db)
            {
                case "Work":
                    return await taskcommand.TaskSqlProcedureSoprovod(_parameterService.ConectWork, setting);
                default:
                    return null;
            }
        }

        /// <summary>
        /// Выполнения процедур по КРСБ
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public async Task<string> StoreProcedureKrsb(FullSetting setting)
        {
            var taskcommand = new TaskResult();
            switch (setting.Db)
            {
                case "Work":
                    return await taskcommand.TaskSqlProcedureKrsb(_parameterService.ConectWork, setting);
                default:
                    return null;
            }
        }

        /// <summary>
        /// Модель для создания писем
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public string StartNewOpenXmlTemplate(FullSetting setting)
        {
            try
            {
                Task.Factory.StartNew(() =>
                {
                    GenerateDocument report = new GenerateDocument();
                    report.GenerateOutBdk(_parameterService.ConectWork, _parameterService.ConnectionString,
                        _parameterService.ReportMassTemplate, setting);
                });
                return "Документы для печати запущены и сохраняются в папку " + _parameterService.ReportMassTemplate;
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
                return e.Message;
            }
        }

        /// <summary>
        /// Получение выборки для генерации командя на сайте
        /// </summary>
        /// <param name="setting">Параметры</param>
        /// <returns></returns>
        public async Task<string> ModelServiceCommand(FullSetting setting)
        {
            var selectfull = new SelectFull();
            return await Task.Factory.StartNew(() => selectfull.ServiceCommand(_parameterService.ConectWork, setting));
        }

        /// <summary>
        /// Обработка всех комманд с сервера и возврат данных надо тестить
        /// </summary>
        /// <param name="command">WCf модель взаимодействия с сайтом</param>
        /// <returns></returns>
        public async Task<string> ModelSqlSelect(AngularModel command)
        {
            var connect = command.Db == "Work" ? _parameterService.ConectWork : _parameterService.ConectTest;
            var selectfull = new SelectFull();
            return await Task.Factory.StartNew(() => selectfull.SqlSelect(connect, command));
        }

        /// <summary>
        /// Метод передачи файла на сайт
        /// </summary>
        /// <param name="angular">Параметры файла Angular</param>
        /// <returns></returns>
        public async Task<Stream> AngularDonloadFile(AngularModelFileDonload angular)
        {
            var efangular = new AngularRestEf();
            return await Task.Factory.StartNew(() => efangular.DonloadFile(angular));
        }

        /// <summary>
        /// Метод добавления шаблона в БД
        /// </summary>
        /// <param name="angular">Модель шаблона</param>
        /// <returns></returns>
        public async Task<string> AngularAddTemplate(AngularTemplate angular)
        {
            var efangular = new AngularRestEf();
            return await Task.Factory.StartNew(() => efangular.TemplateSave(angular));
        }

        public async Task<string> AngularCreateKrsb(FullSetting setting)
        {
            var efangular = new AngularRestEf();
            return await Task.Factory.StartNew(() => efangular.CreateKrsb(_parameterService.ConectWork, setting));
        }

        public async Task<Stream> StoreProcedureKam5(FullSetting setting)
        {
            var taskcommand = new TaskResult();
            DonloadsFile donloads = new DonloadsFile();
            switch (setting.Db)
            {
                case "Work":
                    return await donloads.SelectDonloadsFile(_parameterService.Report, "Камеральный №5.xlsx", null, await taskcommand.TaskSqlProcedureKam5(_parameterService.ConectWork, setting));
                default:
                    return null;
            }
        }
        /// <summary>
        /// G
        /// </summary>
        /// <returns></returns>
        public async Task<string> ServerList(FullSetting setting)
        {
            var connect = setting.Db == "Work" ? _parameterService.ConectWork : _parameterService.ConectTest;
            var selectfull = new SelectFull();
            return await Task.Factory.StartNew(() => selectfull.SqlSelect(connect, setting));
        }
        /// <summary>
        /// Авторизация на сервере
        /// </summary>
        /// <param name="setting">Настройки</param>
        /// <param name="callback"></param>
        /// <param name="asyncState"></param>
        /// <returns></returns>
        public IAsyncResult BeginSampleMethod(FullSetting setting, AsyncCallback callback, object asyncState)
        {
            AuthUser.AuthUser authuser = new AuthUser.AuthUser(); 
            return new CompletedAsyncResult<ModelUser>(authuser.AuthUserService(setting));
        }

        public ModelUser EndSampleMethod(IAsyncResult r)
        {
            CompletedAsyncResult<ModelUser> result = r as CompletedAsyncResult<ModelUser>;
            return result.Data;
        }
        /// <summary>
        /// Сервис подготовки документов ошибок о миграции
        /// </summary>
        /// <param name="json">Json отчет</param>
        /// <returns></returns>
        public async Task<ModelPathReport> MigrationReports(MigrationParse json)
        {
            var report = new ModelPathReport();
            try
            {
             return  await Task.Factory.StartNew(() =>
                {
                    var docmigration = new DocumentMigration();
                    docmigration.MigrationDocument(_parameterService.ConectWork, _parameterService.ReportMassTemplate, json, _parameterService.SendServiceLotus);
                    report.Note = "Документы для печати запущены и сохраняются в папку ";
                    report.Url = _parameterService.ReportMassTemplate;
                    return report;
                });
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
                report.Note = e.Message;
                return report;
            }
        }

        /// <summary>
        /// Формирование заявок пляшем от АИС 3
        /// </summary>
        /// <param name="userRule">Данные АИС 3 для создания заявки</param>
        /// <returns></returns>
        public async Task<ModelPathReport> GenerateTemplateRule(UserRules userRule)
        {
            var report = new ModelPathReport();
            try
            {
                return  await Task.Factory.StartNew(() =>
                {
                    var sql = new SelectSql();
                    var templateWord = new TemplateUserRule();
                    var templateRule = new RuleTemplate() { SenderUsers = new SenderUsers() };
                    var modelSelect = sql.SendersUsers(ref templateRule);
                    sql.UserRuleModel(ref templateRule, userRule, modelSelect);
                    templateWord.CreateDocum(_parameterService.ReportMassTemplate, templateRule, null);
                    report.Note = "Заявки запущены и сохраняются в папку";
                    report.Url = _parameterService.ReportMassTemplate;
                    return report;
                });
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
                report.Note = e.Message;
                return report;
            }
        }
        /// <summary>
        /// Загрузка шаблонов в БД
        /// </summary>
        /// <param name="infoTemplate">Шаблоны</param>
        /// <returns></returns>
        public ModelPathReport LoadInfoTemplateToDataBase(InfoRuleTemplate infoTemplate)
        {
            var report = new ModelPathReport();
            if (IsLoadCompletedDataBaseTemplate)
            {
               var task = Task.Run(() =>
                 {
                    try
                    {
                        var sql = new SelectSql(); 
                        sql.LoadTemplateDataBase(infoTemplate);
                        sql.Dispose();
                    }
                    catch (Exception e)
                    {
                        Loggers.Log4NetLogger.Error(e);
                    }
                 });
               IsLoadCompletedDataBaseTemplate = task.ConfigureAwait(true).GetAwaiter().IsCompleted;
               task.ConfigureAwait(true).GetAwaiter().OnCompleted((() => 
                    IsLoadCompletedDataBaseTemplate = true
                    ));
                report.Note = "Процесс по загрузке шаблонов запущен!";
            }
            else
            {
                report.Note = "Задача уже запущенна ожидайте окончание процесса!";
            }
            return report;
        }
    }


    }
class CompletedAsyncResult<T> : IAsyncResult
{
    T data;

    public CompletedAsyncResult(T data)
    { this.data = data; }

    public T Data
    { get { return data; } }

    #region IAsyncResult Members
    public object AsyncState
    { get { return (object)data; } }

    public WaitHandle AsyncWaitHandle
    { get { throw new Exception("The method or operation is not implemented."); } }

    public bool CompletedSynchronously
    { get { return true; } }

    public bool IsCompleted
    { get { return true; } }
    #endregion
}