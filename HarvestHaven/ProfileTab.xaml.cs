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
using System.Windows.Shapes;

namespace HarvestHaven
{
    /// <summary>
    /// Interaction logic for ProfileTab.xaml
    /// </summary>
    public partial class ProfileTab : Window
    {
        private Farm farmScreen;

        public ProfileTab(Farm farmScreen)
        {
            this.farmScreen = farmScreen;
            InitializeComponent();
            SwitchToAchievements();
        }

        private void SwitchToAchievements()
        {
            achievementList.Visibility = Visibility.Visible;
            leaderboardList.Visibility = Visibility.Hidden;
            commentList.Visibility = Visibility.Hidden;
        }

        private void SwitchToLeaderboard()
        {
            achievementList.Visibility = Visibility.Hidden;
            leaderboardList.Visibility = Visibility.Visible;
            commentList.Visibility = Visibility.Hidden;
        }

        private void SwitchToComments()
        {
            achievementList.Visibility = Visibility.Hidden;
            leaderboardList.Visibility = Visibility.Hidden;
            commentList.Visibility = Visibility.Visible;
        }

        private void achievementButton_Click(object sender, RoutedEventArgs e)
        {
            SwitchToAchievements();
        }

        private void leaderboardButton_Click(object sender, RoutedEventArgs e)
        {
            SwitchToLeaderboard();
        }

        private void commentsButton_Click(object sender, RoutedEventArgs e)
        {
            SwitchToComments();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            farmScreen.Top = this.Top;
            farmScreen.Left = this.Left;

            farmScreen.Show();
            this.Close();
        }
    }
}
