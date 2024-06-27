using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostTradesThree.Domain;
using PostTradesThree.Repositories;
using Serilog;

namespace PostTradesThree.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class CurvePointsController : ControllerBase
    {
        private readonly ICurvePointRepository _curvePointRepository;

        public CurvePointsController(ICurvePointRepository curvePointRepository)
        {
            _curvePointRepository = curvePointRepository;
        }

        [Authorize(Roles = "ADMIN, OWNER, USER")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<CurvePoint>>> GetAllCurvePoints()
        {
            var curvePoints = await _curvePointRepository.GetAllCurvePointsAsync();

            if (curvePoints == null)
            {
                Log.Error("No curvePoint found");

                return StatusCode(StatusCodes.Status404NotFound);
            }

            Log.Information($"List of CurvePoints retrieved successfully at {DateTime.UtcNow.ToLocalTime()}");

            return Ok(curvePoints);
        }

        [Authorize(Roles = "ADMIN, OWNER, USER")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CurvePoint>> GetOneCurvePoint(int id)
        {
            var curvePoint = await _curvePointRepository.GetCurvePointByIdAsync(id);

            if (curvePoint == null)
            {
                Log.Error($"CurvePoint {id} not found");

                return NotFound();
            }

            Log.Information($"CurvePoint {id} retrieved successfully at {DateTime.UtcNow.ToLocalTime()}");

            return Ok(curvePoint);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CurvePoint>> CreateOneCurvePoint(CurvePoint curvePoint)
        {
            if (curvePoint == null)
            {
                Log.Error("Invalid curvePoint data");

                return NotFound();
            }

            await _curvePointRepository.CreateCurvePointAsync(curvePoint);

            Log.Information($"New CurvePoint created successfully at {DateTime.UtcNow.ToLocalTime()}");

            return CreatedAtAction(nameof(GetOneCurvePoint), new { id = curvePoint.CurvePointId }, curvePoint);
        }

        [Authorize(Roles = "ADMIN, OWNER")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateOneCurvePoint(int id, CurvePoint curvePoint)
        {
            if (id != curvePoint?.CurvePointId)
            {
                Log.Error($"CurvePoint {id} mismatch");

                return NotFound();
            }

            await _curvePointRepository.UpdateCurvePointAsync(curvePoint);

            Log.Information($"CurvePoint {id} updated successfully at {DateTime.UtcNow.ToLocalTime()}");

            return Ok();
        }

        [Authorize(Roles = "ADMIN")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteOneCurvePoint(int id)
        {
            await _curvePointRepository.DeleteCurvePointByIdAsync(id);

            Log.Information($"CurvePoint {id} deleted successfully at {DateTime.UtcNow.ToLocalTime()}");

            return NoContent();
        }
    }
}
