using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HarvestHaven
{
    /// <summary>
    /// Interaction logic for Farm.xaml
    /// </summary>
    public partial class Farm : Window
    {
        private StackPanel buyButton;

        public Farm()
        {
            InitializeComponent();
        }

        #region Screen Transitions
        private void InventoryButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Inventory inventoryScreen = new Inventory(this);

            inventoryScreen.Top = this.Top;
            inventoryScreen.Left = this.Left;

            inventoryScreen.Show();

            this.Hide();
        }

        private void ShopButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SellMarket sellMarket = new SellMarket(this);

            sellMarket.Top = this.Top;
            sellMarket.Left = this.Left;

            sellMarket.Show();

            this.Hide();
        }

        private void QuitButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void ProfileButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ProfileTab profileTab = new ProfileTab();

            profileTab.Top = this.Top;
            profileTab.Left = this.Left;

            profileTab.Show();

            this.Hide();
        }

        private void TradingHallButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TradingUnlocked tradingScreen = new TradingUnlocked(this);

            tradingScreen.Top = this.Top;
            tradingScreen.Left = this.Left;

            tradingScreen.Show();

            this.Hide();
        }
        #endregion

        private void CreateBuyButton(Button button)
        {
            StackPanel newButton = new StackPanel();

            PropertyInfo[] properties = typeof(StackPanel).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.CanWrite) property.SetValue(newButton, property.GetValue(BuyButton));
            }

            foreach (Label child in BuyButton.Children)
            {
                Label newLabel = new Label();
                properties = typeof(Label).GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    if (property.CanWrite) property.SetValue(newLabel, property.GetValue(child));
                }
                newButton.Children.Add(newLabel);
            }
            newButton.Visibility = Visibility.Visible;

            Thickness thickness = button.Margin;
            thickness.Left += 1;
            thickness.Top += 15;
            newButton.Margin = thickness;

            newButton.MouseDown += OpenBuyMarket;

            FarmGrid.Children.Add(newButton);

            this.buyButton = newButton;
        }

        private void DestroyButton(Button button)
        {
            if (buyButton != null)
            {
                FarmGrid.Children.Remove(buyButton);
                buyButton = null;
            }
        }

        private void OpenBuyMarket(object sender, MouseButtonEventArgs e)
        {
            BuyMarket market = new BuyMarket(this);

            market.Top = this.Top;
            market.Left = this.Left;

            market.Show();

            this.Hide();
        }

        private void Farm_Click(object sender, RoutedEventArgs e)
        {
            DestroyButton((Button)sender);
            CreateBuyButton((Button)sender);
        }

        private void Farm_MouseLeave(object sender, MouseEventArgs e)
        {
            Point position = e.GetPosition((UIElement)sender);

            HitTestResult hitTestResult = VisualTreeHelper.HitTest((UIElement)sender, position);
            if (hitTestResult != null) {
                return;
            }

            DestroyButton((Button)sender);
        }
    }
}
