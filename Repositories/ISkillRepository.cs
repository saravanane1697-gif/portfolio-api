using PortfolioAPI.Models;

namespace PortfolioAPI.Repositories
{
    public interface ISkillRepository
    {
        Task<List<Skill>> GetAllAsync();
        Task<Skill?> GetByIdAsync(int id);
        Task<Skill> AddAsync(Skill skill);
        Task<Skill?> UpdateAsync(Skill skill);
        Task<bool> DeleteAsync(int id);
    }
}
