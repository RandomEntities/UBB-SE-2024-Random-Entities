using HarvestHaven.Repositories;
using HarvestHaven.Entities;
using HarvestHaven.Utils;
namespace HarvestHaven.Services
{
    public static class CommentService
    {
        public static async Task AddCommentForUser(Guid userId, string message)
        {
            if (GameStateManager.GetCurrentUser() == null)
            {
                throw new Exception("User must be logged in!");
            }

             await CommentRepository.CreateCommentAsync(new Comment(
                id: Guid.NewGuid(),
                userId: userId,
                message: message,
                createdTime: DateTime.Now
             ));
        }

        public static async Task GetMyComments()
        {
            if (GameStateManager.GetCurrentUser() == null)
            {
                throw new Exception("User must be logged in!");
            }

            await CommentRepository.GetUserCommentsAsync(GameStateManager.GetCurrentUserId());
        }
    }
}
