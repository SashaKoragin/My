using Prism.Commands;
using TestIFNSTools.ServiceTestIfns;

namespace TestIFNSTools.Detalizacia.WpfBakcupStart.ContentBakcup.MVVM
{
    /// <summary>
    /// Класс связующий патерн MVVM с технологией MCF
    /// </summary>
   internal class DialogWindow
    {
        /// <summary>
        /// Наш сервис взаимодействия с WCF
        /// </summary>
        public Service.VoidService Service { get; }
        /// <summary>
        /// Команда закрыть окно
        /// </summary>
        public DelegateCommand CloseDialog { get;}

        public DelegateCommand BackupCommand { get; }

        public DelegateCommand Save { get; }

        internal DialogWindow(Service.VoidService service)
        {
            Service = service;
            CloseDialog = new DelegateCommand((() => Service.OpenDialog()));
            BackupCommand = new DelegateCommand((() => Service.Bakcup()));
            Save = new DelegateCommand(()=>Service.Save());
        }
    }
}
