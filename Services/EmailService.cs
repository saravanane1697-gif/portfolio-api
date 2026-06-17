using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace PortfolioAPI.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public EmailService(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var apiKey = _configuration["Resend:ApiKey"];
            var fromEmail = _configuration["Resend:FromEmail"]; // e.g. "onboarding@resend.dev"

            var payload = new
            {
                from = $"Portfolio Website <{fromEmail}>",
                to = new[] { toEmail },
                subject = subject,
                html = body
            };

            var json = JsonSerializer.Serialize(payload);
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.resend.com/emails")
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Resend API error: {response.StatusCode} - {error}");
                throw new Exception($"Failed to send email: {response.StatusCode}");
            }
        }
    }
}
