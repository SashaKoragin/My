using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Diagnostics;
using System.Windows.Media;
using System.ComponentModel;
using System.Windows.Data;
using System.Collections.ObjectModel;

namespace PropertyChangeNotification //требуется сопоставить страницу XAML
{
	public partial class Page1 : Page
	{
        ToolTip ttp;

        // Эта функция проверяет параметры фильтра языка, чтобы определить, какой код следует фильтровать, а также выделяет серым цветом пустые вкладки
        public void checkLang(object sender, EventArgs e)
        {
            if (xcsharpCheck.Content == null) // выделяет серым вкладку "XAML + C#"
            {
                xamlcsharp.Background = Brushes.Gainsboro;
                xamlcsharp.Foreground = Brushes.White;
                ttp = new ToolTip();
                ToolTipService.SetShowOnDisabled(xamlcsharp, true);
                ttp.Content = "This sample is not available in XAML + C#.";
                xamlcsharp.ToolTip = (ttp);
                xamlcsharp.IsEnabled = false;
            }
            else if (xcsharpCheck.Content != null)
            {
                xamlcsharp.IsEnabled = true;
            }

            if (xvbCheck.Content == null) // выделяет серым вкладку "XAML + VB"
            {
                xamlvb.Background = Brushes.Gainsboro;
                xamlvb.Foreground = Brushes.White;
                ttp = new ToolTip();
                ToolTipService.SetShowOnDisabled(xamlvb, true);
                ttp.Content = "This sample is not available in XAML + Visual Basic.NET";
                xamlvb.ToolTip = (ttp);
                xamlvb.IsEnabled = false;
            }
            else if (xvbCheck.Content != null)
            {
                xamlvb.IsEnabled = true;
            }

            if (xaml.Content == null) // выделяет серым XAML
            {
                xaml.IsEnabled = false;
                xaml.Background = Brushes.Gainsboro;
                xaml.Foreground = Brushes.White;
                ttp = new ToolTip();
                ToolTipService.SetShowOnDisabled(xaml, true);
                ttp.Content = "This sample is not available in XAML.";
                xaml.ToolTip = (ttp);
            }
            else if (xaml.Content != null)
            {
                xaml.IsEnabled = true;
            }

            if (csharp.Content == null) // выделяет серым C#
            {
                csharp.IsEnabled = false;
                csharp.Background = Brushes.Gainsboro;
                csharp.Foreground = Brushes.White;
                ttp = new ToolTip();
                ToolTipService.SetShowOnDisabled(csharp, true);
                ttp.Content = "This sample is not available in C#.";
                csharp.ToolTip = (ttp);
            }
            else if (csharp.Content != null)
            {
                csharp.IsEnabled = true;
            }

            if (vb.Content == null) // выделяет серым VB
            {
                vb.IsEnabled = false;
                vb.Background = Brushes.Gainsboro;
                vb.Foreground = Brushes.White;
                ttp = new ToolTip();
                ToolTipService.SetShowOnDisabled(vb, true);
                ttp.Content = "This sample is not available in Visual Basic.NET.";
                vb.ToolTip = (ttp);
            }
            else if (vb.Content != null)
            {
                vb.IsEnabled = true;
            }

            if (managedcpp.Content == null) // выделяет серым CPP
            {
                managedcpp.IsEnabled = false;
                managedcpp.Background = Brushes.Gainsboro;
                managedcpp.Foreground = Brushes.White;
                ttp = new ToolTip();
                ToolTipService.SetShowOnDisabled(managedcpp, true);
                ttp.Content = "This sample is not available in Managed C++.";
                managedcpp.ToolTip = (ttp);
            }
            else if (managedcpp.Content != null)
            {
                managedcpp.IsEnabled = true;
            }
            if (Welcome.Page1.myDouble == 1) // только XAML
            {
                xaml.Visibility = Visibility.Visible;
                csharp.Visibility = Visibility.Collapsed;
                vb.Visibility = Visibility.Collapsed;
                managedcpp.Visibility = Visibility.Collapsed;
                xamlcsharp.Visibility = Visibility.Collapsed;
                xamlvb.Visibility = Visibility.Collapsed;
            }
            else if (Welcome.Page1.myDouble == 2) // CSharp
            {
                csharp.Visibility = Visibility.Visible;
                xaml.Visibility = Visibility.Collapsed;
                vb.Visibility = Visibility.Collapsed;
                managedcpp.Visibility = Visibility.Collapsed;
                xamlcsharp.Visibility = Visibility.Collapsed;
                xamlvb.Visibility = Visibility.Collapsed;
            }
            else if (Welcome.Page1.myDouble == 3) // Visual Basic
            {
                vb.Visibility = Visibility.Visible;
                xaml.Visibility = Visibility.Collapsed;
                csharp.Visibility = Visibility.Collapsed;
                managedcpp.Visibility = Visibility.Collapsed;
                xamlcsharp.Visibility = Visibility.Collapsed;
                xamlvb.Visibility = Visibility.Collapsed;
            }
            else if (Welcome.Page1.myDouble == 4) // Управляемый CPP
            {
                managedcpp.Visibility = Visibility.Visible;
                xaml.Visibility = Visibility.Collapsed;
                csharp.Visibility = Visibility.Collapsed;
                vb.Visibility = Visibility.Collapsed;
                xamlcsharp.Visibility = Visibility.Collapsed;
                xamlvb.Visibility = Visibility.Collapsed;
            }
            else if (Welcome.Page1.myDouble == 5) // Без фильтра
            {
                xaml.Visibility = Visibility.Visible;
                csharp.Visibility = Visibility.Visible;
                vb.Visibility = Visibility.Visible;
                managedcpp.Visibility = Visibility.Visible;
                xamlcsharp.Visibility = Visibility.Visible;
                xamlvb.Visibility = Visibility.Visible;
            }
            else if (Welcome.Page1.myDouble == 6) // XAML + CSharp
            {
                xaml.Visibility = Visibility.Collapsed;
                csharp.Visibility = Visibility.Collapsed;
                vb.Visibility = Visibility.Collapsed;
                managedcpp.Visibility = Visibility.Collapsed;
                xamlcsharp.Visibility = Visibility.Visible;
                xamlvb.Visibility = Visibility.Collapsed;
            }
            else if (Welcome.Page1.myDouble == 7) // XAML + VB
            {
                xaml.Visibility = Visibility.Collapsed;
                csharp.Visibility = Visibility.Collapsed;
                vb.Visibility = Visibility.Collapsed;
                managedcpp.Visibility = Visibility.Collapsed;
                xamlcsharp.Visibility = Visibility.Collapsed;
                xamlvb.Visibility = Visibility.Visible;
            }
         }

	// Начните здесь вставку любого кода программной части c#. Эти методы обрабатывают события в файлах XAML, их можно переносить из примеров. Может потребоваться обновление модификаторов доступа.

 

    }

    public class Bid : INotifyPropertyChanged
    {
      private string _biditemname = "Unset";
      private decimal _biditemprice = (decimal)0;

      public Bid(string newBidItemName, decimal newBidItemPrice)
      {
        _biditemname = newBidItemName;
        _biditemprice = newBidItemPrice;
      }

      public string BidItemName
      {
        get
        {
          return _biditemname;
        }
        set
        {
          if (_biditemname.Equals(value) == false)
          {
            _biditemname = value;
            // Вызывайте OnPropertyChanged при каждом обновлении свойства
            OnPropertyChanged("BidItemName");
          }
        }
      }

      public decimal BidItemPrice
      {
        get
        {
          return _biditemprice;
        }
        set
        {
          if (_biditemprice.Equals(value) == false)
          {
            _biditemprice = value;
            // Вызывайте OnPropertyChanged при каждом обновлении свойства
            OnPropertyChanged("BidItemPrice");
          }
        }
      }

      // Объявите событие
      public event PropertyChangedEventHandler PropertyChanged;
      // Обработчик события OnPropertyChanged для обновления значения свойства при привязке
      private void OnPropertyChanged(string propName)
      {
        if (PropertyChanged != null)
        {
          PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
      }
    }

    public class BidCollection : ObservableCollection<Bid>
    {
      private Bid item1 = new Bid("Perseus Vase", (decimal)24.95);
      private Bid item2 = new Bid("Hercules Statue", (decimal)16.05);
      private Bid item3 = new Bid("Odysseus Painting", (decimal)100.0);

      private void Timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
      {
        item1.BidItemPrice += (decimal)1.25;
        item2.BidItemPrice += (decimal)2.45;
        item3.BidItemPrice += (decimal)10.55;
      }

      private void CreateTimer()
      {
        System.Timers.Timer Timer1 = new System.Timers.Timer();
        Timer1.Enabled = true;
        Timer1.Interval = 2000;
        Timer1.Elapsed += new System.Timers.ElapsedEventHandler(Timer1_Elapsed);
      }

      public BidCollection()
        : base()
      {
        Add(item1);
        Add(item2);
        Add(item3);
        CreateTimer();
      }
    }


}