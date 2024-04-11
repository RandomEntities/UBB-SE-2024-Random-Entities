using Microsoft.Data.SqlClient;
using HarvestHaven.Repository.Entities;
using HarvestHaven.Utils;

namespace HarvestHaven.Repository.Repositories
{
    public static class FarmCellRepository
    {
        private static readonly string _connectionString = DatabaseHelper.GetDatabaseFilePath();

        public static async Task<List<FarmCell>> GetFarmCellsByUserIdAsync(Guid userId)
        {
            List<FarmCell> farmCells = new List<FarmCell>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT * FROM FarmCells WHERE UserId = @UserId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            farmCells.Add(new FarmCell
                            (
                                id: (Guid)reader["Id"],
                                userId: (Guid)reader["UserId"],
                                row: (int)reader["Row"],
                                column: (int)reader["Column"],
                                itemId: (Guid)reader["ItemId"],
                                lastTimeEnhanced: reader["LastTimeEnhanced"] != DBNull.Value ? (DateTime?)reader["LastTimeEnhanced"] : null,
                                lastTimeInteracted: reader["LastTimeInteracted"] != DBNull.Value ? (DateTime?)reader["LastTimeInteracted"] : null
                            ));
                        }
                    }
                }
            }
            return farmCells;
        }

        public static async Task AddFarmCellForUserAsync(FarmCell farmCell)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "INSERT INTO FarmCells (Id, UserId, Row, Column, ItemId, LastTimeEnhanced, LastTimeInteracted) VALUES (@Id, @UserId, @Row, @Column, @ItemId, @LastTimeEnhanced, @LastTimeInteracted)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", farmCell.Id);
                    command.Parameters.AddWithValue("@UserId", farmCell.UserId);
                    command.Parameters.AddWithValue("@Row", farmCell.Row);
                    command.Parameters.AddWithValue("@Column", farmCell.Column);
                    command.Parameters.AddWithValue("@ItemId", farmCell.ItemId);
                    command.Parameters.AddWithValue("@LastTimeEnhanced", farmCell.LastTimeEnhanced ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@LastTimeInteracted", farmCell.LastTimeInteracted ?? (object)DBNull.Value);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public static async Task UpdateFarmCellForUserAsync(FarmCell farmCell)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "UPDATE FarmCells SET Row = @Row, Column = @Column, ItemId = @ItemId, LastTimeEnhanced = @LastTimeEnhanced, LastTimeInteracted = @LastTimeInteracted WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", farmCell.Id);
                    command.Parameters.AddWithValue("@Row", farmCell.Row);
                    command.Parameters.AddWithValue("@Column", farmCell.Column);
                    command.Parameters.AddWithValue("@ItemId", farmCell.ItemId);
                    command.Parameters.AddWithValue("@LastTimeEnhanced", farmCell.LastTimeEnhanced ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@LastTimeInteracted", farmCell.LastTimeInteracted ?? (object)DBNull.Value);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public static async Task DeleteFarmCellForUserAsync(Guid farmCellId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "DELETE FROM FarmCells WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", farmCellId);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
