﻿using System;
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
using HarvestHaven.Profile_Gui.Model;

namespace HarvestHaven.Profile_Gui
{
    /// <summary>
    /// Interaction logic for Leaderboard.xaml
    /// </summary>
    public partial class Leaderboard : Window
    {
        public List<LeaderboardItem> Items = new List<LeaderboardItem>();
        public Leaderboard()
        {
            this.Items.Add(new LeaderboardItem(1, "Zsigmond Imre", 100000));
            this.Items.Add(new LeaderboardItem(2, "Kukac", 10000));
            this.Items.Add(new LeaderboardItem(3, "Kekesz", 100));
            this.Items.Add(new LeaderboardItem(4, "Kekesz", 100));
            this.Items.Add(new LeaderboardItem(5, "Kekesz", 100));
            this.Items.Add(new LeaderboardItem(6, "Kekesz", 100));
            this.Items.Add(new LeaderboardItem(7, "Kekesz", 100));

            this.DataContext = Items;
            InitializeComponent();
        }
    }
}
