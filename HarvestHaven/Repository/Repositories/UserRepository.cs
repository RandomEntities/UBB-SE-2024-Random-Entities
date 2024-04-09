using HarvestHaven.Repository.Entities;
using Microsoft.Data.SqlClient;

namespace HarvestHaven.Repository.Repositories
{
    public class UserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            this._connectionString = connectionString;
        }

        #region CRUD

        public async Task AddUserAsync(User user)
        {
            // Create the sql connection and release the resources after use.
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("INSERT INTO Users (Id, Username, Password, Coins, TradeHallUnlockTime) VALUES (@Id, @Username, @Password, @Coins, @TradeHallUnlockTime)", connection))
                {
                    command.Parameters.AddWithValue("@Id", user.Id);
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    command.Parameters.AddWithValue("@Coins", user.Coins);
                    command.Parameters.AddWithValue("@TradeHallUnlockTime", user.TradeHallUnlockTime);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            // Initialize the user variable
            User? user = null;

            // Create the SQL connection and release the resources after use
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                // Create the SQL command to select the user by username
                string query = "SELECT * FROM Users WHERE Username = @Username";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    // Execute the command and read the result
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        // Check if there is a user with the given username
                        if (await reader.ReadAsync())
                        {
                            // If a user is found, populate the user object
                            user = new User
                            {
                                Id = (Guid)reader["Id"],
                                Username = (string)reader["Username"],
                                Password = (string)reader["Password"],
                                Coins = (int)reader["Coins"],
                                TradeHallUnlockTime = (DateTime)reader["TradeHallUnlockTime"]
                            };
                        }
                    }
                }
            }
            // Return the user (or null if not found).
            return user;
        }

        public async Task<List<User>> GetAllUsersAsync()
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
                            {
                                Id = (Guid)reader["Id"],
                                Username = (string)reader["Username"],
                                Password = (string)reader["Password"],
                                Coins = (int)reader["Coins"],
                                TradeHallUnlockTime = (DateTime)reader["TradeHallUnlockTime"]
                            });
                        }
                    }
                }
            }
            return users;
        }

        public async Task UpdateUserAsync(User user)
        {
            // Create the SQL connection and release the resources after use.
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("UPDATE Users SET Username = @Username, Password = @Password, Coins = @Coins, TradeHallUnlockTime = @TradeHallUnlockTime WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", user.Id);
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    command.Parameters.AddWithValue("@Coins", user.Coins);
                    command.Parameters.AddWithValue("@TradeHallUnlockTime", user.TradeHallUnlockTime);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteUserByIdAsync(Guid userId)
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

        private async Task InitializeAsync()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // Check if the Users table exists.
                string checkTableQuery = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Users'";
                using (SqlCommand command = new SqlCommand(checkTableQuery, connection))
                {
                    int tableCount = (int)await command.ExecuteScalarAsync();
                    if (tableCount == 0)
                    {
                        // Create the Users table.
                        string createTableQuery = @"
                            CREATE TABLE Users (
                                Id UNIQUEIDENTIFIER PRIMARY KEY,
                                Username NVARCHAR(50) NOT NULL,
                                Password NVARCHAR(50) NOT NULL,
                                Coins INT NOT NULL,
                                TradeHallUnlockTime DATETIME NOT NULL
                            )";
                        using (SqlCommand createTableCommand = new SqlCommand(createTableQuery, connection))
                        {
                            await createTableCommand.ExecuteNonQueryAsync();
                        }
                    }
                }
            }
        }

        private async Task TestAsync()
        {
            try
            {
                // Create a new instance of the UserRepository
                UserRepository userRepository = new UserRepository(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True;Connect Timeout=30");

                // Get all users asynchronously
                List<User> users = await userRepository.GetAllUsersAsync();

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
                {
                    Id = Guid.NewGuid(),
                    Username = "NewUser",
                    Password = "password",
                    Coins = 100,
                    TradeHallUnlockTime = DateTime.Now
                };
                await userRepository.AddUserAsync(newUser);
                Console.WriteLine($"New user added: {newUser.Username}");

                // Get all users again
                users = await userRepository.GetAllUsersAsync();
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
                await userRepository.UpdateUserAsync(newUser);
                Console.WriteLine($"\nUser {newUser.Username} updated. New coins: {newUser.Coins}");

                // Delete the newly added user
                await userRepository.DeleteUserByIdAsync(newUser.Id);
                Console.WriteLine($"\nUser {newUser.Username} deleted.");

                // Get all users after deletion
                users = await userRepository.GetAllUsersAsync();
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
