﻿using HarvestHaven.Entities;
using HarvestHaven.Repositories;
using HarvestHaven.Services;
using HarvestHaven.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.Xml;
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
        private List<Image> itemIcons = new List<Image>();
        private StackPanel? buyButton;

        private const int columnCount = 6;
        private int clickedRow;
        private int clickedColumn;

        public Farm()
        {
            InitializeComponent();
            RefreshGUI();
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
            ProfileTab profileTab = new ProfileTab(this);

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

        private void OpenBuyMarket(object sender, MouseButtonEventArgs e)
        {
            DestroyBuyButton();

            BuyMarket market = new BuyMarket(this, clickedRow, clickedColumn);

            market.Top = this.Top;
            market.Left = this.Left;

            market.Show();

            this.Hide();
        }
        #endregion

        public async void RefreshGUI()
        {
            User? user = GameStateManager.GetCurrentUser();
            if (user != null) coinLabel.Content = user.Coins;

            #region Update Water
            try
            {
                Dictionary<InventoryResource, Resource> items = await UserService.GetInventoryResources();

                bool found = false;
                foreach (KeyValuePair<InventoryResource, Resource> pair in items)
                {
                    if (pair.Value.ResourceType == ResourceType.Water)
                    {
                        waterLabel.Content = pair.Key.Quantity;
                        found = true;
                        break;
                    }
                }

                if (!found) waterLabel.Content = "0";
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            #endregion

            try
            {
                Dictionary<FarmCell, Item> farmCells = await FarmService.GetAllFarmCellsForUser(user.Id);

                foreach (KeyValuePair<FarmCell, Item> pair in farmCells)
                {
                    int buttonIndex = (pair.Key.Row - 1) * columnCount + pair.Key.Column;
                    
                    Button associatedButton = (Button)FindName("Farm" + buttonIndex);

                    string path = "";
                    CreateItemIcon(associatedButton, path);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void CreateItemIcon(Button associatedButton, string imagePath)
        {
            Image newImage = new Image();

            PropertyInfo[] properties = typeof(Image).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.CanWrite) property.SetValue(newImage, property.GetValue(itemIcon));
            }
            newImage.Visibility = Visibility.Visible;

            Thickness thickness = associatedButton.Margin;
            thickness.Left += 6;
            thickness.Top += 6;
            newImage.Margin = thickness;

            newImage.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/Sprites/Items/corn.png"));

            FarmGrid.Children.Add(newImage);
            itemIcons.Add(newImage);
        }

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

            SetRowColumn(button.Name);
        }

        private void SetRowColumn(string name)
        {
            string possibleNumber = name.Substring(name.Length - 2, 2);
            int number = 0;
            if (int.TryParse(possibleNumber, out number))
            {
                ConvertToRowColumn(number);
                return;
            }

            possibleNumber = name.Substring(name.Length - 1, 1);
            number = int.Parse(possibleNumber);
            ConvertToRowColumn(number);
        }

        private void ConvertToRowColumn(int number)
        {
            int fullRows = number / columnCount;

            int newNumber = number - (fullRows * columnCount);
            if (newNumber == 0)
            {
                this.clickedRow = fullRows;
                this.clickedColumn = columnCount;
            }
            else
            {
                this.clickedRow = fullRows + 1;
                this.clickedColumn = newNumber;
            }
        }

        private void DestroyBuyButton()
        {
            if (buyButton != null)
            {
                FarmGrid.Children.Remove(buyButton);
                buyButton = null;
            }
        }

        private void Farm_Click(object sender, RoutedEventArgs e)
        {
            DestroyBuyButton();
            CreateBuyButton((Button)sender);
        }

        private void Farm_MouseLeave(object sender, MouseEventArgs e)
        {
            Point position = e.GetPosition((UIElement)sender);

            HitTestResult hitTestResult = VisualTreeHelper.HitTest((UIElement)sender, position);
            if (hitTestResult != null)
            {
                return;
            }

            DestroyBuyButton();
        }
    }
}
