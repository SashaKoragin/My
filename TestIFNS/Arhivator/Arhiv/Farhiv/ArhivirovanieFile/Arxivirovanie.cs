using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using ClosedXML.Excel;
using ICSharpCode.SharpZipLib.Zip;


namespace TestIFNSTools.Arhivator.Arhiv.Farhiv.ArhivirovanieFile
{
   internal class Arxivirovanie
    {
        internal static void Arhv(DataTable files,string fileNameOthcet)
        {
            try
            {
            var i = 1;
            
            var formarhiv = ((Arhivator)Application.OpenForms["Arhivator"]);
            var otchet = new List<Tuple<string, string, string, string>>(); //Для отчета Excel
            var proc = (100.0f / files.Rows.Count);
            foreach (DataRow f in files.Rows)
            {
                var oth = new ListViewItem( new[]
                    {
                        i.ToString(CultureInfo.InvariantCulture),
                        File.GetLastWriteTime(f.ItemArray[0].ToString()).Date.ToString("dd.MM.yyyy"),
                        Path.GetFileNameWithoutExtension(f.ItemArray[0].ToString()),
                        Path.GetFullPath(f.ItemArray[0].ToString())
                    }); //Для динамического отчета listview3       
                otchet.Add(new Tuple<string, string, string, string>(i.ToString(CultureInfo.InvariantCulture),
                    File.GetLastWriteTime(f.ItemArray[0].ToString()).Date.ToString("dd.MM.yyyy"),
                    Path.GetFileNameWithoutExtension(f.ItemArray[0].ToString()),
                    Path.GetFullPath(f.ItemArray[0].ToString()))); //Для Excel заполнение
                formarhiv?.BeginInvoke(new MethodInvoker(() => formarhiv.listView3.Items.Add(oth)));
                var filename = f.ItemArray[1].ToString();
                if (!File.Exists(filename))
                  {
                      ArhNoPath(filename,f);
                  }
                else
                  {
                      ArhYesPath(filename,f);
                  }
                i++;
                formarhiv?.BeginInvoke(new MethodInvoker(delegate{formarhiv.backgroundWorker1.ReportProgress((int)(proc * 100.0f));}));
            }
            formarhiv?.BeginInvoke(new MethodInvoker(() => formarhiv.toolStripStatusLabel1.Text = @"Количество отработаных файлов:  "+files.Rows.Count));
            Otchet(otchet, fileNameOthcet);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }


       /// <summary>
        /// Данный метод ArhNoPath преднозначен для создания Архива и добавления в него файла
       /// </summary>
       /// <param name="arhName"></param> Имя создаваемого Архива
       /// <param name="file"></param>    Файл добавляемый
       internal static void ArhNoPath(string arhName,DataRow file)
       {
         using (var zip = File.Open(arhName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
              {
                using (var zipToWrite = new ZipOutputStream(zip))
                 {
                    zipToWrite.UseZip64 = UseZip64.Off;
                       using (FileStream newFileStream = File.OpenRead(file.ItemArray[0].ToString()))
                        {
                           var byteBuffer = new byte[newFileStream.Length - 1];
                           newFileStream.Read(byteBuffer, 0, byteBuffer.Length);
                           var entry = new ZipEntry(Path.GetFileName(file.ItemArray[0].ToString()));
                           zipToWrite.PutNextEntry(entry);
                           zipToWrite.Write(byteBuffer, 0, byteBuffer.Length);
                           entry.Size = byteBuffer.Length;
                           zipToWrite.CloseEntry();
                           newFileStream.Close();
                         }
                      zipToWrite.Close();
                      zipToWrite.Finish();
                }
                   zip.Close();
             } 
       }

       /// <summary>
       /// Данный метод  ArhYesPath преднозначен для добавление в архив файла
       /// </summary>
       /// <param name="arhName"></param>  Параметр Имени Архива
       /// <param name="file"></param>     Параметр добавляемого файла
       internal static void ArhYesPath(string arhName, DataRow file)  
       {
           using (var zipAdd = File.Open(arhName, FileMode.Open, FileAccess.ReadWrite))
           {
            using (var zip1 = new ZipFile(zipAdd))
              {
               zip1.BeginUpdate();
               zip1.Add(Path.GetFullPath(file.ItemArray[0].ToString()), Path.GetFileName(file.ItemArray[0].ToString()));
               zip1.CommitUpdate();
               zip1.Close();
              }
           zipAdd.Close();
         }
       }
        /// <summary>
        /// Данный метод  Otchet создает отчет по тработаным файлам
        /// </summary>
        /// <param name="othet">Параметр содержимого отчета</param>     
        /// <param name="nameFile">Имя отчета задается пользователем</param>  
        /// <param name="filereport">Файл отчета для генерации</param>
        internal static  void Otchet(List<Tuple<string, string, string, string>> othet, string nameFile)
       {
           try
           {
               var workbook = new XLWorkbook();
               var worksheet = workbook.Worksheets.Add("Отчет отработаных.");
               worksheet.Cells("A1").Value = othet.ToArray();
               worksheet.Cell("A1").InsertData(othet.ToArray()).Worksheet.Columns().AdjustToContents();
               workbook.SaveAs(Pathing.PathName.Path2 + nameFile);
           }
           catch (Exception ex)
           {
               MessageBox.Show(ex.Message);
           }

       }
    }
}

