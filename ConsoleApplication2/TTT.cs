using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
namespace ConsoleApplication2
{
    /// <TTT>
    /// Базовый класс тестового приложения
    /// </TTT>
    public class Ttt
    {
        /// <summary>DoWork is a method in the TestClass class.
        /// <para>Here's how you could make a second paragraph in a description. <see cref="System.Console.WriteLine(System.String)"/> for information about output statements.</para>
        /// <seealso cref="Ttt.CreateMyOleDbCommand"/>
        /// </summary>
        static public void CreateMyOleDbCommand(string args)
       {


           string connectionString = "Provider=vfpoledb.1;Data Source=D:\\DBF;Collating Sequence=Machine";

           using (OleDbConnection connection = new OleDbConnection(connectionString))
           {
               connection.Open();
               OleDbCommand cmd = connection.CreateCommand();
               cmd.Parameters.Add(new OleDbParameter("ИНН", "7751512277"));
               cmd.CommandText = "select * from NA Where ИНН = ? ";
               
               Console.WriteLine(cmd.ExecuteScalar());
               Console.ReadKey();
           }
       }

    }
}
