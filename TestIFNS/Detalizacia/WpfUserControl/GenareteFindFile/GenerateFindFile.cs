using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
namespace TestIFNSTools.Detalizacia.WpfUserControl.GenareteFindFile
{
   public class GenerateFindFile
    {
        /// <summary>
        /// Переменная для хранения найденных файлов
        /// </summary>
        public ObservableCollection<FileInfo[]> ColectionFile = new ObservableCollection<FileInfo[]>();
        /// <summary>
        /// Класс для раскладки таблиц и поиска файлов по следующим добавлением его ав переменную
        /// </summary>
        /// <param name="tablesql">Выборка SQL содержит путь и имена файлов</param>
        /// <param name="detal">Наша основная WinForm для отражение в ней ошибок</param>
        /// <param name="yers">Данная переменнная служит для генерации путей на MI файлы</param>
        /// <returns>Возвращаем переменную ColectionFile</returns>
        public ObservableCollection<FileInfo[]> GeheratePath(DataSet tablesql, Detalizacia detal, string yers)
        {
            foreach (DataTable table in tablesql.Tables)
            {
                if (table.Columns.Count == 2)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        AddFile(row.ItemArray[0].ToString().Trim(), row.ItemArray[1].ToString().Trim());
                    }
                }
                else
                {
                    foreach (DataRow row in table.Rows)
                    {
                        AddFile(row.ItemArray[0].ToString().Trim(), row.ItemArray[1].ToString().Trim());
                        if (!string.IsNullOrWhiteSpace(row.ItemArray[2].ToString()))
                        {  //Надо попробовать раскладывать оринтируясь на ячейку t1.Автомат если 1 то dirresulauto если 0 то dirresul!!!!
                            DirectoryInfo dirresul = new DirectoryInfo(String.Format(Arhivator.Pathing.PathMiNdfl.PathResult, yers));
                            DirectoryInfo dirresulauto = new DirectoryInfo(String.Format(Arhivator.Pathing.PathMiNdfl.PathResultAuto, yers));
                            ColectionFile.Add(dirresul.GetFiles("*" + row.ItemArray[2].ToString().Trim() + "*", SearchOption.AllDirectories));
                            ColectionFile.Add(dirresulauto.GetFiles("*" + row.ItemArray[2].ToString().Trim() + "*", SearchOption.AllDirectories));
                        }
                        else
                        {
                            detal?.BeginInvoke(new MethodInvoker(delegate
                            {
                                detal.ErrorMI.Items.Add($@"На файл {row.ItemArray[1].ToString().Trim()} Отсутствует MI");
                            }));
                        }
                    }
                }
            }
            var colectionfileinfo = new ObservableCollection<FileInfo[]>(ColectionFile.Where(filecolection => filecolection.Length != 0));
            return colectionfileinfo;
        }
        /// <summary>
        /// Генерация файлов ЮЛ и ФЛ но не MI
        /// </summary>
        /// <param name="path">Путь к файлу из таблицы</param>
        /// <param name="file">Имя файла из таблицы</param>
        public void AddFile(string path, string file)
        {
            if (!string.IsNullOrWhiteSpace(path) && !string.IsNullOrWhiteSpace(file))
            {
                    DirectoryInfo dir = new DirectoryInfo(path);
                    ColectionFile.Add(dir.GetFiles("*" + file + "*", SearchOption.AllDirectories));
            }
        }
    }
}
