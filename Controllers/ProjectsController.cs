using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortfolioAPI.Models;
using PortfolioAPI.Services;

namespace PortfolioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _service;

        public ProjectsController(IProjectService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllProjectsAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var project = await _service.GetProjectByIdAsync(id);

            if (project == null)
                return NotFound();

            return Ok(project);
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(Project project)
        {
            var created = await _service.AddProjectAsync(project);

            return CreatedAtAction(
                nameof(GetById),
                new { id = created.Id },
                created);
        }


        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Project project)
        {
            project.Id = id;

            var updated = await _service.UpdateProjectAsync(project);

            if (updated == null)
                return NotFound();

            return Ok(updated);
        }


        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteProjectAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
