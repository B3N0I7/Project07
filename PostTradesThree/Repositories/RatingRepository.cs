using Microsoft.EntityFrameworkCore;
using PostTradesThree.Data;
using PostTradesThree.Domain;

namespace PostTradesThree.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly PttDbContext _pttDbContext;

        public RatingRepository(PttDbContext pttDbContext)
        {
            _pttDbContext = pttDbContext;
        }
        public async Task<IEnumerable<Rating>> GetAllRatingsAsync()
        {
            return await _pttDbContext.Ratings.ToListAsync();
        }

        public async Task<Rating> GetRatingByIdAsync(int id)
        {
            return await _pttDbContext.Ratings.FindAsync(id) ?? throw new InvalidOperationException();
        }

        public async Task<int> CreateRatingAsync(Rating rating)
        {
            _pttDbContext.Ratings.Add(rating);

            return await _pttDbContext.SaveChangesAsync();
        }

        public async Task UpdateRatingAsync(Rating rating)
        {
            _pttDbContext.Entry(rating).State = EntityState.Modified;

            await _pttDbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteRatingByIdAsync(int id)
        {
            var rating = await _pttDbContext.Ratings.FindAsync(id);
            if (rating != null)
            {
                _pttDbContext.Ratings.Remove(rating);
            }

            return await _pttDbContext.SaveChangesAsync();
        }
    }
}
