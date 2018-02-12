using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TabControl
{
  public class Tab
    {
        Form1 _owner;
        public Tab(Form1 owner)  //Для того чтобы элементы класса Form1 отражались в DBStringDBF
        {
            _owner = owner;
        }
         public void sss()
        {
            NewMethod();

        }

         private void NewMethod()
         {
             DataGridView dgv = new DataGridView();
             dgv.Name = "Студенты";
             dgv.Left = 0;
             dgv.Top = 0;
             dgv.Width = 200;
             dgv.Height = 200;
             TabPage WWW = new TabPage("111");
             WWW.Controls.Add(dgv);
            // _owner.tabControl1.TabPages.Add(WWW);
         }
    }
}
