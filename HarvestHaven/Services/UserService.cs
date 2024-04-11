using HarvestHaven.Entities;
using HarvestHaven.Repositories;

namespace HarvestHaven.Services
{
    public static class UserService
    {   
        public static async Task<User> GetUserByIdAsync(Guid userId)
        {
            return await UserRepository.GetUserByIdAsync(userId);
        }
        public static async Task<List<User>> GetAllUsersAsync()
        {
            return await UserRepository.GetAllUsersAsync();
        }
    }
}
