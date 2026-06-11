using PortfolioAPI.Models;

namespace PortfolioAPI.Services
{
    public interface IContactService
    {
        Task<List<ContactMessage>> GetAllMessagesAsync();

        Task<ContactMessage?> GetMessageByIdAsync(int id);

        Task<ContactMessage> AddMessageAsync(ContactMessage message);

        Task<ContactMessage?> MarkAsReadAsync(int id);

        Task<bool> DeleteMessageAsync(int id);
    }
}
