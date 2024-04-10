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

            TradingPanel tradingPanel = new();

            Trades_List.Items.Add(tradingPanel);

            tradingPanel.AcceptButton.Click += (sender, e) => AcceptButton_Click(sender, e, tradingPanel);


        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e,TradingPanel tradingPanel)
        {
            tradingPanel.LabelGive.Content = "120";
            tradingPanel.ImageGet.Source = new BitmapImage(new Uri("/Assets/Sprites/Items/tomato.png", UriKind.Relative));

            tradingPanel.LabelGet.Content = "250";
            tradingPanel.ImageGet.Source = new BitmapImage(new Uri("/Assets/Sprites/Items/cow.png", UriKind.Relative));
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

        private void Confirm_Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.Confirm_Cancel_Button.Content.Equals("Confirm"))
            {
                this.Confirm_Cancel_Button.Content = "Cancel";
            }
            else
            {
                this.Confirm_Cancel_Button.Content = "Confirm";
            }
        }
    }
}
