using System;
using System.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;
using ClosedXML.Excel;
using GalaSoft.MvvmLight.Threading;
using TestIFNSTools.Detalizacia.WpfUserControl.Collections.ColectionFilesDbf;
using TestIFNSTools.Detalizacia.WpfUserControl.Collections.ColectionYers;
using TestIFNSTools.Detalizacia.WpfUserControl.Collections.PanelSqlZap;
using TestIFNSTools.Detalizacia.WpfUserControl.Trigers;

namespace TestIFNSTools.Detalizacia.WpfUserControl.Logica
{
   public class Logica
    {
        
        public Detalizacia Detal { get; set; }
        public static BackgroundWorker WorkerUl = new BackgroundWorker();
        public static BackgroundWorker WorkerFl = new BackgroundWorker();
        public TrigersUse Triger { get; set; }
        public SelectPanelUl Ul { get; set; }
        public SelectPanelFl Fl { get; set; }
        public YearsDbf Yers { get; set; }
        public ListFilesDbf ListFile { get; set; }
        public ListFileReport Report { get; set; }
        public Collections.ColectionTab.TabControl Tab { get; set; }
        public DataSet TableSqlUl { get; set; }
        public DataSet TableSqlFl { get; set; }
        public static ObservableCollection<FileInfo[]> CollectionFileUl { get; set; }
        public static ObservableCollection<FileInfo[]> CollectionFileFl { get; set; }
        public void Go(Detalizacia detal, TrigersUse trigerselect, SelectPanelUl ul, SelectPanelFl fl,YearsDbf years, ListFilesDbf filedbf, Collections.ColectionTab.TabControl tab,ListFileReport report)
        {
            Triger = trigerselect;
            Ul = ul;
            Fl = fl;
            Yers = years;
            Detal = detal;
            ListFile = filedbf;
            Report = report;
            Tab = tab;
            if (Yers.IsValidation())
            {
                if (Triger.IsCheked)
                {
                    if (!Fl.IsValidation())
                    {
                        Triger.IsEnableButtonFl = false;
                        WorkerFl.WorkerReportsProgress = true;
                        WorkerFl.WorkerSupportsCancellation = true;
                        WorkerFl.DoWork += worker_DoworkFL;
                        WorkerFl.ProgressChanged += worker_progressChangeFL;
                        WorkerFl.RunWorkerCompleted += worker_RunWorkerCompleteFL;
                        WorkerFl.RunWorkerAsync();
                    }
                }
                else
                {
                    if (!Ul.IsValidation())
                    {
                        Triger.IsEnableButtonUl = false;
                        WorkerUl.WorkerReportsProgress = true;
                        WorkerUl.WorkerSupportsCancellation = true;
                        WorkerUl.DoWork += worker_DoworkUL;
                        WorkerUl.ProgressChanged += worker_progressChangeUL;
                        WorkerUl.RunWorkerCompleted += worker_RunWorkerCompleteUL;
                        WorkerUl.RunWorkerAsync();
                    }
                }
            }
        }

        /// <summary>
        /// Процес по ЮЛ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void worker_DoworkUL(object sender, DoWorkEventArgs e)
        {
            try
            {
            DispatcherHelper.Initialize();
            var logica = new Face.GroupReportTable.AnyUlOnFlReport();
            var workbookreport = new XLWorkbook();
            var colection = new AddColection.AddColection();
            var selectzapros = new Face.UL.SqlParamSelect.Zapros();
            var filedbf = new Face.UL.File.Files.AddFile();
            Detal.Invoke(new MethodInvoker(delegate { Detal.StatusUl.Text = @"Собираем выборки!!!"; }));
            TableSqlUl = selectzapros.ZaprosSql(Ul.InnUl, Yers.SelectYears.Years, Detal);
            Detal.Invoke(new MethodInvoker(delegate { Detal.StatusUl.Text = @"Собираем файлы!!!"; }));
            CollectionFileUl = filedbf.AddF(Ul.InnUl,Yers.SelectYears.Years, Detal);
            Detal.Invoke(new MethodInvoker(delegate { Detal.StatusUl.Text = @"Формируем таблицы!!!"; }));
            TableSqlUl = logica.Generatexsls(TableSqlUl, workbookreport,Arhivator.Pathing.PathName.Path4 + Ul.InnUl + "_" + Yers.SelectYears.Years);
            DispatcherHelper.CheckBeginInvokeOnUI(() => { 
                Dispatcher.CurrentDispatcher.Invoke(()=>
                {
                    ListFile.UpdateOn();
                    Tab.UpdateOn();
                    Report.UpdateOn();
                        Task.Run(async () =>
                        {
                            await Task.Run(() =>
                            {
                                try
                                {
                                    colection.FilesDbf(CollectionFileUl, ListFile, Ul.InnUl);
                                    logica.GenereteReport(TableSqlUl, Tab);
                                    colection.UpdateReport(Report);
                                    ListFile.UpdateOff();
                                    Tab.UpdateOff();
                                    Report.UpdateOff();
                                }
                                catch (Exception exception)
                                {
                                    System.Windows.Forms.MessageBox.Show(exception.ToString());
                                }
                            });

                        });
                    });
                });
            Detal.BeginInvoke(new MethodInvoker(() => WorkerUl.CancelAsync()));}
            catch (Exception exception)
            {
                System.Windows.Forms.MessageBox.Show(exception.ToString());
            }
        }

        internal void worker_progressChangeUL(object sender, ProgressChangedEventArgs e)
        {
            Detal.StatusBarUL.Value += e.ProgressPercentage;
        }
        /// <summary>
        /// Завершение процесса по ЮЛ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void worker_RunWorkerCompleteUL(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
               WorkerUl.DoWork -= worker_DoworkUL;
               WorkerUl.ProgressChanged -= worker_progressChangeUL;
               WorkerUl.RunWorkerCompleted -= worker_RunWorkerCompleteUL;
               Detal.StatusUl.Text = @"Завершено!!!";
               Detal.StatusBarUL.Value = 10000;
               Detal.StatusBarUL.Value = 0;
               Triger.IsEnableButtonUl = true;
            }
        }

        /// <summary>
        /// Процесс по ФЛ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void worker_DoworkFL(object sender, DoWorkEventArgs e)
        {
            try
            {
            var logica = new Face.GroupReportTable.AnyUlOnFlReport();
            var workbookreport = new XLWorkbook();
            var selectzaprosfl = new Face.FL.SqlParamSelect.ZaprosFl();
            var colection = new AddColection.AddColection();
            var filedbffl = new Face.FL.File.SqlParamFile.AddFileFl();
            Detal.Invoke(new MethodInvoker(delegate { Detal.StatusFl.Text = @"Собираем выборки!!!"; }));
            var parametr = selectzaprosfl.GenerateParam(Fl.InnFl, Fl.SeriaNomerPasport, Fl.Familia, Fl.Name,Fl.MiddleName);
            TableSqlFl = selectzaprosfl.ZaprosSql(parametr, Yers.SelectYears.Years, Detal);
            Detal.Invoke(new MethodInvoker(delegate { Detal.StatusFl.Text = @"Собираем файлы!!!"; }));
            CollectionFileFl = filedbffl.AddF(Yers.SelectYears.Years,parametr, Detal);
            Detal.Invoke(new MethodInvoker(delegate { Detal.StatusFl.Text = @"Формируем таблицы!!!"; }));
            TableSqlFl = logica.Generatexsls(TableSqlFl, workbookreport, Arhivator.Pathing.PathName.Path4 + Fl.InnFl + "_" + Fl.SeriaNomerPasport + "_" + Fl.Familia + "_" + Yers.SelectYears.Years);
            DispatcherHelper.CheckBeginInvokeOnUI(() => {
                    Dispatcher.CurrentDispatcher.Invoke(() =>
                    {
                        ListFile.UpdateOn();
                        Tab.UpdateOn();
                        Report.UpdateOn();
                        Task.Run(async () =>
                        {
                            await Task.Run(() =>
                            {
                                try
                                {
                                    colection.FilesDbf(CollectionFileFl, ListFile, Fl.InnFl + "_" + Fl.SeriaNomerPasport + "_" + Fl.Familia);
                                    logica.GenereteReport(TableSqlFl, Tab);
                                    colection.UpdateReport(Report);
                                    ListFile.UpdateOff();
                                    Tab.UpdateOff();
                                    Report.UpdateOff();
                                }
                                catch (Exception exception)
                                {
                                    System.Windows.Forms.MessageBox.Show(exception.ToString());
                                }
                            });

                        });
                    });
                });
            Detal.BeginInvoke(new MethodInvoker(() => WorkerFl.CancelAsync()));
            }
            catch (Exception exception)
            {
                System.Windows.Forms.MessageBox.Show(exception.ToString());
            }
        }

        internal void worker_progressChangeFL(object sender, ProgressChangedEventArgs e)
        {
           Detal.StatusBarFl.Value += e.ProgressPercentage;
        }
       /// <summary>
       /// Завершение процесса по ФЛ
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void worker_RunWorkerCompleteFL(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {

                WorkerFl.DoWork -= worker_DoworkFL;
                WorkerFl.ProgressChanged -= worker_progressChangeFL;
                WorkerFl.RunWorkerCompleted -= worker_RunWorkerCompleteFL;
                Detal.StatusFl.Text = @"Завершено!!!";
                Detal.StatusBarFl.Value = 10000;
                Detal.StatusBarFl.Value = 0;
                Triger.IsEnableButtonFl = true;
            }
        }

    }
}
