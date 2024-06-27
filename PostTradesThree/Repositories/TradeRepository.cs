using Microsoft.EntityFrameworkCore;
using PostTradesThree.Data;
using PostTradesThree.Domain;

namespace PostTradesThree.Repositories
{
    public class TradeRepository : ITradeRepository
    {
        private readonly PttDbContext _pttDbContext;

        public TradeRepository(PttDbContext pttDbContext)
        {
            _pttDbContext = pttDbContext;
        }
        public async Task<IEnumerable<Trade>> GetAllTradesAsync()
        {
            return await _pttDbContext.Trades.ToListAsync();
        }

        public async Task<Trade> GetTradeByIdAsync(int id)
        {
            return await _pttDbContext.Trades.FindAsync(id) ?? throw new InvalidOperationException();
        }

        public async Task<int> CreateTradeAsync(Trade trade)
        {
            _pttDbContext.Trades.Add(trade);

            return await _pttDbContext.SaveChangesAsync();
        }

        public async Task UpdateTradeAsync(Trade trade)
        {
            _pttDbContext.Entry(trade).State = EntityState.Modified;

            await _pttDbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteTradeByIdAsync(int id)
        {
            var trade = await _pttDbContext.Trades.FindAsync(id);
            if (trade != null)
            {
                _pttDbContext.Trades.Remove(trade);
            }

            return await _pttDbContext.SaveChangesAsync();
        }
    }
}
