using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CovertTXTtoExcel.ConvertTXTtoEXCEL.AddItem;
using CovertTXTtoExcel.ConvertTXTtoEXCEL.ExcelFile;

namespace CovertTXTtoExcel.ConvertTXTtoEXCEL.Events
{
   public static class EventsSob
    {
        public static void OpenFile(object sender, ListView otchet, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                if (otchet.Focusable)
                {
                    var item = (ZnExcel)otchet.SelectedItems[0];
                    Opens.OpenFile.Openxls(PathOtchet.Configuration.PathOtchet + item.NameFile);
                }
           }
        }

        public static void OpenFileTxt(object sender, ListView otchettxt, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                if (otchettxt.Focusable)
                {
                    var item = (ZnachView)otchettxt.SelectedItems[0];
                    Opens.OpenFile.Openxls(item.PathFile);
                }
            }
        }
    }
}
