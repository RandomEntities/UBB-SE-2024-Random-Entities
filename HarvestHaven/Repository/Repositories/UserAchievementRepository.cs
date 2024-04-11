using Microsoft.Data.SqlClient;
using HarvestHaven.Repository.Entities;
using HarvestHaven.Utils;

namespace HarvestHaven.Repository.Repositories
{
    public static class UserAchievementRepository
    {
        private static readonly string _connectionString = DatabaseHelper.GetDatabaseFilePath();

        public static async Task<List<UserAchievement>> GetAllUserAchievementsAsync()
        {
            List<UserAchievement> userAchievements = new List<UserAchievement>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("SELECT * FROM UserAchievements", connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            userAchievements.Add(new UserAchievement(
                                id: (Guid)reader["Id"],
                                userId: (Guid)reader["UserId"],
                                achievementId: (Guid)reader["AchievementId"],
                                createdTime: (DateTime)reader["CreatedTime"]
                            ));
                        }
                    }
                }
            }
            return userAchievements;
        }

        public static async Task AddUserAchievementAsync(UserAchievement userAchievement)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "INSERT INTO UserAchievements (Id, UserId, AchievementId, CreatedTime) VALUES (@Id, @UserId, @AchievementId, @CreatedTime)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", userAchievement.Id);
                    command.Parameters.AddWithValue("@UserId", userAchievement.UserId);
                    command.Parameters.AddWithValue("@AchievementId", userAchievement.AchievementId);
                    command.Parameters.AddWithValue("@CreatedTime", userAchievement.CreatedTime);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
