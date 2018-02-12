using System;
using System.Collections.Generic;
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

namespace WPF1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            LstView.Items.Add(new CityInfo("California", "Los Angeles", false, true));
            LstView.Items.Add(new CityInfo("California", "San Francisco", false, true));
            LstView.Items.Add(new CityInfo("California", "Sacramento", true, false));
            LstView.Items.Add(new CityInfo("California", "San Diego", false, false));
            LstView.Items.Add(new CityInfo("Massachusetts", "Boston", true, true));
            LstView.Items.Add(new CityInfo("Massachusetts", "Newton", false, false));
        }
    }
}
