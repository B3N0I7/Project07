using Microsoft.EntityFrameworkCore;
using PostTradesThree.Data;
using PostTradesThree.Domain;

namespace PostTradesThree.Repositories
{
    public class BidRepository : IBidRepository
    {
        private readonly PttDbContext _pttDbContext;

        public BidRepository(PttDbContext pttDbContext)
        {
            _pttDbContext = pttDbContext;
        }
        public async Task<IEnumerable<Bid>> GetAllBidsAsync()
        {
            return await _pttDbContext.Bids.ToListAsync();
        }

        public async Task<Bid> GetBidByIdAsync(int id)
        {
            return await _pttDbContext.Bids.FindAsync(id) ?? throw new InvalidOperationException();
        }

        public async Task<int> CreateBidAsync(Bid bid)
        {
            _pttDbContext.Bids.Add(bid);

            return await _pttDbContext.SaveChangesAsync();
        }

        public async Task UpdateBidAsync(Bid bid)
        {
            _pttDbContext.Entry(bid).State = EntityState.Modified;

            await _pttDbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteBidByIdAsync(int id)
        {
            var bid = await _pttDbContext.Bids.FindAsync(id);
            if (bid != null)
            {
                _pttDbContext.Bids.Remove(bid);
            }

            return await _pttDbContext.SaveChangesAsync();
        }
    }
}
