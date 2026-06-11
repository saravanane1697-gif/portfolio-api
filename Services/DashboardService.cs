using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Data;
using PortfolioAPI.DTOs;

namespace PortfolioAPI.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly PortfolioDbContext _context;

        public DashboardService(PortfolioDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardStatsDto> GetStatsAsync()
        {
            return new DashboardStatsDto
            {
                ProjectsCount =
                    await _context.Projects.CountAsync(),

                SkillsCount =
                    await _context.Skills.CountAsync(),

                ExperiencesCount =
                    await _context.Experiences.CountAsync(),

                MessagesCount =
                    await _context.ContactMessages.CountAsync(),

                UnreadMessagesCount =
                    await _context.ContactMessages
                        .CountAsync(x => !x.IsRead)
            };
        }
    }
}
