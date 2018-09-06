using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Ifns51.Risk;
using LibaryXMLAutoReports.ReportsBdk;

namespace EfDatabaseLogica.AddTable.AddOutBdk
{
    public class AddOutBdk : IDisposable
    {
        public RisksContext Risk { get; set; }
        public Guid Guid { get; set; }
        public AddOutBdk()
        {
            Risk = new RisksContext();
        }

        public void SaveFile(string path, int numdoc)
        {
            try
            {
                byte[] bytefile;
                FileInfo file = new FileInfo(path);
                using (FileStream mem = new FileStream(path, FileMode.Open))
                {
                    bytefile = new byte[mem.Length];
                    mem.Read(bytefile, 0, bytefile.Length);
                }
                var docum = new WordDocument()
                {
                    Document = bytefile,
                    IdNamedocument = numdoc,
                    Namefile = file.Name,
                    TypeFile = Path.GetExtension(path),
                    Numerdocument = Guid = Guid.NewGuid()
                };
                Risk.WordDocuments.Add(docum);
                Risk.SaveChanges();
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);

            }
        }
        public void SaveReestr(FN71 fn)
        {
            try
            {
                List<UseTableTemplateBdk> reestr = new List<UseTableTemplateBdk>();
            foreach (var fn17232 in fn.FN1723_2)
            {
                reestr.Add(new UseTableTemplateBdk()
                {
                        D85 = fn17232.D85,
                        D981 = fn17232.D981,
                        N279 = fn.N279,
                        N280 = fn.N280,
                        Numerdocument = Guid
                });
            }
            Risk.UseTableTemplateBdks.AddRange(reestr);
            Risk.SaveChanges();
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
        }
        /// <summary>
        /// Вытаскиваем Файл с БД
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private WordDocument FileSave(Guid num)
        {
            var file = Risk.WordDocuments.Where(
                    numgui => numgui.Numerdocument == num)
                .Select(numgui=>numgui).FirstOrDefault();
            return file;
        }
        /// <summary>
        /// Загружаем файл в поток
        /// </summary>
        /// <param name="num">Guid файла</param>
        /// <returns></returns>
        public Stream DonloadFileOutBdk(Guid num)
        {
                var file = FileSave(num);
                return new MemoryStream(file.Document);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Risk?.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
