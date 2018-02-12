using System.ComponentModel;


namespace CovertTXTtoExcel.ConvertTXTtoEXCEL.AddItem
{
   public class ErrTriger : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public bool Err = true;
        public string ErrorMsg;

        //Тригеры на ChekBox False Ошибка True Нет ошибки задается при изменениии свойство
        public bool FocusedElement
        {
            get { return Err; }
            set
            {
                Err = value;
                OnPropertyChanged("FocusedElement");
            }
        }
        public string Error
        {
            get { return ErrorMsg; }
            set
            {
                ErrorMsg = value;
                OnPropertyChanged("Error");
            }
        }
      public string Scalare { get; set; }
    }
}
