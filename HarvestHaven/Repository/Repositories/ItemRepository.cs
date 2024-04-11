using Microsoft.Data.SqlClient;
using HarvestHaven.Repository.Entities;
using HarvestHaven.Utils;

namespace HarvestHaven.Repository.Repositories
{
    public static class ItemRepository
    {
        private static readonly string _connectionString = DatabaseHelper.GetDatabaseFilePath();

        public static async Task<List<Item>> GetAllItemsAsync()
        {
            List<Item> items = new List<Item>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Items", connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            items.Add(new Item
                            (
                                id: (Guid)reader["Id"],
                                itemType: (ItemType)reader["ItemType"],
                                requiredResourceId: (Guid)reader["RequiredResourceId"],
                                interactResourceId: (Guid)reader["InteractResourceId"],
                                destroyResourceId: reader["DestroyResourceId"] != DBNull.Value ? (Guid?)reader["DestroyResourceId"] : null
                            ));
                        }
                    }
                }
            }
            return items;
        }

        public static async Task<Item> GetItemByIdAsync(Guid itemId)
        {
            Item item = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT * FROM Items WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", itemId);
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            item = new Item
                            (
                                id: (Guid)reader["Id"],
                                itemType: (ItemType)reader["ItemType"],
                                requiredResourceId: (Guid)reader["RequiredResourceId"],
                                interactResourceId: (Guid)reader["InteractResourceId"],
                                destroyResourceId: reader["DestroyResourceId"] != DBNull.Value ? (Guid?)reader["DestroyResourceId"] : null
                            );
                        }
                    }
                }
            }
            return item;
        }
    }
}
