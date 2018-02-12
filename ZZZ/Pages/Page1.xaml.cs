using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace ZZZ.Pages
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void AddUser(object sender, RoutedEventArgs e)
        {
            var p = new Page2();
            var navigationService = NavigationService;
            if (navigationService != null) navigationService.Navigate(p);
        }
    }
}
