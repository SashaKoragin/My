using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ZZZ.SqldataP.ShellProc
{
    class Procedure
    {
        private static readonly string AddUser = Proc.Procedure.AddUsersProc;

        public DataTable AddUsersP2(string otdel, string name, string tabel, ref string exceptionMsg)
        {

            var dt = new DataTable();
            using (var con = new SqlConnection(Config.ConnectString.Connection))
            {
                
                try
                {
                    using (var cmd = new SqlCommand(AddUser, con))
                    {
                        
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@the_otdel", otdel);
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@the_personnel", tabel);
                        con.Open();
                        using (var dr = cmd.ExecuteReader())
                        {
                            dt.Load(dr);
                        }
                    }
                    con.Close();
                }
                catch (Exception e)
                {
                    exceptionMsg = e.Message;
                }
            }
            return dt;
        }
    }
}
