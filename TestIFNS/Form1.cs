using System;
using System.Configuration;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace TestIFNS
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

           
            if (Directory.Exists(PathName.path))
            {
                String[] dirarr = Directory.GetFiles(PathName.path).Select(x => Path.GetFileName(x)).ToArray();
                listView2.Items.Clear();
                foreach (String item in dirarr)
                {
                    listView2.Items.Add(item);
                }
            }
            if (Directory.Exists(PathName.path1))
            {
                String[] dirarr = Directory.GetFiles(PathName.path1).Select(x => Path.GetFileName(x)).ToArray();
                listView5.Items.Clear();
                foreach (String item in dirarr)
                {
                    listView5.Items.Add(item);
                }
            }
            if (Directory.Exists(PathName.path2))
            {
                String[] dirarr = Directory.GetFiles(PathName.path2).Select(x => Path.GetFileName(x)).ToArray();
                listView4.Items.Clear();
                foreach (String item in dirarr)
                {
                    listView4.Items.Add(item);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog DirDialog = new FolderBrowserDialog();
            DirDialog.Description = "Выбор директорию для поиска!";
            DirDialog.SelectedPath = @"C:\";

            if (DirDialog.ShowDialog() == DialogResult.OK)
            {

                ListViewItem a = listView1.FindItemWithText(DirDialog.SelectedPath); //Проверка есть ли элемент в списке
                if (a == null)
                {
                    textBox1.Text = DirDialog.SelectedPath;
                    listView1.Items.Add(DirDialog.SelectedPath);
                }
                else
                {
                    MessageBox.Show("Такой путь уже есть в списке");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog DirDialog = new FolderBrowserDialog();
            DirDialog.Description = "Выбор директорию для сохранения архивов!";
            DirDialog.SelectedPath = @"C:\";

            if (DirDialog.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = DirDialog.SelectedPath;
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            
            if (listView1.Items.Count > 0)
            {
                DialogForm f = new DialogForm();
                f.ShowDialog();
                if (f.DialogResult == DialogResult.OK)
                {
                        ListViewItem a = listView2.FindItemWithText(f.textBox1.Text.ToString() + ".txt");
                        if (a == null)
                        {
                            using (StreamWriter file = new StreamWriter(PathName.path + f.textBox1.Text.ToString() + ".txt"))
                                foreach (ListViewItem str in listView1.Items)
                                {
                                    file.WriteLine(str.Text);
                                }
                            string path = Path.GetFullPath(PathName.path);
                            if (Directory.Exists(path))
                            {
                                String[] dirarr = Directory.GetFiles(path, "*.txt").Select(x => Path.GetFileName(x)).ToArray();
                                listView2.Items.Clear();
                                foreach (String item in dirarr)
                                {
                                    listView2.Items.Add(item);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Имя файла совпадает с конечным!");
                        }
                }
            }
            else
            {
                MessageBox.Show("Нет элементов для добавления!!!");
            }
        }
        private void listView1_MouseClick(object sender, MouseEventArgs e)  //Для задания координат в listview на элемент
        {
            if (e.Button == MouseButtons.Right)  //Если нажата правая кнопка
            {
                if (listView1.FocusedItem.Bounds.Contains(e.Location) == true)  //Проверка нажатия на элемент listviewItem
                {
                    contextMenuStrip1.Show(Cursor.Position);
                }
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
                if (listView2.FocusedItem.Bounds.Contains(e.Location) == true)
                {
                    contextMenuStrip2.Show(Cursor.Position);
                }
            }
        }
        private void загрузитьQBEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            string lvl = listView2.SelectedItems[0].Text;  //Переменная имени Файла
            string[] file = File.ReadAllLines(PathName.path + lvl);
            foreach (String item in file)
            {
                listView1.Items.Add(item);
            }
        }
        private void удалитьQBEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem lvl = listView2.SelectedItems[0];  //Переменная имени Файла
            listView2.Items.Remove(lvl);
            File.Delete(PathName.path + lvl.Text);
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            //int i = 0;
            //if (i == 0)
            //{
            DateStart.Text = this.monthCalendar1.SelectionRange.Start.ToString("dd.MM.yyyy");
            DateFinish.Text = this.monthCalendar1.SelectionRange.End.ToString("dd.MM.yyyy");
            //}
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(DateStart.Text) || String.IsNullOrWhiteSpace(DateFinish.Text))
            {
                MessageBox.Show("Не введены данные по датам");
            }
            else
            {
                DialogForm f = new DialogForm();
                f.ShowDialog();
                if (f.DialogResult == DialogResult.OK)
                {
                    if (String.IsNullOrWhiteSpace(f.textBox1.Text.ToString()))
                    {
                        MessageBox.Show("Не введено название файла!");
                    }
                    else
                    {
                        ListViewItem a = listView5.FindItemWithText(f.textBox1.Text.ToString() + ".txt");
                        if (a == null)
                        {
                            using (StreamWriter file = new StreamWriter(PathName.path1 + f.textBox1.Text.ToString() + ".txt"))
                            {
                                file.WriteLine(DateStart.Text.ToString() + "/" + DateFinish.Text.ToString());
                            }
                            string path = Path.GetFullPath(PathName.path1);
                            if (Directory.Exists(path))
                            {
                                String[] dirarr = Directory.GetFiles(path, "*.txt").Select(x => Path.GetFileName(x)).ToArray();
                                listView5.Items.Clear();
                                foreach (String item in dirarr)
                                {
                                    listView5.Items.Add(item);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Имя файла совпадает с конечным!");
                        }
                    }
                }
            }
        }
        private void listView5_MouseClick(object sender, MouseEventArgs e)  //Для задания координат в listview на элемент
        {
            if (e.Button == MouseButtons.Right)  //Если нажата правая кнопка
            {
                if (listView5.FocusedItem.Bounds.Contains(e.Location) == true)  //Проверка нажатия на элемент listviewItem
                {
                    contextMenuStrip3.Show(Cursor.Position);
                }
            }
        }
        private void загрузитьQBEToolStripMenuItem1_Click(object sender, EventArgs e)
        {
                string lvl = listView5.SelectedItems[0].Text;  //Переменная имени Файла
                string[] file = File.ReadAllText(PathName.path1 + lvl).Split('/');
                DateStart.Text = file[0];
                DateFinish.Text = file[1];
           
        }

        private void удалитьQBEToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ListViewItem lvl = listView5.SelectedItems[0];  //Переменная имени Файла
            listView5.Items.Remove(lvl);
            File.Delete(PathName.path1 + lvl.Text);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DateStart.Text = "";
            DateFinish.Text = "";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            button6.Enabled = false;
            if (listView1.Items.Count == 0 || String.IsNullOrWhiteSpace(DateStart.Text) || String.IsNullOrWhiteSpace(DateFinish.Text) || String.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Введены не все элементы");
                button6.Enabled = true;
            }
            else
            {

                backgroundWorker1.RunWorkerAsync();
            }

        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Arhiv A = new Arhiv(this);
            A.arhiv();
            
        }
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value += e.ProgressPercentage; // Меняю данные прогрессбара
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            button6.Enabled = true; // После окончания расчета разблокируем опасные кнопки
            progressBar1.Value = 10000;
            progressBar1.Value = 0;
        }

        private void listView4_MouseClick(object sender, MouseEventArgs e)  //Для задания координат в listview на элемент
        {
            if (e.Button == MouseButtons.Right)  //Если нажата правая кнопка
            {
                if (listView4.FocusedItem.Bounds.Contains(e.Location) == true)  //Проверка нажатия на элемент listviewItem
                {
                    contextMenuStrip4.Show(Cursor.Position);
                }
            }
        }
        private void удалитьОТЧЕТToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem lvl = listView4.SelectedItems[0];  //Переменная имени Файла
            listView4.Items.Remove(lvl);
            File.Delete(PathName.path2 + lvl.Text);
        }

        private void открытьОТЧЕТToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem lvl = listView4.SelectedItems[0];  //Переменная имени Файла
            string file = Path.GetFullPath(PathName.path2 + lvl.Text);
            OpenFile OpenX = new OpenFile(this);
            OpenX.Openxls(file);

        }

        private void загрузитьОТЧЕТToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem lvl = listView4.SelectedItems[0];  //Переменная имени Файла
            string file = Path.GetFullPath(PathName.path2 + lvl.Text);
            
           using (XLWorkbook workbook = new XLWorkbook(file))
           {
               IXLWorksheet workSheet = workbook.Worksheet(1);
               DataTable dt=new DataTable();
               bool firstRow = true;
               foreach (IXLRow row in workSheet.Rows())
               {
                   if (firstRow)
                   {
                       foreach (IXLCell cell in row.Cells())
                       {
                           dt.Columns.Add(cell.Value.ToString());
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
                   MessageBox.Show(dt.Columns.ToString());
               }
           }
            
        }

        private void Sovpad_Enter(object sender, EventArgs e)
        {
            ToolTip t = new ToolTip();
            t.SetToolTip(Sovpad, "Примеры ввода: *t*,*t,t*,t где t это текст!");
        }


    }
}




