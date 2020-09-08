using System;
using System.Data;
using System.Data.OleDb;
using System.ServiceModel;
using System.Threading.Tasks;
using TestIFNSLibary.PathJurnalAndUse;
using TestIFNSLibary.WebSevice.Bakcup;
using TestIFNSLibary.Xml.XmlDS;

namespace TestIFNSLibary.Service
{
    [ServiceBehavior(UseSynchronizationContext = true, IncludeExceptionDetailInFaults = true)]
    public class CommandDbf : IReaderCommandDbf
    {
        public Parameter Parametr = new Parameter();
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

        public bool IsActive()
        {
            Xml.Xml xmlstatusbakcup = new Xml.Xml();
            return xmlstatusbakcup.YesandNo();
        }

        public DateTime DateBakcup()
        {
            Xml.Xml xmlstatusbakcup = new Xml.Xml();
            return xmlstatusbakcup.DateBakcup();
        }

        public Parameter Config()
        {
            return new Parameter();
        }

        public void SaveSeting(string testDb, string workDb, int hours, int minutes)
        {
            Parametr.SettingEdit(testDb, workDb, hours,minutes);
        } 

        public void FileBakcup()
        {
            Task task = new Task((() =>
            {
                Parameter param = new Parameter();
                using (BakcupingDb bakcuping = new BakcupingDb())
                {
                    bakcuping.Backup(param.WorkDB, param.TestDB,param.PathJurnal);
                }
            }));
            task.Start();
        }

        public BakcupJurnal[] Jurnal()
        {
            Xml.Xml xmlstatusbakcup = new Xml.Xml();
            return xmlstatusbakcup.Jurnal();
        }
    }
}