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
    public class AchievementItem(string imageSource, string description, int coinAmount)
    {
        public string ImageSource { get; set; } = imageSource;
        public string Description { get; set; } = description;
        public int CoinAmount { get; set; } = coinAmount;
        
    }
    /// <summary>
    /// Interaction logic for Achievements.xaml
    /// </summary>
    public partial class Achievements : Window
    {
        public List<AchievementItem> achievements = new List<AchievementItem> ();
        public Achievements()
        {
            this.achievements.Add(new AchievementItem("/Assets/Sprites/sunglasses_face_icon.png", "Trade with a player", 50));
            this.achievements.Add(new AchievementItem("/Assets/Sprites/Items/cow.png", "Put 5 cows in X shape", 100));
            this.achievements.Add(new AchievementItem("/Assets/Sprites/sunglasses_face_icon.png", "Trade with a player", 50));
            this.achievements.Add(new AchievementItem("/Assets/Sprites/sunglasses_face_icon.png", "Trade with a player", 50));
            this.achievements.Add(new AchievementItem("/Assets/Sprites/sunglasses_face_icon.png", "Trade with a player", 50));
            this.achievements.Add(new AchievementItem("/Assets/Sprites/sunglasses_face_icon.png", "Trade with a player", 50));

            this.DataContext = achievements;
            InitializeComponent();
        }
    }
}
