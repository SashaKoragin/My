using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestIFNSTools.Detalizacia.WpfUserControl.Face.FL.SqlParamSelect
{

   public class Qbe
    {
        public DataSet Sqlfl(string sqlSelect, Detalizacia detal)
        {
            var service = new ServiceTestIfns.ReaderCommandDbfClient("BasicHttpBinding_IReaderCommandDbf");
            var table = new DataSet();
            int i = 0;
            string conect = Arhivator.Pathing.ConnectString.Connection;
            try
            {
                table.Tables.Add();
                table = service.SqlFl(sqlSelect, conect, table, i);
                detal?.BeginInvoke(new MethodInvoker(delegate { detal.StatusBarFl.Value += ((int) (10000)); }));
                detal?.BeginInvoke(new MethodInvoker(delegate { detal.StatusBarFl.Value = 0; }));
                
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
            return table;
        }
    }
}
