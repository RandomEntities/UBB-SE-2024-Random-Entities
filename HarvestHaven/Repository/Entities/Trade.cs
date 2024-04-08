namespace HarvestHaven.Repository.Entities
{
    public class Trade
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; } // User.
        public Guid GivenResourceId { get; set; } // Resource.
        public int GivenResourceQuantity { get; set; }
        public Guid RequestedResourceId { get; set; } // User.
        public int RequestedResourceQuantity { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
