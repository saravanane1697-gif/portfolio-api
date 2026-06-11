using PortfolioAPI.Models;

namespace PortfolioAPI.Services
{
    public interface ISkillService
    {
        Task<List<Skill>> GetAllSkillsAsync();
        Task<Skill?> GetSkillByIdAsync(int id);
        Task<Skill> AddSkillAsync(Skill skill);
        Task<Skill?> UpdateSkillAsync(Skill skill);
        Task<bool> DeleteSkillAsync(int id);
    }
}
