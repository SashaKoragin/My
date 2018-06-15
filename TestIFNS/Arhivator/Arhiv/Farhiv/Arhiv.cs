using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace TestIFNSTools.Arhivator.Arhiv.Farhiv
{
    public class Arhiv
    {
        public Arhivator _owner;

        public Arhiv(Arhivator owner) //Для того чтобы элементы класса Form1 отражались в Arhiv
        {
            _owner = owner;
        }



        internal void ArhivF()
        {
            try
            {
                var way = _owner.textBox2.Text; //Путь куда падают Архивы
                var pathing = _owner.listView1.Items; //Массив переменных путей
                var sovpad = _owner.Sovpad.Text; //Текст совпадения
                var start = _owner.DateStart.Text; //Дата начала поиска
                var finish = _owner.DateFinish.Text; //Дата окончание поиска
                _owner.listView3.Items.Clear(); //Очистить область файлов для новых
                var seath = new SeathFileA.SeathfiletoArhiv(); //Класс для поиска файлов для архивации
                var dialog = new DialogForm.DialogForm(); //Задает Диалоговую форму
                dialog.ShowDialog(); //Открывает Диалоговую форму
                if (dialog.DialogResult != DialogResult.OK) return; //Проверка результата диалога
                var nameFileOtchet = dialog.textBox1.Text + ".xlsx";
                var nameFindFileOtchet = _owner.listView4.FindItemWithText(nameFileOtchet);
                    //Поик совпадений имен в папке

                if (nameFindFileOtchet == null)
                {
                    var files = seath.SeathFile(pathing, sovpad, way, start, finish); //Сам поиск файлов для архивациии
                    if (files.Rows.Count > 0) //Считать строки
                    {
                        _owner.toolStripStatusLabel1.Text = @"Архивируем файлы";
                        _owner.Stat.Value = 0;
                        ArhivirovanieFile.Arxivirovanie.Arhv(files, nameFileOtchet);
                        string path = Path.GetFullPath(Pathing.PathName.Path2);
                        if (Directory.Exists(path))
                        {
                            String[] dirarr = Directory.GetFiles(path, "*.xlsx").Select(Path.GetFileName).ToArray();
                            _owner.listView4.Items.Clear();
                            foreach (String item in dirarr)
                            {
                                _owner.listView4.Items.Add(item);
                            }
                        }
                    }
                    else
                    {
                        _owner.toolStripStatusLabel1.Text = @"Файлы не надены!!!";
                        _owner.toolStripStatusLabel1.ForeColor = Color.Red;
                    }
                }
                else
                {
                    MessageBox.Show(@"Имя файла совпадает с конечным!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
 }