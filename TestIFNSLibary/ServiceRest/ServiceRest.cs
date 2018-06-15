using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.ServiceModel;
using System.Xml;
using LibaryXMLAutoModelXmlSql.Model.FaceError;
using TestIFNSLibary.PathJurnalAndUse;
using TestIFNSLibary.PostRequest.Face;
using LibaryXMLAuto.ModelXmlSql.ConvertModel.DesirializationSql;
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

        public Face SqlFaceError()
        {
            SqlDesirialization xmldesirealiz = new SqlDesirialization();
            Face answer;
            using (var con = new SqlConnection(Parametr.ConnectionString))
            {
                using (var cmd = new SqlCommand(SqlLibaryIfns.SqlSelect.SqlFaceMergin.FaceSelectError.FaceError, con))
                {
                    cmd.Connection.Open();
                    using (XmlReader respons = cmd.ExecuteXmlReader())
                    {
                         answer = (Face) xmldesirealiz.ReadXml(respons, typeof(Face));
                    }
                    cmd.Connection.Close();
                }
            }
            return answer;
        }

        public string AddFace(FaceAdd face)
        {
            try
            {
                using (var con = new SqlConnection(Parametr.ConnectionString))
                {
                    con.InfoMessage +=EventSql.MessageSql.Con_InfoMessage;
                    using (var cmd = new SqlCommand(SqlLibaryIfns.SqlSelect.SqlFaceMergin.FaceSelectError.AddFace, con))
                    {
                        cmd.Parameters.Add("@Old", SqlDbType.Int).Value = Convert.ToInt32(face.N1Old);
                        cmd.Parameters.Add("@New", SqlDbType.Int).Value = Convert.ToInt32(face.N1New);
                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();
                        return EventSql.MessageSql.Messages;
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
                using (var con = new SqlConnection(Parametr.ConnectionString))
                {
                    con.InfoMessage += EventSql.MessageSql.Con_InfoMessage;
                    using (var cmd = new SqlCommand(SqlLibaryIfns.SqlSelect.SqlFaceMergin.FaceSelectError.FaceDelete, con))
                    {
                        cmd.Parameters.Add("@idint", SqlDbType.Int).Value = Convert.ToInt32(id);
                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();
                        return EventSql.MessageSql.Messages;
                    }
                }
            }
            catch (SqlException e)
            {
                return e.Message;
            }
        }
    }
}
