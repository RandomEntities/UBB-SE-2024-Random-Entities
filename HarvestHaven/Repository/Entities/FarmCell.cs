namespace HarvestHaven.Repository.Entities
{
    public class FarmCell
    {
        public Guid Id { get; set; }
        public Guid FarmId { get; set; } // Farm.
        public int Row { get; set; }
        public int Column { get; set; }
        public Guid ItemId {  get; set; } // Item.
        public DateTime? LastTimeEnhanced { get; set; } // Nullable.
        public DateTime? LastTimeInteracted { get; set; } // Nullable.
    }
}
