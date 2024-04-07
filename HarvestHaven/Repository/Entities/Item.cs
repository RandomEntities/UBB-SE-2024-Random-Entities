namespace HarvestHaven.Repository.Entities
{
    public class Item
    {
        public Guid Id { get; set; }
        public ItemType ItemType { get; set; }
        public Guid RequiredResourceId { get; set; } // Resource.
        public Guid InteractResourceId { get; set; } // Resource.
        public Guid? DestroyResourceId { get; set; } // Nullable Resource.
    }

    public enum ItemType
    { 
        Chicken,
        Cow,
        Sheep,
        Duck,
        WheatSeeds,
        CornSeeds,
        CarrotSeeds,
        TomatoSeeds
    }
}
