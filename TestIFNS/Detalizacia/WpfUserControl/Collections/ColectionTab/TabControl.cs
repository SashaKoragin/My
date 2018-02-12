using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using Prism.Mvvm;
using TestIFNSTools.Detalizacia.WpfUserControl.Collections.ColectionYers;

namespace TestIFNSTools.Detalizacia.WpfUserControl.Collections.ColectionTab
{
   public class TabControl : BindableBase
    {
        public object _lock = new object();
        public void UpdateOff()
        {
            BindingOperations.DisableCollectionSynchronization(ShemedTabItems);
        }

        public void UpdateOn()
        {
            BindingOperations.EnableCollectionSynchronization(ShemedTabItems, _lock);
        }
        private ObservableCollection<TabItem> _shemetab = new ObservableCollection<TabItem>();
        public ObservableCollection<TabItem> ShemedTabItems
        { get { return _shemetab; } }

        public TabItem _tab;

        public TabItem SelectedTab
        {
            get { return _tab; }
            set
            {

                _tab = value;
            }
        }

    }
}
