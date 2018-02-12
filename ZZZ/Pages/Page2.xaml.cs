using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZZZ.Sobytie;
using ZZZ.Trige;
using ZZZ.SqldataS.ShellSelec;

namespace ZZZ.Pages
{
    /// <summary>
    /// Логика взаимодействия для Page2.xaml
    /// </summary>
    public partial class Page2 : Page //: INotifyPropertyChanged
    {

        public Page2()
        {
            InitializeComponent();
        }

        public void AddUser(object sender, RoutedEventArgs e)
        {
            var sob2 = new Sob2(this);
            sob2.AddUsers();
            Page2_OnLoaded(sender,e);
        }

        private void Page2_OnLoaded(object sender, RoutedEventArgs e)
        {
            var sel = new Select(this);
            sel.FillDataGridUsers();
        }
    }
}

