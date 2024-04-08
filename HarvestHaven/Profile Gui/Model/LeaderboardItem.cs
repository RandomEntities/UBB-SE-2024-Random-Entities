using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarvestHaven.Profile_Gui.Model
{
    public class LeaderboardItem(int Id, string UserName, int NumberOfCoins)
    {
        public int Id { get; set; } = Id;
        public string UserName { get; set; } = UserName;
        public int NumberOfCoins { get; set; } = NumberOfCoins;
    }
}
