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
using static HarvestHaven.Trading_Unlocked;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HarvestHaven
{
    /// <summary>
    /// Interaction logic for Trading_Unlocked.xaml
    /// </summary>
    public partial class Trading_Unlocked : Window
    {

        public Trading_Unlocked()
        {
            InitializeComponent();

            //Button testButton = new Button() { Content="Click me!"};
            //Trades_List.Items.Add(testButton);
        }

        private void Give_Button_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void Get_Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
