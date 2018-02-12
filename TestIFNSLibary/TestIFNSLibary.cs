using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace TestIFNSLibary
{
    public class CommandDbf : IReaderCommandDbf
    {
        [OperationBehavior]
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

        [OperationBehavior]
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
    }
}
