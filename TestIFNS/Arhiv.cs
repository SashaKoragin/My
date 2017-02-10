using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace TestIFNS
{
    public class Arhiv
    {
        Form1 _owner;
        public Arhiv(Form1 owner)  //Для того чтобы элементы класса Form1 отражались в Arhiv
        {
            _owner = owner;
        }

        public void cal()
        {
            MessageBox.Show("Ведены элементы");
            var i = 1;
            List<string> files = new List<string>();
            List<Tuple<string,string,string,string>> otchet = new List<Tuple<string,string,string,string>>(); //Для отчета создание динамического
            foreach (ListViewItem str in _owner.listView1.Items)
            {
                if (Directory.Exists(str.Text))
                {

                    String[] dirarr = Directory.GetFiles(str.Text, _owner.Sovpad.Text).Select(x => Path.GetFullPath(x)).ToArray();

                    foreach (string file in dirarr)
                    {
                        var date = File.GetLastWriteTime(file).Date;
                        //MessageBox.Show(date.ToString());
                        //MessageBox.Show(DateStart.Text);
                        //MessageBox.Show(DateFinish.Text);
                        if (DateTime.Parse(_owner.DateStart.Text) <= DateTime.Parse(date.ToString()) && DateTime.Parse(_owner.DateFinish.Text) >= DateTime.Parse(date.ToString()))
                        {
                            files.Add(file);
                        }
                    }
                }
            }
            if (files.Count > 0)
            {
                float Proc = (100/files.Count);
                foreach (string f in files)
                {
                    ListViewItem oth = new ListViewItem(new string[] { i.ToString(), File.GetLastWriteTime(f).Date.ToString(), Path.GetFileName(f).ToString(), Path.GetFullPath(f).ToString()});
                    otchet.Add(new Tuple<string,string,string,string>(i.ToString(), File.GetLastWriteTime(f).Date.ToString(), Path.GetFileName(f).ToString(), Path.GetFullPath(f).ToString()));
                    _owner.listView3.Items.Add(oth);
                    i++;
                    _owner.backgroundWorker1.ReportProgress((int)(Proc * 100.0f));  //В нее можно только integer (не double с плавующей точкой) подумать!!!!! 
                }

                var workbook = new XLWorkbook();
                var worksheet =  workbook.Worksheets.Add("Отчет отработаных.");
                worksheet.Cells("A1").Value = otchet.ToArray();
                workbook.SaveAs(@"C:\Users\7751_svc_admin\Desktop\C\ddd.xlsx");

                _owner.label5.Text = "Количество файлов:  " + files.Count;
            }
            else
            {
                _owner.label5.Text = "Файлы не надены!!!";
                
            }

        }

    }
}
