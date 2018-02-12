using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;

namespace TestIFNSTools.Detalizacia.WpfUserControl.Collections.ColectionFilesDbf
{
   public class ListFilesDbf : BindableBase
    {
        private ObservableCollection<ListFilesDbf> _shemesfiles = new ObservableCollection<ListFilesDbf>();
        public ObservableCollection<ListFilesDbf> ShemesFiles
        { get { return _shemesfiles; } }

        private ListFilesDbf _filedbf;
        private Icon   _icon;
        private string _name;
        private string _path;
        private string _fileuse;


        public ListFilesDbf FileDbf
        {
            get { return _filedbf; }
            set { _filedbf = value; }
        }

        public string FileUse
        {
            get { return _fileuse; }
            set { _fileuse = value; }
        }
        public Icon Icon
        {
            get { return _icon; }
            set
            {
                _icon = value;
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
            }
        }

        public string Path
        {
            get { return _path;}
            set
            {
                _path = value; 
            }
        }
    }

    public class ListFileReport : BindableBase
    {
        private ObservableCollection<ListFileReport> _shemesfilesreport = new ObservableCollection<ListFileReport>();
        public ObservableCollection<ListFileReport> ShemesFilesReport
        { get { return _shemesfilesreport; } }

        private ListFileReport _report;
        private Icon _icon;
        private string _name;
        private string _fullpath;
        //public ICommand Rem
        //{
        //    get { return new DelegateCommand(RemoveReportxsl); }
        //}

        //public void RemoveReportxsl()
        //{
        //    MessageBox.Show(Report.Name);
        //}

        public ListFileReport Report
        {
            get { return _report; }
            set { _report = value; }
        }
        public Icon Icon
        {
            get { return _icon; }
            set
            {
                _icon = value;
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
            }
        }

        public string FullPath
        {
            get { return _fullpath; }
            set { _fullpath = value; }
        }


    }
}
