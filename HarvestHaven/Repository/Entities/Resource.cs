﻿namespace HarvestHaven.Repository.Entities
{
    public class Resource
    {
        public Guid Id { get; set; }
        public ResourceType ResourceType { get; set; }
    }

    public enum ResourceType
    {
        Water,
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
