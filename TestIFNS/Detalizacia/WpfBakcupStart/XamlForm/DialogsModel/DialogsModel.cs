using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TestIFNSTools.Detalizacia.WpfBakcupStart.XamlForm.DialogsModel
{
    /// <summary>
    /// Модель для диалоговых окон
    /// </summary>
   public class DialogsModel :Prism.Mvvm.BindableBase
   {
        /// <summary>
        /// Колекция Диалоговых окон
        /// </summary>
        public  ObservableCollection<DialogsModel> Dialogmodel = new ObservableCollection<DialogsModel>();

       private DialogsModel _dialog;
        /// <summary>
        /// Диалог
        /// </summary>
        public DialogsModel Dialog
        { get { return _dialog; }
            set
            {
                _dialog = value;
                RaisePropertyChanged();
            }
        }

       private UserControl _userControl;

       private string _name;
        /// <summary>
        /// Диалоговое окно
        /// </summary>
       public UserControl UserControl
       {
           get { return _userControl; }
           set
           {
               _userControl = value;
               RaisePropertyChanged();
           }
       }

       public string Name
       {
           get { return _name; }
           set
           {
               _name = value;
               RaisePropertyChanged();
           }
       }
    }

    public class VoidDialog : DialogsModel
    {
        public VoidDialog(){}
        public VoidDialog(ContentBakcup.Service.VoidService service)
        {
            AddDialog(service);
        }

        public void AddDialog(ContentBakcup.Service.VoidService service)
        {
            Dialogmodel.Add(new DialogsModel() {UserControl = new ControlBakcup(service), Name = "DialogBakcup"});
            Dialogmodel.Add(new DialogsModel() { UserControl = new EditConfigWcf(service), Name = "ConfigWcf" });
        }
    }
}
