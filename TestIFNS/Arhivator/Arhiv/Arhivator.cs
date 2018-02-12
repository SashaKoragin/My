using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace TestIFNSTools.Arhivator.Arhiv
{

    public partial class Arhivator : Form
    {
        public Arhivator()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            var dirDialog = new FolderBrowserDialog
            {
                Description = @"Выбор директорию для поиска!",
                SelectedPath = @"C:\"
            };

            if (dirDialog.ShowDialog() != DialogResult.OK) return;
            if(listView1.Items.Count > 0)
            {
                var a = listView1.FindItemWithText(dirDialog.SelectedPath,false,0,false); //Проверка есть ли элемент в списке
                if (a == null)
                {
                    textBox1.Text = dirDialog.SelectedPath;
                    listView1.Items.Add(dirDialog.SelectedPath);
                }
                else
                {
                    MessageBox.Show(@"Такой путь уже есть в списке");
                }
            }
            else
            {
                var a = listView1.FindItemWithText(dirDialog.SelectedPath); //Проверка есть ли элемент в списке
                if (a == null)
                {
                    textBox1.Text = dirDialog.SelectedPath;
                    listView1.Items.Add(dirDialog.SelectedPath);
                }
                else
                {
                    MessageBox.Show(@"Такой путь уже есть в списке");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var dirDialog = new FolderBrowserDialog
            {
                Description = @"Выбор директорию для сохранения архивов!",
                SelectedPath = @"C:\"
            };

            if (dirDialog.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = dirDialog.SelectedPath;
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            
            if (listView1.Items.Count > 0)
            {
                var f = new DialogForm.DialogForm();
                f.ShowDialog();
                if (f.DialogResult == DialogResult.OK)
                {
                        var a = listView2.FindItemWithText(f.textBox1.Text + ".txt");
                        if (a == null)
                        {
                            using (var file = new StreamWriter(Pathing.PathName.Path + f.textBox1.Text + ".txt"))
                                foreach (ListViewItem str in listView1.Items)
                                {
                                    file.WriteLine(str.Text);
                                }
                            string path = Path.GetFullPath(Pathing.PathName.Path);
                            if (!Directory.Exists(path)) return;
                            var dirarr = Directory.GetFiles(path, "*.txt").Select(Path.GetFileName).ToArray();
                            listView2.Items.Clear();
                            foreach (String item in dirarr)
                            {
                                listView2.Items.Add(item);
                            }
                        }
                        else
                        {
                            MessageBox.Show(@"Имя файла совпадает с конечным!");
                        }
                }
            }
            else
            {
                MessageBox.Show(@"Нет элементов для добавления!!!");
            }
        }
        private void listView1_MouseClick(object sender, MouseEventArgs e)  //Для задания координат в listview на элемент
        {
            if (e.Button != MouseButtons.Right) return;
            if (listView1.FocusedItem.Bounds.Contains(e.Location))  //Проверка нажатия на элемент listviewItem
            {
                contextMenuStrip1.Show(Cursor.Position);
            }
        }

        private void удалитьПутьToolStripMenuItem_Click(object sender, EventArgs e)  //Закодирована кнопка для нахатия на элемент происходит удаление
        {
            ListViewItem lvl = listView1.SelectedItems[0];
            listView1.Items.Remove(lvl);
        }
        private void listView2_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (listView2.FocusedItem.Bounds.Contains(e.Location))
                {
                    contextMenuStrip2.Show(Cursor.Position);
                }
            }
        }
        private void загрузитьQBEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            string lvl = listView2.SelectedItems[0].Text;  //Переменная имени Файла
            string[] file = File.ReadAllLines(Pathing.PathName.Path + lvl);
            foreach (String item in file)
            {
                listView1.Items.Add(item);
            }
        }
        private void удалитьQBEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem lvl = listView2.SelectedItems[0];  //Переменная имени Файла
            listView2.Items.Remove(lvl);
            File.Delete(Pathing.PathName.Path + lvl.Text);
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            DateStart.Text = monthCalendar1.SelectionRange.Start.ToString("dd.MM.yyyy");
            DateFinish.Text = monthCalendar1.SelectionRange.End.ToString("dd.MM.yyyy");
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(DateStart.Text) || String.IsNullOrWhiteSpace(DateFinish.Text))
            {
                MessageBox.Show(@"Не введены данные по датам");
            }
            else
            {
                var f = new DialogForm.DialogForm();
                f.ShowDialog();
                if (f.DialogResult == DialogResult.OK)
                {
                    if (String.IsNullOrWhiteSpace(f.textBox1.Text))
                    {
                        MessageBox.Show(@"Не введено название файла!");
                    }
                    else
                    {
                        var a = listView5.FindItemWithText(f.textBox1.Text + ".txt");
                        if (a == null)
                        {
                            using (var file = new StreamWriter(Pathing.PathName.Path1 + f.textBox1.Text + ".txt"))
                            {
                                file.WriteLine(DateStart.Text + "/" + DateFinish.Text);
                            }
                            string path = Path.GetFullPath(Pathing.PathName.Path1);
                            if (Directory.Exists(path))
                            {
                                var dirarr = Directory.GetFiles(path, "*.txt").Select(Path.GetFileName).ToArray();
                                listView5.Items.Clear();
                                foreach (String item in dirarr)
                                {
                                    listView5.Items.Add(item);
                                }
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
        private void listView5_MouseClick(object sender, MouseEventArgs e)  //Для задания координат в listview на элемент
        {
            if (e.Button == MouseButtons.Right)  //Если нажата правая кнопка
            {
                if (listView5.FocusedItem.Bounds.Contains(e.Location))  //Проверка нажатия на элемент listviewItem
                {
                    contextMenuStrip3.Show(Cursor.Position);
                }
            }
        }
        private void загрузитьQBEToolStripMenuItem1_Click(object sender, EventArgs e)
        {
                string lvl = listView5.SelectedItems[0].Text;  //Переменная имени Файла
                string[] file = File.ReadAllText(Pathing.PathName.Path1 + lvl).Split('/');
                DateStart.Text = file[0];
                DateFinish.Text = file[1];
           
        }

        private void удалитьQBEToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ListViewItem lvl = listView5.SelectedItems[0];  //Переменная имени Файла
            listView5.Items.Remove(lvl);
            File.Delete(Pathing.PathName.Path1 + lvl.Text);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DateStart.Text = "";
            DateFinish.Text = "";
        }

        private void listView4_MouseClick(object sender, MouseEventArgs e)  //Для задания координат в listview на элемент
        {
            if (e.Button == MouseButtons.Right)  //Если нажата правая кнопка
            {
                if (listView4.FocusedItem.Bounds.Contains(e.Location))  //Проверка нажатия на элемент listviewItem
                {
                    contextMenuStrip4.Show(Cursor.Position);
                }
            }
        }
        private void удалитьОТЧЕТToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem lvl = listView4.SelectedItems[0];  //Переменная имени Файла
            listView4.Items.Remove(lvl);
            File.Delete(Pathing.PathName.Path2 + lvl.Text);
        }

        private void открытьОТЧЕТToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem lvl = listView4.SelectedItems[0];  //Переменная имени Файла
            string file = Path.GetFullPath(Pathing.PathName.Path2 + lvl.Text);
            var openX = new OpenFile.OpenFile();
            openX.Openxls(file);

        }

        private void загрузитьОТЧЕТToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView3.Items.Clear();
            ListViewItem lvl = listView4.SelectedItems[0];  //Переменная имени Файла
            string file = Path.GetFullPath(Pathing.PathName.Path2 + lvl.Text);
           using (var workbook = new XLWorkbook(file))
           {
               IXLWorksheet workSheet = workbook.Worksheet(1);
               var dt=new DataTable();
               bool firstRow = true;
               foreach (IXLRow row in workSheet.Rows())
               {
                   if (firstRow)
                   {
                       dt.Rows.Add();
                       int i = 0;
                       foreach (IXLCell cell in row.Cells())
                       {
                           dt.Columns.Add(cell.Value.ToString());
                           dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
                           i++;
                       }
                       firstRow = false;
                   }
                   else
                   {
                       dt.Rows.Add();
                       int i = 0;
                       foreach (IXLCell cell in row.Cells())
                       {
                           dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
                           i++;
                       }
                   }
               }
               
               foreach (DataRow rows in dt.Rows)
               {
                   var item = new ListViewItem(rows[0].ToString());
                   for (int i = 1; i < dt.Columns.Count; i++)
                   {
                       item.SubItems.Add(rows[i].ToString());
                   }
                   listView3.Items.Add(item);
               }
           
           }
            
            
        }

        private void Sovpad_Enter(object sender, EventArgs e)
        {
            var t = new ToolTip();
            t.SetToolTip(Sovpad, "Примеры ввода: *t*,*t,t*,t где t это текст!");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var conect = new OtcetKV.DbStringDbf(this);
            conect.ExistsOpenDbf();
        }

        private void listView6_MouseClick(object sender, MouseEventArgs e)  //Для задания координат в listview на элемент
        {
            if (e.Button == MouseButtons.Right)  //Если нажата правая кнопка
            {
                if (listView6.FocusedItem.Bounds.Contains(e.Location))  //Проверка нажатия на элемент listviewItem
                {
                    contextMenuStrip5.Show(Cursor.Position);
                }
            }
        }


        private void открытьОтчетToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var lvl = listView6.SelectedItems[0];  //Переменная имени Файла
            var file = Path.GetFullPath(Pathing.PathName.Path3 + lvl.Text);
            var openX = new OpenFile.OpenFile();
            openX.Openxls(file);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            button6.Enabled = false;
            if (listView1.Items.Count == 0 || String.IsNullOrWhiteSpace(DateStart.Text) || String.IsNullOrWhiteSpace(DateFinish.Text) || String.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show(@"Введены не все элементы");
                button6.Enabled = true;
            }
            else
            {

                backgroundWorker1.RunWorkerAsync();
            }

        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            var a = new Farhiv.Arhiv(this);
            a.ArhivF();

        }
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Stat.Value += e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            button6.Enabled = true; // После окончания расчета разблокируем опасные кнопки
            Stat.Value = 10000;
            Stat.Value = 0;
        }

        private void просмотретьKVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            просмотретьKVToolStripMenuItem.Enabled = false;
            открытьОтчетToolStripMenuItem1.Enabled = false;
            backgroundWorker2.RunWorkerAsync();
        }
        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            var otcets = new OtcetKV.DbStringDbf(this);
            otcets.OpenSelectDbf();

        }
        private void backgroundWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Stat.Value += e.ProgressPercentage;
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            открытьОтчетToolStripMenuItem1.Enabled = true;
            просмотретьKVToolStripMenuItem.Enabled = true; // После окончания расчета разблокируем опасные кнопки
            toolStripStatusLabel1.Text = @"ОТЧЕТ СОБРАН!!!";
            Stat.Value = 10000;
            Stat.Value = 0;
        }

        private void удалитьОтчетToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ListViewItem lvl = listView6.SelectedItems[0];  //Переменная имени Файла
            listView6.Items.Remove(lvl);
            File.Delete(Pathing.PathName.Path3 + lvl.Text);
        }

        private void button8_Click(object sender, EventArgs e)
        {

            if (Application.OpenForms["Detalizacia"] == null)
            {
                var frm = new Detalizacia.Detalizacia();
                frm.Show();
                Hide();
            }
            else
            {
                Application.OpenForms["Detalizacia"].Show();
                Hide();
            }
        }
    }
}




