namespace HarvestHaven.Repository.Entities
{
    public class MarketSellResource
    {
        public Guid Id { get; set; }
        public Guid ResourceId { get; set; }
        public int SellPrice { get; set; }
    }
}
