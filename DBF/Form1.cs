using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Odbc;
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
            String[] dirarr = Directory.GetFiles(@"D:\DBF", "*.DBF",SearchOption.AllDirectories).Select(x => Path.GetFullPath(x)).ToArray();
            foreach (string file in dirarr)
            {
                listView1.Items.Add(file);


            }

        }


        //Нужна DLL установочник
        private void button2_Click(object sender, EventArgs e)
        {
            string FilePath = @"D:\DBF";
            string DBF_FileName = "Kvitan.DBF";
            string constr = "Provider=vfpoledb;Data Source="+FilePath+";Collating Sequence=machine";
            var i = 1;
            var workbook = new XLWorkbook();
             //   String[] dirarr = Directory.GetFiles(@"D:\DBF", "*.DBF", SearchOption.AllDirectories).Select(x => Path.GetFullPath(x)).ToArray();
             //   foreach (string file in dirarr)
           //     {
                    OleDbConnection con = new OleDbConnection(constr);

                    string sql = "select * from NA Join Statistics on NA.ID_BOSS = Statistics.ID_BOSS left Join Kvitan on Kvitan.ID_FILE=Statistics.ID_FILE Where NA.ID_BOSS =346";
                    //string sql = "select NA.DBF.id_boss,Statistics.DBF.id_boss from NA.DBF,Statistics.DBF  Where NA.DBF.id_boss=Statistics.DBF.id_boss and NA.DBF.id_boss =7069";
                    using (OleDbCommand cmd = new OleDbCommand(sql, con))
                    {
                        con.Open();
                       DataSet objDT = new DataSet();
                       using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
                       {
                           da.Fill(objDT);
                           var worksheet = workbook.Worksheets.Add("Отчет" + i);
                           worksheet.Cells("A1").Value = DBF_FileName; //file;
                        //   worksheet.Cells("A2").Value = objDT.Tables[0];
                           dataGridView1.DataSource = objDT.Tables[0];
                           i++;
                       }
                       con.Close();
                    }
          //  }
                workbook.SaveAs(@"C:\Users\7751_svc_admin\Desktop\C\1\fff.xlsx");
        }


    }
}

