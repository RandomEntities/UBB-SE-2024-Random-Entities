namespace HarvestHaven.Repository.Entities
{
    public class Comment
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; } // User.
        public string Message { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
