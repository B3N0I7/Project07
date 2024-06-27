using PostTradesThree.Domain;

namespace PostTradesThree.Repositories
{
    public interface IRatingRepository
    {
        Task<IEnumerable<Rating>> GetAllRatingsAsync();
        Task<Rating> GetRatingByIdAsync(int id);
        Task<int> CreateRatingAsync(Rating rating);
        Task UpdateRatingAsync(Rating rating);
        Task<int> DeleteRatingByIdAsync(int id);
    }
}
