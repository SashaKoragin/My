using System.Collections.ObjectModel;
using System.ComponentModel;
using Prism.Mvvm;

namespace TestIFNSTools.Detalizacia.WpfUserControl.Collections.ColectionYers
{
   public class YearsDbf : BindableBase, IDataErrorInfo
    {
        private ObservableCollection<YearsDbf> _shemedyears = new ObservableCollection<YearsDbf>();
        public ObservableCollection<YearsDbf> Shemedyears
        { get { return _shemedyears; } }

        public string years;
        public string Years
        {
            get { return years; }
            set { years = value; }
        }

        private YearsDbf _selectyears;
        public YearsDbf SelectYears
        {
            get { return _selectyears; }
            set { _selectyears = value; }
        }

        public string Error { get; set; }
        private bool IsValid { get; set; } = true;

        public bool IsValidation()
        {
            IsValid = false;
            RaisePropertyChanged("SelectYears");
            return IsValid;
        }
        public string this[string columnName]
        {
            get { return ValidateErrs(columnName); }
        }

        private string ValidateErrs(string columnName)
        {
            Error = null;
            if (!IsValid)
                switch (columnName)
                {
                    case "SelectYears":
                        if (SelectYears!=null)
                            { IsValid = true;break; }
                            { Error = "Не выбран год!!!"; break; }
                }
            return Error;
        }
    }
}
