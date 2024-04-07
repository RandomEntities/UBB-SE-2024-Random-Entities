namespace HarvestHaven.Repository.Entities
{
    public class MarketBuyItem
    {
        public Guid Id { get; set; }
        public Guid ItemId { get; set; } // Item.
        public int BuyPrice { get; set; }
    }
}
