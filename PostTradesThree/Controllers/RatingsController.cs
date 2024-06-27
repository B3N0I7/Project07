using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostTradesThree.Domain;
using PostTradesThree.Repositories;
using Serilog;

namespace PostTradesThree.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly IRatingRepository _ratingRepository;

        public RatingsController(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        [Authorize(Roles = "ADMIN, OWNER, USER")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Rating>>> GetAllRatings()
        {
            var ratings = await _ratingRepository.GetAllRatingsAsync();

            if (ratings == null)
            {
                Log.Error("No rating found");

                return StatusCode(StatusCodes.Status404NotFound);
            }

            Log.Information($"List of Ratings retrieved successfully at {DateTime.UtcNow.ToLocalTime()}");

            return Ok(ratings);
        }

        [Authorize(Roles = "ADMIN, OWNER, USER")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Rating>> GetOneRating(int id)
        {
            var rating = await _ratingRepository.GetRatingByIdAsync(id);

            if (rating == null)
            {
                Log.Error($"Rating {id} not found");

                return NotFound();
            }

            Log.Information($"Rating {id} retrieved successfully at {DateTime.UtcNow.ToLocalTime()}");

            return Ok(rating);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Rating>> CreateOneRating(Rating rating)
        {
            if (rating == null)
            {
                Log.Error("Invalid rating data");

                return NotFound();
            }

            await _ratingRepository.CreateRatingAsync(rating);

            Log.Information($"New Rating created successfully at {DateTime.UtcNow.ToLocalTime()}");

            return CreatedAtAction(nameof(GetOneRating), new { id = rating.RatingId }, rating);
        }

        [Authorize(Roles = "ADMIN, OWNER")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateOneRating(int id, Rating rating)
        {
            if (id != rating?.RatingId)
            {
                Log.Error($"Rating {id} mismatch");

                return NotFound();
            }

            await _ratingRepository.UpdateRatingAsync(rating);

            Log.Information($"Rating {id} updated successfully at {DateTime.UtcNow.ToLocalTime()}");

            return Ok();
        }

        [Authorize(Roles = "ADMIN")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteOneRating(int id)
        {
            await _ratingRepository.DeleteRatingByIdAsync(id);

            Log.Information($"Rating {id} deleted successfully at {DateTime.UtcNow.ToLocalTime()}");

            return NoContent();
        }
    }
}
