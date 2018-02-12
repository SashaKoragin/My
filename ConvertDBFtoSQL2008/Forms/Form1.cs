using System.Windows.Forms;

namespace ConvertDBFtoSQL2008.Forms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Convert_Click(object sender, System.EventArgs e)
        {
         //   ConectSelect.ConectSelect con = new ConectSelect.ConectSelect();
          //  var g = con.Conect();
          //  Resul.TabPages.Add(g);
         ConectSelect.ConectSelect use = new ConectSelect.ConectSelect();
            use.use();
        }
    }
}
