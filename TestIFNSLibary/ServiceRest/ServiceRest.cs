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
using LibaryDocumentGenerator.GenerateDocument.GenerateWord;
using LibaryXMLAuto.ModelServiceWcfCommand.AngularModel;
using LibaryXMLAuto.ModelXmlSql.Model.FullSetting;
using LibaryXMLAutoModelServiceWcfCommand.TestIfnsService;
using SqlLibaryIfns.ExcelReport.Report;
using SqlLibaryIfns.SqlSelect.ModelSqlFullService;
using SqlLibaryIfns.SqlZapros.SqlConnections;
using SqlLibaryIfns.ZaprosSelectNotParam;
using LibaryDocumentGenerator.DonloadFile.Angular;
using LibaryXMLAuto.Reports.FullTemplateSheme;


namespace TestIFNSLibary.ServiceRest
{
    [ServiceBehavior(UseSynchronizationContext = true, IncludeExceptionDetailInFaults = true)]
    public class ServiceRest : IServiceRest
    {
        readonly Parametr _parametrService = new Parametr();
        //Функции для сайта IFNS
        public async Task<Face> SqlFaceError()
        {
            var selectfull = new SelectFull();
            return
                await Task.Factory.StartNew(
                    () =>
                        selectfull.FaceError(_parametrService.ConnectionString,
                            SqlLibaryIfns.SqlSelect.SqlFaceMergin.FaceSelectError.FaceError));
        }

        public string AddFace(FaceAdd face)
        {
            try
            {
                Sobytie sobytie = new Sobytie() {Messages = null};
                using (var con = new SqlConnection(_parametrService.ConnectionString))
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
                using (var con = new SqlConnection(_parametrService.ConnectionString))
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
                    return await taskcommand.TaskSqlProcedure(_parametrService.ConectWork, seting);
                case "Test":
                    return await taskcommand.TaskSqlProcedure(_parametrService.ConectTest, seting);
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
            return await donloads.SelectDonloadsFile(_parametrService.Report, filename, _parametrService.ConectWork);
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
                                selectfull.BdkSqlSelect(_parametrService.ConectWork,
                                ((ServiceWcf)
                                    sqlconnect.SelectFullParametrSqlReader(_parametrService.ConectWork,
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
                    return await taskcommand.TaskSqlProcedureBdk(_parametrService.ConectWork, setting);
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
                    return await taskcommand.TaskSqlProcedureSoprovod(_parametrService.ConectWork, setting);
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
                    return await taskcommand.TaskSqlProcedureKrsb(_parametrService.ConectWork, setting);
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
                    report.GenerateOutBdk(_parametrService.ConectWork, _parametrService.ConnectionString,
                        _parametrService.ReportMassTemplate, setting);
                });
                return "Документы для печати запущены и сохраняются в папку " + _parametrService.ReportMassTemplate;
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
            return await Task.Factory.StartNew(() => selectfull.ServiceCommand(_parametrService.ConectWork, setting));
        }

        /// <summary>
        /// Обработка всех комманд с сервера и возврат данных надо тестить
        /// </summary>
        /// <param name="command">WCf модель взаимодействия с сайтом</param>
        /// <returns></returns>
        public async Task<string> ModelSqlSelect(AngularModel command)
        {
            var connect = command.Db == "Work" ? _parametrService.ConectWork : _parametrService.ConectTest;
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
            return await Task.Factory.StartNew(() => efangular.CreateKrsb(_parametrService.ConectWork, setting));
        }

        public async Task<Stream> StoreProcedureKam5(FullSetting setting)
        {
            var taskcommand = new TaskResult();
            DonloadsFile donloads = new DonloadsFile();
            switch (setting.Db)
            {
                case "Work":
                    return await donloads.SelectDonloadsFile(_parametrService.Report, "Камеральный №5.xlsx", null, await taskcommand.TaskSqlProcedureKam5(_parametrService.ConectWork, setting));
                default:
                    return null;
            }
        }
        
        /// <summary >
        /// Авторизация на сайте ВАЖНО ПОКА НЕ ПОЛУЧИЛОСЬ ПЕРЕДАТЬ ДАТУ С СЕРВЕРА ОШИБКА ФОРМАТА
        /// </summary>
        /// <param name="setting">Настройки там пользователь Login Password</param>
        /// <returns></returns>
        public async Task<ModelUser> AuthService(FullSetting setting)
        {
            AuthUser.AuthUser authuser = new AuthUser.AuthUser();
            return await Task.Factory.StartNew(() => authuser.AuthUserService(setting));
        }

        }
    }