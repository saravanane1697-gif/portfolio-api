using PortfolioAPI.Models;
using PortfolioAPI.Repositories;

namespace PortfolioAPI.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _repository;

        public ProjectService(IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Project>> GetAllProjectsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Project?> GetProjectByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Project> AddProjectAsync(Project project)
        {
            return await _repository.AddAsync(project);
        }

        public async Task<Project?> UpdateProjectAsync(Project project)
        {
            return await _repository.UpdateAsync(project);
        }

        public async Task<bool> DeleteProjectAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
