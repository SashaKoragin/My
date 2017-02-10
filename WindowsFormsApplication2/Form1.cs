using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using WindowsFormsApplication2;

namespace Interface
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string x;
            if (checkBox1.Checked == true)
            {
                x = "Where C.i4 = 2";
            }
            else
            {
                if (checkBox2.Checked == true)
                {
                    x = "Where C.i4 = 5";
                }
                else
                {
                    x = "";
                }
            }
            //if (!int.TryParse(textBox1.Text, out x))
            //{
            //    label2.Text = "Следует вводить положительные числа";
            //    label2.ForeColor = Color.Red;
            //    return;
            //}
            string connStr = "Data Source=i7751-w40000722;Initial Catalog=Application;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connStr);
            try
            {
                conn.Open();
                label2.Text = "Подключили";
                SqlCommand cmd = new SqlCommand("Select E.name,A.Num,A.NumOUT,C.name_status,name_status,name_resource,Coment From OUT A join Answer B on A.Num=B.Num Join Status C on C.i4=B.i4 join Resource D on D.i3=A.i3 Join Sender E on E.i2=A.i2 " + x.ToString(), conn);
               using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                   
            {

                DataTable list = new DataTable();
                list.Load(dr);
                    {
                        dataGridView1.DataSource = list;
                        conn.Close();
                }
            }

            }
            catch
            {

                label2.Text = "Не подключили";
                return;
            }

        }


        public void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                checkBox2.Checked = false;
            }

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
             if (checkBox2.Checked == true)
            {
                checkBox1.Checked = false;
            }
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            //Hide();
            Form2 B = new Form2();
            B.ShowDialog();
        }




    }

}
