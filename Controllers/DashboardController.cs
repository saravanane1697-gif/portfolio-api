using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortfolioAPI.Services;

namespace PortfolioAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _service;

        public DashboardController(
            IDashboardService service)
        {
            _service = service;
        }

        [HttpGet("stats")]
        public async Task<IActionResult> GetStats()
        {
            var stats =
                await _service.GetStatsAsync();

            return Ok(stats);
        }
    }
}
