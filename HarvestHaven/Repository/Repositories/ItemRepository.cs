using Microsoft.Data.SqlClient;
using HarvestHaven.Repository.Entities;
using HarvestHaven.Utils;

namespace HarvestHaven.Repository.Repositories
{
    public class ItemRepository
    {
        private readonly string _connectionString;

        public ItemRepository()
        {
            this._connectionString = DatabaseHelper.GetDatabaseFilePath();
        }

        public async Task<List<Item>> GetAllItemsAsync()
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
                            {
                                Id = (Guid)reader["Id"],
                                ItemType = (ItemType)reader["ItemType"],
                                RequiredResourceId = (Guid)reader["RequiredResourceId"],
                                InteractResourceId = (Guid)reader["InteractResourceId"],
                                DestroyResourceId = reader["DestroyResourceId"] != DBNull.Value ? (Guid?)reader["DestroyResourceId"] : null
                            });
                        }
                    }
                }
            }
            return items;
        }

        public async Task<Item> GetItemByIdAsync(Guid itemId)
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
                            {
                                Id = (Guid)reader["Id"],
                                ItemType = (ItemType)reader["ItemType"],
                                RequiredResourceId = (Guid)reader["RequiredResourceId"],
                                InteractResourceId = (Guid)reader["InteractResourceId"],
                                DestroyResourceId = reader["DestroyResourceId"] != DBNull.Value ? (Guid?)reader["DestroyResourceId"] : null
                            };
                        }
                    }
                }
            }
            return item;
        }
    }
}
