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
using LibaryXMLAuto.ModelXmlSql.Model.FullSetting;
using LibaryXMLAutoModelServiceWcfCommand.TestIfnsService;
using SqlLibaryIfns.ExcelReport.Report;
using SqlLibaryIfns.SqlModelReport.SqlReshenue;
using SqlLibaryIfns.SqlSelect.ModelSqlFullService;
using SqlLibaryIfns.SqlZapros.SqlConnections;
using SqlLibaryIfns.ZaprosSelectNotParam;


namespace TestIFNSLibary.ServiceRest
{
    [ServiceBehavior(UseSynchronizationContext = true, IncludeExceptionDetailInFaults = true)]
    public class ServiceRest : IServiceRest
    {
        //Функции для сайта IFNS
        public async Task<Face> SqlFaceError()
        {
            var selectfull = new SelectFull();
            return await Task.Factory.StartNew<Face>(() => selectfull.FaceError(Parametr.ConnectionString, SqlLibaryIfns.SqlSelect.SqlFaceMergin.FaceSelectError.FaceError));
        }

        public string AddFace(FaceAdd face)
        {
            try
            {
                Sobytie sobytie = new Sobytie { Messages = null };
                using (var con = new SqlConnection(Parametr.ConnectionString))
                {
                    con.InfoMessage +=sobytie.Con_InfoMessage;
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
                Sobytie sobytie = new Sobytie { Messages = null };
                using (var con = new SqlConnection(Parametr.ConnectionString))
                {
                    con.InfoMessage += sobytie.Con_InfoMessage;
                    using (var cmd = new SqlCommand(SqlLibaryIfns.SqlSelect.SqlFaceMergin.FaceSelectError.FaceDelete, con))
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
                    return await taskcommand.TaskSqlProcedure(Parametr.ConectWork, seting);
                case "Test":
                    return await taskcommand.TaskSqlProcedure(Parametr.ConectTest, seting); ;
                default:
                    return null;
            }
        }
        /// <summary>
        /// Выполнение Sql запроса в зависимости от настроек Test или Work
        /// </summary>
        /// <param name="seting">Настройки</param>
        /// <returns>Возврат модели JSON в виде строки</returns>
        public async Task<string> LoaderReshenie(FullSetting seting)
        {
            SqlReshen resselect = new SqlReshen();
            switch (seting.Db)
            {
                case "Work":
                    return
                        await Task.Factory.StartNew<string>(() =>resselect.SysNumReshenie(Parametr.ConectWork, seting));
                case "Test":
                      return await Task.Factory.StartNew<string>(() => resselect.SysNumReshenie(Parametr.ConectTest, seting));
                default:
                    return null;
            }
        }
        /// <summary>
        /// Загрузка файла с сервера
        /// </summary>
        /// <param name="filename">Имя файла</param>
        /// <returns></returns>
        public async Task<Stream> DonloadFile(string filename)
        {
          DonloadsFile donloads = new DonloadsFile();
          return await donloads.SelectDonloadsFile(Parametr.Report, filename, Parametr.ConectWork);
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
                    return await Task.Factory.StartNew(() => selectfull.BdkSqlSelect(Parametr.ConectWork, ((ServiceWcf)sqlconnect.SelectFullParametrSqlReader(Parametr.ConectWork, ModelSqlFullService.ProcedureSelectParametr, typeof(ServiceWcf), ModelSqlFullService.ParamCommand("7"))).ServiceWcfCommand.Command));
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
                    return await taskcommand.TaskSqlProcedureBdk(Parametr.ConectWork, setting);
                default:
                    return null;
            }
        }

        public string StartNewOpenXmlTemplate(FullSetting setting)
        {
            try
            {
                Task.Factory.StartNew(() =>
                {
                    GenerateDocument report = new GenerateDocument();
                    report.GenerateOutBdk(Parametr.ConectWork, Parametr.ConnectionString, Parametr.ReportMassTemplate, setting);
                });
                return "Документы для печати запущены и сохраняются в папку "+ Parametr.ReportMassTemplate;
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
                return e.Message;
            }
        }
    }
}
