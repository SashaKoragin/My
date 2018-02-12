using System;
using System.Windows.Forms;
using TestIFNSTools.Detalizacia.WpfUserControl;

namespace TestIFNSTools.Detalizacia
{
    public partial class Detalizacia : Form 
    {
        internal SelectControlSqlZaprosUlOnFl UserControlSqlZaprosUlOnFl;
        public Detalizacia()
        {
            InitializeComponent();
            UserControlSqlZaprosUlOnFl = new SelectControlSqlZaprosUlOnFl(this);
            HostWpfXaml.Child = UserControlSqlZaprosUlOnFl;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["Arhivator"] == null)
            {
                var frm = new Arhivator.Arhiv.Arhivator();
                frm.Show();
                Hide();
            }
            else
            {
                Application.OpenForms["Arhivator"].Show();
                Hide();
            }
        }
    }
}
