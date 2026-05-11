using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace Fighting
{
 
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            myVideo.Play();
        }

        private void myVideo_MediaEnded(object sender, RoutedEventArgs e)
        {
            myVideo.Position = TimeSpan.Zero;
            myVideo.Play();
        }

        private void exitButton(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void seveHeadButton(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Блок головы");
        }

        private void seveBodyButton(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Блок тела");
        }

        private void seveLedsButton(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Блок ног");
        }

        private void atackHeadButton(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Атака головы");
        }

        private void atackBodyButton(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Атака тела");
        }

        private void atackLedsButton(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Атака ног");
        }

        private void historyFight(string result)
        {
            Console.WriteLine(result);
        }
    }

}
