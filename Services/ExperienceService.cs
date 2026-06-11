using PortfolioAPI.Models;
using PortfolioAPI.Repositories;

namespace PortfolioAPI.Services
{
    public class ExperienceService : IExperienceService
    {
        private readonly IExperienceRepository _repository;

        public ExperienceService(IExperienceRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Experience>> GetAllExperiencesAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Experience?> GetExperienceByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Experience> AddExperienceAsync(Experience experience)
        {
            return await _repository.AddAsync(experience);
        }

        public async Task<Experience?> UpdateExperienceAsync(Experience experience)
        {
            return await _repository.UpdateAsync(experience);
        }

        public async Task<bool> DeleteExperienceAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
