using Microsoft.Data.SqlClient;
using HarvestHaven.Repository.Entities;
using HarvestHaven.Utils;

namespace HarvestHaven.Repository.Repositories
{
    public class ResourceRepository
    {
        private readonly string _connectionString;

        public ResourceRepository()
        {
            this._connectionString = DatabaseHelper.GetDatabaseFilePath();
        }

        public async Task<List<Resource>> GetAllResourcesAsync()
        {
            List<Resource> resources = new List<Resource>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Resources", connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            resources.Add(new Resource
                            {
                                Id = (Guid)reader["Id"],
                                ResourceType = (ResourceType)reader["ResourceType"]
                            });
                        }
                    }
                }
            }
            return resources;
        }

        public async Task<Resource> GetResourceByIdAsync(Guid resourceId)
        {
            Resource resource = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT * FROM Resources WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", resourceId);
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            resource = new Resource
                            {
                                Id = (Guid)reader["Id"],
                                ResourceType = (ResourceType)reader["ResourceType"]
                            };
                        }
                    }
                }
            }
            return resource;
        }
    }
}
