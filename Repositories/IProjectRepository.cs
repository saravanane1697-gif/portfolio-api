using PortfolioAPI.Models;

namespace PortfolioAPI.Repositories
{
    public interface IProjectRepository
    {
        Task<List<Project>> GetAllAsync();

        Task<Project?> GetByIdAsync(int id);

        Task<Project> AddAsync(Project project);

        Task<Project?> UpdateAsync(Project project);

        Task<bool> DeleteAsync(int id);
    }
}
