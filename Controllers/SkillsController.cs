using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Data;
using PortfolioAPI.Models;
using PortfolioAPI.Services;

namespace PortfolioAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillService _service;

        public SkillsController(ISkillService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllSkillsAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var skill = await _service.GetSkillByIdAsync(id);

            if (skill == null)
                return NotFound();

            return Ok(skill);
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(Skill skill)
        {
            var created = await _service.AddSkillAsync(skill);

            return CreatedAtAction(
                nameof(GetById),
                new { id = created.Id },
                created);
        }


        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Skill skill)
        {
            skill.Id = id;

            var updated = await _service.UpdateSkillAsync(skill);

            if (updated == null)
                return NotFound();

            return Ok(updated);
        }


        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteSkillAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
