using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Data;
using PortfolioAPI.Models;

namespace PortfolioAPI.Repositories
{
    public class ExperienceRepository : IExperienceRepository
    {
        private readonly PortfolioDbContext _context;

        public ExperienceRepository(PortfolioDbContext context)
        {
            _context = context;
        }

        public async Task<List<Experience>> GetAllAsync()
        {
            return await _context.Experiences.ToListAsync();
        }

        public async Task<Experience?> GetByIdAsync(int id)
        {
            return await _context.Experiences.FindAsync(id);
        }

        public async Task<Experience> AddAsync(Experience experience)
        {
            _context.Experiences.Add(experience);
            await _context.SaveChangesAsync();
            return experience;
        }

        public async Task<Experience?> UpdateAsync(Experience experience)
        {
            var existing = await _context.Experiences.FindAsync(experience.Id);

            if (existing == null)
                return null;

            existing.Company = experience.Company;
            existing.Role = experience.Role;
            existing.Duration = experience.Duration;
            existing.Description = experience.Description;

            await _context.SaveChangesAsync();

            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var experience = await _context.Experiences.FindAsync(id);

            if (experience == null)
                return false;

            _context.Experiences.Remove(experience);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
