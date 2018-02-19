using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Threading;
using DocumentFormat.OpenXml.Spreadsheet;
using TestIFNSTools.Detalizacia.WpfUserControl.Collections.ColectionFilesDbf;
using TestIFNSTools.Detalizacia.WpfUserControl.Collections.ColectionYers;

namespace TestIFNSTools.Detalizacia.WpfUserControl.AddColection
{
    class AddColection
    {
        /// <summary>
        ///  Добавление года в колекцию.
        /// </summary>
        /// <returns></returns>
        public YearsDbf Years()
        {
            YearsDbf years = new YearsDbf();
            years.Shemedyears.Add(new YearsDbf { years = "2012" });
            years.Shemedyears.Add(new YearsDbf { years = "2013" });
            years.Shemedyears.Add(new YearsDbf { years = "2014" });
            years.Shemedyears.Add(new YearsDbf { years = "2015" });
            years.Shemedyears.Add(new YearsDbf { years = "2016" });
            years.Shemedyears.Add(new YearsDbf { years = "2017" });
            years.Shemedyears.Add(new YearsDbf { years = "2018" });
            return years;
        }
        public ListFileReport Report()
        {
            ListFileReport report = new ListFileReport(); //ListFileReport listfilereport
            report.ShemesFilesReport.Clear();
            if (Directory.Exists(Arhivator.Pathing.PathName.Path4))
            {
                var dir = new DirectoryInfo(Arhivator.Pathing.PathName.Path4);
                var files = dir.GetFiles();
                foreach (FileInfo file in files)
                {
                    report.ShemesFilesReport.Add(new ListFileReport { Icon = IconsDetalization.Icons.Extracticonfile(file.FullName), Name = file.Name, FullPath = file.FullName });
                }
            }
            return report;
        }

        public void UpdateReport(ListFileReport report)
        {

            report.ShemesFilesReport.Clear();
            lock (report._lock)
            {
            if (Directory.Exists(Arhivator.Pathing.PathName.Path4))
            {
                var dir = new DirectoryInfo(Arhivator.Pathing.PathName.Path4);
                var files = dir.GetFiles();
                foreach (FileInfo file in files)
                {
                    report.ShemesFilesReport.Add(new ListFileReport { Icon = IconsDetalization.Icons.Extracticonfile(file.FullName), Name = file.Name, FullPath = file.FullName });
                }
            }
            }
        }


        public void FilesDbf(ObservableCollection<FileInfo[]> files,ListFilesDbf dbffiles,string usefile)
        {
                    lock (dbffiles._lock)
                    {
                        foreach (var fileInfose in files)
                        {
                            foreach (var fileInfo in fileInfose)
                            {
                                dbffiles.ShemesFiles.Add(new ListFilesDbf { Icon = IconsDetalization.Icons.Extracticonfile(fileInfo.FullName), Name = fileInfo.Name, Path = fileInfo.FullName, FileUse = usefile });
                            }
                        }
                    }
        }
    }
}
