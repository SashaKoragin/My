using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using TestIFNSTools.Detalizacia.WpfUserControl.Collections.ColectionFilesDbf;

namespace TestIFNSTools.Detalizacia.WpfUserControl.SobytieAndCommandContext
{
   internal class ContextCommand
    {
        public void RemoveReport(ListFileReport reportxsl,ObservableCollection<ListFileReport> reportcolection )
        {
            File.Delete(reportxsl.FullPath);
            reportcolection.Remove(reportxsl);
        }

        public void OpenReport(ListFileReport reportxsl)
        {
           var openfile = new OpenFile.OpenFile();
           openfile.Openxls(reportxsl.FullPath);
        }
    }

    internal class SobytieReport
    {
        public void MoveCopy(object parametr , ListFileReport file)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                var objectiv = (ListView)parametr;
                var files = new String[1];
                files[0] = file.FullPath;
                var data = new DataObject();
                data.SetData(DataFormats.FileDrop, files);
                DragDrop.DoDragDrop(objectiv, data, DragDropEffects.Copy);
            }
        }

        public void OpenReportEvent(ListFileReport reportxsl)
        {
          
                var openfile = new OpenFile.OpenFile();
                openfile.Openxls(reportxsl.FullPath);
        }
    }

    internal class SobytieDbfFile
    {
        public void MoveCopyDbf(object parametr, ListFilesDbf file)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                var objectiv = (ListView)parametr;
                var files = new String[1];
                files[0] = file.Path;
                var data = new DataObject();
                data.SetData(DataFormats.FileDrop, files);
                DragDrop.DoDragDrop(objectiv, data, DragDropEffects.Copy);
            }
        }

        public void OpenDbfEvent(ListFilesDbf reportxsl)
        {

            var openfile = new OpenFile.OpenFile();
            openfile.Openxls(reportxsl.Path);
        }
    }
}
