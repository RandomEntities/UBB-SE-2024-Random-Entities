using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using static HarvestHaven.TradingUnlocked;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HarvestHaven
{
    /// <summary>
    /// Interaction logic for TradingUnlocked.xaml
    /// </summary>
    public partial class TradingUnlocked : Window
    {
        private Farm farmScreen;

        public TradingUnlocked(Farm farmScreen)
        {
            this.farmScreen = farmScreen;
            InitializeComponent();
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            farmScreen.Top = this.Top;
            farmScreen.Left = this.Left;

            farmScreen.Show();
            this.Close();
        }

        private void Give_Button_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void Get_Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
