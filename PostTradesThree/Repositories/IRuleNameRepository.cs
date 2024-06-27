using PostTradesThree.Domain;

namespace PostTradesThree.Repositories
{
    public interface IRuleNameRepository
    {
        Task<IEnumerable<RuleName>> GetAllRuleNamesAsync();
        Task<RuleName> GetRuleNameByIdAsync(int id);
        Task<int> CreateRuleNameAsync(RuleName ruleName);
        Task UpdateRuleNameAsync(RuleName ruleName);
        Task<int> DeleteRuleNameByIdAsync(int id);
    }
}
