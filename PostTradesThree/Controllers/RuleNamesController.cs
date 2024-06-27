using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostTradesThree.Domain;
using PostTradesThree.Repositories;
using Serilog;

namespace PostTradesThree.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class RuleNamesController : ControllerBase
    {
        private readonly IRuleNameRepository _ruleNameRepository;

        public RuleNamesController(IRuleNameRepository ruleNameRepository)
        {
            _ruleNameRepository = ruleNameRepository;
        }

        [Authorize(Roles = "ADMIN, OWNER, USER")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<RuleName>>> GetAllRuleNames()
        {
            var ruleNames = await _ruleNameRepository.GetAllRuleNamesAsync();

            if (ruleNames == null)
            {
                Log.Error("No ruleName found");

                return StatusCode(StatusCodes.Status404NotFound);
            }

            Log.Information($"List of RuleNames retrieved successfully at {DateTime.UtcNow.ToLocalTime()}");

            return Ok(ruleNames);
        }

        [Authorize(Roles = "ADMIN, OWNER, USER")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RuleName>> GetOneRuleName(int id)
        {
            var ruleName = await _ruleNameRepository.GetRuleNameByIdAsync(id);

            if (ruleName == null)
            {
                Log.Error($"RuleName {id} not found");

                return NotFound();
            }

            Log.Information($"RuleName {id} retrieved successfully at {DateTime.UtcNow.ToLocalTime()}");

            return Ok(ruleName);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RuleName>> CreateOneRuleName(RuleName ruleName)
        {
            if (ruleName == null)
            {
                Log.Error("Invalid ruleName data");

                return NotFound();
            }

            await _ruleNameRepository.CreateRuleNameAsync(ruleName);

            Log.Information($"New RuleName created successfully at {DateTime.UtcNow.ToLocalTime()}");

            return CreatedAtAction(nameof(GetOneRuleName), new { id = ruleName.RuleNameId }, ruleName);
        }

        [Authorize(Roles = "ADMIN, OWNER")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateOneRuleName(int id, RuleName ruleName)
        {
            if (id != ruleName?.RuleNameId)
            {
                Log.Error($"RuleName {id} mismatch");

                return NotFound();
            }

            await _ruleNameRepository.UpdateRuleNameAsync(ruleName);

            Log.Information($"RuleName {id} updated successfully at {DateTime.UtcNow.ToLocalTime()}");

            return Ok();
        }

        [Authorize(Roles = "ADMIN")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteOneRuleName(int id)
        {
            await _ruleNameRepository.DeleteRuleNameByIdAsync(id);

            Log.Information($"RuleName {id} deleted successfully at {DateTime.UtcNow.ToLocalTime()}");

            return NoContent();
        }
    }
}
