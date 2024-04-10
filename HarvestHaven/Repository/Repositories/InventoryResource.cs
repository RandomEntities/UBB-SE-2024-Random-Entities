using Microsoft.Data.SqlClient;
using HarvestHaven.Repository.Entities;
using HarvestHaven.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HarvestHaven.Repository.Repositories
{
    public class InventoryResourceRepository
    {
        private readonly string _connectionString;

        public InventoryResourceRepository()
        {
            this._connectionString = DatabaseHelper.GetDatabaseFilePath();
        }

        public async Task<List<InventoryResource>> GetUserResourcesAsync(Guid userId)
        {
            List<InventoryResource> userResources = new List<InventoryResource>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT * FROM InventoryResources WHERE UserId = @UserId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            userResources.Add(new InventoryResource
                            {
                                Id = (Guid)reader["Id"],
                                UserId = (Guid)reader["UserId"],
                                ResourceId = (Guid)reader["ResourceId"],
                                Quantity = (int)reader["Quantity"]
                            });
                        }
                    }
                }
            }
            return userResources;
        }

        public async Task AddUserResourceAsync(InventoryResource userResource)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "INSERT INTO InventoryResources (Id, UserId, ResourceId, Quantity) VALUES (@Id, @UserId, @ResourceId, @Quantity)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", userResource.Id);
                    command.Parameters.AddWithValue("@UserId", userResource.UserId);
                    command.Parameters.AddWithValue("@ResourceId", userResource.ResourceId);
                    command.Parameters.AddWithValue("@Quantity", userResource.Quantity);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateUserResourceAsync(InventoryResource userResource)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "UPDATE InventoryResources SET Quantity = @Quantity WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", userResource.Id);
                    command.Parameters.AddWithValue("@Quantity", userResource.Quantity);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteUserResourceAsync(Guid userResourceId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "DELETE FROM InventoryResources WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", userResourceId);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
