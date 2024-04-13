using HarvestHaven.Entities;
using HarvestHaven.Utils;
using Microsoft.Data.SqlClient;

namespace HarvestHaven.Repositories
{
    public static class UserRepository
    {
        private static readonly string _connectionString = DatabaseHelper.GetDatabaseFilePath();

        #region CRUD

        public static async Task AddUserAsync(User user)
        {
            // Create the sql connection and release the resources after use.
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("INSERT INTO Users (Id, Username, Coins, TradeHallUnlockTime, LastTimeReceivedWater) VALUES (@Id, @Username, @Coins, @TradeHallUnlockTime, @LastTimeReceivedWater)", connection))
                {
                    command.Parameters.AddWithValue("@Id", user.Id);
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@Coins", user.Coins);
                    command.Parameters.AddWithValue("@TradeHallUnlockTime", user.TradeHallUnlockTime ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@LastTimeReceivedWater", user.LastTimeReceivedWater ?? (object)DBNull.Value);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public static async Task<User> GetUserByIdAsync(Guid userId)
        {
            // Initialize the user variable
            User? user = null;

            // Create the SQL connection and release the resources after use
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                // Create the SQL command to select the user by username
                string query = "SELECT * FROM Users WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", userId);
                    // Execute the command and read the result
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        // Check if there is a user with the given username
                        if (await reader.ReadAsync())
                        {
                            // If a user is found, populate the user object
                            user = new User
                            (
                                id: (Guid)reader["Id"],
                                username: (string)reader["Username"],
                                coins: (int)reader["Coins"],
                                tradeHallUnlockTime: reader["TradeHallUnlockTime"] != DBNull.Value ? (DateTime)reader["TradeHallUnlockTime"] : null,
                                lastTimeReceivedWater: reader["LastTimeReceivedWater"] != DBNull.Value ? (DateTime)reader["LastTimeReceivedWater"] : null
                            );
                        }
                    }
                }
            }
            // Return the user (or null if not found).
            return user;
        }

        public static async Task<List<User>> GetAllUsersAsync()
        {
            List<User> users = new List<User>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Users", connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            users.Add(new User
                            (
                                id: (Guid)reader["Id"],
                                username: (string)reader["Username"],
                                coins: (int)reader["Coins"],
                                tradeHallUnlockTime: reader["DestroyResourceId"] != DBNull.Value ? (DateTime)reader["TradeHallUnlockTime"] : null,
                                lastTimeReceivedWater: reader["LastTimeReceivedWater"] != DBNull.Value ? (DateTime)reader["LastTimeReceivedWater"] : null
                            ));
                        }
                    }
                }
            }
            return users;
        }

        public static async Task UpdateUserAsync(User user)
        {
            // Create the SQL connection and release the resources after use.
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("UPDATE Users SET Username = @Username, Coins = @Coins, TradeHallUnlockTime = @TradeHallUnlockTime, LastTimeReceivedWater = @LastTimeReceivedWater WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", user.Id);
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@Coins", user.Coins);
                    command.Parameters.AddWithValue("@TradeHallUnlockTime", user.TradeHallUnlockTime ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@LastTimeReceivedWater", user.LastTimeReceivedWater ?? (object)DBNull.Value);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public static async Task DeleteUserByIdAsync(Guid userId)
        {
            // Create the SQL connection and release the resources after use.
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("DELETE FROM Users WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", userId);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        #endregion

        #region Helper Functions
        public static async Task TestAsync()
        {
            try
            {
                // Get all users asynchronously
                List<User> users = await GetAllUsersAsync();

                // Display the usernames
                if (users != null && users.Any())
                {
                    Console.WriteLine("Initial Users:");
                    foreach (var user in users)
                    {
                        Console.WriteLine($"Username: {user.Username}, Coins: {user.Coins}");
                    }
                }
                else
                {
                    Console.WriteLine("No users found.");
                }

                // Add a random user
                User newUser = new User
                (
                    id: Guid.NewGuid(),
                    username: "NewUser",
                    coins: 100,
                    tradeHallUnlockTime: DateTime.Now,
                    lastTimeReceivedWater: DateTime.Now
                );
                await AddUserAsync(newUser);
                Console.WriteLine($"New user added: {newUser.Username}");

                // Get all users again
                users = await GetAllUsersAsync();
                if (users != null && users.Any())
                {
                    Console.WriteLine("\nUpdated Users:");
                    foreach (var user in users)
                    {
                        Console.WriteLine($"Username: {user.Username}, Coins: {user.Coins}");
                    }
                }
                else
                {
                    Console.WriteLine("No users found.");
                }

                // Update the newly added user
                newUser.Coins += 50;
                await UpdateUserAsync(newUser);
                Console.WriteLine($"\nUser {newUser.Username} updated. New coins: {newUser.Coins}");

                // Delete the newly added user
                //await this.DeleteUserByIdAsync(newUser.Id);
                Console.WriteLine($"\nUser {newUser.Username} deleted.");

                // Get all users after deletion
                users = await GetAllUsersAsync();
                if (users != null && users.Any())
                {
                    Console.WriteLine("\nUsers after deletion:");
                    foreach (var user in users)
                    {
                        Console.WriteLine($"Username: {user.Username}, Coins: {user.Coins}");
                    }
                }
                else
                {
                    Console.WriteLine("No users found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
            }
        }

        #endregion
    }
}
