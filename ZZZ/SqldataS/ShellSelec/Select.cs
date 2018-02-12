using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using ZZZ.Pages;

namespace ZZZ.SqldataS.ShellSelec
{
   internal class Select
    {
        public Page2 P2;
        internal Select(Page2 owner)  //Для того чтобы элементы класса Form1 отражались в DBStringDBF
        {
          P2 = owner;
        }

        internal void FillDataGridUsers()
        {
            using (var con = new SqlConnection(Config.ConnectString.Connection))
            {
                var dt = new DataTable();
                var cmd = new SqlCommand(Selec.Select.SelectUsers,con);
                var sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                P2.Users.ItemsSource = dt.DefaultView;
            }
        }
    }
}
