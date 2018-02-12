using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using ConvertDBFtoSQL2008.DBFConvert;

namespace ConvertDBFtoSQL2008.ConectSelect
{
    public class ConectSelect
    {

    public void use()
        {
            FileInfo vet = new FileInfo("D:\\KLADR\\KLADR.DBF");
            Dictionary<int, string> FieldIndex = new Dictionary<int, string>();
            FieldIndex.Add(0, "NAME");
            FieldIndex.Add(1, "SOCR");
            FieldIndex.Add(2, "CODE");
            FieldIndex.Add(3, "INDEX");
            FieldIndex.Add(4, "GNINMB");
            FieldIndex.Add(5, "UNO");
            FieldIndex.Add(6, "OCATD");
            FieldIndex.Add(7, "STATUS");
            SaveToTable(vet ,"d",ConectionString.Conections.SqlConection, FieldIndex);

        }

        public TabPage Conect()
        {
            var  TTT = new TabPage();
            using (var con = new OleDbConnection(ConectionString.Conections.DbfConect))
            {
                con.Open();
                using (var comand = con.CreateCommand())
                {
                    comand.CommandText = "Select * From STREET.DBF";
                    using (var da = comand.ExecuteReader())
                    {
                        if (da != null)
                        {
                        TTT = TabPage(da);
                        
                        }
                      
                    }
                    con.Close();
                }
            }
            return TTT;
        }


       public void SaveToTable(FileInfo dir, string TableName, string connestionString, Dictionary<int, string> FieldIndex)
        {
            using (var loader = new SqlBulkCopy(connestionString, SqlBulkCopyOptions.Default))
            {
                loader.DestinationTableName = TableName;
                loader.BulkCopyTimeout = 9999;
                loader.WriteToServer(new DbfBulkReader(dir.FullName, FieldIndex));
            }
        }


        public static TabPage TabPage(OleDbDataReader data)
        {
            var table = new DataSet().Tables.Add();
            table.Load(data);
            var  dataGrid =new DataGridView
            {
                Left = 0, Top = 0, Width = 1100, Height = 200, DataSource = table
            };
            var pages = new TabPage(table.TableName);
            pages.Controls.Add(dataGrid);
            return pages;
        }


    }


 
    //internal static TabPage TabPages()
    //{
    ////var dataGrid = new DataGridView {Left = 0, Top = 0, Width = 1191, Height = 224, DataSource = table};
    ////var pages = new TabPage(table.TableName);
    ////pages.Controls.Add
    ////(dataGrid);
    //return ;
    ////pages;
    //}

}






//namespace TestIFNSTools.Detalizacia.UL.SQLZap
//{
//    public class Qbe
//    {

//        public DataSet Sqlul(string inn, int god, object[] qwering)
//        {
//            var dd = ((Detalizacia)Application.OpenForms["Detalizacia"]);
//            var table = new DataSet();
//            var i = 0;
//            // ReSharper disable once SpecifyACultureInStringConversionExplicitly
//            var goding = Convert.ToString(god.ToString());
//            var proc = (100.0f / qwering.Count());
//            foreach (string sql in qwering)
//            {
//                table.Tables.Add();
//                using (var con = new OleDbConnection(Arhivator.Pathing.ConnectString.Connection))
//                {
//                    con.Open();
//                    using (var cmd = con.CreateCommand())
//                    {
//                        cmd.Parameters.Add(new OleDbParameter("ИНН", inn));
//                        cmd.Parameters.Add(new OleDbParameter("ГОДДОХ", goding));
//                        cmd.CommandText = sql;
//                        using (var da = cmd.ExecuteReader())
//                        {
 //                           if (da != null) table.Tables[i].Load(da);  ndfl.Fn1534.Length > 1    ?    ", {0}"   :    "."
//                        }
//                    }
//                    con.Close();
//                    i++;
//                    if (dd != null)
//                        dd.BeginInvoke(new MethodInvoker(delegate { dd.StatusBar.Value += ((int)(proc * 100.0f)); }));
//                }
//            }
//            if (dd != null)
//                dd.BeginInvoke(new MethodInvoker(delegate { dd.StatusBar.Value = 0; }));
//            return table;
//        }
//    }
//}