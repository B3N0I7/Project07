using Microsoft.EntityFrameworkCore;
using PostTradesThree.Data;
using PostTradesThree.Domain;

namespace PostTradesThree.Repositories
{
    public class RuleNameRepository : IRuleNameRepository
    {
        private readonly PttDbContext _pttDbContext;

        public RuleNameRepository(PttDbContext pttDbContext)
        {
            _pttDbContext = pttDbContext;
        }
        public async Task<IEnumerable<RuleName>> GetAllRuleNamesAsync()
        {
            return await _pttDbContext.RuleNames.ToListAsync();
        }

        public async Task<RuleName> GetRuleNameByIdAsync(int id)
        {
            return await _pttDbContext.RuleNames.FindAsync(id) ?? throw new InvalidOperationException();
        }

        public async Task<int> CreateRuleNameAsync(RuleName ruleName)
        {
            _pttDbContext.RuleNames.Add(ruleName);

            return await _pttDbContext.SaveChangesAsync();
        }

        public async Task UpdateRuleNameAsync(RuleName ruleName)
        {
            _pttDbContext.Entry(ruleName).State = EntityState.Modified;

            await _pttDbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteRuleNameByIdAsync(int id)
        {
            var ruleName = await _pttDbContext.RuleNames.FindAsync(id);
            if (ruleName != null)
            {
                _pttDbContext.RuleNames.Remove(ruleName);
            }

            return await _pttDbContext.SaveChangesAsync();
        }
    }
}
