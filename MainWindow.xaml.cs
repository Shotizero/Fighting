using System;
using System.Windows;
using System.Windows.Controls;

namespace Fighting
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            myVideo.Play();

            MenuBorder.Visibility = Visibility.Visible;
            GameGrid.Visibility = Visibility.Collapsed;
            MenuAnimation.Play();
            AttackEnabled(false);
        }

        private void MyVideo_MediaEnded(object sender, RoutedEventArgs e)
        {
            myVideo.Position = TimeSpan.Zero;
            myVideo.Play();
        }
        private void MenuPlayButton_Click(object sender, RoutedEventArgs e)
        {
            MenuBorder.Visibility = Visibility.Collapsed;
            MenuAnimation.Stop();
            GameGrid.Visibility = Visibility.Visible;
            myVideo.Play();

            healthPl = 100;
            healthEn = 100;
            PlayerHP.Value = 100;
            EnemyHP.Value = 100;
            lastPlayerAttack = "None";
            lastPlayerBlock = "None";
            lastEnemyBlock = "None";
            savedPlayer = "None";
            playerHealUsed = false;
            enemyHealUsed = false;
            HealButton.Visibility = Visibility.Collapsed;
            EndGameMenu.Visibility = Visibility.Collapsed;
            Text.Clear();

            AttackEnabled(false);
            SaveEnabled(true);
        }
        private void Menu_MediaEnded(object sender, RoutedEventArgs e)
        {
            MenuAnimation.Position = TimeSpan.Zero;
            MenuAnimation.Play();
        }
        private void ExitButton(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SeveButton(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string bodyPartsSave = button.Tag.ToString();

            if (bodyPartsSave == lastPlayerBlock)
            {
                MessageBox.Show($"Нельзя защищать {bodyPartsSave} два раза подряд! Выберите другую зону.");
                return;
            }

            DefinitionSeve(bodyPartsSave);
            TextOutput($"Вы приготовились защищать: {bodyPartsSave}");

            SaveEnabled(false);
            AttackEnabled(true);
        }
        private void HealButton_Click(object sender, RoutedEventArgs e)
        {
            PlayerHeal();
        }

        Random random = new Random();

        private void Definitions(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string bodyParts = button.Tag.ToString();

            if (bodyParts == lastPlayerAttack)
            {
                MessageBox.Show($"Нельзя атаковать {bodyParts} два раза подряд! Выберите другую зону.");
                return;
            }

            int damage = random.Next(5, 16);
            if (random.Next(1, 101) <= 5)
            {
                damage = 50;
            }

            string name = "Ваш персонаж";
            ChooseEnemyBlock();
            if (bodyParts == currentEnemyBlock)
            {
                damage = (int)(damage * (1 - 0.50));
                TextOutput($"Противник заблокировал ваш удар в {bodyParts}");
            }
            HealthEnemy(damage);
            HistoryFight(bodyParts, damage, name);
            DefinitionEnemy();

            lastPlayerAttack = bodyParts;
            lastPlayerBlock = savedPlayer;
            lastEnemyBlock = currentEnemyBlock;

            EndGame();

            if (healthEn > 0 && healthPl > 0)
            {
                AttackEnabled(false);
                SaveEnabled(true);
            }
        }

        private void HistoryFight(string bodyParts, int damage, string name)
        {
            if (damage >= 50)
            {
                TextOutput($"{name} наносит КРИТИЧЕСКИЙ урона в {bodyParts}");
            }
            else
                TextOutput($"{name} наносит {damage} урона в {bodyParts}");
        }

        private void TextOutput(string text)
        {
            Text.AppendText(text + Environment.NewLine);
            Text.ScrollToEnd();
        }

        private string DefinitionEnemy()
        {
            EnemyDamage = random.Next(5, 16);
            string[] bodyPartsArray = { "Head", "Body", "Legs" };
            int bodyParts = random.Next(0, bodyPartsArray.Length);
            AttackZone = bodyPartsArray[bodyParts];
            if (random.Next(1, 101) <= 5)
            {
                EnemyDamage = 50;
            }
            string name = "Противник";
            SaveSystem(savedPlayer);
            HistoryFight(AttackZone, EnemyDamage, name);

            if (!enemyHealUsed && healthEn < 50 && healthEn > 0)
            {
                EnemyHeal();
            }

            return AttackZone;
        }

        private void DefinitionSeve(string bodyPartsSave)
        {
            savedPlayer = bodyPartsSave;
        }

        private void SaveSystem(string bodyPartsSave)
        {
            if (AttackZone == bodyPartsSave)
            {
                int finalDamage = (int)(EnemyDamage * (1 - 0.50));
                TextOutput("Вы заблокировали удар противника");
                HealthPlayer(finalDamage);
            }
            else
                HealthPlayer(EnemyDamage);
        }
        private void PlayerHeal()
        {
            if (playerHealUsed) return;
            if (healthPl >= 100) return;

            int healAmount = random.Next(20, 41);
            healthPl += healAmount;
            if (healthPl > 100) healthPl = 100;
            PlayerHP.Value = healthPl;

            TextOutput($"Вы восстановили {healAmount} HP!");
            playerHealUsed = true;
            HealButton.Visibility = Visibility.Collapsed;
        }
        private void EnemyHeal()
        {
            if (enemyHealUsed) return;
            if (healthEn > 50) return;

            int healAmount = random.Next(20, 41);
            healthEn += healAmount;
            if (healthEn > 100) healthEn = 100;
            EnemyHP.Value = healthEn;

            TextOutput($"Противник восстановил {healAmount} HP!");
            enemyHealUsed = true;
        }

        bool playerHealUsed = false;
        bool enemyHealUsed = false;
        string lastPlayerAttack = "None";
        string lastPlayerBlock = "None";
        string lastEnemyBlock = "None";
        string currentEnemyBlock = "None";
        string savedPlayer = "None";
        string AttackZone = "None";
        int EnemyDamage = 0;
        int healthEn = 100;
        int healthPl = 100;

        private void HealthPlayer(int damage)
        {
            healthPl -= damage;
            if (healthPl < 0) healthPl = 0;
            PlayerHP.Value = healthPl;

            if (!playerHealUsed && healthPl < 100 && healthPl > 0)
            {
                HealButton.Visibility = Visibility.Visible;
            }
        }

        private void HealthEnemy(int damage)
        {
            healthEn = (healthEn - damage);
            if (healthEn < 0) healthEn = 0;
            EnemyHP.Value = healthEn;
        }

        private void AttackEnabled(bool isEnabled)
        {
            if (AttackHead != null) AttackHead.IsEnabled = isEnabled;
            if (AttackBody != null) AttackBody.IsEnabled = isEnabled;
            if (AttackLegs != null) AttackLegs.IsEnabled = isEnabled;
        }
        
        private void SaveEnabled(bool isEnabled)
        {
            Button[] saveButtons = { SaveHead, SaveBody, SaveLegs };

            foreach (Button btn in saveButtons)
            {
                if (btn == null) continue;

                if (isEnabled)
                {
                    btn.IsEnabled = true;
                }
                else
                {
                    btn.IsEnabled = false;
                }
            }
        }

        private void EndGame()
        {
            if (healthEn <= 0 || healthPl <= 0)
            {
                if (healthEn <= 0)
                {
                    EndGameText.Text = "ПОБЕДА!";
                }
                else if (healthPl <= 0)
                {
                    EndGameText.Text = "ПОРАЖЕНИЕ!";
                }

                EndGameMenu.Visibility = Visibility.Visible;

                AttackEnabled(false);
                SaveEnabled(false);

                HealButton.Visibility = Visibility.Collapsed;
            }
        }
        private void PlayAgain_Click(object sender, RoutedEventArgs e)
        {
            EndGameMenu.Visibility = Visibility.Collapsed;

            healthPl = 100;
            healthEn = 100;
            PlayerHP.Value = 100;
            EnemyHP.Value = 100;

            lastPlayerAttack = "None";
            lastPlayerBlock = "None";
            lastEnemyBlock = "None";
            savedPlayer = "None";
            currentEnemyBlock = "None";
            AttackZone = "None";
            EnemyDamage = 0;
            playerHealUsed = false;
            enemyHealUsed = false;
            HealButton.Visibility = Visibility.Collapsed;

            Text.Clear();

            AttackEnabled(false);
            SaveEnabled(true);

            TextOutput("=== НОВЫЙ БОЙ ===");
        }
        private void BackToMainMenu_Click(object sender, RoutedEventArgs e)
        {
            EndGameMenu.Visibility = Visibility.Collapsed;

            GameGrid.Visibility = Visibility.Collapsed;

            MenuBorder.Visibility = Visibility.Visible;

            MenuAnimation.Position = TimeSpan.Zero;
            MenuAnimation.Play();

            myVideo.Stop();
        }
        private void ChooseEnemyBlock()
        {
            System.Collections.Generic.List<string> availableBlocks = new System.Collections.Generic.List<string>
            { "Head", "Body", "Legs" };

            availableBlocks.Remove(lastEnemyBlock);
            int index = random.Next(0, availableBlocks.Count);
            currentEnemyBlock = availableBlocks[index];
        }
    }
}