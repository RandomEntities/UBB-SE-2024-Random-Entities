using HarvestHaven.Entities;
using HarvestHaven.Repositories;

namespace HarvestHaven.Services
{
    public static class TradeService
    {
        public static async Task<List<Trade>> GetAllTradesAsync()
        {
            return await TradeRepository.GetAllTradesAsync();
        }

        public static async Task<Trade> GetUserTradeAsync(Guid userId)
        {
            return await TradeRepository.GetUserTradeAsync(userId);
        }

        public static async Task CreateTradeAsync(Guid givenResourceId, int givenResourceQuantity, Guid requestedResourceId, int requestedResourceQuantity)
        {
            //await TradeRepository.CreateTradeAsync(trade);
        }

        public static async Task PerformTradeAsync(Guid tradeId)
        {
            //await TradeRepository.PerformTradeAsync(tradeId);
        }
    }
}
