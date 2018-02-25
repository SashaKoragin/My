
using System.ComponentModel;
using TestIFNSTools.Detalizacia.WpfBakcupStart.ContentBakcup.Service;

namespace TestIFNSTools.Detalizacia.WpfUserControl.ServiceDialod
{
    /// <summary>
    /// Данный класс надо перенести на WCF и состояние кна вызывать из Конфигурации приложения WCF 
    /// Метод Копирования надо сделать по времени!!!
    /// </summary>
    public class OpenDialogWpf : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool _isopendialog;

        public bool IsOpenDialog
        {
            get { return _isopendialog; }
            set
            {

                _isopendialog = value;
                OnPropertyChanged("IsOpenDialog");
            }
        }

        public void OpenAndClose(Service service=null)
        {
            if (IsOpenDialog)
            {
                IsOpenDialog = false;
            }
            else
            {
                IsOpenDialog = true;
                if (service != null)
                    service.Update();
            }
        }
    }
}