using Microsoft.Data.SqlClient;
using HarvestHaven.Utils;
using HarvestHaven.Entities;

namespace HarvestHaven.Repositories
{
    public static class MarketSellResourceRepository
    {
        private static readonly string _connectionString = DatabaseHelper.GetDatabaseFilePath();

        public static async Task<List<MarketSellResource>> GetAllSellResourcesAsync()
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
                            (
                                id: (Guid)reader["Id"],
                                resourceId: (Guid)reader["ResourceId"],
                                sellPrice: (int)reader["SellPrice"]
                            ));
                        }
                    }
                }
            }
            return sellResources;
        }
    }
}
