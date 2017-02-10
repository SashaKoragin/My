using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace WindowsFormsApplication1
{
    class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        static void Main(string[] arg)
        {


            var Conect = new System.Data.SqlClient.SqlConnection();
            Conect.ConnectionString = "Data Source=i7751-w40000722;Initial Catalog=Sample;Integrated Security=True";
            Conect.Open();
            var Comand = new System.Data.SqlClient.SqlCommand();
            Comand.Connection = Conect;
            Comand.CommandText = "Select Coment,NumOUT,D40 From OUT";
            Comand.CommandType = CommandType.Text;

            var reader = Comand.ExecuteReader(CommandBehavior.CloseConnection);
            Console.Title = "Выборка из Application";
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.WriteLine("{0,11} {1,-11} {2,-15}" + "\n", reader.GetName(0), reader.GetName(1), reader.GetName(2));
            while (reader.Read() == true)
            {
                Console.WriteLine("{0,-11} {1,-11} {2,-15}",
                    reader.GetValue(0), reader.GetValue(1), reader.GetValue(2));
            }

            Console.ReadKey();
            Conect.Close();
            reader.Close();
        }
    }




}
