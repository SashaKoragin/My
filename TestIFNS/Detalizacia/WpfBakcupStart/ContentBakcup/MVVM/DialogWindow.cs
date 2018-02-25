using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Presentation;
using Prism.Commands;
using TestIFNSTools.Detalizacia.WpfUserControl.ServiceDialod;

namespace TestIFNSTools.Detalizacia.WpfBakcupStart.ContentBakcup.MVVM
{
    /// <summary>
    /// Класс связующий патерн MVVM с технологией MCF
    /// </summary>
   internal class DialogWindow
    {
        
        public Service.Service Service { get; }
        /// <summary>
        /// Переменная для закрытия диалога
        /// </summary>
        private OpenDialogWpf IsOpen { get; }
        /// <summary>
        /// Команда закрыть окно
        /// </summary>
        public DelegateCommand CloseDialog { get;}

        internal DialogWindow(OpenDialogWpf isopen, Service.Service service)
        {
            Service = service;
            IsOpen = isopen;
            CloseDialog = new DelegateCommand((() => IsOpen.OpenAndClose()));
        }
    }
}
