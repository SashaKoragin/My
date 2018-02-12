using System;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms;

namespace TestIFNSTools.Detalizacia.WpfUserControl.Face.UL.File.Files
{
   internal class SeathFile
    {
        static readonly object[] FileDb =
        {
            ResursFile.FileResurs.FilePR,
            ResursFile.FileResurs.ProtokolTKS
        };

        internal DataSet SeathF(string inn, string god, Detalizacia detal)
        {
            try
            {
                var service = new ServiceTestIfns.ReaderCommandDbfClient("BasicHttpBinding_IReaderCommandDbf");
                var table = new DataSet();
                var i = 0;
                var proc = (100.0f / FileDb.Count());
                foreach (string sqlfile in FileDb)
                {
                    table.Tables.Add();
                    table = service.SqlUl(inn, god, sqlfile, Arhivator.Pathing.ConnectString.Connection, table, i);
                    i++;
                    detal.BeginInvoke(new MethodInvoker(delegate { detal.StatusBarUL.Value += ((int) (proc * 100.0f)); }));
                }
                detal.BeginInvoke(new MethodInvoker(delegate { detal.StatusBarUL.Value = 0; }));
                return table;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                throw;
            }
        }
    }
}
