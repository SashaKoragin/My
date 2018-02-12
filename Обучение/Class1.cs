// CommonComponents.Hosting.SplashManager
using System;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;

internal class SplashManager : IDisposable
{
    private sealed class SplashForm : Form
    {
        private Action closeAction;

        private Action cancelAction;

        private PictureBox closeBtn;

        public SplashForm(string splashFileName, double opacity, Action closeAction, Action cancelAction)
        {
            this.closeAction = closeAction;
            this.cancelAction = cancelAction;
            Image image2 = this.BackgroundImage = Image.FromFile(splashFileName);
            this.BackgroundImageLayout = ImageLayout.None;
            base.ClientSize = new Size(image2.Width, image2.Height);
            this.DoubleBuffered = true;
            base.FormBorderStyle = FormBorderStyle.None;
            base.Opacity = opacity;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterScreen;
         //   Bitmap closeSplash = Resources.CloseSplash;
        //    closeSplash.MakeTransparent(Color.White);
            this.closeBtn = new PictureBox();
            this.closeBtn.Size = new Size(16, 16);
         //   this.closeBtn.BackgroundImage = closeSplash;
            this.closeBtn.BackgroundImageLayout = ImageLayout.Stretch;
            this.closeBtn.Location = new Point(image2.Width - this.closeBtn.Width - this.closeBtn.Width / 2, this.closeBtn.Height / 2);
            this.closeBtn.BorderStyle = BorderStyle.FixedSingle;
            this.closeBtn.BackColor = Color.Transparent;
            this.closeBtn.MouseLeave += this.button_MouseMove;
            this.closeBtn.MouseMove += this.button_MouseMove;
            this.closeBtn.Click += this.closeBtn_Click;
            base.Controls.Add(this.closeBtn);
        }

        private void button_MouseMove(object sender, MouseEventArgs e)
        {
            ((Control)sender).BackColor = SystemColors.ControlLight;
        }

        private void button_MouseMove(object sender, EventArgs e)
        {
            ((Control)sender).BackColor = Color.Transparent;
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.closeAction();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.cancelAction();
        }
    }

    private bool disposed;

    private SplashForm splashForm;

    private Action cancelAction;

    private readonly ManualResetEvent showOrFailed = new ManualResetEvent(false);

    public void ShowSplash(Action cancelAction)
    {
        this.cancelAction = cancelAction;
        if (!Debugger.IsAttached)
        {
            AppDomain.CurrentDomain.SetData("splashStart", DateTime.Now);
            Thread thread = new Thread(this.SplashThread);
            thread.Name = "SplashStrategy_ShowSplash";
            thread.IsBackground = true;
            Thread thread2 = thread;
            thread2.Start();
            this.showOrFailed.WaitOne(TimeSpan.FromSeconds(10.0));
        }
    }

    public void HideSplash()
    {
        if (this.splashForm != null)
        {
            Action method = delegate
            {
                this.splashForm.Close();
            };
            this.splashForm.Invoke(method);
            this.splashForm = null;
            AppDomain.CurrentDomain.SetData("splashStop", DateTime.Now);
        }
    }

    private void SplashThread()
    {
        try
        {
            string splashFileName = default(string);
            if (SplashManager.TryGetSplashFileName(out splashFileName))
            {
                using (this.splashForm = new SplashForm(splashFileName, SplashManager.GetOpacity(), this.HideSplash, this.cancelAction))
                {
                    this.splashForm.HandleCreated += delegate
                    {
                        this.showOrFailed.Set();
                    };
                    this.splashForm.ShowDialog();
                }
            }
        }
        finally
        {
            this.showOrFailed.Set();
            this.splashForm = null;
        }
    }

    private static bool TryGetSplashFileName(out string fileName)
    {
        fileName = null;
        string text = ConfigurationManager.AppSettings["splash-image"];
        if (object.ReferenceEquals(text, null))
        {
            return false;
        }
        if (!Path.IsPathRooted(text))
        {
            text = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, text);
        }
        if (!File.Exists(text))
        {
            return false;
        }
        fileName = text;
        return true;
    }

    private static double GetOpacity()
    {
        string text = ConfigurationManager.AppSettings["splash-opacity"];
        double num = default(double);
        if (!string.IsNullOrEmpty(text) && double.TryParse(text, NumberStyles.Float, (IFormatProvider)CultureInfo.InvariantCulture, out num))
        {
            if (!(num < 0.0) && !(num > 1.0))
            {
                return num;
            }
            num = 1.0;
            return num;
        }
        num = 1.0;
        return num;
    }

    void IDisposable.Dispose()
    {
        if (!this.disposed)
        {
            this.HideSplash();
            this.disposed = true;
        }
    }
}