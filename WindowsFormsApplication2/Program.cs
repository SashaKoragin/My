using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Interface
{


    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

    }
    //public class MyClass
    //{
    //      public string connStr = "Data Source=i7751-w40000722;Initial Catalog=App_test;Integrated Security=True";
    //      public string AddSender = "IntoSender";
    //    }
    
}