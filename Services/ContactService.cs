using PortfolioAPI.Models;
using PortfolioAPI.Repositories;

namespace PortfolioAPI.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _repository;
        private readonly IEmailService _emailService;

        public ContactService(IContactRepository repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }

        public async Task<List<ContactMessage>> GetAllMessagesAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<ContactMessage?> GetMessageByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<ContactMessage> AddMessageAsync(ContactMessage message)
        {
            var result = await _repository.AddAsync(message);

            // Notify you
            await _emailService.SendEmailAsync(
                "saravanane1697@gmail.com",
                "New Portfolio Contact Message",
                $@"
                <h2>New Portfolio Contact Message</h2>

                <p><b>Name:</b> {message.Name}</p>
                <p><b>Email:</b> {message.Email}</p>
                <p><b>Subject:</b> {message.Subject}</p>
                <p><b>Message:</b> {message.Message}</p>
                ");

                    // Auto reply to visitor
                    await _emailService.SendEmailAsync(
                        message.Email,
                        "Thank you for contacting me",
                        $@"
                <h2>Hello {message.Name},</h2>

                <p>
                    Thank you for contacting me through my portfolio website.
                </p>

                <p>
                    I have received your message and will get back to you soon.
                </p>

                <br/>

                <p>
                    Regards,<br/>
                    Saravanan Elangovan<br/>
                    .NET Full Stack Developer
                </p>
                ");

            return result;
        }

        public async Task<ContactMessage?> MarkAsReadAsync(int id)
        {
            return await _repository.MarkAsReadAsync(id);
        }

        public async Task<bool> DeleteMessageAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
