using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data;

namespace TestIFNSTools.Arhivator.Arhiv.Farhiv.SeathFileA
{
    internal class SeathfiletoArhiv
    {
        internal DataTable SeathFile(ListView.ListViewItemCollection pathing, string sovpad,string way, string start, string finish)
        {
            var formarhiv = ((Arhivator)Application.OpenForms["Arhivator"]);
            var files = new List<FileInfo>();
            var datetimefile = new List<string>();
            formarhiv?.BeginInvoke(new MethodInvoker(() => formarhiv.toolStripStatusLabel1.Text = @"Собираем файлы для архивации!!!"));
            foreach (var dirarr in from ListViewItem str in pathing select Directory.GetFiles(str.Text, sovpad).Select(Path.GetFullPath).ToArray())
            {
                var proc = (100.0f / dirarr.Length);
                foreach (var file in dirarr)
                {
                    var date = File.GetLastWriteTime(file).Date.ToString("dd.MM.yyyy");
                    formarhiv?.BeginInvoke(new MethodInvoker(delegate{formarhiv.backgroundWorker1.ReportProgress((int) (proc * 100.0f));}));
                    if ((DateTime.Parse(start) <= DateTime.Parse(date)&&DateTime.Parse(finish) >= DateTime.Parse(date)))
                    {
                        files.Add(new FileInfo(file));  //Формируем массив файлов
                        datetimefile.Add(date);
                    }
                }
                formarhiv?.BeginInvoke(new MethodInvoker(delegate { formarhiv.Stat.Value = 0; }));
            }
            var fileList = files.OrderBy(f => f.LastWriteTime).ToList();
            var unit =new List<string>(datetimefile.Distinct());
            var dates = unit.Select(DateTime.Parse).ToList();
            dates.Sort();
            var fileDataSet = FileToPath(fileList, dates, way);
            return fileDataSet;
        }

        static DataTable FileToPath(IEnumerable<FileInfo> file, IList<DateTime> datetList, string way)
        {
            var i = 1;
            var dt1 = new DataTable("FILEOFPATH");
            dt1.Columns.Add("Имя файла(полный путь)");
            dt1.Columns.Add("Архив содержания");
            foreach (var f in file)
            {
                foreach (var daten in datetList)
                {
                    var date = File.GetLastWriteTime(f.FullName).Date.ToString("dd.MM.yyyy");
                    if (daten == DateTime.Parse(date))
                    {
                       var filename = NameF(i, way,f.FullName);
                       dt1.Rows.Add(f.FullName, filename);
                       break;
                    }
                    else
                    {
                       i++;
                       var filename = NameF(i, way, f.FullName);
                       datetList.RemoveAt(0);
                       dt1.Rows.Add(f.FullName, filename);
                       break;
                    }
                }
            }
            return dt1;
        }

        static String NameF(int i,string way,string file)
        {
            if (i>=10)
            {
                var filename = way + @"\7751_2NDFLNA" + File.GetLastWriteTime(file).Year + "_" + File.GetLastWriteTime(file).Date.ToString("ddMMyyyy") + "_" + i + ".zip";
              return filename;
            }
            else
            {
                var filename = way + @"\7751_2NDFLNA" + File.GetLastWriteTime(file).Year + "_" + File.GetLastWriteTime(file).Date.ToString("ddMMyyyy") + "_0" + i + ".zip";
              return filename;
            }
        }


    }
}