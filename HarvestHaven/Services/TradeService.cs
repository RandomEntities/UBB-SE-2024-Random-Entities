using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HarvestHaven.Repository.Entities;
using HarvestHaven.Repository.Repositories;

namespace HarvestHaven.Services
{
    public class TradeService
    {
        private readonly TradeRepository _tradeRepository;

        public TradeService()
        {
            _tradeRepository = new TradeRepository();
        }

        public async Task<List<Trade>> GetAllTradesAsync()
        {
            return await _tradeRepository.GetAllTradesAsync();
        }

        public async Task<Trade> GetUserTradeAsync(Guid userId)
        {
            return await _tradeRepository.GetUserTradeAsync(userId);
        }

        public async Task CreateTradeAsync(Guid givenResourceId, int givenResourceQuantity, Guid requestedResourceId, int requestedResourceQuantity)
        {
            //await _tradeRepository.CreateTradeAsync(trade);
        }

        public async Task PerformTradeAsync(Guid tradeId)
        {
            //await _tradeRepository.PerformTradeAsync(tradeId);
        }
    }
}
