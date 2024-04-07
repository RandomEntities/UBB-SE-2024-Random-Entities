namespace HarvestHaven.Repository.Entities
{
    public class Resource
    {
        public Guid Id { get; set; }
        public ResourceType ResourceType { get; set; }
    }

    public enum ResourceType
    {
        Wheat,
        Corn,
        Carrot,
        Tomato,
        ChickenEgg,
        DuckEgg,
        SheepWool,
        CowMilk,
        ChickenMeat,
        DuckMeat,
        Mutton,
        Steak
    }
}
