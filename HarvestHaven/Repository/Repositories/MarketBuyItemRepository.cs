using Microsoft.Data.SqlClient;
using HarvestHaven.Repository.Entities;
using HarvestHaven.Utils;

namespace HarvestHaven.Repository.Repositories
{
    public static class MarketBuyItemRepository
    {
        private static readonly string _connectionString = DatabaseHelper.GetDatabaseFilePath();

        public static async Task<List<MarketBuyItem>> GetAllMarketBuyItemsAsync()
        {
            List<MarketBuyItem> marketBuyItems = new List<MarketBuyItem>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("SELECT * FROM MarketBuyItems", connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            marketBuyItems.Add(new MarketBuyItem
                            (
                                id: (Guid)reader["Id"],
                                itemId: (Guid)reader["ItemId"],
                                buyPrice: (int)reader["BuyPrice"]
                            ));
                        }
                    }
                }
            }
            return marketBuyItems;
        }
    }
}
