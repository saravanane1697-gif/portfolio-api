using PortfolioAPI.DTOs;

namespace PortfolioAPI.Services
{
    public interface IDashboardService
    {
        Task<DashboardStatsDto> GetStatsAsync();
    }
}
