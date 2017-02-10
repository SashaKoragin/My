using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Квадратный_корень
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)

        {
            button1.Text = "Извлечь корень";
            //label2.Text = null;
            base.Text = "Извлечение квадратного корня";
            textBox1.Clear();
            textBox1.TabIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        
         //{ MessageBox.Show("Всем привет!");}
        {
            Single x;
            bool Число_ли = Single.TryParse(textBox1.Text,
                System.Globalization.NumberStyles.Number,
                System.Globalization.NumberFormatInfo.CurrentInfo,
                out x);
            if (Число_ли == false)
            {
                label2.Text = "Следует вводить числа";
                label2.ForeColor = Color.Red;
                return;
            }
            else
            {
                var Y = (Single)Math.Sqrt(x);
                label2.ForeColor = Color.Black;
                label2.Text = String.Format("Корень из {0} равно {1:F5}", x, Y);
                label2.ForeColor = Color.Green;
                return;
            }
        }
        }
    }