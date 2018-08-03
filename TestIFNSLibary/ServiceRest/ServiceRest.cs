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
using SqlLibaryIfns.SqlZapros.ZaprosSelectNotParam;
using System.IO;
using LibaryDocumentGenerator.Documents.Word.Generate.StartGenerate;
using LibaryXMLAuto.ModelXmlSql.Model.FullSetting;
using SqlLibaryIfns.ExcelReport.Report;
using SqlLibaryIfns.SqlSelect.SqlReshenia;



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
          var selectfull = new SelectFull();
            switch (seting.Db)
            {
                case "Work":
                   return await Task.Factory.StartNew<string>(() => selectfull.SysNumReshenie(Parametr.ConectWork, ProcedureReshenie.SelectReshenie));
                case "Test":
                      return await Task.Factory.StartNew<string>(() => selectfull.SysNumReshenie(Parametr.ConectTest, ProcedureReshenie.SelectReshenie));
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
                    return await Task.Factory.StartNew(() => selectfull.BdkSqlSelect(Parametr.ConectWork, SqlLibaryIfns.SqlSelect.SqlBdkIt.SqlBdkIt.SelectAnalisBdk));
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

        public void StartNewOpenXmlTemplate()
        {
            Task.Factory.StartNew(() =>
            {
                DocumentsWord report = new DocumentsWord();
                report.StartWordBdk(Parametr.ConectTest, Parametr.ConnectionString, Parametr.ReportMassTemplate);
            });
        }
    }
}
