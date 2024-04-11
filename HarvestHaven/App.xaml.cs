using HarvestHaven.Repositories;
using System.Windows;

namespace HarvestHaven
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            UserRepository.TestAsync();
        }
    }

}
