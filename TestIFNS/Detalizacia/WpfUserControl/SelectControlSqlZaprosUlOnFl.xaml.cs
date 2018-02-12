using TestIFNSTools.Detalizacia.WpfUserControl.DataContext;
using UserControl = System.Windows.Controls.UserControl;

namespace TestIFNSTools.Detalizacia.WpfUserControl
{
    /// <summary>
    /// Логика взаимодействия для SelectControlSqlZaprosUlOnFl.xaml
    /// </summary>
    public partial class SelectControlSqlZaprosUlOnFl : UserControl
    {
        public SelectControlSqlZaprosUlOnFl(Detalizacia detal)
        {
            InitializeComponent();
            DataContext = new DataDatalization(detal);
        }
    }
}
