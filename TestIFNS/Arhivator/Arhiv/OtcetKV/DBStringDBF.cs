using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Discovery;
using System.Windows.Forms;
using ClosedXML.Excel;
using TestIFNSTools.ServiceTestIfns;

namespace TestIFNSTools.Arhivator.Arhiv.OtcetKV
{
    class DbStringDbf
    {
        readonly Arhivator _owner;
        public DbStringDbf(Arhivator owner)  //Для того чтобы элементы класса Form1 отражались в DBStringDBF
        {
            _owner = owner;
        }

        public void ExistsOpenDbf ()
       {
           DiscoveryClient dc = new DiscoveryClient(new UdpDiscoveryEndpoint());
            FindResponse discoveryresponce = dc.Find(new FindCriteria(typeof(IReaderCommandDbf)));
            if (discoveryresponce.Endpoints.Count > 0)
            {
                _owner.button7.Text = @"Соединение со службой WCF ОК!!!";
                _owner.button7.BackColor = Color.Green;
            }
            else
            {
                _owner.button7.Text = @"Нет соединения со службой WCF!!!";
                _owner.button7.BackColor = Color.Red;
            }
            //EndpointAddress address = discoveryresponce.Endpoints[0].Address;
             //    var service = new ReaderCommandDbfClient(new BasicHttpsBinding(), address);
       }

        public void OpenSelectDbf()
        {
            var ll = new DialogForm.DialogForm();
            ll.ShowDialog();
            if (ll.DialogResult == DialogResult.OK)
            {
                var a = _owner.listView6.FindItemWithText(ll.textBox1.Text + ".xlsx");
                if (a == null)
                {
                    _owner.toolStripStatusLabel1.Text = @"ОТЧЕТ СОБИРАЕТСЯ!!!";
                    var con = new OleDbConnection(Pathing.ConnectString.Connection);
                    try
                    {
                        var lvl = _owner.listView4.SelectedItems[0];  //Переменная имени Файла
                        string file = Path.GetFullPath(Pathing.PathName.Path2 + lvl.Text);
                        using (var workbook = new XLWorkbook(file))
                        {
                            IXLWorksheet workSheet = workbook.Worksheet(1);
                            var dt = new DataTable("NDFLIF");
                            var dt1 = new DataTable("NDFLNULLNOT");
                            dt1.Columns.Add("Имя файла");
                            dt1.Columns.Add("Статус");
                            dt1.Columns.Add("Количество");
                            var dt2 = new DataTable("NDFLNOTNULLNOT");
                            dt2.Columns.Add("Имя файла");
                            dt2.Columns.Add("Статус");
                            var dt3 = new DataTable("Detalization");
                            var proc = (100.0f / workSheet.Rows().Count());
                            foreach (var row in workSheet.Rows())
                            {
                                foreach (var cell in row.Cells("C1"))
                                {
                                    string sql = "select ИМЯФАЙЛАВЫ, * from Kvitan Where КОДНООТПР = '9965' and ИМЯФАЙЛАВЫ = '" + cell.Value + "'";
                                    using (var cmd = new OleDbCommand(sql, con))
                                    {
                                        con.Open();
                                        using (var da = new OleDbDataAdapter(cmd))
                                        {
                                            da.Fill(dt);
                                            da.Fill(dt3);
                                            if (dt.Rows.Count > 0)
                                            {
                                                dt1.Rows.Add(cell.Value.ToString(), "Квитанция принята", dt.Rows.Count);
                                            }
                                            else
                                            {
                                                if (cell.Value.ToString() != "")
                                                {
                                                    string sql1 = "select Statistics.ИМЯФАЙЛАВЫ, ИМЯФАЙЛАПР, NA.НАИМОРГ, NA.ИНН, Statistics.ИНФПОЛЕ,'Нет файла квитанции!!!' as 'Состояние' from Statistics left Join NA on NA.ID_BOSS = Statistics.ID_BOSS Where Statistics.ИМЯФАЙЛАВЫ ='" + cell.Value + "'";
                                                    using (var cmd1 = new OleDbCommand(sql1, con))
                                                    {
                                                        using (var da1 = new OleDbDataAdapter(cmd1))
                                                        {
                                                            da1.Fill(dt);
                                                            if (dt.Rows.Count > 0)
                                                            {
                                                                da1.Fill(dt2);
                                                            }
                                                            else
                                                            {
                                                                dt2.Rows.Add(cell.Value.ToString(), "Файл отсутствует в таблице");
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            dt.Clear();
                                        }
                                        con.Close();
                                        _owner.backgroundWorker2.ReportProgress((int)(proc * 100.0f));  //В нее можно только integer (не double с плавующей точкой) подумать!!!!! 
                                    }
                                }
                            }
                            var workbook1 = new XLWorkbook();
                            var worksheet1 = workbook1.Worksheets.Add("Отчет отработаных.");
                            worksheet1.Cell("A1").InsertTable(dt1).Worksheet.Columns().AdjustToContents();
                            var worksheet2 = workbook1.Worksheets.Add("Отчет нет квитанций");
                            worksheet2.Cell("A1").InsertTable(dt2).Worksheet.Columns().AdjustToContents();
                            var worksheet3 = workbook1.Worksheets.Add("Детализация");
                            worksheet3.Cell("A1").InsertTable(dt3).Worksheet.Columns().AdjustToContents();
                            workbook1.SaveAs(Pathing.PathName.Path3 + ll.textBox1.Text + ".xlsx");
                            string path = Path.GetFullPath(Pathing.PathName.Path3);
                            if (Directory.Exists(path))
                            {
                                String[] dirarr = Directory.GetFiles(path, "*.xlsx").Select(Path.GetFileName).ToArray();
                                _owner.listView6.Items.Clear();
                                foreach (String item in dirarr)
                                {
                                    _owner.listView6.Items.Add(item);
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }
                else
                {
                    MessageBox.Show(@"Имя файла совпадает с конечным!");
                }
            }

        }
    }
}
