using Microsoft.Data.SqlClient;
using HarvestHaven.Repository.Entities;
using HarvestHaven.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HarvestHaven.Repository.Repositories
{
    public class TradeRepository
    {
        private readonly string _connectionString;

        public TradeRepository()
        {
            this._connectionString = DatabaseHelper.GetDatabaseFilePath();
        }

        public async Task<List<Trade>> GetAllTradesAsync()
        {
            List<Trade> trades = new List<Trade>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Trades", connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            trades.Add(new Trade
                            {
                                Id = (Guid)reader["Id"],
                                UserId = (Guid)reader["UserId"],
                                GivenResourceId = (Guid)reader["GivenResourceId"],
                                GivenResourceQuantity = (int)reader["GivenResourceQuantity"],
                                RequestedResourceId = (Guid)reader["RequestedResourceId"],
                                RequestedResourceQuantity = (int)reader["RequestedResourceQuantity"],
                                CreatedTime = (DateTime)reader["CreatedTime"],
                                IsCompleted = (bool)reader["IsCompleted"]
                            });
                        }
                    }
                }
            }
            return trades;
        }

        public async Task<Trade> GetUserTradeAsync(Guid userId)
        {
            Trade userTrade = new Trade();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT * FROM Trades WHERE UserId = @UserId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            userTrade = new Trade
                            {
                                Id = (Guid)reader["Id"],
                                UserId = (Guid)reader["UserId"],
                                GivenResourceId = (Guid)reader["GivenResourceId"],
                                GivenResourceQuantity = (int)reader["GivenResourceQuantity"],
                                RequestedResourceId = (Guid)reader["RequestedResourceId"],
                                RequestedResourceQuantity = (int)reader["RequestedResourceQuantity"],
                                CreatedTime = (DateTime)reader["CreatedTime"],
                                IsCompleted = (bool)reader["IsCompleted"]
                            };
                        }
                    }
                }
            }
            return userTrade;
        }

        public async Task CreateTradeAsync(Trade trade)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "INSERT INTO Trades (Id, UserId, GivenResourceId, GivenResourceQuantity, RequestedResourceId, RequestedResourceQuantity, CreatedTime, IsCompleted) VALUES (@Id, @UserId, @GivenResourceId, @GivenResourceQuantity, @RequestedResourceId, @RequestedResourceQuantity, @CreatedTime, @IsCompleted)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", trade.Id);
                    command.Parameters.AddWithValue("@UserId", trade.UserId);
                    command.Parameters.AddWithValue("@GivenResourceId", trade.GivenResourceId);
                    command.Parameters.AddWithValue("@GivenResourceQuantity", trade.GivenResourceQuantity);
                    command.Parameters.AddWithValue("@RequestedResourceId", trade.RequestedResourceId);
                    command.Parameters.AddWithValue("@RequestedResourceQuantity", trade.RequestedResourceQuantity);
                    command.Parameters.AddWithValue("@CreatedTime", trade.CreatedTime);
                    command.Parameters.AddWithValue("@IsCompleted", trade.IsCompleted);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateTradeAsync(Trade trade)
        {
            // Create the SQL connection and release the resources after use.
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("UPDATE Trades SET IsCompleted = @IsCompleted WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", trade.Id);
                    command.Parameters.AddWithValue("@IsCompleted", trade.IsCompleted);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteTradeAsync(Guid tradeId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "DELETE FROM Trades WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", tradeId);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
