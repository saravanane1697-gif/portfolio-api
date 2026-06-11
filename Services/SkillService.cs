using PortfolioAPI.Models;
using PortfolioAPI.Repositories;

namespace PortfolioAPI.Services
{
    public class SkillService : ISkillService
    {
        private readonly ISkillRepository _repository;

        public SkillService(ISkillRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Skill>> GetAllSkillsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Skill?> GetSkillByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Skill> AddSkillAsync(Skill skill)
        {
            return await _repository.AddAsync(skill);
        }

        public async Task<Skill?> UpdateSkillAsync(Skill skill)
        {
            return await _repository.UpdateAsync(skill);
        }

        public async Task<bool> DeleteSkillAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
