using PortfolioAPI.Models;

namespace PortfolioAPI.Repositories
{
    public interface IExperienceRepository
    {
        Task<List<Experience>> GetAllAsync();
        Task<Experience?> GetByIdAsync(int id);
        Task<Experience> AddAsync(Experience experience);
        Task<Experience?> UpdateAsync(Experience experience);
        Task<bool> DeleteAsync(int id);
    }
}
