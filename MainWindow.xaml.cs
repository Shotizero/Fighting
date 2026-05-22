using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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
using static System.Net.Mime.MediaTypeNames;

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

        private void seveButton(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Блок");
        }   

        private void definitions(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string bodyParts = button.Tag.ToString();
            
            Random random = new Random();
            int damage = random.Next(5, 16);
            if(random.Next(1, 101) <= 5)
            {
                damage = 50;                
            }
            healthEnemy(damage);
            historyFight(bodyParts, damage);
            endGame();
        }        

        private void historyFight(string bodyParts, int damage)
        {
            if (damage >= 50)
            {
                textOutput($"Противник получает КРИТИЧЕСКИЙ урона в {bodyParts}");
            }else
                textOutput($"Противник получает {damage} урона в {bodyParts}");              
        }

        private void textOutput(string text)
        {
            Text.AppendText(text + Environment.NewLine);
            Text.ScrollToEnd();
        }

        private void healthPlayer(int damage)
        {

        }

        private void blockHP()
        {

        }

        private void enemy()
        {
            
        }
        
        int healthEn = 100;
        int healthPl = 100;
        private void healthEnemy(int damage)
        {            
            healthEn = (healthEn - damage);
            EnemyHP.Value = healthEn;
        }
        
        private void endGame()
        {
            if(healthEn <= 0 || healthPl <= 0)
            {
                MessageBox.Show("Игра окончена");                
            }
        }

    }

}
