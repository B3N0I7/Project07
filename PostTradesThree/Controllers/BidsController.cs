using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostTradesThree.Domain;
using PostTradesThree.Repositories;
using Serilog;

namespace PostTradesThree.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class BidsController : ControllerBase
    {
        private readonly IBidRepository _bidRepository;

        public BidsController(IBidRepository bidRepository)
        {
            _bidRepository = bidRepository;
        }

        [Authorize(Roles = "ADMIN, OWNER, USER")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Bid>>> GetAllBids()
        {
            var bids = await _bidRepository.GetAllBidsAsync();

            if (bids == null)
            {
                Log.Error("No bid found");

                return StatusCode(StatusCodes.Status404NotFound);
            }

            Log.Information($"List of Bids retrieved successfully at {DateTime.UtcNow.ToLocalTime()}");

            return Ok(bids);
        }

        [Authorize(Roles = "ADMIN, OWNER, USER")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Bid>> GetOneBid(int id)
        {
            var bid = await _bidRepository.GetBidByIdAsync(id);

            if (bid == null)
            {
                Log.Error($"Bid {id} not found");

                return NotFound();
            }

            Log.Information($"Bid {id} retrieved successfully at {DateTime.UtcNow.ToLocalTime()}");

            return Ok(bid);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Bid>> CreateOneBid(Bid bid)
        {
            if (bid == null)
            {
                Log.Error("Invalid bid data");

                return NotFound();
            }

            await _bidRepository.CreateBidAsync(bid);

            Log.Information($"New Bid created successfully at {DateTime.UtcNow.ToLocalTime()}");

            return CreatedAtAction(nameof(GetOneBid), new { id = bid.BidId }, bid);
            //return StatusCode(StatusCodes.Status200OK);
            //return CreatedAtRoute("GetBid", new { id = bid.BidId }, bid);
        }

        [Authorize(Roles = "ADMIN, OWNER")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateOneBid(int id, Bid bid)
        {
            if (id != bid?.BidId)
            {
                Log.Error($"Bid {id} mismatch");

                return NotFound();
            }

            await _bidRepository.UpdateBidAsync(bid);

            Log.Information($"Bid {id} updated successfully at {DateTime.UtcNow.ToLocalTime()}");

            return Ok();
        }

        [Authorize(Roles = "ADMIN")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteOneBid(int id)
        {
            await _bidRepository.DeleteBidByIdAsync(id);

            Log.Information($"Bid {id} deleted successfully at {DateTime.UtcNow.ToLocalTime()}");

            return NoContent();
        }
    }
}
