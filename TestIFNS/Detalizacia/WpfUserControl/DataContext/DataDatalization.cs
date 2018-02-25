﻿using System.Windows.Threading;
using Prism.Commands;
using System.Windows.Input;
using TestIFNSTools.Detalizacia.WpfBakcupStart.XamlForm;
using TestIFNSTools.Detalizacia.WpfUserControl.Collections.ColectionFilesDbf;
using TestIFNSTools.Detalizacia.WpfUserControl.Collections.ColectionYers;
using TestIFNSTools.Detalizacia.WpfUserControl.Collections.PanelSqlZap;
using TestIFNSTools.Detalizacia.WpfUserControl.ServiceDialod;
using TestIFNSTools.Detalizacia.WpfUserControl.Trigers;
using TabControl = TestIFNSTools.Detalizacia.WpfUserControl.Collections.ColectionTab.TabControl;

namespace TestIFNSTools.Detalizacia.WpfUserControl.DataContext
{
   public class DataDatalization
    {
        /// <summary>
        /// Колекция годов.
        /// </summary>
        public YearsDbf Years { get; }
        /// <summary>
        /// Привязка к панели ЮЛ для дальнейшей проверки.
        /// </summary>
        public SelectPanelUl PanelUl { get; }
        /// <summary>
        /// Привязка к панели ФЛ для дальнейшей проверки.
        /// </summary>
        public SelectPanelFl PanelFl { get; }
        /// <summary>
        /// Привязка к тригеру для переключения между панелями.
        /// </summary>
        public TrigersUse Trigers { get; }
        /// <summary>
        /// Комманда для переключения.
        /// </summary>
        public DelegateCommand IsSelectedFace { get; } 
        /// <summary>
        /// Привязка ListFiles к файлам.
        /// </summary>
        public ListFilesDbf ListFilesDbf { get; }
        /// <summary>
        /// Привязка к отчету Report
        /// </summary>
        public ListFileReport ListReport { get; }
        /// <summary>
        /// Команда Поиск
        /// </summary>
        public DelegateCommand Seath { get; }
        /// <summary>
        /// Созданый интерфейс для таблиц Генерится в Автомате в общем классе
        /// </summary>
        public TabControl Tab { get; }
       // public System.Windows.Controls.Button Butt { get; }
       /// <summary>
       /// Комманда для удаления отчета
       /// </summary>
        public ICommand RemoveReport { get; }
        /// <summary>
        /// Комманда для открытия отчета
        /// </summary>
        public ICommand OpenReport { get; }
        /// <summary>
        /// Событие перетягивания Отчета Report
        /// </summary>
        public ICommand FileDropDrapReport { get; }
        /// <summary>
        /// Команда открыть Report для двойного клика
        /// </summary>
        public DelegateCommand FileOpenEvent { get; }
        /// <summary>
        /// Перетащить найденные файлы с сервера
        /// </summary>
        public DelegateCommand<object> FileDropDrapDbf { get; }
        /// <summary>
        /// Открыть найденные файлы на сервере
        /// </summary>
        public DelegateCommand FileOpenDbf { get; }
        /// <summary>
        /// Окно для работы с диалогом Bakcup класс статический для удобства вызова
        /// </summary>
        public DelegateCommand OpenDialogWcfBackup { get; }
        /// <summary>
        /// Диалоговое окно
        /// </summary>
        public ControlBakcup Dialog { get; }
        /// <summary>
        /// Открыто ли Диалоговое окно
        /// </summary>
        public OpenDialogWpf OpenDialog { get; }

        public WpfBakcupStart.ContentBakcup.Service.Service Service { get; }
        /// <summary>
        /// Наш MVVM Патерн
        /// </summary>
        /// <param name="detal"></param>
        public DataDatalization(Detalizacia detal)
        {
            
            var logic = new Logica.Logica();
            var contextlogic = new SobytieAndCommandContext.ContextCommand();
            var sobytie = new SobytieAndCommandContext.SobytieReport(); 
            var sobytiedbf = new SobytieAndCommandContext.SobytieDbfFile();
            Service = new WpfBakcupStart.ContentBakcup.Service.Service();
            OpenDialog = new OpenDialogWpf();
            Dialog = new ControlBakcup(OpenDialog,Service);
            Years = new AddColection.AddColection().Years();
            PanelUl = new SelectPanelUl();
            PanelFl = new SelectPanelFl();
            Trigers = new TrigersUse();
            Tab = new TabControl();
            IsSelectedFace = new DelegateCommand(() => Trigers.IsSelectFace());
            ListFilesDbf = new ListFilesDbf();
            ListReport = new AddColection.AddColection().Report();
            Seath = new DelegateCommand(()=>Dispatcher.CurrentDispatcher.Invoke(() => logic.Go(detal,Trigers,PanelUl,PanelFl,Years,ListFilesDbf,Tab,ListReport)));
            RemoveReport = new DelegateCommand(() => contextlogic.RemoveReport(ListReport.Report,ListReport.ShemesFilesReport));
            OpenReport = new DelegateCommand(()=>contextlogic.OpenReport(ListReport.Report));
            FileDropDrapReport = new DelegateCommand<object>(parameter=>sobytie.MoveCopy(parameter,ListReport.Report));
            FileOpenEvent = new DelegateCommand(()=>sobytie.OpenReportEvent(ListReport.Report));
            FileOpenDbf = new DelegateCommand((() => sobytiedbf.OpenDbfEvent(ListFilesDbf.FileDbf)));
            FileDropDrapDbf = new DelegateCommand<object>(parameter=>sobytiedbf.MoveCopyDbf(parameter,ListFilesDbf.FileDbf));
            OpenDialogWcfBackup =new DelegateCommand((() => OpenDialog.OpenAndClose(Service)));
        }
    }
}
