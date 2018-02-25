using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestIFNSTools.Detalizacia.WpfBakcupStart.ContentBakcup.Service
{
    /// <summary>
    /// Класс Связки сервиса Нужен  ли??? Рефакторинг нужно прокинуть сервис
    /// </summary>
    public class Service : INotifyPropertyChanged
    {
        private static ServiceTestIfns.ReaderCommandDbfClient service = new ServiceTestIfns.ReaderCommandDbfClient("BasicHttpBinding_IReaderCommandDbf");

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private DateTime _dateBakcup = service.DateBakcup();
        public DateTime DateBakcup
        {
            get { return _dateBakcup; }
            set
            {
                _dateBakcup = value;
                OnPropertyChanged("DateBakcup");
            }
        }

        public void Update()
        {
            DateBakcup = service.DateBakcup();
        }
    }
}
