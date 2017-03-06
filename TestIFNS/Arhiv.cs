using System;
using System.Data;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;
using ICSharpCode.SharpZipLib.Zip;

namespace TestIFNSTools
{
    public class Arhiv
    {
        Form1 _owner;
        public Arhiv(Form1 owner)  //Для того чтобы элементы класса Form1 отражались в Arhiv
        {
            _owner = owner;
        }

        public void arhiv()
        {
            DialogForm ll = new DialogForm();
            ll.ShowDialog();
            if (ll.DialogResult == DialogResult.OK)
            {
                    ListViewItem a = _owner.listView4.FindItemWithText(ll.textBox1.ToString() + ".xlsx");
                    if (a == null)
                    {
                        var i = 1;
                        List<string> files = new List<string>();
                        List<Tuple<string, string, string, string>> otchet = new List<Tuple<string, string, string, string>>(); //Для отчета создание динамического
                        foreach (ListViewItem str in _owner.listView1.Items)
                        {
                            if (Directory.Exists(str.Text))
                            {

                                String[] dirarr = Directory.GetFiles(str.Text, _owner.Sovpad.Text).Select(x => Path.GetFullPath(x)).ToArray();

                                foreach (string file in dirarr)
                                {
                                    var date = File.GetLastWriteTime(file).Date;

                                    if (DateTime.Parse(_owner.DateStart.Text) <= DateTime.Parse(date.ToString()) && DateTime.Parse(_owner.DateFinish.Text) >= DateTime.Parse(date.ToString()))
                                    {
                                        files.Add(file);
                                    }
                                }
                            }
                        }
                        if (files.Count > 0)
                        {
                            float Proc = (100 / files.Count);
                            foreach (var f in files)
                            {
                                ListViewItem oth = new ListViewItem(new string[] { i.ToString(), File.GetLastWriteTime(f).Date.ToString("dd.MM.yyyy"), Path.GetFileName(f).ToString(), Path.GetFullPath(f).ToString() });
                                otchet.Add(new Tuple<string, string, string, string>(i.ToString(), File.GetLastWriteTime(f).Date.ToString("dd.MM.yyyy"), Path.GetFileName(f).ToString(), Path.GetFullPath(f).ToString()));
                                _owner.listView3.Items.Add(oth);
                                //Архивирование 2НДФЛ
                                var filename = _owner.textBox2.Text + "\\7751_2NDFLNA_" + File.GetLastWriteTime(f).Date.ToString("ddMMyyyy") + ".zip";
                                if (!File.Exists(filename))
                                {
                                    using (var zip = File.Open(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                                    {
                                        using (ZipOutputStream zipToWrite = new ZipOutputStream(zip))
                                        {
                                            zipToWrite.UseZip64 = UseZip64.Off;
                                            using (FileStream newFileStream = File.OpenRead(f))
                                            {
                                                byte[] byteBuffer = new byte[newFileStream.Length - 1];
                                                newFileStream.Read(byteBuffer, 0, byteBuffer.Length);
                                                ZipEntry entry = new ZipEntry(Path.GetFileName(f));
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
                                else
                                {
                                        using (var zipAdd = File.Open(filename, FileMode.Open, FileAccess.ReadWrite))
                                        {
                                            using (ZipFile zip1 = new ZipFile(zipAdd))
                                            {
                                                zip1.BeginUpdate();
                                                zip1.Add(Path.GetFullPath(f), Path.GetFileName(f));
                                                zip1.CommitUpdate();
                                                zip1.Close();
                                            }
                                            zipAdd.Close();
                                        }
                                }
                                    i++;
                                    _owner.backgroundWorker1.ReportProgress((int)(Proc * 100.0f));  //В нее можно только integer (не double с плавующей точкой) подумать!!!!! 
                             }
                                _owner.label5.Text = "Количество файлов:  " + files.Count;
                                var workbook = new XLWorkbook();
                                var worksheet = workbook.Worksheets.Add("Отчет отработаных.");
                                worksheet.Cells("A1").Value = otchet.ToArray();
                                workbook.SaveAs(PathName.path2 + ll.textBox1.Text.ToString() + ".xlsx");
                                string path = Path.GetFullPath(PathName.path2);
                                if (Directory.Exists(path))
                                {
                                    String[] dirarr = Directory.GetFiles(path, "*.xlsx").Select(x => Path.GetFileName(x)).ToArray();
                                    _owner.listView4.Items.Clear();
                                    foreach (String item in dirarr)
                                    {
                                        _owner.listView4.Items.Add(item);
                                    }
                                }


                                else
                                {
                                    _owner.label5.Text = "Файлы не надены!!!";

                                }
                            }
                        }
                    
                    else
                    {
                        MessageBox.Show("Имя файла совпадает с конечным!");
                    }
            }
        }
    }
}    
