using Microsoft.Data.SqlClient;
using HarvestHaven.Repository.Entities;
using HarvestHaven.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HarvestHaven.Repository.Repositories
{
    public class MarketSellResourceRepository
    {
        private readonly string _connectionString;

        public MarketSellResourceRepository()
        {
            this._connectionString = DatabaseHelper.GetDatabaseFilePath();
        }

        public async Task<List<MarketSellResource>> GetAllSellResourcesAsync()
        {
            List<MarketSellResource> sellResources = new List<MarketSellResource>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("SELECT * FROM MarketSellResources", connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            sellResources.Add(new MarketSellResource
                            {
                                Id = (Guid)reader["Id"],
                                ResourceId = (Guid)reader["ResourceId"],
                                SellPrice = (int)reader["SellPrice"]
                            });
                        }
                    }
                }
            }
            return sellResources;
        }
    }
}
