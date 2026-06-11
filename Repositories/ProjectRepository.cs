using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Data;
using PortfolioAPI.Models;

namespace PortfolioAPI.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly PortfolioDbContext _context;

        public ProjectRepository(PortfolioDbContext context)
        {
            _context = context;
        }

        public async Task<List<Project>> GetAllAsync()
        {
            return await _context.Projects.ToListAsync();
        }

        public async Task<Project?> GetByIdAsync(int id)
        {
            return await _context.Projects.FindAsync(id);
        }

        public async Task<Project> AddAsync(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<Project?> UpdateAsync(Project project)
        {
            var existing = await _context.Projects.FindAsync(project.Id);

            if (existing == null)
                return null;

            existing.Title = project.Title;
            existing.Description = project.Description;
            existing.GitHubUrl = project.GitHubUrl;
            existing.DemoUrl = project.DemoUrl;

            await _context.SaveChangesAsync();

            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var project = await _context.Projects.FindAsync(id);

            if (project == null)
                return false;

            _context.Projects.Remove(project);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
