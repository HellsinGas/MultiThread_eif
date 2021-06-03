using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _02_Networking_Receiver_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Receiver receiver = null;
        bool isCancelRequested = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            isCancelRequested = false;
            Thread receiverThread = new Thread(() =>
            {
                receiver = new Receiver(Environment.ExpandEnvironmentVariables(@"F:\MULTITHREAD4TESTSPACE\DownloadFolder\"));
                receiver.Start();
            });

            receiverThread.Start();

            Thread uiThread = new Thread(() =>
            {
                while (true)
                {
                    this.Dispatcher.Invoke(() => statusLabel.Content = Receiver.Message);
                    Thread.Sleep(100);
                    if (isCancelRequested)
                        break;
                }
            });

            uiThread.Start();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            if (receiver != null)
                receiver.Stop();
            isCancelRequested = true;
        }


    }
}
