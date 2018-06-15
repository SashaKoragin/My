using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using TestIFNSTools.Detalizacia.WpfBakcupStart.XamlForm.DialogsModel;
using TestIFNSTools.ServiceTestIfns;

namespace TestIFNSTools.Detalizacia.WpfBakcupStart.ContentBakcup.Service
{
    /// <summary>
    /// ����� ��� ������ � ����� ��������
    /// </summary>
    public class Service : Prism.Mvvm.BindableBase
    {

        private string _workDb;
        private string _serviceDb;
        private string _timebakcup;
        private BakcupJurnal[] _Jurnal;
        /// <summary>
        /// ������ ��������� �����
        /// </summary>
        public BakcupJurnal[] Jurnal
        {
            get { return _Jurnal; }
            set
            {
                _Jurnal = value;
                RaisePropertyChanged();
            }
        }
        /// <summary>
        /// ����� ����� �������� �����
        /// </summary>
        public string Timebakcup
        {
            get
            {
                return _timebakcup;
            }
            set
            {
                _timebakcup = value;
                RaisePropertyChanged();
            }
        } 

        /// <summary>
        /// ������� ��
        /// </summary>
        public string WorkDb
        {
            get { return _workDb; }
            set
            {
                _workDb = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// �� �� �������
        /// </summary>
        public string ServiceDb
        {
            get { return _serviceDb; }
            set
            {
                _serviceDb = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// ������� �� ���������� ���������� �����������
        /// </summary>
        private bool _isactiv;

        /// <summary>
        /// ���� ����� ���������� ���������� �����������
        /// </summary>
        private DateTime _dateBakcup;

        /// <summary>
        /// ������� ������ �� ������
        /// </summary>
        private bool _isopendialog;

        /// <summary>
        /// ���� ����� ���������� ���������� �����������
        /// </summary>
        public DateTime DateBakcup
        {
            get { return _dateBakcup; }
            set
            {
                _dateBakcup = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// ������� �� ���������� ���������� �����������
        /// </summary>
        public bool IsActive
        {
            get { return _isactiv; }
            set
            {
                _isactiv = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// ������� ������ �� ������
        /// </summary>
        public bool IsOpenDialog
        {
            get { return _isopendialog; }
            set
            {
                _isopendialog = value;
                RaisePropertyChanged();
            }
        }
    }

    public class VoidService : Service
    {

        /// <summary>
        /// ������� �������� ������� � ��������� ������ � �������
        /// </summary>
        public void SelectDialog(string name, DialogsModel  dialog)
        {
            var dialogselect = dialog.Dialogmodel.ToArray().Single(namedialog => namedialog.Name == name);
            dialog.Dialog = dialogselect;
            OpenDialog();
        }

        public void OpenDialog()
        {
            if (IsOpenDialog)
            {
                IsOpenDialog = false;
            }
            else
            {
                IsOpenDialog = true;
                Update();
            }
        }

        /// <summary>
        /// ��������� ������ � �������
        /// </summary>
        public void Update()
        {
            ReaderCommandDbfClient service =new ReaderCommandDbfClient("BasicHttpBinding_IReaderCommandDbf");
            DateBakcup = service.DateBakcup();
            IsActive = service.IsActive();
            WorkDb = service.Config().WorkDB;
            ServiceDb = service.Config().TestDB;
            Timebakcup = string.Format("{0}:{1}", service.Config().Hours, service.Config().Minutes);
            Jurnal = service.Jurnal();

        }

        public void Bakcup()
        {
            ReaderCommandDbfClient service = new ReaderCommandDbfClient("BasicHttpBinding_IReaderCommandDbf");
            Task task = new Task((() => { service.FileBakcup(); }));
            task.Start();
            IsActive = false;
        }

        public void Save()
        {
            var time = Timebakcup.Split(Char.Parse(":"));
            ReaderCommandDbfClient service = new ReaderCommandDbfClient("BasicHttpBinding_IReaderCommandDbf");
            service.SaveSetingAsync(ServiceDb, WorkDb, Convert.ToInt32(time[0]), Convert.ToInt32(time[1]));
        }

    }
}