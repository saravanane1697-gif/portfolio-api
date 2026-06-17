using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
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
        private readonly Cloudinary _cloudinary;

        public ProfileController(PortfolioDbContext context, Cloudinary cloudinary)
        {
            _context = context;
            _cloudinary = cloudinary;
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
            if (file == null) return BadRequest("File is null");

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                Folder = "portfolio/photos"
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.Error != null)
                return BadRequest(uploadResult.Error.Message);

            var imageUrl = uploadResult.SecureUrl.ToString();

            var profile = await _context.Profiles.FirstOrDefaultAsync();
            if (profile != null)
            {
                profile.ImageUrl = imageUrl;
                await _context.SaveChangesAsync();
            }

            return Ok(new { imageUrl });
        }

        [Authorize]
        [HttpPost("upload-resume")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadResume(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var uploadParams = new RawUploadParams
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                Folder = "portfolio/resumes"
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.Error != null)
                return BadRequest(uploadResult.Error.Message);

            var resumeUrl = uploadResult.SecureUrl.ToString();

            var profile = await _context.Profiles.FirstOrDefaultAsync();
            if (profile != null)
            {
                profile.ResumeUrl = resumeUrl;
                await _context.SaveChangesAsync();
            }

            return Ok(new { resumeUrl });
        }

        [Authorize]
        [HttpGet("secure")]
        public IActionResult Secure() => Ok("JWT Works");
    }
}
