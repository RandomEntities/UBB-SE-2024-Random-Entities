using Microsoft.Data.SqlClient;
using HarvestHaven.Repository.Entities;
using HarvestHaven.Utils;

namespace HarvestHaven.Repository.Repositories
{
    public static class AchievementRepository
    {
        private static readonly string _connectionString = DatabaseHelper.GetDatabaseFilePath();

        public static async Task<List<Achievement>> GetAllAchievementsAsync()
        {
            List<Achievement> achievements = new List<Achievement>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Achievements", connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            achievements.Add(new Achievement
                            (
                                id: (Guid)reader["Id"],
                                description: (string)reader["Description"],
                                rewardCoins: (int)reader["RewardCoins"]
                            ));
                        }
                    }
                }
            }
            return achievements;
        }

        public static async Task<Achievement> GetAchievementByIdAsync(Guid achievementId)
        {
            Achievement achievement = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT * FROM Achievements WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", achievementId);
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            achievement = new Achievement
                            (
                                id: (Guid)reader["Id"],
                                description: (string)reader["Description"],
                                rewardCoins: (int)reader["RewardCoins"]
                            );
                        }
                    }
                }
            }
            return achievement;
        }
    }
}
