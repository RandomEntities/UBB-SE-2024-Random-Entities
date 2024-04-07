namespace HarvestHaven.Repository.Entities
{
    public class UserAchievement
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; } // User.
        public Guid AchievementId { get; set; } // Achievement.
        public DateTime CreatedTime { get; set; }
    }
}
