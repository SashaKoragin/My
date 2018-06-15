using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TestIFNSTools.Detalizacia.WpfBakcupStart.ContentBakcup.Service;


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
        /// <param name="service">Наш сервис взаимодействия с WCF</param>
        public ControlBakcup(VoidService service)
        {
            InitializeComponent();
            DataContext = new ContentBakcup.MVVM.DialogWindow(service);
        }


    }
}
