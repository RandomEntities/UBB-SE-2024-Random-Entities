namespace HarvestHaven.Repository.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Coins { get; set; }
        public DateTime TradeHallUnlockTime { get; set; }
    }
}
