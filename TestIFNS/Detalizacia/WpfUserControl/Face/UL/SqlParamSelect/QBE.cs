using System;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms;


namespace TestIFNSTools.Detalizacia.WpfUserControl.Face.UL.SqlParamSelect
{
    public class Qbe
    {
        public DataSet Sqlul(string inn, int god, object[] qwering,Detalizacia detal)
        {
            var service = new ServiceTestIfns.ReaderCommandDbfClient("BasicHttpBinding_IReaderCommandDbf");
            var table = new DataSet();
            var i = 0;
            var goding = Convert.ToString(god.ToString());
            var proc = (100.0f / qwering.Count());
            foreach (string sql in qwering)
            {
                table.Tables.Add();
                table = service.SqlUl(inn, goding, sql, Arhivator.Pathing.ConnectString.Connection, table, i);
                i++;
                detal.BeginInvoke(new MethodInvoker(delegate { detal.StatusBarUL.Value += ((int)(proc * 100.0f)); }));
            }
                detal.BeginInvoke(new MethodInvoker(delegate { detal.StatusBarUL.Value = 0; }));
            return table;
        }
    }
}