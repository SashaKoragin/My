using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Diagnostics;
using System.Windows.Media;

namespace ControlProperties //требуется сопоставить страницу XAML
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



         string str;
         FontFamily ffamily;
         Double fsize;

         // Чтобы использовать PaneLoaded, поместите Loaded="PaneLoaded" в корневой элемент файла .xaml.
         // private void PaneLoaded(object sender, EventArgs e) {}
         // Обработчик событий примера:  
         void ChangeBackground(object sender, RoutedEventArgs e)
         {
           if (btn.Background == Brushes.Red)
           {
             btn.Background = new LinearGradientBrush(Colors.LightBlue, Colors.SlateBlue, 90);
             btn.Content = "Background";
           }
           else
           {
             btn.Background = Brushes.Red;
             btn.Content = "Control background changes from red to a blue gradient.";
           }
         }
         void ChangeForeground(object sender, RoutedEventArgs e)
         {
           if (btn1.Foreground == Brushes.Green)
           {
             btn1.Foreground = Brushes.Black;
             btn1.Content = "Foreground";
           }
           else
           {
             btn1.Foreground = Brushes.Green;
             btn1.Content = "Control foreground(text) changes from black to green.";
           }
         }
         void ChangeFontFamily(object sender, RoutedEventArgs e)
         {
           ffamily = btn2.FontFamily;
           str = ffamily.ToString();
           if (str == ("Arial Black"))
           {
             btn2.FontFamily = new FontFamily("Arial");
             btn2.Content = "FontFamily";
           }
           else
           {
             btn2.FontFamily = new FontFamily("Arial Black");
             btn2.Content = "Control font family changes from Arial to Arial Black.";

           }
         }
         void ChangeFontSize(object sender, RoutedEventArgs e)
         {
           fsize = btn3.FontSize;
           if (fsize == 16.0)
           {
             btn3.FontSize = 10.0;
             btn3.Content = "FontSize";
           }
           else
           {
             btn3.FontSize = 16.0;
             btn3.Content = "Control font size changes from 10 to 16.";
           }
         }
         void ChangeFontStyle(object sender, RoutedEventArgs e)
         {
           if (btn4.FontStyle == FontStyles.Italic)
           {
             btn4.FontStyle = FontStyles.Normal;
             btn4.Content = "FontStyle";
           }
           else
           {
             btn4.FontStyle = FontStyles.Italic;
             btn4.Content = "Control font style changes from Normal to Italic.";
           }
         }
         void ChangeFontWeight(object sender, RoutedEventArgs e)
         {
           if (btn5.FontWeight == FontWeights.Bold)
           {
             btn5.FontWeight = FontWeights.Normal;
             btn5.Content = "FontWeight";
           }
           else
           {
             btn5.FontWeight = FontWeights.Bold;
             btn5.Content = "Control font weight changes from Normal to Bold.";
           }
         }
         void ChangeBorderBrush(object sender, RoutedEventArgs e)
         {
           str = ((btn6.BorderBrush).ToString());
           if (btn6.BorderBrush == Brushes.Red)
           {
             btn6.BorderBrush = Brushes.Black;
             btn6.Content = "BorderBrush";

           }
           else
           {
             btn6.BorderBrush = Brushes.Red;
             btn6.Content = "Control border brush changes from red to black.";
           }
         }
	         
    }
}