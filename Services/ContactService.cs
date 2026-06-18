using PortfolioAPI.Models;
using PortfolioAPI.Repositories;

namespace PortfolioAPI.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _repository;
        private readonly IServiceScopeFactory _scopeFactory;

        public ContactService(IContactRepository repository, IServiceScopeFactory scopeFactory)
        {
            _repository = repository;
            _scopeFactory = scopeFactory;
        }

        public async Task<List<ContactMessage>> GetAllMessagesAsync()
            => await _repository.GetAllAsync();

        public async Task<ContactMessage?> GetMessageByIdAsync(int id)
            => await _repository.GetByIdAsync(id);

        public async Task<ContactMessage> AddMessageAsync(ContactMessage message)
        {
            var result = await _repository.AddAsync(message);

            // Fire-and-forget with its own DI scope (avoids disposed HttpClient issue)
            _ = Task.Run(async () =>
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

                    await SendNotificationEmailsAsync(emailService, message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Background email task failed: {ex.Message}");
                }
            });

            return result;
        }

        private async Task SendNotificationEmailsAsync(IEmailService emailService, ContactMessage message)
        {
            try
            {
                await emailService.SendEmailAsync(
                    "saravanane1697@gmail.com",
                    "New Portfolio Contact Message",
                    $@"
        <h2>New Portfolio Contact Message</h2>
        <p><b>Name:</b> {message.Name}</p>
        <p><b>Email:</b> {message.Email}</p>
        <p><b>Subject:</b> {message.Subject}</p>
        <p><b>Message:</b> {message.Message}</p>
        ");

                await emailService.SendEmailAsync(
                    message.Email,
                    "Thank you for contacting me",
                    $@"
        <h2>Hello {message.Name},</h2>
        <p>Thank you for contacting me through my portfolio website.</p>
        <p>I have received your message and will get back to you soon.</p>
        <br/>
        <p>Regards,<br/>Saravanan Elangovan<br/>.NET Full Stack Developer</p>
        ");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Email sending failed: {ex.Message}");
            }
        }

        public async Task<ContactMessage?> MarkAsReadAsync(int id)
            => await _repository.MarkAsReadAsync(id);

        public async Task<bool> DeleteMessageAsync(int id)
            => await _repository.DeleteAsync(id);
    }
}
