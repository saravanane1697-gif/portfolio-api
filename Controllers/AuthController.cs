using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PortfolioAPI.Data;
using PortfolioAPI.DTOs;
using PortfolioAPI.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PortfolioAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly PortfolioDbContext _context;
        private readonly IEmailService _emailService;

        public AuthController(
            IConfiguration configuration,
            PortfolioDbContext context, IEmailService emailService)
        {
            _configuration = configuration;
            _context = context;
            _emailService = emailService;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto login)
        {
            var user = _context.AdminUsers.FirstOrDefault(
                x => x.Username == login.Username
                  && x.Password == login.Password);

            if (user == null)
                return Unauthorized();

            var claims = new[]
            {
                new Claim(
                    ClaimTypes.Name,
                    user.Username)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _configuration["Jwt:Key"]));

            var creds = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(24),
                signingCredentials: creds);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler()
                    .WriteToken(token)
            });
        }

        //[HttpGet("test-email")]
        //public async Task<IActionResult> TestEmail()
        //{
        //    await _emailService.SendEmailAsync(
        //        "Portfolio Test",
        //        "<h1>Email Service Working</h1>");

        //    return Ok("Email Sent");
        //}
    }
}
