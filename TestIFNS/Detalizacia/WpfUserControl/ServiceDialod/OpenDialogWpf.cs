using System;
using MaterialDesignThemes.Wpf;

namespace TestIFNSTools.Detalizacia.WpfUserControl.ServiceDialod
{
    internal static class OpenDialogWpf
    {
        /// <summary>
        /// Диалоговое окно для Нашего Bakcup!!!
        /// </summary>
        internal static async void OpenDialog()
        {
            try
            {
                var dialogwindow = new WpfBakcupStart.XamlForm.ControlBakcup() { DataContext = new WpfBakcupStart.ContentBakcup.MVVM.DialogWindow() };
                var result = await DialogHost.Show(dialogwindow);
            }
            catch (Exception e)
            {
                 
            }

        }
    }
}
