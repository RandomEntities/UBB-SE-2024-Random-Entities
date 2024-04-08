namespace HarvestHaven.Repository.Entities
{
    public class InventoryResource
    {
        public Guid Id { get; set; }
        public Guid InventoryId { get; set; } // Inventory.
        public Guid ResourceId { get; set; } // Resource.
    }
}
