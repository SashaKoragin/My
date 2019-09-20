using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;
using DocumentFormat.OpenXml.Bibliography;
using System.DirectoryServices.AccountManagement;
using System.Text;
using System.Xml;
using LibaryDocumentGenerator.Documents.Template;

namespace DBF
{
    public partial class Form1 : Form
    {
        public HotKey _hotkeystart = new HotKey();
        public HotKey _hotkeystop = new HotKey();

        public Form1()
        {
            InitializeComponent();
            _hotkeystart.KeyModifier = HotKey.KeyModifiers.Alt;
            _hotkeystart.Key = Keys.X;
            _hotkeystart.HotKeyPressed += Stop;
            _hotkeystop.KeyModifier = HotKey.KeyModifiers.Alt;
            _hotkeystop.Key = Keys.Y;
            _hotkeystop.HotKeyPressed += Start;
        }



        public CancellationTokenSource CancellationToken = new CancellationTokenSource();

        public CancellationToken Token => CancellationToken.Token;



        public void Start()
        {
            Task.Run(delegate
            {
                var i = 1;
                while (true)
                {
                    progressBar1.Invoke(new MethodInvoker(delegate { progressBar1.Value = i; }));
                    Thread.Sleep(1000);
                    if (progressBar1.Value == progressBar1.Maximum)
                    {
                        progressBar1.Value = 0;
                        break;
                    }
                    if (Token.IsCancellationRequested)
                    {
                        return;
                    }
                    i++;
                }
                MessageBox.Show("Завершили обработку");
            });
        }

        public void Stop()
        {
            Task.Run(delegate
            {
                MessageBox.Show("Остановили обработку");
                CancellationToken.Cancel();

                //  CancellationToken.Dispose();
            });
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PrincipalContext context = new PrincipalContext(ContextType.Domain);
            if (context.ValidateCredentials(Names.Text, Pass.Text))
            {
                UserPrincipal user = UserPrincipal.FindByIdentity(context, Names.Text);
                if (user != null)
                    MessageBox.Show(@"Авторизованы " + user.MiddleName + " " + user.EmailAddress + " " +
                                    user.Description + " " + user.DisplayName + " " + user.Name + " " +
                                    user.SamAccountName);
                return;
            }
            MessageBox.Show("Данный пользователь отсутствует в системе!!!");
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Test test = new Test();
            //System.IO.StringWriter writer = new System.IO.StringWriter();
            //DataSet dataSet = new DataSet();
            //using (var con = new SqlConnection(test.Conection))
            //{
            //    using (var cmd = new SqlCommand(test.Selectxml, con))
            //    {
            //        cmd.Connection.Open();
            //        using (var sqlreport = new SqlDataAdapter(cmd))
            //        {
            //            if (sqlreport != null)
            //            {

            //                sqlreport.Fill(dataSet);
            //            }
            //        }
            //    }
            //}
            //dataSet.Tables[0].WriteXml(writer);
            //Console.Write(writer.ToString());
            //dataGridView1.DataSource = dataSet.Tables[0];
            {
                StringBuilder sb = new StringBuilder();
                using (var con = new SqlConnection(test.Conection))
                {
                    using (var cmd = new SqlCommand(test.Selectxml, con))
                    {
                        cmd.Connection.Open();
                        using (XmlReader reader = cmd.ExecuteXmlReader())
                        {
                         //   if (reader != null)
                         //   {
                         //   string s = "";
                        //    sb.Append(s);
                           string str = null;
                           reader.Read();
                           while (str != "")
                           {
                               str = reader.ReadInnerXml();
                               sb.AppendLine(str);
                           }
                      Console.Write(sb.ToString());
                      //          //  return sb.ToString();
                      ////      }
                        }
                    }
                }
                //  return string.Empty;
            }
        }
    }
}


