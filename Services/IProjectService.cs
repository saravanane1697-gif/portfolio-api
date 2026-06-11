using PortfolioAPI.Models;

namespace PortfolioAPI.Services
{
    public interface IProjectService
    {
        Task<List<Project>> GetAllProjectsAsync();

        Task<Project?> GetProjectByIdAsync(int id);

        Task<Project> AddProjectAsync(Project project);

        Task<Project?> UpdateProjectAsync(Project project);

        Task<bool> DeleteProjectAsync(int id);
    }
}
