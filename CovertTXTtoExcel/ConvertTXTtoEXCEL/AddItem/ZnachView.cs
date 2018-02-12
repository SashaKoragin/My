using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace CovertTXTtoExcel.ConvertTXTtoEXCEL.AddItem
{
    public class ZnachView  : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<ZnachView> File { get; set; }

            private void OnPropertyChanged(String propertyName)
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }

            public string Pathfile;
            public string Namefile;
        public string NameFile
            {
                get { return Namefile; }
                set
                {
                    Namefile = value;
                    OnPropertyChanged("NameFile");
                }
            }

            public string PathFile
            {
                get { return Pathfile; }
                set
                {
                    Pathfile = value;
                    OnPropertyChanged("PathFile");
                }
            }
    }
}