using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using ZZZ.Pages;

namespace ZZZ.Trige
{
    //Тригеры на метку 1 Ошибка 0 Нет ошибки задается при изменениии свойства.
    public class Trig : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public int A=100; //Надо че то воткнуть !!!!!

        public int FocusedElement
        {
            get { return A; }
            set
            {
                A = value;
                OnPropertyChanged("FocusedElement");
            }
        }
    }
}
