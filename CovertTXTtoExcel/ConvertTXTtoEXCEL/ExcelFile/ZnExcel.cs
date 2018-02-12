using System.Drawing;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace CovertTXTtoExcel.ConvertTXTtoEXCEL.ExcelFile
{
    class ZnExcel : INotifyPropertyChanged
    {


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<ZnExcel> FileExcels { get; set; }

        public string Namefile;
        public long Sizen;
        public Icon Icons;

        public string NameFile
        {
            get { return Namefile; }
            set
            {
                Namefile = value;
                OnPropertyChanged("Отчет Excel");
            }
        }

        public long Size
        {
            get { return Sizen; }
            set
            {
                Sizen = value;
                OnPropertyChanged("Размер файла в байтах!");
            }
        }

        public Icon Icon
        {
            get { return Icons; }
            set
            {
                Icons = value;
                OnPropertyChanged("Icon");
            }
        }
    }
}
