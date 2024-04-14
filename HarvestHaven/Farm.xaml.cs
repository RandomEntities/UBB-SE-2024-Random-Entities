using HarvestHaven.Entities;
using HarvestHaven.Repositories;
using HarvestHaven.Services;
using HarvestHaven.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
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

        #region Image Paths
        private const string carrotPath = "Assets/Sprites/Items/carrot.png";
        private const string cornPath = "Assets/Sprites/Items/corn.png";
        private const string wheatPath = "Assets/Sprites/Items/wheat.png";
        private const string tomatoPath = "Assets/Sprites/Items/tomato.png";
        private const string chickenPath = "Assets/Sprites/Items/chicken.png";
        private const string sheepPath = "Assets/Sprites/Items/sheep.png";
        private const string cowPath = "Assets/Sprites/Items/cow.png";
        private const string duckPath = "Assets/Sprites/Items/duck.png";
        #endregion

        private const int columnCount = 6;
        private int clickedRow;
        private int clickedColumn;

        public Farm()
        {
            InitializeComponent();
            RefreshGUI();
            Hello();
        }

        private async void Hello()
        {
                await InventoryResourceRepository.AddUserResourceAsync(new InventoryResource(
                        id: Guid.NewGuid(),
                        userId: GameStateManager.GetCurrentUserId(),
                        resourceId: Guid.Parse("42f91ff4-6bc1-4e89-9aab-14d738f67f08"),
                        quantity: 0
                        ));;

            await InventoryResourceRepository.AddUserResourceAsync(new InventoryResource(
        id: Guid.NewGuid(),
        userId: GameStateManager.GetCurrentUserId(),
        resourceId: Guid.Parse("51ababa9-ef2f-4f5c-bf80-1a2eb6308c42"),
        quantity: 0
        )); ;

            await InventoryResourceRepository.AddUserResourceAsync(new InventoryResource(
        id: Guid.NewGuid(),
        userId: GameStateManager.GetCurrentUserId(),
        resourceId: Guid.Parse("ae362eff-6245-4744-9b28-2d8e42a5c273"),
        quantity: 0
        )); ;

            await InventoryResourceRepository.AddUserResourceAsync(new InventoryResource(
        id: Guid.NewGuid(),
        userId: GameStateManager.GetCurrentUserId(),
        resourceId: Guid.Parse("34ed9fba-09c3-4557-ad7a-88f65c7702c1"),
        quantity: 0
        )); ;

            await InventoryResourceRepository.AddUserResourceAsync(new InventoryResource(
        id: Guid.NewGuid(),
        userId: GameStateManager.GetCurrentUserId(),
        resourceId: Guid.Parse("c99016b9-2b96-43af-8791-3becba627d9b"),
        quantity: 0
        )); ;

            await InventoryResourceRepository.AddUserResourceAsync(new InventoryResource(
        id: Guid.NewGuid(),
        userId: GameStateManager.GetCurrentUserId(),
        resourceId: Guid.Parse("6d1bc4da-5b49-493d-a238-92f6618d3c15"),
        quantity: 0
        )); ;

            await InventoryResourceRepository.AddUserResourceAsync(new InventoryResource(
        id: Guid.NewGuid(),
        userId: GameStateManager.GetCurrentUserId(),
        resourceId: Guid.Parse("6869409b-8bb7-4e30-bcab-97e9c8820d3f"),
        quantity: 0
        )); ;

            await InventoryResourceRepository.AddUserResourceAsync(new InventoryResource(
        id: Guid.NewGuid(),
        userId: GameStateManager.GetCurrentUserId(),
        resourceId: Guid.Parse("843b9703-258c-40b3-aa50-9e763d0602e6"),
        quantity: 0
        )); ;

            await InventoryResourceRepository.AddUserResourceAsync(new InventoryResource(
        id: Guid.NewGuid(),
        userId: GameStateManager.GetCurrentUserId(),
        resourceId: Guid.Parse("bd8e0d17-b20f-401e-946d-ab089ed94ae4"),
        quantity: 0
        )); ;

            await InventoryResourceRepository.AddUserResourceAsync(new InventoryResource(
id: Guid.NewGuid(),
userId: GameStateManager.GetCurrentUserId(),
resourceId: Guid.Parse("0a2e32a8-ab8d-46bb-9139-e08ee95f419f"),
quantity: 0
)); ;

            await InventoryResourceRepository.AddUserResourceAsync(new InventoryResource(
id: Guid.NewGuid(),
userId: GameStateManager.GetCurrentUserId(),
resourceId: Guid.Parse("5e9181b1-98c9-45ee-81ae-e50f2e80abb1"),
quantity: 0
)); ;
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
                InventoryResource water = await UserService.GetInventoryResourceByType(ResourceType.Water);
                waterLabel.Content = water.Quantity;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            #endregion

            #region Farm Rendering
            try
            {
                Dictionary<FarmCell, Item> farmCells = await FarmService.GetAllFarmCellsForUser(user.Id);

                foreach (KeyValuePair<FarmCell, Item> pair in farmCells)
                {
                    int buttonIndex = (pair.Key.Row - 1) * columnCount + pair.Key.Column;
                    
                    Button associatedButton = (Button)FindName("Farm" + buttonIndex);

                    ItemType type = pair.Value.ItemType;
                    string path = "";
                    if (type == ItemType.CarrotSeeds) path = carrotPath;
                    else if (type == ItemType.CornSeeds) path = cornPath;
                    else if (type == ItemType.WheatSeeds) path = wheatPath;
                    else if (type == ItemType.TomatoSeeds) path = tomatoPath;
                    else if (type == ItemType.Chicken) path = chickenPath;
                    else if (type == ItemType.Duck) path = duckPath;
                    else if (type == ItemType.Sheep) path = sheepPath;
                    else path = cowPath;

                    CreateItemIcon(associatedButton, path);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            #endregion
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

            newImage.Margin = associatedButton.Margin;

            newImage.Source = new BitmapImage(new Uri("pack://application:,,,/" + imagePath));

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
