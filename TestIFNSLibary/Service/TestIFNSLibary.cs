using System;
using System.Data;
using System.Data.OleDb;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Timers;
using TestIFNSLibary.PathJurnalAndUse;
using TestIFNSLibary.WebSevice.Bakcup;
using TestIFNSLibary.Xml.XmlDS;

namespace TestIFNSLibary.Service
{
    [ServiceBehavior(UseSynchronizationContext = true, IncludeExceptionDetailInFaults = true)]
    public class CommandDbf : IReaderCommandDbf
    {
        public Parametr Parametr = new Parametr();
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

        public Parametr Config()
        {
            return new Parametr();
        }

        public void SaveSeting(string testDb, string workDb, int hours, int minutes)
        {
            Parametr.SettingEdit(testDb, workDb, hours,minutes);
        } 

        public void FileBakcup()
        {
            Task task = new Task((() =>
            {
                BakcupingDb bakcuping = new BakcupingDb();
                bakcuping.Backup(Parametr.WorkDB, Parametr.TestDB);
            }));
            task.Start();
        }

        public BakcupJurnal[] Jurnal()
        {
            Xml.Xml xmlstatusbakcup = new Xml.Xml();
            return xmlstatusbakcup.Jurnal();
        }
    }

    public class TimerGo
    {
        private Timer _timerstart;
        public void TimerStart()
        {
            _timerstart = new Timer
           {
                Interval = 60000,
                Enabled = true,
                AutoReset = true
           };
          _timerstart.Elapsed += Bakcup;
          _timerstart.Start();
        }

        private void Bakcup(object sender, EventArgs e)
        {
            try
            {
                Parametr parametr = new Parametr();
                DateTime date = DateTime.Now;
                if (date.Hour == parametr.Hours && date.Minute == parametr.Minutes)
                {
                    Task task = new Task((() =>
                    {
                        BakcupingDb bakcuping = new BakcupingDb();
                        bakcuping.Backup(parametr.WorkDB, parametr.TestDB);
                    }));
                    task.Start();
                }
            }
            catch (Exception ex)
            {
                Loggers.Log4NetLogger.Error(ex);
            }
        }
    }
}