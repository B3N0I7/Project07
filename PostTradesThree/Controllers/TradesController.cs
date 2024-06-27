using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostTradesThree.Domain;
using PostTradesThree.Repositories;
using Serilog;

namespace PostTradesThree.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class TradesController : ControllerBase
    {
        private readonly ITradeRepository _tradeRepository;

        public TradesController(ITradeRepository tradeRepository)
        {
            _tradeRepository = tradeRepository;
        }

        [Authorize(Roles = "ADMIN, OWNER, USER")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Trade>>> GetAllTrades()
        {
            var trades = await _tradeRepository.GetAllTradesAsync();

            if (trades == null)
            {
                Log.Error("No trade found");

                return StatusCode(StatusCodes.Status404NotFound);
            }

            Log.Information($"List of Trades retrieved successfully at {DateTime.UtcNow.ToLocalTime()}");

            return Ok(trades);
        }

        [Authorize(Roles = "ADMIN, OWNER, USER")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Trade>> GetOneTrade(int id)
        {
            var trade = await _tradeRepository.GetTradeByIdAsync(id);

            if (trade == null)
            {
                Log.Error($"Trade {id} not found");

                return NotFound();
            }

            Log.Information($"Trade {id} retrieved successfully at {DateTime.UtcNow.ToLocalTime()}");

            return Ok(trade);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Trade>> CreateOneTrade(Trade trade)
        {
            if (trade == null)
            {
                Log.Error("Invalid trade data");

                return NotFound();
            }

            await _tradeRepository.CreateTradeAsync(trade);

            Log.Information($"New Trade created successfully at {DateTime.UtcNow.ToLocalTime()}");

            return CreatedAtAction(nameof(GetOneTrade), new { id = trade.TradeId }, trade);
        }

        [Authorize(Roles = "ADMIN, OWNER")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateOneTrade(int id, Trade trade)
        {
            if (id != trade?.TradeId)
            {
                Log.Error($"Trade {id} mismatch");

                return NotFound();
            }

            await _tradeRepository.UpdateTradeAsync(trade);

            Log.Information($"Trade {id} updated successfully at {DateTime.UtcNow.ToLocalTime()}");

            return Ok();
        }

        [Authorize(Roles = "ADMIN")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteOneTrade(int id)
        {
            await _tradeRepository.DeleteTradeByIdAsync(id);

            Log.Information($"Trade {id} deleted successfully at {DateTime.UtcNow.ToLocalTime()}");

            return NoContent();
        }
    }
}
