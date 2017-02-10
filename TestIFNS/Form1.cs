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

            string path = Path.GetFullPath(@"C:\Users\7751_svc_admin\Desktop\C\Path\");
            string path1 = Path.GetFullPath(@"C:\Users\7751_svc_admin\Desktop\C\QBEDate\");
            if (Directory.Exists(path))
            {
                String[] dirarr = Directory.GetFiles(path).Select(x => Path.GetFileName(x)).ToArray();
                listView2.Items.Clear();
                foreach (String item in dirarr)
                {
                    listView2.Items.Add(item);
                }
            }
            if (Directory.Exists(path1))
            {
                String[] dirarr = Directory.GetFiles(path1).Select(x => Path.GetFileName(x)).ToArray();
                listView5.Items.Clear();
                foreach (String item in dirarr)
                {
                    listView5.Items.Add(item);
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
                    if (String.IsNullOrWhiteSpace(f.TextBox))
                    {
                        MessageBox.Show("Не введено название файла!");
                    }
                    else
                    {
                        ListViewItem a = listView2.FindItemWithText(f.TextBox + ".txt");
                        if (a == null)
                        {
                            using (StreamWriter file = new StreamWriter(@"C:\Users\7751_svc_admin\Desktop\C\Path\" + f.TextBox + ".txt"))
                                foreach (ListViewItem str in listView1.Items)
                                {
                                    file.WriteLine(str.Text);
                                }
                            string path = Path.GetFullPath(@"C:\Users\7751_svc_admin\Desktop\C\Path\");
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
            string[] file = File.ReadAllLines(@"C:\Users\7751_svc_admin\Desktop\C\Path\" + lvl);
            foreach (String item in file)
            {
                listView1.Items.Add(item);
            }
        }
        private void удалитьQBEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem lvl = listView2.SelectedItems[0];  //Переменная имени Файла
            listView2.Items.Remove(lvl);
            File.Delete(@"C:\Users\7751_svc_admin\Desktop\C\Path\" + lvl.Text);
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
                    if (String.IsNullOrWhiteSpace(f.TextBox))
                    {
                        MessageBox.Show("Не введено название файла!");
                    }
                    else
                    {
                        ListViewItem a = listView5.FindItemWithText(f.TextBox + ".txt");
                        if (a == null)
                        {
                            using (StreamWriter file = new StreamWriter(@"C:\Users\7751_svc_admin\Desktop\C\QBEDate\" + f.TextBox + ".txt"))
                            {
                                file.WriteLine(DateStart.Text.ToString() + "/" + DateFinish.Text.ToString());
                            }
                            string path = Path.GetFullPath(@"C:\Users\7751_svc_admin\Desktop\C\QBEDate\");
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
            {
                string lvl = listView5.SelectedItems[0].Text;  //Переменная имени Файла
                string[] file = File.ReadAllText(@"C:\Users\7751_svc_admin\Desktop\C\QBEDate\" + lvl).Split('/');
                DateStart.Text = file[0];
                DateFinish.Text = file[1];
            }
        }

        private void удалитьQBEToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ListViewItem lvl = listView5.SelectedItems[0];  //Переменная имени Файла
            listView5.Items.Remove(lvl);
            File.Delete(@"C:\Users\7751_svc_admin\Desktop\C\QBEDate\" + lvl.Text);
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
            A.cal();
        }
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value += e.ProgressPercentage; // Меняю данные прогрессбара
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            button6.Enabled = true; // После окончания расчета разблокируем опасные кнопки
            progressBar1.Value = 10000;
        }

    }
}




