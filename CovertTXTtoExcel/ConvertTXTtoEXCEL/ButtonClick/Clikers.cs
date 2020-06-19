using System.Collections.ObjectModel;
using System.IO;
using CovertTXTtoExcel.ConvertTXTtoEXCEL.Convert;
using CovertTXTtoExcel.ConvertTXTtoEXCEL.Errors;
using CovertTXTtoExcel.ConvertTXTtoEXCEL.ExcelFile;
using CovertTXTtoExcel.ConvertTXTtoEXCEL.WPFFORM;
using System.Windows;
using CovertTXTtoExcel.ConvertTXTtoEXCEL.AddItem;
using Microsoft.Win32;
using OfficeOpenXml;
using System.ComponentModel;

namespace CovertTXTtoExcel.ConvertTXTtoEXCEL.ButtonClick
{
   public class SelectionFile
    {
        public MainWindow P2;
        public SelectionFile(MainWindow owner)  //Конструктор для формы xaml
        {
          P2 = owner;
          P2.DataContext = this;
        }

        public BackgroundWorker Worker = new BackgroundWorker();

        internal void ToGo()
        {
            Worker.WorkerReportsProgress = true;
            Worker.WorkerSupportsCancellation = true;
            Worker.DoWork += worker_DoworkConvertFile;
            Worker.ProgressChanged += worker_progressChange;
            Worker.RunWorkerCompleted += worker_RunWorkerComplete;
            P2.AddFiles.IsEnabled = false;
            P2.CnFiles.IsEnabled = false;
            Worker.RunWorkerAsync();
        }

        internal void LoadPathExcel()
        {
            var i = new ZnExcel {FileExcels = new ObservableCollection<ZnExcel>()};
            if (Directory.Exists(PathOtchet.Configuration.PathOtchet))
            {
                var di = new DirectoryInfo(PathOtchet.Configuration.PathOtchet);
                var fiArr = di.GetFiles();
                foreach (var item in fiArr)
                {
                    i.FileExcels.Add(new ZnExcel {Icons = IconsCreate.Icondll.Extract(item.FullName), Namefile = item.Name, Sizen = item.Length });
                }
                P2.Otcet.Dispatcher.Invoke(() => P2.Otcet.ItemsSource = i.FileExcels); 
            }
        }
        internal void AddFile(object sender, RoutedEventArgs e)
        {
            var i = new ZnachView {File = new ObservableCollection<ZnachView>()};
            var win = new OpenFileDialog { Filter = "Файлы txt|*.txt|Файлы crv|*crv|Файлы tsv|*tsv", Multiselect = true };
            if (win.ShowDialog() == true)
            {
                foreach ( string file in win.FileNames)
                {
                    i.File.Add(new ZnachView{ NameFile = Path.GetFileNameWithoutExtension(file), PathFile = Path.GetFullPath(file)});
                }
                P2.ListFile.Dispatcher.Invoke(() => P2.ListFile.ItemsSource = i.File); 
            }
        }
        internal void worker_DoworkConvertFile(object sender, DoWorkEventArgs e)
        {
            var i = (ErrTriger)P2.Resources["ErrTriger"];
            var convertfull = new Convettxt();
            P2.Status.Dispatcher.Invoke(() => P2.Status.Content = @"Проверяем параметры!!!");
            if (!Err.FileAddError(P2.ListFile) || !Err.FileError(P2.ListFile.Dispatcher.Invoke(() =>P2.ListFile.SelectedItems.Count), i)|| !Err.FileScalarError(P2.Scalare))
            {
                 Worker.CancelAsync();
            }
            else
            {
                using (var excelPackage = new ExcelPackage(new FileInfo(PathOtchet.Configuration.PathOtchet + GenereteName.GenerateName.Generatenamedowhile())))
                {
                    var proc = (100.0f / P2.ListFile.Dispatcher.Invoke(() => P2.ListFile.SelectedItems.Count));
                    P2.Status.Dispatcher.Invoke(() => P2.Status.Content = @"Конвертируем!!!");
                    if (P2.Scalare.Dispatcher.Invoke(() => P2.Scalare.Text != ""))
                    {
                        
                        foreach (ZnachView item in P2.ListFile.Dispatcher.Invoke(() => P2.ListFile.SelectedItems))
                        {
                            Worker.ReportProgress((int) (proc * 100.0f));
                            convertfull.ConvtxtScalar(item, excelPackage, P2.Scalare.Dispatcher.Invoke(() => P2.Scalare.Text));
                        }
                    }
                    else
                    {
                        foreach (ZnachView item in P2.ListFile.Dispatcher.Invoke(()=>P2.ListFile.SelectedItems))
                        {
                            Worker.ReportProgress((int) (proc * 100.0f));
                            convertfull.ConvtxtFull(item, excelPackage);
                        }
                    }
                    excelPackage.Save();
                    excelPackage.Dispose();
                    LoadPathExcel();
                }
            }
        }
        internal void worker_progressChange(object sender, ProgressChangedEventArgs e)
        {
            P2.Progress.Value = +e.ProgressPercentage;
        }
        private void worker_RunWorkerComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            P2.Status.Dispatcher.Invoke(() => P2.Status.Content = @"Закончили!!!");
            P2.Progress.Value=1000;
            P2.Progress.Value = 0;
            P2.AddFiles.IsEnabled = true;
            P2.CnFiles.IsEnabled = true;
        }
    }
}
