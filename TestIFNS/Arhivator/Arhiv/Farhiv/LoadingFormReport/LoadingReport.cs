using System;
using System.IO;
using System.Linq;

namespace TestIFNSTools.Arhivator.Arhiv.Farhiv.LoadingFormReport
{
    /// <summary>
    /// Загрызка отчетов Report при загрузке приложения
    /// </summary>
   public class LoadingReport
    {
        /// <summary>
        /// Пути где файлы для архивации
        /// </summary>
        /// <param name="path">Путь</param>
        /// <param name="list">Элемент хранения</param>
        public void ReportPath(string path, System.Windows.Forms.ListView list)
        {
            String[] dirarr = Directory.GetFiles(path, "*.txt").Select(Path.GetFileName).ToArray();
            list.Items.Clear();
            foreach (String item in dirarr)
            {
                list.Items.Add(item);
            }
        }
        /// <summary>
        /// Путь QBE DAtE
        /// </summary>
        /// <param name="path">Путь</param>
        /// <param name="list">Элемент хранения</param>
        public void ReportQbedate(string path, System.Windows.Forms.ListView list)
        {
            String[] dirarr = Directory.GetFiles(path, "*.txt").Select(Path.GetFileName).ToArray();
            list.Items.Clear();
            foreach (String item in dirarr)
            {
                list.Items.Add(item);
            }
        }
        /// <summary>
        /// Загрузка отчетов 
        /// </summary>
        /// <param name="path">Путь </param>
        /// <param name="list">Элемент хранения</param>
        public void ReportExcel(string path, System.Windows.Forms.ListView list)
        {
            String[] dirarr = Directory.GetFiles(path, "*.xlsx").Select(Path.GetFileName).ToArray();
            list.Items.Clear();
            foreach (String item in dirarr)
            {
                list.Items.Add(item);
            }
        }
       /// <summary>
       /// Отчеты по квитациям
       /// </summary>
       /// <param name="path">Путь </param>
       /// <param name="list">Элемент хранения</param>
        public void ReportKvOtchet(string path, System.Windows.Forms.ListView list)
        {
            String[] dirarr = Directory.GetFiles(path, "*.xlsx").Select(Path.GetFileName).ToArray();
            list.Items.Clear();
            foreach (String item in dirarr)
            {
                list.Items.Add(item);
            }
        }
    }
}
