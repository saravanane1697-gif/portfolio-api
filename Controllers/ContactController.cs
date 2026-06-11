using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Models;
using PortfolioAPI.Services;

namespace PortfolioAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _service;

        public ContactController(IContactService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllMessagesAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create(ContactMessage message)
        {
            message.CreatedAt = DateTime.UtcNow;
            message.IsRead = false;

            var result =
                await _service.AddMessageAsync(message);

            return Ok(result);
        }

        [Authorize]
        [HttpPut("{id}/mark-read")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var result =
                await _service.MarkAsReadAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted =
                await _service.DeleteMessageAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
