using System;
using System.IO;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.OleDb;
using ClosedXML.Excel;

namespace DBF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String[] dirarr =
                Directory.GetFiles(@"D:\DBF", "*.DBF", SearchOption.AllDirectories)
                    .Select(x => Path.GetFullPath(x))
                    .ToArray();
            foreach (string file in dirarr)
            {
                listView1.Items.Add(file);


            }

        }


        //Нужна DLL установочник
        private void button2_Click(object sender, EventArgs e)
        {
            //string FilePath = @"\\i7751-w40000722\2017_03_16 05_00_03";
            string constr1 = @"Provider=vfpoledb.1;Data Source=D:\DBF;Collating Sequence=Machine";
            //var i = 1;
            var workbook = new XLWorkbook();
            //   string sql = "select statistics.ID_FILE, * from NA left Join statistics on NA.ID_BOSS=statistics.ID_BOSS Where ИНН in ('7751001314','7751506971')";  //Join statistics on NA.ID_BOSS=statistics.ID_BOSS left Join 2016\\Sved2016.dbf t1 on t1.ID_FILE=statistics.ID_FILE left join 2016\\Sv_summ2016 t2 on t2.ID_FILE=statistics.ID_FILE
            // string sql = "select statistics.ID_FILE, * from NA left Join statistics on NA.ID_BOSS=statistics.ID_BOSS Where ИНН = '5074117066' and ГОДДОХ = 2012";
            // string sql = "select statistics.ID_FILE, * from NA left Join statistics on NA.ID_BOSS=statistics.ID_BOSS left Join 2016\\Sved2016 t1 on t1.ID_FILE=statistics.ID_FILE left join 2016\\Sv_summ2016 t2 on t2.ID_FILE=statistics.ID_FILE Where ИНН = '7728262893' and ГОДДОХ ='2016'";  //Join statistics on NA.ID_BOSS=statistics.ID_BOSS left Join 2016\\Sved2016.dbf t1 on t1.ID_FILE=statistics.ID_FILE left join 2016\\Sv_summ2016 t2 on t2.ID_FILE=statistics.ID_FILE
            // string sql = "select distinct t1.ИННФЛ,t1.ФАМИЛИЯ,t1.ИМЯ,t1.ОТЧЕСТВО,t1.ДАТАРОЖД,t1.ГРАЖД,t1.СЕРНОМДОК,t2.* from NA left Join statistics on NA.ID_BOSS=statistics.ID_BOSS left Join 2017\\Sved2017 t1 on t1.ID_FILE=statistics.ID_FILE left join 2017\\Sv_summ2017 t2 on t2.ID_DOK=t1.ID_DOK  Where t1.ИННФЛ ='7728262893' and ГОДДОХ = '2016'"; //1

            string sql1 = "Update statistics set АРХИВ = STRTRAN(АРХИВ,'app002','w40000650')"; //5

            string sql2 = "Update statistics set АРХИВ = STRTRAN(АРХИВ,'w40000722','w40000650')"; //5

            string sql3 = "Update statistics set DIRPRGPR1 = STRTRAN(DIRPRGPR1,'app002','w40000650')"; //5

            string sql4 = "Update statistics set DIRPRGPR1 = STRTRAN(DIRPRGPR1,'w40000722','w40000650')"; //5
            string sql5 = "Update statistics set АРХИВ = STRTRAN(АРХИВ,'i7751-app021\\MNSExchange\\Export\\NDFL','i7751-app033\\LPK_NBO\\EXPORT\\2NDFL')"; //5
            //   string sql2 = "Delete From 2016\\Sved2016  Where ID_FILE in (37494,45714)"; //
            //string sql3 = "Delete From 2016\\Sv_summ2016  Where ID_FILE in (37494,45714)";
            //string sql4 = "Delete From 2016\\KsSved2016.DBF  Where ID_FILE in (37494,45714)";
            //string sql5 = "Delete From Kvitan  Where ID_FILE in (37494,45714)";

            //          select statistics.ID_FILE, * from NA left Join statistics on NA.ID_BOSS=statistics.ID_BOSS left Join 2016\\Sved2016 t1 on t1.ID_FILE=statistics.ID_FILE left join 2016\\Sv_summ2016 t2 on t2.ID_FILE=statistics.ID_FILE Where ИНН = ? and ГОДДОХ = ?
            //Нужно менять ID_BOSS В statistics, NA, Sved2016
            //string sql = "select distinct t1.* from 2016\\Sved2016 t1";
            //string sql = "Select * From NA";
            //string sql = "select * from statistics Join NA on NA.ID_BOSS=statistics.ID_BOSS Where statistics.ID_BOSS=14823";

            OleDbDataAdapter oledbAdapter = new OleDbDataAdapter();
            try
            {

                using (OleDbConnection connection = new OleDbConnection(constr1))
                {
                    connection.Open();
                    oledbAdapter.UpdateCommand = connection.CreateCommand();
                    oledbAdapter.UpdateCommand.CommandText = sql5;
                    int rows = oledbAdapter.UpdateCommand.ExecuteNonQuery();
                    MessageBox.Show(rows.ToString());
                    if (rows > 0)
                    {
                        MessageBox.Show("Вроде обновили");
                    }
                }
            }
            catch (Exception ef)
            {
                MessageBox.Show(ef.Message);
            }


            //DataTable objDT = new DataTable();
            //OleDbConnection con = new OleDbConnection(constr1);
            //   using (OleDbCommand cmd = new OleDbCommand(sql1, con))
            //  {
            //        con.Open();
            //        try
            //        {
            //           // using (OleDbCommand cmd = con.CreateCommand())
            //           // {
            //                //   cmd.Parameters.Add(new OleDbParameter("ИННФЛ", inn));
            //                //  cmd.Parameters.Add(new OleDbParameter("ГОДДОХ", goding));
            //                //cmd.CommandText = sql;
            //                using (OleDbDataReader da = cmd.ExecuteReader())
            //                /  {
            //                //   dataGridView1.DataSource = da;
            //                //     //if (da != null) obj/DT.Load(da);
            //                //  }
            //                  }
            //                da.DeleteCommand = con.CreateCommand();
            //                da.DeleteCommand.CommandText = sql1;
            //                int rows = da.DeleteCommand.ExecuteNonQuery();
            //                MessageBox.Show(rows.ToString());
            //                da.Fill(objDT);
            //                // var worksheet = workbook.Worksheets.Add("Отчет" + i);
            //                //  worksheet.Cell("A1").InsertTable(objDT).Worksheet.Columns().AdjustToContents();

            //                //i++;
            //                con.Close();
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show(ex.ToString());
            //        }
            //     }

            //  }
            //  workbook.SaveAs(@"C:\Users\7751_svc_admin\Desktop\C\1\fff.xlsx");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string connectStr = @"Data Source=i7751-w40000722;Initial Catalog=Testing;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connectStr);
            try
            {
                conn.Open();
            }
            catch (SqlException se)
            {
                if (se.Number == 4060)
                {
                    label1.Text = "Ждите";

                    conn.Close();
                    conn = new SqlConnection(@"Data Source=i7751-w40000722;Integrated Security=True");
                    SqlCommand cmdCreateDatabase = new SqlCommand(string.Format("Create DATABASE [{0}]", "Testing"),
                        conn);
                    conn.Open();
                    label1.Text = "Посылаем запрос";
                    cmdCreateDatabase.ExecuteNonQuery();
                    conn.Close();
                    conn = new SqlConnection(connectStr);
                    conn.Open();
                }


            }
            finally
            {
                label1.Text = "Проверяй";
                conn.Close();


            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //string result;
            OleDbConnection conn = new OleDbConnection();
            SqlConnection connSQL = new SqlConnection();
            DataTable dt = new DataTable();
            conn.ConnectionString =
                @"Provider=vfpoledb.1;Data Source=\\i7751-w40000722\SrezDBF;Collating Sequence=Machine";
            connSQL.ConnectionString = @"Data Source=i7751-w40000722;Initial Catalog=Testing;Integrated Security=True";


            try
            {

                conn.Open();
                connSQL.Open();




                SqlBulkCopy bulk = new SqlBulkCopy(connSQL);
                bulk.DestinationTableName = "dbo.OUT";
                bulk.BatchSize = 1000000;
                OleDbCommand comm = conn.CreateCommand();
                comm.CommandText = @"SELECT * FROM Sos";
                dt.Load(comm.ExecuteReader());
                bulk.WriteToServer(dt);

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.Close();
                connSQL.Close();
                //result = "копирование завершено";

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

            try
            {
                string sqlConnection =
                    @"Data Source=i7751-w40000722;Initial Catalog=FIASXML;Integrated Security=True;MultipleActiveResultSets=True";
                SqlCommand sqlCommand = new SqlCommand("Select ImageColumn From fff");

               // string sqlConnection = @"Data Source=i7751-app196;Initial Catalog=Taxes51;Integrated Security=True;MultipleActiveResultSets=True";
               // SqlCommand sqlCommand = new SqlCommand("select Template From Reports Where ReportID = 22");

                DataTable dt = GetData(sqlCommand, sqlConnection);
                if (dt != null)
                {
                    Byte[] bytes = (Byte[])dt.Rows[0]["ImageColumn"];
                    MemoryStream stream = new MemoryStream();
                    stream.Write(bytes, 0, bytes.Length);
                    File.WriteAllBytes(@"C:\rr.dotx", stream.ToArray());
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }



        }

        private DataTable GetData(SqlCommand cmd, string sqlConnection)
        {
            DataTable dt = new DataTable();
            String strConnString = sqlConnection;
            SqlConnection con = new SqlConnection(strConnString);
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            try
            {
                con.Open();
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                return dt;
            }
            catch
            {
                return null;
            }
            finally
            {
                con.Close();
                sda.Dispose();
                con.Dispose();
            }
        }
    }

   
}


