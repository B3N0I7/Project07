using PostTradesThree.Domain;

namespace PostTradesThree.Repositories
{
    public interface IBidRepository
    {
        Task<IEnumerable<Bid>> GetAllBidsAsync();
        Task<Bid> GetBidByIdAsync(int id);
        Task<int> CreateBidAsync(Bid bid);
        Task UpdateBidAsync(Bid bid);
        Task<int> DeleteBidByIdAsync(int id);
    }
}
