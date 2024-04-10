using Microsoft.Data.SqlClient;
using HarvestHaven.Repository.Entities;
using HarvestHaven.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HarvestHaven.Repository.Repositories
{
    public class AchievementRepository
    {
        private readonly string _connectionString;

        public AchievementRepository()
        {
            this._connectionString = DatabaseHelper.GetDatabaseFilePath();
        }

        public async Task<List<Achievement>> GetAllAchievementsAsync()
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
                            {
                                Id = (Guid)reader["Id"],
                                Description = (string)reader["Description"],
                                RewardCoins = (int)reader["RewardCoins"]
                            });
                        }
                    }
                }
            }
            return achievements;
        }

        public async Task<Achievement> GetAchievementByIdAsync(Guid achievementId)
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
                            {
                                Id = (Guid)reader["Id"],
                                Description = (string)reader["Description"],
                                RewardCoins = (int)reader["RewardCoins"]
                            };
                        }
                    }
                }
            }
            return achievement;
        }
    }
}
