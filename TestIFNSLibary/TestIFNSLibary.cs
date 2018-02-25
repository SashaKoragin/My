using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.ServiceModel;

namespace TestIFNSLibary
{
    [ServiceBehavior(UseSynchronizationContext = true)]
    public class CommandDbf : IReaderCommandDbf
    {

        public DataSet SqlFl(string command, string conectionstring, DataSet datasetreport, int i)
        {
            using (var con = new OleDbConnection(conectionstring))
            {
                con.Open();
                using (OleDbCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = command;

                    OleDbDataReader da = cmd.ExecuteReader(CommandBehavior.Default);
                    if (da != null) datasetreport.Tables[i].Load(da);
                }
                con.Close();
            }
            return datasetreport;
        }

        public DataSet SqlUl(string inn, string god, string command, string conectionstring, DataSet datasetreport,int i)
        {
            using (var con = new OleDbConnection(conectionstring))
            {
                con.Open();
                using (var cmd = con.CreateCommand())
                {
                    cmd.Parameters.Add(new OleDbParameter("ИНН", inn));
                    cmd.Parameters.Add(new OleDbParameter("ГОДДОХ", god));
                    cmd.CommandText = command;
                    using (var da = cmd.ExecuteReader())
                    {
                        if (da != null) datasetreport.Tables[i].Load(da);
                    }
                }
                con.Close();
            }
            return datasetreport;
        }

        public DateTime DateBakcup()
        {
            DateTime value = Convert.ToDateTime(ConfigurationManager.AppSettings["Date"]);
            return value;
        }
    }
}