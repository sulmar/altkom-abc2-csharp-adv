using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async Task Button_Click(object sender, RoutedEventArgs e)
        {
            // await SendAsync();

            //Thread thread = new Thread(Send);
            //thread.Start();

            
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            await SendAsync();
        }

        private  async Task SendAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(5));
        }
    }
}
