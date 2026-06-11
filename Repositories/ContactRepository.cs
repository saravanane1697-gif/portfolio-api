using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Data;
using PortfolioAPI.Models;

namespace PortfolioAPI.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly PortfolioDbContext _context;

        public ContactRepository(PortfolioDbContext context)
        {
            _context = context;
        }

        public async Task<List<ContactMessage>> GetAllAsync()
        {
            return await _context.ContactMessages
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<ContactMessage?> GetByIdAsync(int id)
        {
            return await _context.ContactMessages
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ContactMessage> AddAsync(ContactMessage message)
        {
            _context.ContactMessages.Add(message);

            await _context.SaveChangesAsync();

            return message;
        }

        public async Task<ContactMessage?> MarkAsReadAsync(int id)
        {
            var message = await _context.ContactMessages
                .FindAsync(id);

            if (message == null)
                return null;

            message.IsRead = true;

            await _context.SaveChangesAsync();

            return message;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var message = await _context.ContactMessages
                .FindAsync(id);

            if (message == null)
                return false;

            _context.ContactMessages.Remove(message);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
