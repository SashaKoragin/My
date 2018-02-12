using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using ZZZ.Pages;
using System.Windows.Controls;
using ZZZ.Trige;
using ZZZ.SqldataS.ShellSelec;

namespace ZZZ.Sobytie
{
  public class Sob2
    {
        public Page2 P2;
        internal Sob2(Page2 owner)  //Для того чтобы элементы класса Form1 отражались в DBStringDBF
        {
          P2 = owner;
        }

        internal String ValOtdelP2()
        {
            var bindingExpression = P2.OtdelTextBox.GetBindingExpression(TextBox.TextProperty);
            if (bindingExpression != null)
                bindingExpression.UpdateSource();
            return !Validation.GetHasError(P2.OtdelTextBox) ? P2.OtdelTextBox.Text : null;
        }

        internal String ValNameUserP2()
        {
            var expression = P2.NameUserTextBox.GetBindingExpression(TextBox.TextProperty);
            if (expression != null)
                expression.UpdateSource();
            return !Validation.GetHasError(P2.NameUserTextBox) ? P2.NameUserTextBox.Text : null;
        }

        internal String ValTabelP2()
        {
            var bindingExpression1 = P2.TabelTextBox.GetBindingExpression(TextBox.TextProperty);
            if (bindingExpression1 != null)
                bindingExpression1.UpdateSource();
            return !Validation.GetHasError(P2.TabelTextBox) ? P2.TabelTextBox.Text : null;
        }


      public void AddUsers()
      {
          var otdel = ValOtdelP2();
          var user = ValNameUserP2();
          var tabel = ValTabelP2();
          string exceptionMsg = "";
          if (String.IsNullOrWhiteSpace(otdel) || String.IsNullOrWhiteSpace(user) || String.IsNullOrWhiteSpace(tabel))
          {}
          else
          {
              var add = new SqldataP.ShellProc.Procedure();
              var table = add.AddUsersP2(otdel,user,tabel,ref exceptionMsg);
              if (exceptionMsg.Equals(""))
              {
                  P2.Error.Clear();
                  P2.Resul.ItemsSource = table.DefaultView;
                  var gg = (Trig)P2.Resources["Trig"];
                  gg.FocusedElement = 0;
              }
              else
              {
                  P2.Resul.ItemsSource = null;
                  P2.Error.Text = exceptionMsg;
                  var gg = (Trig)P2.Resources["Trig"];
                  gg.FocusedElement = 1;
              }

          }
      }

    }
}
