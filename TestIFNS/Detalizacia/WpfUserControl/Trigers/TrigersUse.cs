using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TestIFNSTools.Detalizacia.WpfUserControl.Collections.PanelSqlZap;

namespace TestIFNSTools.Detalizacia.WpfUserControl.Trigers
{
   public class TrigersUse : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private Boolean _isenablebuttonul = true;
        private Boolean _isenablebuttonfl = true;
        private Boolean _visibleFl;
        private Boolean _visibleUl = true;
        private Boolean _ul = true;
        private Boolean _fl;

        public Boolean IsVisibleUl
        {
            get { return _visibleUl; }
            set
            {
                _visibleUl = value;
                OnPropertyChanged("IsVisibleUl");
            }
        }
        public Boolean IsVisibleFl
        {
            get { return _visibleFl; }
            set
            {
                _visibleFl = value;
                OnPropertyChanged("IsVisibleFl");
            }
        }
        public Boolean IsEnableButtonUl
        {
            get { return _isenablebuttonul; }
            set
            {
                _isenablebuttonul = value; 
                OnPropertyChanged("IsEnableButtonUl");
            }
        }
        public Boolean IsEnableButtonFl
        {
            get { return _isenablebuttonfl; }
            set
            {
                _isenablebuttonfl = value;
                OnPropertyChanged("IsEnableButtonFl");
            }
        }
        public Boolean Ul
        {
            get { return _ul; }
            set
            {
                _ul = value;
                OnPropertyChanged("Ul");
            }
        }

        public Boolean Fl
        {
            get { return _fl; }
            set
            {
                _fl = value;
                OnPropertyChanged("Fl");
            }
        }

        private Boolean _isCheked;
        private string _selected = @"Поставте галку чтобы выбрать ФЛ!!!";

        public string Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                OnPropertyChanged("Selected");
            }
        }
        public Boolean IsCheked
        {
            get { return _isCheked; }
            set
            {
                _isCheked = value;
                OnPropertyChanged("IsCheked");
            }
        }

        public void IsSelectFace()
        {
            if (IsCheked)
            {
                IsVisibleFl = true;
                IsVisibleUl = false;
                IsCheked = true;
                Fl = true;
                Ul = false;
                Selected = @"Снимите галку чтобы выбрать ЮЛ!!!";
            }
            else
            {
                IsVisibleFl = false;
                IsVisibleUl = true;
                IsCheked = false;
                Fl = false;
                Ul = true;
                Selected = @"Поставте галку чтобы выбрать ФЛ!!!";
            }
        }
    }
}
