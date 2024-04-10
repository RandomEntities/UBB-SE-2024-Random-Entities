namespace HarvestHaven.Repository.Entities
{
    public class InventoryResource
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; } // User.
        public Guid ResourceId { get; set; } // Resource.
        public int Quantity { get; set; } // Quantity of the resource.
    }
}
