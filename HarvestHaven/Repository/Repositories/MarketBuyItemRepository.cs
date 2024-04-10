using Microsoft.Data.SqlClient;
using HarvestHaven.Repository.Entities;
using HarvestHaven.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HarvestHaven.Repository.Repositories
{
    public class MarketBuyItemRepository
    {
        private readonly string _connectionString;

        public MarketBuyItemRepository()
        {
            this._connectionString = DatabaseHelper.GetDatabaseFilePath();
        }

        public async Task<List<MarketBuyItem>> GetAllMarketBuyItemsAsync()
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
                            {
                                Id = (Guid)reader["Id"],
                                ItemId = (Guid)reader["ItemId"],
                                BuyPrice = (int)reader["BuyPrice"]
                            });
                        }
                    }
                }
            }
            return marketBuyItems;
        }
    }
}
