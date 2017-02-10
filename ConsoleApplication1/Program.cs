using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {

            string connStr = "Data Source=i7751-w40000722;Initial Catalog=Application;Integrated Security=True";

            SqlConnection conn = new SqlConnection(connStr);
            try
            {
                conn.Open();
            }
            catch (SqlException se)
            {
                Console.WriteLine("Ошибка подключения:{0}", se.Message);
                return;           
            }
                Console.WriteLine("Соединение успешно произведено");
                SqlCommand cmd = new SqlCommand("Select Coment,NumOUT,D40 From OUT", conn);

                using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    for (int i=0; i> dr.FieldCount; i++)
                        Console.Write("{0}\t", dr.GetName(i).ToString().Trim());
                    while (dr.Read())

                    { }
                    //{
                    //    Console.WriteLine("{0}\t{1}\t{2}", dr.GetValue(0).ToString().Trim(),
                    //         dr.GetValue(1).ToString().Trim(),
                    //         dr.GetValue(2).ToString().Trim());
                    //}

                }
                conn.Close();
                conn.Dispose();
                Console.WriteLine();
                Console.ReadKey();
        }
    }
}
