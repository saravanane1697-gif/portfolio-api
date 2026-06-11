using PortfolioAPI.Models;

namespace PortfolioAPI.Repositories
{
    public interface IContactRepository
    {
        Task<List<ContactMessage>> GetAllAsync();

        Task<ContactMessage?> GetByIdAsync(int id);

        Task<ContactMessage> AddAsync(ContactMessage message);

        Task<ContactMessage?> MarkAsReadAsync(int id);

        Task<bool> DeleteAsync(int id);
    }
}
