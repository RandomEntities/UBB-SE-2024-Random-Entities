using HarvestHaven.Repositories;
using HarvestHaven.Entities;

namespace HarvestHaven.Services
{
    public static class AchievementService
    {
        public static async Task<Achievement> GetAchivementByIdAsync(Guid achievementId)
        {
            return await AchievementRepository.GetAchievementByIdAsync(achievementId);
        }
        public static async Task<List<Achievement>> GetAllAchievementsAsync()
        {
            return await AchievementRepository.GetAllAchievementsAsync();
        }

        //TODO: Add user achivement's thingy.
    }
}
