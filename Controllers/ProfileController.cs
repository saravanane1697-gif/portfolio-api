using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Data;

namespace PortfolioAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly PortfolioDbContext _context;

        public ProfileController(PortfolioDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var profile = await _context.Profiles.FirstOrDefaultAsync();

            return Ok(profile);
        }

        [Authorize]
        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadPhoto(IFormFile file)
        {
            Console.WriteLine("Upload endpoint hit");
            //if (file == null || file.Length == 0)
            //    return BadRequest("No file uploaded.");
            if (file == null)
            {
                return BadRequest("File is null");
            }

            var uploadsFolder =
                Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "uploads");

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var fileName =
                $"{Guid.NewGuid()}_{file.FileName}";

            var filePath =
                Path.Combine(
                    uploadsFolder,
                    fileName);

            using (var stream =
                   new FileStream(
                       filePath,
                       FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var profile =
                await _context.Profiles
                    .FirstOrDefaultAsync();

            if (profile != null)
            {
                profile.ImageUrl =
                    $"/uploads/{fileName}";

                await _context.SaveChangesAsync();
            }

            return Ok(new
            {
                imageUrl =
                    $"/uploads/{fileName}"
            });
        }

        [Authorize]
        [HttpGet("secure")]
        public IActionResult Secure()
        {
            return Ok("JWT Works");
        }


        [Authorize]
        [HttpPost("upload-resume")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadResume(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var uploadsFolder =
                Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "resumes");

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var fileName =
                $"{Guid.NewGuid()}_{file.FileName}";

            var filePath =
                Path.Combine(
                    uploadsFolder,
                    fileName);

            using (var stream =
                   new FileStream(
                       filePath,
                       FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var profile =
                await _context.Profiles
                    .FirstOrDefaultAsync();

            if (profile != null)
            {
                profile.ResumeUrl =
                    $"/resumes/{fileName}";

                await _context.SaveChangesAsync();
            }

            return Ok(new
            {
                resumeUrl =
                    $"/resumes/{fileName}"
            });
        }
    }
}
