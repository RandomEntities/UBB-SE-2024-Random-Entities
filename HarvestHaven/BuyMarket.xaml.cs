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
    /// Interaction logic for BuyMarket.xaml
    /// </summary>
    public partial class BuyMarket : Window
    {
        private Farm farmScreen;

        public BuyMarket(Farm farmScreen)
        {
            this.farmScreen = farmScreen;
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            farmScreen.Top = this.Top;
            farmScreen.Left = this.Left;

            farmScreen.Show();
            this.Close();
        }
    }
}
