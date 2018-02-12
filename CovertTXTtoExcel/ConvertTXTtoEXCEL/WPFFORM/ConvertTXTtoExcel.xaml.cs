using System.Windows;
using System.Windows.Input;

namespace CovertTXTtoExcel.ConvertTXTtoEXCEL.WPFFORM
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void AddFile(object sender, RoutedEventArgs e)
        {
            var cliker = new ButtonClick.SelectionFile(this);
            cliker.AddFile(sender,e);
        }

        private void ConvertFile(object sender, RoutedEventArgs e)
        {
            var convert = new ButtonClick.SelectionFile(this);
            convert.ToGo();
        }

        private void MenuItem_Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            var add = new ButtonClick.SelectionFile(this);
            add.LoadPathExcel();
        }

        private void Otcet_Open(object sender, MouseButtonEventArgs e)
        {
            Events.EventsSob.OpenFile(sender,Otcet,e);
        }

        private void File_Open(object sender, MouseButtonEventArgs e)
        {
            Events.EventsSob.OpenFileTxt(sender, ListFile, e);
        }
    }
}
