using PostTradesThree.Domain;

namespace PostTradesThree.Repositories
{
    public interface ITradeRepository
    {
        Task<IEnumerable<Trade>> GetAllTradesAsync();
        Task<Trade> GetTradeByIdAsync(int id);
        Task<int> CreateTradeAsync(Trade trade);
        Task UpdateTradeAsync(Trade trade);
        Task<int> DeleteTradeByIdAsync(int id);
    }
}
