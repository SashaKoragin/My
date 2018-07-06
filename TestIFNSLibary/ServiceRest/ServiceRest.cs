using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.ServiceModel;
using System.Threading.Tasks;
using LibaryXMLAuto.ModelXmlSql.Model.Trebovanie;
using LibaryXMLAutoModelXmlSql.Model.FaceError;
using TestIFNSLibary.PathJurnalAndUse;
using TestIFNSLibary.PostRequest.Face;
using SqlLibaryIfns.SqlEntytiCommand.TaskUse;
using SqlLibaryIfns.SqlZapros.SobytieSql;
using SqlLibaryIfns.SqlZapros.ZaprosSelectNotParam;

namespace TestIFNSLibary.ServiceRest
{
    [ServiceBehavior(UseSynchronizationContext = true, IncludeExceptionDetailInFaults = true)]
    public class ServiceRest : IServiceRest
    {
        //Функции для сайта IFNS
        /// <summary>
        /// Тест пост запроса хотим посмотреть как работает
        /// </summary>
        /// <returns></returns>
        public FaceAdd Test()
        {
            FaceAdd add= new FaceAdd();
            add.N1New = 1212;
            add.N1Old = 23232;
            return add;
        }

       public List<Test.Test> Test1()
        {
            List<Test.Test> _products = new List<Test.Test>
                        {
                            new Test.Test() {ProductId = 1, Name = "Product 1", CategoryName = "Category 1", Price = 10},
                            new Test.Test() {ProductId = 2, Name = "Product 2", CategoryName = "Category 1", Price = 5},
                            new Test.Test() {ProductId = 3, Name = "Product 3", CategoryName = "Category 2", Price = 15},
                            new Test.Test() {ProductId = 4, Name = "Product 4", CategoryName = "Category 3", Price = 9}
                        };
            return _products;
        }

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
        public async Task<string> StoreProcedure(Setting seting)
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
        public async Task<string> LoaderReshenie(Setting seting)
        {
          var selectfull = new SelectFull();
            switch (seting.Db)
            {
                case "Work":
                   return await Task.Factory.StartNew<string>(() => selectfull.SysNumReshenie(Parametr.ConectWork, SqlLibaryIfns.SqlSelect.SqlReshenia.ProcedureReshenie.SelectReshenie));
                case "Test":
                      return await Task.Factory.StartNew<string>(() => selectfull.SysNumReshenie(Parametr.ConectTest, SqlLibaryIfns.SqlSelect.SqlReshenia.ProcedureReshenie.SelectReshenie));
                default:
                    return null;
            }
        }
    }
}
