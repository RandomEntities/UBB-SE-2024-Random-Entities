using HarvestHaven.Repository.Entities;
using HarvestHaven.Repository.Repositories;

namespace HarvestHaven.Services
{
    public class UserService
    {
        public static UserService Instance
        {
            get
            {
                return _instance != null ? _instance : _instance = new UserService();
            }
        }
        private static UserService? _instance;
        private readonly UserRepository _userRepository;

        public UserService()
        {
            _userRepository = new UserRepository();
        }

        public async Task<User> GetUserByIdAsync(Guid userId)
        {
            return await _userRepository.GetUserByIdAsync(userId);
        }
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }
    }
}
