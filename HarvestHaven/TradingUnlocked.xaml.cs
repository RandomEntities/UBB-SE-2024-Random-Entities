using HarvestHaven.Entities;
using HarvestHaven.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

        private const string carrotPath = "/Assets/Sprites/Items/carrot.png";
        private const string cornPath = "/Assets/Sprites/Items/corn.png";
        private const string wheatPath = "/Assets/Sprites/Items/wheat.png";
        private const string tomatoPath = "/Assets/Sprites/Items/tomato.png";
        private const string chickenPath = "/Assets/Sprites/Items/chicken.png";
        private const string sheepPath = "/Assets/Sprites/Items/sheep.png";
        private const string cowPath = "/Assets/Sprites/Items/cow.png";
        private const string duckPath = "/Assets/Sprites/Items/duck.png";
        private const string duckEggPath = "/Assets/Sprites/Items/duck-egg.png";
        private const string chickenEggPath = "/Assets/Sprites/Items/chicken-egg.png";
        private const string woolPath = "/Assets/Sprites/Items/wool.png";
        private const string milkPath = "/Assets/Sprites/Items/milk.png";


        private List<Trade> tradeList;
        ResourceType getResource;
        ResourceType giveResource;

        public TradingUnlocked(Farm farmScreen)
        {
            this.farmScreen = farmScreen;
            InitializeComponent();

            GetAllTrades();
        }

        private string GetResourcePath(ResourceType resourceType)
        {
            string path = "";
            if (resourceType == ResourceType.Carrot) path = carrotPath;
            else if (resourceType == ResourceType.Corn) path = cornPath;
            else if (resourceType == ResourceType.Wheat) path = wheatPath;
            else if (resourceType == ResourceType.Tomato) path = tomatoPath;
            else if (resourceType == ResourceType.ChickenMeat) path = chickenPath;
            else if (resourceType == ResourceType.DuckMeat) path = duckPath;
            else if (resourceType == ResourceType.Mutton) path = sheepPath;
            else if (resourceType == ResourceType.SheepWool) path = woolPath;
            else if (resourceType == ResourceType.ChickenEgg) path = chickenEggPath;
            else if (resourceType == ResourceType.DuckEgg) path = duckEggPath;
            else if (resourceType == ResourceType.CowMilk) path = milkPath;
            else path = cowPath;

            return path;
        }

        private async void GetAllTrades()
        {
            try
            {
                tradeList = await TradeService.GetTradesAsync();
                foreach (Trade item in tradeList)
                {
                    TradingPanel tradingPanel = new(item);

                    Resource resource1 = await ResourceService.GetResourceByIdAsync(item.GivenResourceId);
                    ResourceType resourceType1 = resource1.ResourceType;
                    tradingPanel.LabelGive.Content = item.GivenResourceQuantity;
                    tradingPanel.ImageGive.Source = new BitmapImage(new Uri(GetResourcePath(resourceType1), UriKind.Relative));

                    Resource resource2 = await ResourceService.GetResourceByIdAsync(item.RequestedResourceId);
                    ResourceType resourceType2 = resource2.ResourceType;
                    tradingPanel.LabelGet.Content = item.RequestedResourceQuantity;
                    tradingPanel.ImageGet.Source = new BitmapImage(new Uri(GetResourcePath(resourceType2), UriKind.Relative));

                    Trades_List.Items.Add(tradingPanel);
                    tradingPanel.AcceptButton.Click += (sender, e) => AcceptButton_Click(sender, e, tradingPanel);
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private async void AcceptButton_Click(object sender, RoutedEventArgs e, TradingPanel tradingPanel)
        {
            //Accept trade
            try
            {
                await TradeService.PerformTradeAsync(tradingPanel.trade.Id);
                Trades_List.Items.Remove(tradingPanel);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
            //Open inventory and select the resource you want to give
            //and return the resource type
            Give_Button.Source = new BitmapImage(new Uri(chickenEggPath, UriKind.Relative));
            giveResource = ResourceType.ChickenEgg;
        }

        private void Get_Button_Click(object sender, RoutedEventArgs e)
        {
            //Open inventory and select the resource you want to get
            //and return the resource type
            Get_Button.Source = new BitmapImage(new Uri(chickenPath, UriKind.Relative));
            giveResource = ResourceType.ChickenMeat;
        }

        private void Confirm_Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.Confirm_Cancel_Button.Content.Equals("Confirm"))
            {
                //Create trade
                string amountGet = Get_TextBox.Text;
                string amountGive = Give_TextBox.Text;
                try
                {
                    int intGet = Convert.ToInt32(amountGet);
                    int intGive = Convert.ToInt32(amountGive);
                    if(intGet < 0 || intGive < 0)
                    {
                        throw new Exception();
                    }
                    //TradeService.CreateTradeAsync(giveResource, intGive, getResource, intGet);
                    this.Confirm_Cancel_Button.Content = "Cancel";
                }
                catch 
                {
                    MessageBox.Show("Input should be a positive number!");
                }
            }
            else
            {
                //Cancel trade
                this.Confirm_Cancel_Button.Content = "Confirm";
            }
        }
    }
}
