using HarvestHaven.Entities;
using HarvestHaven.Repositories;
using HarvestHaven.Utils;

namespace HarvestHaven.Services
{
    public static class UserService
    {
        #region Authentification
        public static async Task<User> GetUserByIdAsync(Guid userId)
        {
            return await UserRepository.GetUserByIdAsync(userId);
        }
        #endregion

        #region Inventory

        public static async Task<Dictionary<InventoryResource, Resource>> GetInventoryResources()
        {
            // Throw an error if the user is not logged in.
            if (GameStateManager.GetCurrentUser() == null) throw new Exception("User must be logged in!");

            // Get all the inventory resources for the current user from the database.
            List<InventoryResource> inventoryResources = await InventoryResourceRepository.GetUserResourcesAsync(GameStateManager.GetCurrentUserId());

            // Initialize the dictionary that will be returned.
            Dictionary<InventoryResource, Resource> inventoryResourcesMap = new Dictionary<InventoryResource, Resource>();


            // Go through each inventory resource.
            foreach(InventoryResource inventoryResource in inventoryResources)
            {
                // Get the corresponding resource from the database.
                Resource resource = await ResourceRepository.GetResourceByIdAsync(inventoryResource.ResourceId);
                if (resource == null) throw new Exception($"No corresponding resource found for the inventory resource with id: {inventoryResource.Id}");
            
                // Add the pair in the dictionary.
                inventoryResourcesMap.Add(inventoryResource, resource);
            }

            return inventoryResourcesMap;
        }

        public static async Task<InventoryResource> GetInventoryResourceByType(ResourceType resourceType)
        {
            // Throw an error if the user is not logged in.
            if (GameStateManager.GetCurrentUser() == null) throw new Exception("User must be logged in!");

            // Get the resource with the given type from the database.
            Resource resource = await ResourceRepository.GetResourceByTypeAsync(resourceType);
            if (resource == null) throw new Exception($"Resource with type: {resourceType.ToString()} found in the database.");

            // Get the inventory resource from the database.
            InventoryResource inventoryResource = await InventoryResourceRepository.GetUserResourceByResourceIdAsync(GameStateManager.GetCurrentUserId(), resource.Id);

            return inventoryResource;
        }

        #endregion

        #region Comments
        public static async Task AddCommentForAnotherUser(Guid targetUserId, string message)
        {
            // Throw an error if the user is not logged in.
            if (GameStateManager.GetCurrentUser() == null) throw new Exception("User must be logged in!");
            
            await CommentRepository.CreateCommentAsync(new Comment(
               id: Guid.NewGuid(),
               userId: targetUserId,
               message: message,
               createdTime: DateTime.UtcNow
            ));
        }

        public static async Task<List<Comment>> GetMyComments()
        {
            // Throw an error if the user is not logged in.
            if (GameStateManager.GetCurrentUser() == null) throw new Exception("User must be logged in!");
            
            return await CommentRepository.GetUserCommentsAsync(GameStateManager.GetCurrentUserId());
        }

        public static async Task DeleteComment(Guid commentId)
        {
            // Throw an error if the user is not logged in.
            if (GameStateManager.GetCurrentUser() == null) throw new Exception("User must be logged in!");

            await CommentRepository.DeleteCommentAsync(commentId);
        }


        #endregion

        #region Achievements

        public static async Task<List<Achievement>> GetAllAchievementsAsync()
        {
            // Throw an error if the user is not logged in.
            if (GameStateManager.GetCurrentUser() == null) throw new Exception("User must be logged in!");

            return await AchievementRepository.GetAllAchievementsAsync();
        }

        public static async Task<Dictionary<UserAchievement, Achievement>> GetUserAchievements()
        {
            // Throw an error if the user is not logged in.
            if (GameStateManager.GetCurrentUser() == null) throw new Exception("User must be logged in!");

            // Get all user achievements from the database.
            List<UserAchievement> userAchievements = await UserAchievementRepository.GetAllUserAchievementsAsync(GameStateManager.GetCurrentUserId());

            // Initialize the dictionary that will be returned.
            Dictionary<UserAchievement, Achievement> userAchievementsMap = new Dictionary<UserAchievement, Achievement>();

            // Go through each user achievement.
            foreach (UserAchievement userAchievement in userAchievements)
            {
                // Get the corresponding achievement from the database.
                Achievement achievement = await AchievementRepository.GetAchievementByIdAsync(userAchievement.AchievementId);
                if (achievement == null) throw new Exception($"No corresponding achievement found for the user achievement with id: {userAchievement.Id}");

                // Add the pair in the dictionary.
                userAchievementsMap.Add(userAchievement, achievement);
            }

            return userAchievementsMap;
        }

        public static async Task AddUserAchievement()
        {
            // TODO: To be implemented (based on different events).
        }

        #endregion

        #region Leaderboard
        public static async Task<List<User>> GetAllUsersSortedByCoinsAsync()
        {
            // Get a list with all the users from the database.
            List<User> users = await UserRepository.GetAllUsersAsync();

            // Sort the users by coins.
            users.Sort((user1, user2) => user2.Coins.CompareTo(user1.Coins));

            return users;
        }

        #endregion
    }
}
