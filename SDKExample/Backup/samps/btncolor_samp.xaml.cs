using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Diagnostics;
using System.Windows.Media;

namespace ButtonColor //требуется сопоставить страницу XAML
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

         static int newcolor = 0;
         Button btncsharp;
         void OnClick1(object sender, RoutedEventArgs e)
         {
           btn1.Background = Brushes.LightBlue;
         }

         void OnClick2(object sender, RoutedEventArgs e)
         {
           btn2.Background = Brushes.Pink;
         }

         void OnClick3(object sender, RoutedEventArgs e)
         {
           btn1.Background = Brushes.Pink;
           btn2.Background = Brushes.LightBlue;
         }
         void OnClick4(object sender, RoutedEventArgs e)
         {
           switch (newcolor)
           {
             case 0:
               btn4.Background = Brushes.Red;
               btn4.Foreground = Brushes.Beige;
               btn4.Content = "Font Size 10";
               btn4.FontSize = 10;
               break;

             case 1:
               btn4.Background = Brushes.CadetBlue;
               btn4.Foreground = Brushes.LemonChiffon;
               btn4.Content = "Font Size 12";
               btn4.FontSize = 12;
               break;

             case 2:
               btn4.Background = Brushes.Purple;
               btn4.Foreground = Brushes.PeachPuff;
               btn4.Content = "Font Size 14";
               btn4.FontSize = 14;
               break;

             case 3:
               btn4.Background = Brushes.BlanchedAlmond;
               btn4.Foreground = Brushes.DarkRed;
               btn4.Content = "Font Size 16";
               btn4.FontSize = 16;
               break;

             case 4:
               btn4.Background = Brushes.Green;
               btn4.Foreground = Brushes.White;
               btn4.Content = "Font Size 18";
               btn4.FontSize = 18;
               break;
           }
           newcolor = newcolor + 1;
           if (newcolor > 4)
           {
             newcolor = 0;
           }
         }
         void OnClick5(object sender, RoutedEventArgs e)
         {
           btn6.FontSize = 16;
           btn6.Content = "This is my favorite photo.";
           btn6.Background = Brushes.Red;
         }
         void OnClick6(object sender, RoutedEventArgs e)
         {
           btn7.FontSize = 16;
           txt.Text = "You clicked the button.";
           btn7.Background = Brushes.Yellow;
         }
         void OnClick7(object sender, RoutedEventArgs e)
         {
           btncsharp = new Button();
           btncsharp.Content = "Created with C# code.";
           btncsharp.Background = SystemColors.ControlDarkDarkBrush;
           btncsharp.FontSize = SystemFonts.CaptionFontSize;
           cv2.Children.Add(btncsharp);
         }

    }
}