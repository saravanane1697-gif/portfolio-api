using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortfolioAPI.Models;
using PortfolioAPI.Services;

namespace PortfolioAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExperienceController : ControllerBase
    {
        private readonly IExperienceService _service;

        public ExperienceController(IExperienceService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllExperiencesAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var experience = await _service.GetExperienceByIdAsync(id);

            if (experience == null)
                return NotFound();

            return Ok(experience);
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(Experience experience)
        {
            var created = await _service.AddExperienceAsync(experience);

            return CreatedAtAction(nameof(GetById),
                new { id = created.Id },
                created);
        }


        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Experience experience)
        {
            experience.Id = id;

            var updated = await _service.UpdateExperienceAsync(experience);

            if (updated == null)
                return NotFound();

            return Ok(updated);
        }


        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteExperienceAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
