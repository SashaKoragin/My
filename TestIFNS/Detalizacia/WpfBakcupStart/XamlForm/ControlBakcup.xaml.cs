using System.Windows.Controls;
using TestIFNSTools.Detalizacia.WpfBakcupStart.ContentBakcup.Service;
using TestIFNSTools.Detalizacia.WpfUserControl.ServiceDialod;


namespace TestIFNSTools.Detalizacia.WpfBakcupStart.XamlForm
{
    /// <summary>
    /// Логика взаимодействия для ControlBakcup.xaml
    /// </summary>
    public partial class ControlBakcup : UserControl
    {
        /// <summary>
        /// Наш диалог с сервисом  WCF
        /// </summary>
        /// <param name="isopen">Параметр открыт True диалог или закрыт False</param>
        public ControlBakcup(OpenDialogWpf isopen, Service service)
        {
            InitializeComponent();
            DataContext = new ContentBakcup.MVVM.DialogWindow(isopen, service);
        }
    }
}
