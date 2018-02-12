using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using MaterialDesignThemes.Wpf;
using MyWPF.ServiceReference1;

namespace MyWPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
          //  DataContext = new Dil();
        }



        //private void Button_OnClick(object sender, RoutedEventArgs e)
        //{
        //    var xValue = int.Parse(TextBox.Text);
        //    var yValue = int.Parse(TextBox1.Text);

        //   // var client = new Summator.SummatorClient("BasicHttpBinding_ISummator");
        //   // if (client.State. == CommunicationState.Closed)
        //    //{

        //    //    Label.Content = "Рщые закрыт!!!! Запустите службу!!!";


        //    //}
        //    //else
        //    //{
        //  //      Label.Content = client.GetSumm(xValue, yValue).ToString();
        //   //     client.Close();
        //  //  }             

        //}

        //private void Button_OnClick1(object sender, RoutedEventArgs e)
        //{
        //    var cli = new ServiceLotusNotesClient("BasicHttpBinding_IServiceLotusNotes");  // new ServiceLotusNotesClient("BasicHttpBinding_IServiceLotusNotes");
        //    cli.Open();
        //    cli.IntrfaceLitus();           
        //    //var client = new Summator.SummatorClient("BasicHttpBinding_ISummator");
        //    //client.Ddd();
        //}
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                //Task.Run(() =>
                //{
                //    Thread thread = new Thread(() =>
                //   {
                //       var client = new ServiceLotusNotesClient("BasicHttpBinding_IServiceLotusNotes");
                //       client.IntrfaceLitus();
                //   });
                //    thread.SetApartmentState(ApartmentState.STA); //This is essential
                //    thread.Start();
                //    thread.Join();
                //});
                //    Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                //        (ThreadStart)delegate ()
                //    {

                //if (Dispatcher.CheckAccess())
                //{
                //    //var client = new ServiceLotusNotesClient("BasicHttpBinding_IServiceLotusNotes");

                //if (Dispatcher.Thread.Equals(Thread.CurrentThread))
                //{

                //    var client = new ServiceLotusNotesClient("BasicHttpBinding_IServiceLotusNotes");

                //    Application.Current.Dispatcher.Invoke((Action) delegate { client.IntrfaceLitus(); });

                //}
                //else
                //{

                //    var client = new ServiceLotusNotesClient("BasicHttpBinding_IServiceLotusNotes");
                //    //client.IntrfaceLitusAsync();
                //    Application.Current.Dispatcher.Invoke(() => client.IntrfaceLitus());
                //}
                // create a thread  
                Thread newWindowThread = new Thread(new ThreadStart(() =>
                {
                    var client = new ServiceLotusNotesClient("BasicHttpBinding_IServiceLotusNotes");
                    client.IntrfaceLitus();
                    Dispatcher.Run();
                }));
                newWindowThread.SetApartmentState(ApartmentState.STA);
                newWindowThread.IsBackground = true;
                newWindowThread.Start();
            }
            catch (Exception ev)
            {
                MessageBox.Show(ev.ToString());
            }
        }


    }

    //public class Dil
    //{
    //    public LinkCommutator[] LinkZg { get; set; }

    //    public Dil()
    //    {
    //        LinkZg = Did();
    //    }


    //    [STAThread]
    //    public LinkCommutator[] Did()
    //    {
    //        try
    //        {
    //           // Thread thread = new Thread(() =>
    //           //{
    //           //    try
    //            var client = new ServiceLotusNotesClient("BasicHttpBinding_IServiceLotusNotes");
    //            return client.Links();
    //            //ystem.Windows.Threading.Dispatcher.Run(); //Perhaps use this for a longer running Window, but not needed for this issue
    //         //       }
    //         //       catch (Exception e)
    //          //      {
    //         //           MessageBox.Show(e.ToString());
    //           //    }
    //          //  });

    //         //    thread.SetApartmentState(ApartmentState.STA); //This is essential
    //        //    thread.Start();
    //         //   thread.Join();
    //            // TheWindowHostingTheControl.Dispatcher.Invoke(or BeginInvoke, or one of the AsyncInvokes), passing in a delegate to the code that instances your control.that will cause the control to be created on the same thread that the host window has affinity for.

    //        }
    //        catch
    //            (Exception e)
    //        {

    //            MessageBox.Show(e.ToString());
    //        }
    //        return null;
    //    }
    //}
}

