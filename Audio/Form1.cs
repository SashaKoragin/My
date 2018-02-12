using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using NAudio.FileFormats.Mp3;
using System.Windows.Forms;
using NAudio.Wave;
using NAudio.Lame;
using NAudio.Gui;

namespace Audio
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }
       
        

        

        // WaveIn - поток для записи
        BufferedWaveProvider _bf;
        WaveIn _www;
        WaveOut _out;
        //Класс для записи в файл
        WaveFileWriter _writer;
        //Имя файла для записи
   
        public string OutputFilename
        {
            get
            {
                return  textBox.Text.Trim();
            }
            set
            {
                textBox.Text = value;
            }
        }


        //Получение данных из входного буфера 
        void waveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (InvokeRequired)
            {
                
                BeginInvoke(new EventHandler<WaveInEventArgs>(waveIn_DataAvailable), sender, e);
            }
            else
            {
                //Записываем данные из буфера в файл
                _writer.Write(e.Buffer, 0, e.BytesRecorded);
                
            }
        }
        //Завершаем запись
        void StopRecording()
        {
            MessageBox.Show(@"StopRecording");
            _www.StopRecording();
        }
        //Окончание записи
        private void waveIn_RecordingStopped(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new EventHandler(waveIn_RecordingStopped), sender, e);
            }
            else
            {
                _www.Dispose();
                _www = null;
                _writer.Close();
                _writer.Dispose();
                try
                {

                        using (var reader = new AudioFileReader(_writer.Filename))
                        {
                            using (var write = new LameMP3FileWriter(@"D:\MP3\gg.mp3", reader.WaveFormat, 128))
                            {

                                reader.CopyTo(write);
                                write.Flush();
                            }
                            reader.Flush();
                        }
                  
                    
                }
                catch (Exception ev)
                {
                    MessageBox.Show(ev.Message);
                }
            }
        }
        //Начинаем запись
        private void button1_Click(object sender, EventArgs e)  //Запись в    файл
        {
            try
            {
                MessageBox.Show(@"Start Recording");
                _www = new WaveIn {DeviceNumber = 0};
                _www.DataAvailable += waveIn_DataAvailable;
                _www.RecordingStopped += waveIn_RecordingStopped;
                _www.WaveFormat = new WaveFormat(16000,2);
                _writer = new WaveFileWriter(OutputFilename,_www.WaveFormat);
                _www.StartRecording();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //Прерываем запись

        private void button2_Click_1(object sender, EventArgs e) //Сброс ресурсов
        {
            if (_www != null)
            {
                StopRecording();

            }
        }

        private void button3_Click(object sender, EventArgs e) //Выход на колонки
        {
            var win = new WaveIn();
            if (_out != null)
            {
                _out.Stop();
                _out.Dispose();
                _out = null;
                win.Dispose();
            }
            else
            {
                _out = new WaveOut();
                win.DataAvailable += wi_DataAvailable;
                _bf = new BufferedWaveProvider(win.WaveFormat);
                _bf.DiscardOnBufferOverflow = true;
                _out.Init(_bf);
                win.StartRecording();
                _out.Play();
                
   
            }
            
        }

        void wi_DataAvailable(object sender, WaveInEventArgs e)
        {
            _bf.AddSamples(e.Buffer, 0, e.BytesRecorded);

        }
    }
}
