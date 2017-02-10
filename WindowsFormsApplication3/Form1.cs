using System;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Program
{
    public partial class Form1 : Form
    {
        public int global1; // Используем для передачи одного из результатов фонового расчета в форму для отображения

        public Form1()
        {
            InitializeComponent();
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
            backgroundWorker1.WorkerReportsProgress = true;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolStripProgressBar1.Value = e.ProgressPercentage; // Меняю данные прогрессбара
            toolStripStatusLabel1.Text = (String)e.UserState; // Меняю значение метки
        }

        // Расчет запускается при нажатии на кнопку button1
        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false; // На время расчета блокируем опасные кнопки
            toolStrip1.Enabled = false;

            backgroundWorker1.RunWorkerAsync(); // ЗАПУСКАЕМ ВЫЧИСЛЕНИЯ В ФОНОВОМ ПОТОКЕ!!!!
        }

        // Точка запуска фоновых расчетов
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Calc(); // Вызываем метод с расчетами            
        }

        // Метод с расчетами
        private void Calc()
        {
            int bigNumber = 77777777;
            for (int i = 1; i < bigNumber; i++)
            {
                int n = (int)i * 100 / bigNumber; // вычисляем значение % для прогрессбара
                String s = "Выполнено " + n + "% расчета";
                backgroundWorker1.ReportProgress(n, s); // Отправляем данные в ProgressChanged
            }
            int global1 = i; // данные переменной global1 потом используем по окончании расчета для изменения формы 
            backgroundWorker1.ReportProgress(100, "Расчет завершен!"); // Выводим данные на форму
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            button1.Enabled = true; // После окончания расчета разблокируем опасные кнопки
            toolStrip1.Enabled = true;
            label2.Text = "По окончанию расчета перешли к числу" + global1;
            MessageBox.Show("Расчет окончен!");
        }
    }
}