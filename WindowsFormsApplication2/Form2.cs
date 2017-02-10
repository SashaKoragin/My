using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace WindowsFormsApplication2
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)  //Отправитель
        {
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            groupBox1.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string Otdel = textBox1.Text.ToString();
            string name = textBox2.Text.ToString();
            string tabel = textBox3.Text.ToString();
            if (String.IsNullOrWhiteSpace(textBox1.Text) || String.IsNullOrWhiteSpace(textBox2.Text) || String.IsNullOrWhiteSpace(textBox3.Text))
            {
                label3.Text = "Не все данные заполнены";
            }
            else
            {
                string AddSender = "IntoSender";
                      string connStr = "Data Source=i7751-w40000722;Initial Catalog=App_test;Integrated Security=True";
                      //MyClass n = new MyClass();
                      SqlConnection conn = new SqlConnection(connStr);
                      
                      conn.Open();
                     label3.Text = "Подключение удалось";
                     SqlCommand command = new SqlCommand(AddSender, conn);
                     command.CommandType = CommandType.StoredProcedure;
                     SqlParameter OtdelParam = new SqlParameter
                     {
                         ParameterName = "@the_otdel",
                         Value = Otdel
                     };
                     command.Parameters.Add(OtdelParam);
                     SqlParameter nameParam = new SqlParameter
                     {
                         ParameterName = "@name",
                         Value = name
                     };
                     command.Parameters.Add(nameParam);
                     SqlParameter tabelParam = new SqlParameter
                     {
                         ParameterName = "@the_personnel",
                         Value = tabel
                     };
                     command.Parameters.Add(tabelParam);
                     
                     try
                     {
                         object result = command.ExecuteScalar();
                         label3.Text = result.ToString();
                         conn.Close();
                     }
                     catch (Exception ex)
                     {
                         MessageBox.Show(ex.Message); 
                     }
                 }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }
        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            button1.Visible = true;
            button2.Visible = true;
            button3.Visible = true;
            groupBox1.Visible = false;
        }
        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}