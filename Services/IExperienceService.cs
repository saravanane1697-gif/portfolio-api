using PortfolioAPI.Models;

namespace PortfolioAPI.Services
{
    public interface IExperienceService
    {
        Task<List<Experience>> GetAllExperiencesAsync();
        Task<Experience?> GetExperienceByIdAsync(int id);
        Task<Experience> AddExperienceAsync(Experience experience);
        Task<Experience?> UpdateExperienceAsync(Experience experience);
        Task<bool> DeleteExperienceAsync(int id);
    }
}
