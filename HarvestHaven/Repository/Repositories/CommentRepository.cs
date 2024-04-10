using Microsoft.Data.SqlClient;
using HarvestHaven.Repository.Entities;
using HarvestHaven.Utils;

namespace HarvestHaven.Repository.Repositories
{
    public class CommentRepository
    {
        private readonly string _connectionString;

        public CommentRepository()
        {
            this._connectionString = DatabaseHelper.GetDatabaseFilePath();
        }

        public async Task AddCommentForUserAsync(Guid userId, string message)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // Create the SQL command to insert a new comment.
                string query = "INSERT INTO Comments (Id, UserId, Message, CreatedTime) VALUES (@Id, @UserId, @Message, @CreatedTime)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Set the parameters for the SQL command.
                    command.Parameters.AddWithValue("@Id", Guid.NewGuid());
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@Message", message);
                    command.Parameters.AddWithValue("@CreatedTime", DateTime.Now);

                    // Execute the SQL command to insert the comment.
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<List<Comment>> GetCommentsForUserAsync(Guid userId)
        {
            List<Comment> comments = new List<Comment>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT * FROM Comments WHERE UserId = @UserId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            comments.Add(new Comment
                            {
                                Id = (Guid)reader["Id"],
                                UserId = (Guid)reader["UserId"],
                                Message = (string)reader["Message"],
                                CreatedTime = (DateTime)reader["CreatedTime"]
                            });
                        }
                    }
                }
            }
            return comments;
        }
    }
}
